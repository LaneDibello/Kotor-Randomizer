using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace kotor_Randomizer_2.Digraph
{
    /// <summary>
    /// Parsed information describing the transition from one module to another.
    /// </summary>
    public class ModuleEdge
    {
        #region Properties
        public string WarpCode { get; }
        public string CommonName { get; }

        public bool IsFake { get; } = false;
        public bool IsLocked { get; } = false;
        public bool IsOnce { get; } = false;

        public bool IsClip { get; } = false;
        public bool IsDlz { get; } = false;
        public bool IsFlu { get; } = false;
        public bool IsGpw { get; } = false;

        public bool IsAccessHangar { get; } = false;
        public bool IsFixBox { get; } = false;
        public bool IsFixHangar { get; } = false;
        public bool IsFixMap { get; } = false;
        public bool IsFixSpice { get; } = false;

        public bool IsUnlockDanRuins { get; } = false;
        public bool IsUnlockKorAcademy { get; } = false;
        public bool IsUnlockManEmbassy { get; } = false;
        public bool IsUnlockManHangar { get; } = false;
        public bool IsUnlockStaBastila { get; } = false;
        public bool IsUnlockTarVulkar { get; } = false;
        public bool IsUnlockTarUndercity { get; } = false;
        public bool IsUnlockUnkSummit { get; } = false;
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

            // Get WarpCode [REQUIRED]
            var code = element.Attribute(XmlConsts.ATTR_CODE);
            WarpCode = code == null || string.IsNullOrEmpty(code.Value)
                ? throw new ArgumentException("No \'WarpCode\' attribute found in the XML element.")
                : code.Value;

            // Get CommonName [REQUIRED]
            var name = element.Attribute(XmlConsts.ATTR_NAME);
            CommonName = name == null || string.IsNullOrEmpty(name.Value)
                ? throw new ArgumentException("No \'CommonName\' attribute found in the XML element.")
                : name.Value;

            // Get list of Tags
            var tags = element.Attribute(XmlConsts.ATTR_TAGS);
            if (tags != null && !string.IsNullOrWhiteSpace(tags.Value))
            {
                foreach (var tag in tags.Value.Split(XmlConsts.TAG_SEPARATOR_COMMA))
                    Tags.Add(tag);
            }

            // Get list of Unlocks
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

            // Check for the boolean Locked attribute.
            var locked = element.Attribute(XmlConsts.TAG_LOCKED);
            if (locked != null && !string.IsNullOrWhiteSpace(locked.Value))
                IsLocked = bool.Parse(locked.Value);

            // Check for the boolean Once attribute.
            var once = element.Attribute(XmlConsts.TAG_ONCE);
            if (once != null && !string.IsNullOrWhiteSpace(once.Value))
                IsOnce = bool.Parse(once.Value);

            IsClip = Tags.Contains(XmlConsts.TAG_CLIP);
            IsDlz = Tags.Contains(XmlConsts.TAG_DLZ);
            IsFlu = Tags.Contains(XmlConsts.TAG_FLU);
            IsGpw = Tags.Contains(XmlConsts.TAG_GPW);

            IsAccessHangar = Tags.Contains(XmlConsts.TAG_HANGAR_ACCESS);
            IsFixHangar = Tags.Contains(XmlConsts.TAG_FIX_ELEV);
            IsFixBox    = Tags.Contains(XmlConsts.TAG_FIX_BOX);
            IsFixMap    = Tags.Contains(XmlConsts.TAG_FIX_MAP);
            IsFixSpice  = Tags.Contains(XmlConsts.TAG_FIX_SPICE);

            IsUnlockDanRuins      = Tags.Contains(XmlConsts.TAG_UNLOCK_DAN_RUINS);
            IsUnlockKorAcademy    = Tags.Contains(XmlConsts.TAG_UNLOCK_KOR_ACADEMY);
            IsUnlockManEmbassy    = Tags.Contains(XmlConsts.TAG_UNLOCK_MAN_EMBASSY);
            IsUnlockManHangar     = Tags.Contains(XmlConsts.TAG_UNLOCK_MAN_HANGAR);
            IsUnlockStaBastila    = Tags.Contains(XmlConsts.TAG_UNLOCK_STA_BASTILA);
            IsUnlockTarVulkar     = Tags.Contains(XmlConsts.TAG_UNLOCK_TAR_VULKAR);
            IsUnlockTarUndercity  = Tags.Contains(XmlConsts.TAG_UNLOCK_TAR_UNDERCITY);
            IsUnlockUnkSummit     = Tags.Contains(XmlConsts.TAG_UNLOCK_UNK_SUMMIT);
            IsUnlockUnkTempleExit = Tags.Contains(XmlConsts.TAG_UNLOCK_UNK_TEMPLE_EXIT);

            //UnlockSets.Any(uls => uls.Contains(XmlConsts.TAG_T3M4));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            _ = sb.Append($"Code: {WarpCode}, Name: {CommonName}");
            _ = Tags.Count > 0
                ? sb.Append($", Tags: [{Tags.Aggregate((i, j) => $"{i},{j}")}]")
                : sb.Append(", Tags: []");

            if (UnlockSets.Count > 0)
            {
                _ = sb.Append($", Unlock: [");
                foreach (var set in UnlockSets)
                {
                    _ = sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                }
                _ = sb.Append("]");
            }

            return sb.ToString();
        }
    }
}
