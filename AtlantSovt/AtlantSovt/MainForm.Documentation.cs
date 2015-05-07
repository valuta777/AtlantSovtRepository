using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace AtlantSovt
{
    partial class MainForm
    {
        readonly string TemplateFileName = @"C:\Users\v2\Desktop\doc.docx";

        Client clientFirstPersonDocument;
        Transporter transporterFirstPersonDocument;
        Forwarder forwarderFirstPersonDocument;
        Forwarder forwarderSecondPersonDocument;

        void SplitClientFirstPersonComboBoxDocument()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = firstPersonNameComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                clientFirstPersonDocument = db.Clients.Find(id);
            }
        }

        void SplitTransporterFirstPersonComboBoxDocument()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = firstPersonNameComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterFirstPersonDocument = db.Transporters.Find(id);
            }
        }

        void SplitForwarderFirstPersonComboBoxDocument()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = firstPersonNameComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarderFirstPersonDocument = db.Forwarders.Find(id);
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
                forwarderSecondPersonDocument = db.Forwarders.Find(id);
            }
        }

        void CreateTransporterForwarderContract()
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            
            try
            {
                using (var db = new AtlantSovtContext())
                {
                    var wordDocument = wordApp.Documents.Open(TemplateFileName);

                    var secondForwarderName = forwarderSecondPersonDocument.Name;
                    var secondForwarderDirector = forwarderSecondPersonDocument.Director;
                    string secondForwarderWorkDocument = "";                    
                    string secondForwarderTaxPayerStatus = "";    
                    var secondForwarderPhysicalAddress = "";
                    var secondForwarderGeographycalAddress = "";
                    string secondForwarderBankDetailsCertificateNumber = "";
                    string secondForwarderBankDetailsEDRPOU = "";
                    string secondForwarderBankDetailsAccountNumber = "";
                    string secondForwarderBankDetailsBankName = "";
                    string secondForwarderBankDetailsMFO = "";
                    string secondForwarderBankDetailsIBAN = "";
                    string secondForwarderBankDetailsIPN = "";
                    string secondForwarderBankDetailsSWIFT = "";

                    if (forwarderSecondPersonDocument.WorkDocumentId != null)
                    {
                        secondForwarderWorkDocument = db.WorkDocuments.Find(forwarderSecondPersonDocument.WorkDocumentId).Status;
                    }
                    if(forwarderSecondPersonDocument.TaxPayerStatusId != null)
                    {
                        secondForwarderTaxPayerStatus = db.TaxPayerStatus.Find(forwarderSecondPersonDocument.TaxPayerStatusId).Status;
                    }
                    if(db.Forwarders.Find(forwarderSecondPersonDocument.Id).ForwarderBankDetail != null)
                    {
                        secondForwarderBankDetailsCertificateNumber = db.ForwarderBankDetails.Find(forwarderSecondPersonDocument.Id).CertificateNamber;
                        secondForwarderBankDetailsEDRPOU = db.ForwarderBankDetails.Find(forwarderSecondPersonDocument.Id).EDRPOU;
                        secondForwarderBankDetailsAccountNumber = db.ForwarderBankDetails.Find(forwarderSecondPersonDocument.Id).AccountNumber;
                        secondForwarderBankDetailsBankName = db.ForwarderBankDetails.Find(forwarderSecondPersonDocument.Id).BankName;
                        secondForwarderBankDetailsMFO = db.ForwarderBankDetails.Find(forwarderSecondPersonDocument.Id).MFO;
                        secondForwarderBankDetailsIBAN = db.ForwarderBankDetails.Find(forwarderSecondPersonDocument.Id).IBAN;
                        secondForwarderBankDetailsIPN = db.ForwarderBankDetails.Find(forwarderSecondPersonDocument.Id).IPN;
                        secondForwarderBankDetailsSWIFT = db.ForwarderBankDetails.Find(forwarderSecondPersonDocument.Id).SWIFT;
                    }


                    var transporterFullName = transporterFirstPersonDocument.FullName;
                    var transporterDirector = transporterFirstPersonDocument.Director;

                    string transporterWorkDocument = "";
                    string transporterTaxPayerStatus = "";

                    string transporterBankDetailsCertificateNumber = "";
                    string transporterBankDetailsEDRPOU = "";
                    string transporterBankDetailsAccountNumber = "";
                    string transporterBankDetailsBankName = "";
                    string transporterBankDetailsMFO = "";
                    string transporterBankDetailsIBAN = "";
                    string transporterBankDetailsIPN = "";
                    string transporterBankDetailsSWIFT = "";

                    if (transporterFirstPersonDocument.WorkDocumentId != null) 
                    {
                        transporterWorkDocument = db.WorkDocuments.Find(transporterFirstPersonDocument.WorkDocumentId).Status;
                    }
                    if (transporterFirstPersonDocument.TaxPayerStatusId != null)
                    {
                        transporterTaxPayerStatus = db.TaxPayerStatus.Find(transporterFirstPersonDocument.TaxPayerStatusId).Status;
                    }
                    
                    var transporterPhysicalAddress = transporterFirstPersonDocument.PhysicalAddress;
                    var transporterGeographycalAddress = transporterFirstPersonDocument.GeographyAddress;

                    if (db.Forwarders.Find(forwarderSecondPersonDocument.Id).ForwarderBankDetail != null)
                    {
                        transporterBankDetailsCertificateNumber = db.TransporterBankDetails.Find(transporterFirstPersonDocument.Id).CertificateNamber;
                        transporterBankDetailsEDRPOU = db.TransporterBankDetails.Find(transporterFirstPersonDocument.Id).EDRPOU;
                        transporterBankDetailsAccountNumber = db.TransporterBankDetails.Find(transporterFirstPersonDocument.Id).AccountNumber;
                        transporterBankDetailsBankName = db.TransporterBankDetails.Find(transporterFirstPersonDocument.Id).BankName;
                        transporterBankDetailsMFO = db.TransporterBankDetails.Find(transporterFirstPersonDocument.Id).MFO;
                        transporterBankDetailsIBAN = db.TransporterBankDetails.Find(transporterFirstPersonDocument.Id).IBAN;
                        transporterBankDetailsIPN = db.TransporterBankDetails.Find(transporterFirstPersonDocument.Id).IPN;
                        transporterBankDetailsSWIFT = db.TransporterBankDetails.Find(transporterFirstPersonDocument.Id).SWIFT;
                    }

                    var contractDateBegin = contractBeginDateTimePicker.Value.ToLongDateString();

                    ReplaseWordStub("{ContractDateBegin}", contractDateBegin, wordDocument);

                    if (secondForwarderName == "" || secondForwarderDirector == "" || 
                        secondForwarderWorkDocument == "" || secondForwarderTaxPayerStatus == "" ||
                        secondForwarderBankDetailsBankName == "" || secondForwarderBankDetailsAccountNumber == "" || secondForwarderBankDetailsCertificateNumber == "" || 
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
                        ReplaseWordStub("{ForwarderCerificateNumber}", secondForwarderBankDetailsCertificateNumber, wordDocument);
                        ReplaseWordStub("{ForwarderEDRPOU}", secondForwarderBankDetailsEDRPOU, wordDocument);
                        ReplaseWordStub("{ForwarderAccountNumber}", secondForwarderBankDetailsAccountNumber, wordDocument);
                        ReplaseWordStub("{ForwarderBankName}", secondForwarderBankDetailsBankName, wordDocument);
                        ReplaseWordStub("{ForwarderMFO}", secondForwarderBankDetailsMFO, wordDocument);
                    }

                    if (transporterFullName == "" || transporterDirector == "" ||    transporterWorkDocument == "" || 
                        transporterTaxPayerStatus == "" || transporterBankDetailsBankName == "" || transporterBankDetailsAccountNumber == "" ||
                        transporterBankDetailsCertificateNumber == "" || transporterBankDetailsEDRPOU == "" || transporterBankDetailsIBAN == "" ||
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
                        ReplaseWordStub("{TransporterCerificateNumber}", transporterBankDetailsCertificateNumber, wordDocument);
                        ReplaseWordStub("{TransporterEDRPOU}", transporterBankDetailsEDRPOU, wordDocument);
                        ReplaseWordStub("{TransporterAccountNumber}", transporterBankDetailsAccountNumber, wordDocument);
                        ReplaseWordStub("{TransporterBankName}", transporterBankDetailsBankName, wordDocument);
                        ReplaseWordStub("{TransporterMFO}", transporterBankDetailsMFO, wordDocument);
                    }

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        wordDocument.SaveAs(folderBrowserDialog.SelectedPath + @"\test.docx");
                        wordApp.Visible = true;
                    }
                    else
                    {
                        wordDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                    }

                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Помилка: " + e);
            }
        }

        void ReplaseWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Replace: Word.WdReplace.wdReplaceAll);
        }
 
        void LoadClientFirstPersonDiapasonCombobox()
        {
            firstPersonDiapasonComboBox.Items.Clear();
            firstPersonNameComboBox.Items.Clear();
            firstPersonNameComboBox.Text = "";
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

        void LoadClientFirstPersonNameComboBox()
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
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        firstPersonNameComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
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

        void LoadForwarderFirstPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    firstPersonNameComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
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
    }
}
