using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace kotor_Randomizer_2
{
    /// <summary>
    /// Collection of constants used for parsing modules and edges from an XML file.
    /// </summary>
    public struct XmlConsts
    {
        // Games
        public const string GAME_KOTOR_1 = "Kotor1"; // Modules
        public const string GAME_KOTOR_2 = "Kotor2"; // Modules

        // Elements
        public const string ELEM_MODULES  = "Modules"; // Modules
        public const string ELEM_MODULE   = "Module";  // Vertex
        public const string ELEM_LEADS_TO = "LeadsTo"; // Edge

        // Attributes
        public const string ATTR_GAME       = "Game";       // Modules
        public const string ATTR_CODE       = "WarpCode";   // Vertex, Edge
        public const string ATTR_NAME       = "CommonName"; // Vertex, Edge
        public const string ATTR_TAGS       = "Tags";       // Vertex, Edge
        public const string ATTR_LOCKED_TAG = "LockedTag";  // Vertex
        public const string ATTR_UNLOCK     = "Unlock";     // Vertex, Edge

        // General
        public const char   TAG_SEPARATOR_COMMA = ',';
        public const char   TAG_SEPARATOR_SEMICOLON = ';';

        // Blocking Tags ... Edge (Tags)
        public const string TAG_FAKE      = "Fake";
        public const string TAG_LOCKED    = "Locked";
        public const string TAG_ONCE      = "Once";

        // Goal Tags ... Vertex (Tags)
        public const string TAG_MALAK    = "Malak";
        public const string TAG_PAZAAK   = "Pazaak";
        public const string TAG_START    = "Start";
        public const string TAG_STAR_MAP = "StarMap";

        // Party Tags ... Vertex (Tags, LockedTag), Edge (Unlock)
        public const string TAG_BASTILA   = "Bastila";
        public const string TAG_CANDEROUS = "Canderous";
        public const string TAG_CARTH     = "Carth";
        public const string TAG_HK47      = "HK47";
        public const string TAG_JOLEE     = "Jolee";
        public const string TAG_JUHANI    = "Juhani";
        public const string TAG_MISSION   = "Mission";
        public const string TAG_T3M4      = "T3M4";
        public const string TAG_ZAALBAR   = "Zaalbar";

        // Glitch Tags ... Edge (Tags)
        public const string TAG_CLIP = "Clip";
        public const string TAG_DLZ = "DLZ";
        public const string TAG_FLU = "FLU";
        public const string TAG_GPW = "GPW";

        // Fix Tags ... Edge (Tags)
        public const string TAG_FIX_BOX   = "FixBox";
        public const string TAG_FIX_ELEV  = "FixElev";
        public const string TAG_FIX_MAP   = "FixMap";
        public const string TAG_FIX_SPICE = "FixSpice";

        // Unlock Tags ... Edge (Tags)
        public const string TAG_UNLOCK_DAN_RUINS       = "UL_Ruins";
        public const string TAG_UNLOCK_KOR_ACADEMY     = "UL_Academy";
        public const string TAG_UNLOCK_MAN_EMBASSY     = "UL_Embassy";
        public const string TAG_UNLOCK_MAN_HANGAR      = "UL_Hangar";
        public const string TAG_UNLOCK_STA_BASTILA     = "UL_Deck3";
        public const string TAG_UNLOCK_TAR_VULKAR      = "UL_Vulkar";
        public const string TAG_UNLOCK_TAR_UNDERCITY   = "UL_Undercity";
        public const string TAG_UNLOCK_UNK_SUMMIT      = "UL_Summit";
        public const string TAG_UNLOCK_UNK_TEMPLE_EXIT = "UL_TempleExit";
    }

    /// <summary>
    /// Directional graph of the layout of game modules. Implements reachability testing for module randomization.
    /// </summary>
    public class ModuleDigraph
    {
        #region Properties
        /// <summary> Collection of parsed modules that are the unrandomized vertices of the digraph. </summary>
        public List<ModuleVertex> Modules { get; }
        /// <summary> Lookup that indicates which modules are reachable. Keys used are the original warp code. Reachable[Original.WarpCode] = isReachable; </summary>
        public Dictionary<string, bool> Reachable { get; private set; }
        /// <summary> Flag indicating that the Reachable lookup table has been updated in the latest cycle of DFS. </summary>
        private bool ReachableUpdated { get; set; } = false;
        /// <summary> Queue containing edges labeled with the Once tag. These will be checked after every other option has been taken during a cycle. </summary>
        public Queue<ModuleEdge> OnceQueue { get; } = new Queue<ModuleEdge>();
        /// <summary> Lookup table for the randomization. Noteably, it is the reverse of ModuleRando.LookupTable. RandomLookup[Randomized.WarpCode] = Original.WarpCode; </summary>
        public Dictionary<string, string> RandomLookup  { get; set; }

        /// <summary> Reaching the tag(s) Malak is a goal for this randomization. </summary>
        public bool GoalIsMalak   { get; set; } = true;
        /// <summary> Reaching the tag(s) Pazaak is a goal for this randomization. </summary>
        public bool GoalIsPazaak  { get; set; } = false;
        /// <summary> Reaching the tag(s) StarMap is a goal for this randomization. </summary>
        public bool GoalIsStarMap { get; set; } = false;

        /// <summary> Allow usage of the glitch Clipping to bypass locked edges. </summary>
        public bool AllowGlitchClip { get; set; } = false;
        /// <summary> Allow usage of the glitch DLZ to bypass locked edges. </summary>
        public bool AllowGlitchDlz { get; set; } = false;
        /// <summary> Allow usage of the glitch FLU to bypass locked edges. </summary>
        public bool AllowGlitchFlu { get; set; } = false;
        /// <summary> Allow usage of the glitch GPW to bypass locked edges. </summary>
        public bool AllowGlitchGpw { get; set; } = false;

        /// <summary> Locked edges will be ignored until they are unlocked. </summary>
        public bool EnforceEdgeTagLocked { get; set; } = true;
        /// <summary> Allow usage of Once edges. If false, they will be fully blocked as they can be unreliable. </summary>
        public bool IgnoreOnceEdges      { get; set; } = true;

        /// <summary> FixBox is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledFixBox   { get; set; } = false;
        /// <summary> FixElev is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledFixElev  { get; set; } = false;
        /// <summary> FixMap is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledFixMap   { get; set; } = false;
        /// <summary> FixSpice is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledFixSpice { get; set; } = false;

        /// <summary> UnlockDanRuins is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledUnlockDanRuins      { get; set; } = false;
        /// <summary> UnlockKorValley is enabled for this randomization. </summary>
        public bool EnabledUnlockKorAcademy    { get; set; } = false;
        /// <summary> UnlockManEmbassy is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledUnlockManEmbassy    { get; set; } = false;
        /// <summary> UnlockManHangar is enabled for this randomization. </summary>
        public bool EnabledUnlockManHangar     { get; set; } = false;
        /// <summary> UnlockStaBastila is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledUnlockStaBastila    { get; set; } = false;
        /// <summary> UnlockTarUndercity is enabled for this randomization. </summary>
        public bool EnabledUnlockTarUndercity  { get; set; } = false;
        /// <summary> UnlockTarVulkar is enabled for this randomization. </summary>
        public bool EnabledUnlockTarVulkar     { get; set; } = false;
        /// <summary> UnlockUnkSummit is enabled for this randomization. Locked and Once tags will be ignored on the same edge. </summary>
        public bool EnabledUnlockUnkSummit     { get; set; } = false;
        /// <summary> UnlockeUnkTempleExit is enabled for this randomization. </summary>
        public bool EnabledUnlockUnkTempleExit { get; set; } = false;
        #endregion

        /// <summary>
        /// Creates a digraph of the modules in the given XML file and checks reachability.
        /// </summary>
        /// <param name="path">Full path to the XML file to parse.</param>
        /// <param name="lookup">Lookup dictionary from original code to a randomized code.</param>
        public ModuleDigraph(string path)
        {
            // Parse XML document containing game modules.
            XDocument doc = XDocument.Load(path);
            var game = doc.Descendants(XmlConsts.ELEM_MODULES).FirstOrDefault(x => x.Attributes().Where(a => a.Name == XmlConsts.ATTR_GAME && a.Value == XmlConsts.GAME_KOTOR_1).Any());
            Modules = game.Descendants(XmlConsts.ELEM_MODULE).Select(x => new ModuleVertex(x)).ToList();

            // Create a fake lookup (original -> original).
            RandomLookup = Modules.ToDictionary(m => m.WarpCode, m => m.WarpCode);

            // Get currently enabled settings.
            ResetSettings();
        }

        public void ResetSettings()
        {
            // Get currently enabled settings.
            AllowGlitchClip = Properties.Settings.Default.AllowGlitchClip;
            AllowGlitchDlz = Properties.Settings.Default.AllowGlitchDlz;
            AllowGlitchFlu = Properties.Settings.Default.AllowGlitchFlu;
            AllowGlitchGpw = Properties.Settings.Default.AllowGlitchGpw;
            EnabledFixBox = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison);
            EnabledFixElev = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockLevElev);
            EnabledFixMap = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap);
            EnabledFixSpice = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ);
            EnabledUnlockDanRuins = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockDanRuins);
            EnabledUnlockKorAcademy = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockKorValley);
            EnabledUnlockManEmbassy = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManEmbassy);
            EnabledUnlockManHangar = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManHangar);
            EnabledUnlockStaBastila = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockStaBastila);
            EnabledUnlockTarUndercity = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarUndercity);
            EnabledUnlockTarVulkar = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarVulkar);
            EnabledUnlockUnkSummit = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkSummit);
            EnabledUnlockUnkTempleExit = Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkTempleExit);
            EnforceEdgeTagLocked = true;
            IgnoreOnceEdges = Properties.Settings.Default.IgnoreOnceEdges;
            GoalIsMalak = Properties.Settings.Default.GoalIsMalak;
            GoalIsPazaak = Properties.Settings.Default.GoalIsPazaak;
            GoalIsStarMap = Properties.Settings.Default.GoalIsStarMaps;
        }

        /// <summary>
        /// Writes all reachable modules and their (randomized) edges to the console for debugging purposes.
        /// </summary>
        public void WriteReachableToConsole()
        {
            foreach (var vertex in Reachable.Where(kvp => kvp.Value == true))
            {
                if (!Modules.Any(v => v.WarpCode == vertex.Key)) continue;
                var module = Modules.Find(v => v.WarpCode == vertex.Key);

                StringBuilder sb = new StringBuilder();
                sb.Append($"{module.WarpCode}");

                if (module.Tags.Count > 0)
                    sb.Append($" [{module.Tags.Aggregate((i, j) => $"{i},{j}")}]");

                if (!string.IsNullOrWhiteSpace(module.LockedTag))
                    sb.Append($" -[{module.LockedTag}]-");

                if (module.Unlock.Count > 0)
                    sb.Append($" =[{module.Unlock.Aggregate((i, j) => $"{i},{j}")}]=");

                sb.AppendLine();

                foreach (var edge in module.LeadsTo)
                {
                    sb.Append($"-> {edge.WarpCode} ({RandomLookup[edge.WarpCode]})");

                    if (edge.Tags.Count > 0)
                        sb.Append($" [{edge.Tags.Aggregate((i, j) => $"{i},{j}")}]");

                    if (edge.UnlockSets.Count > 0)
                    {
                        sb.Append(" =[");
                        foreach (var set in edge.UnlockSets)
                            sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                        sb.Append("]=");
                    }

                    sb.AppendLine();
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
            if (lookup.Values.Distinct().Count() == lookup.Values.Count)
                RandomLookup = lookup.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
            else
                throw new ArgumentException("Lookup table does not have both a distinct set of keys and values.");
        }

        /// <summary>
        /// Begin the reachability search.
        /// </summary>
        public void CheckReachability()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            // Reset objects needed for reachability testing.
            Reachable = Modules.ToDictionary(m => m.WarpCode, b => false);
            OnceQueue.Clear();

            // Check reachability again to find unlocked edges.
            CheckReachabilityDFS(Reachable);

            // Continue to check to find newly unlocked edges, until none are unlocked.
            do
            {
                ReachableUpdated = false;
                var touched = Reachable.ToDictionary(kvp => kvp.Key, kvp => false);
                CheckReachabilityDFS(touched);
            } while (ReachableUpdated);

            Console.WriteLine($" > Time used to create digraph and check reachability...{sw.Elapsed}");
        }

        /// <summary>
        /// Begins the Depth-First Search reachability checking.
        /// </summary>
        /// <param name="touched">Dictionary indicating if each module has been checked during this cycle.</param>
        /// <param name="origStart">Module where the depth-first search will begin. If null, the Start tag is used.</param>
        private void CheckReachabilityDFS(Dictionary<string, bool> touched, ModuleVertex origStart = null)
        {
            if (origStart == null) origStart = Modules.Find(v => v.IsStart);
            var randStart = Modules.Find(v => v.WarpCode == RandomLookup[origStart.WarpCode]);

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
            for (int i = 0; i < OnceQueue.Count; i++)
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
                if (!Reachable.ContainsKey(t)) Reachable.Add(t, true);
                else Reachable[t] = true;
            }

            // Check to see if the locked tag is reachable too.
            if (!string.IsNullOrWhiteSpace(v.LockedTag))
            {
                var isUnlocked = true;

                // If ALL tags are reachable, this tag is also reachable.
                foreach (var ul in v.Unlock)
                {
                    if (Reachable.ContainsKey(ul) && Reachable[ul]) isUnlocked = true;
                    else
                    {
                        isUnlocked = false;
                        break;
                    }
                }

                if (!Reachable.ContainsKey(v.LockedTag)) Reachable.Add(v.LockedTag, isUnlocked);
                else Reachable[v.LockedTag] = isUnlocked;
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
            bool isOnce = false;
            if (edge.IsOnce)
            {
                if ((EnabledFixBox              && edge.IsFixBox             ) ||
                    (EnabledFixElev             && edge.IsFixElev            ) ||
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
            bool isLocked = false;
            if (edge.IsLocked)
            {
                // Check to see if we can bypass this lock with an allowed glitch or enabled fix.
                if ((AllowGlitchClip            && edge.IsClip               ) ||
                    (AllowGlitchDlz             && edge.IsDlz                ) ||
                    (AllowGlitchFlu             && edge.IsFlu                ) || // How to handle FluReq? One FLU still requires Carth...
                    (AllowGlitchGpw             && edge.IsGpw                ) ||
                    (EnabledFixBox              && edge.IsFixBox             ) ||
                    (EnabledFixElev             && edge.IsFixElev            ) ||
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
                            // If the tag is not in Reachable, then it's a one that we haven't seen and the edge is still locked.
                            if (!Reachable.ContainsKey(target))
                            {
                                Reachable.Add(target, false);
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
                goal &= Reachable[end.WarpCode];
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
            return goal;
        }
        #endregion
    }

    /// <summary>
    /// Parsed module information for the ModuleDigraph.
    /// </summary>
    public class ModuleVertex
    {
        #region Properties
        public string WarpCode          { get; }
        public string CommonName        { get; }
        public List<ModuleEdge> LeadsTo { get; } = new List<ModuleEdge>();

        public bool IsMalak   { get; } = false;
        public bool IsPazaak  { get; } = false;
        public bool IsStart   { get; } = false;
        public bool IsStarMap { get; } = false;

        public bool IsBastila   { get; } = false;
        public bool IsCanderous { get; } = false;
        public bool IsCarth     { get; } = false;
        public bool IsHK47      { get; } = false;
        public bool IsJolee     { get; } = false;
        public bool IsJuhani    { get; } = false;
        public bool IsMission   { get; } = false;
        public bool IsT3M4      { get; } = false;
        public bool IsZaalbar   { get; } = false;

        public List<string> Tags { get; } = new List<string>();
        public string LockedTag { get; }
        public List<string> Unlock { get; } = new List<string>();
        #endregion

        public ModuleVertex(XElement element)
        {
            // Check for null parameter.
            if (element == null)
                throw new ArgumentException("Parameter \'element\' can't be null.", "element");

            // Get WarpCode
            var code = element.Attribute(XmlConsts.ATTR_CODE);
            if (code == null || string.IsNullOrEmpty(code.Value))
                throw new ArgumentException("No \'WarpCode\' attribute found in the XML element.");
            else
                WarpCode = code.Value;

            // Get CommonName
            var name = element.Attribute(XmlConsts.ATTR_NAME);
            if (name == null || string.IsNullOrEmpty(name.Value))
                throw new ArgumentException("No \'CommonName\' attribute found in the XML element.");
            else
                CommonName = name.Value;

            // Get list of Tags
            var tags = element.Attribute(XmlConsts.ATTR_TAGS);
            if (tags != null && !string.IsNullOrWhiteSpace(tags.Value))
            {
                foreach (var tag in tags.Value.Split(XmlConsts.TAG_SEPARATOR_COMMA))
                    Tags.Add(tag);
            }

            // Parse Tags
            IsMalak   = Tags.Contains(XmlConsts.TAG_MALAK);
            IsPazaak  = Tags.Contains(XmlConsts.TAG_PAZAAK);
            IsStart   = Tags.Contains(XmlConsts.TAG_START);
            IsStarMap = Tags.Contains(XmlConsts.TAG_STAR_MAP);

            IsBastila   = Tags.Contains(XmlConsts.TAG_BASTILA);
            IsCanderous = Tags.Contains(XmlConsts.TAG_CANDEROUS);
            IsCarth     = Tags.Contains(XmlConsts.TAG_CARTH);
            IsHK47      = Tags.Contains(XmlConsts.TAG_HK47);
            IsJolee     = Tags.Contains(XmlConsts.TAG_JOLEE);
            IsJuhani    = Tags.Contains(XmlConsts.TAG_JUHANI);
            IsMission   = Tags.Contains(XmlConsts.TAG_MISSION);
            IsT3M4      = Tags.Contains(XmlConsts.TAG_T3M4);
            IsZaalbar   = Tags.Contains(XmlConsts.TAG_ZAALBAR);

            // Get LockedTag
            LockedTag = element.Attribute(XmlConsts.ATTR_LOCKED_TAG)?.Value; // Null if it doesn't exist.

            // Get list of Unlocks
            var unlocks = element.Attribute(XmlConsts.ATTR_UNLOCK);
            if (unlocks != null && !string.IsNullOrWhiteSpace(unlocks.Value))
            {
                foreach (var unlock in unlocks.Value.Split(XmlConsts.TAG_SEPARATOR_COMMA))
                    Unlock.Add(unlock);
            }

            // Get adjacent vertices
            var descendants = element.Descendants(XmlConsts.ELEM_LEADS_TO);
            foreach (var desc in descendants)
            {
                LeadsTo.Add(new ModuleEdge(desc));
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Code: {WarpCode}, Name: {CommonName}");
            if (Tags.Count > 0)
            {
                sb.Append($", Tags: [{Tags.Aggregate((i, j) => $"{i},{j}")}]");
            }
            else
            {
                sb.Append(", Tags: []");
            }

            if (!string.IsNullOrWhiteSpace(LockedTag))
            {
                sb.Append($", LockedTag: {LockedTag}");
            }

            if (Unlock.Count > 0)
            {
                sb.Append($", Unlock: [{Unlock.Aggregate((i, j) => $"{i},{j}")}]");
            }

            if (LeadsTo.Count > 0)
            {
                sb.Append(" -> [");
                sb.Append(LeadsTo[0].WarpCode);
                for (int i = 1; i < LeadsTo.Count; i++)
                {
                    sb.Append(", ");
                    sb.Append(LeadsTo[i].WarpCode);
                }
                sb.Append("]");
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// Parsed information describing the transition from one module to another.
    /// </summary>
    public class ModuleEdge
    {
        #region Properties
        public string WarpCode   { get; }
        public string CommonName { get; }

        public bool IsFake   { get; } = false;
        public bool IsLocked { get; } = false;
        public bool IsOnce   { get; } = false;

        public bool IsClip { get; } = false;
        public bool IsDlz { get; } = false;
        public bool IsFlu { get; } = false;
        public bool IsGpw { get; } = false;

        public bool IsFixBox   { get; } = false;
        public bool IsFixElev  { get; } = false;
        public bool IsFixMap   { get; } = false;
        public bool IsFixSpice { get; } = false;

        public bool IsUnlockDanRuins      { get; } = false;
        public bool IsUnlockKorAcademy    { get; } = false;
        public bool IsUnlockManEmbassy    { get; } = false;
        public bool IsUnlockManHangar     { get; } = false;
        public bool IsUnlockStaBastila    { get; } = false;
        public bool IsUnlockTarVulkar     { get; } = false;
        public bool IsUnlockTarUndercity  { get; } = false;
        public bool IsUnlockUnkSummit     { get; } = false;
        public bool IsUnlockUnkTempleExit { get; } = false;

        public List<string> Tags { get; } = new List<string>();

        // List of sets: Unlock="A,B,C; C,D,E; E,F,G".
        //  , is AND within the set
        //  ; is OR between sets
        public List<List<string>> UnlockSets { get; } = new List<List<string>>();
        #endregion

        public ModuleEdge(XElement element)
        {
            // Check for null parameter.
            if (element == null)
                throw new ArgumentException("Parameter \'element\' can't be null.", "element");

            // Get WarpCode
            var code = element.Attribute(XmlConsts.ATTR_CODE);
            if (code == null || string.IsNullOrEmpty(code.Value))
                throw new ArgumentException("No \'WarpCode\' attribute found in the XML element.");
            else
                WarpCode = code.Value;

            // Get CommonName
            var name = element.Attribute(XmlConsts.ATTR_NAME);
            if (name == null || string.IsNullOrEmpty(name.Value))
                throw new ArgumentException("No \'CommonName\' attribute found in the XML element.");
            else
                CommonName = name.Value;

            // Get list of Tags
            var tags = element.Attribute(XmlConsts.ATTR_TAGS);
            if (tags != null && !string.IsNullOrWhiteSpace(tags.Value))
            {
                foreach (var tag in tags.Value.Split(XmlConsts.TAG_SEPARATOR_COMMA))
                    Tags.Add(tag);
            }

            var unlocks = element.Attribute(XmlConsts.ATTR_UNLOCK);
            if (unlocks != null && !string.IsNullOrWhiteSpace(unlocks.Value))
            {
                foreach (var set in unlocks.Value.Split(XmlConsts.TAG_SEPARATOR_SEMICOLON))
                {
                    var unlockSet = new List<string>();
                    foreach (var unlock in set.Split(XmlConsts.TAG_SEPARATOR_COMMA))
                    {
                        unlockSet.Add(unlock);
                    }
                    UnlockSets.Add(unlockSet);
                }
            }

            // Parse list of Tags
            IsFake = Tags.Contains(XmlConsts.TAG_FAKE);
            IsLocked = Tags.Contains(XmlConsts.TAG_LOCKED);
            IsOnce   = Tags.Contains(XmlConsts.TAG_ONCE);

            IsClip = Tags.Contains(XmlConsts.TAG_CLIP);
            IsDlz = Tags.Contains(XmlConsts.TAG_DLZ);
            IsFlu = Tags.Contains(XmlConsts.TAG_FLU);
            IsGpw = Tags.Contains(XmlConsts.TAG_GPW);

            IsFixBox   = Tags.Contains(XmlConsts.TAG_FIX_BOX);
            IsFixElev  = Tags.Contains(XmlConsts.TAG_FIX_ELEV);
            IsFixMap   = Tags.Contains(XmlConsts.TAG_FIX_MAP);
            IsFixSpice = Tags.Contains(XmlConsts.TAG_FIX_SPICE);

            IsUnlockDanRuins      = Tags.Contains(XmlConsts.TAG_UNLOCK_DAN_RUINS);
            IsUnlockKorAcademy    = Tags.Contains(XmlConsts.TAG_UNLOCK_KOR_ACADEMY);
            IsUnlockManEmbassy    = Tags.Contains(XmlConsts.TAG_UNLOCK_MAN_EMBASSY);
            IsUnlockManHangar     = Tags.Contains(XmlConsts.TAG_UNLOCK_MAN_HANGAR);
            IsUnlockStaBastila    = Tags.Contains(XmlConsts.TAG_UNLOCK_STA_BASTILA);
            IsUnlockTarVulkar     = Tags.Contains(XmlConsts.TAG_UNLOCK_TAR_VULKAR);
            IsUnlockTarUndercity  = Tags.Contains(XmlConsts.TAG_UNLOCK_TAR_UNDERCITY);
            IsUnlockUnkSummit     = Tags.Contains(XmlConsts.TAG_UNLOCK_UNK_SUMMIT);
            IsUnlockUnkTempleExit = Tags.Contains(XmlConsts.TAG_UNLOCK_UNK_TEMPLE_EXIT);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Code: {WarpCode}, Name: {CommonName}");
            if (Tags.Count > 0)
            {
                sb.Append($", Tags: [{Tags.Aggregate((i, j) => $"{i},{j}")}]");
            }
            else
            {
                sb.Append(", Tags: []");
            }

            if (UnlockSets.Count > 0)
            {
                sb.Append($", Unlock: [");
                foreach (var set in UnlockSets)
                {
                    sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                }
                sb.Append("]");
            }

            return sb.ToString();
        }
    }
}
