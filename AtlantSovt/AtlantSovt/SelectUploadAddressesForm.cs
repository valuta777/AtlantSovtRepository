﻿using AtlantSovt.Additions;
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
        bool IsUpdate;
        AddAddressForm addAddressForm;
        public SelectUploadAddressesForm(Client new_client)
        {
            InitializeComponent();
            client = new_client;
            IsUpdate = false;
        }
        public SelectUploadAddressesForm(Client new_client, Order new_order)
        {
            InitializeComponent();
            client = new_client;
            order = new_order;
            IsUpdate = true;
        }

        private void CheckSelectedUploadAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var getAddreses = from UA in db.Orders.Find(order.Id).OrderUploadAdresses
                                  orderby UA.UploadAddress.Id
                                  select UA.UploadAddress.Id;

                List<long> GetAddresses = getAddreses.ToList();
                List<int> index = new List<int>();

                if (GetAddresses.Count != 0)
                {
                    foreach (var ItemAddress in uploadAddressListBox.Items)
                    {
                        string fullText = ItemAddress.ToString();
                        string[] tempAddress = fullText.Split(new char[] { '[', ']' });
                        long tempAddressId = Convert.ToInt64(tempAddress[1]);

                        if (GetAddresses.Contains(tempAddressId))
                        {
                            index.Add(uploadAddressListBox.Items.IndexOf(ItemAddress));
                        }
                    }
                    foreach (int i in index)
                    {
                        uploadAddressListBox.SetItemChecked(i, true);
                    }
                    uploadAddressListBox.Update();
                }
            }
        }

        private void UpdateUploadAddresses()
        {
            if (order != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        var getAddreses = from address in db.Orders.Find(order.Id).OrderUploadAdresses
                                          select address.AddressId;

                        List<long> GetAddreses = getAddreses.ToList();
                        List<long> getCheked = new List<long>();
                        foreach (var ItemAddress in uploadAddressListBox.CheckedItems)
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
                                    UploadAddress Address = db.UploadAddresses.Find(deleteItem);

                                    OrderUploadAdress OrderAddress = db.Orders.Find(order.Id).OrderUploadAdresses.Where(Oua => Oua.AddressId == Address.Id).FirstOrDefault();

                                    db.OrderUploadAdresses.Remove(OrderAddress);
                                }
                                MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_видалено + deletedAdresses.Count + AtlantSovt.Properties.Resources.Адрес_розвантаження);
                            }

                            if (newAddreses.Count != 0)
                            {
                                foreach (var newItem in newAddreses)
                                {
                                    UploadAddress Address = db.UploadAddresses.Find(newItem);

                                    OrderUploadAdress new_OrderAddress = new OrderUploadAdress
                                    {
                                        AddressId = Address.Id
                                    };
                                    db.Orders.Find(order.Id).OrderUploadAdresses.Add(new_OrderAddress);
                                }
                                MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_додано + newAddreses.Count + AtlantSovt.Properties.Resources.Адрес_розвантаження);
                            }
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                }
            }
        }

        public void LoadClientUploadAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from address in db.UploadAddresses
                            where address.ClientId == client.Id
                            select address;
                foreach (var item in query)
                {
                    string countryName = "";
                    string countryCode = "";
                    string cityName = "";
                    string cityCode = "";
                    string streetName = "";
                    string houseNumber = "";
                    string companyName = "";
                    long addressId;

                    if (item.Country != null)
                    {
                        countryName = (item.Country.Name != null && item.Country.Name != "") ? item.Country.Name + ", " : "";
                    }

                    countryCode = (item.CountryCode != null && item.CountryCode != "") ? item.CountryCode + ", " : "";
                    cityName = (item.CityName != null && item.CityName != "") ? item.CityName + ", " : "";
                    cityCode = (item.CityCode != null && item.CityCode != "") ? item.CityCode + ", " : "";
                    streetName = (item.StreetName != null && item.StreetName != "") ? item.StreetName + ", " : "";
                    houseNumber = (item.HouseNumber != null && item.HouseNumber != "") ? item.HouseNumber + ", " : "";
                    companyName = (item.CompanyName != null && item.CompanyName != "") ? item.CompanyName + " " : " ";
                    addressId = item.Id;

                    uploadAddressListBox.Items.Add(countryName + countryCode + cityName + cityCode + streetName + houseNumber + companyName + "[" + addressId + "]");

                }
            }
        }
        public void LoadClientUploadAddresses(long? id )
        {
            if (id.HasValue)
            {
                using (var db = new AtlantSovtContext())
                {
                    var query = from address in db.UploadAddresses
                                where address.Id == id.Value
                                select address;
                    foreach (var item in query)
                    {
                        string countryName = "";
                        string countryCode = "";
                        string cityName = "";
                        string cityCode = "";
                        string streetName = "";
                        string houseNumber = "";
                        string companyName = "";
                        long addressId;

                        if (item.Country != null)
                        {
                            countryName = (item.Country.Name != null && item.Country.Name != "") ? item.Country.Name + ", " : "";
                        }

                        countryCode = (item.CountryCode != null && item.CountryCode != "") ? item.CountryCode + ", " : "";
                        cityName = (item.CityName != null && item.CityName != "") ? item.CityName + ", " : "";
                        cityCode = (item.CityCode != null && item.CityCode != "") ? item.CityCode + ", " : "";
                        streetName = (item.StreetName != null && item.StreetName != "") ? item.StreetName + ", " : "";
                        houseNumber = (item.HouseNumber != null && item.HouseNumber != "") ? item.HouseNumber + ", " : "";
                        companyName = (item.CompanyName != null && item.CompanyName != "") ? item.CompanyName + " " : " ";
                        addressId = item.Id;

                        uploadAddressListBox.Items.Add(countryName + countryCode + cityName + cityCode + streetName + houseNumber + companyName + "[" + addressId + "]");

                    }
                }
            }
        }

        private void uploadAddressListBox_DoubleClick(object sender, EventArgs e)
        {
            uploadAddressListBox.Items.Clear();
            LoadClientUploadAddresses();
            if (order != null)
            {
                CheckSelectedUploadAddresses();
            }            
        }

        private void addUploadAddressButton_Click(object sender, EventArgs e)
        {
            if (addAddressForm == null || addAddressForm.IsDisposed)
            {
                addAddressForm = new AddAddressForm(client, 2, this);
                addAddressForm.Show();
            }
            else
            {
                addAddressForm.Show();
                addAddressForm.Focus();
            }
        }

        private void addUploadAdressesToOrderButton_Click(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                UpdateUploadAddresses();
                this.Dispose();
            }
            else
            {
                this.Hide();
            }
        }

        internal string UploadAddressesSelect(Order new_order)
        {
            order = new_order;
            return SaveUploadAdresses();
        }

        private string SaveUploadAdresses()
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
                        return AtlantSovt.Properties.Resources.Успішно_вибрано + uploadAddressListBox.CheckedItems.Count + AtlantSovt.Properties.Resources.Адрес_розвантаження + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private void SelectUploadAddressesForm_Load(object sender, EventArgs e)
        {
            LoadClientUploadAddresses();

            if (order != null)
            {
                CheckSelectedUploadAddresses();
            }
        }

        private void SelectUploadAddressesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (order == null)
            {
                if (uploadAddressListBox.CheckedItems.Count != 0)
                {
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Закрити_форму_без_збереження_адрес, AtlantSovt.Properties.Resources.Підтвердження_закриття, MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
