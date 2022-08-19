using kotor_Randomizer_2.Extensions;

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

        /**** Noise ****/
        [Info("Ambient Noise")]
        AmbientNoise,

        [Info("Cutscene Noise")]
        CutsceneNoise,

        /**** Character Sounds ****/
        [Info("NPC Sounds")]
        NpcSounds,

        [Info("Party Sounds")]
        PartySounds,
    }
}
