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
    public partial class AddStaffForm : Form
    {
        public AddStaffForm()
        {
            InitializeComponent();
        }

        public void AddStaff()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addStaffTextBox.Text != "")
                {
                    var new_Type = addStaffTextBox.Text;

                    var New_Staff = new Staff
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.Staffs.Add(New_Staff);
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

        private void addStaffButton_Click(object sender, EventArgs e)
        {
            AddStaff();
            this.Dispose();
        }
    }
}
