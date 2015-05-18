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
    }
}
