using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Randomizer_WPF
{
    public class SettingsFile
    {
        #region Constants
        public const string ELEM_SETTINGS = "Settings";
        public const string ATTR_KOTOR1_PATH = "Kotor1Path";
        public const string ATTR_KOTOR2_PATH = "Kotor2Path";
        public const string ATTR_SPOILER_PATH = "SpoilersPath";
        #endregion

        #region Constructors
        public SettingsFile() { }

        public SettingsFile(string path)
        {
            ReadFile(path);
        }
        #endregion

        #region Properties
        public string Kotor1Path { get; set; }
        public string Kotor2Path { get; set; }
        public string SpoilerPath { get; set; }
        #endregion

        #region Methods
        public void ReadFile(string path)
        {
            XDocument doc = XDocument.Load(path);
            var settings = doc.Descendants(ELEM_SETTINGS).FirstOrDefault();
            if (settings == null) throw new ArgumentException("Settings file is not correctly formatted.");

            Kotor1Path  = settings.Attribute(ATTR_KOTOR1_PATH )?.Value ?? string.Empty;
            Kotor2Path  = settings.Attribute(ATTR_KOTOR2_PATH )?.Value ?? string.Empty;
            SpoilerPath = settings.Attribute(ATTR_SPOILER_PATH)?.Value ?? string.Empty;
        }

        public void WriteFile(string path)
        {
            using (var writer = new XmlTextWriter(path, null))
            {
                writer.WriteStartElement(ELEM_SETTINGS);
                writer.WriteAttributeString(ATTR_KOTOR1_PATH,  Kotor1Path);
                writer.WriteAttributeString(ATTR_KOTOR2_PATH,  Kotor2Path);
                writer.WriteAttributeString(ATTR_SPOILER_PATH, SpoilerPath);
                writer.WriteEndElement();
                writer.Flush();
            }
        }
        #endregion
    }
}
