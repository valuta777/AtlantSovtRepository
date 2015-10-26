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
    public partial class TransporterContactDeleteForm : Form
    {
        Transporter transporter;
        TransporterContact contact;
        public TransporterContactDeleteForm()
        {
            InitializeComponent();
        }

        internal void DeleteTransporterContact(Transporter update_transporter)
        {
            transporter = update_transporter;
        }

        private void LoadContactDeleteTransporterContactComboBoxInfo()
        {
            using (var db = new AtlantSovtContext())
            {

                var query = from tc in db.TransporterContacts
                            where tc.TransporterId == transporter.Id
                            orderby tc.ContactPerson
                            select tc;

                foreach (var item in query)
                {
                    transporterUpdateSelectDeleteContactComboBox.Items.Add(item.ContactPerson +" "+ item.TelephoneNumber +" "+item.FaxNumber +" "+ item.Email+" [" + item.Id + "]");
                }

            }
        }

        private void SplitDeleteTransporterContact()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = transporterUpdateSelectDeleteContactComboBox.SelectedItem.ToString();
                string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedIdAndContactPerson[1];

                long id = Convert.ToInt64(comboBoxSelectedId);
                contact = db.TransporterContacts.Find(id);
            }
        }

        private void DeleteTransporterContact()
        {
            using (var db = new AtlantSovtContext())
            {

                if (MessageBox.Show("Видалити контакт " + contact.ContactPerson + "?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.TransporterContacts.Attach(contact);
                        db.TransporterContacts.Remove(contact);
                        db.SaveChanges();
                        MessageBox.Show("Контакт успішно видалено");
                        transporterUpdateSelectDeleteContactComboBox.Items.Remove(transporterUpdateSelectDeleteContactComboBox.SelectedItem);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка!" + Environment.NewLine + e);
                    }
                }
            }
        }

        private void TransporterUpdateSelectDeleteContactComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            transporterUpdateSelectDeleteContactComboBox.Items.Clear();
            LoadContactDeleteTransporterContactComboBoxInfo();
            transporterUpdateSelectDeleteContactComboBox.DroppedDown = true;
            if (transporterUpdateSelectDeleteContactComboBox.Items.Count == 0)
            {
                transporterUpdateContactDeleteButton.Enabled = false;
            }
            else
            {
                transporterUpdateContactDeleteButton.Enabled = true;
            }
        }

        private void TransporterUpdateSelectDeleteContactComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplitDeleteTransporterContact();
        }

        private void DeleteTransporterContactButton_Click(object sender, EventArgs e)
        {
            transporterUpdateSelectDeleteContactComboBox.Text = "";
            transporterUpdateSelectDeleteContactComboBox.Items.Clear();
            transporterUpdateContactDeleteButton.Enabled = false;
            DeleteTransporterContact();
        }
    }
}
