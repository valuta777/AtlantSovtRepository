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
        Forwarder forwarderDocument;
        Client clientDocument;
        ForwarderStamp forwarderStamp;
        DocumentCounter documentCount;
        Contract contract;

        int contractType;

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

        void SplitForwarderDocumentComboBox(ComboBox forwarderComboBox)
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = forwarderComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarderDocument = db.Forwarders.Find(id);
                forwarderStamp = forwarderDocument.ForwarderStamp;
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
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void LoadTransporterDocumentComboBox(ComboBox transporterComboBox, ComboBox transporterDiapason)
        {
            using (var db = new AtlantSovtContext())
            {
                if (transporterDiapason.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
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
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void LoadClientDocumentComboBox(ComboBox clientComboBox, ComboBox clientDiapason)
        {
            using (var db = new AtlantSovtContext())
            {
                if (clientDiapason.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
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
        /*

        bool IsForwarderFull()
        {
            bool isForwarderFull = false;
            try
            {
                using (var db = new AtlantSovtContext())
                {
                    var forwarderName = forwarderDocument.Name;
                    var forwarderDirector = forwarderDocument.Director;
                    var forwarderPhysicalAddress = forwarderDocument.PhysicalAddress;
                    var forwarderGeographycalAddress = forwarderDocument.GeographyAddress;

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

                    if (forwarderDocument.WorkDocumentId != null)
                    {
                        forwarderWorkDocument = db.WorkDocuments.Find(forwarderDocument.WorkDocumentId).Status;
                    }

                    if (forwarderDocument.TaxPayerStatusId != null)
                    {
                        forwarderTaxPayerStatus = db.TaxPayerStatus.Find(forwarderDocument.TaxPayerStatusId).Status;
                    }

                    if (db.Forwarders.Find(forwarderDocument.Id).ForwarderBankDetail != null)
                    {
                        forwarderBankDetailsCertificateSerial = db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateSerial;
                        forwarderBankDetailsCertificateNumber = db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateNamber;
                        forwarderBankDetailsEDRPOU = db.ForwarderBankDetails.Find(forwarderDocument.Id).EDRPOU;
                        forwarderBankDetailsAccountNumber = db.ForwarderBankDetails.Find(forwarderDocument.Id).AccountNumber;
                        forwarderBankDetailsBankName = db.ForwarderBankDetails.Find(forwarderDocument.Id).BankName;
                        forwarderBankDetailsMFO = db.ForwarderBankDetails.Find(forwarderDocument.Id).MFO;
                        forwarderBankDetailsIBAN = db.ForwarderBankDetails.Find(forwarderDocument.Id).IBAN;
                        forwarderBankDetailsIPN = db.ForwarderBankDetails.Find(forwarderDocument.Id).IPN;
                        forwarderBankDetailsSWIFT = db.ForwarderBankDetails.Find(forwarderDocument.Id).SWIFT;
                    }

                    if (forwarderName != "" || forwarderDirector != "" ||
                        forwarderWorkDocument != "" || forwarderTaxPayerStatus != "" ||
                        forwarderBankDetailsBankName != "" || forwarderBankDetailsAccountNumber != "" || forwarderBankDetailsCertificateNumber != "" || forwarderBankDetailsCertificateSerial != "" ||
                        forwarderBankDetailsEDRPOU != "" || forwarderBankDetailsIBAN != "" ||
                        forwarderBankDetailsIPN != "" || forwarderBankDetailsMFO != "" || forwarderBankDetailsSWIFT != "" || forwarderDocument.ForwarderStamp.Stamp != null)
                    {
                        isForwarderFull = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Помилка: " + ex.Message);
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

                    if (transporterFullName != "" || transporterDirector != "" || transporterWorkDocument != "" ||
                        transporterTaxPayerStatus != "" || transporterBankDetailsBankName != "" || transporterBankDetailsAccountNumber != "" ||
                        transporterBankDetailsCertificateNumber != "" || transporterBankDetailsCertificateSerial != "" || transporterBankDetailsEDRPOU != "" || transporterBankDetailsIBAN != "" ||
                        transporterBankDetailsIPN != "" || transporterBankDetailsMFO != "" || transporterBankDetailsSWIFT != "")
                    {
                        isTransporterFull = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
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

                    if (transporterFullName != "" || transporterDirector != "" || transporterWorkDocument != "" ||
                        transporterTaxPayerStatus != "" || transporterBankDetailsBankName != "" || transporterBankDetailsAccountNumber != "" ||
                        transporterBankDetailsCertificateNumber != "" || transporterBankDetailsCertificateSerial != "" || transporterBankDetailsEDRPOU != "" || transporterBankDetailsIBAN != "" ||
                        transporterBankDetailsIPN != "" || transporterBankDetailsMFO != "" || transporterBankDetailsSWIFT != "")
                    {
                        isTransporterFull = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return isClientFull;
        }

        void IsForwarderAndTransporterFull()
        {
            bool check = false;
            bool isForwarderFull = IsForwarderFull();
            bool isTransporterFull = IsTransporterFull();
            bool isClientFull = IsClientFull();
            string isFull = "Деякі дані не заповнені в: ";
            try
            {
                using (var db = new AtlantSovtContext())
                {
                   if(isForwarderFull != true)
                   {
                       check = true;
                       isFull += "\r\n- <Експедитор> ";
                   }
                   if (isTransporterFull != true)
                   {
                       check = true;
                       isFull += "\r\n- <Перевізник> ";
                   }
                   if (isClientFull != true)
                   {
                       check = true;
                       isFull += "\r\n- <Клієнт> ";
                   }


                    if(check)
                    {
                        if (MessageBox.Show(isFull + "\r\nПродовжити без повного заповнення даних?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            isForwarderFull = true;
                            isTransporterFull = true;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Помилка: " + e.Message);
            }
        }
        */
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
                    Person2 = (c.ForwarderContracts.Where(f => f.IsFirst == 1).Count() == 1) ? "<Експедитор> " + c.ForwarderContracts.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder.Name : (c.TransporterId != null) ? "<Перевізник> " + c.Transporter.FullName : "<Клієнт> " + c.Client.Name,
                    ContractDateBegin = c.ContractDataBegin,
                    ContractDateEnd = c.ContractDataEnd,
                    Type = (c.Type == 0) ? "По Україні" : "За кордон",
                    Template = c.TemplateName
                };

                contractShowDataGridView.DataSource = query.ToList();
                contractShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                contractShowDataGridView.Columns[1].HeaderText = "Номер договору";
                contractShowDataGridView.Columns[2].HeaderText = "Експедитор";
                contractShowDataGridView.Columns[3].HeaderText = "Другий учасник договору";
                contractShowDataGridView.Columns[4].HeaderText = "Дата початку";
                contractShowDataGridView.Columns[5].HeaderText = "Дата завершення";
                contractShowDataGridView.Columns[6].HeaderText = "Напрямок";
                contractShowDataGridView.Columns[7].HeaderText = "Використаний шаблон";

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
                    Person2 = (c.ForwarderContracts.Where(f => f.IsFirst == 1).Count() == 1) ? "<Експедитор> " + c.ForwarderContracts.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder.Name : (c.TransporterId != null) ? "<Перевізник> " + c.Transporter.FullName : "<Клієнт> " + c.Client.Name,
                    ContractDateBegin = c.ContractDataBegin,
                    ContractDateEnd = c.ContractDataEnd,
                    Type = (c.Type == 0) ? "По Україні" : "За кордон",
                    Template = c.TemplateName
                };

                contractShowDataGridView.DataSource = query.ToList();
                contractShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                contractShowDataGridView.Columns[1].HeaderText = "Номер договору";
                contractShowDataGridView.Columns[2].HeaderText = "Експедитор";
                contractShowDataGridView.Columns[3].HeaderText = "Другий учасник договору";
                contractShowDataGridView.Columns[4].HeaderText = "Дата початку";
                contractShowDataGridView.Columns[5].HeaderText = "Дата завершення";
                contractShowDataGridView.Columns[6].HeaderText = "Напрямок";
                contractShowDataGridView.Columns[7].HeaderText = "Використаний шаблон";

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
                    MessageBox.Show("Немає жодного договору");
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
                    contractShowTransporterContactDataGridView.Columns[0].HeaderText = "Контактна особа";
                    contractShowTransporterContactDataGridView.Columns[1].HeaderText = "Телефон";
                    contractShowTransporterContactDataGridView.Columns[2].HeaderText = "Факс";
                    contractShowTransporterContactDataGridView.Columns[3].HeaderText = "Email";

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
                    MessageBox.Show("Немає жодного договору");
                }
            }
            contractShowTransporterContactDataGridView.Update();
            contractShowTransporterContactDataGridView.Visible = true;
        }
        /*
        void AddContract()
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                    from d in db.DocumentCounters
                    select d;

                try
                {
                    if (query.Count() == 0)
                    {
                        documentCount = new DocumentCounter
                        {
                            Id = 1,
                            ForeignDocument = 0,
                            LocalDocument = 0
                        };
                        db.DocumentCounters.Add(documentCount);
                        db.SaveChanges();
                        documentCount = null;
                    }
                }
                catch(Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: " + ex.Message);
                }
                documentCount = new DocumentCounter();
                try
                {
                    if (contractLanguage == 1)
                    {
                        documentCount = db.DocumentCounters.Find(1);
                        documentCount.LocalDocument += 1;
                        db.Entry(documentCount).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        documentCount = db.DocumentCounters.Find(1);
                        documentCount.ForeignDocument += 1;
                        db.Entry(documentCount).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: " + ex.Message);
                }

                try
                {
                    contract = new TransporterForwarderContract();
                    contract.ForwarderId = forwarderDocument.Id;
                    contract.TransporterId = transporterDocument.Id;
                    if (contractLanguage == 1)
                    {
                        contract.ContractNumber = documentCount.LocalDocument;
                        contract.Language = 1;
                    }
                    else if (contractLanguage == 2)
                    {
                        contract.ContractNumber = documentCount.ForeignDocument;
                        contract.Language = 2;
                    }

                    contract.ContractDataBegin = contractBeginDateTimePicker.Value.Date;
                    contract.ContractDataEnd = contractBeginDateTimePicker.Value.AddYears(1);
                    contract.PorZ = (forwarderAsComboBox.SelectedIndex == 0) ? true : false;
                    db.TransporterForwarderContracts.Add(contract);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }
        */
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
                        if (MessageBox.Show("Видалити договір " + contract.ContractNumber + @"\" + contract.ContractDataBegin.Value.Year + " ?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                db.Contracts.Attach(contract);
                                db.Contracts.Remove(contract);
                                db.SaveChanges();
                                MessageBox.Show("Договір успішно видалений");
                                contractShowDeleteContractButton.Enabled = false;
                            }
                            catch (Exception ex)
                            {
                                Log.Write(ex);
                                MessageBox.Show("Помилка!" + Environment.NewLine + ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                contractShowOpenDocButton.Enabled = false;
                contractShowDeleteContractButton.Enabled = false;
                MessageBox.Show("Немає жодного договору");
            }
        }
        /*
        void CreateTransporterForwarderContract()
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            if(GetDocumentPath() == "")
            {
                MessageBox.Show("Не вибраний шаблон, спробуйте ще раз");
                return;
            }
            var wordDocument = wordApp.Documents.Open(GetDocumentPath());
            try
            {
                using (var db = new AtlantSovtContext())
                {
                    string title;
                    if (contractLanguage == 1)
                    {
                        title = contract.ContractNumber + @"\" + contract.ContractDataBegin.Value.Year + @"\" + ((contract.PorZ == true) ? "З" : "П");
                    }
                    else
                    {
                        title = contract.ContractNumber + @"\" + contract.ContractDataBegin.Value.Year + @"\P";
                    }
                    var contractDateBegin = contractBeginDateTimePicker.Value;

                    var secondForwarderName = (forwarderDocument.Name == "" || forwarderDocument.Name == null) ? "_______________" : forwarderDocument.Name;
                    var secondForwarderDirector = (forwarderDocument.Director == "" || forwarderDocument.Director == null) ? "_______________" : forwarderDocument.Director;
                    var secondForwarderPhysicalAddress = (forwarderDocument.PhysicalAddress == "" || forwarderDocument.PhysicalAddress == null) ? "_______________" : forwarderDocument.PhysicalAddress;
                    var secondForwarderGeographycalAddress = (forwarderDocument.GeographyAddress == "" || forwarderDocument.GeographyAddress == null) ? "_______________" : forwarderDocument.GeographyAddress;

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

                    var transporterFullName = (transporterDocument.FullName == "" || transporterDocument.FullName == null) ? "_______________" : transporterDocument.FullName;
                    var transporterDirector = (transporterDocument.Director == "" || transporterDocument.Director == null) ? "_______________": transporterDocument.Director; 
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

                    if (forwarderDocument.WorkDocumentId != null)
                    {
                        secondForwarderWorkDocument = db.WorkDocuments.Find(forwarderDocument.WorkDocumentId).Status;
                    }

                    if(forwarderDocument.TaxPayerStatusId != null)
                    {
                        secondForwarderTaxPayerStatus = db.TaxPayerStatus.Find(forwarderDocument.TaxPayerStatusId).Status;
                    }

                    if (transporterDocument.WorkDocumentId != null)
                    {
                        transporterWorkDocument = db.WorkDocuments.Find(transporterDocument.WorkDocumentId).Status;
                    }

                    if (transporterDocument.TaxPayerStatusId != null)
                    {
                        transporterTaxPayerStatus = db.TaxPayerStatus.Find(transporterDocument.TaxPayerStatusId).Status;
                    }

                    if(db.Forwarders.Find(forwarderDocument.Id).ForwarderBankDetail != null)
                    {
                        secondForwarderBankDetailsCertificateSerial = (db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateSerial == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateSerial == null) ? "_______________" : db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateSerial;
                        secondForwarderBankDetailsCertificateNumber = (db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateNamber == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateNamber == null)? "_______________" : db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateNamber;
                        secondForwarderBankDetailsEDRPOU = (db.ForwarderBankDetails.Find(forwarderDocument.Id).EDRPOU == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).EDRPOU == null) ? "_______________" : db.ForwarderBankDetails.Find(forwarderDocument.Id).EDRPOU;
                        secondForwarderBankDetailsAccountNumber = (db.ForwarderBankDetails.Find(forwarderDocument.Id).AccountNumber == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).AccountNumber == null) ? "_______________" : db.ForwarderBankDetails.Find(forwarderDocument.Id).AccountNumber;
                        secondForwarderBankDetailsBankName = (db.ForwarderBankDetails.Find(forwarderDocument.Id).BankName == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).BankName == null) ? "_______________" : db.ForwarderBankDetails.Find(forwarderDocument.Id).BankName;
                        secondForwarderBankDetailsMFO = (db.ForwarderBankDetails.Find(forwarderDocument.Id).MFO == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).MFO == null) ? "_______________" : db.ForwarderBankDetails.Find(forwarderDocument.Id).MFO;
                        secondForwarderBankDetailsIBAN = (db.ForwarderBankDetails.Find(forwarderDocument.Id).IBAN == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).IBAN == null) ? "_______________" : db.ForwarderBankDetails.Find(forwarderDocument.Id).IBAN;
                        secondForwarderBankDetailsIPN = (db.ForwarderBankDetails.Find(forwarderDocument.Id).IPN == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).IPN == null) ? "_______________" : db.ForwarderBankDetails.Find(forwarderDocument.Id).IPN;
                        secondForwarderBankDetailsSWIFT = (db.ForwarderBankDetails.Find(forwarderDocument.Id).SWIFT == "" || db.ForwarderBankDetails.Find(forwarderDocument.Id).SWIFT == null) ? "_______________" :db.ForwarderBankDetails.Find(forwarderDocument.Id).SWIFT;
                    }
                    
                    if (db.Transporters.Find(transporterDocument.Id).TransporterBankDetail != null)
                    {
                        transporterBankDetailsCertificateSerial = (db.TransporterBankDetails.Find(transporterDocument.Id).CertificateSerial == "" || db.TransporterBankDetails.Find(transporterDocument.Id).CertificateSerial == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).CertificateSerial;
                        transporterBankDetailsCertificateNumber = (db.TransporterBankDetails.Find(transporterDocument.Id).CertificateNamber == "" || db.TransporterBankDetails.Find(transporterDocument.Id).CertificateNamber == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).CertificateNamber;
                        transporterBankDetailsEDRPOU = (db.TransporterBankDetails.Find(transporterDocument.Id).EDRPOU == "" || db.TransporterBankDetails.Find(transporterDocument.Id).EDRPOU == null) ? "_______________": db.TransporterBankDetails.Find(transporterDocument.Id).EDRPOU;
                        transporterBankDetailsAccountNumber = (db.TransporterBankDetails.Find(transporterDocument.Id).AccountNumber == "" || db.TransporterBankDetails.Find(transporterDocument.Id).AccountNumber == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).AccountNumber;
                        transporterBankDetailsBankName = (db.TransporterBankDetails.Find(transporterDocument.Id).BankName == "" || db.TransporterBankDetails.Find(transporterDocument.Id).BankName == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).BankName;
                        transporterBankDetailsMFO = (db.TransporterBankDetails.Find(transporterDocument.Id).MFO == "" || db.TransporterBankDetails.Find(transporterDocument.Id).MFO == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).MFO;
                        transporterBankDetailsIBAN = (db.TransporterBankDetails.Find(transporterDocument.Id).IBAN == "" || db.TransporterBankDetails.Find(transporterDocument.Id).IBAN == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).IBAN;
                        transporterBankDetailsIPN = (db.TransporterBankDetails.Find(transporterDocument.Id).IPN == "" || db.TransporterBankDetails.Find(transporterDocument.Id).IPN == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).IPN;
                        transporterBankDetailsSWIFT = (db.TransporterBankDetails.Find(transporterDocument.Id).SWIFT == "" || db.TransporterBankDetails.Find(transporterDocument.Id).SWIFT == null) ? "_______________" : db.TransporterBankDetails.Find(transporterDocument.Id).SWIFT;
                    }

                    if (contractLanguage == 1)
                    {
                        ReplaseWordStub("{ContractDateBegin}", contractDateBegin.ToLongDateString(), wordDocument);
                    }
                    else if (contractLanguage == 2)
                    {
                        ReplaseWordStub("{ContractDateBegin}", contractDateBegin.ToString("D",new System.Globalization.CultureInfo("ru-RU")), wordDocument);
                        ReplaseWordStub("{engContractDateBegin}", contractDateBegin.ToString("D",new System.Globalization.CultureInfo("en-US")), wordDocument);
                    }

                    ReplaseWordStub("{ContractNumber}", title, wordDocument);

                    ReplaseWordStub("{ForwarderName}", secondForwarderName, wordDocument);
                    ReplaseWordStub("{ForwarderDirector}", secondForwarderDirector, wordDocument);
                    ReplaseWordStub("{ForwarderWorkDocument}", secondForwarderWorkDocument, wordDocument);
                    ReplaseWordStub("{ForwarderTaxPayerStatus}", secondForwarderTaxPayerStatus, wordDocument);
                    ReplaseWordStub("{ForwarderPhysicalAddress}", secondForwarderPhysicalAddress, wordDocument);
                    ReplaseWordStub("{ForwarderGeographycalAddress}", secondForwarderGeographycalAddress, wordDocument);
                    ReplaseWordStub("{ForwarderCerificateSerial}", secondForwarderBankDetailsCertificateSerial, wordDocument);
                    ReplaseWordStub("{ForwarderCerificateNumber}", secondForwarderBankDetailsCertificateNumber, wordDocument);
                    ReplaseWordStub("{ForwarderEDRPOU}", secondForwarderBankDetailsEDRPOU, wordDocument);
                    ReplaseWordStub("{ForwarderAccountNumber}", secondForwarderBankDetailsAccountNumber, wordDocument);
                    ReplaseWordStub("{ForwarderBankName}", secondForwarderBankDetailsBankName, wordDocument);
                    ReplaseWordStub("{ForwarderMFO}", secondForwarderBankDetailsMFO, wordDocument);

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


                    if (forwarderStamp.Stamp != null)
                    {
                        AddStamp(wordDocument, UploadForwarderStapm(forwarderDocument), "{Stamp1}");
                        if (contract.Language == 2 || contract.Language == 3)
                        {
                            AddStamp(wordDocument, UploadForwarderStapm(forwarderDocument), "{Stamp2}");
                        }
                        Directory.Delete((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp\").Replace("\\bin\\Release", ""), true);
                        Directory.CreateDirectory((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp").Replace("\\bin\\Release", ""));
                    }

                    if (isForwarderFull && isTransporterFull)
                    {
                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            title = title.Replace(@"\", "_");
                            wordDocument.SaveAs(folderBrowserDialog.SelectedPath + "\\" + title + ".docx");
                            wordApp.Visible = true;
                        }
                        else
                        {
                            wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                        }
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
                MessageBox.Show("Помилка: " + ex.Message);
                wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
            }
        }

        void OpenWordDoc()
        {
            var wordApp = new Word.Application();
            Document wordDocument = null;
            try
            {
                var ClikedId = Convert.ToInt32(contractShowDataGridView.CurrentRow.Cells[0].Value);

                using (var db = new AtlantSovtContext())
                {

                    contract = db.TransporterForwarderContracts.Find(ClikedId);
                    wordApp.Visible = false;
                    wordDocument = wordApp.Documents.Open((System.AppDomain.CurrentDomain.BaseDirectory + ((contract.Language == 1) ? @"Resources\ukrDocumentationTransporterForwarder.docx" : (contract.Language == 2) ? @"Resources\engDocumentationTransporterForwarder.docx" : @"Resources\gerDocumentationTransporterForwarder.docx")).Replace("\\bin\\Release", ""));

                    if (contract != null)
                    {
                        string title;
                        if (contract.Language == 1)
                        {
                            title = contract.ContractNumber + @"\" + contract.ContractDataBegin.Value.Year + @"\" + ((contract.PorZ == true) ? "З" : "П");
                        }
                        else
                        {
                            title = contract.ContractNumber + @"\" + contract.ContractDataBegin.Value.Year + @"\P";
                        }
                        var contractDateBegin = contract.ContractDataBegin;

                        var secondForwarderName = (contract.Forwarder.Name == "" || contract.Forwarder.Name == null) ? "_______________" : contract.Forwarder.Name;
                        var secondForwarderDirector = (contract.Forwarder.Director == "" || contract.Forwarder.Director == null) ? "_______________" : contract.Forwarder.Director;
                        var secondForwarderPhysicalAddress = (contract.Forwarder.PhysicalAddress == "" || contract.Forwarder.PhysicalAddress == null) ? "_______________" : contract.Forwarder.PhysicalAddress;
                        var secondForwarderGeographycalAddress = (contract.Forwarder.GeographyAddress == "" || contract.Forwarder.GeographyAddress == null) ? "_______________" : contract.Forwarder.GeographyAddress;

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

                        var transporterFullName = (contract.Transporter.FullName == "" || contract.Transporter.FullName == null) ? "_______________" : contract.Transporter.FullName;
                        var transporterDirector = (contract.Transporter.Director == "" || contract.Transporter.Director == null) ? "_______________" : contract.Transporter.Director;
                        var transporterPhysicalAddress = (contract.Transporter.PhysicalAddress == "" || contract.Transporter.PhysicalAddress == null) ? "_______________" : contract.Transporter.PhysicalAddress;
                        var transporterGeographycalAddress = (contract.Transporter.GeographyAddress == "" || contract.Transporter.GeographyAddress == null) ? "_______________" : contract.Transporter.GeographyAddress;

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

                        if (contract.Forwarder.WorkDocumentId != null)
                        {
                            secondForwarderWorkDocument = db.WorkDocuments.Find(contract.Forwarder.WorkDocumentId).Status;
                        }

                        if (contract.Forwarder.TaxPayerStatusId != null)
                        {
                            secondForwarderTaxPayerStatus = db.TaxPayerStatus.Find(contract.Forwarder.TaxPayerStatusId).Status;
                        }

                        if (contract.Transporter.WorkDocumentId != null)
                        {
                            transporterWorkDocument = db.WorkDocuments.Find(contract.Transporter.WorkDocumentId).Status;
                        }

                        if (contract.Transporter.TaxPayerStatusId != null)
                        {
                            transporterTaxPayerStatus = db.TaxPayerStatus.Find(contract.Transporter.TaxPayerStatusId).Status;
                        }

                        if (db.Forwarders.Find(contract.Forwarder.Id).ForwarderBankDetail != null)
                        {
                            secondForwarderBankDetailsCertificateSerial = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).CertificateSerial == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).CertificateSerial == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).CertificateSerial;
                            secondForwarderBankDetailsCertificateNumber = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).CertificateNamber == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).CertificateNamber == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).CertificateNamber;
                            secondForwarderBankDetailsEDRPOU = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).EDRPOU == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).EDRPOU == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).EDRPOU;
                            secondForwarderBankDetailsAccountNumber = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).AccountNumber == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).AccountNumber == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).AccountNumber;
                            secondForwarderBankDetailsBankName = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).BankName == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).BankName == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).BankName;
                            secondForwarderBankDetailsMFO = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).MFO == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).MFO == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).MFO;
                            secondForwarderBankDetailsIBAN = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).IBAN == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).IBAN == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).IBAN;
                            secondForwarderBankDetailsIPN = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).IPN == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).IPN == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).IPN;
                            secondForwarderBankDetailsSWIFT = (db.ForwarderBankDetails.Find(contract.Forwarder.Id).SWIFT == "" || db.ForwarderBankDetails.Find(contract.Forwarder.Id).SWIFT == null) ? "_______________" : db.ForwarderBankDetails.Find(contract.Forwarder.Id).SWIFT;
                        }

                        if (db.Transporters.Find(contract.Transporter.Id).TransporterBankDetail != null)
                        {
                            transporterBankDetailsCertificateSerial = (db.TransporterBankDetails.Find(contract.Transporter.Id).CertificateSerial == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).CertificateSerial == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).CertificateSerial;
                            transporterBankDetailsCertificateNumber = (db.TransporterBankDetails.Find(contract.Transporter.Id).CertificateNamber == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).CertificateNamber == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).CertificateNamber;
                            transporterBankDetailsEDRPOU = (db.TransporterBankDetails.Find(contract.Transporter.Id).EDRPOU == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).EDRPOU == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).EDRPOU;
                            transporterBankDetailsAccountNumber = (db.TransporterBankDetails.Find(contract.Transporter.Id).AccountNumber == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).AccountNumber == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).AccountNumber;
                            transporterBankDetailsBankName = (db.TransporterBankDetails.Find(contract.Transporter.Id).BankName == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).BankName == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).BankName;
                            transporterBankDetailsMFO = (db.TransporterBankDetails.Find(contract.Transporter.Id).MFO == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).MFO == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).MFO;
                            transporterBankDetailsIBAN = (db.TransporterBankDetails.Find(contract.Transporter.Id).IBAN == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).IBAN == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).IBAN;
                            transporterBankDetailsIPN = (db.TransporterBankDetails.Find(contract.Transporter.Id).IPN == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).IPN == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).IPN;
                            transporterBankDetailsSWIFT = (db.TransporterBankDetails.Find(contract.Transporter.Id).SWIFT == "" || db.TransporterBankDetails.Find(contract.Transporter.Id).SWIFT == null) ? "_______________" : db.TransporterBankDetails.Find(contract.Transporter.Id).SWIFT;
                        }

                        if (contract.Language == 1)
                        {
                            ReplaseWordStub("{ContractDateBegin}", contract.ContractDataBegin.Value.ToLongDateString(), wordDocument);
                        }
                        else if (contract.Language == 2)
                        {
                            ReplaseWordStub("{ContractDateBegin}", contract.ContractDataBegin.Value.ToString("D", new System.Globalization.CultureInfo("ru-RU")), wordDocument);
                            ReplaseWordStub("{engContractDateBegin}", contract.ContractDataBegin.Value.ToString("D", new System.Globalization.CultureInfo("en-US")), wordDocument);
                        }
                        else if (contract.Language == 3)
                        {
                            ReplaseWordStub("{ContractDateBegin}", contract.ContractDataBegin.Value.ToString("D", new System.Globalization.CultureInfo("ru-RU")), wordDocument);
                            ReplaseWordStub("{gerContractDateBegin}", contract.ContractDataBegin.Value.ToString("D", new System.Globalization.CultureInfo("de-DE")), wordDocument);
                        }

                        ReplaseWordStub("{ContractNumber}", title, wordDocument);

                        ReplaseWordStub("{ForwarderName}", secondForwarderName, wordDocument);
                        ReplaseWordStub("{ForwarderDirector}", secondForwarderDirector, wordDocument);
                        ReplaseWordStub("{ForwarderWorkDocument}", secondForwarderWorkDocument, wordDocument);
                        ReplaseWordStub("{ForwarderTaxPayerStatus}", secondForwarderTaxPayerStatus, wordDocument);
                        ReplaseWordStub("{ForwarderPhysicalAddress}", secondForwarderPhysicalAddress, wordDocument);
                        ReplaseWordStub("{ForwarderGeographycalAddress}", secondForwarderGeographycalAddress, wordDocument);
                        ReplaseWordStub("{ForwarderCerificateSerial}", secondForwarderBankDetailsCertificateSerial, wordDocument);
                        ReplaseWordStub("{ForwarderCerificateNumber}", secondForwarderBankDetailsCertificateNumber, wordDocument);
                        ReplaseWordStub("{ForwarderEDRPOU}", secondForwarderBankDetailsEDRPOU, wordDocument);
                        ReplaseWordStub("{ForwarderAccountNumber}", secondForwarderBankDetailsAccountNumber, wordDocument);
                        ReplaseWordStub("{ForwarderBankName}", secondForwarderBankDetailsBankName, wordDocument);
                        ReplaseWordStub("{ForwarderMFO}", secondForwarderBankDetailsMFO, wordDocument);

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

                        if (contract.Forwarder.ForwarderStamp.Stamp != null)
                        {
                            AddStamp(wordDocument, UploadForwarderStapm(contract.Forwarder), "{Stamp1}");
                            if (contract.Language == 2 || contract.Language == 3)
                            {
                                AddStamp(wordDocument, UploadForwarderStapm(contract.Forwarder), "{Stamp2}");
                            }
                            Directory.Delete((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp\").Replace("\\bin\\Release", ""), true);
                            Directory.CreateDirectory((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp").Replace("\\bin\\Release", ""));
                        }

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            title = title.Replace(@"\", "_");
                            wordDocument.SaveAs(folderBrowserDialog.SelectedPath + "\\" + title + ".docx");
                            wordApp.Visible = true;
                        }
                        else
                        {
                            wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                        }
                    }
                }
            }
            catch (NullReferenceException nullClickedId)
            {
                Log.Write(nullClickedId);
                contractShowOpenDocButton.Enabled = false;
                contractShowDeleteContractButton.Enabled = false;
                contractShowTransporterContactDataGridView.Visible = false;
                MessageBox.Show("Немає жодного договору");
            }
            catch (System.Runtime.InteropServices.COMException wordException)
            {
                Log.Write(wordException);
                wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                MessageBox.Show("Помилка, спробуйте ще раз");
            }
        }
        */
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
            range.Find.Execute(FindText: bookmark, Replace: Word.WdReplace.wdReplaceNone);
            Word.InlineShape inLineShape = range.InlineShapes.AddPicture(path);
            Word.Shape shape = inLineShape.ConvertToShape();
            shape.WrapFormat.Type = WdWrapType.wdWrapBehind;
            shape.Left = (float)Word.WdShapePosition.wdShapeLeft;
            shape.Top = (float)Word.WdShapePosition.wdShapeTop;
            ReplaseWordStub(bookmark, "", wordDocument);
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

            var files = Directory.GetFiles((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Contracts\").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", "")).Where(s => s.EndsWith(".docx") || s.EndsWith(".doc") || s.EndsWith(".rtf"));

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                contractFilecheckedListBox.Items.Add(fileName);
            }
        }

        string GetDocumentPath()
        {
            string filePath = "";
            try
            {
                if (contractFilecheckedListBox.CheckedItems.Count > 0)
                {
                    filePath = (System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Contracts\" + contractFilecheckedListBox.SelectedItem.ToString()).Replace("\\bin\\Release", "").Replace("\\bin\\Debug", "");
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Помилка: не правильний шлях шаблону");
            }
            return filePath;
        }

    }
}
