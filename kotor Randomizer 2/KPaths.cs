using kotor_Randomizer_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2
{
    /// <summary>
    /// Contains paths to the various subdirectories within the game directory.
    /// </summary>
    public class KPaths
    {
        #region Folder and File Names
        /// <summary> Filename of the log file indicating that the game has been randomized. </summary>
        public const string RANDOMIZED_LOG_FILENAME = "RANDOMIZED.log";

        public const string K_CHITIN = "chitin.key";
        public const string K_DIALOG = "dialog.tlk";

        public const string K1_DATA = "data";
        public const string K1_LIPS = "lips";
        public const string K1_MODULES = "modules";
        public const string K1_OVERRIDE = "Override";
        public const string K1_RIMS = "rims";
        public const string K1_MUSIC = "streammusic";
        public const string K1_SOUNDS = "streamsounds";
        public const string K1_TEXTURE = "TexturePacks";

        public const string K2_DATA = "data";
        public const string K2_LIPS = "lips";
        public const string K2_MODULES = "Modules";
        public const string K2_OVERRIDE = "override";
        public const string K2_RIMS = "rims";
        public const string K2_MUSIC = "StreamMusic";
        public const string K2_SOUNDS = "StreamSounds";
        public const string K2_TEXTURE = "TexturePacks";
        #endregion

        /// <summary> The game this path contains. </summary>
        public Game game { get; private set; }

        #region Paths

        /// <summary> Path to the swkotor game directory. </summary>
        public string swkotor { get; private set; }

        /// <summary> Path to the RANDOMIZED.log file within the swkotor directory. </summary>
        public string RANDOMIZED_LOG => Path.Combine(swkotor, RANDOMIZED_LOG_FILENAME);

        /// <summary> Path to the chitin.key file within the swkotor directory. </summary>
        public string chitin => Path.Combine(swkotor, K_CHITIN);

        /// <summary> Path to the dialog.tlk file within the swkotor directory </summary>
        public string dialog => Path.Combine(swkotor, K_DIALOG);

        /// <summary> Path to the swkotor\data directory. </summary>
        public string data => Path.Combine(swkotor, game == Game.Kotor1 ? K1_DATA : K2_DATA) + "\\";

        /// <summary> Path to the swkotor\lips directory. </summary>
        public string lips => Path.Combine(swkotor, game == Game.Kotor1 ? K1_LIPS : K2_LIPS) + "\\";

        /// <summary> Path to the swkotor\modules directory. </summary>
        public string modules => Path.Combine(swkotor, game == Game.Kotor1 ? K1_MODULES : K2_MODULES) + "\\";

        /// <summary> Path to the swkotor\Override directory. </summary>
        public string Override => Path.Combine(swkotor, game == Game.Kotor1 ? K1_OVERRIDE : K2_OVERRIDE) + "\\";

        /// <summary> Path to the swkotor\rims directory. </summary>
        public string rims => Path.Combine(swkotor, game == Game.Kotor1 ? K1_RIMS : K2_RIMS) + "\\";

        /// <summary> Path to the swkotor\streammusic directory. </summary>
        public string music => Path.Combine(swkotor, game == Game.Kotor1 ? K1_MUSIC : K2_MUSIC) + "\\";

        /// <summary> Path to the swkotor\streamsounds directory. </summary>
        public string sounds => Path.Combine(swkotor, game == Game.Kotor1 ? K1_SOUNDS : K2_SOUNDS) + "\\";

        /// <summary> Path to the swkotor\TexturePacks directory. </summary>
        public string TexturePacks => Path.Combine(swkotor, game == Game.Kotor1 ? K1_TEXTURE : K2_TEXTURE) + "\\";

        #endregion

        #region Backup Paths

        /// <summary> Path to the backup of the chitin.key file within the swkotor directory. </summary>
        public string chitin_backup => GetBackupPath(chitin);

        /// <summary> Path to the backup of the dialog.tlk file within the swkotor directory. </summary>
        public string dialog_backup => GetBackupPath(dialog);

        /// <summary> Path to the backup of the swkotor\data directory. </summary>
        public string data_backup => GetBackupPath(data);

        /// <summary> Path to the backup of the swkotor\lips directory. </summary>
        public string lips_backup => GetBackupPath(lips);

        /// <summary> Path to the backup of the swkotor\modules directory. </summary>
        public string modules_backup => GetBackupPath(modules);

        /// <summary> Path to the backup of the swkotor\Override directory. </summary>
        public string Override_backup => GetBackupPath(Override);

        /// <summary> Path to the backup of the swkotor\rims directory. </summary>
        public string rims_backup => GetBackupPath(rims);

        /// <summary> Path to the backup of the swkotor\streammusic directory. </summary>
        public string music_backup => GetBackupPath(music);

        /// <summary> Path to the backup of the swkotor\streamsounds directory. </summary>
        public string sounds_backup => GetBackupPath(sounds);

        /// <summary> Path to the backup of the swkotor\TexturePacks directory. </summary>
        public string TexturePacks_backup => GetBackupPath(TexturePacks);

        #endregion

        /// <summary>
        /// Constructs paths to the SW KotOR directory and subdirectories.
        /// </summary>
        /// <param name="swkotor_path">Path to the base swkotor game directory.</param>
        public KPaths(string swkotor_path)
        {
            var dir = new DirectoryInfo(swkotor_path);
            swkotor = dir.FullName;

            var files = dir.EnumerateFiles();
            if (files.Any(fi => fi.Name == "swkotor.exe"))
            {
                game = Game.Kotor1;
            }
            else if (files.Any(fi => fi.Name == "swkotor2.exe"))
            {
                game = Game.Kotor2;
            }
            else
            {
                throw new FileNotFoundException($"Supported game file not found in directory \"{swkotor_path}\"");
            }
        }

        #region Get Files

        /// <summary> Returns a list of the current files in the swkotor base directory. </summary>
        public FileInfo[] FilesInBaseDir
        { get { return new DirectoryInfo(swkotor).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\data directory. </summary>
        public FileInfo[] FilesInData
        { get { return new DirectoryInfo(data).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\lips directory. </summary>
        public FileInfo[] FilesInLips
        { get { return new DirectoryInfo(lips).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\modules directory. </summary>
        public FileInfo[] FilesInModules
        { get { return new DirectoryInfo(modules).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\music directory. </summary>
        public FileInfo[] FilesInMusic
        { get { return new DirectoryInfo(music).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\music_bak directory. </summary>
        public FileInfo[] FilesInMusicBackup
        { get { return new DirectoryInfo(music_backup).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\sounds directory. </summary>
        public FileInfo[] FilesInSounds
        { get { return new DirectoryInfo(sounds).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\sounds_bak directory. </summary>
        public FileInfo[] FilesInSoundsBackup
        { get { return new DirectoryInfo(sounds_backup).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\Override directory. </summary>
        public FileInfo[] FilesInOverride
        { get { return new DirectoryInfo(Override).GetFiles(); } }

        /// <summary> Returns a list of the current files in the swkotor\rims directory. </summary>
        public FileInfo[] FilesInRims => Directory.Exists(rims) ? new DirectoryInfo(rims).GetFiles() : null;

        /// <summary> Returns a list of the current files in the swkotor\TexturePacks directory. </summary>
        public FileInfo[] FilesInTexturePacks
        { get { return new DirectoryInfo(TexturePacks).GetFiles(); } }

        #endregion

        #region Back Up

        /// <summary>
        /// Creates a backup of the modules directory if it doesn't exist already.
        /// </summary>
        public void BackUpModulesDirectory()
        {
            if (!Directory.Exists(modules_backup))
            {
                Directory.CreateDirectory(modules_backup);
                foreach (FileInfo file in FilesInModules)
                {
                    file.CopyTo(Path.Combine(modules_backup, file.Name), true);
                }
            }
        }

        /// <summary>
        /// Creates a backup of the lips directory if it doesn't exist already.
        /// </summary>
        public void BackUpLipsDirectory()
        {
            if (!Directory.Exists(lips_backup))
            {
                Directory.CreateDirectory(lips_backup);
                foreach (FileInfo file in FilesInLips)
                {
                    file.CopyTo(Path.Combine(lips_backup, file.Name), true);
                }
            }
        }

        /// <summary>
        /// Creates a backup of the Override directory if it doesn't exist already.
        /// </summary>
        public void BackUpOverrideDirectory()
        {
            if (!Directory.Exists(Override_backup))
            {
                Directory.CreateDirectory(Override_backup);
                foreach (FileInfo file in FilesInOverride)
                {
                    file.CopyTo(Path.Combine(Override_backup, file.Name), true);
                }
            }
        }

        /// <summary>
        /// Creates a backup of the chitin file if it doesn't exist already.
        /// </summary>
        public void BackUpChitinFile()
        {
            if (!File.Exists(chitin_backup))
            {
                File.Copy(chitin, chitin_backup);
            }
        }

        /// <summary>
        /// Creates a backup of the music directory if it doesn't exist already.
        /// </summary>
        public void BackUpMusicDirectory()
        {
            if (!Directory.Exists(music_backup))
            {
                Directory.CreateDirectory(music_backup);
                foreach (FileInfo file in FilesInMusic)
                {
                    file.CopyTo(Path.Combine(music_backup, file.Name), true);
                }
            }
        }

        /// <summary>
        /// Creates a backup of the sound directory if it doesn't exist already.
        /// </summary>
        public void BackUpSoundDirectory()
        {
            if (!Directory.Exists(sounds_backup))
            {
                Directory.CreateDirectory(sounds_backup);
                foreach (FileInfo file in FilesInSounds)
                {
                    file.CopyTo(Path.Combine(sounds_backup, file.Name), true);
                }
            }
        }

        /// <summary>
        /// Creates a backup of the TexturePacks directory if it doesn't exist already.
        /// </summary>
        public void BackUpTexturesDirectory()
        {
            if (!Directory.Exists(TexturePacks_backup))
            {
                Directory.CreateDirectory(TexturePacks_backup);
                foreach (FileInfo file in FilesInTexturePacks)
                {
                    file.CopyTo(Path.Combine(TexturePacks_backup, file.Name), true);
                }
            }
        }

        /// <summary>
        /// Creates a backup of the dialog file if it doesn't exist already.
        /// </summary>
        public void BackUpDialogFile()
        {
            if (!File.Exists(dialog_backup))
            {
                File.Copy(dialog, dialog_backup);
            }
        }

        #endregion

        #region Restore

        /// <summary>
        /// If the backup Modules directory exists, restore it to the active directory.
        /// </summary>
        public void RestoreModulesDirectory()
        {
            if (Directory.Exists(modules_backup))
            {
                if (Directory.Exists(modules))
                    Directory.Delete(modules, true);
                Directory.Move(modules_backup, modules);
            }
        }

        /// <summary>
        /// If the backup Lips directory exists, restore it to the active directory.
        /// </summary>
        public void RestoreLipsDirectory()
        {
            if (Directory.Exists(lips_backup))
            {
                if (Directory.Exists(lips))
                    Directory.Delete(lips, true);
                Directory.Move(lips_backup, lips);
            }
        }

        /// <summary>
        /// If the backup Override directory exists, restore it to the active directory.
        /// </summary>
        public void RestoreOverrideDirectory()
        {
            if (Directory.Exists(Override_backup))
            {
                if (Directory.Exists(Override))
                    Directory.Delete(Override, true);
                Directory.Move(Override_backup, Override);
            }
        }

        /// <summary>
        /// If the backup Music directory exists, restore it to the active directory.
        /// </summary>
        public void RestoreMusicDirectory()
        {
            if (Directory.Exists(music_backup))
            {
                if (Directory.Exists(music))
                    Directory.Delete(music, true);
                Directory.Move(music_backup, music);
            }
        }

        /// <summary>
        /// If the backup Sounds directory exists, restore it to the active directory.
        /// </summary>
        public void RestoreSoundsDirectory()
        {
            if (Directory.Exists(sounds_backup))
            {
                if (Directory.Exists(sounds))
                    Directory.Delete(sounds, true);
                Directory.Move(sounds_backup, sounds);
            }
        }

        /// <summary>
        /// If the backup TexturePacks directory exists, restore it to the active directory.
        /// </summary>
        public void RestoreTexturePacksDirectory()
        {
            if (Directory.Exists(TexturePacks_backup))
            {
                if (Directory.Exists(TexturePacks))
                    Directory.Delete(TexturePacks, true);
                Directory.Move(TexturePacks_backup, TexturePacks);
            }
        }

        /// <summary>
        /// If the backup chitin file exists, restore it to the active directory.
        /// </summary>
        public void RestoreChitinFile()
        {
            if (File.Exists(chitin_backup))
            {
                if (File.Exists(chitin))
                    File.Delete(chitin);
                File.Move(chitin_backup, chitin);
            }
        }

        /// <summary>
        /// If the backup dialog file exists, restore it to the active directory.
        /// </summary>
        public void RestoreDialogFile()
        {
            if (File.Exists(dialog_backup))
            {
                if (File.Exists(dialog))
                    File.Delete(dialog);
                File.Move(dialog_backup, dialog);
            }
        }

        #endregion

        /// <summary>
        /// Gets the backup version of the requested path.
        /// </summary>
        /// <param name="path">Path to a directory or file.</param>
        /// <returns>Path to the backup version.</returns>
        public static string GetBackupPath(string path)
        {
            if (path.Last() == '\\')
            {
                return path.TrimEnd('\\') + "_bak\\";
            }
            else
            {
                return path + ".bak";
            }
        }

        /// <summary>
        /// Gets the old version of the requested path.
        /// </summary>
        /// <param name="path">Path to a directory or file.</param>
        /// <returns>Path to the old version.</returns>
        public static string GetOldPath(string path)
        {
            if (path.Last() == '\\')
            {
                return path.TrimEnd('\\') + "_old\\";
            }
            else
            {
                return path + ".old";
            }
        }

        /// <summary>
        /// Prints the path to the base directory for KotOR.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Game: {game}, Base directory: {swkotor}";
        }
    }
}
