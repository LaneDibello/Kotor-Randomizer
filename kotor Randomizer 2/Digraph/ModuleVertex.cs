using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool IsStart => Tags.Contains(XmlConsts.TAG_START);
        public bool IsMalak => Tags.Contains(XmlConsts.TAG_MALAK);
        public bool IsStarMap => Tags.Contains(XmlConsts.TAG_STAR_MAP);
        public bool IsTraya => Tags.Contains(XmlConsts.TAG_TRAYA);
        public bool IsMaster => Tags.Contains(XmlConsts.TAG_MASTER);
        public bool IsPazaak => Tags.Contains(XmlConsts.TAG_PAZAAK);

        public bool IsParty =>
            IsBastila || IsCanderous  || IsCarth    ||  // KOTOR 1 PARTY
            IsHK47    || IsJolee      || IsJuhani   ||
            IsMission || IsT3M4       || IsZaalbar  ||
            IsAtton   || IsBaoDur     || IsDisciple ||  // KOTOR 2 PARTY
            IsG0T0    || IsHandmaiden || IsHanharr  ||
            IsKreia   || IsMandalore  || IsMira     ||
            IsVisas;

        // KOTOR 1 PARTY MEMBERS
        public bool IsBastila => Tags.Contains(XmlConsts.TAG_BASTILA) || LockedTag == XmlConsts.TAG_BASTILA;
        public bool IsCanderous => Tags.Contains(XmlConsts.TAG_CANDEROUS) || LockedTag == XmlConsts.TAG_CANDEROUS;
        public bool IsCarth => Tags.Contains(XmlConsts.TAG_CARTH) || LockedTag == XmlConsts.TAG_CARTH;
        public bool IsHK47 => Tags.Contains(XmlConsts.TAG_HK47) || LockedTag == XmlConsts.TAG_HK47;
        public bool IsJolee => Tags.Contains(XmlConsts.TAG_JOLEE) || LockedTag == XmlConsts.TAG_JOLEE;
        public bool IsJuhani => Tags.Contains(XmlConsts.TAG_JUHANI) || LockedTag == XmlConsts.TAG_JUHANI;
        public bool IsMission => Tags.Contains(XmlConsts.TAG_MISSION) || LockedTag == XmlConsts.TAG_MISSION;
        public bool IsT3M4 => Tags.Contains(XmlConsts.TAG_T3M4) || LockedTag == XmlConsts.TAG_T3M4;
        public bool IsZaalbar => Tags.Contains(XmlConsts.TAG_ZAALBAR) || LockedTag == XmlConsts.TAG_ZAALBAR;

        // KOTOR 2 PARTY MEMBERS
        public bool IsAtton => Tags.Contains(XmlConsts.TAG_ATTON) || LockedTag == XmlConsts.TAG_ATTON;
        public bool IsBaoDur => Tags.Contains(XmlConsts.TAG_BAODUR) || LockedTag == XmlConsts.TAG_BAODUR;
        public bool IsDisciple => Tags.Contains(XmlConsts.TAG_DISCIPLE) || LockedTag == XmlConsts.TAG_DISCIPLE;
        public bool IsG0T0 => Tags.Contains(XmlConsts.TAG_G0T0) || LockedTag == XmlConsts.TAG_G0T0;
        public bool IsHandmaiden => Tags.Contains(XmlConsts.TAG_HANDMAIDEN) || LockedTag == XmlConsts.TAG_HANDMAIDEN;
        public bool IsHanharr => Tags.Contains(XmlConsts.TAG_HANHARR) || LockedTag == XmlConsts.TAG_HANHARR;
        public bool IsKreia => Tags.Contains(XmlConsts.TAG_KREIA) || LockedTag == XmlConsts.TAG_KREIA;
        public bool IsMandalore => Tags.Contains(XmlConsts.TAG_MANDALORE) || LockedTag == XmlConsts.TAG_MANDALORE;
        public bool IsMira => Tags.Contains(XmlConsts.TAG_MIRA) || LockedTag == XmlConsts.TAG_MIRA;
        public bool IsVisas => Tags.Contains(XmlConsts.TAG_VISAS) || LockedTag == XmlConsts.TAG_VISAS;

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
            WarpCode = code == null || string.IsNullOrEmpty(code.Value)
                ? throw new ArgumentException("No \'WarpCode\' attribute found in the XML element.")
                : code.Value;

            // Get CommonName
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

            // Get LockedTag
            LockedTag = element.Attribute(XmlConsts.ATTR_LOCKED_TAG)?.Value; // Null if it doesn't exist.

            // Parse Tags
            //IsMalak   = Tags.Contains(XmlConsts.TAG_MALAK);
            //IsPazaak  = Tags.Contains(XmlConsts.TAG_PAZAAK);
            //IsStart   = Tags.Contains(XmlConsts.TAG_START);
            //IsStarMap = Tags.Contains(XmlConsts.TAG_STAR_MAP);

            //IsBastila   = Tags.Contains(XmlConsts.TAG_BASTILA) || LockedTag == XmlConsts.TAG_BASTILA;
            //IsCanderous = Tags.Contains(XmlConsts.TAG_CANDEROUS) || LockedTag == XmlConsts.TAG_CANDEROUS;
            //IsCarth     = Tags.Contains(XmlConsts.TAG_CARTH) || LockedTag == XmlConsts.TAG_CARTH;
            //IsHK47      = Tags.Contains(XmlConsts.TAG_HK47) || LockedTag == XmlConsts.TAG_HK47;
            //IsJolee     = Tags.Contains(XmlConsts.TAG_JOLEE) || LockedTag == XmlConsts.TAG_JOLEE;
            //IsJuhani    = Tags.Contains(XmlConsts.TAG_JUHANI) || LockedTag == XmlConsts.TAG_JUHANI;
            //IsMission   = Tags.Contains(XmlConsts.TAG_MISSION) || LockedTag == XmlConsts.TAG_MISSION;
            //IsT3M4      = Tags.Contains(XmlConsts.TAG_T3M4) || LockedTag == XmlConsts.TAG_T3M4;
            //IsZaalbar   = Tags.Contains(XmlConsts.TAG_ZAALBAR) || LockedTag == XmlConsts.TAG_ZAALBAR;

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
            var sb = new StringBuilder();
            _ = sb.Append($"Code: {WarpCode}, Name: {CommonName}");
            _ = Tags.Count > 0
                ? sb.Append($", Tags: [{Tags.Aggregate((i, j) => $"{i},{j}")}]")
                : sb.Append(", Tags: []");

            if (!string.IsNullOrWhiteSpace(LockedTag))
            {
                _ = sb.Append($", LockedTag: {LockedTag}");
            }

            if (UnlockSets.Count > 0)
            {
                _ = sb.Append($", Unlock: [");
                foreach (var set in UnlockSets)
                {
                    _ = sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                }
                _ = sb.Remove(sb.Length - 1, 1);    // Remove trailing ';'
                _ = sb.Append("]");
            }

            if (LeadsTo.Count > 0)
            {
                _ = sb.Append(" -> [");
                _ = sb.Append(LeadsTo[0].WarpCode);
                for (var i = 1; i < LeadsTo.Count; i++)
                {
                    _ = sb.Append(", ");
                    _ = sb.Append(LeadsTo[i].WarpCode);
                }
                _ = sb.Append("]");
            }

            return sb.ToString();
        }
    }
}
