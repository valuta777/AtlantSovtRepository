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

        public TransporterContactAddForm()
        {
            InitializeComponent();
        }

        internal string AddTransporterContact(long id , bool IsAdding)
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
        internal void AddTransporterContact2(long id) 
        {
            Id = id;
        }
    }
}
