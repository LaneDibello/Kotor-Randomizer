using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kotor_Randomizer_2
{
    public partial class ModuleForm : Form
    {
        
        #region Public Members

        public ModuleForm()
        {
            InitializeComponent();

            if (!Properties.Settings.Default.ModulesInitialized)
            {
                foreach (string s in Globals.MODULES)
                {
                    Globals.BoundModules.Add(new Globals.Mod_Entry(s, true));
                }
                Properties.Settings.Default.ModulesInitialized = true;
            }

            updateListBoxes();
            RandomizedListBox.DisplayMember = "name";
            OmittedListBox.DisplayMember = "name";
            
            PresetComboBox.DataSource = Globals.PRESETS.Keys.ToList();
            PresetComboBox.SelectedIndex = Properties.Settings.Default.LastPresetComboIndex;
        }

        #endregion
        #region Private Members

        private void updateListBoxes()
        {
            RandomizedListBox.DataSource = Globals.BoundModules.Where(x => !x.ommitted).ToList();
            RandomizedListBox.Update();
            OmittedListBox.DataSource = Globals.BoundModules.Where(x => x.ommitted).ToList();
            OmittedListBox.Update();
        }

        private void loadPreset(string preset)
        {
            if(PresetComboBox.SelectedIndex == -1 || !Properties.Settings.Default.ModulePresetSelected) { return; }

            for (int i = 0; i < Globals.BoundModules.Count; i++)
            {
                Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].name, false);

                if (Globals.PRESETS[preset].Contains(Globals.BoundModules[i].name))
                {
                    Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].name, true);
                }
            }
            updateListBoxes();
        }

        #endregion
        #region Events

        private void RandomizedListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.ModulePresetSelected = false;

            for (int i = 0; i < Globals.BoundModules.Count; i++)
            {
                if (RandomizedListBox.SelectedItems.Contains(Globals.BoundModules[i]))
                {
                    Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].name, true);
                }
            }
            updateListBoxes();
        }

        private void OmittedListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.ModulePresetSelected = false;

            for (int i = 0; i < Globals.BoundModules.Count; i++)
            {
                if (OmittedListBox.SelectedItems.Contains(Globals.BoundModules[i]))
                {
                    Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].name, false);
                }
            }
            updateListBoxes();
        }

        private void RandomizedListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Properties.Settings.Default.ModulePresetSelected = false;

                for (int i = 0; i < Globals.BoundModules.Count; i++)
                {
                    if (RandomizedListBox.SelectedItems.Contains(Globals.BoundModules[i]))
                    {
                        Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].name, true);
                    }
                }
                updateListBoxes();
            }
        }

        private void OmittedListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Properties.Settings.Default.ModulePresetSelected = false;

                for (int i = 0; i < Globals.BoundModules.Count; i++)
                {
                    if (OmittedListBox.SelectedItems.Contains(Globals.BoundModules[i]))
                    {
                        Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].name, false);
                    }
                }
                updateListBoxes();
            }
        }

        private void PresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPreset(PresetComboBox.Text);
            updateListBoxes();
        }

        private void PresetComboBox_Enter(object sender, EventArgs e)
        {
            Properties.Settings.Default.ModulePresetSelected = true;
        }

        private void ModuleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastPresetComboIndex = PresetComboBox.SelectedIndex;
        }

        #endregion
    }
}
