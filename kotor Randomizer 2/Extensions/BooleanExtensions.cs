namespace kotor_Randomizer_2.Extensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts boolean value to "Enabled" if true and "Disabled" if false. The "reverse" parameter swaps this behavior.
        /// </summary>
        public static string ToEnabledDisabled(this bool isEnabled, bool reverse = false)
        {

            return reverse ? isEnabled ? "Disabled" : "Enabled"
                           : isEnabled ? "Enabled" : "Disabled";
        }

        /// <summary>
        /// Converts boolean value to "Locked" if true and "Unlocked" if false. The "reverse" parameter swaps this behavior.
        /// </summary>
        public static string ToLockedUnlocked(this bool isLocked, bool reverse = false)
        {

            return reverse ? isLocked ? "Unlocked" : "Locked"
                           : isLocked ? "Locked" : "Unlocked";
        }

        /// <summary>
        /// Converts boolean value to "Yes" if true and "No" if false. The "reverse" parameter swaps this behavior.
        /// </summary>
        public static string ToYesNo(this bool isYes, bool reverse = false)
        {
            return reverse ? isYes ? "No" : "Yes"
                           : isYes ? "Yes" : "No";
        }
    }
}
