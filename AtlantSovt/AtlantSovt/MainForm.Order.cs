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

        DateTime? orderAddDate;
        DateTime? orderAddUploadDate;
        DateTime? orderAddDeliveryDate;

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
                    FineForDelaysId = (fineForDelayOrderAdd != null) ? (long?)fineForDelayOrderAdd.Id : null,
                    Freight = (OrderAddFreightTextBox.Text != "") ? OrderAddFreightTextBox.Text : null,
                    OrderDenyId = (orderDenyOrderAdd != null) ? (long?)orderDenyOrderAdd.Id : null,
                    PaymentTermsId = (paymentOrderAdd != null) ? (long?)paymentOrderAdd.Id : null,
                    RegularyDelaysId = (regularyDelayOrderAdd != null) ? (long?)regularyDelayOrderAdd.Id : null,
                    TrailerId = (trailerOrderAdd != null) ? (long?)trailerOrderAdd.Id : null,
                    TransporterId = (transporterOrderAdd != null) ? (long?)transporterOrderAdd.Id : null,
                    Date = OrderAddDateSelectDateTimePicker.Value,
                    DownloadDate =  OrderAddDownloadDateTimePicker.Checked ? (DateTime?)OrderAddDownloadDateTimePicker.Value : null,
                    UploadDate = OrderAddUploadDateTimePicker.Checked ? (DateTime?)OrderUpdateDownloadDateTimePicker.Value : null,
                    State = null,
                    YorU = ((OrderAddYOrUComboBox.SelectedIndex != -1) || (OrderAddYOrUComboBox.Text != "")) ? ((OrderAddYOrUComboBox.SelectedIndex == 0) ? "У" : "І") : null
                };
                try
                {
                    db.Orders.Add(New_Order);
                    db.Entry(New_Order).State = EntityState.Added;
                    db.SaveChanges();
                    MessageBox.Show("Заявку успішно створено");
                    BridgeAddes(New_Order);
                }
                catch (DbEntityValidationException e)
                {
                    MessageBox.Show("Помилка при створенні заявки\n" + e);
                }
            }
        }
        void BridgeAddes(Order New_Order)
        {
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
                        MessageBox.Show("Успішно вибрано першу форму завантаження");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка (1 форма завантаження)\n" + e.Message);
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
                        MessageBox.Show("Успішно вибрано другу форму завантаження");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка (2 форма завантаження)\n" + e.Message);
                    }
                }

                //адреси              
                if (selectDownloadAddressesForm != null)
                {
                    selectDownloadAddressesForm.DownloadAddressesSelect(db.Orders.Find(New_Order.Id));
                    selectDownloadAddressesForm = null;
                }
                if (selectUploadAddressesForm != null)
                {
                    selectUploadAddressesForm.UploadAddressesSelect(db.Orders.Find(New_Order.Id));
                    selectUploadAddressesForm = null;
                }
                if (selectCustomsAddressesForm != null)
                {
                    selectCustomsAddressesForm.CustomsAddressesSelect(db.Orders.Find(New_Order.Id));
                    selectCustomsAddressesForm = null;
                }
                if (selectUncustomsAddressesForm != null)
                {
                    selectUncustomsAddressesForm.UncustomsAddressesSelect(db.Orders.Find(New_Order.Id));
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
                            IsFirst = true
                        };
                        db.Orders.Find(New_Order.Id).ForwarderOrders.Add(New_Forwarder1Order);
                        db.Entry(New_Forwarder1Order).State = EntityState.Added;
                        db.SaveChanges();

                        MessageBox.Show("Успішно вибрано першого експедитора");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка (1 експедитор)\n" + e.Message);
                    }

                }
                if (forwarder2OrderAdd != null)
                {
                    try
                    {
                        ForwarderOrder New_Forwarder2Order = new ForwarderOrder
                        {
                            ForwarderId = forwarder2OrderAdd.Id,
                            IsFirst = false
                        };
                        db.Orders.Find(New_Order.Id).ForwarderOrders.Add(New_Forwarder2Order);
                        db.Entry(New_Forwarder2Order).State = EntityState.Added;
                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано другого експедитора");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка (2 експедитор)\n" + e.Message);
                    }
                }

            }
        }
            
    
        void SetOrderDate()
        {
            orderAddDate = OrderAddDateSelectDateTimePicker.Value;
        }
        void SetOrderUploadDate()
        {
            orderAddUploadDate = OrderAddDownloadDateTimePicker.Value;
        }
        void SetOrderDeliveryDate()
        {
            orderAddDeliveryDate = OrderAddUploadDateTimePicker.Value;
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
       // Adresses
        void UploadAddressForm()
        {
            if (clientOrderAdd != null)
            {
                if (selectUploadAddressesForm == null)
                {
                    selectUploadAddressesForm = new SelectUploadAddressesForm(clientOrderAdd);
                    selectUploadAddressesForm.Show();
                }
                else
                {
                    selectUploadAddressesForm.Show();
                }
            }
            else 
            {
                MessageBox.Show("Виберіть спочатку клієнта");
            }
        }
        void DownloadAddressForm()
        {
            if (clientOrderAdd != null)
            {
                if (selectDownloadAddressesForm == null)
                {
                    selectDownloadAddressesForm = new SelectDownloadAddressesForm(clientOrderAdd);
                    selectDownloadAddressesForm.Show();
                }
                else
                {
                    selectDownloadAddressesForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Виберіть спочатку клієнта");
            }
        }
        void CustomsAddressForm()
        {
            if (clientOrderAdd != null)
            {
                if (selectCustomsAddressesForm == null)
                {
                    selectCustomsAddressesForm = new SelectCustomsAddressesForm(clientOrderAdd);
                    selectCustomsAddressesForm.Show();
                }
                else
                {
                    selectCustomsAddressesForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Виберіть спочатку клієнта");
            }
        }
        void UncustomsAddressForm()
        {
            if (clientOrderAdd != null)
            {
                if (selectCustomsAddressesForm == null)
                {
                    selectUncustomsAddressesForm = new SelectUncustomsAddressesForm(clientOrderAdd);
                    selectUncustomsAddressesForm.Show();
                }
                else
                {
                    selectUncustomsAddressesForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Виберіть спочатку клієнта");
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

        AdditionalTerm additionalTermOrderUpdate;
        Cargo cargoOrderUpdate;

        FineForDelay fineForDelayOrderUpdate;
        TirCmr tirCmrOrderUpdate;
        OrderDeny orderDenyOrderUpdate;
        Payment paymentOrderUpdate;
        RegularyDelay regularyDelayOrderUpdate;

        Cube cubeOrderUpdate;
        Trailer trailerOrderUpdate;

        LoadingForm loadingForm1OrderUpdate;
        LoadingForm loadingForm2OrderUpdate;

        DateTime? orderUpdateDate;
        DateTime? orderUpdateUploUpdateate;
        DateTime? orderUpdateDeliveryDate;


        SelectUploadAddressesForm updateUploadAddressesForm;
        SelectDownloadAddressesForm updateDownloadAddressesForm;
        SelectCustomsAddressesForm updateCustomsAddressesForm;
        SelectUncustomsAddressesForm updateUncustomsAddressesForm;

        void ClearAllBoxesOrderUpdate() 
        {
            OrderUpdateAdditionalTermsSelectComboBox.SelectedIndex = -1;
            OrderUpdatePaymentTermsSelectComboBox.SelectedIndex = -1;
            OrderUpdateDenyFineSelectComboBox.SelectedIndex = -1;
            OrderUpdateCargoSelectComboBox.SelectedIndex = -1;
            OrderUpdateRegularyDelaySelectComboBox.SelectedIndex = -1;
            OrderUpdateTrailerSelectComboBox.SelectedIndex = -1;
            OrderUpdateCubeSelectComboBox.SelectedIndex = -1;
            OrderUpdateTirCmrSelectComboBox.SelectedIndex = -1;
            OrderUpdateYOrUComboBox.SelectedIndex = -1;
            OrderUpdateADRSelectComboBox.SelectedIndex = -1;
            OrderUpdateClientSelectComboBox.SelectedIndex = -1;
            OrderUpdateTransporterSelectComboBox.SelectedIndex = -1;
            OrderUpdateTransporterDiapasoneComboBox.SelectedIndex = -1;
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
            OrderUpdateTirCmrSelectComboBox.Items.Clear();
            OrderUpdateClientSelectComboBox.Items.Clear();
            OrderUpdateTransporterSelectComboBox.Items.Clear();
            OrderUpdateTransporterDiapasoneComboBox.Items.Clear();
            OrderUpdateForwarder2SelectComboBox.Items.Clear();
            OrderUpdateForwarder1SelectComboBox.Items.Clear();
            OrderUpdateClientDiapasoneComboBox.Items.Clear();
            OrderUpdateFineForDelaySelectComboBox.Items.Clear();
            OrderUpdateLoadingForm1SelectComboBox.Items.Clear();
            OrderUpdateLoadingForm2SelectComboBox.Items.Clear();

            OrderUpdateDateDateTimePicker.Checked = false;                    
            OrderUpdateDownloadDateTimePicker.Checked = false;
            OrderUpdateUploadDateTimePicker.Checked = false;
            

            OrderUpdateWeightTextBox.Text = "";
            OrderUpdateFreightTextBox.Text = "";
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
                        OrderUpdateOrderSelectComboBox.Items.Add(item.Client.Name + " ," + item.Date + " [" + item.Id + "]");
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
                        OrderUpdateOrderSelectComboBox.Items.Add(item.Client.Name + " ," + item.Date + " [" + item.Id + "]");
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
            LoadOrderUpdateTrailerSelectComboBox();
            LoadOrderUpdateLoadingForm1SelectComboBox();
            LoadOrderUpdateLoadingForm2SelectComboBox();

            LoadOrderUpdateClientDiapasonCombobox();
            LoadOrderUpdateTransporterDiapasonCombobox();

         
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
                    if (updateOrder.ForwarderOrders.Where(f1 => f1.IsFirst == true).Count() != 0)
                    {
                        OrderUpdateForwarder1SelectComboBox.SelectedIndex =
                            OrderUpdateForwarder1SelectComboBox.FindString(updateOrder.ForwarderOrders.Where(f1 => f1.IsFirst == true).FirstOrDefault().Forwarder.Name
                            + " , " + (updateOrder.ForwarderOrders.Where(f1 => f1.IsFirst == true).FirstOrDefault().Forwarder.Director)
                            + " [" + (updateOrder.ForwarderOrders.Where(f1 => f1.IsFirst == true).FirstOrDefault().Forwarder.Id + "]"));
                        SplitForwarder1OrderUpdate();
                    }
                    else
                    {
                        OrderUpdateForwarder1SelectComboBox.SelectedIndex = -1;
                    }
                    if (updateOrder.ForwarderOrders.Where(f2 => f2.IsFirst == false).Count() != 0)
                    {
                        OrderUpdateForwarder2SelectComboBox.SelectedIndex =
                            OrderUpdateForwarder2SelectComboBox.FindString(updateOrder.ForwarderOrders.Where(f2 => f2.IsFirst == false).FirstOrDefault().Forwarder.Name
                            + " , " + (updateOrder.ForwarderOrders.Where(f2 => f2.IsFirst == false).FirstOrDefault().Forwarder.Director)
                            + " [" + (updateOrder.ForwarderOrders.Where(f2 => f2.IsFirst == false).FirstOrDefault().Forwarder.Id + "]"));
                        SplitForwarder2OrderUpdate();
                    }
                    else
                    {
                        OrderUpdateForwarder2SelectComboBox.SelectedIndex = -1;
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
                    if (updateOrder.YorU != null)
                    {
                        OrderUpdateYOrUComboBox.SelectedIndex =
                            OrderUpdateYOrUComboBox.FindString(updateOrder.YorU.ToString());
                    }
                    else
                    {
                        OrderUpdateADRSelectComboBox.SelectedIndex = -1;
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
                            if (Enumerable.Range(from, to).Contains(Convert.ToInt32(updateOrder.Client.Id)))
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
                    
                    if (updateOrder.DownloadDate != null)
                    {
                        OrderUpdateDownloadDateTimePicker.Checked = true;
                        OrderUpdateDownloadDateTimePicker.Value = updateOrder.DownloadDate.Value;
                    }
                    else
                    {
                        OrderUpdateDownloadDateTimePicker.Checked = false;
                    }
                    
                    if (updateOrder.UploadDate != null)
                    {
                        OrderUpdateUploadDateTimePicker.Checked = true;
                        OrderUpdateUploadDateTimePicker.Value = updateOrder.UploadDate.Value;
                    }
                    else
                    {
                        OrderUpdateUploadDateTimePicker.Checked = false;
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
                    updateUploadAddressesForm = new SelectUploadAddressesForm(clientOrderUpdate, updateOrder);
                    updateUploadAddressesForm.Show();
                }
                else
                {
                    MessageBox.Show("Виберіть спочатку заявку");
                } 
            }
            else
            {
                MessageBox.Show("Виберіть спочатку клієнта");
            }
        }
        void DownloadAddressUpdate()
        {
            if (clientOrderUpdate != null)
            {
                if (updateOrder != null)
                {
                    updateDownloadAddressesForm = new SelectDownloadAddressesForm(clientOrderUpdate, updateOrder);
                    updateDownloadAddressesForm.Show();
                }
                else
                {
                    MessageBox.Show("Виберіть спочатку заявку");
                }                
            }
            else
            {
                MessageBox.Show("Виберіть спочатку клієнта");
            }
        }
        void CustomsAddressUpdate()
        {
            if (clientOrderUpdate != null)
            {
                if (updateOrder != null)
                {
                    selectCustomsAddressesForm = new SelectCustomsAddressesForm(clientOrderUpdate, updateOrder);
                    selectCustomsAddressesForm.Show();
                }
                else
                {
                    MessageBox.Show("Виберіть спочатку заявку");
                }  

            }
            else
            {
                MessageBox.Show("Виберіть спочатку клієнта");
            }
        }
        void UncustomsAddressUpdate()
        {
            if (clientOrderUpdate != null)
            {
                if (updateOrder != null)
                {
                    selectUncustomsAddressesForm = new SelectUncustomsAddressesForm(clientOrderUpdate, updateOrder);
                    selectUncustomsAddressesForm.Show();
                }
                else
                {
                    MessageBox.Show("Виберіть спочатку заявку");
                }  
            }
            else
            {
                MessageBox.Show("Виберіть спочатку клієнта");
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
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }
        void LoadOrderUpdateClientSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateClientDiapasoneComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Ви не вибрали діапазон");
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
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }
        void LoadOrderUpdateTransporterSelectComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (OrderUpdateTransporterDiapasoneComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
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

        void OrderUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                updateOrder = db.Orders.Find(updateOrder.Id);
                updateOrder.AdditionalTermsId = (additionalTermOrderUpdate != null) ? (long?)additionalTermOrderUpdate.Id : null;
                updateOrder.ADRNumber = (OrderUpdateADRSelectComboBox.Text != "") ? (int?)Convert.ToInt32(OrderUpdateADRSelectComboBox.Text) : null;
                updateOrder.TirCmrId = (tirCmrOrderUpdate != null) ? (long?)tirCmrOrderUpdate.Id : null;
                updateOrder.CargoId = (cargoOrderUpdate != null) ? (long?)cargoOrderUpdate.Id : null;
                updateOrder.CargoWeight = (OrderUpdateWeightTextBox.Text != "") ? (double?)Double.Parse(OrderUpdateWeightTextBox.Text, CultureInfo.InvariantCulture) : null;
                updateOrder.ClientId = (clientOrderUpdate != null) ? (long?)clientOrderUpdate.Id : null;
                updateOrder.CubeId = (cubeOrderUpdate != null) ? (long?)cubeOrderUpdate.Id : null;
                updateOrder.FineForDelaysId = (fineForDelayOrderUpdate != null) ? (long?)fineForDelayOrderUpdate.Id : null;
                updateOrder.Freight = (OrderUpdateFreightTextBox.Text != "") ? OrderUpdateFreightTextBox.Text : null;
                updateOrder.OrderDenyId = (orderDenyOrderUpdate != null) ? (long?)orderDenyOrderUpdate.Id : null;
                updateOrder.PaymentTermsId = (paymentOrderUpdate != null) ? (long?)paymentOrderUpdate.Id : null;
                updateOrder.RegularyDelaysId = (regularyDelayOrderUpdate != null) ? (long?)regularyDelayOrderUpdate.Id : null;
                updateOrder.TrailerId = (trailerOrderUpdate != null) ? (long?)trailerOrderUpdate.Id : null;
                updateOrder.TransporterId = (transporterOrderUpdate != null) ? (long?)transporterOrderUpdate.Id : null;
                updateOrder.Date = OrderUpdateDateDateTimePicker.Value;
                updateOrder.DownloadDate =  OrderUpdateDownloadDateTimePicker.Checked ? (DateTime?)OrderUpdateDownloadDateTimePicker.Value : null;
                updateOrder.UploadDate = OrderUpdateUploadDateTimePicker.Checked ? (DateTime?)OrderUpdateUploadDateTimePicker.Value : null;
                updateOrder.YorU = ((OrderUpdateYOrUComboBox.SelectedIndex != -1) || (OrderUpdateYOrUComboBox.Text != "")) ? ((OrderUpdateYOrUComboBox.SelectedIndex == 0) ? "У" : "І") : null;
            
                try
                {
                    db.Entry(updateOrder).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Зміни збереженно");
                    BridgeUpdates();
                    ClearAllBoxesOrderUpdate();
                }
                catch (DbEntityValidationException e)
                {
                    MessageBox.Show("Помилка при створенні заявки\n" + e);
                }
            }
        }
        void BridgeUpdates()
        {
            using (var db = new AtlantSovtContext())
            {
                updateOrder = db.Orders.Find(updateOrder.Id);
                db.Orders.Find(updateOrder.Id).OrderLoadingForms.Remove(db.Orders.Find(updateOrder.Id).OrderLoadingForms.Where(lf1 => lf1.IsFirst == true).FirstOrDefault());
                if (loadingForm1OrderUpdate != null)
                {

                    try
                    {
                        OrderLoadingForm New_OrderLoadingForm1 = new OrderLoadingForm
                        {
                            LoadingFormId = loadingForm1OrderUpdate.Id,
                            IsFirst = true
                        };
                        db.Orders.Find(updateOrder.Id).OrderLoadingForms.Add(New_OrderLoadingForm1);
                        db.Entry(New_OrderLoadingForm1).State = EntityState.Added;
                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано першу форму завантаження");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка (1 форма завантаження)\n" + e.Message);
                    }
                } 
                db.Orders.Find(updateOrder.Id).OrderLoadingForms.Remove(db.Orders.Find(updateOrder.Id).OrderLoadingForms.Where(lf1 => lf1.IsFirst == false).FirstOrDefault());
                if (loadingForm2OrderUpdate != null)
                {
                    try
                    {
                        OrderLoadingForm New_OrderLoadingForm2 = new OrderLoadingForm
                        {
                            LoadingFormId = loadingForm2OrderUpdate.Id,
                            IsFirst = false
                        };
                        db.Orders.Find(updateOrder.Id).OrderLoadingForms.Add(New_OrderLoadingForm2);
                        db.Entry(New_OrderLoadingForm2).State = EntityState.Added;
                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано другу форму завантаження");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка (2 форма завантаження)\n" + e.Message);
                    }
                }   
                //експедитори
                db.Orders.Find(updateOrder.Id).ForwarderOrders.Remove(db.Orders.Find(updateOrder.Id).ForwarderOrders.Where(f1 => f1.IsFirst == true).FirstOrDefault());
                if (forwarder1OrderUpdate!= null)
                {
                    try
                    {
                        ForwarderOrder New_Forwarder1Order = new ForwarderOrder
                        {
                            ForwarderId = forwarder1OrderAdd.Id,
                            IsFirst = true
                        };
                        db.Orders.Find(updateOrder.Id).ForwarderOrders.Add(New_Forwarder1Order);
                        db.Entry(New_Forwarder1Order).State = EntityState.Added;
                        db.SaveChanges();

                        MessageBox.Show("Успішно вибрано першого експедитора");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка (1 експедитор)\n" + e.Message);
                    }
                }
                db.Orders.Find(updateOrder.Id).ForwarderOrders.Remove(db.Orders.Find(updateOrder.Id).ForwarderOrders.Where(f1 => f1.IsFirst == false).FirstOrDefault());
                if (forwarder2OrderUpdate != null)
                {
                    try
                    {
                        ForwarderOrder New_Forwarder2Order = new ForwarderOrder
                        {
                            ForwarderId = forwarder2OrderAdd.Id,
                            IsFirst = false
                        };
                        db.Orders.Find(updateOrder.Id).ForwarderOrders.Add(New_Forwarder2Order);
                        db.Entry(New_Forwarder2Order).State = EntityState.Added;
                        db.SaveChanges();
                        MessageBox.Show("Успішно вибрано другого експедитора");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Помилка (2 експедитор)\n" + e.Message);
                    }
                }

            }
        }
    }
}
