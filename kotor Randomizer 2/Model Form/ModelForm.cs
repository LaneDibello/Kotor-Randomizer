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
    public partial class ModelForm : Form
    {
        private bool checks_set = false;

        public ModelForm()
        {
            InitializeComponent();
            /*
             * These randomize settings use simple bitwise flags to store their settings
             * The right-most bit signalling if the category is active or not
             * The second bit denoting omission of large models
             * And the third for the omission of broken mdoels
             */ 

            cbCharRando.Checked = (Properties.Settings.Default.RandomizeCharModels & 1) > 0;
            cbLargeChars.Checked = (Properties.Settings.Default.RandomizeCharModels & 2) > 0;
            cbBrokenChars.Checked = (Properties.Settings.Default.RandomizeCharModels & 4) > 0;

            cbPlaceRando.Checked = (Properties.Settings.Default.RandomizePlaceModels & 1) > 0;
            cbLargePlace.Checked = (Properties.Settings.Default.RandomizePlaceModels & 2) > 0;
            cbBrokenPlace.Checked = (Properties.Settings.Default.RandomizePlaceModels & 4) > 0;

            cbDoorRando.Checked = (Properties.Settings.Default.RandomizeDoorModels & 1) > 0;
            cbLargeDoor.Checked = (Properties.Settings.Default.RandomizeDoorModels & 2) > 0;
            cbBrokenDoor.Checked = (Properties.Settings.Default.RandomizeDoorModels & 4) > 0;

            checks_set = true;
        }

        #region Private Methods

        //Main Checkboxes
        private void cbCharRando_CheckedChanged(object sender, EventArgs e)
        {
            pCharRando.Enabled = cbCharRando.Checked;
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizeCharModels ^= 1;
        }
        
        private void cbPlaceRando_CheckedChanged(object sender, EventArgs e)
        {
            pPlaceRando.Enabled = cbPlaceRando.Checked;
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizePlaceModels ^= 1;
        }

        private void cbDoorRando_CheckedChanged(object sender, EventArgs e)
        {
            pDoorRando.Enabled = cbDoorRando.Checked;
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizeDoorModels ^= 1;
        }

        //Omission Checkboxes
        private void cbLargeChars_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizeCharModels ^= 1 << 1;
        }

        private void cbBrokenChars_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizeCharModels ^= 1 << 2;
        }

        private void cbLargePlace_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizePlaceModels ^= 1 << 1;
        }

        private void cbBrokenPlace_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizePlaceModels ^= 1 << 2;
        }

        private void cbLargeDoor_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizeDoorModels ^= 1 << 1;
        }

        private void cbBrokenDoor_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            Properties.Settings.Default.RandomizeDoorModels ^= 1 << 2;
        }

        //Form Closing
        private void ModelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        #endregion
    }
}
