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
    public partial class SelectCustomsAddressesForm : Form
    {
        Client client;
        Order order;
        bool IsUpdate;
        AddAddressForm addCustomsAddressForm;
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
                                MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_видалено + deletedAdresses.Count + AtlantSovt.Properties.Resources.Адрес_замитнення);
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
                                MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_додано + newAddreses.Count + AtlantSovt.Properties.Resources.Адрес_замитнення);
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

        public void LoadClientCustomsAddresses()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from address in db.CustomsAddresses
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

                    customsAddressesListBox.Items.Add(countryName + countryCode + cityName + cityCode + streetName + houseNumber + companyName + "[" + addressId + "]");
                }
            }
        }

        public void LoadClientCustomsAddresses(long? id)
        {
            if (id.HasValue)
            {
                using (var db = new AtlantSovtContext())
                {
                    var query = from address in db.CustomsAddresses
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

                        customsAddressesListBox.Items.Add(countryName + countryCode + cityName + cityCode + streetName + houseNumber + companyName + "[" + addressId + "]");

                    }
                }
            }
        }

        private void сustomsAddressListBox_DoubleClick(object sender, EventArgs e)
        {
            customsAddressesListBox.Items.Clear();
            LoadClientCustomsAddresses();
            if (order != null)
            {
                CheckSelectedCustomsAddresses();
            }
        }

        private void addCustomsAddressButton_Click(object sender, EventArgs e)
        {
            if (addCustomsAddressForm == null || addCustomsAddressForm.IsDisposed)
            {
                addCustomsAddressForm = new AddAddressForm(client, 3, this);
                addCustomsAddressForm.Show();
            }
            else
            {
                addCustomsAddressForm.Show();
                addCustomsAddressForm.Focus();
            }
        }        

        internal string CustomsAddressesSelect(Order new_order)
        {
            order = new_order;
            return SaveCustomsAddresses();
        }

        private string SaveCustomsAddresses()
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
                        return AtlantSovt.Properties.Resources.Успішно_вибрано + customsAddressesListBox.CheckedItems.Count + AtlantSovt.Properties.Resources.Адрес_замитнення + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка, ex.Message);
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private void SelectCustomsAddressesForm_Load(object sender, EventArgs e)
        {
            LoadClientCustomsAddresses();
            if (order != null)
            {
                CheckSelectedCustomsAddresses();
            }
        }

        private void SelectCustomsAddressesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (order == null)
            {
                if (customsAddressesListBox.CheckedItems.Count != 0)
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
