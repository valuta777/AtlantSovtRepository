using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    public partial class ClientContactAddForm : Form
    {
        string new_ContactPerson;
        string new_TelephoneNumber;
        string new_FaxNumber;
        string new_Email;
        bool IsAdding;
        long Id;

        public ClientContactAddForm()
        {
            InitializeComponent();
            long Id = 0;
            IsAdding = true;
        }

        internal string AddClientContact(long id, bool IsAddingnew)
        {
            using (var db = new AtlantSovtContext())
            {
                var New_ClientContact = new ClientContact
                {
                    ClientId = id,
                    ContactPerson = new_ContactPerson,
                    TelephoneNumber = new_TelephoneNumber,
                    FaxNumber = new_FaxNumber,
                    Email = new_Email,
                };
                try
                {
                    db.ClientContacts.Add(New_ClientContact);
                    db.SaveChanges();
                    IsAdding = IsAddingnew;
                    if (IsAdding)
                    {
                        return AtlantSovt.Properties.Resources.Контакт_успішно_доданий_клієнту + "[" + New_ClientContact.ClientId + @"]";
                    }
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Контакт_успішно_доданий_клієнту + New_ClientContact.ClientId);
                        return string.Empty;
                    }                    

                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);
                    return string.Empty;
                }

            }
        }

        private void addContactClientButton_Click(object sender, EventArgs e)
        {
            if (clientAddContactPersonTextBox.Text != "" || clientAddContactPhoneTextBox.Text != "" || clientAddContactFaxTextBox.Text != "" || clientAddContactEmailTextBox.Text != "")
            {

                new_ContactPerson = clientAddContactPersonTextBox.Text;
                new_TelephoneNumber = clientAddContactPhoneTextBox.Text;
                new_FaxNumber = clientAddContactFaxTextBox.Text;
                new_Email = clientAddContactEmailTextBox.Text;

                this.Hide();
                if (Id != 0)
                {
                    AddClientContact(Id, false);
                }
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Для_збереження_заповніть_хоча_б_одне_поле);
            }
        }

        internal void AddClientContact2(long id) 
        {
            Id = id;
        }

        private void ClientContactAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAdding)
            {
                if (clientAddContactPersonTextBox.Text != "" || clientAddContactPhoneTextBox.Text != "" || clientAddContactFaxTextBox.Text != "" || clientAddContactEmailTextBox.Text != "")
                {
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Закрити_форму_без_збереження_контакту, AtlantSovt.Properties.Resources.Підтвердження_закриття, MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        public void changeLanguage(ResourceManager rm, CultureInfo ci)
        {
            clientAddContactPersonLabel.Text = rm.GetString("addContactForm", ci);
        }

    }
}
