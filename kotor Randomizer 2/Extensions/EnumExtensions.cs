using KotOR_IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace kotor_Randomizer_2.Extensions
{
    public static partial class EnumExtensions
    {
        /// <summary>
        /// Converts the <see cref="ModuleExtras"/> value into the first <see cref="SavePatchOptionAttribute"/> marked.
        /// </summary>
        public static SavePatchOptions ToSPO(this ModuleExtras value)
        {
            var attribute = value.GetAttributeOfType<SavePatchOptionAttribute>();
            return attribute == null ? SavePatchOptions.Invalid : attribute.SavePatchOption;
        }

        /// <summary>
        /// Converts the <see cref="ModuleExtras"/> value into the first <see cref="QualityOfLifeAttribute"/> marked.
        /// </summary>
        public static QualityOfLife ToQoL(this ModuleExtras value)
        {
            var attribute = value.GetAttributeOfType<QualityOfLifeAttribute>();
            return attribute == null ? QualityOfLife.Unknown : attribute.QualityOfLife;
        }

        /// <summary>
        /// Converts the flag based <see cref="ModuleExtras"/> value into a list of the selected flags.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static List<ModuleExtras> ToList(this ModuleExtras options)
        {
            var list = Enum.GetValues(typeof(ModuleExtras)).Cast<ModuleExtras>().Where(o => (options & o) == o).Select(o => o).ToList();
            if (list.Count > 1) list.Remove(ModuleExtras.Default);
            return list;
        }

        /// <summary>
        /// Retrieves the area value of the <see cref="InfoAttribute"/> attached to this enum.
        /// </summary>
        public static string ToArea(this Enum value)
        {
            var attribute = value.GetAttributeOfType<InfoAttribute>();
            return attribute == null ? "[NAN]" : attribute.Area;
        }

        /// <summary>
        /// Retrieves the label value of the <see cref="InfoAttribute"/> attached to this enum.
        /// </summary>
        public static string ToLabel(this Enum value)
        {
            var attribute = value.GetAttributeOfType<InfoAttribute>();
            return attribute == null ? "[No Label]" : attribute.Label;
        }

        /// <summary>
        /// Retrieves the tooltip value of the <see cref="InfoAttribute"/> attached to this enum.
        /// </summary>
        public static string ToToolTip(this Enum value)
        {
            var attribute = value.GetAttributeOfType<InfoAttribute>();
            return attribute == null ? "[No ToolTip]" : attribute.ToolTip;
        }

        /// <summary>
        /// Converts the flags of <see cref="RandoLevelFlags"/> to a proper <see cref="RandomizationLevel"/>.
        /// </summary>
        public static RandomizationLevel ToRandomizationLevel(this RandoLevelFlags flags)
        {
            if (!flags.HasFlag(RandoLevelFlags.Enabled)) return RandomizationLevel.None;        // Check for enabled randomization first.
            if (flags.HasFlag(RandoLevelFlags.Max))      return RandomizationLevel.Max;         // Prioritize higher levels of randomization.
            if (flags.HasFlag(RandoLevelFlags.Type))     return RandomizationLevel.Type;
            if (flags.HasFlag(RandoLevelFlags.Subtype))  return RandomizationLevel.Subtype;
            return RandomizationLevel.None; // Enabled, but no level selected.
        }

        public static RandoLevelFlags ToRandoLevelFlags(this RandomizationLevel level, bool subtypeVisible = true)
        {
            switch (level)
            {
                case RandomizationLevel.None:
                default:
                    return subtypeVisible
                        ? RandoLevelFlags.Subtype
                        : RandoLevelFlags.Type;
                case RandomizationLevel.Subtype:
                    return subtypeVisible
                        ? RandoLevelFlags.Enabled | RandoLevelFlags.Subtype
                        : RandoLevelFlags.Type;
                case RandomizationLevel.Type:
                    return RandoLevelFlags.Enabled | RandoLevelFlags.Type;
                case RandomizationLevel.Max:
                    return RandoLevelFlags.Enabled | RandoLevelFlags.Max;
            }
        }
    }

    /// <summary>
    /// Associate something with a particular <see cref="SavePatchOptions"/>.
    /// </summary>
    public class SavePatchOptionAttribute : Attribute
    {
        public SavePatchOptions SavePatchOption { get; set; }

        public SavePatchOptionAttribute(SavePatchOptions savePatchOption)
        {
            SavePatchOption = savePatchOption;
        }
    }

    /// <summary>
    /// Associate something with a particular <see cref="QualityOfLife"/>.
    /// </summary>
    public class QualityOfLifeAttribute : Attribute
    {
        public QualityOfLife QualityOfLife { get; set; }
        public QualityOfLifeAttribute(QualityOfLife qualityOfLife)
        {
            QualityOfLife = qualityOfLife;
        }
    }

    public class InfoAttribute : Attribute
    {
        public string Area { get; set; }
        public string Label { get; set; }
        public string ToolTip { get; set; }
        public InfoAttribute(string area, string label, string tooltip)
        {
            Area = area;
            Label = label;
            ToolTip = tooltip;
        }
        public InfoAttribute(string label, string tooltip)
        {
            Label = label;
            ToolTip = tooltip;
        }
        public InfoAttribute(string label)
        {
            Label = label;
        }
    }
}
