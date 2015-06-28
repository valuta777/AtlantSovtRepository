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
                        MessageBox.Show("Нове значення успішно додане");
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message);
                    }
                }
            }
        }

        private void addCountryButton_Click(object sender, EventArgs e)
        {
            AddCountry();
            this.Dispose();
        }
    }
}
