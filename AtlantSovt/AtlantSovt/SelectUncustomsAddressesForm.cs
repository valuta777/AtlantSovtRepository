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
    public partial class SelectUncustomsAddressesForm : Form
    {
        Client client;
        Order order;
        bool IsUpdate;
        public SelectUncustomsAddressesForm(Client new_client)
        {
            InitializeComponent();
            client = new_client;
            IsUpdate = false;
        }
        public SelectUncustomsAddressesForm(Client new_client, Order new_order)
        {
            InitializeComponent();
            client = new_client;
            order = new_order;
            IsUpdate = true;
        }

        private void CheckSelectedUncustomsAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var getAddreses = from DA in db.Orders.Find(order.Id).OrderUnCustomsAddresses
                                  orderby DA.UnCustomsAddress.Id
                                  select DA.UnCustomsAddress.Id;

                List<long> GetAddresses = getAddreses.ToList();
                List<int> index = new List<int>();

                if (GetAddresses.Count != 0)
                {
                    foreach (var ItemAddress in uncustomsAddressesListBox.Items)
                    {
                        string fullText = ItemAddress.ToString();
                        string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                        long tempAddressId = Convert.ToInt64(tempAddress[1]);

                        if (GetAddresses.Contains(tempAddressId))
                        {
                            index.Add(uncustomsAddressesListBox.Items.IndexOf(ItemAddress));
                        }
                    }
                    foreach (int i in index)
                    {
                        uncustomsAddressesListBox.SetItemChecked(i, true);
                    }
                    uncustomsAddressesListBox.Update();
                }
            }
        }

        private void addUncustomsAddressToOrderButton_Click(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                UpdateUncustomsAddresses();
                this.Dispose();
            }
            else
            {
                this.Hide();
            }

        }

        private void UpdateUncustomsAddresses()
        {
            if (order != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        var getAddreses = from address in db.Orders.Find(order.Id).OrderUnCustomsAddresses
                                          select address.AddressId;

                        List<long> GetAddreses = getAddreses.ToList();
                        List<long> getCheked = new List<long>();
                        foreach (var ItemAddress in uncustomsAddressesListBox.CheckedItems)
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
                                    UnCustomsAddress Address = db.UnCustomsAddresses.Find(deleteItem);

                                    OrderUnCustomsAddress OrderAddress = db.Orders.Find(order.Id).OrderUnCustomsAddresses.Where(Oda => Oda.AddressId == Address.Id).FirstOrDefault();

                                    db.OrderUnCustomsAddresses.Remove(OrderAddress);
                                }
                                MessageBox.Show("Успішно видалено " + deletedAdresses.Count + " Адрес розмитнення");
                            }

                            if (newAddreses.Count != 0)
                            {
                                foreach (var newItem in newAddreses)
                                {
                                    UnCustomsAddress Address = db.UnCustomsAddresses.Find(newItem);

                                    OrderUnCustomsAddress new_OrderAddress = new OrderUnCustomsAddress
                                    {
                                        AddressId = Address.Id
                                    };
                                    db.Orders.Find(order.Id).OrderUnCustomsAddresses.Add(new_OrderAddress);
                                }
                                MessageBox.Show("Успішно додано " + newAddreses.Count + " Адрес розмитнення");
                            }
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: ", ex.ToString());
                }
            }
        }

        public void LoadClientUncustomsAddresses()
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
        public void LoadClientUncustomsAddresses(long? id)
        {
            if (id.HasValue)
            { 
                using (var db = new AtlantSovtContext())
                {
                    var query = from address in db.UnCustomsAddresses
                                where address.Id == id.Value
                                select address;
                    foreach (var item in query)
                    {
                        uncustomsAddressesListBox.Items.Add(item.Country.Name + "," + item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                    }
                }
            }
        }

        private void unсustomsAddressListBox_DoubleClick(object sender, EventArgs e)
        {
            uncustomsAddressesListBox.Items.Clear();
            LoadClientUncustomsAddresses();
            if (order != null)
            {
                CheckSelectedUncustomsAddresses();
            }
        }

        private void addUncustomsAddressButton_Click(object sender, EventArgs e)
        {
            AddAddressForm addUncustomsAddressForm = new AddAddressForm(client, 4, this);
            addUncustomsAddressForm.Show();
        }

        internal string UncustomsAddressesSelect(Order new_order)
        {
            order = new_order;
            return SaveUncustomsAddresses();
        }

        private string SaveUncustomsAddresses()
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
                        return "Успішно вибрано " + uncustomsAddressesListBox.CheckedItems.Count + " Адрес розмитнення\n";
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка:", ex.ToString());
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private void SelectUncustomsAddressesForm_Load(object sender, EventArgs e)
        {
            LoadClientUncustomsAddresses();
            if (order != null)
            {
                CheckSelectedUncustomsAddresses();
            }
        }

    }
}
