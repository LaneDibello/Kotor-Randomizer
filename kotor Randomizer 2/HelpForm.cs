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
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            cbCata.DataSource = help_catagories;
            
        }

        static private List<string> help_catagories = new List<string>() { "General", "Modules", "Items", "Music and Sounds", "Models", "Textures", "2DAs", "Text", "Other", "Paths", "Seeds", "Presets", "Randomization" };

        private void ParseHelpText(string res)
        {
            string source = " \t ";

            switch (res)
            {
                case "General":
                    source = Properties.Resources.GeneralHelp;
                    break;
                case "Modules":
                    source = Properties.Resources.ModuleHelp;
                    break;
                case "Items":
                    source = Properties.Resources.ItemHelp;
                    break;
                case "Music and Sounds":
                    source = Properties.Resources.SoundHelp;
                    break;
                case "Models":
                    source = Properties.Resources.ModelHelp;
                    break;
                case "Textures":
                    source = Properties.Resources.TextureHelp;
                    break;
                case "2DAs":
                    source = Properties.Resources.TwoDAHelp;
                    break;
                case "Text":

                    break;
                case "Other":

                    break;
                case "Paths":

                    break;
                case "Seeds":

                    break;
                case "Presets":

                    break;
                case "Randomization":

                    break;
            }

            string[] parts = source.Split("\t".ToCharArray(), 2);

            tbInfo1.Text = parts[0];
            tbInfo2.Text = parts[1];
        }

        private void llDisc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llDisc.LinkVisited = true;
            
            System.Diagnostics.Process.Start("http://discord.gg/Q2uPRVu");
        }

        private void llSRC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llSRC.LinkVisited = true;

            System.Diagnostics.Process.Start("https://www.speedrun.com/kotor1");

        }

        private void llStratWiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llStratWiki.LinkVisited = true;

            System.Diagnostics.Process.Start("https://strategywiki.org/wiki/Star_Wars:_Knights_of_the_Old_Republic/Cheats#Windows");

        }

        private void llTwitch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llTwitch.LinkVisited = true;

            System.Diagnostics.Process.Start("https://www.twitch.tv/lane_dibello");

        }

        private void llYoutube_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llYoutube.LinkVisited = true;

            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UC_EnGk6GWeY-jaAoMzUiG2w");
        }

        private void cbCata_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParseHelpText(cbCata.SelectedValue as string);
        }
    }
}
