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
    public partial class AddTaxPayerStatusForm : Form
    {
        public AddTaxPayerStatusForm()
        {
            InitializeComponent();
        }
        public void AddTaxPayerStatus()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addTaxPayerStatusTextBox.Text != "")
                {
                    var new_Status = addTaxPayerStatusTextBox.Text;

                    var New_TaxPayerStatus = new TaxPayerStatu
                    {
                        Status = new_Status,
                    };
                    try
                    {
                        db.TaxPayerStatus.Add(New_TaxPayerStatus);
                        db.SaveChanges();
                        MessageBox.Show("Нове значення успішно додане");
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void addTaxPayerStatusButton_Click(object sender, EventArgs e)
        {
            AddTaxPayerStatus();
            this.Dispose();
        }
    }
}
