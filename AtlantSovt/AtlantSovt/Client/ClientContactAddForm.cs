using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
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
                        return "Контакт успішно доданий клієнту [" + New_ClientContact.ClientId + "]\n";
                    }
                    else
                    {
                        MessageBox.Show("Контакт успішно доданий клієнту " + New_ClientContact.ClientId);
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
            if (contactPersonClientContactTextBox.Text != "" || telephoneNumberClientContactTextBox.Text != "" || faxNumberClientContactTextBox.Text != "" || emailClientContactTextBox.Text != "")
            {

                new_ContactPerson = contactPersonClientContactTextBox.Text;
                new_TelephoneNumber = telephoneNumberClientContactTextBox.Text;
                new_FaxNumber = faxNumberClientContactTextBox.Text;
                new_Email = emailClientContactTextBox.Text;

                this.Hide();
                if (Id != 0)
                {
                    AddClientContact(Id, false);
                }
            }
            else
            {
                MessageBox.Show("Для збереження заповніть хочаб одне поле");
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
                if (contactPersonClientContactTextBox.Text != "" || telephoneNumberClientContactTextBox.Text != "" || faxNumberClientContactTextBox.Text != "" || emailClientContactTextBox.Text != "")
                {
                    if (MessageBox.Show("Закрити форму без збереження?\nКонтакт НЕ збережеться.\n Для збереження натисніть <Отмена> та <Додати контакт>", "Підтвердження закриття", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
