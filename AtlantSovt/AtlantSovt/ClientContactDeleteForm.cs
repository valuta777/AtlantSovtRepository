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
                string comboboxText = ClientUpdateSelectDeleteContactComboBox.SelectedItem.ToString();
                string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedIdAndContactPerson[1];

                long id = Convert.ToInt64(comboBoxSelectedId);
                contact = db.ClientContacts.Find(id);
            }
        }
        private void DeleteClientContact()
        {
            using (var db = new AtlantSovtContext())
            {

                if (MessageBox.Show("Видалити контакт " + contact.ContactPerson + "?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.ClientContacts.Attach(contact);
                        db.ClientContacts.Remove(contact);
                        db.SaveChanges();
                        MessageBox.Show("Контакт успішно видалено");
                        ClientUpdateSelectDeleteContactComboBox.Items.Remove(ClientUpdateSelectDeleteContactComboBox.SelectedItem);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка!" + Environment.NewLine + e);
                    }
                }
            }
        }
        private void ClientUpdateSelectDeleteContactComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            ClientUpdateSelectDeleteContactComboBox.Items.Clear();
            LoadContactDeleteContactComboBoxInfo();
            ClientUpdateSelectDeleteContactComboBox.DroppedDown = true;
        }
        private void ClientUpdateSelectDeleteContactComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplitDeleteClientContact();
        }
        private void DeleteClientContactButton_Click(object sender, EventArgs e)
        {
            DeleteClientContact();
        }
    }
}
