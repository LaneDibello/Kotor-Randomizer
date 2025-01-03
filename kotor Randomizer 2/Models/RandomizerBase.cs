﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace kotor_Randomizer_2.Models
{
    /// <summary>
    /// Games which have supported randomization.
    /// </summary>
    public enum Game
    {
        Unsupported,
        Kotor1,
        Kotor2,
    }

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
        /// <summary> Back up is in progress. </summary>
        BackingUp,
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
        #region Backing Fields
        private string _settingsFilePath = string.Empty;
        #endregion

        #region Properties

        /// <summary> The game this class stores randomization settings for. </summary>
        public virtual Game Game { get; }

        /// <summary> File extention of settings save files for this game. </summary>
        public virtual string Extension { get; }

        /// <summary> Path to the loaded settings file. </summary>
        public string SettingsFilePath
        {
            get => _settingsFilePath;
            protected set => SetField(ref _settingsFilePath, value);
        }

        public virtual bool SupportsAnimation => false;
        public virtual bool SupportsAudio => false;
        public virtual bool SupportsCosmetics => SupportsAnimation || SupportsModels || SupportsTextures;
        public virtual bool SupportsItems => false;
        public virtual bool SupportsModels => false;
        public virtual bool SupportsModules => false;
        public virtual bool SupportsOther => false;
        public virtual bool SupportsText => false;
        public virtual bool SupportsTextures => false;
        public virtual bool SupportsTables => false;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
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

            // Is the file in KRP format?
            if (fi.Extension.ToLower() == ".krp")
                ReadKRP(File.OpenRead(path));
            else
                ReadFromFile(path);

            SettingsFilePath = fi.FullName;
        }

        /// <summary>
        /// Saves a settings file at the requested location.
        /// </summary>
        public virtual void Save(string path)
        {
            if (File.Exists(path)) File.Delete(path);
            var fi = new FileInfo(path);
            SettingsFilePath = fi.FullName;
            WriteToFile(path);
        }

        /// <summary> Resets all randomization settings to the default value. </summary>
        public abstract void ResetAllSettings();

        /// <summary> Reads an xml preset file. This provides backwards compatibility. </summary>
        protected abstract void ReadFromFile(string path);

        /// <summary> Writes an xml preset file. This provides backwards compatibility. </summary>
        protected abstract void WriteToFile(string path);

        /// <summary> Reads a KRP file using the old, compact format. </summary>
        protected abstract void ReadKRP(Stream s);

        /// <summary> Writes a KRP file using the old, compact format. </summary>
        //protected abstract void WriteKRP(Stream s);

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Reset static randomization classes for a new randomization.
        /// </summary>
        protected virtual void ResetStaticRandomizationClasses()
        {
            ModuleRando.Reset(this);
            ItemRando.Reset();
            SoundRando.Reset();
            ModelRando.Reset();
            TextureRando.Reset();
            TwodaRandom.Reset();
            TextRando.Reset();
            OtherRando.Reset();
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of enumerated constants
        /// to an equivalent enumerated object of the provided enumeration type.
        /// </summary>
        /// <typeparam name="T">An enumeration type.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <returns>An object of type T whose value is represented by value.</returns>
        protected static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        #endregion
    }
}
