using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace kotor_Randomizer_2.Digraph
{
    /// <summary>
    /// Directional graph of the layout of game modules. Implements reachability testing for module randomization.
    /// </summary>
    public class ModuleDigraph
    {
        #region Properties

        /// <summary> The game being graphed. </summary>
        public Models.Game Game { get; set; }

        /// <summary> Collection of parsed modules that are the unrandomized vertices of the digraph. </summary>
        public List<ModuleVertex> Modules { get; private set; }

        /// <summary> Lookup that indicates which modules are reachable. Keys used are the original warp code. Reachable[Original.WarpCode] = isReachable; </summary>
        public Dictionary<string, bool> Reachable { get; private set; }

        /// <summary>
        /// Lookup that forms a 2-dimensional table of reachability. The first module is considered the starting point for the DFS.
        /// ReachableTable[Start.WarpCode][Destination.WarpCode] = isReachable;
        /// </summary>
        public Dictionary<string, Dictionary<string, bool>> ReachableTable { get; private set; } = new Dictionary<string, Dictionary<string, bool>>();

        /// <summary> Flag indicating that the Reachable lookup table has been updated in the latest cycle of DFS. </summary>
        private bool ReachableUpdated { get; set; } = false;

        /// <summary> Queue containing edges labeled with the Once tag. These will be checked after every other option has been taken during a cycle. </summary>
        public Queue<ModuleEdge> OnceQueue { get; } = new Queue<ModuleEdge>();

        /// <summary> Lookup table for the randomization. Noteably, it is the reverse of ModuleRando.LookupTable. RandomLookup[Randomized.WarpCode] = Original.WarpCode; </summary>
        public Dictionary<string, string> RandomLookup { get; set; }

        #region Goals
        /// <summary> Reaching the tag Malak is a goal for this randomization. </summary>
        public bool GoalIsMalak { get; set; } = true;

        /// <summary> Reaching the tags StarMap is a goal for this randomization. </summary>
        public bool GoalIsStarMap { get; set; } = false;

        /// <summary> Reaching the tag Kreia is the goal for this randomization. </summary>
        public bool GoalIsKreia { get; set; } = false;

        /// <summary> Reaching the modules tagged with Master is the goal for this randomization. </summary>
        public bool GoalIsMasters { get; set; } = false;

        /// <summary> Reaching the tags Pazaak is a goal for this randomization. </summary>
        public bool GoalIsPazaak { get; set; } = false;

        /// <summary> Reaching the tags of each party member is a goal for this randomization. </summary>
        public bool GoalIsFullParty { get; set; } = false;

        /// <summary> List of all party member tags. </summary>
        public readonly List<string> PARTY_MEMBERS = new List<string>() { "Bastila", "Canderous", "Carth", "HK47", "Jolee", "Juhani", "Mission", "T3M4", "Zaalbar" };

        /// <summary> List of all party member tags, separated by game. </summary>
        public readonly Dictionary<Models.Game, List<string>> GamePartyMembers = new Dictionary<Models.Game, List<string>>()
        {
            { Models.Game.Kotor1, new List<string>() { "Bastila", "Canderous", "Carth", "HK47", "Jolee", "Juhani", "Mission", "T3M4", "Zaalbar" } },
            { Models.Game.Kotor2, new List<string>() { "Atton", "BaoDur", "Disciple", "G0T0", "Handmaiden", "Hanharr", "HK47", "Kreia", "Mandalore", "Mira", "T3M4", "Visas" } },
        };
        #endregion

        #region Glitches
        /// <summary> Allow usage of the glitch Clipping to bypass locked edges. </summary>
        public bool AllowGlitchClip { get; set; } = false;

        /// <summary> Allow usage of the glitch DLZ to bypass locked edges. </summary>
        public bool AllowGlitchDlz { get; set; } = false;

        /// <summary> Allow usage of the glitch FLU to bypass locked edges. </summary>
        public bool AllowGlitchFlu { get; set; } = false;

        /// <summary> Allow usage of the glitch GPW to bypass locked edges. </summary>
        public bool AllowGlitchGpw { get; set; } = false;
        #endregion

        #region Reachability Logic
        /// <summary> Setting used to verify that all active goals can reach all other active goals. </summary>
        public bool EnabledStrongGoals { get; set; } = false;

        /// <summary> Locked edges will be ignored until they are unlocked. </summary>
        public bool EnforceEdgeTagLocked { get; set; } = true;

        /// <summary> Allow usage of Once edges. If false, they will be fully blocked as they can be unreliable. </summary>
        public bool IgnoreOnceEdges { get; set; } = true;
        #endregion

        #region Common Fixes & Unlocks
        /// <summary> FixMap is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledFixMap { get; set; } = false;
        #endregion

        #region Kotor 1 Fixes & Unlocks
        /// <summary> FixBox is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledFixBox { get; set; } = false;

        /// <summary> HangarAccess is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledHangarAccess { get; set; } = false;

        /// <summary> FixElev is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledFixHangarElev { get; set; } = false;

        /// <summary> FixSpice is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledFixSpice { get; set; } = false;

        /// <summary> UnlockDanRuins is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledUnlockDanRuins { get; set; } = false;

        /// <summary> UnlockKorValley is enabled for this randomization. </summary>
        public bool EnabledUnlockKorAcademy { get; set; } = false;

        /// <summary> UnlockManEmbassy is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledUnlockManEmbassy { get; set; } = false;

        /// <summary> UnlockManHangar is enabled for this randomization. </summary>
        public bool EnabledUnlockManHangar { get; set; } = false;

        /// <summary> UnlockStaBastila is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledUnlockStaBastila { get; set; } = false;

        /// <summary> UnlockTarUndercity is enabled for this randomization. </summary>
        public bool EnabledUnlockTarUndercity { get; set; } = false;

        /// <summary> UnlockTarVulkar is enabled for this randomization. </summary>
        public bool EnabledUnlockTarVulkar { get; set; } = false;

        /// <summary> UnlockUnkSummit is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledUnlockUnkSummit { get; set; } = false;

        /// <summary> UnlockeUnkTempleExit is enabled for this randomization. </summary>
        public bool EnabledUnlockUnkTempleExit { get; set; } = false;

        /// <summary> Early T3M4 is enabled for this randomization. </summary>
        public bool EnabledEarlyT3M4 { get; set; } = false;
        #endregion

        #region Kotor 2 Fixes & Unlocks

        #endregion

        #endregion

        /// <summary>
        /// Creates a digraph of the modules in the given XML file and checks reachability.
        /// </summary>
        /// <param name="path">Full path to the XML file to parse.</param>
        /// <param name="lookup">Lookup dictionary from original code to a randomized code.</param>
        public ModuleDigraph(string path, Models.Game game = Models.Game.Kotor1)
        {
            Game = game;

            // Parse XML document containing game modules.
            var doc = XDocument.Load(path);
            var xmlModules = doc.Descendants(XmlConsts.ELEM_MODULES).FirstOrDefault(x => x.Attributes().Any(a => a.Name == XmlConsts.ATTR_GAME && a.Value == game.ToString()));
            Modules = xmlModules.Descendants(XmlConsts.ELEM_MODULE).Select(x => new ModuleVertex(x)).ToList();

            // Create a fake lookup (original -> original).
            RandomLookup = Modules.ToDictionary(m => m.WarpCode, m => m.WarpCode);

            // Get currently enabled settings.
            ResetSettings();
        }

        public void ResetSettings(Models.RandomizerBase rando = null, string moduleFile = null)
        {
            // Refresh the list of modules.
            if (!string.IsNullOrEmpty(moduleFile) && rando != null)
            {
                // Parse XML document containing game modules.
                var doc = XDocument.Load(moduleFile);
                var xmlModules = doc.Descendants(XmlConsts.ELEM_MODULES).FirstOrDefault(x => x.Attributes().Any(a => a.Name == XmlConsts.ATTR_GAME && a.Value == rando.Game.ToString()));
                Modules = xmlModules.Descendants(XmlConsts.ELEM_MODULE).Select(x => new ModuleVertex(x)).ToList();

                // Create a fake lookup (original -> original).
                RandomLookup = Modules.ToDictionary(m => m.WarpCode, m => m.WarpCode);
            }

            if (rando == null)
            {
                // Get currently enabled settings.
                AllowGlitchClip            = Properties.Settings.Default.AllowGlitchClip;
                AllowGlitchDlz             = Properties.Settings.Default.AllowGlitchDlz;
                AllowGlitchFlu             = Properties.Settings.Default.AllowGlitchFlu;
                AllowGlitchGpw             = Properties.Settings.Default.AllowGlitchGpw;
                EnabledFixBox              = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison);
                EnabledHangarAccess        = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockLevElev);
                EnabledFixHangarElev       = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.EnableLevHangarElev);
                EnabledFixMap              = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap);
                EnabledFixSpice            = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ);
                EnabledUnlockDanRuins      = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockDanRuins);
                EnabledUnlockKorAcademy    = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockKorValley);
                EnabledUnlockManEmbassy    = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManEmbassy);
                EnabledUnlockManHangar     = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManHangar);
                EnabledUnlockStaBastila    = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockStaBastila);
                EnabledUnlockTarUndercity  = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarUndercity);
                EnabledUnlockTarVulkar     = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarVulkar);
                EnabledUnlockUnkSummit     = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkSummit);
                EnabledUnlockUnkTempleExit = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkTempleExit);
                EnabledEarlyT3M4           = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.EarlyT3);
                EnforceEdgeTagLocked       = true;
                IgnoreOnceEdges            = Properties.Settings.Default.IgnoreOnceEdges;
                GoalIsMalak                = Properties.Settings.Default.GoalIsMalak;
                GoalIsStarMap              = Properties.Settings.Default.GoalIsStarMaps;
                GoalIsFullParty            = Properties.Settings.Default.GoalIsParty;
                GoalIsPazaak               = Properties.Settings.Default.GoalIsPazaak;
                EnabledStrongGoals         = Properties.Settings.Default.StrongGoals;
            }
            else
            {
                var moduleRando = rando as Models.IRandomizeModules;
                var genSettings = rando as Models.IGeneralSettings;
                var k1rando = rando as Models.Kotor1Randomizer;
                var k2rando = rando as Models.Kotor2Randomizer;

                AllowGlitchClip            = moduleRando.ModuleAllowGlitchClip;
                AllowGlitchDlz             = moduleRando.ModuleAllowGlitchDlz;
                AllowGlitchFlu             = moduleRando.ModuleAllowGlitchFlu;
                AllowGlitchGpw             = moduleRando.ModuleAllowGlitchGpw;
                EnabledFixBox              = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison);
                EnabledHangarAccess        = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockLevElev);
                EnabledFixHangarElev       = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.EnableLevHangarElev);
                EnabledFixMap              = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap);
                EnabledFixSpice            = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ);
                EnabledUnlockDanRuins      = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockDanRuins);
                EnabledUnlockKorAcademy    = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockKorValley);
                EnabledUnlockManEmbassy    = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockManEmbassy);
                EnabledUnlockManHangar     = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockManHangar);
                EnabledUnlockStaBastila    = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockStaBastila);
                EnabledUnlockTarUndercity  = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarUndercity);
                EnabledUnlockTarVulkar     = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarVulkar);
                EnabledUnlockUnkSummit     = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkSummit);
                EnabledUnlockUnkTempleExit = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkTempleExit);
                EnabledEarlyT3M4           = genSettings.GeneralModuleExtrasValue.HasFlag(ModuleExtras.EarlyT3);
                EnforceEdgeTagLocked       = true;
                IgnoreOnceEdges            = moduleRando.ModuleLogicIgnoreOnceEdges;
                GoalIsMalak                = k1rando?.ModuleGoalIsMalak ?? false;
                GoalIsStarMap              = k1rando?.ModuleGoalIsStarMap ?? false;
                GoalIsFullParty            = k1rando?.ModuleGoalIsFullParty ?? false;
                GoalIsPazaak               = k1rando?.ModuleGoalIsPazaak ?? false;
                GoalIsKreia                = k2rando?.ModuleGoalIsKreia ?? false;
                GoalIsMasters              = k2rando?.ModuleGoalIsMasters ?? false;
                GoalIsFullParty            = k2rando?.ModuleGoalIsFullParty ?? false;
                GoalIsPazaak               = k2rando?.ModuleGoalIsPazaak ?? false;
                EnabledStrongGoals         = moduleRando.ModuleLogicStrongGoals;
            }
        }

        /// <summary>
        /// Writes all reachable modules and their (randomized) edges to the console for debugging purposes.
        /// </summary>
        public void WriteReachableToConsole()
        {
            foreach (var vertex in ReachableTable.First().Value.Where(kvp => kvp.Value == true))
            {
                if (!Modules.Any(v => v.WarpCode == vertex.Key)) continue;
                var module = Modules.Find(v => v.WarpCode == vertex.Key);

                var sb = new StringBuilder();
                _ = sb.Append($"{module.WarpCode}");

                if (module.Tags.Count > 0)
                    _ = sb.Append($" [{module.Tags.Aggregate((i, j) => $"{i},{j}")}]");

                if (!string.IsNullOrWhiteSpace(module.LockedTag))
                    _ = sb.Append($" -[{module.LockedTag}]-");

                if (module.UnlockSets.Count > 0)
                {
                    _ = sb.Append($" =[");
                    foreach (var set in module.UnlockSets)
                        _ = sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                    _ = sb.Append($"]=");
                }

                _ = sb.AppendLine();

                foreach (var edge in module.LeadsTo)
                {
                    _ = sb.Append($"-> {edge.WarpCode} ({RandomLookup[edge.WarpCode]})");

                    if (edge.Tags.Count > 0)
                        _ = sb.Append($" [{edge.Tags.Aggregate((i, j) => $"{i},{j}")}]");

                    if (edge.UnlockSets.Count > 0)
                    {
                        _ = sb.Append(" =[");
                        foreach (var set in edge.UnlockSets)
                            _ = sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                        _ = sb.Append("]=");
                    }

                    _ = sb.AppendLine();
                }

                Console.Write(sb.ToString());
            }
        }

        #region Reachability
        /// <summary>
        /// Updates the randomization lookup table by inverting the given table.
        /// </summary>
        /// <param name="lookup">New lookup table.</param>
        public void SetRandomizationLookup(Dictionary<string, string> lookup)
        {
            RandomLookup = lookup.Values.Distinct().Count() == lookup.Values.Count
                ? lookup.ToDictionary(kvp => kvp.Value, kvp => kvp.Key)
                : throw new ArgumentException("Lookup table does not have both a distinct set of keys and values.");
        }

        /// <summary>
        /// Begin the reachability search.
        /// </summary>
        public void CheckReachability()
        {
            var modulesToCheck = new List<ModuleVertex> { Modules.Find(v => v.IsStart) };

            // If strong goals are enabled, we need to create a digraph starting from each goal module.
            if (EnabledStrongGoals)
            {
                modulesToCheck.AddRange(GetActiveGoalModules());        // Get a list of all active goal modules.
                modulesToCheck = modulesToCheck.Distinct().ToList();    // Remove any duplicate modules.
            }

            // Create a new table to clear any stale data.
            ReachableTable = new Dictionary<string, Dictionary<string, bool>>();

            Console.WriteLine($" > Checking reachability for {modulesToCheck.Count} module(s).");
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            // Perform reachability testing for each module.
            foreach (var startModule in modulesToCheck)
            {
                // Reset objects needed for reachability testing.
                Reachable = Modules.ToDictionary(m => m.WarpCode, b => false);
                AddVertexTagUnlocks();
                OnceQueue.Clear();

                // Check reachability again to find unlocked edges.
                CheckReachabilityDFS(Reachable, startModule);

                // Continue to check to find newly unlocked edges, until none are unlocked.
                do
                {
                    ReachableUpdated = false;
                    var touched = Reachable.ToDictionary(kvp => kvp.Key, kvp => false);
                    CheckReachabilityDFS(touched, startModule);
                } while (ReachableUpdated);

                // Store the reachability array for the starting module.
                ReachableTable[startModule.WarpCode] = Reachable.ToDictionary(r => r.Key, r => r.Value);
            }

            Console.WriteLine($" > {modulesToCheck.Count} digraph(s) created and searched in {sw.Elapsed}.");
        }

        /// <summary>
        /// Adds special unlocks for vertex LockedTags.
        /// </summary>
        private void AddVertexTagUnlocks()
        {
            if (EnabledEarlyT3M4) Reachable.Add(XmlConsts.TAG_UNLOCK_EARLY_T3M4, true);
        }

        /// <summary>
        /// Get a list of the modules with tags related to the active goals.
        /// </summary>
        /// <returns>Collection of modules with tags related to the active goals.</returns>
        private List<ModuleVertex> GetActiveGoalModules()
        {
            var modList = new List<ModuleVertex>();

            if (GoalIsMalak    ) modList.AddRange(Modules.Where(v => v.IsMalak  ));
            if (GoalIsStarMap  ) modList.AddRange(Modules.Where(v => v.IsStarMap));

            if (GoalIsKreia    ) modList.AddRange(Modules.Where(v => v.IsTraya  ));
            if (GoalIsMasters  ) modList.AddRange(Modules.Where(v => v.IsMaster ));

            if (GoalIsPazaak   ) modList.AddRange(Modules.Where(v => v.IsPazaak ));
            if (GoalIsFullParty) modList.AddRange(Modules.Where(v => v.IsParty  ));

            return modList;
        }

        /// <summary>
        /// Begins the Depth-First Search reachability checking.
        /// </summary>
        /// <param name="touched">Dictionary indicating if each module has been checked during this cycle.</param>
        /// <param name="startingModule">Module where the depth-first search will begin. If null, the Start tag is used.</param>
        private void CheckReachabilityDFS(Dictionary<string, bool> touched, ModuleVertex startingModule)
        {
            if (startingModule == null) startingModule = Modules.Find(v => v.IsStart);
            var randStart = Modules.Find(v => v.WarpCode == RandomLookup[startingModule.WarpCode]);

            // Search through all modules connected to the start. For now, treat start as not reachable
            // since we may have no opportunity to return here.
            foreach (var edge in randStart.LeadsTo)
            {
                // If this is a once edge, determine how to handle it.
                if (IsOnceEdge(edge))
                {
                    // Ignore once edges if setting is enabled.
                    if (!IgnoreOnceEdges)
                    {
                        // If allowed, enqueue the once edge for checking at the end of this cycle.
                        if (!Reachable[RandomLookup[edge.WarpCode]] && !OnceQueue.Contains(edge))
                        {
                            OnceQueue.Enqueue(edge);
                            ReachableUpdated = true;
                        }
                    }
                    continue;
                }
                // If this is a locked edge, determine if it is still locked.
                if (EnforceEdgeTagLocked && IsLockedEdge(edge)) { continue; }
                DepthFirstSearch(touched, Modules.Find(m => m.WarpCode == RandomLookup[edge.WarpCode]));
            }

            // Check edges marked as Once for reachability.
            for (var i = 0; i < OnceQueue.Count; i++)
            {
                var once = OnceQueue.Dequeue();

                // Remove if already reachable.
                if (Reachable[RandomLookup[once.WarpCode]]) continue;

                // Send to back if still locked.
                if (EnforceEdgeTagLocked && IsLockedEdge(once))
                {
                    OnceQueue.Enqueue(once);
                    //ReachableUpdated = true;
                    continue;
                }

                // Otherwise, visit.
                DepthFirstSearch(touched, Modules.Find(m => m.WarpCode == RandomLookup[once.WarpCode]));
            }
        }

        /// <summary>
        /// Recursive method to perform Depth-First Search reachability checking.
        /// </summary>
        /// <param name="touched">Dictionary indicating if each module has been checked during this cycle.</param>
        /// <param name="v">The current module to check.</param>
        private void DepthFirstSearch(Dictionary<string, bool> touched, ModuleVertex v)
        {
            // This module has been reached, so assume reachable.
            touched[v.WarpCode] = true;
            if (touched[v.WarpCode] != Reachable[v.WarpCode])
            {
                ReachableUpdated = true;
                Reachable[v.WarpCode] = true;
            }

            // Any tags associated with this vertex are also reachable.
            foreach (var t in v.Tags)
            {
                if (!Reachable.ContainsKey(t))
                {
                    ReachableUpdated = true;
                    Reachable.Add(t, true);
                }
                else if (Reachable[t] == false)
                {
                    ReachableUpdated = true;
                    Reachable[t] = true;
                }
            }

            // Check to see if the locked tag is reachable too. If there is one AND
            // that tag is not already reachable, determine if the tag can be unlocked.
            if (!string.IsNullOrWhiteSpace(v.LockedTag) &&
                (!Reachable.ContainsKey(v.LockedTag) || !Reachable[v.LockedTag]))
            {
                var isUnlocked = true;

                // If ALL tags are reachable, this tag is also reachable.
                foreach (var set in v.UnlockSets)
                {
                    // Reset assumption to true, as we're just looking for one false in each set.
                    isUnlocked = true;

                    // ALL vertices within a set need to be reachable.
                    foreach (var target in set)
                    {
                        // If the tag is not in Reachable or it is not reachable, then the tag is still locked.
                        if (!Reachable.ContainsKey(target) || !Reachable[target])
                        {
                            isUnlocked = false;
                            break;
                        }
                    }

                    // If a reachable set has been found, break out of the loop.
                    if (isUnlocked) break;
                }

                // If unlocked, update reachable.
                if (isUnlocked)
                {
                    if (!Reachable.ContainsKey(v.LockedTag))    // If not added, add to reachable.
                    {
                        ReachableUpdated = true;
                        Reachable.Add(v.LockedTag, isUnlocked);
                    }
                    else if (Reachable[v.LockedTag] == false)   // Else if false in reachable, set to true.
                    {
                        ReachableUpdated = true;
                        Reachable[v.LockedTag] = isUnlocked;
                    }
                }
            }

            // Check each edge that hasn't been reached already.
            foreach (var edge in v.LeadsTo)
            {
                // If this is a once edge, determine how to handle it.
                if (IsOnceEdge(edge))
                {
                    // Ignore once edges if setting is enabled.
                    if (!IgnoreOnceEdges)
                    {
                        // If allowed, enqueue the once edge for checking at the end of this cycle.
                        if (!Reachable[RandomLookup[edge.WarpCode]] && !OnceQueue.Contains(edge))
                        {
                            OnceQueue.Enqueue(edge);
                            ReachableUpdated = true;
                        }
                    }
                    continue;
                }
                // If this is a locked edge, determine if it is still locked.
                if (EnforceEdgeTagLocked && IsLockedEdge(edge)) continue;
                if (!touched[RandomLookup[edge.WarpCode]]) DepthFirstSearch(touched, Modules.Find(m => m.WarpCode == RandomLookup[edge.WarpCode]));
            }
        }

        /// <summary>
        /// If the edge contains the Once tag it should be skipped unless an enabled fix is also tagged.
        /// </summary>
        /// <param name="edge">Edge to check.</param>
        /// <returns>True if the edge can be skipped.</returns>
        private bool IsOnceEdge(ModuleEdge edge)
        {
            bool isOnce;
            if (edge.IsOnce)
            {
                if ((EnabledFixBox              && edge.IsFixBox             ) ||
                    (EnabledHangarAccess        && edge.IsAccessHangar       ) ||
                    (EnabledFixHangarElev       && edge.IsFixHangar          ) ||
                    (EnabledFixMap              && edge.IsFixMap             ) ||
                    (EnabledFixSpice            && edge.IsFixSpice           ) ||
                    (EnabledUnlockDanRuins      && edge.IsUnlockDanRuins     ) ||
                    (EnabledUnlockKorAcademy    && edge.IsUnlockKorAcademy   ) ||
                    (EnabledUnlockManEmbassy    && edge.IsUnlockManEmbassy   ) ||
                    (EnabledUnlockManHangar     && edge.IsUnlockManHangar    ) ||
                    (EnabledUnlockStaBastila    && edge.IsUnlockStaBastila   ) ||
                    (EnabledUnlockTarUndercity  && edge.IsUnlockTarUndercity ) ||
                    (EnabledUnlockTarVulkar     && edge.IsUnlockTarVulkar    ) ||
                    (EnabledUnlockUnkSummit     && edge.IsUnlockUnkSummit    ) ||
                    (EnabledUnlockUnkTempleExit && edge.IsUnlockUnkTempleExit))
                {
                    isOnce = false;
                }
                else
                {
                    isOnce = true;
                }
            }
            else
            {
                isOnce = false;
            }
            return isOnce;
        }

        /// <summary>
        /// If the edge contains the Locked tag it should be skipped unless an enabled fix or glitch is also tagged or
        /// if all of the edge's unlocks are reachable.
        /// </summary>
        /// <param name="edge">Edge to check.</param>
        /// <returns>True if the edge can be skipped.</returns>
        private bool IsLockedEdge(ModuleEdge edge)
        {
            bool isLocked;
            if (edge.IsLocked)
            {
                // Check to see if we can bypass this lock with an allowed glitch or enabled fix.
                if ((AllowGlitchClip            && edge.IsClip               ) ||
                    (AllowGlitchDlz             && edge.IsDlz                ) ||
                    (AllowGlitchFlu             && edge.IsFlu                ) || // How to handle FluReq? One FLU still requires Carth...
                    (AllowGlitchGpw             && edge.IsGpw                ) ||
                    (EnabledFixBox              && edge.IsFixBox             ) ||
                    (EnabledHangarAccess        && edge.IsAccessHangar       ) ||
                    (EnabledFixHangarElev       && edge.IsFixHangar          ) ||
                    (EnabledFixMap              && edge.IsFixMap             ) ||
                    (EnabledFixSpice            && edge.IsFixSpice           ) ||
                    (EnabledUnlockDanRuins      && edge.IsUnlockDanRuins     ) ||
                    (EnabledUnlockKorAcademy    && edge.IsUnlockKorAcademy   ) ||
                    (EnabledUnlockManEmbassy    && edge.IsUnlockManEmbassy   ) ||
                    (EnabledUnlockManHangar     && edge.IsUnlockManHangar    ) ||
                    (EnabledUnlockStaBastila    && edge.IsUnlockStaBastila   ) ||
                    (EnabledUnlockTarUndercity  && edge.IsUnlockTarUndercity ) ||
                    (EnabledUnlockTarVulkar     && edge.IsUnlockTarVulkar    ) ||
                    (EnabledUnlockUnkSummit     && edge.IsUnlockUnkSummit    ) ||
                    (EnabledUnlockUnkTempleExit && edge.IsUnlockUnkTempleExit))
                {
                    isLocked = false;
                }
                else
                {
                    // Check to see if the edge can be unlocked by any of the possible sets.
                    var unlocked = true;
                    foreach (var set in edge.UnlockSets)
                    {
                        // Reset assumption to true, as we're just looking for one false in each set.
                        unlocked = true;

                        // ALL vertices within a set need to be reachable.
                        foreach (var target in set)
                        {
                            // If the tag is not in Reachable, then it's one that we haven't seen and the edge is still locked.
                            if (!Reachable.ContainsKey(target))
                            {
                                //Reachable.Add(target, false);
                                unlocked = false;
                                break;
                            }

                            // If not Reachable, then the edge is still locked.
                            if (!Reachable[target])
                            {
                                unlocked = false;
                                break;
                            }
                        }

                        // If a reachable set has been found, break out of the loop.
                        if (unlocked) break;
                    }

                    // Set return value.
                    isLocked = !unlocked;
                }
            }
            else
            {
                // Edge isn't locked.
                isLocked = false;
            }
            return isLocked;
        }
        #endregion

        #region IsReachable
        /// <summary>
        /// Returns true if all vertices in the given collection are reachable. Returns true if empty.
        /// </summary>
        /// <param name="ends">The original vertices to check for reachability.</param>
        private bool IsReachable(IEnumerable<ModuleVertex> ends)
        {
            bool goal = true;
            foreach (var end in ends)
            {
                foreach (var start in ReachableTable)
                {
                    // Verify that this goal end point can be reached from each starting location.
                    goal &= start.Value[end.WarpCode];
                    if (goal == false)
                    {
                        Console.WriteLine($" - Unable to reach {end.WarpCode} starting from {start.Key}.");
                        break;
                    }
                }

                if (goal == false) break;
            }
            return goal;
        }

        /// <summary>
        /// Returns true if all Malak modules are reachable.
        /// </summary>
        public bool IsMalakReachable()
        {
            return IsReachable(Modules.Where(v => v.IsMalak));
        }

        /// <summary>
        /// Returns true if all Pazaak modules are reachable.
        /// </summary>
        public bool IsPazaakReachable()
        {
            return IsReachable(Modules.Where(v => v.IsPazaak));
        }

        /// <summary>
        /// Returns true if all StarMap modules are reachable.
        /// </summary>
        public bool IsStarMapReachable()
        {
            return IsReachable(Modules.Where(v => v.IsStarMap));
        }

        /// <summary>
        /// Returns true if all Party modules are reachable.
        /// </summary>
        private bool IsPartyReachable()
        {
            bool goal = true;

            foreach (var member in PARTY_MEMBERS)
            {
                foreach (var start in ReachableTable)
                {
                    goal &= (start.Value.ContainsKey(member) && start.Value[member]);
                    if (goal == false)
                    {
                        Console.WriteLine($" - Unable to reach {member} starting from {start.Key}.");
                        break;
                    }
                }

                if (goal == false) break;
            }

            return goal;
        }

        /// <summary>
        /// Returns true if all active goals are reachable. Returns true if no goals are active.
        /// </summary>
        public bool IsGoalReachable()
        {
            bool goal = true;
            if (GoalIsMalak)
            {
                goal &= IsMalakReachable();
                if (!goal) Console.WriteLine(" - Goal unreachable: Malak");
            }
            if (goal && GoalIsPazaak)
            {
                goal &= IsPazaakReachable();
                if (!goal) Console.WriteLine(" - Goal unreachable: Pazaak");
            }
            if (goal && GoalIsStarMap)
            {
                goal &= IsStarMapReachable();
                if (!goal) Console.WriteLine(" - Goal unreachable: Star Map");
            }
            if (goal && GoalIsFullParty)
            {
                goal &= IsPartyReachable();
                if (!goal) Console.WriteLine(" - Goal unreachable: Party");
            }
            return goal;
        }
        #endregion
    }
}
