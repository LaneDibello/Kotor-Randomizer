using System;
using System.Drawing;

namespace kotor_Randomizer_2
{
    public partial class StartForm
    {

        //Makes the buttons change colors as you hover over. (After coding this it occured to me I could of just created my own derived control, but what's done is done)
        #region button colors
        private void module_button_MouseEnter(object sender, EventArgs e)
        {
            module_button.ForeColor = Color.FromArgb(211, 216, 8);
            module_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void module_button_MouseLeave(object sender, EventArgs e)
        {
            module_button.ForeColor = Color.FromArgb(0, 175, 255);
            module_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void item_button_MouseEnter(object sender, EventArgs e)
        {
            item_button.ForeColor = Color.FromArgb(211, 216, 8);
            item_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void item_button_MouseLeave(object sender, EventArgs e)
        {
            item_button.ForeColor = Color.FromArgb(0, 175, 255);
            item_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void sound_button_MouseEnter(object sender, EventArgs e)
        {
            sound_button.ForeColor = Color.FromArgb(211, 216, 8);
            sound_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void sound_button_MouseLeave(object sender, EventArgs e)
        {
            sound_button.ForeColor = Color.FromArgb(0, 175, 255);
            sound_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void model_button_MouseEnter(object sender, EventArgs e)
        {
            model_button.ForeColor = Color.FromArgb(211, 216, 8);
            model_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void model_button_MouseLeave(object sender, EventArgs e)
        {
            model_button.ForeColor = Color.FromArgb(0, 175, 255);
            model_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void texture_button_MouseEnter(object sender, EventArgs e)
        {
            texture_button.ForeColor = Color.FromArgb(211, 216, 8);
            texture_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void texture_button_MouseLeave(object sender, EventArgs e)
        {
            texture_button.ForeColor = Color.FromArgb(0, 175, 255);
            texture_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            twoda_button.ForeColor = Color.FromArgb(211, 216, 8);
            twoda_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            twoda_button.ForeColor = Color.FromArgb(0, 175, 255);
            twoda_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            text_button.ForeColor = Color.FromArgb(211, 216, 8);
            text_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            text_button.ForeColor = Color.FromArgb(0, 175, 255);
            text_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            other_button.ForeColor = Color.FromArgb(211, 216, 8);
            other_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            other_button.ForeColor = Color.FromArgb(0, 175, 255);
            other_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void path_button_MouseEnter(object sender, EventArgs e)
        {
            path_button.ForeColor = Color.FromArgb(211, 216, 8);
            path_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void path_button_MouseLeave(object sender, EventArgs e)
        {
            path_button.ForeColor = Color.FromArgb(0, 175, 255);
            path_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            seed_button.ForeColor = Color.FromArgb(211, 216, 8);
            seed_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            seed_button.ForeColor = Color.FromArgb(0, 175, 255);
            seed_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void randomize_button_MouseEnter(object sender, EventArgs e)
        {
            randomize_button.ForeColor = Color.FromArgb(211, 216, 8);
            randomize_button.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void randomize_button_MouseLeave(object sender, EventArgs e)
        {
            randomize_button.ForeColor = Color.FromArgb(0, 175, 255);
            randomize_button.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }

        private void bPresets_button_MouseEnter(object sender, EventArgs e)
        {
            bPresets.ForeColor = Color.FromArgb(211, 216, 8);
            bPresets.FlatAppearance.BorderColor = Color.FromArgb(211, 216, 8);
        }

        private void bPresets_button_MouseLeave(object sender, EventArgs e)
        {
            bPresets.ForeColor = Color.FromArgb(0, 175, 255);
            bPresets.FlatAppearance.BorderColor = Color.FromArgb(0, 175, 255);
        }
        #endregion
        //Fancy radio buttons that mimic the kotor style. similair story to the buttons above.
        #region custom radio buttons
        //Modules
        private void module_radio_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.module_rando_active)
            {
                module_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
            else
            {
                module_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
        }

        private void module_radio_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.module_rando_active)
            {
                module_radio.BackgroundImage = Properties.Resources.BoxSlected;
            }
            else
            {
                module_radio.BackgroundImage = Properties.Resources.BoxUnslected;
            }
        }

        private void module_radio_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.module_rando_active)
            {
                Properties.Settings.Default.module_rando_active = false;
                module_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
            else
            {
                Properties.Settings.Default.module_rando_active = true;
                module_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
        }

        //Sounds

        private void sound_radio_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.sound_rando_active)
            {
                Properties.Settings.Default.sound_rando_active = false;
                sound_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
            else
            {
                Properties.Settings.Default.sound_rando_active = true;
                sound_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
        }

        private void sound_radio_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.sound_rando_active)
            {
                sound_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
            else
            {
                sound_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
        }

        private void sound_radio_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.sound_rando_active)
            {
                sound_radio.BackgroundImage = Properties.Resources.BoxSlected;
            }
            else
            {
                sound_radio.BackgroundImage = Properties.Resources.BoxUnslected;
            }
        }

        //Models

        private void model_radio_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.model_rando_active)
            {
                Properties.Settings.Default.model_rando_active = false;
                model_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
            else
            {
                Properties.Settings.Default.model_rando_active = true;
                model_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
        }

        private void model_radio_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.model_rando_active)
            {
                model_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
            else
            {
                model_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
        }

        private void model_radio_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.model_rando_active)
            {
                model_radio.BackgroundImage = Properties.Resources.BoxSlected;
            }
            else
            {
                model_radio.BackgroundImage = Properties.Resources.BoxUnslected;
            }
        }

        //Items

        private void item_radio_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.item_rando_active)
            {
                Properties.Settings.Default.item_rando_active = false;
                item_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
            else
            {
                Properties.Settings.Default.item_rando_active = true;
                item_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
        }

        private void item_radio_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.item_rando_active)
            {
                item_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
            else
            {
                item_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
        }

        private void item_radio_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.item_rando_active)
            {
                item_radio.BackgroundImage = Properties.Resources.BoxSlected;
            }
            else
            {
                item_radio.BackgroundImage = Properties.Resources.BoxUnslected;
            }
        }

        //Textures

        private void texture_radio_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.texture_rando_active)
            {
                Properties.Settings.Default.texture_rando_active = false;
                texture_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
            else
            {
                Properties.Settings.Default.texture_rando_active = true;
                texture_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
        }

        private void texture_radio_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.texture_rando_active)
            {
                texture_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
            else
            {
                texture_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
        }

        private void texture_radio_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.texture_rando_active)
            {
                texture_radio.BackgroundImage = Properties.Resources.BoxSlected;
            }
            else
            {
                texture_radio.BackgroundImage = Properties.Resources.BoxUnslected;
            }
        }

        //2das

        private void twoda_radio_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.twoda_rando_active)
            {
                Properties.Settings.Default.twoda_rando_active = false;
                twoda_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
            else
            {
                Properties.Settings.Default.twoda_rando_active = true;
                twoda_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
        }

        private void twoda_radio_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.twoda_rando_active)
            {
                twoda_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
            else
            {
                twoda_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
        }

        private void twoda_radio_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.twoda_rando_active)
            {
                twoda_radio.BackgroundImage = Properties.Resources.BoxSlected;
            }
            else
            {
                twoda_radio.BackgroundImage = Properties.Resources.BoxUnslected;
            }
        }

        //Text

        private void text_radio_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.text_rando_active)
            {
                Properties.Settings.Default.text_rando_active = false;
                text_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
            else
            {
                Properties.Settings.Default.text_rando_active = true;
                text_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
        }

        private void text_radio_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.text_rando_active)
            {
                text_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
            else
            {
                text_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
        }

        private void text_radio_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.text_rando_active)
            {
                text_radio.BackgroundImage = Properties.Resources.BoxSlected;
            }
            else
            {
                text_radio.BackgroundImage = Properties.Resources.BoxUnslected;
            }
        }

        //Others

        private void other_radio_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.other_rando_active)
            {
                Properties.Settings.Default.other_rando_active = false;
                other_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
            else
            {
                Properties.Settings.Default.other_rando_active = true;
                other_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
        }

        private void other_radio_MouseEnter(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.other_rando_active)
            {
                other_radio.BackgroundImage = Properties.Resources.BoxActive;
            }
            else
            {
                other_radio.BackgroundImage = Properties.Resources.BoxActUnselected;
            }
        }

        private void other_radio_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.other_rando_active)
            {
                other_radio.BackgroundImage = Properties.Resources.BoxSlected;
            }
            else
            {
                other_radio.BackgroundImage = Properties.Resources.BoxUnslected;
            }
        }

        #endregion
    }
}