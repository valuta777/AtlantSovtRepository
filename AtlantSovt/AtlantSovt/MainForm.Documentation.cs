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

namespace AtlantSovt
{
    partial class MainForm
    {
        Transporter transporterDocument;
        Forwarder forwarderDocument;
        DocumentCounter documentCount;
        TransporterForwarderContract contract;

        int contractLanguage;
        bool isForwarderFull;
        bool isTransporterFull;

        void SplitTransporterFirstPersonComboBoxDocument()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = firstPersonNameComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterDocument = db.Transporters.Find(id);
            }
        }

        void SplitForwarderSecondPersonComboBoxDocument()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = secondPersonNameComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarderDocument = db.Forwarders.Find(id);
            }
        }

        void LoadTransporterFirstPersonDiapasonCombobox()
        {
            firstPersonDiapasonComboBox.Items.Clear();
            firstPersonNameComboBox.Items.Clear();
            firstPersonNameComboBox.Text = "";
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
                        firstPersonDiapasonComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    firstPersonDiapasonComboBox.DroppedDown = true;
                    firstPersonNameComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void LoadTransporterFirstPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (firstPersonDiapasonComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = firstPersonDiapasonComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        firstPersonNameComboBox.Items.Add(item.FullName + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadForwarderSecondPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    secondPersonNameComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }

        void IsForwarderAndTransporterFull()
        {
            isForwarderFull = false;
            isTransporterFull = false;
            bool check = false;
            try
            {
                using (var db = new AtlantSovtContext())
                {
                    var secondForwarderName = forwarderDocument.Name;
                    var secondForwarderDirector = forwarderDocument.Director;
                    var secondForwarderPhysicalAddress = forwarderDocument.PhysicalAddress;
                    var secondForwarderGeographycalAddress = forwarderDocument.GeographyAddress;

                    string secondForwarderWorkDocument = "";
                    string secondForwarderTaxPayerStatus = "";
                    string secondForwarderBankDetailsCertificateSerial = "";
                    string secondForwarderBankDetailsCertificateNumber = "";
                    string secondForwarderBankDetailsEDRPOU = "";
                    string secondForwarderBankDetailsAccountNumber = "";
                    string secondForwarderBankDetailsBankName = "";
                    string secondForwarderBankDetailsMFO = "";
                    string secondForwarderBankDetailsIBAN = "";
                    string secondForwarderBankDetailsIPN = "";
                    string secondForwarderBankDetailsSWIFT = "";

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

                    if (forwarderDocument.WorkDocumentId != null)
                    {
                        secondForwarderWorkDocument = db.WorkDocuments.Find(forwarderDocument.WorkDocumentId).Status;
                    }

                    if (forwarderDocument.TaxPayerStatusId != null)
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

                    if (db.Forwarders.Find(forwarderDocument.Id).ForwarderBankDetail != null)
                    {
                        secondForwarderBankDetailsCertificateSerial = db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateSerial;
                        secondForwarderBankDetailsCertificateNumber = db.ForwarderBankDetails.Find(forwarderDocument.Id).CertificateNamber;
                        secondForwarderBankDetailsEDRPOU = db.ForwarderBankDetails.Find(forwarderDocument.Id).EDRPOU;
                        secondForwarderBankDetailsAccountNumber = db.ForwarderBankDetails.Find(forwarderDocument.Id).AccountNumber;
                        secondForwarderBankDetailsBankName = db.ForwarderBankDetails.Find(forwarderDocument.Id).BankName;
                        secondForwarderBankDetailsMFO = db.ForwarderBankDetails.Find(forwarderDocument.Id).MFO;
                        secondForwarderBankDetailsIBAN = db.ForwarderBankDetails.Find(forwarderDocument.Id).IBAN;
                        secondForwarderBankDetailsIPN = db.ForwarderBankDetails.Find(forwarderDocument.Id).IPN;
                        secondForwarderBankDetailsSWIFT = db.ForwarderBankDetails.Find(forwarderDocument.Id).SWIFT;
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

                    if (secondForwarderName == "" || secondForwarderDirector == "" ||
                        secondForwarderWorkDocument == "" || secondForwarderTaxPayerStatus == "" ||
                        secondForwarderBankDetailsBankName == "" || secondForwarderBankDetailsAccountNumber == "" || secondForwarderBankDetailsCertificateNumber == "" || secondForwarderBankDetailsCertificateSerial == "" ||
                        secondForwarderBankDetailsEDRPOU == "" || secondForwarderBankDetailsIBAN == "" ||
                        secondForwarderBankDetailsIPN == "" || secondForwarderBankDetailsMFO == "" || secondForwarderBankDetailsSWIFT == "")
                    {
                        check = true;
                        MessageBox.Show("Заповніть спочатку всі дані експедитора");
                    }
                    else
                    {
                        isForwarderFull = true;
                    }

                    if (transporterFullName == "" || transporterDirector == "" || transporterWorkDocument == "" ||
                        transporterTaxPayerStatus == "" || transporterBankDetailsBankName == "" || transporterBankDetailsAccountNumber == "" ||
                        transporterBankDetailsCertificateNumber == "" || transporterBankDetailsCertificateSerial == "" || transporterBankDetailsEDRPOU == "" || transporterBankDetailsIBAN == "" ||
                        transporterBankDetailsIPN == "" || transporterBankDetailsMFO == "" || transporterBankDetailsSWIFT == "")
                    {
                        check = true;
                        MessageBox.Show("Заповніть спочатку всі дані перевізника");
                    }
                    else
                    {
                        isTransporterFull = true;
                    }

                    if(check)
                    {
                        if (MessageBox.Show("Продовжити без повного заповнення даних?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        void ShowContract()
        {
            contractShowTransporterContactDataGridView.Visible = false;

            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.TransporterForwarderContracts
                orderby c.Id
                select
                new
                {
                    Id = c.Id,
                    ContractNumber = c.ContractNumber + @"\" + c.ContractDataBegin.Value.Year +  @"\" + ((c.Language == 1) ? ((c.PorZ == true) ? "З" : "П") : "P"),
                    Forwarder = c.Forwarder.Name,
                    Transporter = c.Transporter.FullName,
                    ContractDateBegin = c.ContractDataBegin,
                    ContractDateEnd = c.ContractDataEnd,
                    Language = (c.Language == 1) ? "Українська" : (c.Language == 2) ? "Англійська/Російська" : "Німецька/Російська",
                    PorZ = (c.PorZ == true) ? "Замовника" : "Перевізника"
                };

                contractShowDataGridView.DataSource = query.ToList();
                contractShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                contractShowDataGridView.Columns[1].HeaderText = "Номер договору";
                contractShowDataGridView.Columns[2].HeaderText = "Експедитор";
                contractShowDataGridView.Columns[3].HeaderText = "Перевізник";
                contractShowDataGridView.Columns[4].HeaderText = "Дата початку";
                contractShowDataGridView.Columns[5].HeaderText = "Дата завершення";
                contractShowDataGridView.Columns[6].HeaderText = "Мова";
                contractShowDataGridView.Columns[7].HeaderText = "Виступає у якості";

            } contractShowDataGridView.Update();

        }

        void ShowContractSearch()
        {
            contractShowTransporterContactDataGridView.Visible = false;

            var text = contractShowSearchTextBox.Text;
            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.TransporterForwarderContracts
                where (c.Forwarder.Name.Contains(text) || c.Transporter.FullName.Contains(text) || c.Transporter.ShortName.Contains(text) || c.Transporter.TransporterContacts.Any(con => con.ContactPerson.Contains(text)) || c.Transporter.TransporterContacts.Any(con => con.Email.Contains(text)))
                select
                new
                {
                    Id = c.Id,
                    Name = c.ContractNumber,
                    Forwarder = c.Forwarder.Name,
                    Transporter = c.Transporter.FullName,
                    ContractDateBegin = c.ContractDataBegin,
                    ContractDateEnd = c.ContractDataEnd,
                    Language = (c.Language == 1) ? "Українська" : (c.Language == 2) ? "Англійська/Російська" : "Німецька/Російська",
                    PorZ = (c.PorZ == true) ? "Замовника" : "Перевізника"
                };

                contractShowDataGridView.DataSource = query.ToList();
                contractShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                contractShowDataGridView.Columns[1].HeaderText = "Номер договору";
                contractShowDataGridView.Columns[2].HeaderText = "Експедитор";
                contractShowDataGridView.Columns[3].HeaderText = "Перевізник";
                contractShowDataGridView.Columns[4].HeaderText = "Дата початку";
                contractShowDataGridView.Columns[5].HeaderText = "Дата завершення";
                contractShowDataGridView.Columns[6].HeaderText = "Мова";
                contractShowDataGridView.Columns[7].HeaderText = "Виступає у якості";

            } contractShowDataGridView.Update();

        }

        void ShowContractInfo()
        {
             using (var db = new AtlantSovtContext())
            {
                try
                {
                    var ClikedId = Convert.ToInt32(contractShowDataGridView.CurrentRow.Cells[0].Value);
                    var query =
                    from con in db.TransporterContacts
                    where con.TransporterId ==
                    (
                        from c in db.TransporterForwarderContracts
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
                }
                catch (Exception ex)
                {
                    contractShowOpenDocButton.Enabled = false;
                    contractShowDeleteContractButton.Enabled = false;
                    MessageBox.Show("Немає жодного договору");
                }
            }
            contractShowTransporterContactDataGridView.Update();
            contractShowTransporterContactDataGridView.Visible = true;
        }

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
                catch(Exception e)
                {
                    MessageBox.Show("Помилка: " + e.Message);
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
                catch (Exception e)
                {
                    MessageBox.Show("Помилка: " + e.Message);
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
                    else if (contractLanguage == 3)
                    {
                        contract.ContractNumber = documentCount.ForeignDocument;
                        contract.Language = 3;
                    }

                    contract.ContractDataBegin = contractBeginDateTimePicker.Value.Date;
                    contract.ContractDataEnd = contractBeginDateTimePicker.Value.AddYears(1);
                    contract.PorZ = (forwarderAsComboBox.SelectedIndex == 0) ? true : false;
                    db.TransporterForwarderContracts.Add(contract);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Помилка: " + e.Message);
                }
            }
        }

        void CreateTransporterForwarderContract()
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            var wordDocument = wordApp.Documents.Open((System.AppDomain.CurrentDomain.BaseDirectory + ((contractLanguage == 1) ? @"Resources\ukrDocumentationTransporterForwarder.docx" : (contractLanguage == 2) ? @"Resources\engDocumentationTransporterForwarder.docx" : @"Resources\gerDocumentationTransporterForwarder.docx")).Replace("\\bin\\Debug", ""));
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
                    else if (contractLanguage == 3)
                    {
                        ReplaseWordStub("{ContractDateBegin}", contractDateBegin.ToString("D", new System.Globalization.CultureInfo("ru-RU")), wordDocument);
                        ReplaseWordStub("{gerContractDateBegin}", contractDateBegin.ToString("D", new System.Globalization.CultureInfo("de-DE")), wordDocument);
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
            catch(Exception e)
            {
                MessageBox.Show("Помилка: " + e.Message);
                wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
            }
        }

        void DeleteContract()
        {
            try
            {
                var ClikedId = Convert.ToInt32(contractShowDataGridView.CurrentRow.Cells[0].Value);

            using (var db = new AtlantSovtContext())
            {
                contract = db.TransporterForwarderContracts.Find(ClikedId);

                if (contract != null)
                {
                    if (MessageBox.Show("Видалити договір " + contract.ContractNumber + @"\" + contract.ContractDataBegin.Value.Year + @"\" + ((contract.Language == 1) ? ((contract.PorZ == true) ? "З" : "П") : "P") + "?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.TransporterForwarderContracts.Attach(contract);
                            db.TransporterForwarderContracts.Remove(contract);
                            db.SaveChanges();
                            MessageBox.Show("Договір успішно видалений");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Помилка!" + Environment.NewLine + e);
                        }
                    }
                }
            }
            }
            catch (Exception e)
            {
                contractShowOpenDocButton.Enabled = false;
                contractShowDeleteContractButton.Enabled = false;
                MessageBox.Show("Немає жодного договору");
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
                    wordDocument = wordApp.Documents.Open((System.AppDomain.CurrentDomain.BaseDirectory + ((contract.Language == 1) ? @"Resources\ukrDocumentationTransporterForwarder.docx" : (contract.Language == 2) ? @"Resources\engDocumentationTransporterForwarder.docx" : @"Resources\gerDocumentationTransporterForwarder.docx")).Replace("\\bin\\Debug", ""));


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
                contractShowOpenDocButton.Enabled = false;
                contractShowDeleteContractButton.Enabled = false;
                contractShowTransporterContactDataGridView.Visible = false;
                MessageBox.Show("Немає жодного договору");
            }
            catch(System.Runtime.InteropServices.COMException wordException)
            {
                wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                MessageBox.Show("Помилка, спробуйте ще раз");
            }

        }

        void ReplaseWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Replace: Word.WdReplace.wdReplaceAll);
            
        }
    }
}
