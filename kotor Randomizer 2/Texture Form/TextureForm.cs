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
    public partial class TextureForm : Form
    {
        public TextureForm()
        {
            InitializeComponent();

            //Set Initial Values
            RandomizeCubeMaps = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeCubeMaps;
            RandomizeCreatures = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeCreatures;
            RandomizeEffects = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeEffects;
            RandomizeItems = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeItems;
            RandomizePlanetary = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizePlanetary;
            RandomizeNPC = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeNPC;
            RandomizePlayHeads = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizePlayHeads;
            RandomizePlayBodies = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizePlayBodies;
            RandomizePlaceables = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizePlaceables;
            RandomizeParty = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeParty;
            RandomizeStunt = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeStunt;
            RandomizeVehicles = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeVehicles;
            RandomizeWeapons = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeWeapons;
            RandomizeOther = (Globals.RandomizationLevel)Properties.Settings.Default.TextureRandomizeOther;
        }

        #region Public Properties
        public Globals.RandomizationLevel RandomizeCubeMaps
        {
            get
            {
                if (cbCubeMaps.Checked)
                {
                    if (rbCubeMapsType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbCubeMapsMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbCubeMapsType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbCubeMaps.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeCreatures
        {
            get
            {
                if (cbCreatures.Checked)
                {
                    if (rbCreaturesType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbCreaturesMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbCreaturesType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbCreatures.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeEffects
        {
            get
            {
                if (cbEffects.Checked)
                {
                    if (rbEffectsType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbEffectsMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbEffectsType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbEffects.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeItems
        {
            get
            {
                if (cbItems.Checked)
                {
                    if (rbItemsType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbItemsMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbItemsType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbItems.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizePlanetary
        {
            get
            {
                if (cbPlanetary.Checked)
                {
                    if (rbPlanetaryType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbPlanetaryMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbPlanetaryType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbPlanetary.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeNPC
        {
            get
            {
                if (cbNPC.Checked)
                {
                    if (rbNPCType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbNPCMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbNPCType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbNPC.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizePlayHeads
        {
            get
            {
                if (cbPlayHeads.Checked)
                {
                    if (rbPlayHeadsType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbPlayHeadsMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbPlayHeadsType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbPlayHeads.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizePlayBodies
        {
            get
            {
                if (cbPlayBodies.Checked)
                {
                    if (rbPlayBodiesType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbPlayBodiesMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbPlayBodiesType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbPlayBodies.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizePlaceables
        {
            get
            {
                if (cbPlaceables.Checked)
                {
                    if (rbPlaceablesType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbPlaceablesMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbPlaceablesType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbPlaceables.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeParty
        {
            get
            {
                if (cbParty.Checked)
                {
                    if (rbPartyType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbPartyMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbPartyType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbParty.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeStunt
        {
            get
            {
                if (cbStunt.Checked)
                {
                    if (rbStuntType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbStuntMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbStuntType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbStunt.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeVehicles
        {
            get
            {
                if (cbVehicles.Checked)
                {
                    if (rbVehiclesType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbVehiclesMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbVehiclesType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbVehicles.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeWeapons
        {
            get
            {
                if (cbWeapons.Checked)
                {
                    if (rbWeaponsType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbWeaponsMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbWeaponsType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbWeapons.Checked = false;
                        break;
                }
            }
        }

        public Globals.RandomizationLevel RandomizeOther
        {
            get
            {
                if (cbOther.Checked)
                {
                    if (rbOtherType.Checked)
                    {
                        return Globals.RandomizationLevel.Type;
                    }
                    else
                    {
                        return Globals.RandomizationLevel.Max;
                    }
                }
                else
                {
                    return Globals.RandomizationLevel.None;
                }
            }
            set
            {
                switch (value)
                {
                    case Globals.RandomizationLevel.Max:
                        rbOtherMax.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Type:
                        rbOtherType.Checked = true;
                        break;
                    case Globals.RandomizationLevel.Subtype:
                    case Globals.RandomizationLevel.None:
                    default:
                        cbOther.Checked = false;
                        break;
                }
            }
        }

        #endregion

        #region private methods
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
        #endregion

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

            Properties.Settings.Default.Save();
        }

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
                        if ((o as RadioButton).Text == "Max")
                        {
                            (o as RadioButton).Checked = true;
                        }
                    }
                }
            }
        }


    }
}
