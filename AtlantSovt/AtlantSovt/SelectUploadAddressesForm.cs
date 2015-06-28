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
    public partial class SelectUploadAddressesForm : Form
    {
        Client client;
        Order order;
        public SelectUploadAddressesForm(Client new_client)
        {
            InitializeComponent();
            client = new_client;
        }
        void LoadClientUploadAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from address in db.UploadAddresses
                            where address.ClientId == client.Id
                            select address;
                foreach (var item in query)
                {
                    uploadAddressListBox.Items.Add(item.Country.Name + "," +item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                }
            }
        }

        private void uploadAddressListBox_DoubleClick(object sender, EventArgs e)
        {
            uploadAddressListBox.Items.Clear();
            LoadClientUploadAddresses();
        }

        private void addUploadAddressButton_Click(object sender, EventArgs e)
        {
            AddAddressForm addAddressForm = new AddAddressForm(client,2);
            addAddressForm.Show();
        }

        private void addUploadAdressesToOrderButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        internal void UploadAddressesSelect(Order new_order)
        {
            order = new_order;
            SaveUploadAdresses();
        }

        private void SaveUploadAdresses()
        {
            if (uploadAddressListBox.CheckedItems.Count != 0 && order != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        foreach (var ItemAddress in uploadAddressListBox.CheckedItems)
                        {
                            string fullText = ItemAddress.ToString();
                            string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                            long tempAddressId = Convert.ToInt64(tempAddress[1]);
                            UploadAddress address = db.UploadAddresses.Find(tempAddressId);

                            var new_OrderUploadAdress = new OrderUploadAdress
                            {
                                AddressId = address.Id
                            };
                            db.Orders.Find(order.Id).OrderUploadAdresses.Add(new_OrderUploadAdress);
                        }
                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано " + uploadAddressListBox.CheckedItems.Count + " Адрес завантаження");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Помилка!!", e.ToString());
                }
            }
        }

    }
}
