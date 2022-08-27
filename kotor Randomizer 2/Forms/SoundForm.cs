using System;
using System.Collections.Generic;
using System.Linq;
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

            RandomizeAreaMusic     = Properties.Settings.Default.RandomizeAreaMusic;
            RandomizeBattleMusic   = Properties.Settings.Default.RandomizeBattleMusic;
            RandomizeAmbientNoise  = Properties.Settings.Default.RandomizeAmbientNoise;
            RandomizeCutsceneNoise = Properties.Settings.Default.RandomizeCutsceneNoise;
            //RandomizeNpcSounds     = Properties.Settings.Default.RandomizeNpcSounds;
            RandomizeNpcSounds     = RandomizationLevel.None;   // Functionality Disabled
            RandomizePartySounds   = Properties.Settings.Default.RandomizePartySounds;
            cbRemoveDmca.Checked   = Properties.Settings.Default.RemoveDmcaMusic;

            //MixNpcAndParty = Properties.Settings.Default.MixNpcAndPartySounds;
            MixNpcAndParty = false; // Functionality Disabled

            // Create easy access lists.
            CheckBoxes.AddRange(new List<CheckBox>()
            {
                cbAreaMusic,
                cbBattleMusic,
                cbAmbientNoise,
                cbCutsceneNoise,
                //cbNpcSounds,      // Randomization not implemented.
                cbPartySounds,
            });

            TypeRadioButtons.AddRange(new List<RadioButton>()
            {
                rbAreaMusicType,
                rbBattleMusicType,
                rbAmbientNoiseType,
                rbCutsceneNoiseType,
                rbPartySoundsType,
            });

            MaxRadioButtons.AddRange(new List<RadioButton>()
            {
                rbAreaMusicMax,
                rbBattleMusicMax,
                rbAmbientNoiseMax,
                rbCutsceneNoiseMax,
                rbPartySoundsMax,
            });
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

        private List<CheckBox> CheckBoxes = new List<CheckBox>();
        private List<RadioButton> TypeRadioButtons = new List<RadioButton>();
        private List<RadioButton> MaxRadioButtons = new List<RadioButton>();

        // Checkbox handlers
        private void cbAreaMusic_CheckedChanged(object sender, EventArgs e)
        {
            flpAreaMusic.Enabled = cbAreaMusic.Checked;
            Properties.Settings.Default.RandomizeAreaMusic = RandomizeAreaMusic;
        }

        private void cbAmbientNoise_CheckedChanged(object sender, EventArgs e)
        {
            flpAmbientNoise.Enabled = cbAmbientNoise.Checked;
            Properties.Settings.Default.RandomizeAmbientNoise = RandomizeAmbientNoise;
        }

        private void cbBattleMusic_CheckedChanged(object sender, EventArgs e)
        {
            flpBattleMusic.Enabled = cbBattleMusic.Checked;
            Properties.Settings.Default.RandomizeBattleMusic = RandomizeBattleMusic;
        }

        private void cbCutsceneNoise_CheckedChanged(object sender, EventArgs e)
        {
            flpCutsceneNoise.Enabled = cbCutsceneNoise.Checked;
            Properties.Settings.Default.RandomizeCutsceneNoise = RandomizeCutsceneNoise;
        }

        private void cbNpcSounds_CheckedChanged(object sender, EventArgs e)
        {
            flpNpcSounds.Enabled = cbNpcSounds.Checked;
            Properties.Settings.Default.RandomizeNpcSounds = RandomizeNpcSounds;
        }

        private void cbPartySounds_CheckedChanged(object sender, EventArgs e)
        {
            flpPartySounds.Enabled = cbPartySounds.Checked;
            Properties.Settings.Default.RandomizePartySounds = RandomizePartySounds;
        }

        private void cbRemoveDmca_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RemoveDmcaMusic = cbRemoveDmca.Checked;
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

        // Radio button handlers
        private void rbAreaMusicType_CheckedChanged(object sender, EventArgs e)      { Properties.Settings.Default.RandomizeAreaMusic     = RandomizeAreaMusic;     }
        private void rbAreaMusicMax_CheckedChanged(object sender, EventArgs e)       { Properties.Settings.Default.RandomizeAreaMusic     = RandomizeAreaMusic;     }
        private void rbAmbientNoiseType_CheckedChanged(object sender, EventArgs e)   { Properties.Settings.Default.RandomizeAmbientNoise  = RandomizeAmbientNoise;  }
        private void rbAmbientNoiseMax_CheckedChanged(object sender, EventArgs e)    { Properties.Settings.Default.RandomizeAmbientNoise  = RandomizeAmbientNoise;  }
        private void rbBattleMusicType_CheckedChanged(object sender, EventArgs e)    { Properties.Settings.Default.RandomizeBattleMusic   = RandomizeBattleMusic;   }
        private void rbBattleMusicMax_CheckedChanged(object sender, EventArgs e)     { Properties.Settings.Default.RandomizeBattleMusic   = RandomizeBattleMusic;   }
        private void rbCutsceneNoiseType_CheckedChanged(object sender, EventArgs e)  { Properties.Settings.Default.RandomizeCutsceneNoise = RandomizeCutsceneNoise; }
        private void rbCutsceneNoiseMax_CheckedChanged(object sender, EventArgs e)   { Properties.Settings.Default.RandomizeCutsceneNoise = RandomizeCutsceneNoise; }
        private void rbPartySoundsActions_CheckedChanged(object sender, EventArgs e) { Properties.Settings.Default.RandomizePartySounds   = RandomizePartySounds;   }
        private void rbPartySoundsType_CheckedChanged(object sender, EventArgs e)    { Properties.Settings.Default.RandomizePartySounds   = RandomizePartySounds;   }
        private void rbPartySoundsMax_CheckedChanged(object sender, EventArgs e)     { Properties.Settings.Default.RandomizePartySounds   = RandomizePartySounds;   }

        private void SoundForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
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
