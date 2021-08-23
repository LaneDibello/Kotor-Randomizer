using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Randomizer_WPF
{
    /// <summary>
    /// Interaction logic for RandoWindow.xaml
    /// </summary>
    public partial class TestWindow2 : Window
    {
        #region Properties
        public RandomizerVM RandoVM { get; set; } = new RandomizerVM();
        public string WindowTitle
        {
            get
            {
                Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return $"Kotor Randomizer (v{v.Major}.{v.Minor}.{v.Build})";
            }
        }
        #endregion

        #region Constructors
        public TestWindow2()
        {
            InitializeComponent();
        }
        #endregion

        private void OnChecked(object sender, RoutedEventArgs e)
        {

        }

        private void OnUnchecked(object sender, RoutedEventArgs e)
        {

        }
    }

    public class RandomizerVM
    {
        public ObservableCollection<TabVM> Tabs { get; set; } = new ObservableCollection<TabVM>();
        public RandomizerVM()
        {
            Tabs.Add(new TabVM()
            {
                Header = "General",
                RemoveCheckbox = true,
                Content = new ContentVM("General", new Views.GeneralView()),
            });
            Tabs.Add(new TabVM()
            {
                Header = "Modules",
                Content = new ContentVM("Modules", new Views.ModuleView()),
            });
            Tabs.Add(new TabVM()
            {
                Header = "Items",
                Content = new ContentVM("General", new Views.ItemView()),
            });
            Tabs.Add(new TabVM()
            {
                Header = "Audio",
                Content = new ContentVM("Audio", new Views.AudioView()),
            });
            Tabs.Add(new TabVM()
            {
                Header = "Cosmetics",
                Content = new ContentVM("Cosmetics", new Views.GeneralView()),
            });
            Tabs.Add(new TabVM()
            {
                Header = "Party",
                Content = new ContentVM("Party", new Views.GeneralView()),
            });
            Tabs.Add(new TabVM()
            {
                Header = "Text",
                Content = new ContentVM("Text", new Views.TextView()),
            });
            Tabs.Add(new TabVM()
            {
                Header = "Tables",
                Content = new ContentVM("Tables", new Views.GeneralView()),
            });
            Tabs.Add(new TabVM()
            {
                Header = "Randiomize",
                RemoveCheckbox = true,
                Content = new ContentVM("Randomize", new Views.RandomizeView()),
            });
        }
    }

    public class TabVM : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
        #endregion
        #region Properties
        string _header;
        public string Header
        {
            get => _header;
            set => SetField(ref _header, value);
        }

        bool _removeCheckbox = false;
        public bool RemoveCheckbox
        {
            get => _removeCheckbox;
            set => SetField(ref _removeCheckbox, value);
        }

        ContentVM _content = null;
        public ContentVM Content
        {
            get => _content;
            set => SetField(ref _content, value);
        }


        #endregion
    }

    public class ContentVM
    {
        public ContentVM(string name, UserControl content)
        {
            Name = name;
            Content = content;
        }
        public string Name { get; set; }
        public UserControl Content { get; set; }
    }
}
