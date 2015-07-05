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
                    string new_Type ="";
                    new_Type = addRegularyDelayTextBox.Text != "" ? new_Type + addRegularyDelayTextBox.Text : "__";
                    new_Type = addRegularyDelayTextBox2.Text != "" ? new_Type +" - "+ addRegularyDelayTextBox2.Text : "__";
                    new_Type = addRegularyDelayTextBox3.Text != "" ? new_Type +" - " + addRegularyDelayTextBox3.Text : "__";
                    new_Type = addRegularyDelayTextBox4.Text != "" ? new_Type +" - " + addRegularyDelayTextBox4.Text : "__";
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void addRegularyDelayTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) &&  e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void addRegularyDelayTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) &&  e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void addRegularyDelayTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void addRegularyDelayTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
    }
}
