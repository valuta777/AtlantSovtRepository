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
    public partial class AddOrderDenyForm : Form
    {
        public AddOrderDenyForm()
        {
            InitializeComponent();
        }

        public void AddOrderDeny()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addOrderDenyTextBox.Text != "")
                {
                    var new_Type = addOrderDenyTextBox.Text;

                    var New_OrderDeny = new OrderDeny
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.OrderDenies.Add(New_OrderDeny);
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
            AddOrderDeny();
            this.Dispose();
        }
    }
}
