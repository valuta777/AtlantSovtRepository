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
    public partial class TransporterCountryUpdateVehicleSelectForm : Form
    {
        Transporter transporter;
        AddCountryForm country;
        AddTransporterVehicleForm vehicle;

        public TransporterCountryUpdateVehicleSelectForm()
        {
            InitializeComponent();
            LoadCoutriesToChechedBoxList();
            LoadVehicleToChechedBoxList();
        }

        public void LoadCoutriesToChechedBoxList()
        {
            transporterUpdateFilterSelectCountryCheckedListBox.Items.Clear();
            using (var db = new AtlantSovtContext())
            {
                var query = from country in db.Countries
                            orderby country.Name
                            select country.Name;
                foreach (var item in query)
                {
                    transporterUpdateFilterSelectCountryCheckedListBox.Items.Add(item);
                }
            }        
        }

        public void LoadVehicleToChechedBoxList()
        {
            transporterUpdateFilterSelectVehicleCheckedListBox.Items.Clear();
            using (var db = new AtlantSovtContext())
            {
                var query = from vehicle in db.Vehicles
                            orderby vehicle.Type
                            select vehicle.Type;

                foreach (var item in query)
                {
                    transporterUpdateFilterSelectVehicleCheckedListBox.Items.Add(item);
                }
            }
        }

        public void LoadTransporterCoutriesToChechedBoxList()
        {
            using (var db = new AtlantSovtContext())
            {

                var getCountries =
                    from tcountry in  db.Transporters.Find(transporter.Id).TransporterCountries
                    orderby tcountry.Country.Name
                    select tcountry.Country.Name;

                List<string> GetCountries = getCountries.ToList();                   
                List<int> index = new List<int>();                
                
                if (GetCountries.Count != 0)
                {
                    foreach (var item in transporterUpdateFilterSelectCountryCheckedListBox.Items)
                    {
                        if (GetCountries.Contains(item))
                        {
                            index.Add(transporterUpdateFilterSelectCountryCheckedListBox.Items.IndexOf(item));
                        }
                    }
                    foreach(int i in index)
                    {
                        transporterUpdateFilterSelectCountryCheckedListBox.SetItemChecked(i, true);
                    }
                    transporterUpdateFilterSelectCountryCheckedListBox.Update();
                }
            }
        }

        public void LoadTransporterVehicleToChechedBoxList()
        {
            using (var db = new AtlantSovtContext())
            {
                var getVehicles = from tvehicle in db.Transporters.Find(transporter.Id).TransporterVehicles
                            orderby tvehicle.Vehicle.Type
                            select tvehicle.Vehicle.Type;

                List<string> GetVehicles = getVehicles.ToList();
                List<int> index = new List<int>();

                if (GetVehicles.Count != 0)
                {
                    foreach (var item in transporterUpdateFilterSelectVehicleCheckedListBox.Items)
                    {
                        if (GetVehicles.Contains(item))
                        {
                            index.Add(transporterUpdateFilterSelectVehicleCheckedListBox.Items.IndexOf(item));
                        }
                    }
                    foreach (int i in index)
                    {
                        transporterUpdateFilterSelectVehicleCheckedListBox.SetItemChecked(i, true);
                    }
                    transporterUpdateFilterSelectVehicleCheckedListBox.Update();
                }
            }
        }

        internal void CoutriesAndVehiclesSelect(Transporter add_transporter) 
        {
            transporter = add_transporter;
            LoadTransporterCoutriesToChechedBoxList();
            LoadTransporterVehicleToChechedBoxList();            
        }

        private void SaveCountries()
        {
            if (transporter != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                       var getCountriesQwery =
                            from tcountry in db.Transporters.Find(transporter.Id).TransporterCountries
                            orderby tcountry.Country.Name
                            select tcountry.Country.Name;

                       List<string> getCountries = getCountriesQwery.ToList();
                       List<string> getCheked = new List<string>();
                       foreach(var item in transporterUpdateFilterSelectCountryCheckedListBox.CheckedItems)
                       {
                            getCheked.Add(item.ToString());
                       }
                       List<string> newCountries = getCheked.Except(getCountries).ToList(); ;
                       List<string> deletedCountries = getCountries.Except(getCheked).ToList();

                       if (deletedCountries.Count != 0 || newCountries.Count != 0)
                       {
                           if (deletedCountries.Count != 0)
                           {
                                foreach (var deleteItem in deletedCountries)
                                {
                                    Country country = db.Countries.Where(c => c.Name == deleteItem).FirstOrDefault();
                                    TransporterCountry transporterCountry = db.Transporters.Find(transporter.Id).TransporterCountries.Where(tc => tc.CountryId == country.Id).FirstOrDefault();

                                    db.TransporterCountries.Remove(transporterCountry);
                                }
                            }
                           if (newCountries.Count != 0)
                           {
                               foreach (var newItem in newCountries)
                               {
                                   Country country = db.Countries.Where(c => c.Name == newItem).FirstOrDefault();

                                   var newTransporterCountry = new TransporterCountry
                                   {
                                       CountryId = country.Id
                                   };
                                   db.Transporters.Find(transporter.Id).TransporterCountries.Add(newTransporterCountry);
                               }                               
                           }
                            db.SaveChanges();
                            if (newCountries.Count != 0)
                            {
                               MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_додано + newCountries.Count + AtlantSovt.Properties.Resources.Країн); 
                            }
                            if (deletedCountries.Count != 0)
                            {
                                MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_видалено + deletedCountries.Count + AtlantSovt.Properties.Resources.Країн);
                            }
                                                    
                       }           
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + e.Message);
                    Log.Write(e);
                }
            }
        }

        private void SaveVehicles()
        {
            if (transporter != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        var getVehiclesQwery =
                             from tvehicle in db.Transporters.Find(transporter.Id).TransporterVehicles
                             orderby tvehicle.Vehicle.Type
                             select tvehicle.Vehicle.Type;

                        List<string> getVehicles = getVehiclesQwery.ToList();
                        List<string> getCheked = new List<string>();
                        foreach (var item in transporterUpdateFilterSelectVehicleCheckedListBox.CheckedItems)
                        {
                            getCheked.Add(item.ToString());
                        }
                        List<string> newVehicles = getCheked.Except(getVehicles).ToList(); ;
                        List<string> deletedVehicles = getVehicles.Except(getCheked).ToList();

                        if (deletedVehicles.Count != 0 || newVehicles.Count != 0)
                        {
                            if (deletedVehicles.Count != 0)
                            {
                                foreach (var deleteItem in deletedVehicles)
                                {
                                    Vehicle vehicle = db.Vehicles.Where(c => c.Type == deleteItem).FirstOrDefault();

                                    TransporterVehicle transporterVehicle = db.Transporters.Find(transporter.Id).TransporterVehicles.Where(tv => tv.TransportVehicleId == vehicle.Id).FirstOrDefault();

                                    db.TransporterVehicles.Remove(transporterVehicle);
                                }

                            }
                            if (newVehicles.Count != 0)
                            {
                                foreach (var newItem in newVehicles)
                                {
                                     Vehicle vehicle = db.Vehicles.Where(c => c.Type == newItem).FirstOrDefault();

                                     var newTransporterVehicle = new TransporterVehicle
                                     {
                                         TransportVehicleId = vehicle.Id
                                     };
                                     db.Transporters.Find(transporter.Id).TransporterVehicles.Add(newTransporterVehicle);
                                }
                                
                            }
                            db.SaveChanges();
                                if (newVehicles.Count != 0)
                                {
                                    MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_додано + newVehicles.Count + AtlantSovt.Properties.Resources.Типів_транспорту);
                                }
                                if (deletedVehicles.Count != 0)
                                {
                                    MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_видалено + deletedVehicles.Count + AtlantSovt.Properties.Resources.Типів_транспорту);
                                }
                            }
                        }
                    
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + e.Message);
                }
            }
        }

        private void transporterFilterSelectButton_Click(object sender, EventArgs e)
        {
            SaveCountries();
            SaveVehicles();
            this.Dispose();
        }

        private void transporterAddCountryButton_Click(object sender, EventArgs e)
        {
            if (country == null || country.IsDisposed)
            {
                country = new AddCountryForm(this);
                country.Show();
            }
            else
            {
                country.Show();
                country.Focus();
            }
        }

        private void transporterAddVehicleButton_Click(object sender, EventArgs e)
        {
            if ((vehicle == null || vehicle.IsDisposed))
            {
                vehicle = new AddTransporterVehicleForm(this);
                vehicle.Show();
            }
            else
            {
                vehicle.Show();
                vehicle.Focus();
            }            
        }
    }
}
