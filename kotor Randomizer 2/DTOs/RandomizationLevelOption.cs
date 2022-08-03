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

        public RandomizationLevelOption(string label, string toolTip)
        {
            CheckboxLabel = label;
            CheckboxToolTip = toolTip;
        }

        #endregion

        #region Properties
        public RandomizationLevel Level
        {
            get => Flags.ToRandomizationLevel();
            set => Flags = value.ToRandoLevelFlags(SubtypeVisible);
        }
        //private RandomizationLevel _level;
        //public RandomizationLevel Level
        //{
        //    get => _level;
        //    set => SetField(ref _level, value);
        //}

        private RandoLevelFlags _flags = RandoLevelFlags.Subtype;
        public RandoLevelFlags Flags
        {
            get => _flags;
            set => SetField(ref _flags, value);
        }

        private string _checkboxLabel = "Default";
        public string CheckboxLabel
        {
            get => _checkboxLabel;
            set => SetField(ref _checkboxLabel, value);
        }

        private string _checkboxToolTip;
        public string CheckboxToolTip
        {
            get => _checkboxToolTip;
            set => SetField(ref _checkboxToolTip, value);
        }

        private bool _subtypeVisible = true;
        public bool SubtypeVisible
        {
            get => _subtypeVisible;
            set => SetField(ref _subtypeVisible, value);
        }

        private string _subtypeLabel = "Subtype";
        public string SubtypeLabel
        {
            get => _subtypeLabel;
            set => SetField(ref _subtypeLabel, value);
        }

        private bool _typeVisible = true;
        public bool TypeVisible
        {
            get => _typeVisible;
            set => SetField(ref _typeVisible, value);
        }

        private string _typeLabel = "Type";
        public string TypeLabel
        {
            get => _typeLabel;
            set => SetField(ref _typeLabel, value);
        }

        private bool _maxVisible = true;
        public bool MaxVisible
        {
            get => _maxVisible;
            set => SetField(ref _maxVisible, value);
        }

        private string _maxLabel = "Max";
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
