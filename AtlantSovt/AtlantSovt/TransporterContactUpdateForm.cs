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
    public partial class TransporterContactUpdateForm : Form
    {
        Transporter transporter;
        TransporterContact contact;
        bool contactPersonChanged, telephoneNumberChanged, faxNumberChanged, emailChanged;

        public TransporterContactUpdateForm()
        {
            InitializeComponent();
        }

        internal void UpdateForwarderContact(Transporter update_transporter)
        {
            transporter = update_transporter;
        }

        private void LoadContactUpdateTransporterContactComboBoxInfo()
        {
            using (var db = new AtlantSovtContext())
            {

                var query = from tc in db.TransporterContacts
                            where tc.TransporterId == transporter.Id
                            orderby tc.ContactPerson
                            select tc;

                foreach (var item in query)
                {
                    transporterUpdateContactSelectComboBox.Items.Add(item.ContactPerson + " [" + item.Id + "]");
                }
            }
        }

        private void SplitUpdateTransporterContact()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = transporterUpdateContactSelectComboBox.SelectedItem.ToString();
                string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedIdAndContactPerson[1];

                long id = Convert.ToInt64(comboBoxSelectedId);
                contact = db.TransporterContacts.Find(id);

                if (contact != null)
                {
                    contactPersonUpdateTransporterContactTextBox.Text = contact.ContactPerson;
                    telephoneNumberUpdateTransporterContactTextBox.Text = contact.TelephoneNumber;
                    faxNumberUpdateTransporterContactTextBox.Text = contact.FaxNumber;
                    emailUpdateTransporterContactTextBox.Text = contact.Email;
                }
                contactPersonChanged = telephoneNumberChanged = faxNumberChanged = emailChanged = false;
            }
        }

        private void UpdateTransporterContact()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (contactPersonChanged || telephoneNumberChanged || faxNumberChanged || emailChanged)
                {
                    if (contactPersonChanged)
                    {
                        contact.ContactPerson = contactPersonUpdateTransporterContactTextBox.Text;
                    }
                    if (telephoneNumberChanged)
                    {
                        contact.TelephoneNumber = telephoneNumberUpdateTransporterContactTextBox.Text;
                    }
                    if (faxNumberChanged)
                    {
                        contact.FaxNumber = faxNumberUpdateTransporterContactTextBox.Text;
                    }
                    if (emailChanged)
                    {
                        contact.Email = emailUpdateTransporterContactTextBox.Text;
                    }
                    db.Entry(contact).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Успішно змінено");
                }
                else
                {
                    MessageBox.Show("Змін не знайдено");
                }
            }
        }

        private void contactPersonUpdateTransporterContactTextBox_TextChanged(object sender, EventArgs e)
        {
            contactPersonChanged = true;
        }

        private void telephoneNumberUpdateTransporterContactTextBox_TextChanged(object sender, EventArgs e)
        {
            telephoneNumberChanged = true;
        }

        private void faxNumberUpdateTransporterContactTextBox_TextChanged(object sender, EventArgs e)
        {
            faxNumberChanged = true;
        }

        private void emailUpdateTransporterContactTextBox_TextChanged(object sender, EventArgs e)
        {
            emailChanged = true;
        }

        private void updateTransporterContactButton_Click(object sender, EventArgs e)
        {
            UpdateTransporterContact();
        }

        private void transporterUpdateContactSelectComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            transporterUpdateContactSelectComboBox.Items.Clear();
            LoadContactUpdateTransporterContactComboBoxInfo();
            transporterUpdateContactSelectComboBox.DroppedDown = true;
        }

        private void transporterUpdateContactSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllBoxesTransporterContactUpdate();
            SplitUpdateTransporterContact();
        }

        private void ClearAllBoxesTransporterContactUpdate()
        {
            contactPersonUpdateTransporterContactTextBox.Clear();
            telephoneNumberUpdateTransporterContactTextBox.Clear();
            faxNumberUpdateTransporterContactTextBox.Clear();
            emailUpdateTransporterContactTextBox.Clear();
        }
    }
}
