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
    public partial class AddRegularyDelayForm : Form
    {
        public AddRegularyDelayForm()
        {
            InitializeComponent();
        }

        public void AddRegularyDelay()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addRegularyDelayTextBox.Text != "")
                {
                    var new_Type = addRegularyDelayTextBox.Text;

                    var New_RegularyDelay = new RegularyDelay
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.RegularyDelays.Add(New_RegularyDelay);
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

        private void addCargoButton_Click(object sender, EventArgs e)
        {
            AddRegularyDelay();
            this.Dispose();
        }
    }
}
