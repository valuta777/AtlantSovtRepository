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
    public partial class ClientContactUpdateForm : Form
    {
        Client client;
        ClientContact contact;
        bool contactPersonChanged, telephoneNumberChanged, faxNumberChanged, emailChanged;

        public ClientContactUpdateForm()
        {
            InitializeComponent();
        }

        internal void UpdateContact(Client update_client)
        {
            client = update_client;
        }

        private void LoadContactUpdateContactComboBoxInfo()
        {
            using (var db = new AtlantSovtContext())
            {

                var query = from cc in db.ClientContacts
                            where cc.ClientId == client.Id
                            orderby cc.ContactPerson
                            select cc;

                foreach (var item in query)
                {
                    clientUpdateContactSelectComboBox.Items.Add(item.ContactPerson + " [" + item.Id + "]");
                }

            }
        }

        private void SplitUpdateClientContact()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = clientUpdateContactSelectComboBox.SelectedItem.ToString();
                string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedIdAndContactPerson[1];

                long id = Convert.ToInt64(comboBoxSelectedId);
                contact = db.ClientContacts.Find(id);

                if (contact != null)
                {
                    contactPersonUpdateClientContactTextBox.Text = contact.ContactPerson;
                    telephoneNumberUpdateClientContactTextBox.Text = contact.TelephoneNumber;
                    faxNumberUpdateClientContactTextBox.Text = contact.FaxNumber;
                    emailUpdateClientContactTextBox.Text = contact.Email;
                }
                contactPersonChanged = telephoneNumberChanged = faxNumberChanged = emailChanged = false;
            }
        }

        private void UpdateClientContact()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (contactPersonChanged || telephoneNumberChanged || faxNumberChanged || emailChanged)
                {
                    if (contactPersonChanged)
                    {
                        contact.ContactPerson = contactPersonUpdateClientContactTextBox.Text;
                    }
                    if (telephoneNumberChanged)
                    {
                        contact.TelephoneNumber = telephoneNumberUpdateClientContactTextBox.Text;
                    }
                    if (faxNumberChanged)
                    {
                        contact.FaxNumber = faxNumberUpdateClientContactTextBox.Text;
                    }
                    if (emailChanged)
                    {
                        contact.Email = emailUpdateClientContactTextBox.Text;
                    }
                    db.Entry(contact).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_змінено);
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Змін_не_знайдено);
                }
            }
        }

        private void contactPersonUpdateClientContactTextBox_TextChanged(object sender, EventArgs e)
        {
            contactPersonChanged = true;
        }

        private void telephoneNumberUpdateClientContactTextBox_TextChanged(object sender, EventArgs e)
        {
            telephoneNumberChanged = true;
        }

        private void faxNumberUpdateClientContactTextBox_TextChanged(object sender, EventArgs e)
        {
            faxNumberChanged = true;
        }

        private void emailUpdateClientContactTextBox_TextChanged(object sender, EventArgs e)
        {
            emailChanged = true;
        }

        private void updateContactButton_Click(object sender, EventArgs e)
        {
            UpdateClientContact();
        }

        private void clientUpdateContactSelectComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            clientUpdateContactSelectComboBox.Items.Clear();
            LoadContactUpdateContactComboBoxInfo();
            clientUpdateContactSelectComboBox.DroppedDown = true;
        }

        private void clientUpdateContactSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllBoxesClientContactUpdate();
            SplitUpdateClientContact();
        }

        private void ClearAllBoxesClientContactUpdate()
        {
            contactPersonUpdateClientContactTextBox.Clear();
            telephoneNumberUpdateClientContactTextBox.Clear();
            faxNumberUpdateClientContactTextBox.Clear();
            emailUpdateClientContactTextBox.Clear();
        }


    }
}
