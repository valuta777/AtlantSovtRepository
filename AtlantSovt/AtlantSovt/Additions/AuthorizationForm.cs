using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AtlantSovt.Additions
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm(MainForm mainForm)
        {
            InitializeComponent();
            this.loginTextBox.Text = LoadAuthorizationSettings(2);
            this.passwordTextBox.Text = LoadAuthorizationSettings(3);
            if(LoadAuthorizationSettings(4) == "true")
            {
                this.rememberPasswordCheckBox.Checked = true;
            }
            this.mainForm = mainForm;
        }

        MainForm mainForm { get; set; }

        public void SaveAuthorizationSettings(string login, string password, int user)
        {
            IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForDomain();

            string connectionString = "";

            switch(user)
            {
                case 1:
                    connectionString = $@"data source = 185.46.221.133,1903; initial catalog = AtlantSovt; persist security info = True; user id = {login}; password = {password}; MultipleActiveResultSets = True; App=EntityFramework; Connection Timeout = 30";
                    break;
                case 2:
                    connectionString =  $@"data source=YK-WORK\SQLEXPRESS;initial catalog=AtlantSovt;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework; Connection Timeout = 30";
                    break;
                case 3:
                    connectionString = $@"data source=V2-NB\SQLEXPRESS;initial catalog=AtlantSovt;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework; Connection Timeout = 30";
                    break;
            }

            IsolatedStorageFileStream stmWriter = new IsolatedStorageFileStream("AuthorizationSettings.xml", FileMode.Create, isoStorage);
            XmlTextWriter writer = new XmlTextWriter(stmWriter, Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("AuthorizationSettings");
            writer.WriteStartElement("ConnectionString");                   
            writer.WriteString(connectionString);
            writer.WriteEndElement();
            writer.WriteStartElement("Login");
            writer.WriteString(login);
            writer.WriteEndElement();
            writer.WriteStartElement("Password");
            if(rememberPasswordCheckBox.Checked)
            {
                writer.WriteString(password);
            }
            else
            {
                writer.WriteString("");
            }
            writer.WriteEndElement();
            writer.WriteStartElement("IsPasswordRemembered");
            if(rememberPasswordCheckBox.Checked)
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

        /// <summary>
        ///  1 - load ConnectionString;
        ///  2 - load Login; 
        ///  3 - load Password;
        ///  4 - load RememberPasswordCheckBox
        /// </summary>
        public static string LoadAuthorizationSettings(int selectedData)
        {
            IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForDomain();
            string returnedString = "";
            string connectionString = "";
            string password =  "";
            string login = "";
            string isPasswordRemembered = "";
            // Check whether the file exists
            if (isoStorage.GetFileNames("AuthorizationSettings.xml").Length > 0) 
            {
                // Create isoStorage StreamReader.
                StreamReader stmReader = new StreamReader
                                             (new IsolatedStorageFileStream
                                                   ("AuthorizationSettings.xml",
                                                    FileMode.Open,
                                                    isoStorage)); 

                XmlTextReader xmlReader = new XmlTextReader(stmReader);

                // Loop through the XML file until all Nodes have been read and processed.
                while (xmlReader.Read())
                {
                    switch (xmlReader.Name)
                    {
                        case "ConnectionString":                                      
                            connectionString = xmlReader.ReadString();
                            break;
                        case "Login":
                            login = xmlReader.ReadString();
                            break;
                        case "Password":
                            password = xmlReader.ReadString();
                            break;
                        case "IsPasswordRemembered":
                            isPasswordRemembered = xmlReader.ReadString();
                            break;
                    }
                }

                // Close the reader
                xmlReader.Close();
                stmReader.Close();
            }

            isoStorage.Close();

            switch (selectedData)
            {
                case 1:
                    returnedString = connectionString;
                    break;
                case 2:
                    returnedString = login;
                    break;
                case 3:
                    returnedString = password;
                    break;
                case 4:
                    returnedString = isPasswordRemembered;
                    break;
            }

            return returnedString;
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            if (loginTextBox.Text != "" && passwordTextBox.Text != "")
            {
                SaveAuthorizationSettings(loginTextBox.Text, passwordTextBox.Text, 1);
                this.Close();
                this.Dispose();
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Введіть_логін_пароль);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            mainForm.isServerConnected = false;
            this.Close();
            this.Dispose();
            Application.Exit();
        }
    }
}
