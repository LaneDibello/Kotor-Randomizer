using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2.Models
{
    /// <summary>
    /// Encapsulates an unlockable door within the game.
    /// </summary>
    public class UnlockableDoor
    {
        /// <summary> Description of what is unlocked. </summary>
        public string Label { get; set; }
        /// <summary> Area where the "door" exists. </summary>
        public string Area { get; set; }
        /// <summary> Associated ModuleExtras value. </summary>
        public ModuleExtras Tag { get; set; }
        /// <summary> Message to display as a ToolTip. </summary>
        public string ToolTipMessage { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"[{Area}] {Label} ({Tag.ToString()})";
        }
    }
}
