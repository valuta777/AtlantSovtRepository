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
                    Date = orderAddDate ?? null,
                    DownloadDate = orderAddDeliveryDate ?? null,
                    UploadDate = orderAddUploadDate ?? null,
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
            orderAddUploadDate = OrderAddUploadDateTimePicker.Value;
        }
        void SetOrderDeliveryDate()
        {
            orderAddDeliveryDate = OrderAddDeliveryDateTimePicker.Value;
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
                selectUploadAddressesForm = new SelectUploadAddressesForm(clientOrderAdd);
                selectUploadAddressesForm.Show();
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
                selectDownloadAddressesForm = new SelectDownloadAddressesForm(clientOrderAdd);
                selectDownloadAddressesForm.Show();
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
                selectCustomsAddressesForm = new SelectCustomsAddressesForm(clientOrderAdd);
                selectCustomsAddressesForm.Show();
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
                selectUncustomsAddressesForm = new SelectUncustomsAddressesForm(clientOrderAdd);
                selectUncustomsAddressesForm.Show();
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
    }
}
