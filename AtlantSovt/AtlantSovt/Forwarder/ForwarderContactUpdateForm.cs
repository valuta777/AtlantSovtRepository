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
    public partial class ForwarderContactUpdateForm : Form
    {
        Forwarder forwarder;
        ForwarderContact contact;
        bool contactPersonChanged, telephoneNumberChanged, faxNumberChanged, emailChanged;

        public ForwarderContactUpdateForm()
        {
            InitializeComponent();
        }

        internal void UpdateForwarderContact(Forwarder update_forwarder)
        {
            forwarder = update_forwarder;
        }

        private void LoadContactUpdateForwarderContactComboBoxInfo()
        {
            using (var db = new AtlantSovtContext())
            {

                var query = from fc in db.ForwarderContacts
                            where fc.ForwarderId == forwarder.Id
                            orderby fc.ContactPerson
                            select fc;

                foreach (var item in query)
                {
                    forwarderUpdateContactSelectComboBox.Items.Add(item.ContactPerson + " [" + item.Id + "]");
                }
            }
        }

        private void SplitUpdateForwarderContact()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = forwarderUpdateContactSelectComboBox.SelectedItem.ToString();
                string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedIdAndContactPerson[1];

                long id = Convert.ToInt64(comboBoxSelectedId);
                contact = db.ForwarderContacts.Find(id);

                if (contact != null)
                {
                    contactPersonUpdateForwarderContactTextBox.Text = contact.ContactPerson;
                    telephoneNumberUpdateForwarderContactTextBox.Text = contact.TelephoneNumber;
                    faxNumberUpdateForwarderContactTextBox.Text = contact.FaxNumber;
                    emailUpdateForwarderContactTextBox.Text = contact.Email;
                }
                contactPersonChanged = telephoneNumberChanged = faxNumberChanged = emailChanged = false;
            }
        }

        private void UpdateForwarderContact()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (contactPersonChanged || telephoneNumberChanged || faxNumberChanged || emailChanged)
                {
                    if (contactPersonChanged)
                    {
                        contact.ContactPerson = contactPersonUpdateForwarderContactTextBox.Text;
                    }
                    if (telephoneNumberChanged)
                    {
                        contact.TelephoneNumber = telephoneNumberUpdateForwarderContactTextBox.Text;
                    }
                    if (faxNumberChanged)
                    {
                        contact.FaxNumber = faxNumberUpdateForwarderContactTextBox.Text;
                    }
                    if (emailChanged)
                    {
                        contact.Email = emailUpdateForwarderContactTextBox.Text;
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
            UpdateForwarderContact();
        }

        private void clientUpdateContactSelectComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            forwarderUpdateContactSelectComboBox.Items.Clear();
            LoadContactUpdateForwarderContactComboBoxInfo();
            forwarderUpdateContactSelectComboBox.DroppedDown = true;
        }

        private void clientUpdateContactSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllBoxesForwarderContactUpdate();
            SplitUpdateForwarderContact();
        }

        private void ClearAllBoxesForwarderContactUpdate()
        {
            contactPersonUpdateForwarderContactTextBox.Clear();
            telephoneNumberUpdateForwarderContactTextBox.Clear();
            faxNumberUpdateForwarderContactTextBox.Clear();
            emailUpdateForwarderContactTextBox.Clear();
        }
    }
}
