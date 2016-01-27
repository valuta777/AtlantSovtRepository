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
    public partial class AddTirCmrForm : Form
    {
        public AddTirCmrForm()
        {
            InitializeComponent();
        }

        public void AddTirCmr()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addTirCmrTextBox.Text != "")
                {
                    var new_Type = addTirCmrTextBox.Text;

                    var New_TirCmr = new TirCmr
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.TirCmrs.Add(New_TirCmr);
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
            AddTirCmr();
            this.Dispose();
        }
    }
}
