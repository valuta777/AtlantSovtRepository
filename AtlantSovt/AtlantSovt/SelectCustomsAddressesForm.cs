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
    public partial class SelectCustomsAddressesForm : Form
    {
        Client client;
        Order order;
        public SelectCustomsAddressesForm(Client new_client)
        {
            InitializeComponent();
            client = new_client;
        }
        void LoadClientCustomsAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from address in db.CustomsAddresses
                            where address.ClientId == client.Id
                            select address;
                foreach (var item in query)
                {
                    customsAddressesListBox.Items.Add(item.Country.Name + "," +item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                }
            }
        }

        private void сustomsAddressListBox_DoubleClick(object sender, EventArgs e)
        {
            customsAddressesListBox.Items.Clear();
            LoadClientCustomsAddresses();
        }

        private void addCustomsAddressButton_Click(object sender, EventArgs e)
        {
            AddAddressForm addCustomsAddressForm = new AddAddressForm(client, 3);
            addCustomsAddressForm.Show();
        }

        private void addCustomsAddressToOrderButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        internal void CustomsAddressesSelect(Order new_order)
        {
            order = new_order;
            SaveCustomsAddresses();
        }

        private void SaveCustomsAddresses()
        {
            if (customsAddressesListBox.CheckedItems.Count != 0 && order != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        foreach (var ItemAddress in customsAddressesListBox.CheckedItems)
                        {                            
                            string fullText = ItemAddress.ToString();
                            string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                            long tempAddressId = Convert.ToInt64(tempAddress[1]);
                            CustomsAddress address = db.CustomsAddresses.Find(tempAddressId);

                            var new_OrderCustomsAddress = new OrderCustomsAddress
                            {
                                AddressId = address.Id
                            };
                            db.Orders.Find(order.Id).OrderCustomsAddresses.Add(new_OrderCustomsAddress);
                        }

                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано " + customsAddressesListBox.CheckedItems.Count + " Адрес замитнення");
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
