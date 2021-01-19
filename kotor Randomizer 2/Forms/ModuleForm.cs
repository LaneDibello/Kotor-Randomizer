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
        // Prevents Construction from triggering certain events
        private bool Constructed = false;

        #region Public Members

        public ModuleForm()
        {
            InitializeComponent();
            var settings = Properties.Settings.Default;

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

            cbDeleteMilestones.Checked = !settings.ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete);
            cbSaveMiniGame.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames);
            cbSaveAllMods.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules);
            cbSaveMiniGame.Enabled = !cbSaveAllMods.Checked; // If all save checked, disable mg save checkbox.

            cbFixDream.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.FixDream);
            cbUnlockGalaxyMap.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap);
            cbFixCoordinates.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates);
            cbFixMindPrison.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison);

            cbDoorFix.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockVarDoors);
            cbFixLevElevators.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.FixLevElevators);
            cbVulkSpiceLZ.Checked = settings.ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ);

            // Initialize reachability settings
            cbReachability.Checked = settings.VerifyReachability;
            cbIgnoreOnceEdges.Enabled = cbReachability.Checked;
            cbGoalMalak.Enabled = cbReachability.Checked;
            cbGoalStarMaps.Enabled = cbReachability.Checked;
            cbGoalPazaak.Enabled = cbReachability.Checked;
            cbGlitchClip.Enabled = cbReachability.Checked;
            cbGlitchDlz.Enabled = cbReachability.Checked;
            cbGlitchFlu.Enabled = cbReachability.Checked;
            cbGlitchGpw.Enabled = cbReachability.Checked;
            cbIgnoreOnceEdges.Checked = settings.IgnoreOnceEdges;
            cbGoalMalak.Checked = settings.GoalIsMalak;
            cbGoalStarMaps.Checked = settings.GoalIsStarMaps;
            cbGoalPazaak.Checked = settings.GoalIsPazaak;
            cbGlitchClip.Checked = settings.AllowGlitchClip;
            cbGlitchDlz.Checked = settings.AllowGlitchDlz;
            cbGlitchFlu.Checked = settings.AllowGlitchFlu;
            cbGlitchGpw.Checked = settings.AllowGlitchGpw;

            PresetComboBox.DataSource = Globals.OMIT_PRESETS.Keys.ToList();
            Constructed = true;

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
            if (!Constructed) return;

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
            Properties.Settings.Default.Save();
        }

        private void cbDeleteMilestones_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.NoSaveDelete;
        }

        private void cbSaveMiniGame_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.SaveMiniGames;
        }

        private void cbSaveAllMods_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.SaveAllModules;
            cbSaveMiniGame.Checked = true;
            cbSaveMiniGame.Enabled = !cbSaveAllMods.Checked;    // If all save checked, disable mg save.
        }

        private void cbFixDream_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.FixDream;
        }

        private void cbUnlockGalaxyMap_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.UnlockGalaxyMap;
        }

        private void cbFixCoordinates_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.FixCoordinates;
        }

        private void lblRandomized_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(Properties.Settings.Default.ModuleExtrasValue));
        }

        private void cbFixMindPrison_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.FixMindPrison;
        }

        private void cbDoorFix_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.UnlockVarDoors;
        }

        private void cbFixLevElevators_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.FixLevElevators;
        }

        private void cbVulkSpiceLZ_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.ModuleExtrasValue ^= ModuleExtras.VulkarSpiceLZ;
        }

        private void cbReachability_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.VerifyReachability = cbReachability.Checked;
            cbIgnoreOnceEdges.Enabled = cbReachability.Checked;
            cbGoalMalak.Enabled = cbReachability.Checked;
            cbGoalStarMaps.Enabled = cbReachability.Checked;
            cbGoalPazaak.Enabled = cbReachability.Checked;
            cbGlitchClip.Enabled = cbReachability.Checked;
            cbGlitchDlz.Enabled = cbReachability.Checked;
            cbGlitchFlu.Enabled = cbReachability.Checked;
            cbGlitchGpw.Enabled = cbReachability.Checked;
        }

        private void cbGoalMalak_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.GoalIsMalak = cbGoalMalak.Checked;
        }

        private void cbGoalStarMaps_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.GoalIsStarMaps = cbGoalStarMaps.Checked;
        }

        private void cbGoalPazaak_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.GoalIsPazaak = cbGoalPazaak.Checked;
        }

        private void cbGlitchClip_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.AllowGlitchClip = cbGlitchClip.Checked;
        }

        private void cbGlitchDlz_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.AllowGlitchDlz = cbGlitchDlz.Checked;
        }

        private void cbGlitchFlu_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.AllowGlitchFlu = cbGlitchFlu.Checked;
        }

        private void cbGlitchGpw_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.AllowGlitchGpw = cbGlitchGpw.Checked;
        }

        private void cbAllowOnceEdges_CheckedChanged(object sender, EventArgs e)
        {
            if (!Constructed) { return; }
            Properties.Settings.Default.IgnoreOnceEdges = cbIgnoreOnceEdges.Checked;
        }

        private void ModuleForm_Activated(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.LastPresetComboIndex == -2)
            {
                Properties.Settings.Default.LastPresetComboIndex = -1;
                PresetComboBox.SelectedIndex = -1;
                UpdateListBoxes();
                Constructed = false;

                // Load new fix settings.
                cbDeleteMilestones.Checked = !Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete);
                cbSaveMiniGame.Checked = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames);
                cbSaveAllMods.Checked = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules);
                cbSaveMiniGame.Enabled = !cbSaveAllMods.Checked; // If all save checked, disable mg save checkbox.

                cbFixDream.Checked = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixDream);
                cbUnlockGalaxyMap.Checked = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap);
                cbFixCoordinates.Checked = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates);
                cbFixMindPrison.Checked = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison);
                cbDoorFix.Checked = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockVarDoors);
                cbFixLevElevators.Checked = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixLevElevators);

                // Load new reachability settings.
                cbReachability.Checked = Properties.Settings.Default.VerifyReachability;
                cbIgnoreOnceEdges.Checked = Properties.Settings.Default.IgnoreOnceEdges;
                cbGoalMalak.Checked = Properties.Settings.Default.GoalIsMalak;
                cbGoalStarMaps.Checked = Properties.Settings.Default.GoalIsStarMaps;
                cbGoalPazaak.Checked = Properties.Settings.Default.GoalIsPazaak;
                cbGlitchClip.Checked = Properties.Settings.Default.AllowGlitchClip;
                cbGlitchDlz.Checked = Properties.Settings.Default.AllowGlitchDlz;
                cbGlitchFlu.Checked = Properties.Settings.Default.AllowGlitchFlu;
                cbGlitchGpw.Checked = Properties.Settings.Default.AllowGlitchGpw;
                cbIgnoreOnceEdges.Enabled = cbReachability.Checked;
                cbGoalMalak.Enabled = cbReachability.Checked;
                cbGoalStarMaps.Enabled = cbReachability.Checked;
                cbGoalPazaak.Enabled = cbReachability.Checked;
                cbGlitchClip.Enabled = cbReachability.Checked;
                cbGlitchDlz.Enabled = cbReachability.Checked;
                cbGlitchFlu.Enabled = cbReachability.Checked;
                cbGlitchGpw.Enabled = cbReachability.Checked;

                Constructed = true;
            }
        }

        #endregion
    }
}
