﻿using AtlantSovt.AtlantSovtDb;
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
    public partial class TransporterShowFiltrationForm : Form
    {
        MainForm mainForm;

        public TransporterShowFiltrationForm(MainForm m)
        {
            InitializeComponent();
            mainForm = m;
        }

        public void LoadCoutriesToTransporterShowChechedBoxList()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from country in db.Countries
                            orderby country.Name
                            select country.Name;
                foreach (var item in query)
                {
                    transporterShowFiltersSelectCountryCheckedListBox.Items.Add(item);
                }
            }
        }

        public void LoadVehicleToTransporterShowChechedBoxList()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from vehicle in db.Vehicles
                            orderby vehicle.Type
                            select vehicle.Type;
                foreach (var item in query)
                {
                    transporterShowFiltersSelectVehicleCheckedListBox.Items.Add(item);
                }
            }
        }

        public List<long> GetCountries()
        {
            using(var db = new AtlantSovtContext())
            {
                if (transporterShowFiltersSelectCountryCheckedListBox.CheckedItems != null)
                {
                    List<long> checkedCountriesList = new List<long>();
                    foreach (var itemCountry in transporterShowFiltersSelectCountryCheckedListBox.CheckedItems)
                    {
                        checkedCountriesList.Add(Convert.ToInt32(db.Countries.Where(c => c.Name == itemCountry).FirstOrDefault().Id));
                    }
                    return checkedCountriesList;
                }
                else return null;
            }
        }

        public List<long> GetVehicle()
        {
            using(var db = new AtlantSovtContext())
            {
                if (transporterShowFiltersSelectVehicleCheckedListBox.CheckedItems != null)
                {
                    List<long> checkedVehicleList = new List<long>();
                    foreach (var itemVehicle in transporterShowFiltersSelectVehicleCheckedListBox.CheckedItems)
                    {
                        checkedVehicleList.Add(Convert.ToInt32(db.Vehicles.Where(c => c.Type == itemVehicle).FirstOrDefault().Id));
                    }
                    return checkedVehicleList;
                }
                else return null;
            }
        }
 
        private void transporterShowFilterButton_Click(object sender, EventArgs e)
        {
          
            mainForm.ShowTransporterFilter();
            this.Dispose();
        }
        
    }
}
