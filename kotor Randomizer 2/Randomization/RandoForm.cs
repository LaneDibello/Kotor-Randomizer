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
    //This is no where near finished, and partially temporary
    public partial class RandoForm : Form
    {
        public RandoForm()
        {
            InitializeComponent();

            if (!Properties.Settings.Default.KotorIsRandomized) { RunRando(); }
            else { UnRando(); }

        }

        private void RunRando()
        {
            int ActiveCats = CountActiveCategories();

            if (ActiveCats == 0)
            {
                MessageBox.Show("No Randomization Categories Selected");
                ActiveCats++;
            }

            RandomizationProgress.Step = 100 / ActiveCats;

            if (Properties.Settings.Default.module_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Modules";
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.item_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Items";
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.sound_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Music and Sounds";
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.model_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Models";
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.texture_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Textures";
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.twoda_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing 2-D Arrays";
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.text_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Text";
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.other_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Other Things";
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }

            currentRandoTask_label.Text = "Done!";
            //The Last Step in the Rando Process should be to set the progress value to 100
            RandomizationProgress.Value = 100;
        }

        private void UnRando()
        {
            //Figure out how many rando categories have been done
            //Consider placing a file in swkotor during the rando process the details waht was done so it can be more easily un-done.

            //Set the step to 100/those categories
            //RandomizationProgress.Step = 100 / ActiveCats;

            //Undo each category
            //if (CategoryRandomized)
            //{
            //    currentRandoTask_label.Text = "Unrandomizing Category";
            //    run appropriate unrando script
            //    RandomizationProgress.PerformStep();
            //}

            currentRandoTask_label.Text = "Done!";
            //The Last Step in the Rando Process should be to set the progress value to 100
            RandomizationProgress.Value = 100;
        }

        private int CountActiveCategories()
        {
            int i = 0;
            if (Properties.Settings.Default.module_rando_active) { i++; }
            if (Properties.Settings.Default.item_rando_active) { i++; }
            if (Properties.Settings.Default.sound_rando_active) { i++; }
            if (Properties.Settings.Default.model_rando_active) { i++; }
            if (Properties.Settings.Default.texture_rando_active) { i++; }
            if (Properties.Settings.Default.twoda_rando_active) { i++; }
            if (Properties.Settings.Default.text_rando_active) { i++; }
            if (Properties.Settings.Default.other_rando_active) { i++; }
            return i;
        }

        private void RandomizationProgress_Click(object sender, EventArgs e)
        {
            //TEMPORARY, DELETE LATER
            //RandomizationProgress.PerformStep();
        }
    }
}
