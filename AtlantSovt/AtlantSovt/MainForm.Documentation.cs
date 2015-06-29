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

namespace AtlantSovt
{
    partial class MainForm
    {
        Transporter transporterDocument;
        Forwarder forwarderDocument;
        DocumentCounter documentCount;
        TransporterForwarderContract contract;

        bool isLocal;
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
                        MessageBox.Show("Заповніть спочатку всі дані перевізника");
                    }
                    else
                    {
                        isTransporterFull = true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Помилка: " + e.Message);
            }
        }

        void AddDocument()
        {
            isLocal = true;
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
                    if (isLocal)
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
                    if (isLocal)
                    {
                        contract.ContractNumber = documentCount.LocalDocument;
                    }
                    else
                    {
                        contract.ContractNumber = documentCount.ForeignDocument;
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
            var wordDocument = wordApp.Documents.Open((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\ukrDocumentationTransporterForwarder.docx").Replace("\\bin\\Debug", ""));
            try
            {
                using (var db = new AtlantSovtContext())
                {
                    string title = contract.ContractNumber + @"\" + contract.ContractDataBegin.Value.Year + @"\" + ((contract.PorZ == true) ? "З": "П");

                    var contractDateBegin = contractBeginDateTimePicker.Value.ToLongDateString();

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

                    ReplaseWordStub("{ContractDateBegin}", contractDateBegin, wordDocument);
                    ReplaseWordStub("{ContractNumber}", title, wordDocument);


                    if (secondForwarderName == "" || secondForwarderDirector == "" || 
                        secondForwarderWorkDocument == "" || secondForwarderTaxPayerStatus == "" ||
                        secondForwarderBankDetailsBankName == "" || secondForwarderBankDetailsAccountNumber == "" || secondForwarderBankDetailsCertificateNumber == "" || secondForwarderBankDetailsCertificateSerial == "" ||
                        secondForwarderBankDetailsEDRPOU == "" || secondForwarderBankDetailsIBAN == "" ||
                        secondForwarderBankDetailsIPN == "" || secondForwarderBankDetailsMFO == "" || secondForwarderBankDetailsSWIFT == "")
                    {
                        MessageBox.Show("Заповніть спочатку всі дані експедитора");
                    }
                    else
                    {
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
                    }

                    if (transporterFullName == "" || transporterDirector == "" ||    transporterWorkDocument == "" || 
                        transporterTaxPayerStatus == "" || transporterBankDetailsBankName == "" || transporterBankDetailsAccountNumber == "" ||
                        transporterBankDetailsCertificateNumber == "" || transporterBankDetailsCertificateSerial == "" || transporterBankDetailsEDRPOU == "" || transporterBankDetailsIBAN == "" ||
                        transporterBankDetailsIPN == "" || transporterBankDetailsMFO == "" || transporterBankDetailsSWIFT == "")
                    {
                        MessageBox.Show("Заповніть спочатку всі дані перевізника");
                    }
                    else
                    {
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

        void ReplaseWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Replace: Word.WdReplace.wdReplaceAll);
        }
    }
}
