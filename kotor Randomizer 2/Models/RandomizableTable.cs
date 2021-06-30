using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2.Models
{
    /// <summary>
    /// Represents a 2DA table that can be randomized.
    /// </summary>
    public class RandomizableTable : INotifyPropertyChanged
    {
        #region Members
        private string _name;
        private ObservableCollection<string> _columns;
        private ObservableCollection<string> _randomized;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a randomizable 2DA with the given name and columns.
        /// </summary>
        /// <param name="n">Table name</param>
        /// <param name="cols">Table columns</param>
        public RandomizableTable(string n, IEnumerable<string> cols)
        {
            Name = n;
            Columns = new ObservableCollection<string>(cols);
            Randomized = new ObservableCollection<string>();

            _randomized.CollectionChanged += Randomized_CollectionChanged;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Name of this table.
        /// </summary>
        public string Name
        {
            get => _name;
            private set => SetField(ref _name, value);
        }

        /// <summary>
        /// List of columns that are not randomized.
        /// </summary>
        public ObservableCollection<string> Columns
        {
            get => _columns;
            set => SetField(ref _columns, value);
        }

        /// <summary>
        /// List of the columns that are randomized.
        /// </summary>
        public ObservableCollection<string> Randomized
        {
            get => _randomized;
            set => SetField(ref _randomized, value);
        }

        /// <summary>
        /// True if any of the columns are randomized.
        /// </summary>
        public bool IsRandomized
        {
            get => Randomized.Any();
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
        private void Randomized_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("IsRandomized");
        }
        #endregion
    }
}
