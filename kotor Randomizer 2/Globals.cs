using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2
{
    public class Globals
    {
        
        #region Types
        public struct Mod_Entry
        {
            private string _name;
            private bool _ommitted;

            public string name
            {
                get
                {
                    return _name;
                }
            }
            public bool ommitted
            {
                get
                {
                    return _ommitted;
                }
                set
                {
                    _ommitted = value;
                }
            }

            public Mod_Entry(string name, bool ommited)
            {
                _name = name;
                _ommitted = ommited;
            }
        }
        [Serializable]
        public enum RandomizationLevel //Thank you Glasnonck
        {
            /// <summary> No randomization. </summary>
            None = 0,
            /// <summary> Randomize similar types within the same category. </summary>
            Actions = 1,
            /// <summary> Randomize within the same category. </summary>
            Type = 2,
            /// <summary> Randomize with everything else set to Max. </summary>
            Max = 3,
        }
        #endregion

        #region Variables
        public static BindingList<Mod_Entry> BoundModules = new BindingList<Mod_Entry>();
        #endregion

        #region Constants
        public static readonly List<string> MODULES = new List<string>() {
            "danm13","danm14aa","danm14ab","danm14ac","danm14ad","danm14ae",
            "danm15","danm16","ebo_m12aa","ebo_m40aa","ebo_m40ad","ebo_m41aa",
            "ebo_m46ab","end_m01aa","end_m01ab","kas_m22aa","kas_m22ab","kas_m23aa",
            "kas_m23ab","kas_m23ac","kas_m23ad","kas_m24aa","kas_m25aa","korr_m33aa",
            "korr_m33ab","korr_m34aa","korr_m35aa","korr_m36aa","korr_m37aa",
            "korr_m38aa","korr_m38ab","korr_m39aa","lev_m40aa","lev_m40ab",
            "lev_m40ac","lev_m40ad","liv_m99aa","M12ab","manm26aa","manm26ab",
            "manm26ac","manm26ad","manm26ae","manm26mg","manm27aa","manm28aa",
            "manm28ab","manm28ac","manm28ad","sta_m45aa","sta_m45ab","sta_m45ac",
            "sta_m45ad","STUNT_00","STUNT_03a","STUNT_06","STUNT_07","STUNT_12",
            "STUNT_14","STUNT_16","STUNT_18","STUNT_19","STUNT_31b","STUNT_34",
            "STUNT_35","STUNT_42","STUNT_44","STUNT_50a","STUNT_51a","STUNT_54a",
            "STUNT_55a","STUNT_56a","STUNT_57","tar_m02aa","tar_m02ab","tar_m02ac",
            "tar_m02ad","tar_m02ae","tar_m02af","tar_m03aa","tar_m03ab","tar_m03ad",
            "tar_m03ae","tar_m03af","tar_m03mg","tar_m04aa","tar_m05aa","tar_m05ab",
            "tar_m08aa","tar_m09aa","tar_m09ab","tar_m10aa","tar_m10ab","tar_m10ac",
            "tar_m11aa","tar_m11ab","tat_m17aa","tat_m17ab","tat_m17ac","tat_m17ad",
            "tat_m17ae","tat_m17af","tat_m17ag","tat_m17mg","tat_m18aa","tat_m18ab",
            "tat_m18ac","tat_m20aa","unk_m41aa","unk_m41ab","unk_m41ac","unk_m41ad",
            "unk_m42aa","unk_m43aa","unk_m44aa","unk_m44ab","unk_m44ac" };

        public static readonly Dictionary<string, List<string>> PRESETS = new Dictionary<string, List<string>>()
        {
            {"Default", new List<string>()
                {
                "M12ab", "end_m01aa", "end_m01ab", "ebo_m40aa", "ebo_m12aa",
                "ebo_m40ad", "STUNT_00", "STUNT_03a", "STUNT_06", "STUNT_07",
                "STUNT_12", "STUNT_14", "STUNT_16", "STUNT_18", "STUNT_19",
                "STUNT_31b", "STUNT_34", "STUNT_35", "STUNT_42", "STUNT_44",
                "STUNT_50a", "STUNT_51a", "STUNT_54a", "STUNT_55a", "STUNT_56a",
                "STUNT_57"
                }
            },
            {"No Major Hubs", new List<string>()
                {
                "tar_m10ab", "ebo_m46ab", "liv_m99aa", "unk_m44ac", "manm26mg",
                "tar_m03mg", "tat_m17mg", "unk_m43aa", "tar_m03aa", "tat_m17aa",
                "korr_m36aa", "M12ab", "end_m01aa", "end_m01ab", "ebo_m40aa",
                "ebo_m12aa", "ebo_m40ad", "STUNT_00", "STUNT_03a", "STUNT_06",
                "STUNT_07", "STUNT_12", "STUNT_14", "STUNT_16", "STUNT_18",
                "STUNT_19", "STUNT_31b", "STUNT_34", "STUNT_35", "STUNT_42",
                "STUNT_44", "STUNT_50a", "STUNT_51a", "STUNT_54a", "STUNT_55a",
                "STUNT_56a", "STUNT_57"
                }
            },
            {"Max Random", new List<string>()
                {
                }
            }

        };
        #endregion

    }
}
