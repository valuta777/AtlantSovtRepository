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
    public partial class AddAddressForm : Form
    {
        Client client;
        Country country;
        Byte currentCase;
        private SelectCustomsAddressesForm selectCustomsAddresses { get; set; }
        private SelectUncustomsAddressesForm selectUncustomsAddresses { get; set; }
        private SelectDownloadAddressesForm selectDownloadAddresses { get; set; }
        private SelectUploadAddressesForm selectUploadAddresses { get; set; }

        public AddAddressForm(Client new_client, Byte new_currentCase, SelectCustomsAddressesForm selectCustomsAddresses)
        {
            InitializeComponent();
            client = new_client;
            currentCase = new_currentCase;
            this.selectCustomsAddresses = selectCustomsAddresses;
        }

        public AddAddressForm(Client new_client, Byte new_currentCase, SelectUncustomsAddressesForm selectUncustomsAddresses)
        {
            InitializeComponent();
            client = new_client;
            currentCase = new_currentCase;
            this.selectUncustomsAddresses = selectUncustomsAddresses;
        }

        public AddAddressForm(Client new_client, Byte new_currentCase, SelectDownloadAddressesForm selectDownloadAddresses)
        {
            InitializeComponent();
            client = new_client;
            currentCase = new_currentCase;
            this.selectDownloadAddresses = selectDownloadAddresses;
        }

        public AddAddressForm(Client new_client, Byte new_currentCase, SelectUploadAddressesForm selectUploadAddresses)
        {
            InitializeComponent();
            client = new_client;
            currentCase = new_currentCase;
            this.selectUploadAddresses = selectUploadAddresses;
        }
         
        string new_CountryCode;
        string new_CityName;
        string new_CityCode;
        string new_StreetName;
        string new_HouseNumber;
        string new_CompanyName;
        string new_ContactPerson;
       
        private void AddUploadAddress()
        {
            using (var db = new AtlantSovtContext())
            {
                var New_UploadAddress = new UploadAddress
                {
                    CountryId = country.Id,
                    ClientId = client.Id,
                    CityCode = new_CityCode,
                    StreetName = new_StreetName,
                    HouseNumber = new_HouseNumber,
                    CompanyName = new_CompanyName,
                    ContactPerson = new_ContactPerson,
                    CityName = new_CityName,
                    CountryCode = new_CountryCode,
                };
                try
                {
                    db.UploadAddresses.Add(New_UploadAddress);
                    db.SaveChanges();
                    MessageBox.Show("Адреса успішно додана ");
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void AddDownloadAddress()
        {
            using (var db = new AtlantSovtContext())
            {
                var New_DownloadAddress = new DownloadAddress
                {
                    CountryId = country.Id,
                    ClientId = client.Id,
                    CityCode = new_CityCode,
                    StreetName = new_StreetName,
                    HouseNumber = new_HouseNumber,
                    CompanyName = new_CompanyName,
                    ContactPerson = new_ContactPerson,
                    CityName = new_CityName,
                    CountryCode = new_CountryCode,
                };
                try
                {
                    db.DownloadAddresses.Add(New_DownloadAddress);
                    db.SaveChanges();
                    MessageBox.Show("Адреса успішно додана ");
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void AddCustomsAddress()
        {
            using (var db = new AtlantSovtContext())
            {
                var New_CustomsAddress = new CustomsAddress
                {
                    CountryId = country.Id,
                    ClientId = client.Id,
                    CityCode = new_CityCode,
                    StreetName = new_StreetName,
                    HouseNumber = new_HouseNumber,
                    CompanyName = new_CompanyName,
                    ContactPerson = new_ContactPerson,
                    CityName = new_CityName,
                    CountryCode = new_CountryCode,
                };
                try
                {
                    db.CustomsAddresses.Add(New_CustomsAddress);
                    db.SaveChanges();
                    MessageBox.Show("Адреса успішно додана ");
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void AddUnCustomsAddress()
        {
            using (var db = new AtlantSovtContext())
            {
                var New_UnCustomsAddress = new UnCustomsAddress
                {
                    CountryId = country.Id,
                    ClientId = client.Id,
                    CityCode = new_CityCode,
                    StreetName = new_StreetName,
                    HouseNumber = new_HouseNumber,
                    CompanyName =  new_CompanyName,
                    ContactPerson =  new_ContactPerson,
                    CityName = new_CityName,
                    CountryCode = new_CountryCode,
                };
                try
                {
                    db.UnCustomsAddresses.Add(New_UnCustomsAddress);
                    db.SaveChanges();
                    MessageBox.Show("Адреса успішно додана ");
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void addAddressButton_Click(object sender, EventArgs e)
        {
             new_CountryCode = addressAddCountryCodeTextBox.Text;
             new_CityName = addressAddCityNameTextBox.Text;
             new_CityCode = addressAddCityCodeTextBox.Text;
             new_StreetName = addressAddStreetNameTextBox.Text;
             new_HouseNumber = addressAddHouseNumberTextBox.Text;
             new_CompanyName = addressAddCompanyNameTextBox.Text;
             new_ContactPerson = addressAddContactPersonTextBox.Text;

            switch(currentCase)
            {
                case 1 : AddDownloadAddress();
                     selectDownloadAddresses.downloadAddresssListBox.Items.Clear();
                     selectDownloadAddresses.LoadClientDownloadAddresses();
                    break;

                case 2 : AddUploadAddress(); 
                    selectUploadAddresses.uploadAddressListBox.Items.Clear();
                    selectUploadAddresses.LoadClientUploadAddresses();
                    break; 
                    
                case 3 : AddCustomsAddress();
                    selectCustomsAddresses.customsAddressesListBox.Items.Clear();
                    selectCustomsAddresses.LoadClientCustomsAddresses();
                    break;
                    
                case 4 : AddUnCustomsAddress();
                    selectUncustomsAddresses.uncustomsAddressesListBox.Items.Clear();
                    selectUncustomsAddresses.LoadClientUncustomsAddresses();
                    break;

                default: MessageBox.Show("Error");break;
            }
            
        }
        private void LoadCountryComboBoxInfo()
        {
            using (var db = new AtlantSovtContext())
            {

                var query = from country in db.Countries
                            orderby country.Name
                            select country;

                foreach (var item in query)
                {
                    addressAddCountryNameComboBox.Items.Add(item.Name + " [" + item.Id + "]");
                }

            }
        }
        private void SplitUpdateCountry()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = addressAddCountryNameComboBox.SelectedItem.ToString();
                string[] selectedIdAndContactPerson = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedIdAndContactPerson[1];

                long id = Convert.ToInt64(comboBoxSelectedId);
                country = db.Countries.Find(id);                
            }
        }
        private void addressAddCountryNameComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            addressAddCountryNameComboBox.Items.Clear();
            LoadCountryComboBoxInfo();
            addressAddCountryNameComboBox.DroppedDown = true;
        }
        private void addressAddCountryNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplitUpdateCountry();
        }

        private void AddAddressForm_Load(object sender, EventArgs e)
        {

        }
        private void addressAddCountryAddButton_Click(object sender, EventArgs e)
        {
            AddCountryForm addCountryForm = new AddCountryForm();
            addCountryForm.Show();
        }
    }    
}
