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
    public partial class MainForm 
    {

        Client clientOrderAdd;
        Transporter transporterOrderAdd;
        Forwarder forwarder1OrderAdd;
        Forwarder forwarder2OrderAdd;

        AdditionalTerm additionalTermOrderAdd;
        Cargo cargoOrderAdd;
        
        FineForDelay fineForDelayOrderAdd;
        TirCmr tirCmrOrderAdd;
        OrderDeny orderDenyOrderAdd;
        Payment paymentOrderAdd;
        RegularyDelay regularyDelayOrderAdd;

        Cube cubeOrderAdd;
        Trailer trailerOrderAdd;

        LoadingForm loadingForm1OrderAdd;
        LoadingForm loadingForm2OrderAdd;

        DateTime orderDate;
        DateTime orderUploadDate;
        DateTime orderDeliveryDate;

        SelectUploadAddressesForm selectUploadAddressesForm;
        // SelectDownloadAddressesForm selectDownloadAddressesForm;
        // SelectCustomsAddressesForm selectCustomsAddressesForm;
        void OrderAdd() 
        {

          //  bool new_YorU 

           // double new_CargoWeight 

          //  string new_LoadingForm1 

          //  string new_LoadingForm2 

          //  int new_ADRNumber 

           // string new_Freight 


            if (selectUploadAddressesForm != null)
            {
                //selectUploadAddressesForm.UploadAdressesSelect(db.Orders.Find(New_Order.Id));
                selectUploadAddressesForm = null;
            }
            //if (selectDownloadAddressesForm != null)
            //{
            //    selectDownloadAddressesForm.DownloadAdressesSelect(db.Orders.Find(New_Order.Id));
            //    selectDownloadAddressesForm = null;
            //}
            //if (selectCustomsAddressesForm != null)
            //{
            //    selectCustomsAddressesForm.CustomsAdressesSelect(db.Orders.Find(New_Order.Id));
            //    selectCustomsAddressesForm = null;
            //}
            //if (selectUncustomsAddressesForm != null)
            //{
            //    selectUncustomsAddressesForm.UncustomsAdressesSelect(db.Orders.Find(New_Order.Id));
            //    selectUncustomsAddressesForm = null;
            //}
        }


        void SetOrderDate()
        {
            orderDate = OrderAddDateSelectDateTimePicker.Value;
        }
        void SetOrderUploadDate()
        {
            orderUploadDate = OrderAddUploadDateTimePicker.Value;
        }
        void SetOrderDeliveryDate()
        {
            orderDeliveryDate = OrderAddDeliveryDateTimePicker.Value;
        }

        //Client
        void LoadOrderAddClientDiapasonCombobox()
        {
            OrderAddClientDiapasoneComboBox.Items.Clear();
            OrderAddClientSelectComboBox.Items.Clear();
            OrderAddClientSelectComboBox.Text = "";
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double clientPart = 0;
                if ((from c in db.Clients select c.Id).Count() != 0)
                {
                    long clientCount = (from c in db.Clients select c.Id).Max();
                    if (clientCount % part == 0)
                    {
                        clientPart = clientCount / part;
                    }
                    else
                    {
                        clientPart = (clientCount / part) + 1;
                    }

                    for (int i = 0; i < clientPart; i++)
                    {
                        OrderAddClientDiapasoneComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    OrderAddClientDiapasoneComboBox.DroppedDown = true;
                    OrderAddClientSelectComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }
        void LoadOrderAddClientSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddClientDiapasoneComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = OrderAddClientDiapasoneComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        OrderAddClientSelectComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }
        void SplitClientOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddClientSelectComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                clientOrderAdd = db.Clients.Find(id);
            }
        }

        //Transporter
        void LoadOrderAddTransporterDiapasonCombobox()
        {
            OrderAddTransporterDiapasoneComboBox.Items.Clear();
            OrderAddTransporterSelectComboBox.Items.Clear();
            OrderAddTransporterSelectComboBox.Text = "";
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double transporterPart = 0;
                if ((from c in db.Transporters select c.Id).Count() != 0)
                {
                    long transporterCount = (from c in db.Transporters select c.Id).Max();
                    if (transporterCount % part == 0)
                    {
                        transporterPart = transporterCount / part;
                    }
                    else
                    {
                        transporterPart = (transporterCount / part) + 1;
                    }

                    for (int i = 0; i < transporterPart; i++)
                    {
                        OrderAddTransporterDiapasoneComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    OrderAddTransporterDiapasoneComboBox.DroppedDown = true;
                    OrderAddTransporterSelectComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }
        void LoadOrderAddTransporterSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddTransporterDiapasoneComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = OrderAddTransporterDiapasoneComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        OrderAddTransporterSelectComboBox.Items.Add(item.FullName + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }
        void SplitTransporterOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddTransporterSelectComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterOrderAdd = db.Transporters.Find(id);
            }
        }

        //Forwarders
        void LoadOrderAddForwarder1SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    OrderAddForwarder1SelectComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderAddForwarder2SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    OrderAddForwarder2SelectComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }    
        void SplitForwarder1OrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddForwarder1SelectComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarder1OrderAdd = db.Forwarders.Find(id);
            }
        }
        void SplitForwarder2OrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddForwarder2SelectComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarder2OrderAdd = db.Forwarders.Find(id);
            }
        }


        //Adresses
        //void UploadAddressForm()
        //{
        //    if (clientOrderAdd != null)
        //    {
        //        selectUploadAddressesForm = new SelectUploadAddressesForm(clientOrderAdd);
        //        selectUploadAddressesForm.Show();
        //    }
        //    else 
        //    {
        //        MessageBox.Show("Виберіть спочатку клієнта");
        //    }
        //}
        //void DownloadAddressForm()
        //{
        //    if (clientOrderAdd != null)
        //    {
        //        selectDownloadAddressesForm = new SelectDownloadAddressesForm(clientOrderAdd);
        //        selectDownloadAddressesForm.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Виберіть спочатку клієнта");
        //    }
        //}
        //void CustomsAddressForm()
        //{
        //    if (clientOrderAdd != null)
        //    {
        //        selectCustomsAddressesForm = new SelectCustomsAddressesForm(clientOrderAdd);
        //        selectCustomsAddressesForm.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Виберіть спочатку клієнта");
        //    }
        //}
        //void UncustomsAddressForm()
        //{
        //    if (clientOrderAdd != null)
        //    {
        //        selectUncustomsAddressesForm = new SelectUncustomsAddressesForm(clientOrderAdd);
        //        selectUncustomsAddressesForm.Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Виберіть спочатку клієнта");
        //    }
        //}

        void SplitAdditionalTermOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddAdditionalTermsSelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                additionalTermOrderAdd = db.AdditionalTerms.Find(id);
            }
        }
        void LoadOrderAddAdditionalTermsSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from at in db.AdditionalTerms
                            orderby at.Id
                            select at;
                foreach (var item in query)
                {
                    OrderAddAdditionalTermsSelectComboBox.Items.Add(item.Type+ " [" + item.Id + "]");
                }
            }
        }

        void SplitCargoOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddCargoSelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                cargoOrderAdd = db.Cargoes.Find(id);
            }
        }
        void LoadOrderAddCargoSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from c in db.Cargoes
                            orderby c.Id
                            select c;
                foreach (var item in query)
                {
                    OrderAddCargoSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void SplitFineForDelayOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddFineForDelaySelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                fineForDelayOrderAdd = db.FineForDelays.Find(id);
            }
        }
        void LoadOrderAddFineForDelaySelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.FineForDelays
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    OrderAddFineForDelaySelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }

        void SplitTirCmrAddOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddTirCmrSelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                tirCmrOrderAdd = db.TirCmrs.Find(id);
            }
        }
        void LoadOrderAddTirCmrSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from Tc in db.TirCmrs
                            orderby Tc.Id
                            select Tc;
                foreach (var item in query)
                {
                    OrderAddTirCmrSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void SplitOrderDenyOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddDenyFineSelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                orderDenyOrderAdd = db.OrderDenies.Find(id);
            }
        }
        void LoadOrderAddDenyFineSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from od in db.OrderDenies
                            orderby od.Id
                            select od;
                foreach (var item in query)
                {
                    OrderAddDenyFineSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        
        void SplitPaymentOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddPaymentTermsSelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                paymentOrderAdd = db.Payments.Find(id);
            }
        }
        void LoadOrderAddPaymentSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from p in db.Payments
                            orderby p.Id
                            select p;
                foreach (var item in query)
                {
                    OrderAddPaymentTermsSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void SplitRegularyDelayOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddRegularyDelaySelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                regularyDelayOrderAdd = db.RegularyDelays.Find(id);
            }
        }
        void LoadOrderAddRegularyDelaySelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from rd in db.RegularyDelays
                            orderby rd.Id
                            select rd;
                foreach (var item in query)
                {
                    OrderAddRegularyDelaySelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void SplitCubeOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddCubeSelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                cubeOrderAdd = db.Cubes.Find(id);
            }
        }
        void LoadOrderAddCubeSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from c in db.Cubes
                            orderby c.Id
                            select c;
                foreach (var item in query)
                {
                    OrderAddCubeSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void SplitTrailerOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddTrailerSelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                trailerOrderAdd = db.Trailers.Find(id);
            }
        }
        void LoadOrderAddTrailerSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.Trailers
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    OrderAddTrailerSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadingForm1OrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddLoadingForm1SelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                loadingForm1OrderAdd = db.LoadingForms.Find(id);
            }
        }
        void SplitLoadingForm2OrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = OrderAddLoadingForm2SelectComboBox.SelectedItem.ToString();
                string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedText[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                loadingForm2OrderAdd = db.LoadingForms.Find(id);
            }
        }
        void LoadOrderAddLoadingForm1SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from lf in db.LoadingForms
                            orderby lf.Id
                            select lf;
                foreach (var item in query)
                {
                    OrderAddLoadingForm1SelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderAddLoadingForm2SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from lf in db.LoadingForms
                            orderby lf.Id
                            select lf;
                foreach (var item in query)
                {
                    OrderAddLoadingForm2SelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }

    }
}
