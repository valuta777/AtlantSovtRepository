using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Globalization;
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
        Forwarder forwarder3OrderAdd;

        AdditionalTerm additionalTermOrderAdd;
        Cargo cargoOrderAdd;        
        FineForDelay fineForDelayOrderAdd;
        TirCmr tirCmrOrderAdd;
        OrderDeny orderDenyOrderAdd;
        Payment paymentOrderAdd;
        RegularyDelay regularyDelayOrderAdd;
        Cube cubeOrderAdd;
        Staff staffOrderAdd;
        Trailer trailerOrderAdd;

        LoadingForm loadingForm1OrderAdd;
        LoadingForm loadingForm2OrderAdd;
        
        SelectUploadAddressesForm selectUploadAddressesForm;
        SelectDownloadAddressesForm selectDownloadAddressesForm;
        SelectCustomsAddressesForm selectCustomsAddressesForm;
        SelectUncustomsAddressesForm selectUncustomsAddressesForm;

        void OrderAdd() 
        {
            using (var db = new AtlantSovtContext())
            {
                Order New_Order = new Order
                {
                    AdditionalTermsId = (additionalTermOrderAdd != null) ? (long?)additionalTermOrderAdd.Id : null,
                    ADRNumber = (OrderAddADRSelectComboBox.Text != "") ? (int?)Convert.ToInt32(OrderAddADRSelectComboBox.Text) : null,
                    TirCmrId = (tirCmrOrderAdd != null) ? (long?)tirCmrOrderAdd.Id : null,
                    CargoId = (cargoOrderAdd != null) ? (long?)cargoOrderAdd.Id : null,
                    CargoWeight = (OrderAddWeightTextBox.Text != "") ? (double?)Double.Parse(OrderAddWeightTextBox.Text, CultureInfo.InvariantCulture) : null,
                    ClientId = (clientOrderAdd != null) ? (long?)clientOrderAdd.Id : null,
                    CubeId = (cubeOrderAdd != null) ? (long?)cubeOrderAdd.Id : null,
                    StaffId = (staffOrderAdd != null) ? (long?)staffOrderAdd.Id : null,
                    FineForDelaysId = (fineForDelayOrderAdd != null) ? (long?)fineForDelayOrderAdd.Id : null,
                    Freight = (OrderAddFreightTextBox.Text != "") ? OrderAddFreightTextBox.Text : null,
                    OrderDenyId = (orderDenyOrderAdd != null) ? (long?)orderDenyOrderAdd.Id : null,
                    PaymentTermsId = (paymentOrderAdd != null) ? (long?)paymentOrderAdd.Id : null,
                    RegularyDelaysId = (regularyDelayOrderAdd != null) ? (long?)regularyDelayOrderAdd.Id : null,
                    TrailerId = (trailerOrderAdd != null) ? (long?)trailerOrderAdd.Id : null,
                    TransporterId = (transporterOrderAdd != null) ? (long?)transporterOrderAdd.Id : null,
                    Date = OrderAddDateSelectDateTimePicker.Value,

                    DownloadDateFrom =  OrderAddDownloadDateFromTimePicker.Checked ? (DateTime?)OrderAddDownloadDateFromTimePicker.Value : null,
                    UploadDateFrom = OrderAddUploadDateFromTimePicker.Checked ? (DateTime?)OrderAddUploadDateFromTimePicker.Value : null,

                    DownloadDateTo = OrderAddDownloadDateToTimePicker.Checked ? (DateTime?)OrderAddDownloadDateToTimePicker.Value : null,
                    UploadDateTo = OrderAddUploadDateToTimePicker.Checked ? (DateTime?)OrderAddUploadDateToTimePicker.Value : null,

                    State = null,

                   
                    Language = (OrderAddLanduageSelectComboBox.SelectedIndex != -1 && OrderAddLanduageSelectComboBox.Text == OrderAddLanduageSelectComboBox.SelectedItem.ToString()) ? (OrderAddLanduageSelectComboBox.SelectedIndex == 0) ? (byte?)0 : (OrderAddLanduageSelectComboBox.SelectedIndex == 1) ? (byte?)1 : (byte?)2 : null
                };
                try
                {
                    db.Orders.Add(New_Order);
                    db.Entry(New_Order).State = EntityState.Added;
                    db.SaveChanges();
                    string massage = AtlantSovt.Properties.Resources.Заявку_успішно_створено;

                    massage += BridgeAddes(New_Order);

                    MessageBox.Show(massage);
                }
                catch (DbEntityValidationException ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_при_створенні_заявки + ex.Message);
                }
            }
        }
        string BridgeAddes(Order New_Order)
        {
            string returnMessage = string.Empty;
            using (var db = new AtlantSovtContext())
            {
                if (loadingForm1OrderAdd != null)
                {
                    try
                    {
                        OrderLoadingForm New_OrderLoadingForm1 = new OrderLoadingForm
                        {
                            LoadingFormId = loadingForm1OrderAdd.Id,
                            IsFirst = true
                        };
                        db.Orders.Find(New_Order.Id).OrderLoadingForms.Add(New_OrderLoadingForm1);
                        db.Entry(New_OrderLoadingForm1).State = EntityState.Added;
                        db.SaveChanges();
                        returnMessage += AtlantSovt.Properties.Resources.Успішно_вибрано_першу_форму_завантаження;
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_1_форма_завантаження + ex.Message);
                    }
                }
                if (loadingForm2OrderAdd != null)
                {
                    try
                    {
                        OrderLoadingForm New_OrderLoadingForm2 = new OrderLoadingForm
                        {
                            LoadingFormId = loadingForm2OrderAdd.Id,
                            IsFirst = false
                        };
                        db.Orders.Find(New_Order.Id).OrderLoadingForms.Add(New_OrderLoadingForm2);
                        db.Entry(New_OrderLoadingForm2).State = EntityState.Added;
                        db.SaveChanges();
                        returnMessage += AtlantSovt.Properties.Resources.Успішно_вибрано_другу_форму_завантаження;
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_2_форма_завантаження + ex.Message);
                    }
                }

                //адреси              
                if (selectDownloadAddressesForm != null)
                {
                    returnMessage += selectDownloadAddressesForm.DownloadAddressesSelect(db.Orders.Find(New_Order.Id));
                    selectDownloadAddressesForm = null;
                }
                if (selectUploadAddressesForm != null)
                {
                    returnMessage += selectUploadAddressesForm.UploadAddressesSelect(db.Orders.Find(New_Order.Id));
                    selectUploadAddressesForm = null;
                }
                if (selectCustomsAddressesForm != null)
                {
                    returnMessage += selectCustomsAddressesForm.CustomsAddressesSelect(db.Orders.Find(New_Order.Id));
                    selectCustomsAddressesForm = null;
                }
                if (selectUncustomsAddressesForm != null)
                {
                    returnMessage += selectUncustomsAddressesForm.UncustomsAddressesSelect(db.Orders.Find(New_Order.Id));
                    selectUncustomsAddressesForm = null;
                }

                //експедитори
                if (forwarder1OrderAdd != null)
                {
                    try
                    {
                        ForwarderOrder New_Forwarder1Order = new ForwarderOrder
                        {
                            ForwarderId = forwarder1OrderAdd.Id,
                            IsFirst = 1
                        };
                        db.Orders.Find(New_Order.Id).ForwarderOrders.Add(New_Forwarder1Order);
                        db.Entry(New_Forwarder1Order).State = EntityState.Added;
                        db.SaveChanges();

                        returnMessage += AtlantSovt.Properties.Resources.Успішно_вибрано_першого_експедитора;
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_1_експедитор + ex.Message);
                    }

                }
                if (forwarder2OrderAdd != null)
                {
                    try
                    {
                        ForwarderOrder New_Forwarder2Order = new ForwarderOrder
                        {
                            ForwarderId = forwarder2OrderAdd.Id,
                            IsFirst = 2
                        };
                        db.Orders.Find(New_Order.Id).ForwarderOrders.Add(New_Forwarder2Order);
                        db.Entry(New_Forwarder2Order).State = EntityState.Added;
                        db.SaveChanges();
                        returnMessage += AtlantSovt.Properties.Resources.Успішно_вибрано_другого_експедитора;
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_2_експедитор + ex.Message);
                    }
                }
                if (forwarder3OrderAdd != null)
                {
                    try
                    {
                        ForwarderOrder New_Forwarder3Order = new ForwarderOrder
                        {
                            ForwarderId = forwarder3OrderAdd.Id,
                            IsFirst = 3
                        };
                        db.Orders.Find(New_Order.Id).ForwarderOrders.Add(New_Forwarder3Order);
                        db.Entry(New_Forwarder3Order).State = EntityState.Added;
                        db.SaveChanges();
                        returnMessage += AtlantSovt.Properties.Resources.Успішно_вибрано_третього_експедитора;
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_3_експедитор + ex.Message);
                    }
                }

            }
            return returnMessage;
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
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
                }
            }
        }
        void LoadOrderAddClientSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddClientDiapasoneComboBox.Text == "")
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
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
                if (OrderAddClientSelectComboBox.SelectedIndex != -1 && OrderAddClientSelectComboBox.Text == OrderAddClientSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddClientSelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    clientOrderAdd = db.Clients.Find(id);
                }
                else 
                {
                    clientOrderAdd = null;
                }
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
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
                }
            }
        }
        void LoadOrderAddTransporterSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddTransporterDiapasoneComboBox.Text == "")
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
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
                if (OrderAddTransporterSelectComboBox.SelectedIndex != -1 && OrderAddTransporterSelectComboBox.Text == OrderAddTransporterSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddTransporterSelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    transporterOrderAdd = db.Transporters.Find(id);
                }
                else 
                {
                    transporterOrderAdd = null;
                }
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

        void LoadOrderAddForwarder3SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    OrderAddForwarder3SelectComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }
        void SplitForwarder1OrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddForwarder1SelectComboBox.SelectedIndex != -1 && OrderAddForwarder1SelectComboBox.Text == OrderAddForwarder1SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddForwarder1SelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarder1OrderAdd = db.Forwarders.Find(id);
                }
                else 
                {
                    forwarder1OrderAdd = null;
                }
            }
        }
        void SplitForwarder2OrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddForwarder2SelectComboBox.SelectedIndex != -1 && OrderAddForwarder2SelectComboBox.Text == OrderAddForwarder2SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddForwarder2SelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarder2OrderAdd = db.Forwarders.Find(id);
                }
                else 
                {
                    forwarder2OrderAdd = null;
                }
            }
        }

        void SplitForwarder3OrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddForwarder3SelectComboBox.SelectedIndex != -1 && OrderAddForwarder3SelectComboBox.Text == OrderAddForwarder3SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddForwarder3SelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarder3OrderAdd = db.Forwarders.Find(id);
                }
                else
                {
                    forwarder3OrderAdd = null;
                }
            }
        }
        // Adresses
        void UploadAddressForm()
        {
            if (clientOrderAdd != null)
            {
                if (selectUploadAddressesForm == null || selectUploadAddressesForm.IsDisposed)
                {
                    selectUploadAddressesForm = new SelectUploadAddressesForm(clientOrderAdd);
                    selectUploadAddressesForm.Show();
                }
                else
                {
                    selectUploadAddressesForm.Show();
                    selectUploadAddressesForm.Focus();
                }
            }
            else 
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_клієнта);
            }
        }
        void DownloadAddressForm()
        {
            if (clientOrderAdd != null)
            {
                if (selectDownloadAddressesForm == null || selectDownloadAddressesForm.IsDisposed)
                {
                    selectDownloadAddressesForm = new SelectDownloadAddressesForm(clientOrderAdd);
                    selectDownloadAddressesForm.Show();
                }
                else
                {
                    selectDownloadAddressesForm.Show();
                    selectDownloadAddressesForm.Focus();
                }
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_клієнта);
            }
        }
        void CustomsAddressForm()
        {
            if (clientOrderAdd != null)
            {
                if (selectCustomsAddressesForm == null || selectCustomsAddressesForm.IsDisposed)
                {
                    selectCustomsAddressesForm = new SelectCustomsAddressesForm(clientOrderAdd);
                    selectCustomsAddressesForm.Show();
                }
                else
                {
                    selectCustomsAddressesForm.Show();
                    selectCustomsAddressesForm.Focus();
                }
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_клієнта);
            }
        }
        void UncustomsAddressForm()
        {
            if (clientOrderAdd != null)
            {
                if (selectUncustomsAddressesForm == null || selectUncustomsAddressesForm.IsDisposed)
                {
                    selectUncustomsAddressesForm = new SelectUncustomsAddressesForm(clientOrderAdd);
                    selectUncustomsAddressesForm.Show();
                }
                else
                {
                    selectUncustomsAddressesForm.Show();
                    selectUncustomsAddressesForm.Focus();
                }
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_клієнта);
            }
        }
        //інші комбобокси
        void SplitAdditionalTermOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddAdditionalTermsSelectComboBox.SelectedIndex != -1 && OrderAddAdditionalTermsSelectComboBox.Text == OrderAddAdditionalTermsSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddAdditionalTermsSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    additionalTermOrderAdd = db.AdditionalTerms.Find(id);
                }
                else                
                {
                    additionalTermOrderAdd = null;
                }
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
                if (OrderAddCargoSelectComboBox.SelectedIndex != -1 && OrderAddCargoSelectComboBox.Text == OrderAddCargoSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddCargoSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    cargoOrderAdd = db.Cargoes.Find(id);
                }
                else 
                {
                    cargoOrderAdd = null;
                }
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

                if (OrderAddFineForDelaySelectComboBox.SelectedIndex != -1 && OrderAddFineForDelaySelectComboBox.Text == OrderAddFineForDelaySelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddFineForDelaySelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    fineForDelayOrderAdd = db.FineForDelays.Find(id);
                }
                else
                {
                    fineForDelayOrderAdd = null;
                }
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
                if (OrderAddTirCmrSelectComboBox.SelectedIndex != -1 && OrderAddTirCmrSelectComboBox.Text == OrderAddTirCmrSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddTirCmrSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    tirCmrOrderAdd = db.TirCmrs.Find(id);
                }
                else
                {
                    tirCmrOrderAdd = null;
                }
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
                if (OrderAddDenyFineSelectComboBox.SelectedIndex != -1 && OrderAddDenyFineSelectComboBox.Text == OrderAddDenyFineSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddDenyFineSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    orderDenyOrderAdd = db.OrderDenies.Find(id);
                }
                else 
                {
                    orderDenyOrderAdd = null;
                }
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
                if (OrderAddPaymentTermsSelectComboBox.SelectedIndex != -1 && OrderAddPaymentTermsSelectComboBox.Text == OrderAddPaymentTermsSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddPaymentTermsSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    paymentOrderAdd = db.Payments.Find(id);
                }
                else 
                {
                    paymentOrderAdd = null;
                }
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
                if (OrderAddRegularyDelaySelectComboBox.SelectedIndex != -1 && OrderAddRegularyDelaySelectComboBox.Text == OrderAddRegularyDelaySelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddRegularyDelaySelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    regularyDelayOrderAdd = db.RegularyDelays.Find(id);
                }
                else 
                {
                    regularyDelayOrderAdd = null;
                }
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
                if (OrderAddCubeSelectComboBox.SelectedIndex != -1 && OrderAddCubeSelectComboBox.Text == OrderAddCubeSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddCubeSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    cubeOrderAdd = db.Cubes.Find(id);
                }
                else
                {
                    cubeOrderAdd = null;
                }
            }
        }
        void SplitStaffOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddStaffComboBox.SelectedIndex != -1 && OrderAddStaffComboBox.Text == OrderAddStaffComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddStaffComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    staffOrderAdd = db.Staffs.Find(id);
                }
                else
                {
                    staffOrderAdd = null;
                }
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
        void LoadOrderAddStaffSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from st in db.Staffs
                            orderby st.Id
                            select st;
                foreach (var item in query)
                {
                    OrderAddStaffComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
                
            }
        }
        void SplitTrailerOrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddTrailerSelectComboBox.SelectedIndex != -1 && OrderAddTrailerSelectComboBox.Text == OrderAddTrailerSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddTrailerSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    trailerOrderAdd = db.Trailers.Find(id);
                    
                }
                else
                {
                    trailerOrderAdd = null;
                }
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
                if (OrderAddLoadingForm1SelectComboBox.SelectedIndex != -1 && OrderAddLoadingForm1SelectComboBox.Text == OrderAddLoadingForm1SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddLoadingForm1SelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    loadingForm1OrderAdd = db.LoadingForms.Find(id);
                }
                else 
                {
                    loadingForm1OrderAdd = null;                                 
                }
            }
        }
        void SplitLoadingForm2OrderAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderAddLoadingForm2SelectComboBox.SelectedIndex != -1 && OrderAddLoadingForm2SelectComboBox.Text == OrderAddLoadingForm2SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderAddLoadingForm2SelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    loadingForm2OrderAdd = db.LoadingForms.Find(id);
                }
                else
                {
                    loadingForm2OrderAdd = null;
                }
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

        //ORDER UPDATE

        Order updateOrder;

        Client clientOrderUpdate;
        Transporter transporterOrderUpdate;
        Forwarder forwarder1OrderUpdate;
        Forwarder forwarder2OrderUpdate;
        Forwarder forwarder3OrderUpdate;

        AdditionalTerm additionalTermOrderUpdate;
        Cargo cargoOrderUpdate;

        FineForDelay fineForDelayOrderUpdate;
        TirCmr tirCmrOrderUpdate;
        OrderDeny orderDenyOrderUpdate;
        Payment paymentOrderUpdate;
        RegularyDelay regularyDelayOrderUpdate;

        Cube cubeOrderUpdate;
        Staff staffOrderUpdate;
        Trailer trailerOrderUpdate;

        LoadingForm loadingForm1OrderUpdate;
        LoadingForm loadingForm2OrderUpdate;
        

        SelectUploadAddressesForm updateUploadAddressesForm;
        SelectDownloadAddressesForm updateDownloadAddressesForm;
        SelectCustomsAddressesForm updateCustomsAddressesForm;
        SelectUncustomsAddressesForm updateUncustomsAddressesForm;

        bool IsModified = false;

        void ClearAllBoxesOrderUpdate() 
        {
            OrderUpdateAdditionalTermsSelectComboBox.SelectedIndex = -1;
            OrderUpdatePaymentTermsSelectComboBox.SelectedIndex = -1;
            OrderUpdateDenyFineSelectComboBox.SelectedIndex = -1;
            OrderUpdateCargoSelectComboBox.SelectedIndex = -1;
            OrderUpdateRegularyDelaySelectComboBox.SelectedIndex = -1;
            OrderUpdateTrailerSelectComboBox.SelectedIndex = -1;
            OrderUpdateCubeSelectComboBox.SelectedIndex = -1;
            OrderUpdateStaffSelectComboBox.SelectedIndex = -1;
            OrderUpdateTirCmrSelectComboBox.SelectedIndex = -1;
            OrderUpdateADRSelectComboBox.SelectedIndex = -1;
            OrderUpdateClientSelectComboBox.SelectedIndex = -1;
            OrderUpdateTransporterSelectComboBox.SelectedIndex = -1;
            OrderUpdateTransporterDiapasoneComboBox.SelectedIndex = -1;
            OrderUpdateForwarder3SelectComboBox.SelectedIndex = -1;
            OrderUpdateForwarder2SelectComboBox.SelectedIndex = -1;
            OrderUpdateForwarder1SelectComboBox.SelectedIndex = -1;
            OrderUpdateFineForDelaySelectComboBox.SelectedIndex = -1;
            OrderUpdateClientDiapasoneComboBox.SelectedIndex = -1;
            OrderUpdateLoadingForm1SelectComboBox.SelectedIndex = -1;
            OrderUpdateLoadingForm2SelectComboBox.SelectedIndex = -1;

            OrderUpdateAdditionalTermsSelectComboBox.Items.Clear();
            OrderUpdatePaymentTermsSelectComboBox.Items.Clear();
            OrderUpdateDenyFineSelectComboBox.Items.Clear();
            OrderUpdateCargoSelectComboBox.Items.Clear();
            OrderUpdateRegularyDelaySelectComboBox.Items.Clear();
            OrderUpdateTrailerSelectComboBox.Items.Clear();
            OrderUpdateCubeSelectComboBox.Items.Clear();
            OrderUpdateStaffSelectComboBox.Items.Clear();
            OrderUpdateTirCmrSelectComboBox.Items.Clear();
            OrderUpdateClientSelectComboBox.Items.Clear();
            OrderUpdateTransporterSelectComboBox.Items.Clear();
            OrderUpdateTransporterDiapasoneComboBox.Items.Clear();
            OrderUpdateForwarder3SelectComboBox.Items.Clear();
            OrderUpdateForwarder2SelectComboBox.Items.Clear();
            OrderUpdateForwarder1SelectComboBox.Items.Clear();
            OrderUpdateClientDiapasoneComboBox.Items.Clear();
            OrderUpdateFineForDelaySelectComboBox.Items.Clear();
            OrderUpdateLoadingForm1SelectComboBox.Items.Clear();
            OrderUpdateLoadingForm2SelectComboBox.Items.Clear();

            OrderUpdateDateDateTimePicker.Checked = false;
                             
            OrderUpdateDownloadDateFromTimePicker.Checked = false;
            OrderUpdateUploadDateFromTimePicker.Checked = false;

            OrderUpdateDownloadDateToTimePicker.Checked = false;
            OrderUpdateUploadDateToTimePicker.Checked = false;


            OrderUpdateWeightTextBox.Text = "";
            OrderUpdateFreightTextBox.Text = "";
            IsModified = false;
        }
        void LoadOrderUpdateOrderSelectComboBox()
        {
            if (OrderUpdateShowNoActiveCheckBox.CheckState == CheckState.Unchecked)
            {
                using (var db = new AtlantSovtContext())
                {
                    var query = from or in db.Orders
                                orderby or.Id
                                where (or.State !=false)&&(or.Date.Value.Month == OrderUpdateDiapasoneDateTimePicker.Value.Month)
                                &&    (or.Date.Value.Year == OrderUpdateDiapasoneDateTimePicker.Value.Year)
                                select or;
                    foreach (var item in query)
                    {
                        if (item.Client != null)
                        {
                            OrderUpdateOrderSelectComboBox.Items.Add(item.Client.Name + " ," + item.Date + " [" + item.Id + "]");
                        }
                        else 
                        {
                            OrderUpdateOrderSelectComboBox.Items.Add("" + " ," + item.Date + " [" + item.Id + "]");
                        }
                    }
                }
            }
            else 
            {
                using (var db = new AtlantSovtContext())
                {
                    var query = from or in db.Orders
                                orderby or.Id
                                where (or.Date.Value.Month == OrderUpdateDiapasoneDateTimePicker.Value.Month)
                                && (or.Date.Value.Year == OrderUpdateDiapasoneDateTimePicker.Value.Year)
                                select or;
                    foreach (var item in query)
                    {
                        if (item.Client != null)
                        {
                            OrderUpdateOrderSelectComboBox.Items.Add(item.Client.Name + " ," + item.Date + " [" + item.Id + "]");
                        }
                        else
                        {
                            OrderUpdateOrderSelectComboBox.Items.Add(" ," + item.Date + " [" + item.Id + "]");
                        }
                    }
                }
                
            }
        }
        void SplitOrderOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateOrderSelectComboBox.SelectedIndex != -1 && OrderUpdateOrderSelectComboBox.Text == OrderUpdateOrderSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateOrderSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    updateOrder = db.Orders.Find(id);
                }
                else
                {
                    updateOrder = null;
                    ClearAllBoxesOrderUpdate();
                }
            }
        }
        void LoadAllFieldsOrderUpdate()
        {
            LoadOrderUpdateForwarder3SelectComboBox();
            LoadOrderUpdateForwarder2SelectComboBox();
            LoadOrderUpdateForwarder1SelectComboBox();
            LoadOrderUpdateAdditionalTermsSelectComboBox();
            LoadOrderUpdateCargoSelectComboBox();
            LoadOrderUpdateFineForDelaySelectComboBox();
            LoadOrderUpdateTirCmrSelectComboBox();
            LoadOrderUpdateDenyFineSelectComboBox();
            LoadOrderUpdatePaymentSelectComboBox();
            LoadOrderUpdateRegularyDelaySelectComboBox();
            LoadOrderUpdateCubeSelectComboBox();
            LoadOrderUpdateStaffSelectComboBox();
            LoadOrderUpdateTrailerSelectComboBox();
            LoadOrderUpdateLoadingForm1SelectComboBox();
            LoadOrderUpdateLoadingForm2SelectComboBox();
            if (updateOrder != null)
            {
                if (updateOrder.ClientId.HasValue)
                {
                    LoadOrderUpdateClientDiapasonCombobox();
                }
                if (updateOrder.TransporterId.HasValue)
                {
                    LoadOrderUpdateTransporterDiapasonCombobox();
                }
            }

         
        }
        void SelectAllFieldsOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (updateOrder != null)
                {
                    updateOrder = db.Orders.Find(updateOrder.Id);
                    if (updateOrder.AdditionalTerm != null)
                    {
                        OrderUpdateAdditionalTermsSelectComboBox.SelectedIndex =
                            OrderUpdateAdditionalTermsSelectComboBox.FindString(updateOrder.AdditionalTerm.Type + " [" + updateOrder.AdditionalTerm.Id + ']');
                        SplitAdditionalTermOrderUpdate();                        
                    }
                    else
                    {
                        OrderUpdateAdditionalTermsSelectComboBox.SelectedIndex = -1;
                    }

                    if (updateOrder.Payment != null)
                    {
                        OrderUpdatePaymentTermsSelectComboBox.SelectedIndex =
                           OrderUpdatePaymentTermsSelectComboBox.FindString(updateOrder.Payment.Type + " [" + updateOrder.Payment.Id + ']');
                        SplitPaymentOrderUpdate();                        
                    }
                    else
                    {
                        OrderUpdatePaymentTermsSelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.OrderDeny != null)
                    {
                        OrderUpdateDenyFineSelectComboBox.SelectedIndex =
                            OrderUpdateDenyFineSelectComboBox.FindString(updateOrder.OrderDeny.Type + " [" + updateOrder.OrderDeny.Id + ']');
                        SplitOrderDenyOrderUpdate();                        
                    }
                    else
                    {
                        OrderUpdateDenyFineSelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.Cargo != null)
                    {
                        OrderUpdateCargoSelectComboBox.SelectedIndex =
                            OrderUpdateCargoSelectComboBox.FindString(updateOrder.Cargo.Type + " [" + updateOrder.Cargo.Id + ']');
                        SplitCargoOrderUpdate();
                    }                      
                    else
                    {
                        OrderUpdateCargoSelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.FineForDelay != null)
                    {
                        OrderUpdateFineForDelaySelectComboBox.SelectedIndex =
                            OrderUpdateFineForDelaySelectComboBox.FindString(updateOrder.FineForDelay.Type + " [" + updateOrder.FineForDelay.Id + ']');
                        SplitFineForDelayOrderUpdate();                        
                    }                    
                    else
                    {
                        OrderUpdateCargoSelectComboBox.SelectedIndex = -1;
                    }

                    if (updateOrder.RegularyDelay != null)
                    {
                        OrderUpdateRegularyDelaySelectComboBox.SelectedIndex =
                            OrderUpdateRegularyDelaySelectComboBox.FindString(updateOrder.RegularyDelay.Type + " [" + updateOrder.RegularyDelay.Id + ']');                       
                        SplitRegularyDelayOrderUpdate(); 
                    }
                    else
                    {
                        OrderUpdateRegularyDelaySelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.Trailer != null)
                    {
                        OrderUpdateTrailerSelectComboBox.SelectedIndex =
                            OrderUpdateTrailerSelectComboBox.FindString(updateOrder.Trailer.Type + " [" + updateOrder.Trailer.Id + ']');
                        SplitTrailerOrderUpdate();                        
                    }
                    else
                    {
                        OrderUpdateTrailerSelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.Cube != null)
                    {
                        OrderUpdateCubeSelectComboBox.SelectedIndex =
                            OrderUpdateCubeSelectComboBox.FindString(updateOrder.Cube.Type + " [" + updateOrder.Cube.Id + ']');                        
                        SplitCubeOrderUpdate();                        
                    }
                    else
                    {
                        OrderUpdateCubeSelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.TirCmr != null)
                    {
                        OrderUpdateTirCmrSelectComboBox.SelectedIndex =
                            OrderUpdateTirCmrSelectComboBox.FindString(updateOrder.TirCmr.Type + " [" + updateOrder.TirCmr.Id + ']');
                        SplitTirCmrUpdateOrderUpdate();                        
                    }
                    else
                    {
                        OrderUpdateTirCmrSelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.ForwarderOrders.Where(f1 => f1.IsFirst == 1).Count() != 0)
                    {
                        OrderUpdateForwarder1SelectComboBox.SelectedIndex =
                            OrderUpdateForwarder1SelectComboBox.FindString(updateOrder.ForwarderOrders.Where(f1 => f1.IsFirst == 1).FirstOrDefault().Forwarder.Name
                            + " , " + (updateOrder.ForwarderOrders.Where(f1 => f1.IsFirst == 1).FirstOrDefault().Forwarder.Director)
                            + " [" + (updateOrder.ForwarderOrders.Where(f1 => f1.IsFirst == 1).FirstOrDefault().Forwarder.Id + "]"));
                        SplitForwarder1OrderUpdate();
                    }
                    else
                    {
                        OrderUpdateForwarder1SelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.ForwarderOrders.Where(f2 => f2.IsFirst == 2).Count() != 0)
                    {
                        OrderUpdateForwarder2SelectComboBox.SelectedIndex =
                            OrderUpdateForwarder2SelectComboBox.FindString(updateOrder.ForwarderOrders.Where(f2 => f2.IsFirst == 2).FirstOrDefault().Forwarder.Name
                            + " , " + (updateOrder.ForwarderOrders.Where(f2 => f2.IsFirst == 2).FirstOrDefault().Forwarder.Director)
                            + " [" + (updateOrder.ForwarderOrders.Where(f2 => f2.IsFirst == 2).FirstOrDefault().Forwarder.Id + "]"));
                        SplitForwarder2OrderUpdate();
                    }
                    else
                    {
                        OrderUpdateForwarder2SelectComboBox.SelectedIndex = -1;
                    }

                    if (updateOrder.ForwarderOrders.Where(f3 => f3.IsFirst == 3).Count() != 0)
                    {
                        OrderUpdateForwarder3SelectComboBox.SelectedIndex =
                            OrderUpdateForwarder3SelectComboBox.FindString(updateOrder.ForwarderOrders.Where(f3 => f3.IsFirst == 3).FirstOrDefault().Forwarder.Name
                            + " , " + (updateOrder.ForwarderOrders.Where(f3 => f3.IsFirst == 3).FirstOrDefault().Forwarder.Director)
                            + " [" + (updateOrder.ForwarderOrders.Where(f3 => f3.IsFirst == 3).FirstOrDefault().Forwarder.Id + "]"));
                        SplitForwarder3OrderUpdate();
                    }
                    else
                    {
                        OrderUpdateForwarder3SelectComboBox.SelectedIndex = -1;
                    }

                    if (updateOrder.OrderLoadingForms.Where(lf1 => lf1.IsFirst == true).Count() != 0)
                    {
                        OrderUpdateLoadingForm1SelectComboBox.SelectedIndex =
                            OrderUpdateLoadingForm1SelectComboBox.FindString(updateOrder.OrderLoadingForms.Where(lf1 => lf1.IsFirst == true).FirstOrDefault().LoadingForm.Type + " [" + updateOrder.OrderLoadingForms.Where(lf1 => lf1.IsFirst == true).FirstOrDefault().LoadingForm.Id + ']');
                        SplitLoadingForm1OrderUpdate();                        
                    }
                    else
                    {
                        OrderUpdateLoadingForm1SelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.OrderLoadingForms.Where(lf2 => lf2.IsFirst == false).Count() != 0)
                    {
                        OrderUpdateLoadingForm2SelectComboBox.SelectedIndex =
                            OrderUpdateLoadingForm2SelectComboBox.FindString(updateOrder.OrderLoadingForms.Where(lf2 => lf2.IsFirst == false).FirstOrDefault().LoadingForm.Type + " [" + updateOrder.OrderLoadingForms.Where(lf2 => lf2.IsFirst == false).FirstOrDefault().LoadingForm.Id + ']');
                        SplitLoadingForm2OrderUpdate();
                    }
                    else
                    {
                        OrderUpdateLoadingForm2SelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.ADRNumber != null)
                    {
                        OrderUpdateADRSelectComboBox.SelectedIndex =
                            OrderUpdateADRSelectComboBox.FindString(updateOrder.ADRNumber.ToString());
                    }
                    else
                    {
                        OrderUpdateADRSelectComboBox.SelectedIndex = -1;
                    }
                    //
                    if (updateOrder.Staff != null)
                    {
                        OrderUpdateStaffSelectComboBox.SelectedIndex =
                            OrderUpdateStaffSelectComboBox.FindString(updateOrder.Staff.Type + " [" + updateOrder.Staff.Id + ']');
                        SplitStaffOrderUpdate();
                    }
                    else
                    {
                        OrderUpdateCubeSelectComboBox.SelectedIndex = -1;
                    }

                    //
                    if (updateOrder.Language != null)
                    {
                       OrderUpdateLanguageSelectComboBox.SelectedIndex = updateOrder.Language.Value;
                    }
                    else
                    {
                        OrderUpdateLanguageSelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.Client != null)
                    {
                        int from, to = 0;
                        foreach (string s in OrderUpdateClientDiapasoneComboBox.Items)
                        {
                            string[] diapasoneText = s.Split('-');
                            from = Convert.ToInt32(diapasoneText[0]);
                            to = Convert.ToInt32(diapasoneText[1]);
                            if (Enumerable.Range(from, to).Contains(Convert.ToInt32(updateOrder.Client.Id)))
                            {
                                OrderUpdateClientDiapasoneComboBox.SelectedIndex = OrderUpdateClientDiapasoneComboBox.FindString(s);
                                LoadOrderUpdateClientSelectComboBox();
                                OrderUpdateClientSelectComboBox.SelectedIndex = OrderUpdateClientSelectComboBox.FindString(updateOrder.Client.Name + " , " + updateOrder.Client.Director + " [" + updateOrder.Client.Id + "]");
                                SplitClientOrderUpdate();
                                break;
                            }
                        }
                    }
                    else
                    {
                        OrderUpdateClientDiapasoneComboBox.SelectedIndex = -1;
                        OrderUpdateClientSelectComboBox.SelectedIndex = -1;
                    }

                    if (updateOrder.Transporter != null)
                    {
                        int from, to = 0;
                        foreach (string s in OrderUpdateTransporterDiapasoneComboBox.Items)
                        {
                            string[] diapasoneText = s.Split('-');
                            from = Convert.ToInt32(diapasoneText[0]);
                            to = Convert.ToInt32(diapasoneText[1]);
                            if (Enumerable.Range(from, to).Contains(Convert.ToInt32(updateOrder.Transporter.Id))) ///FIX
                            {
                                OrderUpdateTransporterDiapasoneComboBox.SelectedIndex = OrderUpdateTransporterDiapasoneComboBox.FindString(s);
                                LoadOrderUpdateTransporterSelectComboBox();
                                OrderUpdateTransporterSelectComboBox.SelectedIndex = OrderUpdateTransporterSelectComboBox.FindString(updateOrder.Transporter.FullName + " , " + updateOrder.Transporter.Director + " [" + updateOrder.Transporter.Id + "]");
                                SplitTransporterOrderUpdate();                                
                                break;
                            }
                        }
                    }
                    else
                    {
                        OrderUpdateTransporterDiapasoneComboBox.SelectedIndex = -1;
                        OrderUpdateTransporterSelectComboBox.SelectedIndex = -1;
                    }

                    if (updateOrder.Freight != null)
                    {
                        OrderUpdateFreightTextBox.Text = updateOrder.Freight;
                    }
                    else
                    {
                        OrderUpdateFreightTextBox.Clear();
                    }

                    if (updateOrder.CargoWeight != null)
                    {
                        OrderUpdateWeightTextBox.Text = Convert.ToString(updateOrder.CargoWeight);
                    }
                    else
                    {
                        OrderUpdateWeightTextBox.Clear();
                    }

                    if (updateOrder.Date != null)
                    {
                        OrderUpdateDateDateTimePicker.Checked = true;
                        OrderUpdateDateDateTimePicker.Value = updateOrder.Date.Value;
                    }
                    else
                    {
                        OrderUpdateDateDateTimePicker.Checked = false;
                    }
                    
                    //from
                    if (updateOrder.DownloadDateFrom != null)
                    {
                        OrderUpdateDownloadDateFromTimePicker.Checked = true;
                        OrderUpdateDownloadDateFromTimePicker.Value = updateOrder.DownloadDateFrom.Value;
                    }
                    else
                    {
                        OrderUpdateDownloadDateFromTimePicker.Checked = false;
                    }
                    
                    if (updateOrder.UploadDateFrom != null)
                    {
                        OrderUpdateUploadDateFromTimePicker.Checked = true;
                        OrderUpdateUploadDateFromTimePicker.Value = updateOrder.UploadDateFrom.Value;
                    }
                    else
                    {
                        OrderUpdateUploadDateFromTimePicker.Checked = false;
                    }


                    //to
                    if (updateOrder.DownloadDateTo != null)
                    {
                        OrderUpdateDownloadDateToTimePicker.Checked = true;
                        OrderUpdateDownloadDateToTimePicker.Value = updateOrder.DownloadDateTo.Value;
                    }
                    else
                    {
                        OrderUpdateDownloadDateToTimePicker.Checked = false;
                    }

                    if (updateOrder.UploadDateTo != null)
                    {
                        OrderUpdateUploadDateToTimePicker.Checked = true;
                        OrderUpdateUploadDateToTimePicker.Value = updateOrder.UploadDateTo.Value;
                    }
                    else
                    {
                        OrderUpdateUploadDateToTimePicker.Checked = false;
                    }
                }
            }
        }

        //All LOADS
        void LoadOrderUpdateAdditionalTermsSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from at in db.AdditionalTerms
                            orderby at.Id
                            select at;
                foreach (var item in query)
                {
                    OrderUpdateAdditionalTermsSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateCargoSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from c in db.Cargoes
                            orderby c.Id
                            select c;
                foreach (var item in query)
                {
                    OrderUpdateCargoSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateFineForDelaySelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.FineForDelays
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    OrderUpdateFineForDelaySelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateTirCmrSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from Tc in db.TirCmrs
                            orderby Tc.Id
                            select Tc;
                foreach (var item in query)
                {
                    OrderUpdateTirCmrSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateDenyFineSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from od in db.OrderDenies
                            orderby od.Id
                            select od;
                foreach (var item in query)
                {
                    OrderUpdateDenyFineSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdatePaymentSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from p in db.Payments
                            orderby p.Id
                            select p;
                foreach (var item in query)
                {
                    OrderUpdatePaymentTermsSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateCubeSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from c in db.Cubes
                            orderby c.Id
                            select c;
                foreach (var item in query)
                {
                    OrderUpdateCubeSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }

        void LoadOrderUpdateStaffSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from c in db.Staffs
                            orderby c.Id
                            select c;
                foreach (var item in query)
                {
                    OrderUpdateStaffSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateRegularyDelaySelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from rd in db.RegularyDelays
                            orderby rd.Id
                            select rd;
                foreach (var item in query)
                {
                    OrderUpdateRegularyDelaySelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateTrailerSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.Trailers
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    OrderUpdateTrailerSelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateLoadingForm1SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from lf in db.LoadingForms
                            orderby lf.Id
                            select lf;
                foreach (var item in query)
                {
                    OrderUpdateLoadingForm1SelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateLoadingForm2SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from lf in db.LoadingForms
                            orderby lf.Id
                            select lf;
                foreach (var item in query)
                {
                    OrderUpdateLoadingForm2SelectComboBox.Items.Add(item.Type + " [" + item.Id + "]");
                }
            }
        }

        //SPLITS
        void SplitAdditionalTermOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateAdditionalTermsSelectComboBox.SelectedIndex != -1 && OrderUpdateAdditionalTermsSelectComboBox.Text == OrderUpdateAdditionalTermsSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateAdditionalTermsSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    additionalTermOrderUpdate = db.AdditionalTerms.Find(id);
                }
                else
                {
                    additionalTermOrderUpdate = null;
                }
            }
        }        
        void SplitCargoOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateCargoSelectComboBox.SelectedIndex != -1 && OrderUpdateCargoSelectComboBox.Text == OrderUpdateCargoSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateCargoSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    cargoOrderUpdate = db.Cargoes.Find(id);
                }
                else
                {
                    cargoOrderUpdate = null;
                }
            }
        }
        void SplitFineForDelayOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {

                if (OrderUpdateFineForDelaySelectComboBox.SelectedIndex != -1 && OrderUpdateFineForDelaySelectComboBox.Text == OrderUpdateFineForDelaySelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateFineForDelaySelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    fineForDelayOrderUpdate = db.FineForDelays.Find(id);
                }
                else
                {
                    fineForDelayOrderUpdate = null;
                }
            }
        }
        void SplitTirCmrUpdateOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateTirCmrSelectComboBox.SelectedIndex != -1 && OrderUpdateTirCmrSelectComboBox.Text == OrderUpdateTirCmrSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateTirCmrSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    tirCmrOrderUpdate = db.TirCmrs.Find(id);
                }
                else
                {
                    tirCmrOrderUpdate = null;
                }
            }
        }
        void SplitOrderDenyOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateDenyFineSelectComboBox.SelectedIndex != -1 && OrderUpdateDenyFineSelectComboBox.Text == OrderUpdateDenyFineSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateDenyFineSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    orderDenyOrderUpdate = db.OrderDenies.Find(id);
                }
                else
                {
                    orderDenyOrderUpdate = null;
                }
            }
        }
        void SplitPaymentOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdatePaymentTermsSelectComboBox.SelectedIndex != -1 && OrderUpdatePaymentTermsSelectComboBox.Text == OrderUpdatePaymentTermsSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdatePaymentTermsSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    paymentOrderUpdate = db.Payments.Find(id);
                }
                else
                {
                    paymentOrderUpdate = null;
                }
            }
        }
        void SplitRegularyDelayOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateRegularyDelaySelectComboBox.SelectedIndex != -1 && OrderUpdateRegularyDelaySelectComboBox.Text == OrderUpdateRegularyDelaySelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateRegularyDelaySelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    regularyDelayOrderUpdate = db.RegularyDelays.Find(id);
                }
                else
                {
                    regularyDelayOrderUpdate = null;
                }
            }
        }
        void SplitCubeOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateCubeSelectComboBox.SelectedIndex != -1 && OrderUpdateCubeSelectComboBox.Text == OrderUpdateCubeSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateCubeSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    cubeOrderUpdate = db.Cubes.Find(id);
                }
                else
                {
                    cubeOrderUpdate = null;
                }
            }
        }
        void SplitStaffOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateStaffSelectComboBox.SelectedIndex != -1 && OrderUpdateStaffSelectComboBox.Text == OrderUpdateStaffSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateStaffSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    staffOrderUpdate = db.Staffs.Find(id);
                }
                else
                {
                    staffOrderUpdate = null;
                }
            }
        }
        void SplitTrailerOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateTrailerSelectComboBox.SelectedIndex != -1 && OrderUpdateTrailerSelectComboBox.Text == OrderUpdateTrailerSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateTrailerSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    trailerOrderUpdate = db.Trailers.Find(id);
                }
                else
                {
                    trailerOrderUpdate = null;
                }
            }
        }
        void SplitLoadingForm1OrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateLoadingForm1SelectComboBox.SelectedIndex != -1 && OrderUpdateLoadingForm1SelectComboBox.Text == OrderUpdateLoadingForm1SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateLoadingForm1SelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    loadingForm1OrderUpdate = db.LoadingForms.Find(id);
                }
                else
                {
                    loadingForm1OrderUpdate = null;
                }
            }
        }
        void SplitLoadingForm2OrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateLoadingForm2SelectComboBox.SelectedIndex != -1 && OrderUpdateLoadingForm2SelectComboBox.Text == OrderUpdateLoadingForm2SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateLoadingForm2SelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    loadingForm2OrderUpdate = db.LoadingForms.Find(id);
                }
                else
                {
                    loadingForm2OrderUpdate = null;
                }
            }
        }

        
        //ADRESES
        void UploadAddressUpdate()
        {
            if (clientOrderUpdate != null)
            {
                if (updateOrder != null)
                {
                    if (updateUploadAddressesForm == null || updateUploadAddressesForm.IsDisposed)
                    {
                        updateUploadAddressesForm = new SelectUploadAddressesForm(clientOrderUpdate, updateOrder);
                        updateUploadAddressesForm.Show();
                    }
                    else
                    {
                        updateUploadAddressesForm.Show();
                        updateUploadAddressesForm.Focus();
                    }
                   
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_заявку);
                } 
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_клієнта);
            }
        }
        void DownloadAddressUpdate()
        {
            if (clientOrderUpdate != null)
            {
                if (updateOrder != null)
                {
                   
                    if (updateDownloadAddressesForm == null || updateDownloadAddressesForm.IsDisposed)
                    {
                        updateDownloadAddressesForm = new SelectDownloadAddressesForm(clientOrderUpdate, updateOrder);
                        updateDownloadAddressesForm.Show();
                    }
                    else
                    {
                        updateDownloadAddressesForm.Show();

                        updateDownloadAddressesForm.Focus();
                    }

                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_заявку);
                }                
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_клієнта);
            }
        }
        void CustomsAddressUpdate()
        {
            if (clientOrderUpdate != null)
            {
                if (updateOrder != null)
                {
                    if (updateCustomsAddressesForm == null || updateCustomsAddressesForm.IsDisposed)
                    {
                        updateCustomsAddressesForm = new SelectCustomsAddressesForm(clientOrderUpdate, updateOrder);
                        updateCustomsAddressesForm.Show();
                    }
                    else
                    {
                        updateCustomsAddressesForm.Show();
                        updateCustomsAddressesForm.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_заявку);
                }  

            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_клієнта);
            }
        }
        void UncustomsAddressUpdate()
        {
            if (clientOrderUpdate != null)
            {
                if (updateOrder != null)
                {
                    if (updateUncustomsAddressesForm == null || updateUncustomsAddressesForm.IsDisposed)
                    {
                        updateUncustomsAddressesForm = new SelectUncustomsAddressesForm(clientOrderUpdate, updateOrder);
                        updateUncustomsAddressesForm.Show();
                    }
                    else
                    {
                        updateUncustomsAddressesForm.Show();
                        updateUncustomsAddressesForm.Focus();
                    }

                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_заявку);
                }  
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_клієнта);
            }
        }
        //Client
        void LoadOrderUpdateClientDiapasonCombobox()
        {
            OrderUpdateClientDiapasoneComboBox.Items.Clear();
            OrderUpdateClientSelectComboBox.Items.Clear();
            OrderUpdateClientSelectComboBox.SelectedIndex = -1;
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
                        OrderUpdateClientDiapasoneComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }                    
                    OrderUpdateClientSelectComboBox.Enabled = true;
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
                }
            }
        }
        void LoadOrderUpdateClientSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateClientDiapasoneComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
                }
                else
                {
                    string text = OrderUpdateClientDiapasoneComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        OrderUpdateClientSelectComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }
        void SplitClientOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateClientSelectComboBox.SelectedIndex != -1 && OrderUpdateClientSelectComboBox.Text == OrderUpdateClientSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateClientSelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    clientOrderUpdate = db.Clients.Find(id);
                }
                else
                {
                    clientOrderUpdate = null;
                }
            }
        }
        //Transporter
        void LoadOrderUpdateTransporterDiapasonCombobox()
        {
            OrderUpdateTransporterDiapasoneComboBox.Items.Clear();
            OrderUpdateTransporterSelectComboBox.Items.Clear();
            OrderUpdateTransporterSelectComboBox.SelectedIndex = -1;
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
                        OrderUpdateTransporterDiapasoneComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    OrderUpdateTransporterSelectComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
                }
            }
        }
        void LoadOrderUpdateTransporterSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateTransporterDiapasoneComboBox.Text == "")
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
                }
                else
                {
                    string text = OrderUpdateTransporterDiapasoneComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        OrderUpdateTransporterSelectComboBox.Items.Add(item.FullName + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }
        void SplitTransporterOrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateTransporterSelectComboBox.SelectedIndex != -1 && OrderUpdateTransporterSelectComboBox.Text == OrderUpdateTransporterSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateTransporterSelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    transporterOrderUpdate = db.Transporters.Find(id);
                }
                else
                {
                    transporterOrderUpdate = null;
                }
            }
        }
        //Forwarders
        void LoadOrderUpdateForwarder1SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    OrderUpdateForwarder1SelectComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }
        void LoadOrderUpdateForwarder2SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    OrderUpdateForwarder2SelectComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }

        void LoadOrderUpdateForwarder3SelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    OrderUpdateForwarder3SelectComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }
        void SplitForwarder1OrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateForwarder1SelectComboBox.SelectedIndex != -1 && OrderUpdateForwarder1SelectComboBox.Text == OrderUpdateForwarder1SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateForwarder1SelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarder1OrderUpdate = db.Forwarders.Find(id);
                }
                else
                {
                    forwarder1OrderUpdate = null;
                }
            }
        }
        void SplitForwarder2OrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateForwarder2SelectComboBox.SelectedIndex != -1 && OrderUpdateForwarder2SelectComboBox.Text == OrderUpdateForwarder2SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateForwarder2SelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarder2OrderUpdate = db.Forwarders.Find(id);
                }
                else
                {
                    forwarder2OrderUpdate = null;
                }
            }
        }

        void SplitForwarder3OrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateForwarder3SelectComboBox.SelectedIndex != -1 && OrderUpdateForwarder3SelectComboBox.Text == OrderUpdateForwarder3SelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = OrderUpdateForwarder3SelectComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarder3OrderUpdate = db.Forwarders.Find(id);
                }
                else
                {
                    forwarder3OrderUpdate = null;
                }
            }
        }
        void OrderUpdate()
        {
            string updateMessage = string.Empty;
            using (var db = new AtlantSovtContext())
            {
                if (updateOrder != null)
                {
                    try
                    {
                        updateOrder = db.Orders.Find(updateOrder.Id);

                        //1/////////////////////////////////////////////
                        if (additionalTermOrderUpdate != null)
                        {
                            if (updateOrder.AdditionalTermsId != additionalTermOrderUpdate.Id)
                            {
                                updateOrder.AdditionalTermsId = additionalTermOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.AdditionalTerm != null) 
                        {
                            updateOrder.AdditionalTermsId = null;
                            IsModified = true;
                        }
                        //2/////////////////////////////////////////////
                        if ((OrderUpdateADRSelectComboBox.SelectedIndex != -1 && OrderUpdateADRSelectComboBox.Text == OrderUpdateADRSelectComboBox.SelectedItem.ToString()))
                        {
                            if (updateOrder.ADRNumber != Convert.ToInt32(OrderUpdateADRSelectComboBox.Text))
                            {
                                updateOrder.ADRNumber = Convert.ToInt32(OrderUpdateADRSelectComboBox.Text);
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.ADRNumber != null)
                        {
                            updateOrder.ADRNumber = null;
                            IsModified = true;
                        }
                        //3/////////////////////////////////////////////
                        if (tirCmrOrderUpdate != null)
                        {
                            if (updateOrder.TirCmrId != tirCmrOrderUpdate.Id)
                            {
                                updateOrder.TirCmrId = tirCmrOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.TirCmr != null)
                        {
                            updateOrder.TirCmrId = null;
                            IsModified = true;
                        }
                        //4///////////////////////////////////////////////
                        
                        if (cargoOrderUpdate != null)
                        {
                            if (updateOrder.CargoId != cargoOrderUpdate.Id)
                            {
                                updateOrder.CargoId = cargoOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.Cargo != null)
                        {
                            updateOrder.CargoId = null;
                            IsModified = true;
                        }
                        //4/////////////////////////////////////////////
                        if (clientOrderUpdate != null)
                        {
                            if (updateOrder.ClientId != clientOrderUpdate.Id)
                            {
                                updateOrder.ClientId = clientOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.Client != null)
                        {
                            updateOrder.ClientId = null;
                            IsModified = true;
                        }
                        //4/////////////////////////////////////////////
                        if (cubeOrderUpdate != null)
                        {
                            if (updateOrder.CubeId != cubeOrderUpdate.Id)
                            {
                                updateOrder.CubeId = cubeOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.Cube != null)
                        {
                            updateOrder.CubeId = null;
                            IsModified = true;
                        }
                        //4/////////////////////////////////////////////
                        if (fineForDelayOrderUpdate != null)
                        {
                            if (updateOrder.FineForDelaysId != fineForDelayOrderUpdate.Id)
                            {
                                updateOrder.FineForDelaysId = fineForDelayOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.FineForDelay != null)
                        {
                            updateOrder.FineForDelaysId = null;
                            IsModified = true;
                        }
                        //4/////////////////////////////////////////////
                        
                        if (orderDenyOrderUpdate != null)
                        {
                            if (updateOrder.OrderDenyId != orderDenyOrderUpdate.Id)
                            {
                                updateOrder.OrderDenyId = orderDenyOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.OrderDeny!= null)
                        {
                            updateOrder.OrderDenyId = null;
                            IsModified = true;
                        }
                        //5//////////////////////////////////////////////
                        if (paymentOrderUpdate != null)
                        {
                            if (updateOrder.PaymentTermsId != paymentOrderUpdate.Id)
                            {
                                updateOrder.PaymentTermsId = paymentOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.Payment != null)
                        {
                            updateOrder.PaymentTermsId = null;
                            IsModified = true;
                        }
                        //4/////////////////////////////////////////////
                        if (regularyDelayOrderUpdate != null)
                        {
                            if (updateOrder.RegularyDelaysId != regularyDelayOrderUpdate.Id)
                            {
                                updateOrder.RegularyDelaysId = regularyDelayOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.RegularyDelay != null)
                        {
                            updateOrder.RegularyDelaysId = null;
                            IsModified = true;
                        }
                        //4/////////////////////////////////////////////
                        if (trailerOrderUpdate != null)
                        {
                            if (updateOrder.TrailerId != trailerOrderUpdate.Id)
                            {
                                updateOrder.TrailerId = trailerOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.Trailer != null)
                        {
                            updateOrder.TrailerId = null;
                            IsModified = true;
                        }

                        //4/////////////////////////////////////////////
                        if (staffOrderUpdate != null)
                        {
                            if (updateOrder.StaffId != staffOrderUpdate.Id)
                            {
                                updateOrder.StaffId = staffOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.Staff != null)
                        {
                            updateOrder.StaffId = null;
                            IsModified = true;
                        }
                        //4/////////////////////////////////////////////
                        if (transporterOrderUpdate != null)
                        {
                            if (updateOrder.TransporterId != transporterOrderUpdate.Id)
                            {
                                updateOrder.TransporterId = transporterOrderUpdate.Id;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.Transporter != null)
                        {
                            updateOrder.TransporterId = null;
                            IsModified = true;
                        }
                        

                        if ((OrderUpdateLanguageSelectComboBox.SelectedIndex != -1 && OrderUpdateLanguageSelectComboBox.Text == OrderUpdateLanguageSelectComboBox.SelectedItem.ToString()))
                        {
                            if (updateOrder.Language != 0)
                            {
                                if (OrderUpdateLanguageSelectComboBox.SelectedIndex == 0)
                                {
                                    updateOrder.Language = 0;
                                    IsModified = true;
                                }
                            }
                            else if (updateOrder.Language != 1)
                            {
                                if (OrderUpdateLanguageSelectComboBox.SelectedIndex == 1)
                                {
                                    updateOrder.Language = 1;
                                    IsModified = true;
                                }
                            }
                            else if (updateOrder.Language !=2)
                            {
                                if (OrderUpdateLanguageSelectComboBox.SelectedIndex == 2)
                                {
                                    updateOrder.Language = 2;
                                    IsModified = true;
                                }
                            }
                        }
                        else if (updateOrder.Language != null)
                        {
                            updateOrder.Language = null;
                            IsModified = true;
                        }
                        ///////////////////////////////////////////////

                        if (OrderUpdateWeightTextBox.Text != "") 
                        {
                            if (updateOrder.CargoWeight != Double.Parse(OrderUpdateWeightTextBox.Text, CultureInfo.InvariantCulture)) 
                            {
                                updateOrder.CargoWeight = Double.Parse(OrderUpdateWeightTextBox.Text, CultureInfo.InvariantCulture);
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.CargoWeight != null) 
                        {
                            updateOrder.CargoWeight = null;
                            IsModified = true;
                        }
                        /////////////////////////////////////////////////
                        if (OrderUpdateFreightTextBox.Text != "")
                        {
                            if (updateOrder.Freight != OrderUpdateFreightTextBox.Text)
                            {
                                updateOrder.Freight = OrderUpdateFreightTextBox.Text;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.Freight != null)
                        {
                            updateOrder.Freight = null;
                            IsModified = true;
                        }
                        //TODO Is bug?

                        if (updateOrder.Date != OrderUpdateDateDateTimePicker.Value)
                        {
                            updateOrder.Date = OrderUpdateDateDateTimePicker.Value;
                            IsModified = true;
                        }
                        //from
                        if (OrderUpdateDownloadDateFromTimePicker.Checked)
                        {
                            if (updateOrder.DownloadDateFrom != OrderUpdateDownloadDateFromTimePicker.Value)
                            {
                                updateOrder.DownloadDateFrom = OrderUpdateDownloadDateFromTimePicker.Value;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.DownloadDateFrom != null)
                        {
                            updateOrder.DownloadDateFrom = null;
                            IsModified = true;
                        }
                        //to
                        if (OrderUpdateDownloadDateToTimePicker.Checked)
                        {
                            if (updateOrder.DownloadDateTo != OrderUpdateDownloadDateToTimePicker.Value)
                            {
                                updateOrder.DownloadDateTo = OrderUpdateDownloadDateToTimePicker.Value;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.DownloadDateTo != null)
                        {
                            updateOrder.DownloadDateTo = null;
                            IsModified = true;
                        }
                        //from
                        if (OrderUpdateUploadDateFromTimePicker.Checked)
                        {
                            if (updateOrder.UploadDateFrom != OrderUpdateUploadDateFromTimePicker.Value)
                            {
                                updateOrder.UploadDateFrom = OrderUpdateUploadDateFromTimePicker.Value;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.UploadDateFrom != null)
                        {
                            updateOrder.UploadDateFrom = null;
                            IsModified = true;
                        }
                        //to
                        if (OrderUpdateUploadDateFromTimePicker.Checked)
                        {
                            if (updateOrder.UploadDateTo != OrderUpdateUploadDateToTimePicker.Value)
                            {
                                updateOrder.UploadDateTo = OrderUpdateUploadDateToTimePicker.Value;
                                IsModified = true;
                            }
                        }
                        else if (updateOrder.UploadDateTo != null)
                        {
                            updateOrder.UploadDateTo = null;
                            IsModified = true;
                        }

                        try
                        {
                            if (loadingForm1OrderUpdate != null)
                            {
                                if (updateOrder.OrderLoadingForms.Where(lf1 => lf1.IsFirst == true).Count() != 0) // якшо вже є
                                {
                                    OrderLoadingForm UpdateLoadingForm1 = db.Orders.Find(updateOrder.Id).OrderLoadingForms.Where(lf1 => lf1.IsFirst == true).FirstOrDefault();

                                    if (UpdateLoadingForm1.LoadingForm.Id != loadingForm1OrderUpdate.Id) //якшо не то саме
                                    {
                                        UpdateLoadingForm1.LoadingFormId = loadingForm1OrderUpdate.Id;
                                        db.Entry(UpdateLoadingForm1).State = EntityState.Modified;
                                        IsModified = true;
                                        updateMessage += AtlantSovt.Properties.Resources.Успішно_змінено_першу_форму_завантаження;

                                    }
                                }
                                else 
                                {
                                    OrderLoadingForm New_OrderLoadingForm1 = new OrderLoadingForm
                                    {
                                        LoadingFormId = loadingForm1OrderUpdate.Id,
                                        IsFirst = true
                                    };
                                    db.Orders.Find(updateOrder.Id).OrderLoadingForms.Add(New_OrderLoadingForm1);
                                    db.Entry(New_OrderLoadingForm1).State = EntityState.Added;
                                    IsModified = true;
                                    updateMessage += AtlantSovt.Properties.Resources.Успішно_вибрано_першу_форму_завантаження;
                                }
                            }
                            else if (updateOrder.OrderLoadingForms.Where(lf1 => lf1.IsFirst == true).Count() != 0)
                            {
                                OrderLoadingForm Delete_OrderLoadingForm1 = db.Orders.Find(updateOrder.Id).OrderLoadingForms.Where(lf1 => lf1.IsFirst == true).FirstOrDefault();
                                db.Orders.Find(updateOrder.Id).OrderLoadingForms.Remove(Delete_OrderLoadingForm1);
                                db.Entry(Delete_OrderLoadingForm1).State = EntityState.Deleted;
                                updateMessage += AtlantSovt.Properties.Resources.Успішно_видалено_першу_форму_завантаження;
                                IsModified = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_1_форма_завантаження + ex.Message);
                        }

                        try
                        {
                            if (loadingForm2OrderUpdate != null)
                            {
                                if (updateOrder.OrderLoadingForms.Where(lf2 => lf2.IsFirst == false).Count() != 0) // якшо вже є
                                {
                                    OrderLoadingForm UpdateLoadingForm2 = db.Orders.Find(updateOrder.Id).OrderLoadingForms.Where(lf1 => lf1.IsFirst == false).FirstOrDefault();

                                    if (UpdateLoadingForm2.LoadingForm.Id != loadingForm2OrderUpdate.Id) //якшо не то саме
                                    {
                                        UpdateLoadingForm2.LoadingFormId = loadingForm2OrderUpdate.Id;
                                        db.Entry(UpdateLoadingForm2).State = EntityState.Modified;
                                        IsModified = true;
                                        updateMessage += AtlantSovt.Properties.Resources.Успішно_змінено_другу_форму_затора;
                                    }
                                }
                                else
                                {
                                    OrderLoadingForm New_OrderLoadingForm2 = new OrderLoadingForm
                                    {
                                        LoadingFormId = loadingForm2OrderUpdate.Id,
                                        IsFirst = false
                                    };
                                    db.Orders.Find(updateOrder.Id).OrderLoadingForms.Add(New_OrderLoadingForm2);
                                    db.Entry(New_OrderLoadingForm2).State = EntityState.Added;
                                    IsModified = true;
                                    updateMessage += AtlantSovt.Properties.Resources.Успішно_вибрано_другу_форму_завантаження;
                                }
                            }
                            else if (updateOrder.OrderLoadingForms.Where(lf1 => lf1.IsFirst == false).Count() != 0)
                            {
                                OrderLoadingForm Delete_OrderLoadingForm2 = db.Orders.Find(updateOrder.Id).OrderLoadingForms.Where(lf1 => lf1.IsFirst == false).FirstOrDefault();
                                db.Orders.Find(updateOrder.Id).OrderLoadingForms.Remove(Delete_OrderLoadingForm2);
                                db.Entry(Delete_OrderLoadingForm2).State = EntityState.Deleted;
                                updateMessage += AtlantSovt.Properties.Resources.Успішно_видалено_другу_форму_завантаження;
                                IsModified = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_2_форма_завантаження + ex.Message);
                        }
                        
                        try
                        {
                            if (forwarder1OrderUpdate != null)
                            {
                                if (updateOrder.ForwarderOrders.Where(fo1 => fo1.IsFirst == 1).Count() != 0) // якшо вже є
                                {
                                    ForwarderOrder UpdateForwarder1Order = db.Orders.Find(updateOrder.Id).ForwarderOrders.Where(fo1 => fo1.IsFirst == 1).FirstOrDefault();

                                    if (UpdateForwarder1Order.Forwarder.Id != forwarder1OrderUpdate.Id) //якшо не то саме
                                    {
                                        UpdateForwarder1Order.ForwarderId = forwarder1OrderUpdate.Id;
                                        db.Entry(UpdateForwarder1Order).State = EntityState.Modified;
                                        IsModified = true;
                                        updateMessage += AtlantSovt.Properties.Resources.Успішно_змінено_першого_експедтора;
                                    }
                                }
                                else
                                {
                                    ForwarderOrder New_Forwarder1Order = new ForwarderOrder
                                    {
                                        ForwarderId = forwarder1OrderUpdate.Id,
                                        IsFirst = 1
                                    };
                                    db.Orders.Find(updateOrder.Id).ForwarderOrders.Add(New_Forwarder1Order);
                                    db.Entry(New_Forwarder1Order).State = EntityState.Added;
                                    IsModified = true;
                                    updateMessage += AtlantSovt.Properties.Resources.Успішно_додано_першого_експедитора;
                                }
                            }
                            else if (updateOrder.ForwarderOrders.Where(fo1 => fo1.IsFirst == 1).Count() != 0)
                            {
                                ForwarderOrder Delete_Forwarder1Order = db.Orders.Find(updateOrder.Id).ForwarderOrders.Where(fo1 => fo1.IsFirst == 1).FirstOrDefault();
                                db.Orders.Find(updateOrder.Id).ForwarderOrders.Remove(Delete_Forwarder1Order);
                                db.Entry(Delete_Forwarder1Order).State = EntityState.Deleted;
                                IsModified = true;
                                updateMessage += AtlantSovt.Properties.Resources.Успішно_видалено_першого_експедитора;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_1_експедитор + ex.Message);
                        }

                        try
                        {
                            if (forwarder2OrderUpdate != null)
                            {
                                if (updateOrder.ForwarderOrders.Where(fo2 => fo2.IsFirst == 2).Count() != 0) // якшо вже є
                                {
                                    ForwarderOrder UpdateForwarder2Order = db.Orders.Find(updateOrder.Id).ForwarderOrders.Where(fo2 => fo2.IsFirst == 2).FirstOrDefault();

                                    if (UpdateForwarder2Order.Forwarder.Id != forwarder2OrderUpdate.Id) //якшо не то саме
                                    {
                                        UpdateForwarder2Order.ForwarderId = forwarder2OrderUpdate.Id;
                                        db.Entry(UpdateForwarder2Order).State = EntityState.Modified;
                                        IsModified = true;
                                        updateMessage += AtlantSovt.Properties.Resources.Успішно_змінено_другого_експедтора;
                                    }
                                }
                                else
                                {
                                    ForwarderOrder New_Forwarder2Order = new ForwarderOrder
                                    {
                                        ForwarderId = forwarder2OrderUpdate.Id,
                                        IsFirst = 2
                                    };
                                    db.Orders.Find(updateOrder.Id).ForwarderOrders.Add(New_Forwarder2Order);
                                    db.Entry(New_Forwarder2Order).State = EntityState.Added;
                                    IsModified = true;
                                    updateMessage += AtlantSovt.Properties.Resources.Успішно_додано_другого_експедитора;
                                }
                            }
                            else if (updateOrder.ForwarderOrders.Where(fo2 => fo2.IsFirst == 2).Count() != 0)
                            {
                                ForwarderOrder Delete_Forwarder2Order = db.Orders.Find(updateOrder.Id).ForwarderOrders.Where(fo2 => fo2.IsFirst == 2).FirstOrDefault();
                                db.Orders.Find(updateOrder.Id).ForwarderOrders.Remove(Delete_Forwarder2Order);
                                db.Entry(Delete_Forwarder2Order).State = EntityState.Deleted;
                                IsModified = true;
                                updateMessage += AtlantSovt.Properties.Resources.Успішно_видалено_другого_експедитора;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_2_експедитор + ex.Message);
                        }

                        try
                        {
                            if (forwarder3OrderUpdate != null)
                            {
                                if (updateOrder.ForwarderOrders.Where(fo3 => fo3.IsFirst == 3).Count() != 0) // якшо вже є
                                {
                                    ForwarderOrder UpdateForwarder3Order = db.Orders.Find(updateOrder.Id).ForwarderOrders.Where(fo3 => fo3.IsFirst == 3).FirstOrDefault();//TODO bool to int

                                    if (UpdateForwarder3Order.Forwarder.Id != forwarder3OrderUpdate.Id) //якшо не то саме
                                    {
                                        UpdateForwarder3Order.ForwarderId = forwarder3OrderUpdate.Id;
                                        db.Entry(UpdateForwarder3Order).State = EntityState.Modified;
                                        IsModified = true;
                                        updateMessage += AtlantSovt.Properties.Resources.Успішно_змінено_третього_експедитора;
                                    }
                                }
                                else
                                {
                                    ForwarderOrder New_Forwarder3Order = new ForwarderOrder
                                    {
                                        ForwarderId = forwarder3OrderUpdate.Id,
                                        IsFirst = 3
                                    };
                                    db.Orders.Find(updateOrder.Id).ForwarderOrders.Add(New_Forwarder3Order);
                                    db.Entry(New_Forwarder3Order).State = EntityState.Added;
                                    IsModified = true;
                                    updateMessage += AtlantSovt.Properties.Resources.Успішно_додано_третього_експедтора;
                                }
                            }
                            else if (updateOrder.ForwarderOrders.Where(fo3 => fo3.IsFirst == 3).Count() != 0)
                            {
                                ForwarderOrder Delete_Forwarder3Order = db.Orders.Find(updateOrder.Id).ForwarderOrders.Where(f3 => f3.IsFirst == 3).FirstOrDefault();
                                db.Orders.Find(updateOrder.Id).ForwarderOrders.Remove(Delete_Forwarder3Order);
                                db.Entry(Delete_Forwarder3Order).State = EntityState.Deleted;
                                IsModified = true;
                                updateMessage += AtlantSovt.Properties.Resources.Успішно_видалено_третього_експедитора;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_3_експедитор + ex.Message);
                        }

                        if (IsModified)
                        {
                            db.Entry(updateOrder).State = EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Зміни_збережено + updateMessage);
                        }
                        else 
                        {
                            MessageBox.Show(AtlantSovt.Properties.Resources.Змін_не_знайдено);
                        }                        
                    }
                    catch (DbEntityValidationException e)
                    {
                        Log.Write(e);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_при_зміні_заявки + e);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_при_зміні_заявки + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Спочатку_виберіть_заявку);
                }
            }
        }
    }
}
