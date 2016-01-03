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
    public partial class SelectDownloadAddressesForm : Form
    {
        Client client;
        Order order;
        bool IsUpdate;
        public SelectDownloadAddressesForm(Client new_client)
        {
            InitializeComponent();
            client = new_client;            
            LoadClientDownloadAddresses();
            IsUpdate = false;
        }
        public SelectDownloadAddressesForm(Client new_client, Order new_order)
        {
            InitializeComponent();
            client = new_client;
            order = new_order;
            LoadClientDownloadAddresses();
            CheckSelectedDownloadAddresses();
            IsUpdate = true;
        }

        private void CheckSelectedDownloadAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var getAddreses = from DA in db.Orders.Find(order.Id).OrderDownloadAddresses
                                  orderby DA.DownloadAddress.Id
                                  select DA.DownloadAddress.Id;

                List<long> GetAddresses = getAddreses.ToList();
                List<int> index = new List<int>();

                if (GetAddresses.Count != 0)
                {
                    foreach (var ItemAddress in downloadAddresssListBox.Items)
                    {
                        string fullText = ItemAddress.ToString();
                        string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                        long tempAddressId = Convert.ToInt64(tempAddress[1]);

                        if (GetAddresses.Contains(tempAddressId))
                        {
                            index.Add(downloadAddresssListBox.Items.IndexOf(ItemAddress));
                        }
                    }
                    foreach (int i in index)
                    {
                        downloadAddresssListBox.SetItemChecked(i, true);
                    }
                    downloadAddresssListBox.Update();
                }
            }
        }

        private void addDownloadAddressToOrderButton_Click(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                UpdateDownloadAddresses();
                this.Dispose();
            }
            else
            {
                this.Hide();
            }

        }

        private void UpdateDownloadAddresses()
        {
            if (order != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        var getAddreses = from address in db.Orders.Find(order.Id).OrderDownloadAddresses
                                          select address.AddressId;

                        List<long> GetAddreses = getAddreses.ToList();
                        List<long> getCheked = new List<long>();
                        foreach (var ItemAddress in downloadAddresssListBox.CheckedItems)
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
                                    DownloadAddress Address = db.DownloadAddresses.Find(deleteItem);

                                    OrderDownloadAddress OrderAddress = db.Orders.Find(order.Id).OrderDownloadAddresses.Where(Oda => Oda.AddressId == Address.Id).FirstOrDefault();

                                    db.OrderDownloadAddresses.Remove(OrderAddress);
                                }
                                MessageBox.Show("Успішно видалено " + deletedAdresses.Count + " Адрес завантаження");
                            }

                            if (newAddreses.Count != 0)
                            {
                                foreach (var newItem in newAddreses)
                                {
                                    DownloadAddress Address = db.DownloadAddresses.Find(newItem);

                                    OrderDownloadAddress new_OrderAddress = new OrderDownloadAddress
                                    {
                                        AddressId = Address.Id
                                    };
                                    db.Orders.Find(order.Id).OrderDownloadAddresses.Add(new_OrderAddress);
                                }
                                MessageBox.Show("Успішно додано " + newAddreses.Count + " Адрес завантаження");
                            }
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка:", ex.ToString());
                }
            }
        }

        public void LoadClientDownloadAddresses()
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
            AddAddressForm addDownloadAddressForm = new AddAddressForm(client, 1, this);
            addDownloadAddressForm.Show();
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
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка!!", ex.ToString());
                }
            }
        }
    }
}
