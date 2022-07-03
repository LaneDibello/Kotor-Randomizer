using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace kotor_Randomizer_2.Digraph
{
    /// <summary>
    /// Parsed module information for the ModuleDigraph.
    /// </summary>
    public class ModuleVertex
    {
        #region Properties
        public string WarpCode { get; }
        public string CommonName { get; }
        public string Planet { get; }
        public List<ModuleEdge> LeadsTo { get; } = new List<ModuleEdge>();

        public bool IsMalak { get; } = false;
        public bool IsPazaak { get; } = false;
        public bool IsStart { get; } = false;
        public bool IsStarMap { get; } = false;
        public bool IsParty
        {
            get
            {
                return IsBastila || IsCanderous || IsCarth  ||
                       IsHK47    || IsJolee     || IsJuhani ||
                       IsMission || IsT3M4      || IsZaalbar;
            }
        }

        public bool IsBastila { get; } = false;
        public bool IsCanderous { get; } = false;
        public bool IsCarth { get; } = false;
        public bool IsHK47 { get; } = false;
        public bool IsJolee { get; } = false;
        public bool IsJuhani { get; } = false;
        public bool IsMission { get; } = false;
        public bool IsT3M4 { get; } = false;
        public bool IsZaalbar { get; } = false;

        public List<string> Tags { get; } = new List<string>();
        public string LockedTag { get; }

        // List of sets: Unlock="A,B,C; C,D,E; E,F,G".
        //  , is AND within the set
        //  ; is OR between sets
        public List<List<string>> UnlockSets { get; } = new List<List<string>>();
        #endregion

        public ModuleVertex(XElement element)
        {
            // Check for null parameter.
            if (element == null)
                throw new ArgumentException("Parameter \'element\' can't be null.", "element");

            // Get Planet
            Planet = element.Attribute(XmlConsts.ATTR_PLANET)?.Value ?? string.Empty;

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

            // Get LockedTag
            LockedTag = element.Attribute(XmlConsts.ATTR_LOCKED_TAG)?.Value; // Null if it doesn't exist.

            // Parse Tags
            IsMalak   = Tags.Contains(XmlConsts.TAG_MALAK);
            IsPazaak  = Tags.Contains(XmlConsts.TAG_PAZAAK);
            IsStart   = Tags.Contains(XmlConsts.TAG_START);
            IsStarMap = Tags.Contains(XmlConsts.TAG_STAR_MAP);

            IsBastila   = Tags.Contains(XmlConsts.TAG_BASTILA) || LockedTag == XmlConsts.TAG_BASTILA;
            IsCanderous = Tags.Contains(XmlConsts.TAG_CANDEROUS) || LockedTag == XmlConsts.TAG_CANDEROUS;
            IsCarth     = Tags.Contains(XmlConsts.TAG_CARTH) || LockedTag == XmlConsts.TAG_CARTH;
            IsHK47      = Tags.Contains(XmlConsts.TAG_HK47) || LockedTag == XmlConsts.TAG_HK47;
            IsJolee     = Tags.Contains(XmlConsts.TAG_JOLEE) || LockedTag == XmlConsts.TAG_JOLEE;
            IsJuhani    = Tags.Contains(XmlConsts.TAG_JUHANI) || LockedTag == XmlConsts.TAG_JUHANI;
            IsMission   = Tags.Contains(XmlConsts.TAG_MISSION) || LockedTag == XmlConsts.TAG_MISSION;
            IsT3M4      = Tags.Contains(XmlConsts.TAG_T3M4) || LockedTag == XmlConsts.TAG_T3M4;
            IsZaalbar   = Tags.Contains(XmlConsts.TAG_ZAALBAR) || LockedTag == XmlConsts.TAG_ZAALBAR;

            // Get list of Unlocks
            var unlocks = element.Attribute(XmlConsts.ATTR_UNLOCK);
            if (unlocks != null && !string.IsNullOrWhiteSpace(unlocks.Value))
            {
                var unlockSplit = unlocks.Value.Split(XmlConsts.TAG_SEPARATOR_SEMICOLON);
                foreach (var set in unlockSplit)
                {
                    var unlockSet = new List<string>();
                    var setSplit = set.Split(XmlConsts.TAG_SEPARATOR_COMMA);
                    foreach (var unlock in setSplit)
                        unlockSet.Add(unlock);
                    UnlockSets.Add(unlockSet);
                }
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

            if (UnlockSets.Count > 0)
            {
                sb.Append($", Unlock: [");
                foreach (var set in UnlockSets)
                {
                    sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                }
                sb.Remove(sb.Length - 1, 1);    // Remove trailing ';'
                sb.Append("]");
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
}
