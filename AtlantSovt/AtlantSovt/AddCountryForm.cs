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
    public partial class AddCountryForm : Form
    {
        public AddCountryForm()
        {
            InitializeComponent();
        }

        public AddCountryForm(TransporterCountryAndVehicleSelectForm form)
        {
            InitializeComponent();
            this.addForm = form;
        }

        public AddCountryForm(TransporterCountryUpdateVehicleSelectForm form)
        {
            InitializeComponent();
            this.updateForm = form;
        }

        private TransporterCountryAndVehicleSelectForm addForm { get; set; }
        private TransporterCountryUpdateVehicleSelectForm updateForm { get; set; }

        public void AddCountry()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addCountryTextBox.Text != "")
                {
                    var new_Name = addCountryTextBox.Text;

                    var New_Country = new Country
                    {
                        Name = new_Name,
                    };
                    try
                    {
                        db.Countries.Add(New_Country);
                        db.SaveChanges();
                        MessageBox.Show(AtlantSovt.Properties.Resources.Нове_значення_успішно_додане);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void addCountryButton_Click(object sender, EventArgs e)
        {
            AddCountry();
            if (this.addForm != null)
            {
                this.addForm.LoadCoutriesToChechedBoxList();
            }
            if(this.updateForm != null)
            {
                this.updateForm.LoadCoutriesToChechedBoxList();
                this.updateForm.LoadTransporterCoutriesToChechedBoxList();
            }
            this.Dispose();
        }
    }
}
