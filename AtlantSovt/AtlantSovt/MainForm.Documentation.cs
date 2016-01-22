using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Data.Entity;
using Microsoft.Office.Interop.Word;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using AtlantSovt.Additions;

namespace AtlantSovt
{
    partial class MainForm
    {
        Transporter transporterDocument;
        Forwarder firstForwarderDocument, secondForwarderDocument;
        Client clientDocument;
        ForwarderStamp firstForwarderStamp, secondForwarderStamp;
        DocumentCounter contractCount;
        Contract contract;

        int contractType;

        string contractOutputFilePath;

        void SplitTransporterDocumentComboBox(ComboBox transporterComboBox)
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = transporterComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterDocument = db.Transporters.Find(id);
            }
        }

        void SplitClientDocumentComboBox(ComboBox clientComboBox)
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = clientComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                clientDocument = db.Clients.Find(id);
            }
        }

        Forwarder SplitForwarderDocumentComboBox(ComboBox forwarderComboBox)
        {
            using (var db = new AtlantSovtContext())
            {
                Forwarder forwarder;
                string comboboxText = forwarderComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarder = db.Forwarders.Find(id);
                db.Forwarders.Include(forwarder.ForwarderStamp.ToString());
                return forwarder;
            }
        }

        void LoadTransporterDiapasonDocumentCombobox(ComboBox transporterComboBox, ComboBox transporterDiapason)
        {
            transporterDiapason.Items.Clear();
            transporterComboBox.Items.Clear();
            transporterComboBox.Text = "";
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
                        transporterDiapason.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    transporterDiapason.DroppedDown = true;
                    transporterComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
                }
            }
        }

        void LoadTransporterDocumentComboBox(ComboBox transporterComboBox, ComboBox transporterDiapason)
        {
            using (var db = new AtlantSovtContext())
            {
                if (transporterDiapason.Text == "")
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
                }
                else
                {
                    string text = transporterDiapason.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        transporterComboBox.Items.Add(item.FullName + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadClientDiapasonDocumentCombobox(ComboBox clientComboBox, ComboBox clientDiapason)
        {
            clientDiapason.Items.Clear();
            clientDiapason.Items.Clear();
            clientDiapason.Text = "";
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
                        clientDiapason.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    clientDiapason.DroppedDown = true;
                    clientComboBox.Enabled = true;
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
                }
            }
        }

        void LoadClientDocumentComboBox(ComboBox clientComboBox, ComboBox clientDiapason)
        {
            using (var db = new AtlantSovtContext())
            {
                if (clientDiapason.Text == "")
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
                }
                else
                {
                    string text = clientDiapason.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        clientComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadForwarderDocumentComboBox(ComboBox forwarderComboBox)
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    forwarderComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }
        
        bool IsForwarderFull(Forwarder forwader)
        {
            bool isForwarderFull = false;
            try
            {
                using (var db = new AtlantSovtContext())
                {
                    var forwarderName = forwader.Name;
                    var forwarderDirector = forwader.Director;
                    var forwarderPhysicalAddress = forwader.PhysicalAddress;
                    var forwarderGeographycalAddress = forwader.GeographyAddress;

                    string forwarderWorkDocument = "";
                    string forwarderTaxPayerStatus = "";
                    string forwarderBankDetailsCertificateSerial = "";
                    string forwarderBankDetailsCertificateNumber = "";
                    string forwarderBankDetailsEDRPOU = "";
                    string forwarderBankDetailsAccountNumber = "";
                    string forwarderBankDetailsBankName = "";
                    string forwarderBankDetailsMFO = "";
                    string forwarderBankDetailsIBAN = "";
                    string forwarderBankDetailsIPN = "";
                    string forwarderBankDetailsSWIFT = "";

                    if (forwader.WorkDocumentId != null)
                    {
                        forwarderWorkDocument = db.WorkDocuments.Find(forwader.WorkDocumentId).Status;
                    }

                    if (forwader.TaxPayerStatusId != null)
                    {
                        forwarderTaxPayerStatus = db.TaxPayerStatus.Find(forwader.TaxPayerStatusId).Status;
                    }

                    if (db.Forwarders.Find(forwader.Id).ForwarderBankDetail != null)
                    {
                        forwarderBankDetailsCertificateSerial = db.ForwarderBankDetails.Find(forwader.Id).CertificateSerial;
                        forwarderBankDetailsCertificateNumber = db.ForwarderBankDetails.Find(forwader.Id).CertificateNamber;
                        forwarderBankDetailsEDRPOU = db.ForwarderBankDetails.Find(forwader.Id).EDRPOU;
                        forwarderBankDetailsAccountNumber = db.ForwarderBankDetails.Find(forwader.Id).AccountNumber;
                        forwarderBankDetailsBankName = db.ForwarderBankDetails.Find(forwader.Id).BankName;
                        forwarderBankDetailsMFO = db.ForwarderBankDetails.Find(forwader.Id).MFO;
                        forwarderBankDetailsIBAN = db.ForwarderBankDetails.Find(forwader.Id).IBAN;
                        forwarderBankDetailsIPN = db.ForwarderBankDetails.Find(forwader.Id).IPN;
                        forwarderBankDetailsSWIFT = db.ForwarderBankDetails.Find(forwader.Id).SWIFT;
                    }

                    if (forwarderName != "" && forwarderDirector != "" &&
                        forwarderWorkDocument != "" && forwarderTaxPayerStatus != "" &&
                        forwarderBankDetailsBankName != "" && forwarderBankDetailsAccountNumber != "" && forwarderBankDetailsCertificateNumber != "" && forwarderBankDetailsCertificateSerial != "" &&
                        forwarderBankDetailsEDRPOU != "" && forwarderBankDetailsIBAN != "" &&
                        forwarderBankDetailsIPN != "" && forwarderBankDetailsMFO != "" && forwarderBankDetailsSWIFT != "" && forwader.ForwarderStamp.Stamp != null)
                    {
                        isForwarderFull = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
            }
            return isForwarderFull;
        }

        bool IsTransporterFull()
        {
            bool isTransporterFull = false;
            try
            {
                using (var db = new AtlantSovtContext())
                {

                    var transporterFullName = transporterDocument.FullName;
                    var transporterDirector = transporterDocument.Director;
                    var transporterPhysicalAddress = transporterDocument.PhysicalAddress;
                    var transporterGeographycalAddress = transporterDocument.GeographyAddress;

                    string transporterWorkDocument = "";
                    string transporterTaxPayerStatus = "";
                    string transporterBankDetailsCertificateSerial = "";
                    string transporterBankDetailsCertificateNumber = "";
                    string transporterBankDetailsEDRPOU = "";
                    string transporterBankDetailsAccountNumber = "";
                    string transporterBankDetailsBankName = "";
                    string transporterBankDetailsMFO = "";
                    string transporterBankDetailsIBAN = "";
                    string transporterBankDetailsIPN = "";
                    string transporterBankDetailsSWIFT = "";

                    if (transporterDocument.WorkDocumentId != null)
                    {
                        transporterWorkDocument = db.WorkDocuments.Find(transporterDocument.WorkDocumentId).Status;
                    }

                    if (transporterDocument.TaxPayerStatusId != null)
                    {
                        transporterTaxPayerStatus = db.TaxPayerStatus.Find(transporterDocument.TaxPayerStatusId).Status;
                    }

                    if (db.Transporters.Find(transporterDocument.Id).TransporterBankDetail != null)
                    {
                        transporterBankDetailsCertificateSerial = db.TransporterBankDetails.Find(transporterDocument.Id).CertificateSerial;
                        transporterBankDetailsCertificateNumber = db.TransporterBankDetails.Find(transporterDocument.Id).CertificateNamber;
                        transporterBankDetailsEDRPOU = db.TransporterBankDetails.Find(transporterDocument.Id).EDRPOU;
                        transporterBankDetailsAccountNumber = db.TransporterBankDetails.Find(transporterDocument.Id).AccountNumber;
                        transporterBankDetailsBankName = db.TransporterBankDetails.Find(transporterDocument.Id).BankName;
                        transporterBankDetailsMFO = db.TransporterBankDetails.Find(transporterDocument.Id).MFO;
                        transporterBankDetailsIBAN = db.TransporterBankDetails.Find(transporterDocument.Id).IBAN;
                        transporterBankDetailsIPN = db.TransporterBankDetails.Find(transporterDocument.Id).IPN;
                        transporterBankDetailsSWIFT = db.TransporterBankDetails.Find(transporterDocument.Id).SWIFT;
                    }

                    if (transporterFullName != "" && transporterDirector != "" && transporterWorkDocument != "" &&
                        transporterTaxPayerStatus != "" && transporterBankDetailsBankName != "" && transporterBankDetailsAccountNumber != "" &&
                        transporterBankDetailsCertificateNumber != "" && transporterBankDetailsCertificateSerial != "" && transporterBankDetailsEDRPOU != "" && transporterBankDetailsIBAN != "" &&
                        transporterBankDetailsIPN != "" && transporterBankDetailsMFO != "" && transporterBankDetailsSWIFT != "")
                    {
                        isTransporterFull = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
            }
            return isTransporterFull;
        }

        bool IsClientFull()
        {
            bool isClientFull = false;
            try
            {
                using (var db = new AtlantSovtContext())
                {
                    var clientName = clientDocument.Name;
                    var clientDirector = clientDocument.Director;
                    var clientPhysicalAddress = clientDocument.PhysicalAddress;
                    var clientGeographycalAddress = clientDocument.GeografphyAddress;

                    string clientWorkDocument = "";
                    string clientTaxPayerStatus = "";
                    string clientBankDetailsCertificateSerial = "";
                    string clientBankDetailsCertificateNumber = "";
                    string clientBankDetailsEDRPOU = "";
                    string clientBankDetailsAccountNumber = "";
                    string clientBankDetailsBankName = "";
                    string clientBankDetailsMFO = "";
                    string clientBankDetailsIBAN = "";
                    string clientBankDetailsIPN = "";
                    string clientBankDetailsSWIFT = "";

                    if (clientDocument.WorkDocumentId != null)
                    {
                        clientWorkDocument = db.WorkDocuments.Find(clientDocument.WorkDocumentId).Status;
                    }

                    if (clientDocument.TaxPayerStatusId != null)
                    {
                        clientTaxPayerStatus = db.TaxPayerStatus.Find(clientDocument.TaxPayerStatusId).Status;
                    }

                    if (db.Clients.Find(clientDocument.Id).ClientBankDetail != null)
                    {
                        clientBankDetailsCertificateSerial = db.ClientBankDetails.Find(clientDocument.Id).CertificateSerial;
                        clientBankDetailsCertificateNumber = db.ClientBankDetails.Find(clientDocument.Id).CertificateNamber;
                        clientBankDetailsEDRPOU = db.ClientBankDetails.Find(clientDocument.Id).EDRPOU;
                        clientBankDetailsAccountNumber = db.ClientBankDetails.Find(clientDocument.Id).AccountNumber;
                        clientBankDetailsBankName = db.ClientBankDetails.Find(clientDocument.Id).BankName;
                        clientBankDetailsMFO = db.ClientBankDetails.Find(clientDocument.Id).MFO;
                        clientBankDetailsIBAN = db.ClientBankDetails.Find(clientDocument.Id).IBAN;
                        clientBankDetailsIPN = db.ClientBankDetails.Find(clientDocument.Id).IPN;
                        clientBankDetailsSWIFT = db.ClientBankDetails.Find(clientDocument.Id).SWIFT;
                    }

                    if (clientName != "" && clientDirector != "" && clientWorkDocument != "" &&
                        clientTaxPayerStatus != "" && clientBankDetailsBankName != "" && clientBankDetailsAccountNumber != "" &&
                        clientBankDetailsCertificateNumber != "" && clientBankDetailsCertificateSerial != "" && clientBankDetailsEDRPOU != "" && clientBankDetailsIBAN != "" &&
                        clientBankDetailsIPN != "" && clientBankDetailsMFO != "" && clientBankDetailsSWIFT != "")
                    {
                        isClientFull = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
            }
            return isClientFull;
        }
        
        void IsDataComplete()
        {
            bool check = false;
            string isFull = AtlantSovt.Properties.Resources.Деякі_дані_не_заповнені_в;
            try
            {
                using (var db = new AtlantSovtContext())
                {

                    if (firstForwarderDocument != null)
                    {
                        if (IsForwarderFull(firstForwarderDocument) != true)
                        {
                            check = true;
                            isFull += AtlantSovt.Properties.Resources._Експедитор_1_;
                        }  
                    }
                    if (transporterDocument != null && secondPersonRoleDocumentСomboBox.SelectedIndex == 0)
                    {
                        if (IsTransporterFull() != true)
                        {
                            check = true;
                            isFull += AtlantSovt.Properties.Resources._Перевізник_;
                        }
                    }
                    else if (clientDocument != null && secondPersonRoleDocumentСomboBox.SelectedIndex == 1)
                    {
                        if (IsClientFull() != true)
                        {
                            check = true;
                            isFull += AtlantSovt.Properties.Resources._Клієнт_;
                        }
                    }
                    else if (secondForwarderDocument != null && secondPersonRoleDocumentСomboBox.SelectedIndex == 2)
                    {
                        if (IsForwarderFull(secondForwarderDocument) != true)
                        {
                            check = true;
                            isFull += AtlantSovt.Properties.Resources._Експедитор_2_;
                        }  
                    }

                    if(check)
                    {
                        if (MessageBox.Show(isFull + AtlantSovt.Properties.Resources.Продовжити_без_повного_заповнення_даних, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            AddContract();
                            CreateContract();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        AddContract();
                        CreateContract();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
            }
        }
        
        void ShowContract()
        {
            contractShowTransporterContactDataGridView.Visible = false;

            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.Contracts
                orderby c.Id
                select
                new
                {
                    Id = c.Id,
                    ContractNumber = c.ContractNumber + @"/" + c.ContractDataBegin.Value.Year,
                    Forwarder = c.ForwarderContracts.Where(f => f.IsFirst == 0).FirstOrDefault().Forwarder.Name,
                    Person2 = (c.ForwarderContracts.Where(f => f.IsFirst == 1).Count() == 1) ? AtlantSovt.Properties.Resources._Експедитор0 + c.ForwarderContracts.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder.Name : (c.TransporterId != null) ? AtlantSovt.Properties.Resources._Перевізник0 + c.Transporter.FullName : AtlantSovt.Properties.Resources._Клієнт0 + c.Client.Name,
                    ContractDateBegin = c.ContractDataBegin,
                    ContractDateEnd = c.ContractDataEnd,
                    Type = (c.Type == 0) ? AtlantSovt.Properties.Resources.По_Україні : AtlantSovt.Properties.Resources.За_кордон,
                    Template = c.TemplateName
                };

                contractShowDataGridView.DataSource = query.ToList();
                contractShowDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                contractShowDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_договору;
                contractShowDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Експедитор;
                contractShowDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Другий_учасник_договору;
                contractShowDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_початку;
                contractShowDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завершення;
                contractShowDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Напрямок_1;
                contractShowDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Використаний_шаблон;

            } contractShowDataGridView.Update();
        }
        
        void ShowContractSearch()
        {
            contractShowTransporterContactDataGridView.Visible = false;

            var text = contractShowSearchTextBox.Text;
            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.Contracts
                where (c.ForwarderContracts.Any(f => f.Forwarder.Name.Contains(text)) || c.Client.Name.Contains(text) ||c.Transporter.FullName.Contains(text) || c.Transporter.ShortName.Contains(text) || c.Transporter.TransporterContacts.Any(con => con.ContactPerson.Contains(text)) || c.Transporter.TransporterContacts.Any(con => con.Email.Contains(text)))
                select
                new
                {
                    Id = c.Id,
                    ContractNumber = c.ContractNumber + @"/" + c.ContractDataBegin.Value.Year,
                    Forwarder = c.ForwarderContracts.Where(f => f.IsFirst == 0).FirstOrDefault().Forwarder.Name,
                    Person2 = (c.ForwarderContracts.Where(f => f.IsFirst == 1).Count() == 1) ? AtlantSovt.Properties.Resources._Експедитор0 + c.ForwarderContracts.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder.Name : (c.TransporterId != null) ? AtlantSovt.Properties.Resources._Перевізник0 + c.Transporter.FullName : AtlantSovt.Properties.Resources._Клієнт0 + c.Client.Name,
                    ContractDateBegin = c.ContractDataBegin,
                    ContractDateEnd = c.ContractDataEnd,
                    Type = (c.Type == 0) ? AtlantSovt.Properties.Resources.По_Україні : AtlantSovt.Properties.Resources.За_кордон,
                    Template = c.TemplateName
                };

                contractShowDataGridView.DataSource = query.ToList();
                contractShowDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                contractShowDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_договору;
                contractShowDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Експедитор;
                contractShowDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Другий_учасник_договору;
                contractShowDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_початку;
                contractShowDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завершення;
                contractShowDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Напрямок_1;
                contractShowDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Використаний_шаблон;

            } contractShowDataGridView.Update();
        }
        
        void ContractChangeState(RadioButton radioButton)
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    Contract contract;
                    int ClikedId = Convert.ToInt32(contractShowDataGridView.CurrentRow.Cells[0].Value);
                    contract = db.Contracts.Find(ClikedId);

                    if (radioButton.Name == "notSelectedContractStateRadioButton")
                    {
                        contract.State = null;
                        db.Entry(contract).State = EntityState.Modified;
                        db.SaveChanges();
                       
                    }
                    else if (radioButton.Name == "originalContractStateRadioButton")
                    {
                        contract.State = true;
                        db.Entry(contract).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else if (radioButton.Name == "faxContractStateRadioButton")
                    {
                        contract.State = false;
                        db.Entry(contract).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_договору);
                }
            }
        }
        
        void ShowContractInfo()
        {
             using (var db = new AtlantSovtContext())
            {
                try
                {
                    var ClikedId = Convert.ToInt32(contractShowDataGridView.CurrentRow.Cells[0].Value);

                    var getContractState =
                        from contract in db.Contracts
                        where contract.Id == ClikedId
                        select contract;

                    if (getContractState.FirstOrDefault().State == true)
                    {
                        originalContractStateRadioButton.Checked = true;
                    }
                    else if (getContractState.FirstOrDefault().State == false)
                    {
                        faxContractStateRadioButton.Checked = true;
                    }
                    else
                    {
                        notSelectedContractStateRadioButton.Checked = true;
                    }

                    var query =
                    from con in db.TransporterContacts
                    where con.TransporterId ==
                    (
                        from c in db.Contracts
                        where c.Id == ClikedId
                        select c
                    ).FirstOrDefault().TransporterId
                    select new
                    {
                        Контактна_персона = con.ContactPerson,
                        Номер = con.TelephoneNumber,
                        Факс = con.FaxNumber,
                        Email = con.Email,
                    };
                    contractShowTransporterContactDataGridView.DataSource = query.ToList();
                    contractShowTransporterContactDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Контактна_особа;
                    contractShowTransporterContactDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Телефон;
                    contractShowTransporterContactDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Факс;
                    contractShowTransporterContactDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Email;

                    contractShowOpenDocButton.Enabled = true;
                    contractShowDeleteContractButton.Enabled = true;
                    notSelectedContractStateRadioButton.Enabled = true;
                    originalContractStateRadioButton.Enabled = true;
                    faxContractStateRadioButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    contractShowOpenDocButton.Enabled = false;
                    contractShowDeleteContractButton.Enabled = false;
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_договору);
                }
            }
            contractShowTransporterContactDataGridView.Update();
            contractShowTransporterContactDataGridView.Visible = true;
        }
        
        void AddContract()
        {
            contractOutputFilePath = "";

            if (contractFilecheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Обов_язково_виберіть_шаблон);
                return;
            }

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                contractOutputFilePath = folderBrowserDialog.SelectedPath;
            }
            else
            {
                return;
            }

            using (var db = new AtlantSovtContext())
            {
                var query =
                    from d in db.DocumentCounters
                    select d;

                try
                {
                    if (query.Count() == 0)
                    {
                        contractCount = new DocumentCounter
                        {
                            Id = 1,
                            ForeignDocument = 0,
                            LocalDocument = 0
                        };
                        db.DocumentCounters.Add(contractCount);
                        db.SaveChanges();
                        contractCount = null;
                    }
                }
                catch(Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                }
                contractCount = new DocumentCounter();
                try
                {
                    if (contractType == 0)
                    {
                        contractCount = db.DocumentCounters.Find(1);
                        contractCount.LocalDocument += 1;
                        db.Entry(contractCount).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        contractCount = db.DocumentCounters.Find(1);
                        contractCount.ForeignDocument += 1;
                        db.Entry(contractCount).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                }

                try
                {
                    contract = new Contract();
                    if(firstForwarderDocument != null)
                    {
                        ForwarderContract forwarder = new ForwarderContract()
                        {
                            ForwarderId = firstForwarderDocument.Id,
                            IsFirst = 0,
                        };
                        contract.ForwarderContracts.Add(forwarder);
                    }
                    if(transporterDocument != null && secondPersonRoleDocumentСomboBox.SelectedIndex == 0)
                    {
                        contract.TransporterId = transporterDocument.Id;
                        contract.ClientId = null;
                    }
                    else if (clientDocument != null && secondPersonRoleDocumentСomboBox.SelectedIndex == 1)
                    {
                        contract.ClientId = clientDocument.Id;
                        contract.TransporterId = null;
                    }
                    else if(secondForwarderDocument != null && secondPersonRoleDocumentСomboBox.SelectedIndex == 2)
                    {
                        ForwarderContract forwarder = new ForwarderContract()
                        {
                            ForwarderId = firstForwarderDocument.Id,
                            IsFirst = 1,
                        };
                        contract.ClientId = null;
                        contract.TransporterId = null;
                        contract.ForwarderContracts.Add(forwarder);
                    }
                    if (contractType == 0)
                    {
                        contract.ContractNumber = contractCount.LocalDocument;
                        contract.Type = 0;
                    }
                    else if (contractType == 1)
                    {
                        contract.ContractNumber = contractCount.ForeignDocument;
                        contract.Type = 1;
                    }

                    contract.TemplateName = contractFilecheckedListBox.SelectedItem.ToString();

                    contract.ContractDataBegin = contractBeginDateTimePicker.Value.Date;
                    contract.ContractDataEnd = contractBeginDateTimePicker.Value.AddYears(1);
                    db.Contracts.Add(contract);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                }
            }
        }
        
        void DeleteContract()
        {
            try
            {
                var ClikedId = Convert.ToInt32(contractShowDataGridView.CurrentRow.Cells[0].Value);

                using (var db = new AtlantSovtContext())
                {
                    contract = db.Contracts.Find(ClikedId);

                    if (contract != null)
                    {
                        if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_договір + contract.ContractNumber + @"\" + contract.ContractDataBegin.Value.Year + " ?", AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                db.Contracts.Attach(contract);
                                db.Contracts.Remove(contract);
                                db.SaveChanges();
                                MessageBox.Show(AtlantSovt.Properties.Resources.Договір_успішно_видалений);
                            }
                            catch (Exception ex)
                            {
                                Log.Write(ex);
                                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_договору);
                contractShowOpenDocButton.Enabled = false;
            }
        }
        
        void CreateContract()
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            if(GetDocumentPath() == "")
            {
                return;
            }
            var wordDocument = wordApp.Documents.Open(GetDocumentPath());
            try
            {
                using (var db = new AtlantSovtContext())
                {
                    string title;
                    contract = db.Contracts.Find(contract.Id);
                    if(contract.ForwarderContracts.Where(f => f.IsFirst == 0).Count() != 0)
                    {
                        firstForwarderDocument = contract.ForwarderContracts.Where(f => f.IsFirst == 0).FirstOrDefault().Forwarder;
                        db.Forwarders.Include(firstForwarderDocument.ForwarderStamp.ToString());
                        firstForwarderStamp = firstForwarderDocument.ForwarderStamp;

                    }
                    else
                    {
                        firstForwarderDocument = null;
                    }
                    if (contract.ForwarderContracts.Where(f => f.IsFirst == 1).Count() != 0)
                    {
                        secondForwarderDocument = contract.ForwarderContracts.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder;
                        db.Forwarders.Include(secondForwarderDocument.ForwarderStamp.ToString());
                        secondForwarderStamp = secondForwarderDocument.ForwarderStamp;
                    }
                    else
                    {
                        secondForwarderDocument = null;
                    }
                    if(contract.Transporter != null )
                    {
                        transporterDocument = contract.Transporter;
                    }
                    else
                    {
                        transporterDocument = null;
                    }
                    if(contract.Client != null)
                    {
                        clientDocument = contract.Client;
                    }
                    else
                    {
                        clientDocument = null;
                    }
                    if (contractType == 0)
                    {
                        title = contract.ContractNumber + @"/" + contract.ContractDataBegin.Value.Year + @"/";
                    }
                    else
                    {
                        title = contract.ContractNumber + @"/" + contract.ContractDataBegin.Value.Year + @"/P";
                    }

                    var contractDateBegin = contract.ContractDataBegin;
                    var contractDateEnd = contract.ContractDataEnd;

                    ReplaseWordStub("{ContractNumber}", title, wordDocument);

                    ReplaseWordStub("{ContractDateBegin}", contractDateBegin.Value.ToString("D", new System.Globalization.CultureInfo("uk-UA")), wordDocument);
                    ReplaseWordStub("{RusContractDateBegin}", contractDateBegin.Value.ToString("D", new System.Globalization.CultureInfo("ru-RU")), wordDocument);
                    ReplaseWordStub("{EngContractDateBegin}", contractDateBegin.Value.ToString("D", new System.Globalization.CultureInfo("en-US")), wordDocument);
                    ReplaseWordStub("{GerContractDateBegin}", contractDateBegin.Value.ToString("D", new System.Globalization.CultureInfo("de-DE")), wordDocument);

                    ReplaseWordStub("{ContractDateEnd}", contractDateEnd.Value.ToString("D", new System.Globalization.CultureInfo("uk-UA")), wordDocument);
                    ReplaseWordStub("{RusContractDateEnd}", contractDateEnd.Value.ToString("D", new System.Globalization.CultureInfo("ru-RU")), wordDocument);
                    ReplaseWordStub("{EngContractDateEnd}", contractDateEnd.Value.ToString("D", new System.Globalization.CultureInfo("en-US")), wordDocument);
                    ReplaseWordStub("{GerContractDateEnd}", contractDateEnd.Value.ToString("D", new System.Globalization.CultureInfo("de-DE")), wordDocument);

                    if (firstForwarderDocument != null)
                    {
                        var firstForwarderName = (firstForwarderDocument.Name == "" || firstForwarderDocument.Name == null) ? "_______________" : firstForwarderDocument.Name;
                        var firstForwarderDirector = (firstForwarderDocument.Director == "" || firstForwarderDocument.Director == null) ? "_______________" : firstForwarderDocument.Director;
                        var firstForwarderPhysicalAddress = (firstForwarderDocument.PhysicalAddress == "" || firstForwarderDocument.PhysicalAddress == null) ? "_______________" : firstForwarderDocument.PhysicalAddress;
                        var firstForwarderGeographycalAddress = (firstForwarderDocument.GeographyAddress == "" || firstForwarderDocument.GeographyAddress == null) ? "_______________" : firstForwarderDocument.GeographyAddress;

                        string firstForwarderWorkDocument = "_______________";
                        string firstForwarderTaxPayerStatus = "_______________";
                        string firstForwarderBankDetailsCertificateSerial = "_______________";
                        string firstForwarderBankDetailsCertificateNumber = "_______________";
                        string firstForwarderBankDetailsEDRPOU = "_______________";
                        string firstForwarderBankDetailsAccountNumber = "_______________";
                        string firstForwarderBankDetailsBankName = "_______________";
                        string firstForwarderBankDetailsMFO = "_______________";
                        string firstForwarderBankDetailsIBAN = "_______________";
                        string firstForwarderBankDetailsIPN = "_______________";
                        string firstForwarderBankDetailsSWIFT = "_______________";

                        if (firstForwarderDocument.WorkDocumentId != null)
                        {
                            firstForwarderWorkDocument = db.WorkDocuments.Find(firstForwarderDocument.WorkDocumentId).Status;
                        }

                        if (firstForwarderDocument.TaxPayerStatusId != null)
                        {
                            firstForwarderTaxPayerStatus = db.TaxPayerStatus.Find(firstForwarderDocument.TaxPayerStatusId).Status;
                        }

                        if (db.Forwarders.Find(firstForwarderDocument.Id).ForwarderBankDetail != null)
                        {
                            firstForwarderBankDetailsCertificateSerial = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).CertificateSerial == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).CertificateSerial == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).CertificateSerial;
                            firstForwarderBankDetailsCertificateNumber = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).CertificateNamber == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).CertificateNamber == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).CertificateNamber;
                            firstForwarderBankDetailsEDRPOU = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).EDRPOU == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).EDRPOU == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).EDRPOU;
                            firstForwarderBankDetailsAccountNumber = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).AccountNumber == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).AccountNumber == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).AccountNumber;
                            firstForwarderBankDetailsBankName = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).BankName == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).BankName == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).BankName;
                            firstForwarderBankDetailsMFO = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).MFO == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).MFO == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).MFO;
                            firstForwarderBankDetailsIBAN = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).IBAN == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).IBAN == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).IBAN;
                            firstForwarderBankDetailsIPN = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).IPN == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).IPN == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).IPN;
                            firstForwarderBankDetailsSWIFT = (db.ForwarderBankDetails.Find(firstForwarderDocument.Id).SWIFT == "" || db.ForwarderBankDetails.Find(firstForwarderDocument.Id).SWIFT == null) ? "_______________" : db.ForwarderBankDetails.Find(firstForwarderDocument.Id).SWIFT;
                        }

                        ReplaseWordStub("{FirstForwarderName}", firstForwarderName, wordDocument);
                        ReplaseWordStub("{FirstForwarderDirector}", firstForwarderDirector, wordDocument);
                        ReplaseWordStub("{FirstForwarderWorkDocument}", firstForwarderWorkDocument, wordDocument);
                        ReplaseWordStub("{FirstForwarderTaxPayerStatus}", firstForwarderTaxPayerStatus, wordDocument);
                        ReplaseWordStub("{FirstForwarderPhysicalAddress}", firstForwarderPhysicalAddress, wordDocument);
                        ReplaseWordStub("{FirstForwarderGeographycalAddress}", firstForwarderGeographycalAddress, wordDocument);
                        ReplaseWordStub("{FirstForwarderCerificateSerial}", firstForwarderBankDetailsCertificateSerial, wordDocument);
                        ReplaseWordStub("{FirstForwarderCerificateNumber}", firstForwarderBankDetailsCertificateNumber, wordDocument);
                        ReplaseWordStub("{FirstForwarderEDRPOU}", firstForwarderBankDetailsEDRPOU, wordDocument);
                        ReplaseWordStub("{FirstForwarderAccountNumber}", firstForwarderBankDetailsAccountNumber, wordDocument);
                        ReplaseWordStub("{FirstForwarderBankName}", firstForwarderBankDetailsBankName, wordDocument);
                        ReplaseWordStub("{FirstForwarderMFO}", firstForwarderBankDetailsMFO, wordDocument);

                        if (firstForwarderStamp.Stamp != null)
                        {
                            AddStamp(wordDocument, UploadForwarderStapm(firstForwarderDocument), "{FirstForwarderStamp}");
                            AddStamp(wordDocument, UploadForwarderStapm(firstForwarderDocument), "{FirstForwarderStamp1}");
                            Directory.Delete((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp\").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""), true);
                            Directory.CreateDirectory((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""));
                        }
                    }
                    if (transporterDocument != null)
                    {
                        clientDocument = null;
                        var transporterFullName = (transporterDocument.FullName == "" || transporterDocument.FullName == null) ? "_______________" : transporterDocument.FullName;
                        var transporterDirector = (transporterDocument.Director == "" || transporterDocument.Director == null) ? "_______________" : transporterDocument.Director;
                        var transporterPhysicalAddress = (transporterDocument.PhysicalAddress == "" || transporterDocument.PhysicalAddress == null) ? "_______________" : transporterDocument.PhysicalAddress;
                        var transporterGeographycalAddress = (transporterDocument.GeographyAddress == "" || transporterDocument.GeographyAddress == null) ? "_______________" : transporterDocument.GeographyAddress;

                        string transporterWorkDocument = "_______________";
                        string transporterTaxPayerStatus = "_______________";
                        string transporterBankDetailsCertificateSerial = "_______________";
                        string transporterBankDetailsCertificateNumber = "_______________";
                        string transporterBankDetailsEDRPOU = "_______________";
                        string transporterBankDetailsAccountNumber = "_______________";
                        string transporterBankDetailsBankName = "_______________";
                        string transporterBankDetailsMFO = "_______________";
                        string transporterBankDetailsIBAN = "_______________";
                        string transporterBankDetailsIPN = "_______________";
                        string transporterBankDetailsSWIFT = "_______________";

                        if (transporterDocument.WorkDocumentId != null)
                        {
                            transporterWorkDocument = db.WorkDocuments.Find(transporterDocument.WorkDocumentId).Status;
                        }

                        if (transporterDocument.TaxPayerStatusId != null)
                        {
                            transporterTaxPayerStatus = db.TaxPayerStatus.Find(transporterDocument.TaxPayerStatusId).Status;
                        }

                        if (db.Transporters.Find(transporterDocument.Id).TransporterBankDetail != null)
                        {
                            transporterBankDetailsCertificateSerial = (db.TransporterBankDetails.Find(transporterDocument.Id).CertificateSerial == "" || db.TransporterBankDetails.Find(transporterDocument.Id).CertificateSerial == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).CertificateSerial;
                            transporterBankDetailsCertificateNumber = (db.TransporterBankDetails.Find(transporterDocument.Id).CertificateNamber == "" || db.TransporterBankDetails.Find(transporterDocument.Id).CertificateNamber == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).CertificateNamber;
                            transporterBankDetailsEDRPOU = (db.TransporterBankDetails.Find(transporterDocument.Id).EDRPOU == "" || db.TransporterBankDetails.Find(transporterDocument.Id).EDRPOU == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).EDRPOU;
                            transporterBankDetailsAccountNumber = (db.TransporterBankDetails.Find(transporterDocument.Id).AccountNumber == "" || db.TransporterBankDetails.Find(transporterDocument.Id).AccountNumber == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).AccountNumber;
                            transporterBankDetailsBankName = (db.TransporterBankDetails.Find(transporterDocument.Id).BankName == "" || db.TransporterBankDetails.Find(transporterDocument.Id).BankName == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).BankName;
                            transporterBankDetailsMFO = (db.TransporterBankDetails.Find(transporterDocument.Id).MFO == "" || db.TransporterBankDetails.Find(transporterDocument.Id).MFO == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).MFO;
                            transporterBankDetailsIBAN = (db.TransporterBankDetails.Find(transporterDocument.Id).IBAN == "" || db.TransporterBankDetails.Find(transporterDocument.Id).IBAN == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).IBAN;
                            transporterBankDetailsIPN = (db.TransporterBankDetails.Find(transporterDocument.Id).IPN == "" || db.TransporterBankDetails.Find(transporterDocument.Id).IPN == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).IPN;
                            transporterBankDetailsSWIFT = (db.TransporterBankDetails.Find(transporterDocument.Id).SWIFT == "" || db.TransporterBankDetails.Find(transporterDocument.Id).SWIFT == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).SWIFT;
                        }

                        ReplaseWordStub("{TransporterName}", transporterFullName, wordDocument);
                        ReplaseWordStub("{TransporterDirector}", transporterDirector, wordDocument);
                        ReplaseWordStub("{TransporterWorkDocument}", transporterWorkDocument, wordDocument);
                        ReplaseWordStub("{TransporterTaxPayerStatus}", transporterTaxPayerStatus, wordDocument);
                        ReplaseWordStub("{TransporterPhysicalAddress}", transporterPhysicalAddress, wordDocument);
                        ReplaseWordStub("{TransporterGeographycalAddress}", transporterGeographycalAddress, wordDocument);
                        ReplaseWordStub("{TransporterCerificateSerial}", transporterBankDetailsCertificateSerial, wordDocument);
                        ReplaseWordStub("{TransporterCerificateNumber}", transporterBankDetailsCertificateNumber, wordDocument);
                        ReplaseWordStub("{TransporterEDRPOU}", transporterBankDetailsEDRPOU, wordDocument);
                        ReplaseWordStub("{TransporterAccountNumber}", transporterBankDetailsAccountNumber, wordDocument);
                        ReplaseWordStub("{TransporterBankName}", transporterBankDetailsBankName, wordDocument);
                        ReplaseWordStub("{TransporterMFO}", transporterBankDetailsMFO, wordDocument);
                    }
                    else if (clientDocument != null)
                    {
                        transporterDocument = null;
                        var clientName = (clientDocument.Name == "" || clientDocument.Name == null) ? "_______________" : clientDocument.Name;
                        var clientDirector = (clientDocument.Director == "" || clientDocument.Director == null) ? "_______________" : clientDocument.Director;
                        var clientPhysicalAddress = (clientDocument.PhysicalAddress == "" || clientDocument.PhysicalAddress == null) ? "_______________" : clientDocument.PhysicalAddress;
                        var clientGeographycalAddress = (clientDocument.GeografphyAddress == "" || clientDocument.GeografphyAddress == null) ? "_______________" : clientDocument.GeografphyAddress;

                        string clientWorkDocument = "_______________";
                        string clientTaxPayerStatus = "_______________";
                        string clientBankDetailsCertificateSerial = "_______________";
                        string clientBankDetailsCertificateNumber = "_______________";
                        string clientBankDetailsEDRPOU = "_______________";
                        string clientBankDetailsAccountNumber = "_______________";
                        string clientBankDetailsBankName = "_______________";
                        string clientBankDetailsMFO = "_______________";
                        string clientBankDetailsIBAN = "_______________";
                        string clientBankDetailsIPN = "_______________";
                        string clientBankDetailsSWIFT = "_______________";

                        if (clientDocument.WorkDocumentId != null)
                        {
                            clientWorkDocument = db.WorkDocuments.Find(clientDocument.WorkDocumentId).Status;
                        }

                        if (clientDocument.TaxPayerStatusId != null)
                        {
                            clientTaxPayerStatus = db.TaxPayerStatus.Find(clientDocument.TaxPayerStatusId).Status;
                        }

                        if (db.Clients.Find(clientDocument.Id).ClientBankDetail != null)
                        {
                            clientBankDetailsCertificateSerial = (db.ClientBankDetails.Find(clientDocument.Id).CertificateSerial == "" || db.ClientBankDetails.Find(clientDocument.Id).CertificateSerial == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).CertificateSerial;
                            clientBankDetailsCertificateNumber = (db.ClientBankDetails.Find(clientDocument.Id).CertificateNamber == "" || db.ClientBankDetails.Find(clientDocument.Id).CertificateNamber == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).CertificateNamber;
                            clientBankDetailsEDRPOU = (db.ClientBankDetails.Find(clientDocument.Id).EDRPOU == "" || db.ClientBankDetails.Find(clientDocument.Id).EDRPOU == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).EDRPOU;
                            clientBankDetailsAccountNumber = (db.ClientBankDetails.Find(clientDocument.Id).AccountNumber == "" || db.ClientBankDetails.Find(clientDocument.Id).AccountNumber == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).AccountNumber;
                            clientBankDetailsBankName = (db.ClientBankDetails.Find(clientDocument.Id).BankName == "" || db.ClientBankDetails.Find(clientDocument.Id).BankName == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).BankName;
                            clientBankDetailsMFO = (db.ClientBankDetails.Find(clientDocument.Id).MFO == "" || db.ClientBankDetails.Find(clientDocument.Id).MFO == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).MFO;
                            clientBankDetailsIBAN = (db.ClientBankDetails.Find(clientDocument.Id).IBAN == "" || db.ClientBankDetails.Find(clientDocument.Id).IBAN == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).IBAN;
                            clientBankDetailsIPN = (db.ClientBankDetails.Find(clientDocument.Id).IPN == "" || db.ClientBankDetails.Find(clientDocument.Id).IPN == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).IPN;
                            clientBankDetailsSWIFT = (db.ClientBankDetails.Find(clientDocument.Id).SWIFT == "" || db.ClientBankDetails.Find(clientDocument.Id).SWIFT == null) ? "_______________" : db.ClientBankDetails.Find(clientDocument.Id).SWIFT;
                        }

                        ReplaseWordStub("{ClientName}", clientName, wordDocument);
                        ReplaseWordStub("{ClientDirector}", clientDirector, wordDocument);
                        ReplaseWordStub("{ClientWorkDocument}", clientWorkDocument, wordDocument);
                        ReplaseWordStub("{ClientTaxPayerStatus}", clientTaxPayerStatus, wordDocument);
                        ReplaseWordStub("{ClientPhysicalAddress}", clientPhysicalAddress, wordDocument);
                        ReplaseWordStub("{ClientGeographycalAddress}", clientGeographycalAddress, wordDocument);
                        ReplaseWordStub("{ClientCerificateSerial}", clientBankDetailsCertificateSerial, wordDocument);
                        ReplaseWordStub("{ClientCerificateNumber}", clientBankDetailsCertificateNumber, wordDocument);
                        ReplaseWordStub("{ClientEDRPOU}", clientBankDetailsEDRPOU, wordDocument);
                        ReplaseWordStub("{ClientAccountNumber}", clientBankDetailsAccountNumber, wordDocument);
                        ReplaseWordStub("{ClientBankName}", clientBankDetailsBankName, wordDocument);
                        ReplaseWordStub("{ClientMFO}", clientBankDetailsMFO, wordDocument);
                    }
                    else if (secondForwarderDocument != null)
                    {
                        clientDocument = null;
                        transporterDocument = null;

                        var secondForwarderName = (secondForwarderDocument.Name == "" || secondForwarderDocument.Name == null) ? "_______________" : secondForwarderDocument.Name;
                        var secondForwarderDirector = (secondForwarderDocument.Director == "" || secondForwarderDocument.Director == null) ? "_______________" : secondForwarderDocument.Director;
                        var secondForwarderPhysicalAddress = (secondForwarderDocument.PhysicalAddress == "" || secondForwarderDocument.PhysicalAddress == null) ? "_______________" : secondForwarderDocument.PhysicalAddress;
                        var secondForwarderGeographycalAddress = (secondForwarderDocument.GeographyAddress == "" || secondForwarderDocument.GeographyAddress == null) ? "_______________" : secondForwarderDocument.GeographyAddress;

                        string secondForwarderWorkDocument = "_______________";
                        string secondForwarderTaxPayerStatus = "_______________";
                        string secondForwarderBankDetailsCertificateSerial = "_______________";
                        string secondForwarderBankDetailsCertificateNumber = "_______________";
                        string secondForwarderBankDetailsEDRPOU = "_______________";
                        string secondForwarderBankDetailsAccountNumber = "_______________";
                        string secondForwarderBankDetailsBankName = "_______________";
                        string secondForwarderBankDetailsMFO = "_______________";
                        string secondForwarderBankDetailsIBAN = "_______________";
                        string secondForwarderBankDetailsIPN = "_______________";
                        string secondForwarderBankDetailsSWIFT = "_______________";

                        if (secondForwarderDocument.WorkDocumentId != null)
                        {
                            secondForwarderWorkDocument = db.WorkDocuments.Find(secondForwarderDocument.WorkDocumentId).Status;
                        }

                        if (secondForwarderDocument.TaxPayerStatusId != null)
                        {
                            secondForwarderTaxPayerStatus = db.TaxPayerStatus.Find(secondForwarderDocument.TaxPayerStatusId).Status;
                        }

                        if (db.Forwarders.Find(secondForwarderDocument.Id).ForwarderBankDetail != null)
                        {
                            secondForwarderBankDetailsCertificateSerial = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).CertificateSerial == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).CertificateSerial == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).CertificateSerial;
                            secondForwarderBankDetailsCertificateNumber = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).CertificateNamber == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).CertificateNamber == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).CertificateNamber;
                            secondForwarderBankDetailsEDRPOU = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).EDRPOU == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).EDRPOU == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).EDRPOU;
                            secondForwarderBankDetailsAccountNumber = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).AccountNumber == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).AccountNumber == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).AccountNumber;
                            secondForwarderBankDetailsBankName = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).BankName == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).BankName == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).BankName;
                            secondForwarderBankDetailsMFO = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).MFO == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).MFO == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).MFO;
                            secondForwarderBankDetailsIBAN = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).IBAN == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).IBAN == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).IBAN;
                            secondForwarderBankDetailsIPN = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).IPN == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).IPN == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).IPN;
                            secondForwarderBankDetailsSWIFT = (db.ForwarderBankDetails.Find(secondForwarderDocument.Id).SWIFT == "" || db.ForwarderBankDetails.Find(secondForwarderDocument.Id).SWIFT == null) ? "_______________" : db.ForwarderBankDetails.Find(secondForwarderDocument.Id).SWIFT;
                        }

                        ReplaseWordStub("{SecondForwarderName}", secondForwarderName, wordDocument);
                        ReplaseWordStub("{SecondForwarderDirector}", secondForwarderDirector, wordDocument);
                        ReplaseWordStub("{SecondForwarderWorkDocument}", secondForwarderWorkDocument, wordDocument);
                        ReplaseWordStub("{SecondForwarderTaxPayerStatus}", secondForwarderTaxPayerStatus, wordDocument);
                        ReplaseWordStub("{SecondForwarderPhysicalAddress}", secondForwarderPhysicalAddress, wordDocument);
                        ReplaseWordStub("{SecondForwarderGeographycalAddress}", secondForwarderGeographycalAddress, wordDocument);
                        ReplaseWordStub("{SecondForwarderCerificateSerial}", secondForwarderBankDetailsCertificateSerial, wordDocument);
                        ReplaseWordStub("{SecondForwarderCerificateNumber}", secondForwarderBankDetailsCertificateNumber, wordDocument);
                        ReplaseWordStub("{SecondForwarderEDRPOU}", secondForwarderBankDetailsEDRPOU, wordDocument);
                        ReplaseWordStub("{SecondForwarderAccountNumber}", secondForwarderBankDetailsAccountNumber, wordDocument);
                        ReplaseWordStub("{SecondForwarderBankName}", secondForwarderBankDetailsBankName, wordDocument);
                        ReplaseWordStub("{SecondForwarderMFO}", secondForwarderBankDetailsMFO, wordDocument);

                        if (secondForwarderStamp.Stamp != null)
                        {
                            AddStamp(wordDocument, UploadForwarderStapm(secondForwarderDocument), "{SecondForwarderStamp}");
                            AddStamp(wordDocument, UploadForwarderStapm(secondForwarderDocument), "{SecondForwarderStamp1}");
                            Directory.Delete((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp\").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""), true);
                            Directory.CreateDirectory((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""));
                        }
                    }


                    if (contractOutputFilePath != "")
                    {
                        title = title.Replace(@"/", "_");
                        wordDocument.SaveAs(contractOutputFilePath + "\\" + title + ".docx");
                        wordApp.Visible = true;
                    }
                    else
                    {
                        wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
            }
        }

        void OpenWordDoc()
        {
            try
            {
                var ClikedId = Convert.ToInt32(contractShowDataGridView.CurrentRow.Cells[0].Value);

                using (var db = new AtlantSovtContext())
                {
                    contract = db.Contracts.Find(ClikedId);
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        contractOutputFilePath = folderBrowserDialog.SelectedPath;
                        CreateContract();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (NullReferenceException nullClickedId)
            {
                Log.Write(nullClickedId);
                contractShowOpenDocButton.Enabled = false;
                contractShowDeleteContractButton.Enabled = false;
                contractShowTransporterContactDataGridView.Visible = false;
                MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_договору);
            }
        }
        
        string UploadForwarderStapm(Forwarder forwarder)
        {
            string path = "";
            MemoryStream mStream = new MemoryStream(forwarder.ForwarderStamp.Stamp);
            
            Image stamp = Image.FromStream(mStream);
            path = (System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp\" + forwarder.Id + ".png").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", "");
            stamp.Save(path);
            
            return path;
        }

        void AddStamp(Document wordDocument, string path, string bookmark)
        {
            //Word.Range range = wordDocument.Bookmarks.get_Item(bookmark).Range;
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            if(range.Find.Execute(bookmark))
            {
                range.Find.Execute(FindText: bookmark, Replace: Word.WdReplace.wdReplaceNone);
                Word.InlineShape inLineShape = range.InlineShapes.AddPicture(path);
                Word.Shape shape = inLineShape.ConvertToShape();
                shape.WrapFormat.Type = WdWrapType.wdWrapBehind;
                shape.Left = (float)Word.WdShapePosition.wdShapeLeft;
                shape.Top = (float)Word.WdShapePosition.wdShapeTop;
                ReplaseWordStub(bookmark, "", wordDocument);
            }
        }

        void ReplaseWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Replace: Word.WdReplace.wdReplaceAll);
        }

        void GetDocumentFiles()
        {
            contractFilecheckedListBox.Items.Clear();
            string[] searchPatterns = {"*.docx","*.doc","*.rtf"};
            DirectoryInfo directory = new DirectoryInfo((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Contracts\").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""));
            List<FileInfo> files = new List<FileInfo>();
            foreach(string sp in searchPatterns)
            {
                files.AddRange(directory.GetFiles(sp));
            }
            var filteredFiles = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden));
            foreach (var file in filteredFiles)
            {
                string fileName = file.Name;
                contractFilecheckedListBox.Items.Add(fileName);
            }
        }

        string GetDocumentPath()
        {
            string filePath = "";
            try
            {
                filePath = (System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Contracts\" + contract.TemplateName.ToString()).Replace("\\bin\\Release", "").Replace("\\bin\\Debug", "");
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_не_правильний_шлях_шаблону);
            }
            return filePath;
        }

    }
}
