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
                        this.Dispose();
                        return AtlantSovt.Properties.Resources.Контакт_успішно_доданий_експедитору + "[" + New_ForwarderContact.Id + @"]";
                    }
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Контакт_успішно_доданий_експедитору);
                        this.Dispose();
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
                MessageBox.Show(AtlantSovt.Properties.Resources.Для_збереження_заповніть_хоча_б_одне_поле);
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
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Закрити_форму_без_збереження_контакту, AtlantSovt.Properties.Resources.Підтвердження_закриття, MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}

