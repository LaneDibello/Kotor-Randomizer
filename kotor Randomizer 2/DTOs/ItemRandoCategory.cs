using kotor_Randomizer_2.Extensions;

namespace kotor_Randomizer_2.DTOs

{
    public enum ItemRandoCategory
    {
        Invalid = 0,

        [Info("Armbands")]
        Armbands,

        [Info("Armor")]
        Armor,

        [Info("Belts")]
        Belts,

        [Info("Blasters", "Any ranged weapon: pistol, rifle, carbine, etc.")]
        Blasters,

        [Info("Creature Hides", "Armor for NPC creatures. Can't be equipped in-game.")]
        CreatureHides,

        [Info("Creature Weapons", "Weapons for NPC creatures. Can't be equipped in-game.")]
        CreatureWeapons,

        [Info("Droid Equipment", "All droid equipment: armor, shields, etc.")]
        DroidEquipment,

        [Info("Gloves")]
        Gloves,

        [Info("Grenades")]
        Grenades,

        [Info("Implants")]
        Implants,

        [Info("Lightsabers")]
        Lightsabers,

        [Info("Masks")]
        Masks,

        [Info("Melee Weapons", "All melee weapons except lightsabers.")]
        MeleeWeapons,

        [Info("Mines")]
        Mines,

        [Info("Pazaak Cards", "Randomizes cards you buy in shops. Does not change opponent's decks or where you buy your own deck.")]
        PazaakCards,

        [Info("Stims/Medpacs")]
        Medical,

        [Info("Upgrades/Crystals", "Any weapon upgrades.")]
        Upgrades,

        [Info("Various", "Items that don't fall into another category.")]
        Various,

        [Info("Personal Crystal", "All variants of the main character's personal crystal.")]
        PCrystal,

        [Info("Props", "Items which are held in the hand by NPCs as a prop (e.g., a hand of pazaak cards).")]
        Props,
    }
}
