using AtlantSovt.Additions;
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
        bool IsUpdate;
        public SelectCustomsAddressesForm(Client new_client)
        {
            InitializeComponent();
            client = new_client;
            IsUpdate = false;
        }
        public SelectCustomsAddressesForm(Client new_client, Order new_order)
        {
            InitializeComponent();
            client = new_client;
            order = new_order;
            LoadClientCustomsAddresses();
            CheckSelectedCustomsAddresses();
            IsUpdate = true;
        }

        private void CheckSelectedCustomsAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var getAddreses = from DA in db.Orders.Find(order.Id).OrderCustomsAddresses
                                  orderby DA.CustomsAddress.Id
                                  select DA.CustomsAddress.Id;

                List<long> GetAddresses = getAddreses.ToList();
                List<int> index = new List<int>();

                if (GetAddresses.Count != 0)
                {
                    foreach (var ItemAddress in customsAddressesListBox.Items)
                    {
                        string fullText = ItemAddress.ToString();
                        string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                        long tempAddressId = Convert.ToInt64(tempAddress[1]);

                        if (GetAddresses.Contains(tempAddressId))
                        {
                            index.Add(customsAddressesListBox.Items.IndexOf(ItemAddress));
                        }
                    }
                    foreach (int i in index)
                    {
                        customsAddressesListBox.SetItemChecked(i, true);
                    }
                    customsAddressesListBox.Update();
                }
            }
        }

        private void addCustomsAddressToOrderButton_Click(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                UpdateCustomsAddresses();
                this.Dispose();
            }
            else
            {
                this.Hide();
            }

        }

        private void UpdateCustomsAddresses()
        {
            if (order != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        var getAddreses = from address in db.Orders.Find(order.Id).OrderCustomsAddresses
                                          select address.AddressId;

                        List<long> GetAddreses = getAddreses.ToList();
                        List<long> getCheked = new List<long>();
                        foreach (var ItemAddress in customsAddressesListBox.CheckedItems)
                        {
                            string fullText = ItemAddress.ToString();
                            string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                            long tempAddressId = Convert.ToInt64(tempAddress[1]);
                            getCheked.Add(tempAddressId);
                        }
                        List<long> newAddreses = getCheked.Except(GetAddreses).ToList();
                        List<long> deletedAdresses = GetAddreses.Except(getCheked).ToList();

                        if (deletedAdresses.Count != 0 || newAddreses.Count != 0)
                        {
                            if (deletedAdresses.Count != 0)
                            {
                                foreach (var deleteItem in deletedAdresses)
                                {
                                    CustomsAddress Address = db.CustomsAddresses.Find(deleteItem);

                                    OrderCustomsAddress OrderAddress = db.Orders.Find(order.Id).OrderCustomsAddresses.Where(Oca => Oca.AddressId == Address.Id).FirstOrDefault();

                                    db.OrderCustomsAddresses.Remove(OrderAddress);
                                }
                                MessageBox.Show("Успішно видалено " + deletedAdresses.Count + " Адрес замитнення");
                            }

                            if (newAddreses.Count != 0)
                            {
                                foreach (var newItem in newAddreses)
                                {
                                    CustomsAddress Address = db.CustomsAddresses.Find(newItem);

                                    OrderCustomsAddress new_OrderAddress = new OrderCustomsAddress
                                    {
                                        AddressId = Address.Id
                                    };
                                    db.Orders.Find(order.Id).OrderCustomsAddresses.Add(new_OrderAddress);
                                }
                                MessageBox.Show("Успішно додано " + newAddreses.Count + " Адрес замитнення");
                            }
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: ", ex.Message);
                }
            }
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
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: ", ex.Message);
                }
            }
        }

        private void SelectCustomsAddressesForm_Load(object sender, EventArgs e)
        {
            LoadClientCustomsAddresses();
        }

    }
}
