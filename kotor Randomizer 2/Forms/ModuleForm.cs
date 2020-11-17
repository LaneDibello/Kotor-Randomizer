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

            // Set up the bound module collection if it hasn't been already
            if (!Properties.Settings.Default.ModulesInitialized)
            {
                Globals.BoundModules.Clear();
                foreach (string s in Globals.MODULES)
                {
                    Globals.BoundModules.Add(new Globals.Mod_Entry(s, true));
                }
                Properties.Settings.Default.ModulesInitialized = true;
            }

            // Set up the controls
            RandomizedListBox.DisplayMember = "name";
            OmittedListBox.DisplayMember = "name";
            
            // TODO: update storage of additional module settings to remove unnamed constants.
            modDelete_checkbox.Checked = (Properties.Settings.Default.ModuleSaveStatus & 1) > 0;
            mgSave_checkbox.Checked = (Properties.Settings.Default.ModuleSaveStatus & 2) > 0;
            allSave_checkbox.Checked = (Properties.Settings.Default.ModuleSaveStatus & 4) > 0;

            // TODO: update override file usage to not use a string collection.
            FixedDream_checkBox.Checked = Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs");
            galmap_checkbox.Checked = Properties.Settings.Default.AddOverideFiles.Contains("k_pebn_galaxy.ncs");
            updatedCoords_checkbox.Checked = Properties.Settings.Default.FixWarpCoords;
            cbRakataRiddle.Checked = Properties.Settings.Default.FixMindPrison;

            PresetComboBox.DataSource = Globals.OMIT_PRESETS.Keys.ToList();
            constructed = true;

            if (Properties.Settings.Default.LastPresetComboIndex < 0)
            {
                Properties.Settings.Default.LastPresetComboIndex = -1;
                PresetComboBox.SelectedIndex = Properties.Settings.Default.LastPresetComboIndex;
            }
            else
            {
                PresetComboBox.SelectedIndex = Properties.Settings.Default.LastPresetComboIndex;
                Properties.Settings.Default.ModulePresetSelected = true;
                LoadPreset(PresetComboBox.Text);
            }

            UpdateListBoxes();
        }

        public static void static_loadPreset(string preset)
        {
            for (int i = 0; i < Globals.BoundModules.Count; i++)
            {
                Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, false);

                if (Globals.OMIT_PRESETS[preset].Contains(Globals.BoundModules[i].Name))
                {
                    Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, true);
                }
            }
        }

        #endregion
        #region Private Members

        // Prevents Construction from triggering certain events
        private bool constructed = false;

        // Makes list work
        private void UpdateListBoxes()
        {
            RandomizedListBox.DataSource = Globals.BoundModules.Where(x => !x.Omitted).ToList();
            RandomizedListBox.Update();
            OmittedListBox.DataSource = Globals.BoundModules.Where(x => x.Omitted).ToList();
            OmittedListBox.Update();
        }

        // How we load the built in presets. (May be subject to change if I change my mind about how I want User-presets to work.)
        private void LoadPreset(string preset)
        {
            if (PresetComboBox.SelectedIndex < 0 || !Properties.Settings.Default.ModulePresetSelected) { return; }

            Properties.Settings.Default.LastPresetComboIndex = PresetComboBox.SelectedIndex;
            for (int i = 0; i < Globals.BoundModules.Count; i++)
            {
                Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, false);

                if (Globals.OMIT_PRESETS[preset].Contains(Globals.BoundModules[i].Name))
                {
                    Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, true);
                }
            }
        }

        #endregion
        #region Events
        // ListBox Functions
        private void RandomizedListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PresetComboBox.SelectedIndex = -1;

            for (int i = 0; i < Globals.BoundModules.Count; i++)
            {
                if (RandomizedListBox.SelectedItems.Contains(Globals.BoundModules[i]))
                {
                    Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, true);
                }
            }
            UpdateListBoxes();
        }

        private void OmittedListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PresetComboBox.SelectedIndex = -1;

            for (int i = 0; i < Globals.BoundModules.Count; i++)
            {
                if (OmittedListBox.SelectedItems.Contains(Globals.BoundModules[i]))
                {
                    Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, false);
                }
            }
            UpdateListBoxes();
        }

        private void RandomizedListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                PresetComboBox.SelectedIndex = -1;

                for (int i = 0; i < Globals.BoundModules.Count; i++)
                {
                    if (RandomizedListBox.SelectedItems.Contains(Globals.BoundModules[i]))
                    {
                        Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, true);
                    }
                }
                UpdateListBoxes();
            }
        }

        private void OmittedListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                PresetComboBox.SelectedIndex = -1;

                for (int i = 0; i < Globals.BoundModules.Count; i++)
                {
                    if (OmittedListBox.SelectedItems.Contains(Globals.BoundModules[i]))
                    {
                        Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, false);
                    }
                }
                UpdateListBoxes();
            }
        }

        // Built-in Preset control functions
        private void PresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!constructed) return;

            if (PresetComboBox.SelectedIndex >= 0)
            {
                Properties.Settings.Default.ModulePresetSelected = true;
                LoadPreset(PresetComboBox.Text);
                UpdateListBoxes();
            }
            else
            {
                Properties.Settings.Default.ModulePresetSelected = false;
            }
        }

        private void PresetComboBox_Enter(object sender, EventArgs e)
        {
            Properties.Settings.Default.ModulePresetSelected = true;
        }

        // Check box functions
        private void ModuleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastPresetComboIndex = PresetComboBox.SelectedIndex;

            if (Properties.Settings.Default.ModuleSaveStatus != 1) { Properties.Settings.Default.AddOverideFiles.Add("modulesave.2da"); }
            else if (Properties.Settings.Default.AddOverideFiles.Contains("modulesave.2da")) { Properties.Settings.Default.AddOverideFiles.Remove("modulesave.2da"); }

            // Remove any duplicates within the StringCollection.
            var noDuplicates = Properties.Settings.Default.AddOverideFiles.Cast<string>().Distinct().ToArray();
            Properties.Settings.Default.AddOverideFiles.Clear();
            Properties.Settings.Default.AddOverideFiles.AddRange(noDuplicates);

            Properties.Settings.Default.Save();
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

            if (allSave_checkbox.Checked && !mgSave_checkbox.Checked)
            {
                mgSave_checkbox.Checked = true;
            }
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

        private void updatedCoords_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!constructed) { return; }
            Properties.Settings.Default.FixWarpCoords = updatedCoords_checkbox.Checked;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(Properties.Settings.Default.ModuleSaveStatus));
        }

        private void cbRakataRiddle_CheckedChanged(object sender, EventArgs e)
        {
            if (!constructed) { return; }
            Properties.Settings.Default.FixMindPrison = cbRakataRiddle.Checked;
        }

        private void ModuleForm_Activated(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.LastPresetComboIndex == -2)
            {
                Properties.Settings.Default.LastPresetComboIndex = -1;
                PresetComboBox.SelectedIndex = -1;
                UpdateListBoxes();
            }
        }

        #endregion
    }
}
