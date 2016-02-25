using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AtlantSovt.Additions
{
    class ChangeLanguage
    {
        public ChangeLanguage(CultureInfo language, MainForm mainForm)
        {
            StartupMode = enumStartupMode.UseSavedCulture;
            SelectedCulture = language;
            this.mainForm = mainForm;
        }

        public ChangeLanguage(CultureInfo language)
        {
            StartupMode = enumStartupMode.UseSavedCulture;
            SelectedCulture = language;
        }

        public ChangeLanguage(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public ChangeLanguage()
        {

        }

        MainForm mainForm {get; set;}

        public enum enumStartupMode
        {
            UseDefaultCulture = 0,
            UseSavedCulture = 1,
            ShowDialog = 2
        }

        public string isLanguageChanged;

        private enumStartupMode StartupMode;

        private CultureInfo SelectedCulture;

        public void LoadSettings()
        {
            // Set the defaults
            SelectedCulture = Thread.CurrentThread.CurrentUICulture;

            // Create an IsolatedStorageFile object and get the store
            // for this application.
            IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForDomain();

            // Check whether the file exists
            if (isoStorage.GetFileNames("CultureSettings.xml").Length > 0) //MLHIDE
            {
                // Create isoStorage StreamReader.
                StreamReader stmReader = new StreamReader
                                             (new IsolatedStorageFileStream
                                                   ("CultureSettings.xml",
                                                    FileMode.Open,
                                                    isoStorage)); //MLHIDE

                XmlTextReader xmlReader = new XmlTextReader(stmReader);

                // Loop through the XML file until all Nodes have been read and processed.
                while (xmlReader.Read())
                {
                    switch (xmlReader.Name)
                    {
                        case "StartupMode":                                         //MLHIDE
                            StartupMode = (enumStartupMode)int.Parse(xmlReader.ReadString());
                            break;
                        case "Culture":                                             //MLHIDE
                            String CultName = xmlReader.ReadString();
                            CultureInfo CultInfo = new CultureInfo(CultName);
                            SelectedCulture = CultInfo;
                            break;
                        case "IsLanguageChanged":
                            isLanguageChanged = xmlReader.ReadString();
                            break;
                    }
                }

                // Close the reader
                xmlReader.Close();
                stmReader.Close();
            }

            isoStorage.Close();

            if (SelectedCulture != null)
            {
                // Actually change the culture of the current thread.
                Thread.CurrentThread.CurrentUICulture = SelectedCulture;
            }
        }

        public void SaveSettings()
        {
            // Get an isolated store for user, domain, and assembly and put it into 
            // an IsolatedStorageFile object.
            IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForDomain();

            // Create isoStorage StreamWriter and assign it to an XmlTextWriter variable.
            IsolatedStorageFileStream stmWriter = new IsolatedStorageFileStream("CultureSettings.xml", FileMode.Create, isoStorage); //MLHIDE
            XmlTextWriter writer = new XmlTextWriter(stmWriter, Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("CultureSettings");                     //MLHIDE
            writer.WriteStartElement("StartupMode");                         //MLHIDE
            writer.WriteString(((int)StartupMode).ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Culture");                             //MLHIDE
            writer.WriteString(SelectedCulture?.Name);
            writer.WriteEndElement();
            writer.WriteStartElement("IsLanguageChanged");
            if (mainForm != null && mainForm.isLanguageChanged)
            {
                writer.WriteString("true");
            }
            else
            {
                writer.WriteString("false");
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();

            stmWriter.Close();
            isoStorage.Close();
        }
    }
}
