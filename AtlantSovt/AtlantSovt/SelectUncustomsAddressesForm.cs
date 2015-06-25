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
    public partial class SelectUncustomsAddressesForm : Form
    {
        Client client;
        Order order;
        public SelectUncustomsAddressesForm(Client new_client)
        {
            InitializeComponent();
            client = new_client;
        }
        void LoadClientUncustomsAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from address in db.UnCustomsAddresses
                            where address.ClientId == client.Id
                            select address;
                foreach (var item in query)
                {
                    uncustomsAddressesListBox.Items.Add(item.Country.Name + "," +item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                }
            }
        }

        private void unсustomsAddressListBox_DoubleClick(object sender, EventArgs e)
        {
            uncustomsAddressesListBox.Items.Clear();
            LoadClientUncustomsAddresses();
        }

        private void addUncustomsAddressButton_Click(object sender, EventArgs e)
        {
            AddAddressForm addUncustomsAddressForm = new AddAddressForm(client, "UncustomsAddress");
            addUncustomsAddressForm.Show();
        }

        private void addUncustomsAddressToOrderButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        internal void UncustomsAddressesSelect(Order new_order)
        {
            order = new_order;
            SaveUncustomsAddresses();
        }

        private void SaveUncustomsAddresses()
        {
            if (uncustomsAddressesListBox.CheckedItems.Count != 0 && order != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        foreach (var ItemAddress in uncustomsAddressesListBox.CheckedItems)
                        {
                            string fullText = ItemAddress.ToString();
                            string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                            long tempAddressId = Convert.ToInt64(tempAddress[1]);
                            UnCustomsAddress address = db.UnCustomsAddresses.Find(tempAddressId);

                            var new_OrderUncustomsAddress = new OrderUnCustomsAddress
                            {
                                AddressId = address.Id
                            };
                            db.Orders.Find(order.Id).OrderUnCustomsAddresses.Add(new_OrderUncustomsAddress);
                        }

                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано " + uncustomsAddressesListBox.CheckedItems.Count + " Адрес розмитнення");
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
