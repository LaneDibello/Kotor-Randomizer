using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RandoHelper.Models
{
    public class ModuleEdge
    {
        #region Properties
        public string WarpCode { get; }
        public string CommonName { get; }

        public bool IsFake { get; }
        public bool IsLocked { get; }
        public bool IsOnce { get; }

        public bool IsClip { get; }
        public bool IsDlz { get; }
        public bool IsFlu { get; }
        public bool IsGpw { get; }

        public bool IsAccessHangar { get; }
        public bool IsFixBox { get; }
        public bool IsFixHangar { get; }
        public bool IsFixMap { get; }
        public bool IsFixSpice { get; }

        public bool IsUnlockDanRuins { get; }
        public bool IsUnlockKorAcademy { get; }
        public bool IsUnlockManEmbassy { get; }
        public bool IsUnlockManHangar { get; }
        public bool IsUnlockStaBastila { get; }
        public bool IsUnlockTarVulkar { get; }
        public bool IsUnlockTarUndercity { get; }
        public bool IsUnlockUnkSummit { get; }
        public bool IsUnlockUnkTempleExit { get; }

        public List<string> Tags { get; } = new List<string>();

        // List of sets: Unlock="A,B,C; C,D,E; E,F,G".
        //  , is AND within the set
        //  ; is OR between sets
        public List<List<string>> UnlockSets { get; } = new List<List<string>>();
        #endregion

        public ModuleEdge()
        {
            WarpCode = "warp_code";
            CommonName = "Common Name";
        }

        public ModuleEdge(XElement element)
        {
            // Check for null parameter.
            if (element == null)
            {
                throw new ArgumentException("Parameter \'element\' can't be null.", nameof(element));
            }

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

            // Parse list of Tags
            IsFake = Tags.Contains(XmlConsts.Fake);
            IsLocked = Tags.Contains(XmlConsts.Locked);
            IsOnce = Tags.Contains(XmlConsts.Once);

            IsClip = Tags.Contains(XmlConsts.Clip);
            IsDlz = Tags.Contains(XmlConsts.DLZ);
            IsFlu = Tags.Contains(XmlConsts.FLU);
            IsGpw = Tags.Contains(XmlConsts.GPW);

            IsAccessHangar = Tags.Contains(XmlConsts.HangarAccess);
            IsFixHangar = Tags.Contains(XmlConsts.FixElev);
            IsFixBox = Tags.Contains(XmlConsts.FixBox);
            IsFixMap = Tags.Contains(XmlConsts.FixMap);
            IsFixSpice = Tags.Contains(XmlConsts.FixSpice);

            IsUnlockDanRuins = Tags.Contains(XmlConsts.UnlockRuins);
            IsUnlockKorAcademy = Tags.Contains(XmlConsts.UnlockAcademy);
            IsUnlockManEmbassy = Tags.Contains(XmlConsts.UnlockEmbassy);
            IsUnlockManHangar = Tags.Contains(XmlConsts.UnlockHangar);
            IsUnlockStaBastila = Tags.Contains(XmlConsts.UnlockBastila);
            IsUnlockTarVulkar = Tags.Contains(XmlConsts.UnlockVulkar);
            IsUnlockTarUndercity = Tags.Contains(XmlConsts.UnlockUndercity);
            IsUnlockUnkSummit = Tags.Contains(XmlConsts.UnlockSummit);
            IsUnlockUnkTempleExit = Tags.Contains(XmlConsts.UnlockTempleExit);
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            _ = sb.Append($"Code: {WarpCode}, Name: {CommonName}");
            _ = Tags.Count > 0
                ? sb.Append($", Tags: [{Tags.Aggregate((i, j) => $"{i},{j}")}]")
                : sb.Append(", Tags: []");

            if (UnlockSets.Count > 0)
            {
                _ = sb.Append($", Unlock: [");
                foreach (List<string> set in UnlockSets)
                {
                    _ = sb.Append($"{set.Aggregate((i, j) => $"{i},{j}")};");
                }
                _ = sb.Append(']');
            }

            return sb.ToString();
        }
    }
}
