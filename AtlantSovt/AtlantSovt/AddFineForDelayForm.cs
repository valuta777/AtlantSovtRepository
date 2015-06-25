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
    public partial class AddFineForDelayForm : Form
    {
        public AddFineForDelayForm()
        {
            InitializeComponent();
        }

        public void AddFineForDelay()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addFineForDelayTextBox.Text != "")
                {
                    var new_Type = addFineForDelayTextBox.Text;

                    var New_FineForDelay = new FineForDelay
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.FineForDelays.Add(New_FineForDelay);
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
            AddFineForDelay();
            this.Dispose();
        }
    }
}
