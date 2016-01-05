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
    public partial class TransporterCountryAndVehicleSelectForm : Form
    {
        Transporter transporter;

        public TransporterCountryAndVehicleSelectForm()
        {
            InitializeComponent();
            LoadCoutriesToChechedBoxList();
            LoadVehicleToChechedBoxList();
        }

        public void LoadCoutriesToChechedBoxList()
        {
            transporterFilterSelectCountryCheckedListBox.Items.Clear();
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

        public void LoadVehicleToChechedBoxList()
        {
            transporterFilterSelectVehicleCheckedListBox.Items.Clear();
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

        internal string CoutriesAndVehiclesSelect(Transporter add_transporter) 
        {
            transporter = add_transporter;
            return  SaveCountries()+ SaveVehicles();
        }

        private string SaveCountries()
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
                    return "Успішно вибрано " + transporterFilterSelectCountryCheckedListBox.CheckedItems.Count + " країн(а) \n";                        
                    }
                }
                catch(Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка!!", ex.ToString());
                    return string.Empty;
                }
            }
            else { return string.Empty; }
        }

        private string SaveVehicles()
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
                        return "Успішно вибрано " + transporterFilterSelectVehicleCheckedListBox.CheckedItems.Count + " тип(ів) транспорту\n";
                    }
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    MessageBox.Show("Помилка!!", e.ToString());
                    return string.Empty;
                }
            }
            else { return string.Empty; }

        }

        private void transporterFilterSelectButton_Click(object sender, EventArgs e)
        {   
            this.Hide();
        }

        private void transporterAddCountryButton_Click(object sender, EventArgs e)
        {
            AddCountryForm country = new AddCountryForm(this);
            country.Show();
        }

        private void transporterAddVehicleButton_Click(object sender, EventArgs e)
        {
            AddTransporterVehicleForm vehicle = new AddTransporterVehicleForm(this);
            vehicle.Show();
        }
    }
}
