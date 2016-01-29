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
                if (transporterUpdateSelectDeleteContactComboBox.SelectedIndex != -1 && transporterUpdateSelectDeleteContactComboBox.Text == transporterUpdateSelectDeleteContactComboBox.SelectedItem.ToString())
                {
                    string comboboxText = transporterUpdateSelectDeleteContactComboBox.SelectedItem.ToString();
                    string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedIdAndContactPerson[1];

                    long id = Convert.ToInt64(comboBoxSelectedId);
                    contact = db.TransporterContacts.Find(id);
                }
                else
                {
                    contact = null;
                }
            }
        }

        private void DeleteTransporterContact()
        {
            using (var db = new AtlantSovtContext())
            {
                if (contact != null)
                {
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_контакт + contact.ContactPerson + "?", AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.TransporterContacts.Attach(contact);
                            db.TransporterContacts.Remove(contact);
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Контакт_успішно_видалено);
                            transporterUpdateSelectDeleteContactComboBox.Items.Remove(transporterUpdateSelectDeleteContactComboBox.SelectedItem);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + e.Message);
                        }
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
            DeleteTransporterContact();
            transporterUpdateSelectDeleteContactComboBox.Text = "";
            transporterUpdateSelectDeleteContactComboBox.Items.Clear();
            transporterUpdateContactDeleteButton.Enabled = false;
            
        }

        private void transporterUpdateSelectDeleteContactComboBox_TextChanged(object sender, EventArgs e)
        {
            SplitDeleteTransporterContact();
        }
    }
}
