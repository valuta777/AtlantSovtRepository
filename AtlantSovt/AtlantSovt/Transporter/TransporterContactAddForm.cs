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
    public partial class TransporterContactAddForm : Form
    {
        string new_ContactPerson;
        string new_TelephoneNumber;
        string new_FaxNumber;
        string new_Email;
        long Id;
        bool IsAdding;


        public TransporterContactAddForm()
        {
            InitializeComponent();
            long Id = 0;
            IsAdding = true;
        }

        internal string AddTransporterContact(long id , bool IsAddingnew)
        {
            using (var db = new AtlantSovtContext())
            {
                var New_TransporterContact = new TransporterContact
                {
                    TransporterId = id,
                    ContactPerson = new_ContactPerson,
                    TelephoneNumber = new_TelephoneNumber,
                    FaxNumber = new_FaxNumber,
                    Email = new_Email,
                };
                try
                {

                    db.TransporterContacts.Add(New_TransporterContact);
                    db.SaveChanges();
                    IsAdding = IsAddingnew;
                    if (IsAdding)
                    {
                        return "Контакт успішно доданий перевізнику [" + New_TransporterContact.TransporterId + "]\n";
                    }
                    else
                    {
                        MessageBox.Show("Контакт успішно доданий перевізнику " + New_TransporterContact.TransporterId);
                        return string.Empty;
                    }
                }
                catch (Exception ec)
                {
                    Log.Write(ec);
                    MessageBox.Show(ec.Message);
                    return string.Empty;
                }
            }
        }

        private void addContactTransporterButton_Click(object sender, EventArgs e)
        {
            if (contactPersonTransporterContactTextBox.Text != "" || telephoneNumberTransporterContactTextBox.Text != "" || faxNumberTransporterContactTextBox.Text != "" || emailTransporterContactTextBox.Text != "")
            {
                new_ContactPerson = contactPersonTransporterContactTextBox.Text;
                new_TelephoneNumber = telephoneNumberTransporterContactTextBox.Text;
                new_FaxNumber = faxNumberTransporterContactTextBox.Text;
                new_Email = emailTransporterContactTextBox.Text;

                this.Hide();
                if (Id != 0)
                {
                    AddTransporterContact(Id, false);
                }
            }
            else
            {
                MessageBox.Show("Для збереження заповніть хочаб одне поле");
            }
        }
        internal void AddTransporterContact2(long id) 
        {
            Id = id;
        }

        private void TransporterContactAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAdding)
            {
                if (contactPersonTransporterContactTextBox.Text != "" || telephoneNumberTransporterContactTextBox.Text != "" || faxNumberTransporterContactTextBox.Text != "" || emailTransporterContactTextBox.Text != "")
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
