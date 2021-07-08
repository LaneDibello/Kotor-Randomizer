using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using ClosedXML.Excel;

namespace kotor_Randomizer_2.Models
{
    /// <summary>
    /// Enumeration of the possible busy states.
    /// </summary>
    public enum BusyState
    {
        /// <summary> Unknown busy state. </summary>
        Unknown = 0,
        /// <summary> Randomization is in progress. </summary>
        Randomizing,
        /// <summary> Spoiling is in progress. </summary>
        Spoiling,
        /// <summary> Unrandomization is in progress. </summary>
        Unrandomizing,
    }

    /// <summary>
    /// Encapsulates information to be passed into the DoRandomization process.
    /// </summary>
    public class RandoArgs
    {
        /// <summary> Randomization seed. </summary>
        public int Seed { get; set; }
        /// <summary> Full path to the game directory. </summary>
        public string GamePath { get; set; }
        /// <summary> Full path to the spoiler directory. </summary>
        public string SpoilersPath { get; set; }
    }

    /// <summary>
    /// Encapsulates information to be passed when reporting randomization progress.
    /// </summary>
    public class RandoProgress
    {
        /// <summary> Percentage complete. </summary>
        public double PercentComplete { get; set; }
        /// <summary> Text to write to the status. </summary>
        public string Status { get; set; }
        /// <summary> Text to write to the log. </summary>
        public string Log { get; set; }
        /// <summary> Current busy state. </summary>
        public BusyState State { get; set; }
    }

    /// <summary>
    /// Base class of randomization settings and processes.
    /// </summary>
    public abstract class RandomizerBase : INotifyPropertyChanged
    {
        /// <summary> Name of the settings file. </summary>
        public string SettingsFileName { get; protected set; }

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
        public abstract void Randomizer_DoWork(object sender, DoWorkEventArgs e);
        public abstract void Unrandomize(object sender, DoWorkEventArgs e);
        #endregion Events

        #region Public Methods
        /// <summary>
        /// Loads the requested settings file.
        /// </summary>
        public virtual void Load(string path)
        {
            var fi = new FileInfo(path);
            SettingsFileName = fi.Name;

            // Is the file in KRP format?
            if (fi.Extension.ToLower() == "krp")
                ReadKRP(File.OpenRead(path));
            else
                ReadFromFile(path);
        }

        /// <summary>
        /// Saves a settings file at the requested location.
        /// </summary>
        public virtual void Save(string path)
        {
            if (File.Exists(path)) File.Delete(path);
            var fi = new FileInfo(path);
            SettingsFileName = fi.Name;

            //// Will the file be in KRP format?
            //if (path.ToLower().EndsWith(".krp"))
            //    WriteKRP(File.OpenWrite(path));
            //else
            WriteToFile(path);
        }

        /// <summary> Reads an xml preset file. This provides backwards compatibility. </summary>
        protected abstract void ReadFromFile(string path);

        /// <summary> Writes an xml preset file. This provides backwards compatibility. </summary>
        protected abstract void WriteToFile(string path);

        /// <summary> Reads a KRP file using the old, compact format. </summary>
        protected abstract void ReadKRP(Stream s);

        /// <summary> Writes a KRP file using the old, compact format. </summary>
        //protected abstract void WriteKRP(Stream s);
        #endregion Public Methods
    }
}
