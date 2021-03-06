using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kotor_Randomizer_2
{
    public partial class ItemForm : Form
    {
        public ItemForm()
        {
            InitializeComponent();

            lbOmitItems.DataSource = Globals.OmitItems;

            //Set Intiial Values
            RandomizeArmbands = Properties.Settings.Default.RandomizeArmbands;
            RandomizeArmor = Properties.Settings.Default.RandomizeArmor;
            RandomizeBelts = Properties.Settings.Default.RandomizeBelts;
            RandomizeBlasters = Properties.Settings.Default.RandomizeBlasters;
            RandomizeHides = Properties.Settings.Default.RandomizeHides;
            RandomizeCreature = Properties.Settings.Default.RandomizeCreature;
            RandomizeDroid = Properties.Settings.Default.RandomizeDroid;
            RandomizeGloves = Properties.Settings.Default.RandomizeGloves;
            RandomizeGrenades = Properties.Settings.Default.RandomizeGrenades;
            RandomizeImplants = Properties.Settings.Default.RandomizeImplants;
            RandomizeLightsabers = Properties.Settings.Default.RandomizeLightsabers;
            RandomizeMask = Properties.Settings.Default.RandomizeMask;
            RandomizeMelee = Properties.Settings.Default.RandomizeMelee;
            RandomizeMines = Properties.Settings.Default.RandomizeMines;
            RandomizePaz = Properties.Settings.Default.RandomizePaz;
            RandomizeStims = Properties.Settings.Default.RandomizeStims;
            RandomizeUpgrade = Properties.Settings.Default.RandomizeUpgrade;
            RandomizeVarious = Properties.Settings.Default.RandomizeVarious;

            // Create easy access lists.
            CheckBoxes.AddRange(new List<CheckBox>()
            {
                cbArmband,
                cbArmor,
                cbBelt,
                cbBlaster,
                cbCreatureHide,
                cbCreatureWeapon,
                cbDroid,
                cbGlove,
                cbGrenade,
                cbImplant,
                cbLightsaber,
                cbMask,
                cbMelee,
                cbMine,
                cbPazaak,
                cbStimulant,
                cbUpgrade,
                cbVarious,
            });

            SubtypeRadioButtons.AddRange(new List<RadioButton>()
            {
                rbArmbandSType,
                rbArmorSType,
                rbBlasterSType,
                rbCreatureWeaponSType,
                rbDroidSType,
                rbImplantSType,
                rbLightsaberSType,
                rbMaskSType,
                rbMeleeSType,
                rbMineSType,
                rbStimSType,
                rbUpgradeSType,
            });

            TypeRadioButtons.AddRange(new List<RadioButton>()
            {
                rbArmbandType,
                rbArmorType,
                rbBeltType,
                rbBlasterType,
                rbCreatureHideType,
                rbCreatureWeaponType,
                rbDroidType,
                rbGloveType,
                rbGrenadeType,
                rbImplantType,
                rbLightsaberType,
                rbMaskType,
                rbMeleeType,
                rbMineType,
                rbPazaakType,
                rbStimulantType,
                rbUpgradeType,
                rbVariousType,
            });

            MaxRadioButtons.AddRange(new List<RadioButton>()
            {
                rbArmbandMax,
                rbArmorMax,
                rbBeltMax,
                rbBlasterMax,
                rbCreatureHideMax,
                rbCreatureWeaponMax,
                rbDroidMax,
                rbGloveMax,
                rbGrenadeMax,
                rbImplantMax,
                rbLightsaberMax,
                rbMaskMax,
                rbMeleeMax,
                rbMineMax,
                rbPazaakMax,
                rbStimulantMax,
                rbUpgradeMax,
                rbVariousMax,
            });
        }

        //Rando level for each item. Either max, type, or subtype
        #region Public Properties

        public RandomizationLevel RandomizeArmor
        {
            get
            {
                if (cbArmor.Checked)
                {
                    if (rbArmorSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbArmorType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbArmorMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbArmorType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbArmorSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbArmor.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeStims
        {
            get
            {
                if (cbStimulant.Checked)
                {
                    if (rbStimSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbStimulantType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbStimulantMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbStimulantType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbStimSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbStimulant.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeBelts
        {
            get
            {
                if (cbBelt.Checked)
                {
                    if (rbBeltType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbBeltMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbBeltType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbBelt.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeVarious
        {
            get
            {
                if (cbVarious.Checked)
                {
                    if (rbVariousType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbVariousMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbVariousType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbVarious.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeHides
        {
            get
            {
                if (cbCreatureHide.Checked)
                {
                    if (rbCreatureHideType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbCreatureHideMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbCreatureHideType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbCreatureHide.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeArmbands
        {
            get
            {
                if (cbArmband.Checked)
                {
                    if (rbArmbandSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbArmbandType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbArmbandMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbArmbandType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbArmbandSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbArmband.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeDroid
        {
            get
            {
                if (cbDroid.Checked)
                {
                    if (rbDroidSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbDroidType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbDroidMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbDroidType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbDroidSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbDroid.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeGloves
        {
            get
            {
                if (cbGlove.Checked)
                {
                    if (rbGloveType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbGloveMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbGloveType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbGlove.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeImplants
        {
            get
            {
                if (cbImplant.Checked)
                {
                    if (rbImplantSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbImplantType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbImplantMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbImplantType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbImplantSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbImplant.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeMask
        {
            get
            {
                if (cbMask.Checked)
                {
                    if (rbMaskSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbMaskType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbMaskMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbMaskType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbMaskSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbMask.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizePaz
        {
            get
            {
                if (cbPazaak.Checked)
                {
                    if (rbPazaakType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbPazaakMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbPazaakType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbPazaak.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeMines
        {
            get
            {
                if (cbMine.Checked)
                {
                    if (rbMineSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbMineType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbMineMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbMineType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbMineSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbMine.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeUpgrade
        {
            get
            {
                if (cbUpgrade.Checked)
                {
                    if (rbUpgradeSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbUpgradeType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbUpgradeMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbUpgradeType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbUpgradeSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbUpgrade.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeBlasters
        {
            get
            {
                if (cbBlaster.Checked)
                {
                    if (rbBlasterSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbBlasterType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbBlasterMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbBlasterType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbBlasterSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbBlaster.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeCreature
        {
            get
            {
                if (cbCreatureWeapon.Checked)
                {
                    if (rbCreatureWeaponSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbCreatureWeaponType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbCreatureWeaponMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbCreatureWeaponType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbCreatureWeaponSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbCreatureWeapon.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeLightsabers
        {
            get
            {
                if (cbLightsaber.Checked)
                {
                    if (rbLightsaberSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbLightsaberType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbLightsaberMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbLightsaberType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbLightsaberSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbLightsaber.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeGrenades
        {
            get
            {
                if (cbGrenade.Checked)
                {
                    if (rbGrenadeType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbGrenadeMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbGrenadeType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbGrenade.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeMelee
        {
            get
            {
                if (cbMelee.Checked)
                {
                    if (rbMeleeSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbMeleeType.Checked)
                    {
                        return RandomizationLevel.Type;
                    }
                    else
                    {
                        return RandomizationLevel.Max;
                    }
                }
                else
                {
                    return RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case RandomizationLevel.Max:
                        rbMeleeMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbMeleeType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbMeleeSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbMelee.Checked = false;
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        private List<CheckBox> CheckBoxes = new List<CheckBox>();
        private List<RadioButton> SubtypeRadioButtons = new List<RadioButton>();
        private List<RadioButton> TypeRadioButtons = new List<RadioButton>();
        private List<RadioButton> MaxRadioButtons = new List<RadioButton>();

        // Checkbox handlers
        private void cbArmbands_CheckedChanged(object sender, EventArgs e)
        {
            flpArmbands.Enabled = cbArmband.Checked;
            Properties.Settings.Default.RandomizeArmbands = RandomizeArmbands;
        }

        private void cbArmor_CheckedChanged(object sender, EventArgs e)
        {
            flpArmor.Enabled = cbArmor.Checked;
            Properties.Settings.Default.RandomizeArmor = RandomizeArmor;
        }

        private void cbBelts_CheckedChanged(object sender, EventArgs e)
        {
            flpBelts.Enabled = cbBelt.Checked;
            Properties.Settings.Default.RandomizeBelts = RandomizeBelts;
        }

        private void cbBlasters_CheckedChanged(object sender, EventArgs e)
        {
            flpBlasters.Enabled = cbBlaster.Checked;
            Properties.Settings.Default.RandomizeBlasters = RandomizeBlasters;
        }

        private void cbCreature_CheckedChanged(object sender, EventArgs e)
        {
            flpCreature.Enabled = cbCreatureWeapon.Checked;
            Properties.Settings.Default.RandomizeCreature = RandomizeCreature;
        }

        private void cbDroid_CheckedChanged(object sender, EventArgs e)
        {
            flpDroid.Enabled = cbDroid.Checked;
            Properties.Settings.Default.RandomizeDroid = RandomizeDroid;
        }

        private void cbGloves_CheckedChanged(object sender, EventArgs e)
        {
            flpGloves.Enabled = cbGlove.Checked;
            Properties.Settings.Default.RandomizeGloves = RandomizeGloves;
        }

        private void cbGrenades_CheckedChanged(object sender, EventArgs e)
        {
            flpGrenades.Enabled = cbGrenade.Checked;
            Properties.Settings.Default.RandomizeGrenades = RandomizeGrenades;
        }

        private void cbHides_CheckedChanged(object sender, EventArgs e)
        {
            flpHides.Enabled = cbCreatureHide.Checked;
            Properties.Settings.Default.RandomizeHides = RandomizeHides;
        }

        private void cbImplants_CheckedChanged(object sender, EventArgs e)
        {
            flpImplants.Enabled = cbImplant.Checked;
            Properties.Settings.Default.RandomizeImplants = RandomizeImplants;
        }

        private void cbLightsabers_CheckedChanged(object sender, EventArgs e)
        {
            flpLightsabers.Enabled = cbLightsaber.Checked;
            Properties.Settings.Default.RandomizeLightsabers = RandomizeLightsabers;
        }

        private void cbMask_CheckedChanged(object sender, EventArgs e)
        {
            flpMasks.Enabled = cbMask.Checked;
            Properties.Settings.Default.RandomizeMask = RandomizeMask;
        }

        private void cbMelee_CheckedChanged(object sender, EventArgs e)
        {
            flpMelee.Enabled = cbMelee.Checked;
            Properties.Settings.Default.RandomizeMelee = RandomizeMelee;
        }

        private void cbMines_CheckedChanged(object sender, EventArgs e)
        {
            flpMines.Enabled = cbMine.Checked;
            Properties.Settings.Default.RandomizeMines = RandomizeMines;
        }

        private void cbPaz_CheckedChanged(object sender, EventArgs e)
        {
            flpPaz.Enabled = cbPazaak.Checked;
            Properties.Settings.Default.RandomizePaz = RandomizePaz;
        }

        private void cbStims_CheckedChanged(object sender, EventArgs e)
        {
            flpStims.Enabled = cbStimulant.Checked;
            Properties.Settings.Default.RandomizeStims = RandomizeStims;
        }

        private void cbUpgrade_CheckedChanged(object sender, EventArgs e)
        {
            flpUpgrades.Enabled = cbUpgrade.Checked;
            Properties.Settings.Default.RandomizeUpgrade = RandomizeUpgrade;
        }

        private void cbVarious_CheckedChanged(object sender, EventArgs e)
        {
            flpVarious.Enabled = cbVarious.Checked;
            Properties.Settings.Default.RandomizeVarious = RandomizeVarious;
        }

        // Form handlers
        /// <summary>
        /// Save appdata on form close. <para/>
        /// (Now that I think about it, we only really need to save this once when the program closes.... oh well)
        /// </summary>
        private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            // Auto-complete is cool
            tbItemOmitAdd.AutoCompleteCustomSource.AddRange(Globals.ITEMS.ToArray());
        }

        // Omitted item handlers
        private void bResetOmittedItems_Click(object sender, EventArgs e)
        {
            Globals.OmitItems.Clear();
            foreach (var item in Globals.DEFAULT_OMIT_ITEMS)
            {
                Globals.OmitItems.Add(item);
            }
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void bAddOmitItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbItemOmitAdd.Text) || Globals.OmitItems.Contains(tbItemOmitAdd.Text))
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
            else
            {
                Globals.OmitItems.Add(tbItemOmitAdd.Text);
            }
            tbItemOmitAdd.Text = string.Empty;
        }

        private void tbItemOmitAdd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bAddOmitItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Remove items from omit list. <para/>
        /// (Consider adding a restore default button in case they remove items that they didn't intend to.)
        /// </summary>
        private void lbOmitItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string[] to_remove = new string[lbOmitItems.SelectedItems.Count];
            lbOmitItems.SelectedItems.CopyTo(to_remove, 0);
            foreach (string s in to_remove)
            {
                Globals.OmitItems.Remove(s);
            }
        }

        private void lbOmitItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                lbOmitItems_MouseDoubleClick(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
            }
        }

        // Toggle all handlers
        private void bToggleAll_Click(object sender, EventArgs e)
        {
            // If all checkboxes are checked, uncheck all of them. If any are unchecked, check all of them.
            bool CheckAllBoxes = false;
            if (CheckBoxes.Any(cb => !cb.Checked))
            {
                CheckAllBoxes = true;
            }

            foreach (var cb in CheckBoxes)
            {
                cb.Checked = CheckAllBoxes;
            }
        }

        private void bSubtypeAll_Click(object sender, EventArgs e)
        {
            foreach (var cb in CheckBoxes)
            {
                cb.Checked = true;
            }
            foreach (var rb in SubtypeRadioButtons)
            {
                rb.Checked = true;
            }
        }

        private void bTypeAll_Click(object sender, EventArgs e)
        {
            foreach (var cb in CheckBoxes)
            {
                cb.Checked = true;
            }
            foreach (var rb in TypeRadioButtons)
            {
                rb.Checked = true;
            }
        }

        private void bMaxAll_Click(object sender, EventArgs e)
        {
            foreach (var cb in CheckBoxes)
            {
                cb.Checked = true;
            }
            foreach (var rb in MaxRadioButtons)
            {
                rb.Checked = true;
            }
        }

        // Radio button handlers
        private void rbArmbandSType_CheckedChanged(object sender, EventArgs e)        { Properties.Settings.Default.RandomizeArmbands    = RandomizeArmbands; }
        private void rbArmbandType_CheckedChanged(object sender, EventArgs e)         { Properties.Settings.Default.RandomizeArmbands    = RandomizeArmbands; }
        private void rbArmbandMax_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeArmbands    = RandomizeArmbands; }

        private void rbArmorSType_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeArmor       = RandomizeArmor; }
        private void rbArmorType_CheckedChanged(object sender, EventArgs e)           { Properties.Settings.Default.RandomizeArmor       = RandomizeArmor; }
        private void rbArmorMax_CheckedChanged(object sender, EventArgs e)            { Properties.Settings.Default.RandomizeArmor       = RandomizeArmor; }

        private void rbBeltType_CheckedChanged(object sender, EventArgs e)            { Properties.Settings.Default.RandomizeBelts       = RandomizeBelts; }
        private void rbBeltMax_CheckedChanged(object sender, EventArgs e)             { Properties.Settings.Default.RandomizeBelts       = RandomizeBelts; }

        private void rbBlasterSType_CheckedChanged(object sender, EventArgs e)        { Properties.Settings.Default.RandomizeBlasters    = RandomizeBlasters; }
        private void rbBlasterType_CheckedChanged(object sender, EventArgs e)         { Properties.Settings.Default.RandomizeBlasters    = RandomizeBlasters; }
        private void rbBlasterMax_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeBlasters    = RandomizeBlasters; }

        private void rbCreatureHideType_CheckedChanged(object sender, EventArgs e)    { Properties.Settings.Default.RandomizeHides       = RandomizeHides; }
        private void rbCreatureHideMax_CheckedChanged(object sender, EventArgs e)     { Properties.Settings.Default.RandomizeHides       = RandomizeHides; }

        private void rbCreatureWeaponSType_CheckedChanged(object sender, EventArgs e) { Properties.Settings.Default.RandomizeCreature    = RandomizeCreature; }
        private void rbCreatureWeaponType_CheckedChanged(object sender, EventArgs e)  { Properties.Settings.Default.RandomizeCreature    = RandomizeCreature; }
        private void rbCreatureWeaponMax_CheckedChanged(object sender, EventArgs e)   { Properties.Settings.Default.RandomizeCreature    = RandomizeCreature; }

        private void rbDroidSType_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeDroid       = RandomizeDroid; }
        private void rbDroidType_CheckedChanged(object sender, EventArgs e)           { Properties.Settings.Default.RandomizeDroid       = RandomizeDroid; }
        private void rbDroidMax_CheckedChanged(object sender, EventArgs e)            { Properties.Settings.Default.RandomizeDroid       = RandomizeDroid; }

        private void rbGloveType_CheckedChanged(object sender, EventArgs e)           { Properties.Settings.Default.RandomizeGloves      = RandomizeGloves; }
        private void rbGloveMax_CheckedChanged(object sender, EventArgs e)            { Properties.Settings.Default.RandomizeGloves      = RandomizeGloves; }

        private void rbGrenadeType_CheckedChanged(object sender, EventArgs e)         { Properties.Settings.Default.RandomizeGrenades    = RandomizeGrenades; }
        private void rbGrenadeMax_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeGrenades    = RandomizeGrenades; }

        private void rbImplantSType_CheckedChanged(object sender, EventArgs e)        { Properties.Settings.Default.RandomizeImplants    = RandomizeImplants; }
        private void rbImplantType_CheckedChanged(object sender, EventArgs e)         { Properties.Settings.Default.RandomizeImplants    = RandomizeImplants; }
        private void rbImplantMax_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeImplants    = RandomizeImplants; }

        private void rbLightsaberSType_CheckedChanged(object sender, EventArgs e)     { Properties.Settings.Default.RandomizeLightsabers = RandomizeLightsabers; }
        private void rbLightsaberType_CheckedChanged(object sender, EventArgs e)      { Properties.Settings.Default.RandomizeLightsabers = RandomizeLightsabers; }
        private void rbLightsaberMax_CheckedChanged(object sender, EventArgs e)       { Properties.Settings.Default.RandomizeLightsabers = RandomizeLightsabers; }

        private void rbMaskSType_CheckedChanged(object sender, EventArgs e)           { Properties.Settings.Default.RandomizeMask        = RandomizeMask; }
        private void rbMaskType_CheckedChanged(object sender, EventArgs e)            { Properties.Settings.Default.RandomizeMask        = RandomizeMask; }
        private void rbMaskMax_CheckedChanged(object sender, EventArgs e)             { Properties.Settings.Default.RandomizeMask        = RandomizeMask; }

        private void rbMeleeSType_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeMelee       = RandomizeMelee; }
        private void rbMeleeType_CheckedChanged(object sender, EventArgs e)           { Properties.Settings.Default.RandomizeMelee       = RandomizeMelee; }
        private void rbMeleeMax_CheckedChanged(object sender, EventArgs e)            { Properties.Settings.Default.RandomizeMelee       = RandomizeMelee; }

        private void rbMineSType_CheckedChanged(object sender, EventArgs e)           { Properties.Settings.Default.RandomizeMines       = RandomizeMines; }
        private void rbMineType_CheckedChanged(object sender, EventArgs e)            { Properties.Settings.Default.RandomizeMines       = RandomizeMines; }
        private void rbMineMax_CheckedChanged(object sender, EventArgs e)             { Properties.Settings.Default.RandomizeMines       = RandomizeMines; }
        private void rbPazaakType_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizePaz         = RandomizePaz; }
        private void rbPazaakMax_CheckedChanged(object sender, EventArgs e)           { Properties.Settings.Default.RandomizePaz         = RandomizePaz; }

        private void rbStimSType_CheckedChanged(object sender, EventArgs e)           { Properties.Settings.Default.RandomizeStims       = RandomizeStims; }
        private void rbStimulantType_CheckedChanged(object sender, EventArgs e)       { Properties.Settings.Default.RandomizeStims       = RandomizeStims; }
        private void rbStimulantMax_CheckedChanged(object sender, EventArgs e)        { Properties.Settings.Default.RandomizeStims       = RandomizeStims; }

        private void rbUpgradeSType_CheckedChanged(object sender, EventArgs e)        { Properties.Settings.Default.RandomizeUpgrade     = RandomizeUpgrade; }
        private void rbUpgradeType_CheckedChanged(object sender, EventArgs e)         { Properties.Settings.Default.RandomizeUpgrade     = RandomizeUpgrade; }
        private void rbUpgradeMax_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeUpgrade     = RandomizeUpgrade; }

        private void rbVariousType_CheckedChanged(object sender, EventArgs e)         { Properties.Settings.Default.RandomizeVarious     = RandomizeVarious; }
        private void rbVariousMax_CheckedChanged(object sender, EventArgs e)          { Properties.Settings.Default.RandomizeVarious     = RandomizeVarious; }

        #endregion
    }
}
