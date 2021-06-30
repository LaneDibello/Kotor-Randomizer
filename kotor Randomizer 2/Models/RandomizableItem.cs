using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2.Models
{
    /// <summary>
    /// Encapsulates template items that can be randomized within the game.
    /// </summary>
    public class RandomizableItem
    {
        /// <summary>
        /// UNUSED. Constructs the object by parsing the ID and Tags from strings.
        /// </summary>
        public RandomizableItem(string id = "", string tags = "")
        {
            if (!string.IsNullOrWhiteSpace(id)) ID = int.Parse(id);
            if (!string.IsNullOrWhiteSpace(tags)) Tags.AddRange(tags.Split(";,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary> Unique identifier for the item. </summary>
        public int ID { get; set; }
        /// <summary> Unique item code string. </summary>
        public string Code { get; set; }
        /// <summary> Description of the item. </summary>
        public string Label { get; set; }
        /// <summary> UNUSED. Collection of tags that identify item groups. </summary>
        public List<string> Tags { get; set; } = new List<string>();
    }
}
