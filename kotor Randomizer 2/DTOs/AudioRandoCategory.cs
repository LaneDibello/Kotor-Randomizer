﻿using kotor_Randomizer_2.Extensions;

namespace kotor_Randomizer_2.DTOs
{
    public enum AudioRandoCategory
    {
        Invalid = 0,

        /**** Music ****/
        [Info("Area Music")]
        AreaMusic,

        [Info("Battle Music")]
        BattleMusic,

        [Info("Charater Themes")]
        CharacterTheme,

        /**** Noise ****/
        [Info("Ambient Noise")]
        AmbientNoise,

        [Info("Cutscene Noise")]
        CutsceneNoise,

        [Info("Feedback SFX")]
        FeedbackSFX,

        [Info("row:footstepsounds", "Footstep SFX Table", null)]
        FootstepSFX,

        [Info("col:guisounds;col:inventorysnds", "GUI/Inventory SFX Tables", null)]
        GuiSFX,

        [Info("col:swingsounds;row:weaponsounds", "Weapon SFX Table", null)]
        WeaponSFX,

        /**** Character Sounds ****/
        [Info("NPC Sounds")]
        NpcSounds,

        [Info("Party Sounds")]
        PartySounds,
    }
}
