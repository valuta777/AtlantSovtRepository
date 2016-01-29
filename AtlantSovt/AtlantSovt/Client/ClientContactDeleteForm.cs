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
    public partial class ClientContactDeleteForm : Form
    {
        Client client;
        ClientContact contact;
        public ClientContactDeleteForm()
        {
            InitializeComponent();
        }

        internal void DeleteContact(Client update_client)
        {
            client = update_client;
        }

        private void LoadContactDeleteContactComboBoxInfo()
        {
            using (var db = new AtlantSovtContext())
            {

                var query = from cc in db.ClientContacts
                            where cc.ClientId == client.Id
                            orderby cc.ContactPerson
                            select cc;

                foreach (var item in query)
                {
                    ClientUpdateSelectDeleteContactComboBox.Items.Add(item.ContactPerson +" "+ item.TelephoneNumber +" "+item.FaxNumber +" "+ item.Email+" [" + item.Id + "]");
                }
            }
        }

        private void SplitDeleteClientContact()
        {
            using (var db = new AtlantSovtContext())
            {
                if (ClientUpdateSelectDeleteContactComboBox.SelectedIndex != -1 && ClientUpdateSelectDeleteContactComboBox.Text == ClientUpdateSelectDeleteContactComboBox.SelectedItem.ToString())
                {
                    string comboboxText = ClientUpdateSelectDeleteContactComboBox.SelectedItem.ToString();
                    string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedIdAndContactPerson[1];

                    long id = Convert.ToInt64(comboBoxSelectedId);
                    contact = db.ClientContacts.Find(id);
                }
                else
                {
                    contact = null;
                }
            }
        }

        private void DeleteClientContact()
        {
            using (var db = new AtlantSovtContext())
            {
                if (contact != null)
                {
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_контакт + contact.ContactPerson + "?", AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.ClientContacts.Attach(contact);
                            db.ClientContacts.Remove(contact);
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Контакт_успішно_видалено);
                            ClientUpdateSelectDeleteContactComboBox.Items.Remove(ClientUpdateSelectDeleteContactComboBox.SelectedItem);
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + Environment.NewLine + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_запис);
                }
            }
        }

        private void ClientUpdateSelectDeleteContactComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            ClientUpdateSelectDeleteContactComboBox.Items.Clear();
            LoadContactDeleteContactComboBoxInfo();
            ClientUpdateSelectDeleteContactComboBox.DroppedDown = true;
            if (ClientUpdateSelectDeleteContactComboBox.Items.Count == 0)
            {
                DeleteClientContactButton.Enabled = false;
            }
            else
            {
                DeleteClientContactButton.Enabled = true;

            }
        }

        private void ClientUpdateSelectDeleteContactComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplitDeleteClientContact();
        }

        private void DeleteClientContactButton_Click(object sender, EventArgs e)
        {
            DeleteClientContact();
            ClientUpdateSelectDeleteContactComboBox.Text = "";
            ClientUpdateSelectDeleteContactComboBox.Items.Clear();
            DeleteClientContactButton.Enabled = false;            
        }

        private void ClientUpdateSelectDeleteContactComboBox_TextUpdate(object sender, EventArgs e)
        {
            SplitDeleteClientContact();
        }
    }
}
