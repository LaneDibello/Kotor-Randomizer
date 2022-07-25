using KotOR_IO;
using kotor_Randomizer_2.Extensions;

namespace kotor_Randomizer_2.Models
{
    /// <summary>
    /// Encapsulates quality of life option within the game.
    /// </summary>
    public class QualityOfLifeOption
    {
        public QualityOfLifeOption() { }

        public QualityOfLifeOption(QualityOfLife qol)
        {
            QoL = qol;
            Area = qol.ToArea();
            Label = qol.ToLabel();
            ToolTipMessage = qol.ToDescription();
        }

        /// <summary> Description of what is unlocked. </summary>
        public string Label { get; set; }

        /// <summary> Area where the "door" exists. </summary>
        public string Area { get; set; }

        /// <summary> Quality of Life option to enable / disable. </summary>
        public QualityOfLife QoL { get; set; }

        /// <summary> Message to display as a ToolTip. </summary>
        public string ToolTipMessage { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"[{Area}] {Label} ({QoL} = {(int)QoL})";
        }
    }
}
