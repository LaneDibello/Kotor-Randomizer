using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RandoHelper.Models
{
    public class ModuleVertex
    {
        #region Properties
        public string WarpCode { get; }
        public string CommonName { get; }
        public string Planet { get; }
        public List<ModuleEdge> LeadsTo { get; } = new List<ModuleEdge>();

        public bool IsMalak { get; }
        public bool IsPazaak { get; }
        public bool IsStart { get; }
        public bool IsStarMap { get; }
        public bool IsParty => IsBastila || IsCanderous || IsCarth  ||
                               IsHK47    || IsJolee     || IsJuhani ||
                               IsMission || IsT3M4      || IsZaalbar;

        public bool IsBastila { get; }
        public bool IsCanderous { get; }
        public bool IsCarth { get; }
        public bool IsHK47 { get; }
        public bool IsJolee { get; }
        public bool IsJuhani { get; }
        public bool IsMission { get; }
        public bool IsT3M4 { get; }
        public bool IsZaalbar { get; }

        public List<string> Tags { get; } = new List<string>();
        public string LockedTag { get; }

        // List of sets: Unlock="A,B,C; C,D,E; E,F,G".
        //  , is AND within the set
        //  ; is OR between sets
        public List<List<string>> UnlockSets { get; } = new List<List<string>>();
        #endregion

        public ModuleVertex()
        {
            WarpCode = "warp_code";
            CommonName = "Common Name";
            Planet = "Planet";
            LeadsTo.AddRange(new List<ModuleEdge> {
                new ModuleEdge(),
                new ModuleEdge(),
                new ModuleEdge(),
                new ModuleEdge(),
            });
        }

        public ModuleVertex(XElement element)
        {
            // Check for null parameter.
            if (element == null)
            {
                throw new ArgumentException("Parameter \'element\' can't be null.", nameof(element));
            }

            // Get Planet
            Planet = element.Attribute(XmlConsts.Planet)?.Value ?? string.Empty;

            // Get WarpCode
            XAttribute code = element.Attribute(XmlConsts.Code);
            WarpCode = code == null || string.IsNullOrEmpty(code.Value)
                ? throw new ArgumentException("No \'WarpCode\' attribute found in the XML element.")
                : code.Value;

            // Get CommonName
            XAttribute name = element.Attribute(XmlConsts.Name);
            CommonName = name == null || string.IsNullOrEmpty(name.Value)
                ? throw new ArgumentException("No \'CommonName\' attribute found in the XML element.")
                : name.Value;

            // Get list of Tags
            XAttribute tags = element.Attribute(XmlConsts.Tags);
            if (tags != null && !string.IsNullOrWhiteSpace(tags.Value))
            {
                foreach (string tag in tags.Value.Split(XmlConsts.SeparatorComma))
                {
                    Tags.Add(tag);
                }
            }

            // Get LockedTag
            LockedTag = element.Attribute(XmlConsts.LockedTag)?.Value; // Null if it doesn't exist.

            // Parse Tags
            IsMalak = Tags.Contains(XmlConsts.Malak);
            IsPazaak = Tags.Contains(XmlConsts.Pazaak);
            IsStart = Tags.Contains(XmlConsts.Start);
            IsStarMap = Tags.Contains(XmlConsts.StarMap);

            IsBastila = Tags.Contains(XmlConsts.Bastila) || LockedTag == XmlConsts.Bastila;
            IsCanderous = Tags.Contains(XmlConsts.Canderous) || LockedTag == XmlConsts.Canderous;
            IsCarth = Tags.Contains(XmlConsts.Carth) || LockedTag == XmlConsts.Carth;
            IsHK47 = Tags.Contains(XmlConsts.HK47) || LockedTag == XmlConsts.HK47;
            IsJolee = Tags.Contains(XmlConsts.Jolee) || LockedTag == XmlConsts.Jolee;
            IsJuhani = Tags.Contains(XmlConsts.Juhani) || LockedTag == XmlConsts.Juhani;
            IsMission = Tags.Contains(XmlConsts.Mission) || LockedTag == XmlConsts.Mission;
            IsT3M4 = Tags.Contains(XmlConsts.T3M4) || LockedTag == XmlConsts.T3M4;
            IsZaalbar = Tags.Contains(XmlConsts.Zaalbar) || LockedTag == XmlConsts.Zaalbar;

            // Get list of Unlocks
            XAttribute unlocks = element.Attribute(XmlConsts.Unlock);
            if (unlocks != null && !string.IsNullOrWhiteSpace(unlocks.Value))
            {
                foreach (string set in unlocks.Value.Split(XmlConsts.SeparatorSemicolon))
                {
                    List<string> unlockSet = new();
                    foreach (string unlock in set.Split(XmlConsts.SeparatorComma))
                    {
                        unlockSet.Add(unlock);
                    }

                    UnlockSets.Add(unlockSet);
                }
            }

            // Get adjacent vertices
            IEnumerable<XElement> descendants = element.Descendants(XmlConsts.LeadsTo);
            foreach (XElement desc in descendants)
            {
                LeadsTo.Add(new ModuleEdge(desc));
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();
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
                foreach (List<string> set in UnlockSets)
                {
                    _ = sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                }
                _ = sb.Append(']');
            }

            if (LeadsTo.Count > 0)
            {
                _ = sb.Append(" -> [");
                _ = sb.Append(LeadsTo[0].WarpCode);
                for (int i = 1; i < LeadsTo.Count; i++)
                {
                    _ = sb.Append(", ");
                    _ = sb.Append(LeadsTo[i].WarpCode);
                }
                _ = sb.Append(']');
            }

            return sb.ToString();
        }
    }
}
