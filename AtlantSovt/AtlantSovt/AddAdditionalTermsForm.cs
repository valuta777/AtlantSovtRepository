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
    public partial class AddAdditionalTermsForm : Form
    {
        public AddAdditionalTermsForm()
        {
            InitializeComponent();
        }

        public void AddAdditionalTerms()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addAdditionalTermsTextBox.Text != "")
                {
                    var new_Type = addAdditionalTermsTextBox.Text;

                    var New_AdditionalTerm = new AdditionalTerm
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.AdditionalTerms.Add(New_AdditionalTerm);
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
            AddAdditionalTerms();
            this.Dispose();
        }
    }
}
