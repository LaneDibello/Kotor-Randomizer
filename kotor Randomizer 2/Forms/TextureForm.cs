using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kotor_Randomizer_2
{
    public partial class TextureForm : Form
    {
        public TextureForm()
        {
            InitializeComponent();

            //Set Initial Values
            RandomizeCubeMaps = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeCubeMaps;
            RandomizeCreatures = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeCreatures;
            RandomizeEffects = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeEffects;
            RandomizeItems = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeItems;
            RandomizePlanetary = (RandomizationLevel)Properties.Settings.Default.TextureRandomizePlanetary;
            RandomizeNPC = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeNPC;
            RandomizePlayHeads = (RandomizationLevel)Properties.Settings.Default.TextureRandomizePlayHeads;
            RandomizePlayBodies = (RandomizationLevel)Properties.Settings.Default.TextureRandomizePlayBodies;
            RandomizePlaceables = (RandomizationLevel)Properties.Settings.Default.TextureRandomizePlaceables;
            RandomizeParty = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeParty;
            RandomizeStunt = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeStunt;
            RandomizeVehicles = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeVehicles;
            RandomizeWeapons = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeWeapons;
            RandomizeOther = (RandomizationLevel)Properties.Settings.Default.TextureRandomizeOther;

            //Radio switch for which texture pack is to be randomized.
            //Right now its set up to only allow the user to randomize one of the 3 texture packs in the game. Primariliy because most
            //people only use the high quality pack, and also if there are stability issues this allows them to switch to a stable pack
            //in-game, without opening this program up.
            switch (Properties.Settings.Default.TexturePack)
            {
                case 1:
                    rbTextMed.Checked = true;
                    break;
                case 2:
                    rbTextLow.Checked = true;
                    break;
                default:
                case 0:
                    rbTextHigh.Checked = true;
                    break;
            }

            // Create easy access lists.
            CheckBoxes.AddRange(new List<CheckBox>()
            {
                cbCubeMaps,
                cbCreatures,
                cbEffects,
                cbItems,
                cbPlanetary,
                cbNPC,
                cbPlayHeads,
                cbPlayBodies,
                cbPlaceables,
                cbParty,
                cbStunt,
                cbVehicles,
                cbWeapons,
                cbOther,
            });

            TypeRadioButtons.AddRange(new List<RadioButton>()
            {
                rbCubeMapsType,
                rbCreaturesType,
                rbEffectsType,
                rbItemsType,
                rbPlanetaryType,
                rbNPCType,
                rbPlayHeadsType,
                rbPlayBodiesType,
                rbPlaceablesType,
                rbPartyType,
                rbStuntType,
                rbVehiclesType,
                rbWeaponsType,
                rbOtherType,
            });

            MaxRadioButtons.AddRange(new List<RadioButton>()
            {
                rbCubeMapsMax,
                rbCreaturesMax,
                rbEffectsMax,
                rbItemsMax,
                rbPlanetaryMax,
                rbNPCMax,
                rbPlayHeadsMax,
                rbPlayBodiesMax,
                rbPlaceablesMax,
                rbPartyMax,
                rbStuntMax,
                rbVehiclesMax,
                rbWeaponsMax,
                rbOtherMax,
            });
        }

        #region Public Properties
        public RandomizationLevel RandomizeCubeMaps
        {
            get
            {
                if (cbCubeMaps.Checked)
                {
                    if (rbCubeMapsType.Checked)
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
                        rbCubeMapsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbCubeMapsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbCubeMaps.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeCreatures
        {
            get
            {
                if (cbCreatures.Checked)
                {
                    if (rbCreaturesType.Checked)
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
                        rbCreaturesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbCreaturesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbCreatures.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeEffects
        {
            get
            {
                if (cbEffects.Checked)
                {
                    if (rbEffectsType.Checked)
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
                        rbEffectsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbEffectsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbEffects.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeItems
        {
            get
            {
                if (cbItems.Checked)
                {
                    if (rbItemsType.Checked)
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
                        rbItemsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbItemsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbItems.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizePlanetary
        {
            get
            {
                if (cbPlanetary.Checked)
                {
                    if (rbPlanetaryType.Checked)
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
                        rbPlanetaryMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbPlanetaryType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbPlanetary.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeNPC
        {
            get
            {
                if (cbNPC.Checked)
                {
                    if (rbNPCType.Checked)
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
                        rbNPCMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbNPCType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbNPC.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizePlayHeads
        {
            get
            {
                if (cbPlayHeads.Checked)
                {
                    if (rbPlayHeadsType.Checked)
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
                        rbPlayHeadsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbPlayHeadsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbPlayHeads.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizePlayBodies
        {
            get
            {
                if (cbPlayBodies.Checked)
                {
                    if (rbPlayBodiesType.Checked)
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
                        rbPlayBodiesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbPlayBodiesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbPlayBodies.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizePlaceables
        {
            get
            {
                if (cbPlaceables.Checked)
                {
                    if (rbPlaceablesType.Checked)
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
                        rbPlaceablesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbPlaceablesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbPlaceables.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeParty
        {
            get
            {
                if (cbParty.Checked)
                {
                    if (rbPartyType.Checked)
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
                        rbPartyMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbPartyType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbParty.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeStunt
        {
            get
            {
                if (cbStunt.Checked)
                {
                    if (rbStuntType.Checked)
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
                        rbStuntMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbStuntType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbStunt.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeVehicles
        {
            get
            {
                if (cbVehicles.Checked)
                {
                    if (rbVehiclesType.Checked)
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
                        rbVehiclesMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbVehiclesType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbVehicles.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeWeapons
        {
            get
            {
                if (cbWeapons.Checked)
                {
                    if (rbWeaponsType.Checked)
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
                        rbWeaponsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbWeaponsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbWeapons.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeOther
        {
            get
            {
                if (cbOther.Checked)
                {
                    if (rbOtherType.Checked)
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
                        rbOtherMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbOtherType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbOther.Checked = false;
                        break;
                }
            }
        }

        #endregion

        #region private methods
        private List<CheckBox> CheckBoxes = new List<CheckBox>();
        private List<RadioButton> TypeRadioButtons = new List<RadioButton>();
        private List<RadioButton> MaxRadioButtons = new List<RadioButton>();

        private void cbCubeMaps_CheckedChanged(object sender, EventArgs e)
        {
            flpCubeMaps.Enabled = cbCubeMaps.Checked;
        }

        private void cbCreatures_CheckedChanged(object sender, EventArgs e)
        {
            flpCreatures.Enabled = cbCreatures.Checked;
        }

        private void cbEffects_CheckedChanged(object sender, EventArgs e)
        {
            flpEffects.Enabled = cbEffects.Checked;
        }

        private void cbItems_CheckedChanged(object sender, EventArgs e)
        {
            flpItems.Enabled = cbItems.Checked;
        }

        private void cbPlanetary_CheckedChanged(object sender, EventArgs e)
        {
            flpPlanetary.Enabled = cbPlanetary.Checked;
        }

        private void cbNPC_CheckedChanged(object sender, EventArgs e)
        {
            flpNPC.Enabled = cbNPC.Checked;
        }

        private void cbPlayHeads_CheckedChanged(object sender, EventArgs e)
        {
            flpPlayHeads.Enabled = cbPlayHeads.Checked;
        }

        private void cbPlayBodies_CheckedChanged(object sender, EventArgs e)
        {
            flpPlayBodies.Enabled = cbPlayBodies.Checked;
        }

        private void cbPlaceables_CheckedChanged(object sender, EventArgs e)
        {
            flpPlaceables.Enabled = cbPlaceables.Checked;
        }

        private void cbParty_CheckedChanged(object sender, EventArgs e)
        {
            flpParty.Enabled = cbParty.Checked;
        }

        private void cbStunt_CheckedChanged(object sender, EventArgs e)
        {
            flpStunt.Enabled = cbStunt.Checked;
        }

        private void cbVehicles_CheckedChanged(object sender, EventArgs e)
        {
            flpVehicles.Enabled = cbVehicles.Checked;
        }

        private void cbWeapons_CheckedChanged(object sender, EventArgs e)
        {
            flpWeapons.Enabled = cbWeapons.Checked;
        }

        private void cbOther_CheckedChanged(object sender, EventArgs e)
        {
            flpOther.Enabled = cbOther.Checked;
        }

        private void TextureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.TextureRandomizeCubeMaps = (int)RandomizeCubeMaps;
            Properties.Settings.Default.TextureRandomizeCreatures = (int)RandomizeCreatures;
            Properties.Settings.Default.TextureRandomizeEffects = (int)RandomizeEffects;
            Properties.Settings.Default.TextureRandomizeItems = (int)RandomizeItems;
            Properties.Settings.Default.TextureRandomizePlanetary = (int)RandomizePlanetary;
            Properties.Settings.Default.TextureRandomizeNPC = (int)RandomizeNPC;
            Properties.Settings.Default.TextureRandomizePlayHeads = (int)RandomizePlayHeads;
            Properties.Settings.Default.TextureRandomizePlayBodies = (int)RandomizePlayBodies;
            Properties.Settings.Default.TextureRandomizePlaceables = (int)RandomizePlaceables;
            Properties.Settings.Default.TextureRandomizeParty = (int)RandomizeParty;
            Properties.Settings.Default.TextureRandomizeStunt = (int)RandomizeStunt;
            Properties.Settings.Default.TextureRandomizeVehicles = (int)RandomizeVehicles;
            Properties.Settings.Default.TextureRandomizeWeapons = (int)RandomizeWeapons;
            Properties.Settings.Default.TextureRandomizeOther = (int)RandomizeOther;

            if (rbTextHigh.Checked) { Properties.Settings.Default.TexturePack = 0; }
            if (rbTextMed.Checked) { Properties.Settings.Default.TexturePack = 1; }
            if (rbTextLow.Checked) { Properties.Settings.Default.TexturePack = 2; }

            Properties.Settings.Default.Save();
        }

        private void bToggleAll_Click(object sender, EventArgs e)
        {
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
