using kotor_Randomizer_2.DBOs;
using System.Collections.Generic;

namespace kotor_Randomizer_2.Models
{
    public partial class RandomizableItem
    {
        /// <summary>
        /// Collection of all randomizable items in Kotor 1.
        /// </summary>
        public static readonly List<RandomizableItem> KOTOR1_ITEMS = new List<RandomizableItem>()
        {
            new RandomizableItem() { ID = 24117330, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g1_a_class5001",   Label = "Light Exoskeleton" },
            new RandomizableItem() { ID = 24117331, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g1_a_class5002",   Label = "Baragwin Shadow Armor" },
            new RandomizableItem() { ID = 24117332, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g1_a_class6001",   Label = "Environmental Bastion Armor" },
            new RandomizableItem() { ID = 24117333, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g1_a_class8001",   Label = "Heavy Exoskeleton" },
            new RandomizableItem() { ID = 24117334, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g1_i_belt001",     Label = "Baragwin Stealth Unit" },
            new RandomizableItem() { ID = 24117335, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g1_i_drdcomspk01", Label = "Advanced Droid Interface" },
            new RandomizableItem() { ID = 24117336, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g1_i_drdhvplat01", Label = "Composite Heavy Plating" },
            new RandomizableItem() { ID = 24117337, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g1_i_drdshld001",  Label = "Baragwin Droid Shield" },
            new RandomizableItem() { ID = 24117338, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g1_i_drdutldev01", Label = "Baragwin Flame Thrower" },
            new RandomizableItem() { ID = 24117339, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g1_i_drdutldev02", Label = "Baragwin Stun Ray" },
            new RandomizableItem() { ID = 24117340, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g1_i_drdutldev03", Label = "Baragwin Shield Disruptor" },
            new RandomizableItem() { ID = 24117341, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g1_i_gauntlet01",  Label = "Advanced Stabilizer Gloves" },
            new RandomizableItem() { ID = 24117342, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g1_i_implant301",  Label = "Advanced Sensory Implant" },
            new RandomizableItem() { ID = 24117343, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g1_i_implant302",  Label = "Advanced Bio-Stabilizer Implant" },
            new RandomizableItem() { ID = 24117344, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g1_i_implant303",  Label = "Advanced Combat Implant" },
            new RandomizableItem() { ID = 24117345, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g1_i_implant304",  Label = "Advanced Alacrity Impant" },
            new RandomizableItem() { ID = 24117346, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g1_i_mask01",      Label = "Advanced Bio-Stabilizer Mask" },
            new RandomizableItem() { ID = 24117347, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g1_i_mask02",      Label = "Medical Interface Visor" },
            new RandomizableItem() { ID = 24117348, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g1_i_mask03",      Label = "Advanced Agent Interface" },
            new RandomizableItem() { ID = 24117349, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g1_w_dblsbr001",   Label = "Double-Bladed Lightsaber, HotG" },
            new RandomizableItem() { ID = 24117350, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g1_w_dblsbr002",   Label = "Double-Bladed Lightsaber, MotF" },
            new RandomizableItem() { ID = 24117351, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g1_w_dsrptrfl001", Label = "Baragwin Disruptor-X Weapon" },
            new RandomizableItem() { ID = 24117352, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g1_w_hvrptbltr01", Label = "Baragwin Heavy Repeating Blaster" },
            new RandomizableItem() { ID = 24117353, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g1_w_ionrfl01",    Label = "Baragwin Ion-X Weapon" },
            new RandomizableItem() { ID = 24117354, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g1_w_lghtsbr01",   Label = "Lightsaber, HotG" },
            new RandomizableItem() { ID = 24117355, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g1_w_lghtsbr02",   Label = "Lightsaber, MotF" },
            new RandomizableItem() { ID = 24117356, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g1_w_rptnblstr01", Label = "Baragwin Assault Gun" },
            new RandomizableItem() { ID = 24117357, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g1_w_sbrcrstl20",  Label = "Heart of the Guardian" },
            new RandomizableItem() { ID = 24117358, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g1_w_sbrcrstl21",  Label = "Mantle of the Force" },
            new RandomizableItem() { ID = 24117359, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g1_w_shortsbr01",  Label = "Short Lightsaber, HotG" },
            new RandomizableItem() { ID = 24117360, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g1_w_shortsbr02",  Label = "Short Lightsaber, MotF" },
            new RandomizableItem() { ID = 24117361, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g1_w_vbroswrd01",  Label = "Baragwin Assault Blade" },
            new RandomizableItem() { ID = 24117362, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4001",    Label = "Combat Suit" },
            new RandomizableItem() { ID = 24117363, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4002",    Label = "Zabrak Combat Suit" },
            new RandomizableItem() { ID = 24117364, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4003",    Label = "Echani Light Armor" },
            new RandomizableItem() { ID = 24117365, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4004",    Label = "Cinnagar Weave Armor" },
            new RandomizableItem() { ID = 24117366, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4005",    Label = "Massassi Ceremonial Armor" },
            new RandomizableItem() { ID = 24117367, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4006",    Label = "Bandon's Fiber Armor" }, // lgt, 5d+5, ug, 25% fire res (dark color)
            new RandomizableItem() { ID = 24117368, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4007",    Label = "Bandon's Fiber Armor +1" }, // lgt, 7d+5, !ug, 25% fire res
            new RandomizableItem() { ID = 24117369, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4008",    Label = "Bandon's Fiber Armor +2" }, // lgt, 7d+5, !ug, 25% fire res, mind Imm
            new RandomizableItem() { ID = 24117370, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class4009",    Label = "Echani Fiber Armor" },
            new RandomizableItem() { ID = 24117371, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5001",    Label = "Heavy Combat Suit" },
            new RandomizableItem() { ID = 24117372, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5002",    Label = "Bonadan Alloy Heavy Suit" },
            new RandomizableItem() { ID = 24117373, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5003",    Label = "Zabrak Battle Armor" },
            new RandomizableItem() { ID = 24117374, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5004",    Label = "Zabrak Field Armor" },
            new RandomizableItem() { ID = 24117375, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5005",    Label = "Reinforced Fiber Armor" },
            new RandomizableItem() { ID = 24117376, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5006",    Label = "Ulic Qel Droma's Mesh Suit" },
            new RandomizableItem() { ID = 24117377, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5007",    Label = "Eriadu Prototype Armor" }, // lgt, 6+4, ug
            new RandomizableItem() { ID = 24117378, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5008",    Label = "Eriadu Prototype Armor +1" }, // lgt, 9+4, fug
            new RandomizableItem() { ID = 24117379, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5009",    Label = "Eriadu Prototype Armor +2" }, // lgt, 9+4, !ug, 30% cold res, mind imm
            new RandomizableItem() { ID = 24117380, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class5010",    Label = "Republic Mod Armor" },
            new RandomizableItem() { ID = 24117381, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6001",    Label = "Military Suit" },
            new RandomizableItem() { ID = 24117382, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6002",    Label = "Echani Battle Armor" },
            new RandomizableItem() { ID = 24117383, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6003",    Label = "Cinnagar War Suit" },
            new RandomizableItem() { ID = 24117384, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6004",    Label = "Verpine Fiber Mesh" },
            new RandomizableItem() { ID = 24117385, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6005",    Label = "Arkanian Bond Armor" },
            new RandomizableItem() { ID = 24117386, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6006",    Label = "Exar Kun's Light Battle Suit" },
            new RandomizableItem() { ID = 24117387, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6007",    Label = "Davik's War Suit" }, // med, 8+3, 10% cold 10% fire res
            new RandomizableItem() { ID = 24117388, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6008",    Label = "Davik's War Suit +1" }, // med, 10+3, !ug, 10% cold 10% fire res
            new RandomizableItem() { ID = 24117389, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class6009",    Label = "Davik's War Suit +2" }, // med, 10+3, !ug, 20% cold 20% fire res, mind imm
            new RandomizableItem() { ID = 24117390, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class7001",    Label = "Light Battle Armor" },
            new RandomizableItem() { ID = 24117391, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class7002",    Label = "Bronzium Light Battle Armor" },
            new RandomizableItem() { ID = 24117392, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class7003",    Label = "Powered Light Battle Armor" },
            new RandomizableItem() { ID = 24117393, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class7004",    Label = "Krath Heavy Armor" },
            new RandomizableItem() { ID = 24117394, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class7005",    Label = "Krath Holy Battle Suit" },
            new RandomizableItem() { ID = 24117395, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class7006",    Label = "Jamoh Hogra's Battle Armor" },
            new RandomizableItem() { ID = 24117396, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class8001",    Label = "Battle Armor" },
            new RandomizableItem() { ID = 24117397, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class8002",    Label = "Powered Battle Armor" },
            new RandomizableItem() { ID = 24117398, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class8003",    Label = "Cinnagar Plate Armor" },
            new RandomizableItem() { ID = 24117399, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class8004",    Label = "Mandalorian Armor" },
            new RandomizableItem() { ID = 24117400, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class8005",    Label = "Calo Nord's Battle Armor" }, // hvy, 9+1, ug, 10% cold fire sonic
            new RandomizableItem() { ID = 24117401, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class8006",    Label = "Calo Nord's Battle Armor +1" }, // hvy, 12+1, !ug, 25% cold fire sonic, crit imm
            new RandomizableItem() { ID = 24117402, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class8007",    Label = "Calo Nord's Battle Armor +2" }, // hvy, 12+1, !ug, 25% cold fire sonic, crit & mind imm
            new RandomizableItem() { ID = 24117403, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class8009",    Label = "Verpine Zal Alloy Mesh" },
            new RandomizableItem() { ID = 24117404, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9001",    Label = "Heavy Battle Armor" },
            new RandomizableItem() { ID = 24117405, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9002",    Label = "Durasteel Heavy Armor" },
            new RandomizableItem() { ID = 24117406, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9003",    Label = "Mandalorian Battle Armor" },
            new RandomizableItem() { ID = 24117407, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9004",    Label = "Mandalorian Heavy Armor" },
            new RandomizableItem() { ID = 24117408, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9005",    Label = "Jurgan Kalta's Power Suit" }, // hvy, 10+0, ug, 10% cold fire sonic
            new RandomizableItem() { ID = 24117409, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9006",    Label = "Jurgan Kalta's Power Suit +1" }, // hvy, 13+0, !ug, 25% cold fire sonic
            new RandomizableItem() { ID = 24117410, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9007",    Label = "Jurgan Kalta's Power Suit +2" }, // hvy, 13+0, !ug, 30% cold fire sonic, mind imm
            new RandomizableItem() { ID = 24117411, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9009",    Label = "Cassus Fett's Battle Armor" }, // hvy, 10+0, ug, 10% cold fire sonic
            new RandomizableItem() { ID = 24117412, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9010",    Label = "Mandalorian Assault Armor" },
            new RandomizableItem() { ID = 24117413, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_class9011",    Label = "Cassus Fett's Battle Armor +1" }, // hvy, 14+0, !ug, 25% cold fire sonic, str+1
            new RandomizableItem() { ID = 24117414, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes01",    Label = "Clothing" },
            new RandomizableItem() { ID = 24117415, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes02",    Label = "Clothing Variant 2" },
            new RandomizableItem() { ID = 24117416, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes03",    Label = "Clothing Variant 3" },
            new RandomizableItem() { ID = 24117417, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes04",    Label = "Clothing Variant 4" },
            new RandomizableItem() { ID = 24117418, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes05",    Label = "Clothing Variant 5" },
            new RandomizableItem() { ID = 24117419, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes06",    Label = "Clothing Variant 6" },
            new RandomizableItem() { ID = 24117420, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes07",    Label = "Clothing Variant 1" },
            new RandomizableItem() { ID = 24117421, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes08",    Label = "Clothing Variant 7" },
            new RandomizableItem() { ID = 24117422, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_clothes09",    Label = "Clothing Variant 8" },
            new RandomizableItem() { ID = 24117423, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_jedirobe01",   Label = "Jedi Robe, brown" },
            new RandomizableItem() { ID = 24117424, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_jedirobe02",   Label = "Dark Jedi Robe, black" },
            new RandomizableItem() { ID = 24117425, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_jedirobe03",   Label = "Jedi Robe, red" },
            new RandomizableItem() { ID = 24117426, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_jedirobe04",   Label = "Jedi Robe, blue" },
            new RandomizableItem() { ID = 24117427, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_jedirobe05",   Label = "Dark Jedi Robe, blue" },
            new RandomizableItem() { ID = 24117428, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_jedirobe06",   Label = "Qel-Droma Robes" },
            new RandomizableItem() { ID = 24117429, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_kghtrobe01",   Label = "Jedi Knight Robe, brown" },
            new RandomizableItem() { ID = 24117430, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_kghtrobe02",   Label = "Dark Jedi Knight Robe, black" },
            new RandomizableItem() { ID = 24117431, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_kghtrobe03",   Label = "Jedi Knight Robe, red" },
            new RandomizableItem() { ID = 24117432, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_kghtrobe04",   Label = "Jedi Knight Robe, blue" },
            new RandomizableItem() { ID = 24117433, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_kghtrobe05",   Label = "Dark Jedi Knight Robe, blue" },
            new RandomizableItem() { ID = 24117434, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_mstrrobe01",   Label = "Jedi Master Robe, brown" },
            new RandomizableItem() { ID = 24117435, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_mstrrobe02",   Label = "Dark Jedi Master Robe, black" },
            new RandomizableItem() { ID = 24117436, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_mstrrobe03",   Label = "Jedi Master Robe, red" },
            new RandomizableItem() { ID = 24117437, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_mstrrobe04",   Label = "Jedi Master Robe, blue" },
            new RandomizableItem() { ID = 24117438, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_mstrrobe05",   Label = "Dark Jedi Master Robe, blue" },
            new RandomizableItem() { ID = 24117439, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_mstrrobe06",   Label = "Darth Revan's Robes" },
            new RandomizableItem() { ID = 24117440, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "g_a_mstrrobe07",   Label = "Star Forge Robes" },
            new RandomizableItem() { ID = 24117490, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_adrnaline001", Label = "Adrenal Strength" },
            new RandomizableItem() { ID = 24117491, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_adrnaline002", Label = "Adrenal Alacrity" },
            new RandomizableItem() { ID = 24117492, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_adrnaline003", Label = "Adrenal Stamina" },
            new RandomizableItem() { ID = 24117493, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_adrnaline004", Label = "Hyper-adrenal Strength" },
            new RandomizableItem() { ID = 24117494, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_adrnaline005", Label = "Hyper-adrenal Alacrity" },
            new RandomizableItem() { ID = 24117495, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_adrnaline006", Label = "Hyper-adrenal Stamina" },
            new RandomizableItem() { ID = 24117496, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_asthitem001",  Label = "Aesthetic Item" },
            new RandomizableItem() { ID = 24117497, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt001",      Label = "Cardio-Regulator" },
            new RandomizableItem() { ID = 24117498, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt002",      Label = "Verpine Cardio-Regulator" },
            new RandomizableItem() { ID = 24117499, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt003",      Label = "Adrenaline Amplifier" },
            new RandomizableItem() { ID = 24117500, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt004",      Label = "Advanced Adrenaline Amplifier" },
            new RandomizableItem() { ID = 24117501, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt005",      Label = "Nerve Amplifier Belt" },
            new RandomizableItem() { ID = 24117502, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt006",      Label = "Sound Dampening Stealth Unit" },
            new RandomizableItem() { ID = 24117503, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt007",      Label = "Advanced Stealth Unit" },
            new RandomizableItem() { ID = 24117504, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt008",      Label = "Eriadu Stealth Unit" },
            new RandomizableItem() { ID = 24117505, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt009",      Label = "Calrissian's Utility Belt" },
            new RandomizableItem() { ID = 24117506, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt010",      Label = "Stealth Field Generator" },
            new RandomizableItem() { ID = 24117507, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt011",      Label = "Adrenaline Stimulator" },
            new RandomizableItem() { ID = 24117508, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt012",      Label = "CNS Strength Enhancer" },
            new RandomizableItem() { ID = 24117509, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt013",      Label = "Electrical Capacitance Shield" },
            new RandomizableItem() { ID = 24117510, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "g_i_belt014",      Label = "Thermal Shield Generator" },
            new RandomizableItem() { ID = 24117511, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_i_bithitem001",  Label = "Bith Guitar" },
            new RandomizableItem() { ID = 24117512, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_i_bithitem002",  Label = "Bith Clarinet" },
            new RandomizableItem() { ID = 24117513, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_i_bithitem003",  Label = "Bith Trombone" },
            new RandomizableItem() { ID = 24117514, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_i_bithitem004",  Label = "Bith Accordian" },
            new RandomizableItem() { ID = 24117515, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_cmbtshot001",  Label = "Battle Stimulant" },
            new RandomizableItem() { ID = 24117516, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_cmbtshot002",  Label = "Hyper-battle Stimulant" },
            new RandomizableItem() { ID = 24117517, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_cmbtshot003",  Label = "Echani Battle Stimulant" },
            new RandomizableItem() { ID = 24117518, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_collarlgt001", Label = "Collar Light" },
            new RandomizableItem() { ID = 24117519, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits001",   Label = "Credits, 0005" },
            new RandomizableItem() { ID = 24117520, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits002",   Label = "Credits, 0010" },
            new RandomizableItem() { ID = 24117521, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits003",   Label = "Credits, 0025" },
            new RandomizableItem() { ID = 24117522, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits004",   Label = "Credits, 0050" },
            new RandomizableItem() { ID = 24117523, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits005",   Label = "Credits, 0100" },
            new RandomizableItem() { ID = 24117524, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits006",   Label = "Credits, 0200" },
            new RandomizableItem() { ID = 24117525, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits007",   Label = "Credits, 0300" },
            new RandomizableItem() { ID = 24117526, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits008",   Label = "Credits, 0400" },
            new RandomizableItem() { ID = 24117527, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits009",   Label = "Credits, 0500" },
            new RandomizableItem() { ID = 24117528, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits010",   Label = "Credits, 1000" },
            new RandomizableItem() { ID = 24117529, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits011",   Label = "Credits, 2000" },
            new RandomizableItem() { ID = 24117530, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits012",   Label = "Credits, 3000" },
            new RandomizableItem() { ID = 24117531, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits013",   Label = "Credits, 4000" },
            new RandomizableItem() { ID = 24117532, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits014",   Label = "Credits, 5000" },
            new RandomizableItem() { ID = 24117533, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_credits015",   Label = "Credits, 0001" },
            new RandomizableItem() { ID = 24117534, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide001",    Label = "Assault Droid Mark I Hide" },
            new RandomizableItem() { ID = 24117535, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide002",    Label = "Assault Droid Mark IV Hide" },
            new RandomizableItem() { ID = 24117536, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide003",    Label = "Rakghoul Fiend & Kataarn Hide" },
            new RandomizableItem() { ID = 24117537, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide004",    Label = "Tuk'ata Hide" }, // FRes+32, Regen+1
            new RandomizableItem() { ID = 24117538, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide005",    Label = "Katarn Hide" },
            new RandomizableItem() { ID = 24117539, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide006",    Label = "Tuk'ata Hide" }, // FRes+32, Regen+1
            new RandomizableItem() { ID = 24117540, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide007",    Label = "Canderous' Hide" },
            new RandomizableItem() { ID = 24117541, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide008",    Label = "Droid Hide" },
            new RandomizableItem() { ID = 24117542, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide009",    Label = "HK-47 Hide 1" },
            new RandomizableItem() { ID = 24117543, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide010",    Label = "HK-47 Hide 2" },
            new RandomizableItem() { ID = 24117544, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide011",    Label = "HK-47 Hide 3" },
            new RandomizableItem() { ID = 24117545, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide012",    Label = "HK-47 Hide 4" },
            new RandomizableItem() { ID = 24117546, Category = "Hides",       CategoryEnum = ItemRandoCategory.CreatureHides,   Code = "g_i_crhide013",    Label = "HK-47 Hide 0" },
            new RandomizableItem() { ID = 24117547, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_datapad001",   Label = "Datapad, template" },
            new RandomizableItem() { ID = 24117548, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdcomspk001", Label = "Computer Probe" },
            new RandomizableItem() { ID = 24117549, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdcomspk002", Label = "Universal Computer Interface" },
            new RandomizableItem() { ID = 24117550, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdcomspk003", Label = "Advanced Computer Tool" },
            new RandomizableItem() { ID = 24117551, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdhvplat001", Label = "Droid Heavy Plating Type 1" },
            new RandomizableItem() { ID = 24117552, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdhvplat002", Label = "Droid Heavy Plating Type 2" },
            new RandomizableItem() { ID = 24117553, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdhvplat003", Label = "Droid Heavy Plating Type 3" },
            new RandomizableItem() { ID = 24117554, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdltplat001", Label = "Droid Light Plating Type 1" },
            new RandomizableItem() { ID = 24117555, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdltplat002", Label = "Droid Light Plating Type 2" },
            new RandomizableItem() { ID = 24117556, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdltplat003", Label = "Droid Light Plating Type 3" },
            new RandomizableItem() { ID = 24117557, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdmdplat001", Label = "Droid Medium Plating Type 1" },
            new RandomizableItem() { ID = 24117558, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdmdplat002", Label = "Droid Medium Plating Type 2" },
            new RandomizableItem() { ID = 24117559, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdmdplat003", Label = "Droid Medium Plating Type 3" },
            new RandomizableItem() { ID = 24117560, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdmtnsen001", Label = "Droid Motion Sensors Type 1" },
            new RandomizableItem() { ID = 24117561, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdmtnsen002", Label = "Droid Motion Sensors Type 2" },
            new RandomizableItem() { ID = 24117562, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdmtnsen003", Label = "Droid Motion Sensors Type 3" },
            new RandomizableItem() { ID = 24117563, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdrepeqp001", Label = "Repair Kit" },
            new RandomizableItem() { ID = 24117564, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdrepeqp002", Label = "Advanced Repair Kit" },
            new RandomizableItem() { ID = 24117565, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdrepeqp003", Label = "Construction Kit" },
            new RandomizableItem() { ID = 24117566, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsecspk001", Label = "Security Interface Tool" },
            new RandomizableItem() { ID = 24117567, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsecspk002", Label = "Security Domination Interface" },
            new RandomizableItem() { ID = 24117568, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsecspk003", Label = "Security Decryption Interface" },
            new RandomizableItem() { ID = 24117569, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdshld001",   Label = "Energy Shield Level 1" },
            new RandomizableItem() { ID = 24117570, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdshld002",   Label = "Energy Shield Level 2" },
            new RandomizableItem() { ID = 24117571, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdshld003",   Label = "Energy Shield Level 3" },
            new RandomizableItem() { ID = 24117572, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdshld005",   Label = "Environment Shield Level 1" },
            new RandomizableItem() { ID = 24117573, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdshld006",   Label = "Environment Shield Level 2" },
            new RandomizableItem() { ID = 24117574, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdshld007",   Label = "Environment Shield Level 3" },
            new RandomizableItem() { ID = 24117575, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsncsen001", Label = "Droid Sonic Sensors Type 1" },
            new RandomizableItem() { ID = 24117576, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsncsen002", Label = "Droid Sonic Sensors Type 2" },
            new RandomizableItem() { ID = 24117577, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsncsen003", Label = "Droid Sonic Sensors Type 3" },
            new RandomizableItem() { ID = 24117578, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsrcscp001", Label = "Droid Search Scope Type 1" },
            new RandomizableItem() { ID = 24117579, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsrcscp002", Label = "Droid Search Scope Type 2" },
            new RandomizableItem() { ID = 24117580, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdsrcscp003", Label = "Droid Search Scope Type 3" },
            new RandomizableItem() { ID = 24117581, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdtrgcom001", Label = "Basic Targeting Computer" },
            new RandomizableItem() { ID = 24117582, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdtrgcom002", Label = "Advanced Targeting Computer" },
            new RandomizableItem() { ID = 24117583, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdtrgcom003", Label = "Superior Targeting Computer" },
            new RandomizableItem() { ID = 24117584, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdtrgcom004", Label = "Sensor Probe" },
            new RandomizableItem() { ID = 24117585, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdtrgcom005", Label = "Verpine Demolitions Probe" },
            new RandomizableItem() { ID = 24117586, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdtrgcom006", Label = "Bothan Demolitions Probe" },
            new RandomizableItem() { ID = 24117587, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev001", Label = "Stun Ray" },
            new RandomizableItem() { ID = 24117588, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev002", Label = "Advanced Stun Ray" },
            new RandomizableItem() { ID = 24117589, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev003", Label = "Shield Disruptor" },
            new RandomizableItem() { ID = 24117590, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev004", Label = "Advanced Shield Disruptor" },
            new RandomizableItem() { ID = 24117591, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev005", Label = "Oil Slick" },
            new RandomizableItem() { ID = 24117592, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev006", Label = "Flame Thrower" },
            new RandomizableItem() { ID = 24117593, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev007", Label = "Advanced Flame Thrower" },
            new RandomizableItem() { ID = 24117594, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev008", Label = "Carbonite Projector" },
            new RandomizableItem() { ID = 24117595, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev009", Label = "Carbonite Projector Mark II" },
            new RandomizableItem() { ID = 24117596, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev010", Label = "Gravity Generator" },
            new RandomizableItem() { ID = 24117597, Category = "Droid",       CategoryEnum = ItemRandoCategory.DroidEquipment,  Code = "g_i_drdutldev011", Label = "Adv. Gravity Generator" },
            new RandomizableItem() { ID = 24117598, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds01",  Label = "Energy Shield" },
            new RandomizableItem() { ID = 24117599, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds02",  Label = "Sith Energy Shield" },
            new RandomizableItem() { ID = 24117600, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds03",  Label = "Arkanian Energy Shield" },
            new RandomizableItem() { ID = 24117601, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds04",  Label = "Echani Shield" },
            new RandomizableItem() { ID = 24117602, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds05",  Label = "Mandalorian Melee Shield" },
            new RandomizableItem() { ID = 24117603, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds06",  Label = "Mandalorian Power Shield" },
            new RandomizableItem() { ID = 24117604, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds07",  Label = "Echani Dueling Shield" },
            new RandomizableItem() { ID = 24117605, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds08",  Label = "Yusanis' Dueling Shield" },
            new RandomizableItem() { ID = 24117606, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds09",  Label = "Verpine Prototype Shield" },
            new RandomizableItem() { ID = 24117607, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds10",  Label = "Lower Saves, All 2" },
            new RandomizableItem() { ID = 24117608, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds11",  Label = "Lower Saves, All 4" },
            new RandomizableItem() { ID = 24117609, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds12",  Label = "Lower Saves, All 5" },
            new RandomizableItem() { ID = 24117610, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds13",  Label = "Lower Saves, Fortitude 2" },
            new RandomizableItem() { ID = 24117611, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds14",  Label = "Lower Saves, Fortitude 4" },
            new RandomizableItem() { ID = 24117612, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds15",  Label = "Lower Saves, Fortitude 5" },
            new RandomizableItem() { ID = 24117613, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds16",  Label = "Lower Saves, Reflex 2" },
            new RandomizableItem() { ID = 24117614, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds17",  Label = "Lower Saves, Reflex 4" },
            new RandomizableItem() { ID = 24117615, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds18",  Label = "Lower Saves, Reflex 5" },
            new RandomizableItem() { ID = 24117616, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds19",  Label = "Lower Saves, Will 2" },
            new RandomizableItem() { ID = 24117617, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds20",  Label = "Lower Saves, Will 4" },
            new RandomizableItem() { ID = 24117618, Category = "Armbands",    CategoryEnum = ItemRandoCategory.Armbands,        Code = "g_i_frarmbnds21",  Label = "Lower Saves, Will 5" },
            new RandomizableItem() { ID = 24117619, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet01",   Label = "Strength Gauntlets" },
            new RandomizableItem() { ID = 24117620, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet02",   Label = "Eriadu Strength Amplifier" },
            new RandomizableItem() { ID = 24117621, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet03",   Label = "Sith Power Gauntlets" },
            new RandomizableItem() { ID = 24117622, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet04",   Label = "Stabilizer Gauntlets" },
            new RandomizableItem() { ID = 24117623, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet05",   Label = "Bothan \"Machinist\" Gloves" },
            new RandomizableItem() { ID = 24117624, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet06",   Label = "Verpine Bond Gauntlets" },
            new RandomizableItem() { ID = 24117625, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet07",   Label = "Dominator Gauntlets" },
            new RandomizableItem() { ID = 24117626, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet08",   Label = "Karakan Gauntlets" },
            new RandomizableItem() { ID = 24117627, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "g_i_gauntlet09",   Label = "Infiltrator Gloves" },
            new RandomizableItem() { ID = 24117628, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_gizkapois001", Label = "Gizka Poison" },
            new RandomizableItem() { ID = 24117629, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_glowrod01",    Label = "Glow Rod" },
            new RandomizableItem() { ID = 24117630, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant101",   Label = "Cardio Package" },
            new RandomizableItem() { ID = 24117631, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant102",   Label = "Response Package" },
            new RandomizableItem() { ID = 24117632, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant103",   Label = "Memory Package" },
            new RandomizableItem() { ID = 24117633, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant104",   Label = "Done" },
            new RandomizableItem() { ID = 24117634, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant201",   Label = "Biotech Package" },
            new RandomizableItem() { ID = 24117635, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant202",   Label = "Retinal Combat Implant" },
            new RandomizableItem() { ID = 24117636, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant203",   Label = "Nerve Enhancement Package" },
            new RandomizableItem() { ID = 24117637, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant204",   Label = "I need to make the party selections screen available" },
            new RandomizableItem() { ID = 24117638, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant301",   Label = "Bavakar Cardio Package" },
            new RandomizableItem() { ID = 24117639, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant302",   Label = "Bavakar Reflex Enhancement" },
            new RandomizableItem() { ID = 24117640, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant303",   Label = "Bavakar Memory Chip" },
            new RandomizableItem() { ID = 24117641, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant304",   Label = "Bio-Antidote Package" },
            new RandomizableItem() { ID = 24117642, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant305",   Label = "Cardio Power System" },
            new RandomizableItem() { ID = 24117643, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant306",   Label = "Gordulan Reaction System" },
            new RandomizableItem() { ID = 24117644, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant307",   Label = "Navardan Regenerator" },
            new RandomizableItem() { ID = 24117645, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant308",   Label = "Sith Regenerator" },
            new RandomizableItem() { ID = 24117646, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant309",   Label = "Beemon Package" },
            new RandomizableItem() { ID = 24117647, Category = "Implants",    CategoryEnum = ItemRandoCategory.Implants,        Code = "g_i_implant310",   Label = "Cyber Reaction System" },
            new RandomizableItem() { ID = 24117648, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask01",       Label = "Light-Scan Visor" },
            new RandomizableItem() { ID = 24117649, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask02",       Label = "Motion Detection Goggles" },
            new RandomizableItem() { ID = 24117650, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask03",       Label = "Bothan Perception Visor" },
            new RandomizableItem() { ID = 24117651, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask04",       Label = "Verpine Ocular Enhancer" },
            new RandomizableItem() { ID = 24117652, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask05",       Label = "Bothan Sensory Visor" },
            new RandomizableItem() { ID = 24117653, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask06",       Label = "Vacuum Mask" },
            new RandomizableItem() { ID = 24117654, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask07",       Label = "Sonic Nullifiers" },
            new RandomizableItem() { ID = 24117655, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask08",       Label = "Aural Amplifier" },
            new RandomizableItem() { ID = 24117656, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask09",       Label = "Advanced Aural Amplifier" },
            new RandomizableItem() { ID = 24117657, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask10",       Label = "Neural Band" },
            new RandomizableItem() { ID = 24117658, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask11",       Label = "Verpine Headband" },
            new RandomizableItem() { ID = 24117659, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask12",       Label = "Breath Mask" },
            new RandomizableItem() { ID = 24117660, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask13",       Label = "Teta's Royal Band" },
            new RandomizableItem() { ID = 24117661, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask14",       Label = "Sith Mask" },
            new RandomizableItem() { ID = 24117662, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask15",       Label = "Stabilizer Mask" },
            new RandomizableItem() { ID = 24117663, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask16",       Label = "Interface Band" },
            new RandomizableItem() { ID = 24117664, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask17",       Label = "Demolitions Sensor" },
            new RandomizableItem() { ID = 24117665, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask18",       Label = "Combat Sensor" },
            new RandomizableItem() { ID = 24117666, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask19",       Label = "Stealth Field Enhancer" },
            new RandomizableItem() { ID = 24117667, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask20",       Label = "Stealth Field Reinforcement" },
            new RandomizableItem() { ID = 24117668, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask21",       Label = "Interface Visor" },
            new RandomizableItem() { ID = 24117669, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask22",       Label = "Circlet of Saresh" },
            new RandomizableItem() { ID = 24117670, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask23",       Label = "Pistol Targetting Optics" },
            new RandomizableItem() { ID = 24117671, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "g_i_mask24",       Label = "Heavy Targetting Optics" },
            new RandomizableItem() { ID = 24117672, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_medeqpmnt01",  Label = "Medpac" },
            new RandomizableItem() { ID = 24117673, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_medeqpmnt02",  Label = "Advanced Medpac" },
            new RandomizableItem() { ID = 24117674, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_medeqpmnt03",  Label = "Life Support Pack" },
            new RandomizableItem() { ID = 24117675, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_medeqpmnt04",  Label = "Antidote Kit" },
            new RandomizableItem() { ID = 24117676, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_medeqpmnt05",  Label = "Antibiotic Kit" },
            new RandomizableItem() { ID = 24117677, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_medeqpmnt06",  Label = "Advanced Medpac" },
            new RandomizableItem() { ID = 24117678, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_medeqpmnt07",  Label = "Life Support Pack" },
            new RandomizableItem() { ID = 24117679, Category = "Stims",       CategoryEnum = ItemRandoCategory.Medical,         Code = "g_i_medeqpmnt08",  Label = "Squad Recovery Stim" },
            new RandomizableItem() { ID = 24117680, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_parts01",      Label = "Parts" },
            new RandomizableItem() { ID = 24117681, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_001",  Label = "Pazaak Card +1" },
            new RandomizableItem() { ID = 24117682, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_002",  Label = "Pazaak Card +2" },
            new RandomizableItem() { ID = 24117683, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_003",  Label = "Pazaak Card +3" },
            new RandomizableItem() { ID = 24117684, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_004",  Label = "Pazaak Card +4" },
            new RandomizableItem() { ID = 24117685, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_005",  Label = "Pazaak Card +5" },
            new RandomizableItem() { ID = 24117686, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_006",  Label = "Pazaak Card +6" },
            new RandomizableItem() { ID = 24117687, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_007",  Label = "Pazaak Card -1" },
            new RandomizableItem() { ID = 24117688, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_008",  Label = "Pazaak Card -2" },
            new RandomizableItem() { ID = 24117689, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_009",  Label = "Pazaak Card -3" },
            new RandomizableItem() { ID = 24117690, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_010",  Label = "Pazaak Card -4" },
            new RandomizableItem() { ID = 24117691, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_011",  Label = "Pazaak Card -5" },
            new RandomizableItem() { ID = 24117692, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_012",  Label = "Pazaak Card -6" },
            new RandomizableItem() { ID = 24117693, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_013",  Label = "Pazaak Card +/-1" },
            new RandomizableItem() { ID = 24117694, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_014",  Label = "Pazaak Card +/-2" },
            new RandomizableItem() { ID = 24117695, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_015",  Label = "Pazaak Card +/-3" },
            new RandomizableItem() { ID = 24117696, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_016",  Label = "Pazaak Card +/-4" },
            new RandomizableItem() { ID = 24117697, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_017",  Label = "Pazaak Card +/-5" },
            new RandomizableItem() { ID = 24117698, Category = "Pazaak",      CategoryEnum = ItemRandoCategory.PazaakCards,     Code = "g_i_pazcard_018",  Label = "Pazaak Card +/-6" },
            new RandomizableItem() { ID = 24117699, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_pazdeck",      Label = "Pazaak Deck" },
            new RandomizableItem() { ID = 24117700, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_pazsidebd001", Label = "Pazaak Side Deck" },
            new RandomizableItem() { ID = 24117701, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_pltuseitm01",  Label = "Keycard, template" },
            new RandomizableItem() { ID = 24117702, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_progspike003", Label = "Repair Spike" },
            new RandomizableItem() { ID = 24117703, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_progspike01",  Label = "Computer Spike" },
            new RandomizableItem() { ID = 24117704, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_progspike02",  Label = "SYSTEM LOADING...COMPLETE...ENTER COMMAND" },
            new RandomizableItem() { ID = 24117705, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_recordrod01",  Label = "Recording Rod" },
            new RandomizableItem() { ID = 24117706, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_secspike01",   Label = "Security Spike" },
            new RandomizableItem() { ID = 24117707, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_secspike02",   Label = "Security Spike Tunneler" },
            new RandomizableItem() { ID = 24117708, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_i_torch01",      Label = "Torch" },
            new RandomizableItem() { ID = 24117709, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit001",   Label = "Minor Flash Mine" },
            new RandomizableItem() { ID = 24117710, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit002",   Label = "Average Flash Mine" },
            new RandomizableItem() { ID = 24117711, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit003",   Label = "Deadly Flash Mine" },
            new RandomizableItem() { ID = 24117712, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit004",   Label = "Minor Frag Mine" },
            new RandomizableItem() { ID = 24117713, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit005",   Label = "Average Frag Mine" },
            new RandomizableItem() { ID = 24117714, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit006",   Label = "Deadly Frag Mine" },
            new RandomizableItem() { ID = 24117715, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit007",   Label = "Minor Plasma Mine" },
            new RandomizableItem() { ID = 24117716, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit008",   Label = "Average Plasma Mine" },
            new RandomizableItem() { ID = 24117717, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit009",   Label = "Deadly Plasma Mine" },
            new RandomizableItem() { ID = 24117718, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit01",    Label = "Flash Mine" },      //Turns into Minor once placed.
            new RandomizableItem() { ID = 24117719, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit010",   Label = "Minor Gas Mine" },
            new RandomizableItem() { ID = 24117720, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit011",   Label = "Average Gas Mine" },
            new RandomizableItem() { ID = 24117721, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit012",   Label = "Deadly Gas Mine" },
            new RandomizableItem() { ID = 24117722, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit02",    Label = "Frag Mine" },       //Turns into Minor once placed.
            new RandomizableItem() { ID = 24117723, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit03",    Label = "Laser Mine" },      //Turns into Minor Plasma once placed.
            new RandomizableItem() { ID = 24117724, Category = "Mines",       CategoryEnum = ItemRandoCategory.Mines,           Code = "g_i_trapkit04",    Label = "Gas Mine" },        //Turns into Minor once placed.
            new RandomizableItem() { ID = 24117725, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade001",   Label = "Scope" },
            new RandomizableItem() { ID = 24117726, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade002",   Label = "Improved Energy Cell" },
            new RandomizableItem() { ID = 24117727, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade003",   Label = "Beam Splitter" },
            new RandomizableItem() { ID = 24117728, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade004",   Label = "Hair Trigger" },
            new RandomizableItem() { ID = 24117729, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade005",   Label = "Armor Reinforcement" },
            new RandomizableItem() { ID = 24117730, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade006",   Label = "Mesh Underlay" },
            new RandomizableItem() { ID = 24117731, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade007",   Label = "Vibration Cell" },
            new RandomizableItem() { ID = 24117732, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade008",   Label = "Durasteel Bonding Alloy" },
            new RandomizableItem() { ID = 24117733, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_i_upgrade009",   Label = "Energy Projector" },
            new RandomizableItem() { ID = 24117906, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_adhsvgren001", Label = "Adhesive Grenade" },
            new RandomizableItem() { ID = 24117907, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn001", Label = "Blaster Carbine" },
            new RandomizableItem() { ID = 24117908, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn002", Label = "Sith Assault Gun" },
            new RandomizableItem() { ID = 24117909, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn003", Label = "Cinnagaran Carbine" },
            new RandomizableItem() { ID = 24117910, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn004", Label = "Jurgan Kalta's Carbine" },
            new RandomizableItem() { ID = 24117911, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn005", Label = "Jamoh Hogra's Carbine" }, // br,  ug, e3-10, p1-4, 10%ct, +2a
            new RandomizableItem() { ID = 24117912, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn006", Label = "Jamoh Hogra's Carbine +1" }, // br, !ug, e2-9,  p2,   20%ct, +1a
            new RandomizableItem() { ID = 24117913, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn007", Label = "Jamoh Hogra's Carbine +2" }, // br, !ug, e2-9,  p4,   20%ct, +1a
            new RandomizableItem() { ID = 24117914, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn008", Label = "Jamoh Hogra's Carbine +3" }, // br, !ug, e5-12, p4,   20%ct, +4a
            new RandomizableItem() { ID = 24117915, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrcrbn009", Label = "Jamoh Hogra's Carbine +4" }, // br, !ug, e5-12, p4,   20%ct+1-8, +5a
            new RandomizableItem() { ID = 24117916, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl001", Label = "Blaster Pistol" },
            new RandomizableItem() { ID = 24117917, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl002", Label = "Mandalorian Blaster" },
            new RandomizableItem() { ID = 24117918, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl003", Label = "Arkanian Pistol" },
            new RandomizableItem() { ID = 24117919, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl004", Label = "Zabrak Blaster Pistol" },
            new RandomizableItem() { ID = 24117920, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl005", Label = "Bendak's Blaster" }, // bp,  ug, e2-7, 5%ct, bal, +1a
            new RandomizableItem() { ID = 24117921, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl006", Label = "Bendak's Blaster +1" }, // bp, !ug, e3-8, 5%ct, bal, +5a
            new RandomizableItem() { ID = 24117922, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl007", Label = "Bendak's Blaster +2" }, // bp, !ug, e5-10,5%ct, bal, +5a
            new RandomizableItem() { ID = 24117923, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl008", Label = "Bendak's Blaster +3" }, // bp, !ug, e6-11,5%ct, bal, +5a
            new RandomizableItem() { ID = 24117924, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl009", Label = "Bendak's Blaster +4" }, // bp, !ug, e6-11,5%ct+2-12, bal, +5a
            new RandomizableItem() { ID = 24117925, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl010", Label = "Carth's Blaster" },
            new RandomizableItem() { ID = 24117926, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrpstl020", Label = "Insta-kill Pistol" },
            new RandomizableItem() { ID = 24117927, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl001",  Label = "Blaster Rifle" },
            new RandomizableItem() { ID = 24117928, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl002",  Label = "Sith Sniper Rifle" },
            new RandomizableItem() { ID = 24117929, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl003",  Label = "Mandalorian Assault Rifle" },
            new RandomizableItem() { ID = 24117930, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl004",  Label = "Zabrak Battle Cannon" },
            new RandomizableItem() { ID = 24117931, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl005",  Label = "Jurgan Kalta's Assault Rifle" }, // br,  ug, e1d8, i1d6, 10%ct, +3a
            new RandomizableItem() { ID = 24117932, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl006",  Label = "Jurgan Kalta's Assault Rifle +1" }, // br, !ug, e1d8+4, i1d8, 20%ct, +3a
            new RandomizableItem() { ID = 24117933, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl007",  Label = "Jurgan Kalta's Assault Rifle +2" }, // br, !ug, e2d8, i1d8, 20%ct, +4a
            new RandomizableItem() { ID = 24117934, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl008",  Label = "Jurgan Kalta's Assault Rifle +3" }, // br, !ug, e2d8, i1d8, 20%ct, +4a, 50%DC10stun
            new RandomizableItem() { ID = 24117935, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_blstrrfl009",  Label = "Jurgan Kalta's Assault Rifle +4" }, // br, !ug, e2d8, i1d8, 20%ct+1d8, +5a, 50%DC10stun
            new RandomizableItem() { ID = 24117936, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_bowcstr001",   Label = "Bowcaster" },
            new RandomizableItem() { ID = 24117937, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_bowcstr002",   Label = "Chuundar's Bowcaster" },
            new RandomizableItem() { ID = 24117938, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_bowcstr003",   Label = "Zaalbar's Bowcaster" },
            new RandomizableItem() { ID = 24117939, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crgore001",    Label = "Katarn Slam" },
            new RandomizableItem() { ID = 24117940, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crgore002",    Label = "Dire Katarn Slam" },
            new RandomizableItem() { ID = 24117941, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash001",   Label = "Claw1d4" },
            new RandomizableItem() { ID = 24117942, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash002",   Label = "Claw1d6" },
            new RandomizableItem() { ID = 24117943, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash003",   Label = "Claw1d10" },
            new RandomizableItem() { ID = 24117944, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash004",   Label = "Claw2d12" },
            new RandomizableItem() { ID = 24117945, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash005",   Label = "Claw3d6" },
            new RandomizableItem() { ID = 24117946, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash006",   Label = "Claw10d6" },
            new RandomizableItem() { ID = 24117947, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash007",   Label = "Selkath Lesser Claw" },
            new RandomizableItem() { ID = 24117948, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash008",   Label = "Selkath Greater Claw" },
            new RandomizableItem() { ID = 24117949, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash009",   Label = "Kinrath Claw" },
            new RandomizableItem() { ID = 24117950, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash010",   Label = "Tach Claw" },
            new RandomizableItem() { ID = 24117951, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash011",   Label = "Kinrath Claw" },
            new RandomizableItem() { ID = 24117952, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslash012",   Label = "Kinrath Stalker Claw" },
            new RandomizableItem() { ID = 24117953, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslprc001",   Label = "Veerkal Bite" },
            new RandomizableItem() { ID = 24117954, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslprc002",   Label = "Tuk'ata Bite" },
            new RandomizableItem() { ID = 24117955, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslprc003",   Label = "Gizka Bite" },
            new RandomizableItem() { ID = 24117956, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslprc004",   Label = "Dire Tuk'ata Bite" },
            new RandomizableItem() { ID = 24117957, Category = "CrMelee",     CategoryEnum = ItemRandoCategory.CreatureWeapons, Code = "g_w_crslprc005",   Label = "Shyrack Wyrm Bite" },
            new RandomizableItem() { ID = 24117958, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_cryobgren001", Label = "CryoBan Grenade" },
            new RandomizableItem() { ID = 24117959, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_dblsbr001",    Label = "Double-Bladed Lightsaber, blue" },  // unobtainable
            new RandomizableItem() { ID = 24117960, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_dblsbr002",    Label = "Double-Bladed Lightsaber, red" },
            new RandomizableItem() { ID = 24117961, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_dblsbr003",    Label = "Double-Bladed Lightsaber, green" }, // unobtainable
            new RandomizableItem() { ID = 24117962, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_dblsbr004",    Label = "Double-Bladed Lightsaber, yellow" },
            new RandomizableItem() { ID = 24117963, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_dblsbr005",    Label = "Double-Bladed Lightsaber, purple" },// unobtainable
            new RandomizableItem() { ID = 24117964, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_dblsbr006",    Label = "Bastila's Lightsaber" },
            new RandomizableItem() { ID = 24117965, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_dblswrd001",   Label = "Double-Bladed Sword" },
            new RandomizableItem() { ID = 24117966, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_dblswrd002",   Label = "Echani Ritual Brand" },
            new RandomizableItem() { ID = 24117967, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_dblswrd003",   Label = "Krath Double Sword" },
            new RandomizableItem() { ID = 24117968, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_dblswrd005",   Label = "Ajunta Pall's Blade" },
            new RandomizableItem() { ID = 24117969, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_drkjdisbr001", Label = "Dark Jedi Lightsaber" },    // unobtainable
            new RandomizableItem() { ID = 24117970, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_drkjdisbr002", Label = "Double-Bladed Lightsaber, red, not upgradable" },   // unobtainable
            new RandomizableItem() { ID = 24117971, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_dsrptpstl001", Label = "Disruptor Pistol" },
            new RandomizableItem() { ID = 24117972, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_dsrptpstl002", Label = "Mandalorian Ripper" },
            new RandomizableItem() { ID = 24117973, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_dsrptrfl001",  Label = "Disruptor Rifle" },
            new RandomizableItem() { ID = 24117974, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_dsrptrfl002",  Label = "Zabrak Disruptor Cannon" },
            new RandomizableItem() { ID = 24117975, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_firegren001",  Label = "Plasma Grenade" },
            new RandomizableItem() { ID = 24117976, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_flashgren001", Label = "Plasma Grenade, broken" },  // unobtainable
            new RandomizableItem() { ID = 24117977, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_fraggren01",   Label = "Frag Grenade" },
            new RandomizableItem() { ID = 24117978, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_gaffi001",     Label = "Gaffi Stick" },
            new RandomizableItem() { ID = 24117979, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hldoblstr01",  Label = "Hold Out Blaster" },
            new RandomizableItem() { ID = 24117980, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hldoblstr02",  Label = "Bothan Quick Draw" },
            new RandomizableItem() { ID = 24117981, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hldoblstr03",  Label = "Sith Assassin Pistol" },
            new RandomizableItem() { ID = 24117982, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hldoblstr04",  Label = "Bothan Needler" },
            new RandomizableItem() { ID = 24117983, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvrptbltr002", Label = "Ordo's Repeating Blaster" },
            new RandomizableItem() { ID = 24117984, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvrptbltr01",  Label = "Heavy Repeating Blaster" },
            new RandomizableItem() { ID = 24117985, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvrptbltr02",  Label = "Mandalorian Heavy Repeater" },
            new RandomizableItem() { ID = 24117986, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr01",   Label = "Heavy Blaster" },
            new RandomizableItem() { ID = 24117987, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr02",   Label = "Arkanian Heavy Pistol" },
            new RandomizableItem() { ID = 24117988, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr03",   Label = "Zabrak Tystel Mark III" },
            new RandomizableItem() { ID = 24117989, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr04",   Label = "Mandalorian Heavy Pistol" },
            new RandomizableItem() { ID = 24117990, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr05",   Label = "Cassus Fett's Heavy Pistol" }, // bp, ug, e1d8+3, 25%stn6sDC10, +3a
            new RandomizableItem() { ID = 24117991, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr06",   Label = "Cassus Fett's Heavy Pistol +1" }, // bp,!ug, e1d8+3, i+4, 25%stn6sDC10, +5a
            new RandomizableItem() { ID = 24117992, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr07",   Label = "Cassus Fett's Heavy Pistol +2" }, // bp,!ug, e1d8+5, i1d8, 25%stn6sDC10, +5a
            new RandomizableItem() { ID = 24117993, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr08",   Label = "Cassus Fett's Heavy Pistol +3" }, // bp,!ug, e1d8+5, i1d8, 25%stn9sDC10, +5a
            new RandomizableItem() { ID = 24117994, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_hvyblstr09",   Label = "Cassus Fett's Heavy Pistol +4" }, // bp,!ug, e1d8+5, i1d8, 25%stn9sDC10, +5a, ct+2d6
            new RandomizableItem() { ID = 24117995, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_ionblstr01",   Label = "Ion Blaster" },
            new RandomizableItem() { ID = 24117996, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_ionblstr02",   Label = "Verpine Prototype Ion Blaster" },
            new RandomizableItem() { ID = 24117997, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_iongren01",    Label = "Ion Grenade" },
            new RandomizableItem() { ID = 24117998, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_ionrfl01",     Label = "Ion Rifle" },
            new RandomizableItem() { ID = 24117999, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_ionrfl02",     Label = "Bothan Droid Disruptor" },
            new RandomizableItem() { ID = 24118000, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_ionrfl03",     Label = "Verpine Droid Disruptor" },
            new RandomizableItem() { ID = 24118001, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_lghtsbr01",    Label = "Lightsaber, blue" },
            new RandomizableItem() { ID = 24118002, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_lghtsbr02",    Label = "Lightsaber, red" },
            new RandomizableItem() { ID = 24118003, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_lghtsbr03",    Label = "Lightsaber, green" },
            new RandomizableItem() { ID = 24118004, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_lghtsbr04",    Label = "Lightsaber, yellow" },  // unobtainable, unless sentinel
            new RandomizableItem() { ID = 24118005, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_lghtsbr05",    Label = "Lightsaber, purple" },
            new RandomizableItem() { ID = 24118006, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_lghtsbr06",    Label = "Malak's Lightsaber" },  // not upgradable, longer saber
            new RandomizableItem() { ID = 24118007, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_lngswrd01",    Label = "Long Sword" },
            new RandomizableItem() { ID = 24118008, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_lngswrd02",    Label = "Krath War Blade" },
            new RandomizableItem() { ID = 24118009, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_lngswrd03",    Label = "Naga Sadow's Poison Blade" },
            new RandomizableItem() { ID = 24118010, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_w_null001",      Label = "Blaster Pistol: Null" },// unobtainable
            new RandomizableItem() { ID = 24118011, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_w_null002",      Label = "Heavy Blaster: Null" }, // unobtainable
            new RandomizableItem() { ID = 24118012, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_w_null003",      Label = "Ion Rifle: Null" },     // unobtainable
            new RandomizableItem() { ID = 24118013, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_w_null004",      Label = "Light Repeating Blaster: Null" },   // unobtainable
            new RandomizableItem() { ID = 24118014, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_w_null005",      Label = "Blaster Rifle: Null" }, // unobtainable
            new RandomizableItem() { ID = 24118015, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "g_w_null006",      Label = "Blaster Carbine: Null" },   // unobtainable
            new RandomizableItem() { ID = 24118016, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_poisngren01",  Label = "Poison Grenade" },
            new RandomizableItem() { ID = 24118017, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_qtrstaff01",   Label = "Quarterstaff" },
            new RandomizableItem() { ID = 24118018, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_qtrstaff02",   Label = "Massassi Battle Staff" },   // unobtainable
            new RandomizableItem() { ID = 24118019, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_qtrstaff03",   Label = "Raito's Gaderffii" },
            new RandomizableItem() { ID = 24118020, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_rptnblstr01",  Label = "Light Repeating Blaster" },
            new RandomizableItem() { ID = 24118021, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_rptnblstr02",  Label = "Medium Repeating Blaster" },
            new RandomizableItem() { ID = 24118022, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_rptnblstr03",  Label = "Blaster Cannon" },
            new RandomizableItem() { ID = 24118023, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl01",   Label = "Crystal, Rubat" },
            new RandomizableItem() { ID = 24118024, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl02",   Label = "Crystal, Damind" },
            new RandomizableItem() { ID = 24118025, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl03",   Label = "Crystal, Eralam" },
            new RandomizableItem() { ID = 24118026, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl04",   Label = "Crystal, Sapith" },
            new RandomizableItem() { ID = 24118027, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl05",   Label = "Crystal, Nextor" },
            new RandomizableItem() { ID = 24118028, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl06",   Label = "Crystal, Opila" },
            new RandomizableItem() { ID = 24118029, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl07",   Label = "Crystal, Jenruax" },
            new RandomizableItem() { ID = 24118030, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl08",   Label = "Crystal, Phond" },
            new RandomizableItem() { ID = 24118031, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl09",   Label = "Crystal, Luxum" },
            new RandomizableItem() { ID = 24118032, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl10",   Label = "Crystal, Firkrann" },
            new RandomizableItem() { ID = 24118033, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl11",   Label = "Crystal, Bondar" },
            new RandomizableItem() { ID = 24118034, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl12",   Label = "Crystal, Sigil" },
            new RandomizableItem() { ID = 24118035, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl13",   Label = "Crystal, Upari" },
            new RandomizableItem() { ID = 24118036, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl14",   Label = "Crystal, Blue" },
            new RandomizableItem() { ID = 24118037, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl15",   Label = "Crystal, Yellow" },
            new RandomizableItem() { ID = 24118038, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl16",   Label = "Crystal, Green" },
            new RandomizableItem() { ID = 24118039, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl17",   Label = "Crystal, Violet" },
            new RandomizableItem() { ID = 24118040, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl18",   Label = "Crystal, Red" },
            new RandomizableItem() { ID = 24118041, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "g_w_sbrcrstl19",   Label = "Crystal, Solari" },
            new RandomizableItem() { ID = 24118042, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_shortsbr01",   Label = "Short Lightsaber, blue" },  // unobtainable
            new RandomizableItem() { ID = 24118043, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_shortsbr02",   Label = "Short Lightsaber, red" },
            new RandomizableItem() { ID = 24118044, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_shortsbr03",   Label = "Short Lightsaber, green" }, // unobtainable
            new RandomizableItem() { ID = 24118045, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_shortsbr04",   Label = "Short Lightsaber, yellow" },// unobtainable
            new RandomizableItem() { ID = 24118046, Category = "Lightsabers", CategoryEnum = ItemRandoCategory.Lightsabers,     Code = "g_w_shortsbr05",   Label = "Short Lightsaber, purple" },
            new RandomizableItem() { ID = 24118047, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_shortswrd01",  Label = "Short Sword" },
            new RandomizableItem() { ID = 24118048, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_shortswrd02",  Label = "Massassi Brand" },  // unobtainable
            new RandomizableItem() { ID = 24118049, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_shortswrd03",  Label = "Teta's Blade" },    // unobtainable
            new RandomizableItem() { ID = 24118050, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_sonicgren01",  Label = "Sonic Grenade" },
            new RandomizableItem() { ID = 24118051, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_sonicpstl01",  Label = "Sonic Pistol" },
            new RandomizableItem() { ID = 24118052, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_sonicpstl02",  Label = "Bothan Shrieker" },
            new RandomizableItem() { ID = 24118053, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_sonicrfl01",   Label = "Sonic Rifle" },
            new RandomizableItem() { ID = 24118054, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_sonicrfl02",   Label = "Bothan Discord Gun" },
            new RandomizableItem() { ID = 24118055, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "g_w_sonicrfl03",   Label = "Arkanian Sonic Rifle" },// unobtainable
            new RandomizableItem() { ID = 24118056, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_stunbaton01",  Label = "Stun Baton" },
            new RandomizableItem() { ID = 24118057, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_stunbaton02",  Label = "Bothan Stun Stick" },
            new RandomizableItem() { ID = 24118058, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_stunbaton03",  Label = "Bothan Chuka" },
            new RandomizableItem() { ID = 24118059, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_stunbaton04",  Label = "Rakatan Battle Wand" },//p3, 5%ct, 50%stn06sDC14, a+2, ug
            new RandomizableItem() { ID = 24118060, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_stunbaton05",  Label = "Rakatan Battle Wand +1" },//p4, 5%ct,100%stn12sDC10, a+3,!ug
            new RandomizableItem() { ID = 24118061, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_stunbaton06",  Label = "Rakatan Battle Wand +2" },//p5, 5%ct,100%stn12sDC10, a+4,!ug
            new RandomizableItem() { ID = 24118062, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_stunbaton07",  Label = "Rakatan Battle Wand +3" },//p5, 5%ct,100%stn12sDC14, a+4,!ug, i1d10
            new RandomizableItem() { ID = 24118063, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_stungren01",   Label = "Concussion Grenade" },
            new RandomizableItem() { ID = 24118064, Category = "Grenades",    CategoryEnum = ItemRandoCategory.Grenades,        Code = "g_w_thermldet01",  Label = "Thermal Detonator" },
            new RandomizableItem() { ID = 24118065, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbrdblswd01",  Label = "Vibro Double-Blade" },
            new RandomizableItem() { ID = 24118066, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbrdblswd02",  Label = "Sith War Sword" },  // Unobtainable
            new RandomizableItem() { ID = 24118067, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbrdblswd03",  Label = "Echani Double-Brand" },
            new RandomizableItem() { ID = 24118068, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbrdblswd04",  Label = "Yusanis' Brand" },//p2d8+2, i+5, 5%ct, a+2, ug
            new RandomizableItem() { ID = 24118069, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbrdblswd05",  Label = "Yusanis' Brand +1" },//p2d8+3, i+5, 5%ct, a+3,!ug
            new RandomizableItem() { ID = 24118070, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbrdblswd06",  Label = "Yusanis' Brand +2" },//p2d8+3, i+5, e+3, 5%ct, a+3,!ug
            new RandomizableItem() { ID = 24118071, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbrdblswd07",  Label = "Yusanis' Brand +3" },//p2d8+3, i+5, e+3, f1d4, 10%ct, a+3,!ug
            new RandomizableItem() { ID = 24118072, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort01",  Label = "Vibroblade" },
            new RandomizableItem() { ID = 24118073, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort02",  Label = "Krath Blood Blade" },
            new RandomizableItem() { ID = 24118074, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort03",  Label = "Echani Vibroblade" },
            new RandomizableItem() { ID = 24118075, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort04",  Label = "Sanasiki's Blade" },//p1d10+2, i+3, 10%ct, a+2, ug
            new RandomizableItem() { ID = 24118076, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort05",  Label = "Sanasiki's Blade +1" },//p1d10+4, i+5, 20%ct, a+4,!ug
            new RandomizableItem() { ID = 24118077, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort06",  Label = "Sanasiki's Blade +2" },//p1d10+7, i+5, 20%ct, a+4,!ug
            new RandomizableItem() { ID = 24118078, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort07",  Label = "Sanasiki's Blade +3" },//p1d10+7, i+5, e1d4, 20%ct, a+4,!ug
            new RandomizableItem() { ID = 24118079, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort08",  Label = "Mission's Vibroblade" },
            new RandomizableItem() { ID = 24118080, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroshort09",  Label = "Prototype Vibroblade" },
            new RandomizableItem() { ID = 24118081, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroswrd01",   Label = "Vibrosword" },
            new RandomizableItem() { ID = 24118082, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroswrd02",   Label = "Krath Dire Sword" },
            new RandomizableItem() { ID = 24118083, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroswrd03",   Label = "Sith Tremor Sword" },
            new RandomizableItem() { ID = 24118084, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroswrd04",   Label = "Echani Foil" },
            new RandomizableItem() { ID = 24118085, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroswrd05",   Label = "Bacca's Ceremonial Blade" },//p2d6+2, e+4 , 10%ct, a+2, ug
            new RandomizableItem() { ID = 24118086, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroswrd06",   Label = "Bacca's Ceremonial Blade +1" },//p2d6  , e1d6, 20%ct, a+5,!ug
            new RandomizableItem() { ID = 24118087, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroswrd07",   Label = "Bacca's Ceremonial Blade +2" },//p2d6  , e1d8, 20%ct, a+5,!ug
            new RandomizableItem() { ID = 24118088, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_vbroswrd08",   Label = "Bacca's Ceremonial Blade +3" },//p2d6  , e2d6, 20%ct, a+5,!ug
            new RandomizableItem() { ID = 24118089, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_waraxe001",    Label = "Gamorrean Battleaxe" }, // Unobtainable
            new RandomizableItem() { ID = 24118090, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "g_w_warblade001",  Label = "Wookiee Warblade" },
            new RandomizableItem() { ID = 24118108, Category = "Armor",       CategoryEnum = ItemRandoCategory.Armor,           Code = "geno_armor",       Label = "GenoHaradan Mesh Armor" },
            new RandomizableItem() { ID = 24118109, Category = "Melee",       CategoryEnum = ItemRandoCategory.MeleeWeapons,    Code = "geno_blade",       Label = "GenoHaradan Poison Blade" },
            new RandomizableItem() { ID = 24118110, Category = "Blasters",    CategoryEnum = ItemRandoCategory.Blasters,        Code = "geno_blaster",     Label = "GenoHaradan Blaster" },
            new RandomizableItem() { ID = 24118111, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "geno_datapad",     Label = "GenoHaradan Datapad" },
            new RandomizableItem() { ID = 24118112, Category = "Gloves",      CategoryEnum = ItemRandoCategory.Gloves,          Code = "geno_gloves",      Label = "GenoHaradan Power Gloves" },
            new RandomizableItem() { ID = 24118113, Category = "Belts",       CategoryEnum = ItemRandoCategory.Belts,           Code = "geno_stealth",     Label = "GenoHaradan Stealth Unit" },
            new RandomizableItem() { ID = 24118114, Category = "Masks",       CategoryEnum = ItemRandoCategory.Masks,           Code = "geno_visor",       Label = "GenoHaradan Visor" },
            new RandomizableItem() { ID = 24118156, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "kas25_wookcrysta", Label = "Rough-cut Upari Amulet" },
            new RandomizableItem() { ID = 24118555, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "ptar_rakghoulser", Label = "Rakghoul Serum" },
            new RandomizableItem() { ID = 24118556, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "ptar_sbpasscrd",   Label = "Sith Base Passcard" },
            new RandomizableItem() { ID = 24118557, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "ptar_sitharmor",   Label = "Sith Armor" },
            new RandomizableItem() { ID = 24118620, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "tat17_sandperdis", Label = "Sand People Clothing" },
            new RandomizableItem() { ID = 24118621, Category = "Upgrades",    CategoryEnum = ItemRandoCategory.Upgrades,        Code = "tat18_dragonprl",  Label = "Krayt Dragon Pearl" },
            new RandomizableItem() { ID = 24118625, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "w_blhvy001",       Label = "Heavy Blaster Pistol, broken" },//+1 attack, melee
            new RandomizableItem() { ID = 24118626, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "w_bstrcrbn",       Label = "Clothing, weapons +1" },//+1 attack and damage
            new RandomizableItem() { ID = 24118627, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "w_lghtsbr001",     Label = "Lightsaber, broken" },//+1 attack, melee
            new RandomizableItem() { ID = 24118628, Category = "Various",     CategoryEnum = ItemRandoCategory.Various,         Code = "w_null",           Label = "Null Item" },//not usable
        };

        /// <summary>
        /// Built-in item omission presets for Kotor 1. The key is the preset name, and the value is a string list of items to omit.
        /// </summary>
        public static readonly Dictionary<string, List<string>> KOTOR1_OMIT_PRESETS = new Dictionary<string, List<string>>()
        {
            { "Default (Omit Broken/Plot)", new List<string>()
                {
                    "g_i_collarlgt001",     // Collar Light (broken item)
                    "g_i_drdutldev005",     // Oil Slick (equippable, but unusable and unobtainable)
                    "g_i_glowrod01",        // Glow Rod
                    "g_i_implant104",       // Stamina Boost Implant
                    "g_i_implant204",       // I need to make the party selections screen available
                    "g_i_progspike02",      // Single-Use Programming Spikes
                    "g_i_torch01",          // Torch (broken item)
                    "g_w_flashgren001",     // Plasma Grenade, broken
                    "ptar_rakghoulser",     // Rakghoul Serum (plot)
                    "ptar_sitharmor",       // Sith Armor
                    "tat17_sandperdis",     // Sand People Disguise
                    "w_blhvy001",           // Hvy Blaster Pistol, broken
                    "w_lghtsbr001",         // Lightsaber, broken
                    "w_null",               // Unusable null item
                }
            },
            { "Default + Omit Duplicates", new List<string>()   // Default + More
                {
                    // Default
                    "g_i_collarlgt001", "g_i_drdutldev005", "g_i_glowrod01",
                    "g_i_implant104", "g_i_implant204", "g_i_progspike02",
                    "g_i_torch01", "g_w_flashgren001", "ptar_rakghoulser",
                    "ptar_sitharmor", "tat17_sandperdis", "w_blhvy001",
                    "w_lghtsbr001", "w_null",
                    // Duplicates
                    "g_a_class4007", "g_a_class4008",   // Bandon's Fiber Armor
                    "g_a_class5008", "g_a_class5009",   // Eriadu Prototype Armor
                    "g_a_class6008", "g_a_class6009",   // Davik's War Suit
                    "g_a_class8006", "g_a_class8007",   // Calo Nord's Battle Armor
                    "g_a_class9006", "g_a_class9007",   // Jurgan Kalta's Power Suit
                    "g_a_class9011",    // Cassus Fett's Battle Armor
                    "g_a_clothes02", "g_a_clothes03", "g_a_clothes04", "g_a_clothes05",
                    "g_a_clothes06", "g_a_clothes07", "g_a_clothes08", "g_a_clothes09", // Clothing Variants
                    "g_i_crhide004", "g_i_crhide006",   // Tuk'ata Hide
                    "g_w_blstrcrbn006", "g_w_blstrcrbn007", "g_w_blstrcrbn008", "g_w_blstrcrbn009", // Jamoh Hogra's Carbine
                    "g_w_blstrpstl006", "g_w_blstrpstl007", "g_w_blstrpstl008", "g_w_blstrpstl009", // Bendak's Blaster
                    "g_w_blstrrfl006",  "g_w_blstrrfl007",  "g_w_blstrrfl008",  "g_w_blstrrfl009",  // Jurgan Kalta's Assault Rifle
                    "g_w_hvyblstr06",   "g_w_hvyblstr07",   "g_w_hvyblstr08",   "g_w_hvyblstr09",   // Cassus Fett's Heavy Pistol
                    "g_w_null001", "g_w_null002", "g_w_null003", "g_w_null004", "g_w_null005", "g_w_null006",   // Null weapons
                    "g_w_stunbaton05", "g_w_stunbaton06", "g_w_stunbaton07",    // Rakatan Battle Wand
                    "g_w_vbrdblswd05", "g_w_vbrdblswd06", "g_w_vbrdblswd07",    // Yusanis' Brand
                    "g_w_vbroshort05", "g_w_vbroshort06", "g_w_vbroshort07",    // Sanasiki's Blade
                    "g_w_vbroswrd06",  "g_w_vbroswrd07",  "g_w_vbroswrd08",     // Bacca's Ceremonial Blade
                    "g_i_frarmbnds10", "g_i_frarmbnds11", "g_i_frarmbnds12",    // Lower Saves, All
                    "g_i_frarmbnds13", "g_i_frarmbnds14", "g_i_frarmbnds15",    // Lower Saves, Fortitude
                    "g_i_frarmbnds16", "g_i_frarmbnds17", "g_i_frarmbnds18",    // Lower Saves, Reflex
                    "g_i_frarmbnds19", "g_i_frarmbnds20", "g_i_frarmbnds21",    // Loser Saves, Will
                }
            },
            { "Default + Omit Inaccessible", new List<string>()   // Default + Omit Duplicates + More
                {
                    // Default
                    "g_i_collarlgt001", "g_i_drdutldev005", "g_i_glowrod01",
                    "g_i_implant104", "g_i_implant204", "g_i_progspike02",
                    "g_i_torch01", "g_w_flashgren001", "ptar_rakghoulser",
                    "ptar_sitharmor", "tat17_sandperdis", "w_blhvy001",
                    "w_lghtsbr001", "w_null",
                    // Duplicates
                    "g_a_class4007", "g_a_class4008",   // Bandon's Fiber Armor
                    "g_a_class5008", "g_a_class5009",   // Eriadu Prototype Armor
                    "g_a_class6008", "g_a_class6009",   // Davik's War Suit
                    "g_a_class8006", "g_a_class8007",   // Calo Nord's Battle Armor
                    "g_a_class9006", "g_a_class9007",   // Jurgan Kalta's Power Suit
                    "g_a_class9011",    // Cassus Fett's Battle Armor
                    "g_a_clothes02", "g_a_clothes03", "g_a_clothes04", "g_a_clothes05",
                    "g_a_clothes06", "g_a_clothes07", "g_a_clothes08", "g_a_clothes09", // Clothing Variants
                    "g_i_crhide004", "g_i_crhide006",   // Tuk'ata Hide
                    "g_w_blstrcrbn006", "g_w_blstrcrbn007", "g_w_blstrcrbn008", "g_w_blstrcrbn009", // Jamoh Hogra's Carbine
                    "g_w_blstrpstl006", "g_w_blstrpstl007", "g_w_blstrpstl008", "g_w_blstrpstl009", // Bendak's Blaster
                    "g_w_blstrrfl006",  "g_w_blstrrfl007",  "g_w_blstrrfl008",  "g_w_blstrrfl009",  // Jurgan Kalta's Assault Rifle
                    "g_w_hvyblstr06",   "g_w_hvyblstr07",   "g_w_hvyblstr08",   "g_w_hvyblstr09",   // Cassus Fett's Heavy Pistol
                    "g_w_null001", "g_w_null002", "g_w_null003", "g_w_null004", "g_w_null005", "g_w_null006",   // Null weapons
                    "g_w_stunbaton05", "g_w_stunbaton06", "g_w_stunbaton07",    // Rakatan Battle Wand
                    "g_w_vbrdblswd05", "g_w_vbrdblswd06", "g_w_vbrdblswd07",    // Yusanis' Brand
                    "g_w_vbroshort05", "g_w_vbroshort06", "g_w_vbroshort07",    // Sanasiki's Blade
                    "g_w_vbroswrd06",  "g_w_vbroswrd07",  "g_w_vbroswrd08",     // Bacca's Ceremonial Blade
                    "g_i_frarmbnds10", "g_i_frarmbnds11", "g_i_frarmbnds12",    // Lower Saves, All
                    "g_i_frarmbnds13", "g_i_frarmbnds14", "g_i_frarmbnds15",    // Lower Saves, Fortitude
                    "g_i_frarmbnds16", "g_i_frarmbnds17", "g_i_frarmbnds18",    // Lower Saves, Reflex
                    "g_i_frarmbnds19", "g_i_frarmbnds20", "g_i_frarmbnds21",    // Loser Saves, Will
                    // Inaccessible
                    "g_a_class4004", "g_a_class5004", "g_a_class5006", "g_a_class6005",
                    "g_a_class8003", "g_a_class8004", "g_a_class9003", "g_a_class9004", // Armors
                    "g_a_jedirobe02", "g_a_jedirobe03", "g_a_jedirobe04", "g_a_jedirobe05", // Jedi Robes (r,b,dB,db)
                    "g_a_mstrrobe03", "g_a_mstrrobe04", // Jedi Master Robes (r,b)
                    "g_i_asthitem001",  // Aesthetic Item
                    "g_i_bithitem001", "g_i_bithitem002", "g_i_bithitem003", "g_i_bithitem004", // Bith Instruments
                    "g_i_credits001", "g_i_credits002", "g_i_credits003", "g_i_credits004",
                    "g_i_credits005", "g_i_credits006", "g_i_credits007", "g_i_credits008",
                    "g_i_credits009", "g_i_credits010", "g_i_credits011", "g_i_credits012",
                    "g_i_credits013", "g_i_credits014", "g_i_credits015",   // Credit Denominations
                    "g_i_crhide001", "g_i_crhide002", "g_i_crhide003", "g_i_crhide004",
                    "g_i_crhide005", "g_i_crhide006", "g_i_crhide007", "g_i_crhide008",
                    "g_i_crhide009", "g_i_crhide010", "g_i_crhide011", "g_i_crhide012",
                    "g_i_crhide013",    // Creature Hides
                    "g_i_datapad001",   // Datapad
                    "g_i_drdsncsen001", "g_i_drdsncsen002", "g_i_drdsncsen003", // Sonic Sensors
                    "g_i_drdsrcscp001", "g_i_drdsrcscp002", "g_i_drdsrcscp003", // Search Scopes
                    "g_i_drdtrgcom006", // Bothan Demolitions Probe
                    "g_i_gauntlet05",   // Bothan 'Machinist' Gloves
                    "g_i_implant201", "g_i_implant302", "g_i_implant303",   // Implants
                    "g_i_mask13", "g_i_mask19", // Masks
                    "g_i_medeqpmnt05", "g_i_medeqpmnt06", "g_i_medeqpmnt07", "g_i_medeqpmnt08", // Medpacks
                    "g_i_pltuseitm01", "g_i_progspike003", "g_i_recordrod01",   // Unused Items
                    "g_i_trapkit01", "g_i_trapkit02", "g_i_trapkit03", "g_i_trapkit04", // Mines
                    "g_w_blstrpstl020", // Insta-kill Pistol
                    "g_w_crgore001", "g_w_crgore002", "g_w_crslash001", "g_w_crslash002",
                    "g_w_crslash003", "g_w_crslash004", "g_w_crslash005", "g_w_crslash006",
                    "g_w_crslash007", "g_w_crslash008", "g_w_crslash009", "g_w_crslash010",
                    "g_w_crslash011", "g_w_crslash012", "g_w_crslprc001", "g_w_crslprc002",
                    "g_w_crslprc003", "g_w_crslprc004", "g_w_crslprc005",   // Creature Weapons
                    "g_w_dblsbr001", "g_w_dblsbr003", "g_w_dblsbr005", "g_w_drkjdisbr002",  // Double-Bladed Sabers
                    "g_w_lghtsbr04", "g_w_lghtsbr06", "g_w_drkjdisbr001",   // Lightsabers
                    "g_w_qtrstaff02",   // Massassi Staff
                    "g_w_shortsbr01", "g_w_shortsbr03", "g_w_shortsbr04",   // Short-sabers
                    "g_w_shortswrd02", "g_w_shortswrd03",   // Shortswords
                    "g_w_sonicrfl03",   // Sonic rifle
                    "g_w_vbrdblswd02", "g_w_vbrdblswd03",   // Sith War Sword
                    "g_w_waraxe001",    // Gamorrean Battleaxe
                    "w_bstrcrbn",   // Clothing, weapons +1
                }
            },
        };
    }
}
