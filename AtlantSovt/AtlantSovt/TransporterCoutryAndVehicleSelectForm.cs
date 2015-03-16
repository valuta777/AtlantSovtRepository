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
    public partial class TransporterCoutryAndVehicleSelectForm : Form
    {
        Transporter transporter;
        public TransporterCoutryAndVehicleSelectForm()
        {
            InitializeComponent();
            LoadCoutriesToChechedBoxList();
            LoadVehicleToChechedBoxList();
        }

        private void LoadCoutriesToChechedBoxList() 
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from country in db.Countries
                            orderby country.Name
                            select country.Name;
                foreach (var item in query)
                {
                    transporterFilterSelectCountryCheckedListBox.Items.Add(item);
                }
            }        
        }
        private void LoadVehicleToChechedBoxList()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from vehicle in db.Vehicles
                            orderby vehicle.Type
                            select vehicle.Type;

                foreach (var item in query)
                {
                    transporterFilterSelectVehicleCheckedListBox.Items.Add(item);
                }
            }
        }

        internal void CoutriesAndVehiclesSelect(Transporter add_transporter) 
        {
            transporter = add_transporter;
        }

        private void SaveCountries()
        {
            if (transporterFilterSelectCountryCheckedListBox.CheckedItems.Count != 0 && transporter != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        foreach (var ItemCountry in transporterFilterSelectCountryCheckedListBox.CheckedItems)
                        {                    
                            Country country = db.Countries.Where(c => c.Name == ItemCountry).FirstOrDefault();

                            var new_TransporterCountry = new TransporterCountry
                            {
                                CountryId = country.Id
                            };
                            db.Transporters.Find(transporter.Id).TransporterCountries.Add(new_TransporterCountry);
                        }

                    db.SaveChanges();
                    MessageBox.Show("Успішно вибрано " + transporterFilterSelectCountryCheckedListBox.CheckedItems.Count + " країн");                        
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Помилка!!",e.ToString());             
                }
            }
        }
        private void SaveVehicles()
        {
            if (transporterFilterSelectVehicleCheckedListBox.CheckedItems.Count != 0 && transporter != null)
            {
                try
                {
                    using (var db = new AtlantSovtContext())
                    {
                        foreach (var ItemVehicle in transporterFilterSelectVehicleCheckedListBox.CheckedItems)
                        {
                            Vehicle vehicle = db.Vehicles.Where(c => c.Type == ItemVehicle).FirstOrDefault();
                            var new_TransporterVehicle = new TransporterVehicle
                            {
                                TransportVehicleId = vehicle.Id
                            };
                            db.Transporters.Find(transporter.Id).TransporterVehicles.Add(new_TransporterVehicle);
                        }

                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано " + transporterFilterSelectVehicleCheckedListBox.CheckedItems.Count + " типів транспорту");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Помилка!!", e.ToString());
                }
            }
        }
        private void transporterFilterSelectButton_Click(object sender, EventArgs e)
        {
            SaveCountries();
            SaveVehicles();
        }

        private void transporterFilterSelectCountryCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
