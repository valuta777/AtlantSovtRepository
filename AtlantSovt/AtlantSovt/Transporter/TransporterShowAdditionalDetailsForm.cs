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
                        TUR = (f.TUR == true) ? AtlantSovt.Properties.Resources.Так : (f.TUR == false) ? AtlantSovt.Properties.Resources.Ні : "",
                        CMR = (f.CMR == true) ? AtlantSovt.Properties.Resources.Так : (f.CMR == false) ? AtlantSovt.Properties.Resources.Ні : "",
                        EKMT = (f.EKMT == true) ? AtlantSovt.Properties.Resources.Так : (f.EKMT == false) ? AtlantSovt.Properties.Resources.Ні : "",
                        Zborny = (f.Zborny == true) ? AtlantSovt.Properties.Resources.Так : (f.Zborny == false) ? AtlantSovt.Properties.Resources.Ні : "",
                        AD = (f.AD == true) ? AtlantSovt.Properties.Resources.Так : (f.AD == false) ? AtlantSovt.Properties.Resources.Ні : "",
                        IfForwarder = (f.IfForwarder == true) ? AtlantSovt.Properties.Resources.Так : (f.IfForwarder == false) ? AtlantSovt.Properties.Resources.Ні : "",
                    };
                    transporterShowAdditionalDetailsGridView.DataSource = query.ToList();
                    transporterShowAdditionalDetailsGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.TIR;
                    transporterShowAdditionalDetailsGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.CMR;
                    transporterShowAdditionalDetailsGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.EKMT;
                    transporterShowAdditionalDetailsGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Збірний;
                    transporterShowAdditionalDetailsGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.ADR;
                    transporterShowAdditionalDetailsGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Чи_є_експедитором;


                    var query1 =
                    from v in db.TransporterVehicles
                    where v.TransporterId == ClikedId
                    select new
                    {
                        Type = v.Vehicle.Type,

                    };
                    transporterShowVehicleAdditionalDetailsGridView.DataSource = query1.ToList();
                    transporterShowVehicleAdditionalDetailsGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Тип_транспорту;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_перевізника);
                }
            }
            transporterShowAdditionalDetailsGridView.Update();

        
        }
    }
}
