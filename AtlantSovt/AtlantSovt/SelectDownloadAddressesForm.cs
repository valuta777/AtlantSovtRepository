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
    public partial class SelectDownloadAddressesForm : Form
    {
        Client client;
        Order order;
        public SelectDownloadAddressesForm(Client new_client)
        {
            InitializeComponent();
            client = new_client;
        }
        void LoadClientDownloadAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from address in db.DownloadAddresses
                            where address.ClientId == client.Id
                            select address;
                foreach (var item in query)
                {
                    downloadAddresssListBox.Items.Add(item.Country.Name + "," +item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                }
            }
        }

        private void downloadAddressListBox_DoubleClick(object sender, EventArgs e)
        {
            downloadAddresssListBox.Items.Clear();
            LoadClientDownloadAddresses();
        }

        private void addDownloadAddressButton_Click(object sender, EventArgs e)
        {
            AddAddressForm addDownloadAddressForm = new AddAddressForm(client, 1);
            addDownloadAddressForm.Show();
        }

        private void addDownloadAddressToOrderButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        internal void DownloadAddressesSelect(Order new_order)
        {
            order = new_order;
            SaveDownloadAddresses();
        }

        private void SaveDownloadAddresses()
        {
            if (downloadAddresssListBox.CheckedItems.Count != 0 && order != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        foreach (var ItemAddress in downloadAddresssListBox.CheckedItems)
                        {
                            string fullText = ItemAddress.ToString();
                            string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                            long tempAddressId = Convert.ToInt64(tempAddress[1]);
                            DownloadAddress address = db.DownloadAddresses.Find(tempAddressId);

                            var new_OrderDownloadAddress = new OrderDownloadAddress
                            {
                                AddressId = address.Id
                            };
                            db.Orders.Find(order.Id).OrderDownloadAddresses.Add(new_OrderDownloadAddress);
                        }
                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано " + downloadAddresssListBox.CheckedItems.Count + " Адрес розвантаження");
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
