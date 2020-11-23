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
            RandomizeArmor = (RandomizationLevel)Properties.Settings.Default.RandomizeArmor;
            RandomizeStims = (RandomizationLevel)Properties.Settings.Default.RandomizeStims;
            RandomizeBelts = (RandomizationLevel)Properties.Settings.Default.RandomizeBelts;
            RandomizeVarious = (RandomizationLevel)Properties.Settings.Default.RandomizeVarious;
            RandomizeHides = (RandomizationLevel)Properties.Settings.Default.RandomizeHides;
            RandomizeArmbands = (RandomizationLevel)Properties.Settings.Default.RandomizeArmbands;
            RandomizeDroid = (RandomizationLevel)Properties.Settings.Default.RandomizeDroid;
            RandomizeGloves = (RandomizationLevel)Properties.Settings.Default.RandomizeGloves;
            RandomizeImplants = (RandomizationLevel)Properties.Settings.Default.RandomizeImplants;
            RandomizeMask = (RandomizationLevel)Properties.Settings.Default.RandomizeMask;
            RandomizePaz = (RandomizationLevel)Properties.Settings.Default.RandomizePaz;
            RandomizeMines = (RandomizationLevel)Properties.Settings.Default.RandomizeMines;
            RandomizeUpgrade = (RandomizationLevel)Properties.Settings.Default.RandomizeUpgrade;
            RandomizeBlasters = (RandomizationLevel)Properties.Settings.Default.RandomizeBlasters;
            RandomizeCreature = (RandomizationLevel)Properties.Settings.Default.RandomizeCreature;
            RandomizeLightsabers = (RandomizationLevel)Properties.Settings.Default.RandomizeLightsabers;
            RandomizeGrenades = (RandomizationLevel)Properties.Settings.Default.RandomizeGrenades;
            RandomizeMelee = (RandomizationLevel)Properties.Settings.Default.RandomizeMelee;

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

        //Check boxes Mostly
        private void cbArmor_CheckedChanged(object sender, EventArgs e)
        {
            flpArmor.Enabled = cbArmor.Checked;
        }

        private void cbStims_CheckedChanged(object sender, EventArgs e)
        {
            flpStims.Enabled = cbStimulant.Checked;
        }

        private void cbBelts_CheckedChanged(object sender, EventArgs e)
        {
            flpBelts.Enabled = cbBelt.Checked;
        }

        private void cbVarious_CheckedChanged(object sender, EventArgs e)
        {
            flpVarious.Enabled = cbVarious.Checked;
        }

        private void cbHides_CheckedChanged(object sender, EventArgs e)
        {
            flpHides.Enabled = cbCreatureHide.Checked;
        }

        private void cbDroid_CheckedChanged(object sender, EventArgs e)
        {
            flpDroid.Enabled = cbDroid.Checked;
        }

        private void cbArmbands_CheckedChanged(object sender, EventArgs e)
        {
            flpArmbands.Enabled = cbArmband.Checked;
        }

        private void cbGloves_CheckedChanged(object sender, EventArgs e)
        {
            flpGloves.Enabled = cbGlove.Checked;
        }

        private void cbImplants_CheckedChanged(object sender, EventArgs e)
        {
            flpImplants.Enabled = cbImplant.Checked;
        }

        private void cbMask_CheckedChanged(object sender, EventArgs e)
        {
            flpMasks.Enabled = cbMask.Checked;
        }

        private void cbPaz_CheckedChanged(object sender, EventArgs e)
        {
            flpPaz.Enabled = cbPazaak.Checked;
        }

        private void cbMines_CheckedChanged(object sender, EventArgs e)
        {
            flpMines.Enabled = cbMine.Checked;
        }

        private void cbUpgrade_CheckedChanged(object sender, EventArgs e)
        {
            flpUpgrades.Enabled = cbUpgrade.Checked;
        }

        private void cbBlasters_CheckedChanged(object sender, EventArgs e)
        {
            flpBlasters.Enabled = cbBlaster.Checked;
        }

        private void cbCreature_CheckedChanged(object sender, EventArgs e)
        {
            flpCreature.Enabled = cbCreatureWeapon.Checked;
        }

        private void cbLightsabers_CheckedChanged(object sender, EventArgs e)
        {
            flpLightsabers.Enabled = cbLightsaber.Checked;
        }

        private void cbGrenades_CheckedChanged(object sender, EventArgs e)
        {
            flpGrenades.Enabled = cbGrenade.Checked;
        }

        private void cbMelee_CheckedChanged(object sender, EventArgs e)
        {
            flpMelee.Enabled = cbMelee.Checked;
        }

        //Save appdata on form close. (Now that I think about it, we only really need to save this once when the program closes.... oh well)
        private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.RandomizeArmor = (int)RandomizeArmor;
            Properties.Settings.Default.RandomizeStims = (int)RandomizeStims;
            Properties.Settings.Default.RandomizeBelts = (int)RandomizeBelts;
            Properties.Settings.Default.RandomizeVarious = (int)RandomizeVarious;
            Properties.Settings.Default.RandomizeHides = (int)RandomizeHides;
            Properties.Settings.Default.RandomizeArmbands = (int)RandomizeArmbands;
            Properties.Settings.Default.RandomizeDroid = (int)RandomizeDroid;
            Properties.Settings.Default.RandomizeGloves = (int)RandomizeGloves;
            Properties.Settings.Default.RandomizeImplants = (int)RandomizeImplants;
            Properties.Settings.Default.RandomizeMask = (int)RandomizeMask;
            Properties.Settings.Default.RandomizePaz = (int)RandomizePaz;
            Properties.Settings.Default.RandomizeMines = (int)RandomizeMines;
            Properties.Settings.Default.RandomizeUpgrade = (int)RandomizeUpgrade;
            Properties.Settings.Default.RandomizeBlasters = (int)RandomizeBlasters;
            Properties.Settings.Default.RandomizeCreature = (int)RandomizeCreature;
            Properties.Settings.Default.RandomizeLightsabers = (int)RandomizeLightsabers;
            Properties.Settings.Default.RandomizeGrenades = (int)RandomizeGrenades;
            Properties.Settings.Default.RandomizeMelee = (int)RandomizeMelee;

            Properties.Settings.Default.Save();
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            //Auto-complete is cool
            tbItemOmitAdd.AutoCompleteCustomSource.AddRange(Globals.ITEMS.ToArray());
        }

        private void bAddOmitItem_Click(object sender, EventArgs e)
        {
            Globals.OmitItems.Add(tbItemOmitAdd.Text);
            lbOmitItems.DataSource = Globals.OmitItems;
        }

        private void tbItemOmitAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                bAddOmitItem_Click(sender, e);
            }
        }

        //Remove items from omit list. (Consider adding a restore default button in case they remove items that they didn't intend to.)
        private void lbOmitItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string[] to_remove = new string[lbOmitItems.SelectedItems.Count];
            lbOmitItems.SelectedItems.CopyTo(to_remove, 0);
            foreach (string s in to_remove)
            {
                Globals.OmitItems.Remove(s);
            }
            lbOmitItems.DataSource = Globals.OmitItems;
        }

        private void lbOmitItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                lbOmitItems_MouseDoubleClick(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
            }
        }

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

        #endregion
    }
}
