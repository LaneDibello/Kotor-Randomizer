using System;
using System.Windows.Forms;

namespace kotor_Randomizer_2
{
    public partial class SoundForm : Form
    {
        public SoundForm()
        {
            InitializeComponent();

            // Disable NPC Randomization
            cbNpcSounds.Enabled = false;
            flpNpcSounds.Enabled = false;
            flpNpcSounds.Visible = false;
            cbMixNpcParty.Enabled = false;
            cbMixNpcParty.Visible = false;

            RandomizeAreaMusic = (RandomizationLevel)Properties.Settings.Default.RandomizeAreaMusic;
            RandomizeBattleMusic = (RandomizationLevel)Properties.Settings.Default.RandomizeBattleMusic;
            RandomizeAmbientNoise = (RandomizationLevel)Properties.Settings.Default.RandomizeAmbientNoise;
            RandomizeCutsceneNoise = (RandomizationLevel)Properties.Settings.Default.RandomizeCutsceneNoise;
            //RandomizeNpcSounds = Properties.Settings.Default.RandomizeNpcSounds;
            RandomizeNpcSounds = RandomizationLevel.None; // Functionality Disabled
            RandomizePartySounds = (RandomizationLevel)Properties.Settings.Default.RandomizePartySounds;

            //MixNpcAndParty = Properties.Settings.Default.MixNpcAndPartySounds;
            MixNpcAndParty = false; // Functionality Disabled
        }


        #region Public Properties

        public RandomizationLevel RandomizeAreaMusic
        {
            get
            {
                if (cbAreaMusic.Checked)
                {
                    if (rbAreaMusicType.Checked)
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
                        rbAreaMusicMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbAreaMusicType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbAreaMusic.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeBattleMusic
        {
            get
            {
                if (cbBattleMusic.Checked)
                {
                    if (rbBattleMusicType.Checked)
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
                        rbBattleMusicMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbBattleMusicType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbBattleMusic.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeAmbientNoise
        {
            get
            {
                if (cbAmbientNoise.Checked)
                {
                    if (rbAmbientNoiseType.Checked)
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
                        rbAmbientNoiseMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbAmbientNoiseType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbAmbientNoise.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeCutsceneNoise
        {
            get
            {
                if (cbCutsceneNoise.Checked)
                {
                    if (rbCutsceneNoiseType.Checked)
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
                        rbCutsceneNoiseMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbCutsceneNoiseType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        cbCutsceneNoise.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizeNpcSounds
        {
            get
            {
                if (cbNpcSounds.Checked)
                {
                    if (rbNpcSoundsActions.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbNpcSoundsType.Checked)
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
                        rbNpcSoundsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbNpcSoundsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbNpcSoundsActions.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbNpcSounds.Checked = false;
                        break;
                }
            }
        }

        public RandomizationLevel RandomizePartySounds
        {
            get
            {
                if (cbPartySounds.Checked)
                {
                    if (rbPartySoundsActions.Checked)
                    {
                        return RandomizationLevel.Subtype;
                    }
                    else if (rbPartySoundsType.Checked)
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
                        rbPartySoundsMax.Checked = true;
                        break;
                    case RandomizationLevel.Type:
                        rbPartySoundsType.Checked = true;
                        break;
                    case RandomizationLevel.Subtype:
                        rbPartySoundsActions.Checked = true;
                        break;
                    case RandomizationLevel.None:
                    default:
                        cbPartySounds.Checked = false;
                        break;
                }
            }
        }

        public bool MixNpcAndParty
        {
            get { return cbMixNpcParty.Checked; }
            set { cbMixNpcParty.Checked = value; }
        }
        
        #endregion

        #region Private Methods

        private void cbAreaMusic_CheckedChanged(object sender, EventArgs e)
        {
            flpAreaMusic.Enabled = cbAreaMusic.Checked;
        }

        private void cbBattleMusic_CheckedChanged(object sender, EventArgs e)
        {
            flpBattleMusic.Enabled = cbBattleMusic.Checked;
        }

        private void cbAmbientNoise_CheckedChanged(object sender, EventArgs e)
        {
            flpAmbientNoise.Enabled = cbAmbientNoise.Checked;
        }

        private void cbCutsceneNoise_CheckedChanged(object sender, EventArgs e)
        {
            flpCutsceneNoise.Enabled = cbCutsceneNoise.Checked;
        }

        private void cbNpcSounds_CheckedChanged(object sender, EventArgs e)
        {
            flpNpcSounds.Enabled = cbNpcSounds.Checked;
        }

        private void cbPartySounds_CheckedChanged(object sender, EventArgs e)
        {
            if (!MixNpcAndParty)
            {
                flpPartySounds.Enabled = cbPartySounds.Checked;
            }
        }

        private void cbMixNpcParty_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbMixNpcParty.Checked;
            cbNpcSounds.Checked = isChecked;
            cbNpcSounds.Enabled = !isChecked;
            cbPartySounds.Checked = isChecked;
            cbPartySounds.Enabled = !isChecked;
            flpPartySounds.Enabled = false;
            flpPartySounds.Visible = !isChecked;
        }

        //I dont think these do anything, but glasnonck had them so i'm leaving em in
        private void rbNpcSoundsActions_CheckedChanged(object sender, EventArgs e)
        {
            if (MixNpcAndParty)
            {
                rbPartySoundsActions.Checked = rbNpcSoundsActions.Checked;
            }
        }

        private void rbNpcSoundsType_CheckedChanged(object sender, EventArgs e)
        {
            if (MixNpcAndParty)
            {
                rbPartySoundsType.Checked = rbNpcSoundsType.Checked;
            }
        }

        private void rbNpcSoundsMax_CheckedChanged(object sender, EventArgs e)
        {
            if (MixNpcAndParty)
            {
                rbPartySoundsMax.Checked = rbNpcSoundsMax.Checked;
            }
        }

        private void SoundForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.RandomizeAreaMusic = (int)RandomizeAreaMusic;
            Properties.Settings.Default.RandomizeAmbientNoise = (int)RandomizeAmbientNoise;
            Properties.Settings.Default.RandomizeBattleMusic = (int)RandomizeBattleMusic;
            Properties.Settings.Default.RandomizeCutsceneNoise = (int)RandomizeCutsceneNoise;
            Properties.Settings.Default.RandomizeNpcSounds = (int)RandomizeNpcSounds;
            Properties.Settings.Default.RandomizePartySounds = (int)RandomizePartySounds;
            Properties.Settings.Default.MixNpcAndPartySounds = MixNpcAndParty;
            Properties.Settings.Default.Save();
        }

        #endregion
    }
}
