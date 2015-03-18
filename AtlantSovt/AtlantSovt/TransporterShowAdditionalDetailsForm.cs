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
    public partial class TransporterShowAdditionalDetailsForm : Form
    {
        public TransporterShowAdditionalDetailsForm()
        {
            InitializeComponent();
        }


        internal void ShowTransporterAdditionalDetails(int ClikedId)
        {
             using (var db = new AtlantSovtContext())
            {
                try
                {
                    var query =
                    from f in db.Filters
                    where f.Id == ClikedId
                    select new
                    {
                        TUR = f.TUR,
                        CMR = f.CMR,
                        EKMT = f.EKMT,
                        Zborny = f.Zborny,
                        AD = f.AD,
                        IfForwarder = f.IfForwarder
                    };
                    transporterShowAdditionalDetailsGridView.DataSource = query.ToList();
                    transporterShowAdditionalDetailsGridView.Columns[0].HeaderText = "TUR";
                    transporterShowAdditionalDetailsGridView.Columns[1].HeaderText = "CMR";
                    transporterShowAdditionalDetailsGridView.Columns[2].HeaderText = "EKMT";
                    transporterShowAdditionalDetailsGridView.Columns[3].HeaderText = "Збірний";
                    transporterShowAdditionalDetailsGridView.Columns[4].HeaderText = "AD";
                    transporterShowAdditionalDetailsGridView.Columns[5].HeaderText = "Чи є експедитором?";


                    var query1 =
                    from v in db.TransporterVehicles
                    where v.TransporterId == ClikedId
                    select new
                    {
                        Type = v.Vehicle.Type,

                    };
                    transporterShowVehicleAdditionalDetailsGridView.DataSource = query1.ToList();
                    transporterShowVehicleAdditionalDetailsGridView.Columns[0].HeaderText = "Тип транспорту";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Немає жодного перевізника" + ex);
                }
            }
            transporterShowAdditionalDetailsGridView.Update();

        
        }
    }
}
