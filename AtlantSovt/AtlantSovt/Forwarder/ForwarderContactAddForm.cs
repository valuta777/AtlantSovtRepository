using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    public partial class ForwarderContactAddForm : Form
    {
        string new_ContactPerson;
        string new_TelephoneNumber;
        string new_FaxNumber;
        string new_Email;
        long Id;
        bool IsAdding;

        public ForwarderContactAddForm()
        {
            InitializeComponent();
            long Id = 0;
            IsAdding = true;
        }

        internal string AddForwarderContact(long id , bool IsAddingnew)
        {
            using (var db = new AtlantSovtContext())
            {
                var New_ForwarderContact = new ForwarderContact
                {
                    ForwarderId = id,
                    ContactPerson = new_ContactPerson,
                    TelephoneNumber = new_TelephoneNumber,
                    FaxNumber = new_FaxNumber,
                    Email = new_Email,
                };
                try
                {
                    db.ForwarderContacts.Add(New_ForwarderContact);
                    db.SaveChanges();
                    IsAdding = IsAddingnew;
                    if (IsAdding)
                    {
                        return "Контакт успішно доданий експедитору [" + New_ForwarderContact.Id + "]\n";
                    }
                    else
                    {
                        MessageBox.Show("Контакт успішно доданий експедитору ");
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

        private void addContactForwarderButton_Click(object sender, EventArgs e)
        {
            if (contactPersonForwarderContactTextBox.Text != "" || telephoneNumberForwarderContactTextBox.Text != "" || faxNumberForwarderContactTextBox.Text != "" || emailForwarderContactTextBox.Text != "")
            {
                new_ContactPerson = contactPersonForwarderContactTextBox.Text;
                new_TelephoneNumber = telephoneNumberForwarderContactTextBox.Text;
                new_FaxNumber = faxNumberForwarderContactTextBox.Text;
                new_Email = emailForwarderContactTextBox.Text;

                this.Hide();
                if (Id != 0)
                {
                    AddForwarderContact(Id, false);
                }
            }
            else
            {
                MessageBox.Show("Для збереження заповніть хочаб одне поле");
            }
        }
       internal void AddForwarderContact2(long id) 
        {
            Id = id;
        }

        private void ForwarderContactAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAdding)
            {
                if (contactPersonForwarderContactTextBox.Text != "" || telephoneNumberForwarderContactTextBox.Text != "" || faxNumberForwarderContactTextBox.Text != "" || emailForwarderContactTextBox.Text != "")
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

