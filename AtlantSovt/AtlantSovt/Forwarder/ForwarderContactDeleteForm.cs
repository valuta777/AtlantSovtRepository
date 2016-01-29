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
    public partial class ForwarderContactDeleteForm : Form
    {
        Forwarder forwarder;
        ForwarderContact contact;
        public ForwarderContactDeleteForm()
        {
            InitializeComponent();
        }

        internal void DeleteForwarderContact(Forwarder update_forwarder)
        {
            forwarder = update_forwarder;
        }

        private void LoadContactDeleteForwarderContactComboBoxInfo()
        {
            using (var db = new AtlantSovtContext())
            {

                var query = from fc in db.ForwarderContacts
                            where fc.ForwarderId == forwarder.Id
                            orderby fc.ContactPerson
                            select fc;

                foreach (var item in query)
                {
                    forwarderUpdateSelectDeleteContactComboBox.Items.Add(item.ContactPerson +" "+ item.TelephoneNumber +" "+item.FaxNumber +" "+ item.Email+" [" + item.Id + "]");
                }

            }
        }

        private void SplitDeleteForwarderContact()
        {
            using (var db = new AtlantSovtContext())
            {
                if (forwarderUpdateSelectDeleteContactComboBox.SelectedIndex != -1 && forwarderUpdateSelectDeleteContactComboBox.Text == forwarderUpdateSelectDeleteContactComboBox.SelectedItem.ToString())
                {
                    string comboboxText = forwarderUpdateSelectDeleteContactComboBox.SelectedItem.ToString();
                    string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedIdAndContactPerson[1];

                    long id = Convert.ToInt64(comboBoxSelectedId);
                    contact = db.ForwarderContacts.Find(id);
                }
                else
                {
                    contact = null;
                }
            }
        }

        private void DeleteForwarderContact()
        {
            using (var db = new AtlantSovtContext())
            {
                if (contact != null)
                {
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_контакт + contact.ContactPerson + "?", AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.ForwarderContacts.Attach(contact);
                            db.ForwarderContacts.Remove(contact);
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Контакт_успішно_видалено);
                            forwarderUpdateSelectDeleteContactComboBox.Items.Remove(forwarderUpdateSelectDeleteContactComboBox.SelectedItem);
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_запис);
                }
            }
        }

        private void ForwarderUpdateSelectDeleteContactComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            forwarderUpdateSelectDeleteContactComboBox.Items.Clear();
            LoadContactDeleteForwarderContactComboBoxInfo();
            forwarderUpdateSelectDeleteContactComboBox.DroppedDown = true;
            if (forwarderUpdateSelectDeleteContactComboBox.Items.Count == 0)
            {
                forwarderUpdateContactDeleteButton.Enabled = false;
            }
            else
            {
                forwarderUpdateContactDeleteButton.Enabled = true;

            }
        }

        private void ForwarderUpdateSelectDeleteContactComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplitDeleteForwarderContact();
        }

        private void DeleteForwarderContactButton_Click(object sender, EventArgs e)
        {
            DeleteForwarderContact();
            forwarderUpdateSelectDeleteContactComboBox.Text = "";
            forwarderUpdateSelectDeleteContactComboBox.Items.Clear();
            forwarderUpdateContactDeleteButton.Enabled = false;
            
        }

        private void forwarderUpdateSelectDeleteContactComboBox_TextChanged(object sender, EventArgs e)
        {
            SplitDeleteForwarderContact();
        }
    }
}
