using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using KotOR_IO;
using ClosedXML.Excel;
using kotor_Randomizer_2.Models;
using System.Text.RegularExpressions;

namespace kotor_Randomizer_2
{
    public static class TwodaRandom
    {
        #region Fields and Properties
        /// <summary>
        /// Lookup table for how 2DAs are randomized.
        /// Usage: LookupTable[2DA][col_name] = (OriginalData, RandomizedData);
        /// </summary>
        private static Dictionary<string, Dictionary<string, List<Tuple<string, string>>>> LookupTable { get; set; } = new Dictionary<string, Dictionary<string, List<Tuple<string, string>>>>();

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, List<string>> Selected2DAs { get; set; }

        private const string ANIMATIONS_TABLE_NAME = "animations";
        private const string ANIMATIONS_TABLE      = "animations.2da";

        #region Regexes
        // Weapon animation naming conventions (explained for Regexes)
        // A: animation category   1: item category
        //     b = blaster             0 = monster / droid
        //     c = common              1 = stun baton
        //     f = feat                2 = single saber
        //     g = generic             3 = two handed saber
        //     m = monster             4 = dual saber
        //                             5 = single blaster
        //                             6 = dual blasters
        //                             7 = rifle
        //                             8 = natural (unarmed)
        //                             9 = heavy carbine
        //                             
        // B: animation type               2: animation number
        //     a = attack                      b?a3 = sniper shot
        //     d = damaged                     b?a4 = power blast
        //     g = dodge                       c?a6 = burn door with saber
        //     n = deflect                     f?a1 = crit strike
        //     p = parry                       f?a2 = flurry
        //     r = idle (loop)                 f?a3 = power attack
        //     w = wield                       f?a4 = force jump
        //     x = knocked down loop       
        //     y = knocked down            
        //     z = stand from knocked down 

