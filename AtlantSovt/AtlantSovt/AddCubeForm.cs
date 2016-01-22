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
    public partial class AddCubeForm : Form
    {
        public AddCubeForm()
        {
            InitializeComponent();
        }

        public void AddCube()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addCubeTextBox.Text != "")
                {
                    var new_Type = addCubeTextBox.Text;

                    var New_Cube = new Cube
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.Cubes.Add(New_Cube);
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
            AddCube();
            this.Dispose();
        }
    }
}
