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
    public partial class AddCargoForm : Form
    {
        public AddCargoForm()
        {
            InitializeComponent();
        }

        public void AddCargo()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addCargoTextBox.Text != "")
                {
                    var new_Type = addCargoTextBox.Text;

                    var New_Cargo = new Cargo
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.Cargoes.Add(New_Cargo);
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

        private void addCargoButton_Click(object sender, EventArgs e)
        {
            AddCargo();
            this.Dispose();
        }
    }
}
