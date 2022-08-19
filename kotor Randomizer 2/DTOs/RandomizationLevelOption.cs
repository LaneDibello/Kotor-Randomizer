using kotor_Randomizer_2.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace kotor_Randomizer_2.DTOs
{
    public class RandomizationLevelOption : INotifyPropertyChanged
    {
        #region Constructors
        public RandomizationLevelOption() { }

        public RandomizationLevelOption(
            string label, string toolTip = "",
            bool subtypeVisible = true, string subtypeLabel = "Subtype",
            bool typeVisible = true, string typeLabel = "Type",
            bool maxVisible = true, string maxLabel = "Max",
            RandomizationLevel level = RandomizationLevel.None)
        {
            CheckboxLabel = label;
            CheckboxToolTip = toolTip;
            SubtypeVisible = subtypeVisible;
            SubtypeLabel = subtypeLabel;
            TypeVisible = typeVisible;
            TypeLabel = typeLabel;
            MaxVisible = maxVisible;
            MaxLabel = maxLabel;
            Level = level;
        }

        #endregion

        #region Properties
        #region Backing Fields
        private RandoLevelFlags _flags = RandoLevelFlags.Subtype;
        private string _checkboxLabel = "Default";
        private string _checkboxToolTip;
        private bool _subtypeVisible = true;
        private string _subtypeLabel = "Subtype";
        private bool _typeVisible = true;
        private string _typeLabel = "Type";
        private bool _maxVisible = true;
        private string _maxLabel = "Max";
        #endregion

        public RandomizationLevel Level
        {
            get => Flags.ToRandomizationLevel();
            set => Flags = value.ToRandoLevelFlags(SubtypeVisible);
        }

        public RandoLevelFlags Flags
        {
            get => _flags;
            set => SetField(ref _flags, value);
        }

        public string CheckboxLabel
        {
            get => _checkboxLabel;
            set => SetField(ref _checkboxLabel, value);
        }

        public string CheckboxToolTip
        {
            get => _checkboxToolTip;
            set => SetField(ref _checkboxToolTip, value);
        }

        public bool SubtypeVisible
        {
            get => _subtypeVisible;
            set => SetField(ref _subtypeVisible, value);
        }

        public string SubtypeLabel
        {
            get => _subtypeLabel;
            set => SetField(ref _subtypeLabel, value);
        }

        public bool TypeVisible
        {
            get => _typeVisible;
            set => SetField(ref _typeVisible, value);
        }

        public string TypeLabel
        {
            get => _typeLabel;
            set => SetField(ref _typeLabel, value);
        }

        public bool MaxVisible
        {
            get => _maxVisible;
            set => SetField(ref _maxVisible, value);
        }

        public string MaxLabel
        {
            get => _maxLabel;
            set => SetField(ref _maxLabel, value);
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{CheckboxLabel}: {Level}";
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
