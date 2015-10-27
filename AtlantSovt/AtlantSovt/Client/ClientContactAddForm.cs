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
        long Id;

        public ClientContactAddForm()
        {
            InitializeComponent();
            long Id = 0;
        }

        internal void AddClientContact(long id)
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
                    MessageBox.Show("Контакт успішно доданий клієнту " + New_ClientContact.ClientId);
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void addContactClientButton_Click(object sender, EventArgs e)
        {

            new_ContactPerson = contactPersonClientContactTextBox.Text;
            new_TelephoneNumber = telephoneNumberClientContactTextBox.Text;
            new_FaxNumber = faxNumberClientContactTextBox.Text;
            new_Email = emailClientContactTextBox.Text;
            
            this.Hide();
            if (Id != 0)
            {
                AddClientContact(Id);
            }
        }
       internal void AddClientContact2(long id) 
        {
            Id = id;
        }
    }
}
