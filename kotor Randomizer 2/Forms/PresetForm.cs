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
        public PresetForm(string fn = "")
        {
            InitializeComponent();

            foreach (string s in Properties.Settings.Default.PresetPaths)
            {
                if (!File.Exists(s))
                {
                    Properties.Settings.Default.PresetPaths.Remove(s);
                    continue;
                }
                PresetPaths.Add(new FileInfo(s));
            }
            lbPresetPaths.DataSource = PresetPaths;
            lbPresetPaths.DisplayMember = "Name";

            if (fn != "" && File.Exists(fn))
            {
                if (!Properties.Settings.Default.PresetPaths.Contains(fn))
                {
                    Properties.Settings.Default.PresetPaths.Add(fn);
                }
                update_listBox();
                if (KRP.ReadKRP(File.OpenRead(fn)))
                {
                    MessageBox.Show(Properties.Resources.MsgPresetLoaded);
                }
            }

            cbIncModu.Checked = Properties.Settings.Default.DoRandomization_Module;
            cbIncItem.Checked = Properties.Settings.Default.DoRandomization_Item;
            cbIncSound.Checked = Properties.Settings.Default.DoRandomization_Sound;
            cbIncModel.Checked = Properties.Settings.Default.DoRandomization_Model;
            cbIncTexture.Checked = Properties.Settings.Default.DoRandomization_Texture;
            cbInc2da.Checked = Properties.Settings.Default.DoRandomization_TwoDA;
            cbIncText.Checked = Properties.Settings.Default.DoRandomization_Text;
            cbIncOther.Checked = Properties.Settings.Default.DoRandomization_Other;

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
                if (!Properties.Settings.Default.PresetPaths.Contains(ofdPresets.FileName))
                {
                    Properties.Settings.Default.PresetPaths.Add(ofdPresets.FileName);
                }
                update_listBox();
                if (KRP.ReadKRP(ofdPresets.OpenFile()))
                {
                    MessageBox.Show(Properties.Resources.MsgPresetLoaded);
                }
            }
        }

        private void lbPresetPaths_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (KRP.ReadKRP((lbPresetPaths.SelectedItem as FileInfo).OpenRead()))
            {
                MessageBox.Show(Properties.Resources.MsgPresetLoaded);
            }
        }

        private void lbPresetPaths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                lbPresetPaths_MouseDoubleClick(sender, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
            }
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
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
            // Category Booleans
            Properties.Settings.Default.DoRandomization_Module = cbIncModu.Checked;
            Properties.Settings.Default.DoRandomization_Item = cbIncItem.Checked;
            Properties.Settings.Default.DoRandomization_Sound = cbIncSound.Checked;
            Properties.Settings.Default.DoRandomization_Model = cbIncModel.Checked;
            Properties.Settings.Default.DoRandomization_Texture = cbIncTexture.Checked;
            Properties.Settings.Default.DoRandomization_TwoDA = cbInc2da.Checked;
            Properties.Settings.Default.DoRandomization_Text = cbIncText.Checked;
            Properties.Settings.Default.DoRandomization_Other = cbIncOther.Checked;

            if (sfdPresets.ShowDialog() == DialogResult.OK)
            {
                KRP.WriteKRP(sfdPresets.OpenFile());

                if (!Properties.Settings.Default.PresetPaths.Contains(sfdPresets.FileName))
                {
                    Properties.Settings.Default.PresetPaths.Add(sfdPresets.FileName);
                }
                update_listBox();
            }
        }
    }
}
