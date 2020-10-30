using System;
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
                if (cbStims.Checked)
                {
                    if (rbStimsSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbStimsType.Checked)
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
                        rbStimsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbStimsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbStimsSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbStims.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeBelts
        {
            get
            {
                if (cbBelts.Checked)
                {
                    if (rbBeltsType.Checked)
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
                        rbBeltsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbBeltsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbBelts.Checked = false;
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
                if (cbHides.Checked)
                {
                    if (rbHidesType.Checked)
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
                        rbHidesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbHidesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbHides.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeArmbands
        {
            get
            {
                if (cbArmbands.Checked)
                {
                    if (rbArmbandsSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbArmbandsType.Checked)
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
                        rbArmbandsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbArmbandsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbArmbandsSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbArmbands.Checked = false;
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
                if (cbGloves.Checked)
                {
                    if (rbGlovesType.Checked)
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
                        rbGlovesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbGlovesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbGloves.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeImplants
        {
            get
            {
                if (cbImplants.Checked)
                {
                    if (rbImplantsSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbImplantsType.Checked)
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
                        rbImplantsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbImplantsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbImplantsSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbImplants.Checked = false;
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
                    if (rbMasksSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbMasksType.Checked)
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
                        rbMasksMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbMasksType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbMasksSType.Checked = true;
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
                if (cbPaz.Checked)
                {
                    if (rbPazType.Checked)
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
                        rbPazMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbPazType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbPaz.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeMines
        {
            get
            {
                if (cbMines.Checked)
                {
                    if (rbMinesSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbMinesType.Checked)
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
                        rbMinesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbMinesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbMinesSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbMines.Checked = false;
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
                    if (rbUpgradesSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbUpgradesType.Checked)
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
                        rbUpgradesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbUpgradesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbUpgradesSType.Checked = true;
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
                if (cbBlasters.Checked)
                {
                    if (rbBlastersSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbBlastersType.Checked)
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
                        rbBlastersMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbBlastersType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbBlastersSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbBlasters.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeCreature
        {
            get
            {
                if (cbCreature.Checked)
                {
                    if (rbCreatureSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbCreatureType.Checked)
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
                        rbCreatureMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbCreatureType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbCreatureSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbCreature.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeLightsabers
        {
            get
            {
                if (cbLightsabers.Checked)
                {
                    if (rbLightsabersSType.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbLightsabersType.Checked)
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
                        rbLightsabersMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbLightsabersType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbLightsabersSType.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbLightsabers.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeGrenades
        {
            get
            {
                if (cbGrenades.Checked)
                {
                    if (rbGrenadesType.Checked)
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
                        rbGrenadesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbGrenadesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbGrenades.Checked = false;
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
        //Check boxes Mostly
        private void cbArmor_CheckedChanged(object sender, EventArgs e)
        {
            flpArmor.Enabled = cbArmor.Checked;
        }

        private void cbStims_CheckedChanged(object sender, EventArgs e)
        {
            flpStims.Enabled = cbStims.Checked;
        }

        private void cbBelts_CheckedChanged(object sender, EventArgs e)
        {
            flpBelts.Enabled = cbBelts.Checked;
        }

        private void cbVarious_CheckedChanged(object sender, EventArgs e)
        {
            flpVarious.Enabled = cbVarious.Checked;
        }

        private void cbHides_CheckedChanged(object sender, EventArgs e)
        {
            flpHides.Enabled = cbHides.Checked;
        }

        private void cbDroid_CheckedChanged(object sender, EventArgs e)
        {
            flpDroid.Enabled = cbDroid.Checked;
        }

        private void cbArmbands_CheckedChanged(object sender, EventArgs e)
        {
            flpArmbands.Enabled = cbArmbands.Checked;
        }

        private void cbGloves_CheckedChanged(object sender, EventArgs e)
        {
            flpGloves.Enabled = cbGloves.Checked;
        }

        private void cbImplants_CheckedChanged(object sender, EventArgs e)
        {
            flpImplants.Enabled = cbImplants.Checked;
        }

        private void cbMask_CheckedChanged(object sender, EventArgs e)
        {
            flpMasks.Enabled = cbMask.Checked;
        }

        private void cbPaz_CheckedChanged(object sender, EventArgs e)
        {
            flpPaz.Enabled = cbPaz.Checked;
        }

        private void cbMines_CheckedChanged(object sender, EventArgs e)
        {
            flpMines.Enabled = cbMines.Checked;
        }

        private void cbUpgrade_CheckedChanged(object sender, EventArgs e)
        {
            flpUpgrades.Enabled = cbUpgrade.Checked;
        }

        private void cbBlasters_CheckedChanged(object sender, EventArgs e)
        {
            flpBlasters.Enabled = cbBlasters.Checked;
        }

        private void cbCreature_CheckedChanged(object sender, EventArgs e)
        {
            flpCreature.Enabled = cbCreature.Checked;
        }

        private void cbLightsabers_CheckedChanged(object sender, EventArgs e)
        {
            flpLightsabers.Enabled = cbLightsabers.Checked;
        }

        private void cbGrenades_CheckedChanged(object sender, EventArgs e)
        {
            flpGrenades.Enabled = cbGrenades.Checked;
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

        //Some quick selection buttons
        private void bAllOff_Click(object sender, EventArgs e)
        {
            foreach (Control c in ActiveForm.Controls)
            {
                if (c is CheckBox)
                {
                    if ((c as CheckBox).Checked)
                    {
                        (c as CheckBox).Checked = false;
                    }
                }
            }
        }

        private void bAllMax_Click(object sender, EventArgs e)
        {
            foreach (Control c in ActiveForm.Controls)
            {
                if (c is CheckBox)
                {
                    if (!(c as CheckBox).Checked)
                    {
                        (c as CheckBox).Checked = true;
                    }
                }
            }

            foreach (Control c in ActiveForm.Controls)
            {
                if (c is FlowLayoutPanel)
                {
                    foreach (Control o in (c as FlowLayoutPanel).Controls)
                    {
                        if (o is RadioButton)
                        {
                            if ((o as RadioButton).Text == "Max")
                            {
                                (o as RadioButton).Checked = true;
                            }
                        }
                        
                    }
                }
            }
        }

        #endregion
    }
}
