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

            modDelete_checkbox.Checked = (Properties.Settings.Default.ModuleSaveStatus & 1) > 0;
            mgSave_checkbox.Checked = (Properties.Settings.Default.ModuleSaveStatus & 2) > 0;
            allSave_checkbox.Checked = (Properties.Settings.Default.ModuleSaveStatus & 4) > 0;

            FixedDream_checkBox.Checked = Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs");
            galmap_checkbox.Checked = Properties.Settings.Default.AddOverideFiles.Contains("k_pebn_galaxy.ncs");
            missionSpawn_checkbox.Checked = Properties.Settings.Default.AddOverideFiles.Contains("MISSIONFILENAME");

            constructed = true;
        }

        #endregion
        #region Private Members

        private bool constructed = false;

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

            if (Properties.Settings.Default.ModuleSaveStatus != 1) { Properties.Settings.Default.AddOverideFiles.Add("modulesave.2da"); }
            else if (Properties.Settings.Default.AddOverideFiles.Contains("modulesave.2da")) { Properties.Settings.Default.AddOverideFiles.Remove("modulesave.2da"); }
        }

        private void modDelete_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!constructed) { return; }
            Properties.Settings.Default.ModuleSaveStatus ^= 1;
        }

        private void mgSave_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!constructed) { return; }
            Properties.Settings.Default.ModuleSaveStatus ^= 1 << 1;
        }

        private void allSave_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!constructed) { return; }
            Properties.Settings.Default.ModuleSaveStatus ^= 1 << 2;
            mgSave_checkbox.Checked = true;
        }

        private void FixedDream_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!constructed) { return; }
            if (FixedDream_checkBox.Checked)
            {
                Properties.Settings.Default.AddOverideFiles.Add("k_ren_visionland.ncs");
            }
            else if (Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs"))
            {
                Properties.Settings.Default.AddOverideFiles.Remove("k_ren_visionland.ncs");
            }
        }

        private void galmap_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!constructed) { return; }
            if (galmap_checkbox.Checked)
            {
                Properties.Settings.Default.AddOverideFiles.Add("k_pebn_galaxy.ncs");
            }
            else if (Properties.Settings.Default.AddOverideFiles.Contains("k_pebn_galaxy.ncs"))
            {
                Properties.Settings.Default.AddOverideFiles.Remove("k_pebn_galaxy.ncs");
            }
        }

        private void missionSpawn_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!constructed) { return; }
            if (missionSpawn_checkbox.Checked)
            {
                Properties.Settings.Default.AddOverideFiles.Add("MISSIONFILENAME");
            }
            else if (Properties.Settings.Default.AddOverideFiles.Contains("MISSIONFILENAME"))
            {
                Properties.Settings.Default.AddOverideFiles.Remove("MISSIONFILENAME");
            }
        }

        #endregion
    }
}
