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
    public partial class AddTrailerForm : Form
    {
        public AddTrailerForm()
        {
            InitializeComponent();
        }

        public void AddTrailer()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addTrailerTextBox.Text != "")
                {
                    var new_Type = addTrailerTextBox.Text;

                    var New_Trailer = new Trailer
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.Trailers.Add(New_Trailer);
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

        private void addCargoButton_Click(object sender, EventArgs e)
        {
            AddTrailer();
            this.Dispose();
        }
    }
}