        /// <summary>
        /// Regular expression of creature attack animations.
        /// </summary>
        private static readonly Regex RegexAniCrAttack = new Regex("^[m][0-9][aw][0-9]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Regular expression of creature damaged animations.
        /// </summary>
        private static readonly Regex RegexAniCrDamage = new Regex("^[m][0-9][d][0-9]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Regular expression of creature parry animations.
        /// </summary>
        private static readonly Regex RegexAniCrParry = new Regex("^[m][0-9][pg][0-9]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Regular expression of attack animations.
        /// </summary>
        private static readonly Regex RegexAniAttack = new Regex("^[bcfg][0-9][aw][0-9]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Regular expression of damaged animations.
        /// </summary>
        private static readonly Regex RegexAniDamage = new Regex("^[bcfg][0-9][d][0-9]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Regular expression of parry animations.
        /// </summary>
        private static readonly Regex RegexAniParry = new Regex("^[bcfg][0-9][pg][0-9]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        #endregion

        #region Animation Settings
        /// <summary>
        /// Rando level of attack animations.
        /// </summary>
        public static RandomizationLevel AnimationAttack { get; set; }

        /// <summary>
        /// Rando level of damaged animations.
        /// </summary>
        public static RandomizationLevel AnimationDamage { get; set; }

        /// <summary>
        /// Rando level of fire and forget animations.
        /// </summary>
        public static RandomizationLevel AnimationFire { get; set; }

        /// <summary>
        /// Rando level of looping animations.
        /// </summary>
        public static RandomizationLevel AnimationLoop { get; set; }

        /// <summary>
        /// Rando level of parry animations.
        /// </summary>
        public static RandomizationLevel AnimationParry { get; set; }

        /// <summary>
        /// Rando level of pause and idle animations.
        /// </summary>
        public static RandomizationLevel AnimationPause { get; set; }

        /// <summary>
        /// Rando level of walking and running animations.
        /// </summary>
        public static RandomizationLevel AnimationMove { get; set; }

        /// <summary>
        /// Is any category of animation randomization enabled?
        /// </summary>
        public static bool IsAnimationRandomized
        {
            get
            {
                RandomizationLevel full = AnimationAttack | AnimationDamage | AnimationFire | AnimationLoop | AnimationParry | AnimationPause | AnimationMove;
                return full != RandomizationLevel.None;
            }
        }
        #endregion

        #region Animation Lists
        /// <summary>
        /// List of animations to randomize all together.
        /// </summary>
        private static List<Animation> AniMaxRando { get; } = new List<Animation>();

        /// <summary>
        /// List of creature animations to randomize all together.
        /// </summary>
        private static List<Animation> AniMaxRandoCreatures { get; } = new List<Animation>();

        /// <summary>
        /// Collection of lists of animations to be randomized by type.
        /// </summary>
        private static List<List<Animation>> AnimationTypeLists { get; } = new List<List<Animation>>();

        /// <summary>
        /// Alignment stance animations.
        /// </summary>
        private static readonly List<string> AniAlignment = new List<string>() { "good", "neutral", "evil", "whirlwind" };

        /// <summary>
        /// Unused animations and animations whose usage is unknown. These seem to include some cutscene specific animations.
        /// </summary>
        private static readonly List<int> AniUnused = new List<int>()
        {
            // Appears Unused
            111, 112, 152, 153, 193, 194, 238, 246, 250, 265,   // c2f1, c2f2, c3f1, c3f2, c4f1, c4f2, g6r2, g7r2, g8r2, fear
            // Unknown Usage
            010, 011, 055, 056, 077, 292, 293, 294, 295, 296,   // hturnr, hturnl, drink, cardplay, spasm, turnforward, castout1, castout2, castout3, powerup
            297, 298, 299, 300, 301, 302, 303, 304, 305, 306,   // powered, powerdown, disabled, attack, parry, dodge, default, damage, default, die
            307, 308, 309, 310, 311, 312, 313, 314, 315, 316,   // dead, on, off, open, close, close2open, open2close, on2off, off2on, animloop01
            317, 318, 319, 320, 321, 322, 323, 324, 325, 326,   // animloop02, animloop03, animloop04, animloop05, animloop06, animloop07, animloop08, animloop09, animloop10, pause
            327, 328, 329, 330, 331, 332, 333, 334, 335, 336,   // default, damage, die, dead, opened1, opened2, closed, opening1, opening2, closing1
            337, 344, 346, 347, 348, 349, 350, 355, 356, 357,   // closing2, trans, usecomp, default, activate, deactivate, detect, fblock, off, pause2
            358, 359, 360, 366, 367, 368, 369, 370, 372, 373,   // cpause2, pause3, weld, busted, turnleft, turnright, weld, talkinj, talkinj, equip
            374, 375, 379, 380, 383, 384, 385,                  // die3, dead3, g0a1, b0a1, kd, kdtlkangry, kdtlksad
        };

        /// <summary>
        /// Creature animations.
        /// </summary>
        private static readonly List<int> AniCreatures = new List<int>()
        {
            125, 126, 127, 128, 129, 130, 166, 167, 168, 169,   // m2a1, m2a2, m2g1, m2g2, m2d1, m2d2, m3a1, m3a2, m3g1, m3g2
            170, 171, 207, 208, 209, 210, 211, 212, 253, 254,   // m3d1, m3d2, m4a1, m4a2, m4g1, m4g2, m4d1, m4d2, cwalk, cwalkinj
            255, 256, 257, 258, 259, 260, 261, 262, 263, 264,   // crun, cpause1, cpause2, chturnl, chturnr, cvictory, tlknorm, talk, ctaunt, choke
            266, 267, 268, 269, 271, 272, 273, 274, 275, 276,   // whirlwind, sleep, cspasm, paralyzed, ckdbcklp, ckdbck, cgustandb, cdie, cdead, g0a1
            277, 278, 279, 280, 281, 282, 283, 284, 285, 286,   // g0a2, creadyr, creadyrtw, cdamages, cdodgeg, m0a1, m0a2, m0p1, m0p2, m0d1
            287,                                                // m0d2
        };
        #endregion
        #endregion

        public static void Twoda_rando(KPaths paths, Kotor1Randomizer k1rando = null)
        {
            // Prepare for new randomization.
            Reset();
            AssignSettings(k1rando);

            BIF b = new BIF(Path.Combine(paths.data, "2da.bif"));
            KEY k = new KEY(paths.chitin_backup);
            b.AttachKey(k, "data\\2da.bif");

            var filesInOverride = paths.FilesInOverride.ToList();

            if (IsAnimationRandomized)
            {
                BIF.VariableResourceEntry VRE = b.VariableResourceTable.FirstOrDefault(x => x.ResRef == ANIMATIONS_TABLE_NAME);
                if (VRE != null)
                {
                    TwoDA t;
                    if (filesInOverride.Any(fi => fi.Name == ANIMATIONS_TABLE))
                    {
                        // Modify the existing table.
                        t = new TwoDA(File.ReadAllBytes(filesInOverride.First(fi => fi.Name == ANIMATIONS_TABLE).FullName), VRE.ResRef);
                    }
                    else
                    {
                        // Fetch the table from the 2DA BIF file.
                        t = new TwoDA(VRE.EntryData, VRE.ResRef);
                    }

                    if (!LookupTable.ContainsKey(VRE.ResRef))
                    {
                        // Add 2DA to the table.
                        LookupTable.Add(VRE.ResRef, new Dictionary<string, List<Tuple<string, string>>>());
                    }

                    RandomizeAnimations(t);
                    t.WriteToDirectory(paths.Override); // Write new 2DA data to file.
                }
            }

            foreach (BIF.VariableResourceEntry VRE in b.VariableResourceTable.Where(x => Selected2DAs.Keys.Contains(x.ResRef)))
            {
                // Check to see if this table is already in the override directory.
                TwoDA t;
                if (filesInOverride.Any(fi => fi.Name == $"{VRE.ResRef}.2da"))
                {
                    // Modify the existing table.
                    t = new TwoDA(File.ReadAllBytes(filesInOverride.First(fi => fi.Name == $"{VRE.ResRef}.2da").FullName), VRE.ResRef);
                }
                else
                {
                    // Fetch the table from the 2DA BIF file.
                    t = new TwoDA(VRE.EntryData, VRE.ResRef);
                }

                if (!LookupTable.ContainsKey(VRE.ResRef))
                {
                    // Add 2DA to the table.
                    LookupTable.Add(VRE.ResRef, new Dictionary<string, List<Tuple<string, string>>>());
                }

                foreach (string col in Selected2DAs[VRE.ResRef])
                {
                    if (!LookupTable[VRE.ResRef].ContainsKey(col))
                    {
                        // Add column to the table.
                        LookupTable[VRE.ResRef].Add(col, new List<Tuple<string, string>>());
                    }

                    var old = t.Data[col].ToList();             // Save list of old data.
                    Randomize.FisherYatesShuffle(t.Data[col]);  // Randomize 2DA column data.

                    for (int i = 0; i < old.Count; i++)
                    {
                        // Add old and new data to the table.
                        LookupTable[VRE.ResRef][col].Add(new Tuple<string, string>(old[i], t.Data[col][i]));
                    }
                }

                t.WriteToDirectory(paths.Override); // Write new 2DA data to file.
            }
        }

        /// <summary>
        /// Grabs table related settings from the Kotor1Randomizer object.
        /// </summary>
        /// <param name="k1rando">Randomizer settings object.</param>
        private static void AssignSettings(Kotor1Randomizer k1rando)
        {
            if (k1rando == null)
            {
                Selected2DAs = Globals.Selected2DAs;
            }
            else
            {
                Selected2DAs = new Dictionary<string, List<string>>();
                foreach (var table in k1rando.Table2DAs.Where(rt => rt.IsRandomized))
                {
                    Selected2DAs.Add(table.Name, table.Randomized.ToList());
                }

                AnimationAttack = k1rando.AnimationAttack;
                AnimationDamage = k1rando.AnimationDamage;
                AnimationFire = k1rando.AnimationFire;
                AnimationLoop = k1rando.AnimationLoop;
                AnimationParry = k1rando.AnimationParry;
                AnimationPause = k1rando.AnimationPause;
                AnimationMove = k1rando.AnimationMove;
            }
        }

        /// <summary>
        /// Creates a worksheet containing the spoiler information for this TwoDA randomization.
        /// </summary>
        /// <param name="workbook">Workbook to add the new worksheet to.</param>
        internal static void CreateSpoilerLog(XLWorkbook workbook)
        {
            const string ORIGINAL = "Orig";
            const string RANDOM = "Rand";

            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("TwoDA");
            int i = 1;

            // Store animation settings to write later.
            var animationSettings = new List<Tuple<string, RandomizationLevel>>()
            {
                new Tuple<string, RandomizationLevel>("Attack", AnimationAttack),
                new Tuple<string, RandomizationLevel>("Damage", AnimationDamage),
                new Tuple<string, RandomizationLevel>("Fire",   AnimationFire),
                new Tuple<string, RandomizationLevel>("Loop",   AnimationLoop),
                new Tuple<string, RandomizationLevel>("Parry",  AnimationParry),
                new Tuple<string, RandomizationLevel>("Pause",  AnimationPause),
                new Tuple<string, RandomizationLevel>("Move",   AnimationMove),
            };

            // TwoDA Randomization
            int iDone = i;
            int j = 1;
            int jMax = 1;

            foreach (var twoDA in LookupTable)
            {
                if (twoDA.Key == ANIMATIONS_TABLE_NAME)
                {
                    // Write animation headings and settings.
                    ws.Cell(i, 1).Value = "Animation Type";
                    ws.Cell(i, 2).Value = "Rando Level";
                    ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, 1).Style.Font.Bold = true;
                    ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, 2).Style.Font.Bold = true;
                    i++;

                    foreach (var setting in animationSettings)
                    {
                        ws.Cell(i, 1).Value = setting.Item1;
                        ws.Cell(i, 2).Value = setting.Item2.ToString();
                        ws.Cell(i, 1).Style.Font.Italic = true;
                        i++;
                    }
                    i++;    // Skip a row.
                }

                // TwoDA Table Header
                ws.Cell(i, 1).Value = twoDA.Key;
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 2).Merge();
                i++;

                var iStart = i;

                foreach (var col in twoDA.Value)
                {
                    if (jMax < j) jMax = j + 1;     // Remember the width of the widest table.

                    // Column Headers
                    i = iStart;
                    ws.Cell(i, j).Value = $"{col.Key} {ORIGINAL}";
                    ws.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Font.Italic = true;
                    ws.Cell(i, j + 1).Value = $"{col.Key} {RANDOM}";
                    ws.Cell(i, j + 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j + 1).Style.Font.Italic = true;
                    i++;

                    foreach (var row in col.Value)
                    {
                        // Row Data
                        ws.Cell(i, j).Value = row.Item1;
                        ws.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j + 1).Value = row.Item2;
                        ws.Cell(i, j + 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        i++;
                    }

                    j += 2;     // Move to the next pair of columns.
                    if (iDone < i) iDone = i;   // Remember the length of this table.
                }

                i = iDone + 1;  // Skip a row.
                j = 1;          // Reset to column A.
            }

            // Adjust columns.
            for (int c = 1; c <= jMax; c++)
            {
                ws.Column(c).AdjustToContents();
            }
        }

        /// <summary>
        /// Creates backups for files modified during this randomization.
        /// </summary>
        /// <param name="paths"></param>
        internal static void CreateTwoDABackups(KPaths paths)
        {
            paths.BackUpChitinFile();
            paths.BackUpOverrideDirectory();
        }

        /// <summary>
        /// Sorts animations into type or max randomization based on the RandomizationLevel and if it is a creature animation.
        /// </summary>
        /// <param name="fullList">Full list of animations from which to remove the short list.</param>
        /// <param name="level">Randomization level for this category.</param>
        /// <param name="shortList">List of animations for this category.</param>
        /// <param name="isCreature">Is this a set of creature animations?</param>
        /// <returns>Full list of animations with this category of animations removed.</returns>
        private static List<Animation> HandleCategory(List<Animation> fullList, RandomizationLevel level, List<Animation> shortList, bool isCreature = false)
        {
            switch (level)
            {
                default:
                case RandomizationLevel.None:
                case RandomizationLevel.Subtype:
                    break;  // Do nothing.
                case RandomizationLevel.Type:
                    AnimationTypeLists.Add(shortList);
                    break;
                case RandomizationLevel.Max:
                    if (isCreature)
                    {
                        AniMaxRandoCreatures.AddRange(shortList);
                    }
                    else
                    {
                        AniMaxRando.AddRange(shortList);
                    }
                    break;
            }

            return fullList.Except(shortList).ToList();
        }

        /// <summary>
        /// Parses the "animations" table from the 2DA to create a list of Animation objects.
        /// </summary>
        /// <param name="t">Table containing "animations"</param>
        /// <returns>List of parsed Animation objects</returns>
        private static List<Animation> ParseAnimations(TwoDA t)
        {
            var animations = new List<Animation>();
            for (int i = 0; i < t.RowCount; i++)
            {
                var animation = new Animation();
                foreach (var column in t.Data)
                {
                    switch (column.Key)
                    {
                        case "row_index":
                            animation.Id = int.Parse(column.Value[i]);
                            break;
                        case "name":
                            animation.Name = column.Value[i];
                            break;
                        case "stationary":
                            animation.Stationary = column.Value[i];
                            break;
                        case "pause":
                            animation.Pause = column.Value[i];
                            break;
                        case "walking":
                            animation.Walk = column.Value[i];
                            break;
                        case "running":
                            animation.Run = column.Value[i];
                            break;
                        case "looping":
                            animation.Loop = column.Value[i];
                            break;
                        case "fireforget":
                            animation.Fire = column.Value[i];
                            break;
                        case "overlay":
                            animation.Overlay = column.Value[i];
                            break;
                        case "playoutofplace":
                            animation.PlayOutOfPlace = column.Value[i];
                            break;
                        case "dialog":
                            animation.Dialog = column.Value[i];
                            break;
                        case "damage":
                            animation.Damage = column.Value[i];
                            break;
                        case "parry":
                            animation.Parry = column.Value[i];
                            break;
                        case "dodge":
                            animation.Dodge = column.Value[i];
                            break;
                        case "attack":
                            animation.Attack = column.Value[i];
                            break;
                        case "hideequippeditems":
                            animation.HideEquippedItems = column.Value[i];
                            break;
                        default:
                            break;
                    }
                }
                animations.Add(animation);
            }

            return animations;
        }

        /// <summary>
        /// Perform requested animation randomizations by category.
        /// </summary>
        /// <param name="t">Table containing "animations"</param>
        private static void RandomizeAnimations(TwoDA t)
        {
            // Parse animations into a usable structure.
            List<Animation> animations = ParseAnimations(t);
            var animationLookup = new List<Tuple<string, string>>();

            // Remove animations that shouldn't be randomized.
            animations.RemoveAll(a => AniUnused.Contains(a.Id));    // Remove unused or unknown animations.
            animations = animations.GroupBy(a => a.Name).Select(g => g.First()).ToList();   // Remove duplicates.

            /// Handle animation categories. The order here matters because they are removed from "animations" each time they are handled.
            /// Creatures are handled before humanoids to keep them separate. Max rando lists are separate as well.
            // Handle attacks.
            animations = HandleCategory(animations, AnimationAttack, animations.Where(a => RegexAniCrAttack.IsMatch(a.Name) || (a.Attack == "1" && AniCreatures.Contains(a.Id))).ToList(), true);
            animations = HandleCategory(animations, AnimationAttack, animations.Where(a => RegexAniAttack.IsMatch(a.Name) || a.Attack == "1").ToList());

            // Handle damage.
            animations = HandleCategory(animations, AnimationDamage, animations.Where(a => RegexAniCrDamage.IsMatch(a.Name) || (a.Damage == "1" && AniCreatures.Contains(a.Id))).ToList(), true);
            animations = HandleCategory(animations, AnimationDamage, animations.Where(a => RegexAniDamage.IsMatch(a.Name) || a.Damage == "1").ToList());

            // Handle parry.
            animations = HandleCategory(animations, AnimationParry, animations.Where(a => RegexAniCrParry.IsMatch(a.Name) || (a.Parry == "1" && AniCreatures.Contains(a.Id))).ToList(), true);
            animations = HandleCategory(animations, AnimationParry, animations.Where(a => RegexAniParry.IsMatch(a.Name) || a.Parry == "1").ToList());

            // Handle pause.
            animations = HandleCategory(animations, AnimationPause, animations.Where(a => a.Pause == "1" && AniCreatures.Contains(a.Id)).ToList(), true);
            animations = HandleCategory(animations, AnimationPause, animations.Where(a => a.Pause == "1" || a.Name == "listen").ToList());

            // Handle movement.
            animations = HandleCategory(animations, AnimationMove, animations.Where(a => (a.Run == "1" || a.Walk == "1") && AniCreatures.Contains(a.Id)).ToList(), true);
            animations = HandleCategory(animations, AnimationMove, animations.Where(a => a.Run == "1" || a.Walk == "1").ToList());

            // Handle looping.
            animations = HandleCategory(animations, AnimationLoop, animations.Where(a => a.Loop == "1" && AniCreatures.Contains(a.Id)).ToList(), true);
            animations = HandleCategory(animations, AnimationLoop, animations.Where(a => a.Loop == "1" && AniAlignment.Contains(a.Name)).ToList());
            animations = HandleCategory(animations, AnimationLoop, animations.Where(a => a.Loop == "1").ToList());

            // Handle fire and forget.
            animations = HandleCategory(animations, AnimationFire, animations.Where(a => a.Fire == "1" && AniCreatures.Contains(a.Id)).ToList(), true);
            animations = HandleCategory(animations, AnimationFire, animations.Where(a => a.Fire == "1").ToList());

            // Handle overlay.
            animations = HandleCategory(animations, RandomizationLevel.Type, animations.Where(a => a.Overlay == "1").ToList());

            // Perform max rando.
            var oldMax = AniMaxRando.ToList();
            Randomize.FisherYatesShuffle(AniMaxRando);
            for (int i = 0; i < oldMax.Count; i++)
            {
                animationLookup.Add(new Tuple<string, string>(oldMax[i].Name, AniMaxRando[i].Name));
                t.Data["name"][oldMax[i].Id] = AniMaxRando[i].Name.ToString();
            }

            // Perform type rando.
            foreach (var typeList in AnimationTypeLists)
            {
                var randoType = typeList.ToList();
                Randomize.FisherYatesShuffle(randoType);
                for (int i = 0; i < typeList.Count; i++)
                {
                    animationLookup.Add(new Tuple<string, string>(typeList[i].Name, randoType[i].Name));
                    t.Data["name"][typeList[i].Id] = randoType[i].Name.ToString();
                }
            }

            // Add animation table changes to LookupTable.
            LookupTable[t.Name].Add("name", animationLookup);
        }

        /// <summary>
        /// Prepare lists for a new randomization.
        /// </summary>
        internal static void Reset()
        {
            // Prepare lists for new randomization.
            AniMaxRando.Clear();
            AnimationTypeLists.Clear();
            LookupTable.Clear();
            Selected2DAs = null;
        }

        #region Nested Classes
        /// <summary>
        /// Parsed entry in the "animations" table.
        /// </summary>
        internal struct Animation
        {
            public int Id;
            public string Name;
            public string Stationary;
            public string Pause;
            public string Walk;
            public string Run;
            public string Loop;
            public string Fire;
            public string Overlay;
            public string PlayOutOfPlace;
            public string Dialog;
            public string Damage;
            public string Parry;
            public string Dodge;
            public string Attack;
            public string HideEquippedItems;

            public override string ToString()
            {
                return $"{Id}: {Name}";
            }
        }
        #endregion
    }
}
