using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace kotor_Randomizer_2
{
    public partial class PresetForm : Form
    {
        public PresetForm()
        {
            InitializeComponent();

            foreach (string s in Properties.Settings.Default.PresetPaths)
            {
                PresetPaths.Add(new FileInfo(s));
            }
            lbPresetPaths.DataSource = PresetPaths;
            lbPresetPaths.DisplayMember = "Name";

            cbIncModu.Checked = Properties.Settings.Default.module_rando_active;
            cbIncItem.Checked = Properties.Settings.Default.item_rando_active;
            cbIncSound.Checked = Properties.Settings.Default.sound_rando_active;
            cbIncModel.Checked = Properties.Settings.Default.model_rando_active;
            cbIncTexture.Checked = Properties.Settings.Default.texture_rando_active;
            cbInc2da.Checked = Properties.Settings.Default.twoda_rando_active;
            cbIncText.Checked = Properties.Settings.Default.text_rando_active;
            cbIncOther.Checked = Properties.Settings.Default.other_rando_active;

        }

        private BindingList<FileInfo> PresetPaths = new BindingList<FileInfo>();

        private void update_listBox()
        {
            PresetPaths.Clear();
            foreach (string s in Properties.Settings.Default.PresetPaths)
            {
                PresetPaths.Add(new FileInfo(s));
            }
            lbPresetPaths.DataSource = PresetPaths;
        }

        private void bload_Click(object sender, EventArgs e)
        {
            if (ofdPresets.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.PresetPaths.Add(ofdPresets.FileName);
                update_listBox();
                if (KRP.ReadKRP(ofdPresets.OpenFile()))
                {
                    MessageBox.Show("Preset Loaded");
                }
            }
        }

        private void lbPresetPaths_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (KRP.ReadKRP((lbPresetPaths.SelectedItem as FileInfo).OpenRead()))
            {
                MessageBox.Show("Preset Loaded");
            }
        }

        private void lbPresetPaths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                lbPresetPaths_MouseDoubleClick(sender, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
            }
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)8)
            {
                if (MessageBox.Show("Are you sure you want to delete this Preset?", "Well?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string path = (lbPresetPaths.SelectedItem as FileInfo).FullName;
                    Properties.Settings.Default.PresetPaths.Remove(path);
                    ofdPresets.FileName = "";
                    sfdPresets.FileName = "";
                    new FileInfo(path).Delete();
                    update_listBox();
                }
            }
        }

        private void bsave_Click(object sender, EventArgs e)
        {
            //Category Booleans
            Properties.Settings.Default.module_rando_active = cbIncModu.Checked;
            Properties.Settings.Default.item_rando_active = cbIncItem.Checked;
            Properties.Settings.Default.sound_rando_active = cbIncSound.Checked;
            Properties.Settings.Default.model_rando_active = cbIncModel.Checked;
            Properties.Settings.Default.texture_rando_active = cbIncTexture.Checked;
            Properties.Settings.Default.twoda_rando_active = cbInc2da.Checked;
            Properties.Settings.Default.text_rando_active = cbIncText.Checked;
            Properties.Settings.Default.other_rando_active = cbIncOther.Checked;

            if (sfdPresets.ShowDialog() == DialogResult.OK)
            {
                KRP.WriteKRP(sfdPresets.OpenFile());

                Properties.Settings.Default.PresetPaths.Add(sfdPresets.FileName);
                update_listBox();
            }

        }
    }
}
