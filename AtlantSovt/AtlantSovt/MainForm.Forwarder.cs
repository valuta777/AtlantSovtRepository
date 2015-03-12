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
    partial class MainForm
    {
        void ShowForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.Forwarders
                select
                new
                {
                    Id = c.Id,
                    Name = c.Name,
                    Director = c.Director,
                    PhysicalAddress = c.PhysicalAddress,
                    GeografphyAddress = c.GeographyAddress,
                    TaxPayerStatusId = c.TaxPayerStatu.Status,
                    WorkDocumentId = c.WorkDocument.Status,
                };

                forwarderDataGridView.DataSource = query.ToList();
                forwarderDataGridView.Columns[0].HeaderText = "Порядковий номер";
                forwarderDataGridView.Columns[1].HeaderText = "Назва";
                forwarderDataGridView.Columns[2].HeaderText = "П.І.Б. Директора";
                forwarderDataGridView.Columns[3].HeaderText = "Фізична адреса";
                forwarderDataGridView.Columns[4].HeaderText = "Юридична адреса";
                forwarderDataGridView.Columns[5].HeaderText = "Статус платника податку";
                forwarderDataGridView.Columns[6].HeaderText = "На основі";

            } forwarderDataGridView.Update();

        }

        void ShowForwarderInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {

                    var ClikedId = Convert.ToInt32(forwarderDataGridView.CurrentRow.Cells[0].Value);
                    var query =
                    from con in db.ForwarderContacts
                    where con.ForwarderId == ClikedId
                    select new
                    {
                        Контактна_персона = con.ContactPerson,
                        Номер = con.TelephoneNumber,
                        Факс = con.FaxNumber,
                        Email = con.Email,
                    };
                    forwarderContactsDataGridView.DataSource = query.ToList();
                    forwarderContactsDataGridView.Columns[0].HeaderText = "Контактна особа";
                    forwarderContactsDataGridView.Columns[1].HeaderText = "Телефон";
                    forwarderContactsDataGridView.Columns[2].HeaderText = "Факс";
                    forwarderContactsDataGridView.Columns[3].HeaderText = "Email";

                    var query1 =
                        from f in db.Forwarders
                        where f.Id == ClikedId
                        select f.Comment;

                    forwarderCommentRichTextBox.Text = query1.FirstOrDefault();


                    var query2 =
                    from b in db.ForwarderBankDetails
                    where b.Id == ClikedId
                    select new
                    {
                        Name = b.BankName,
                        MFO = b.MFO,
                        AccountNumber = b.AccountNumber,
                        EDRPOU = b.EDRPOU,
                        IPN = b.IPN,
                        CertificateNumber = b.CertificateNamber,
                        SWIFT = b.SWIFT,
                        IBAN = b.IBAN
                    };

                    forwarderBankDetailsDataGridView.DataSource = query2.ToList();
                    forwarderBankDetailsDataGridView.Columns[0].HeaderText = "Назва банку";
                    forwarderBankDetailsDataGridView.Columns[1].HeaderText = "МФО";
                    forwarderBankDetailsDataGridView.Columns[2].HeaderText = "Номер рахунку";
                    forwarderBankDetailsDataGridView.Columns[3].HeaderText = "ЕДРПОУ";
                    forwarderBankDetailsDataGridView.Columns[4].HeaderText = "IPN";
                    forwarderBankDetailsDataGridView.Columns[5].HeaderText = "Номер свідоцтва";
                    forwarderBankDetailsDataGridView.Columns[6].HeaderText = "SWIFT";
                    forwarderBankDetailsDataGridView.Columns[7].HeaderText = "IBAN";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Немає жодного експедитора");
                }
            }

            forwarderContactsDataGridView.Update();
            forwarderBankDetailsDataGridView.Update();
            forwarderContactsDataGridView.Visible = true;
            forwarderBankDetailsDataGridView.Visible = true;
        }

        void LoadWorkDocumentForwarderAddInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from w in db.WorkDocuments
                            orderby w.Id
                            select w;
                foreach (var item in query)
                {
                    workDocumentForwarderComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadWorkDocumentForwarderAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = workDocumentForwarderComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                workDocument = db.WorkDocuments.Find(id);
            }
        }

        void LoadTaxPayerStatusForwarderAddInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.TaxPayerStatus
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    taxPayerStatusForwarderComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadTaxPayerStatusForwarderAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = taxPayerStatusForwarderComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                taxPayerStatus = db.TaxPayerStatus.Find(id);
            }
        }

        void AddForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                if (nameForwarderTextBox.Text != "" && directorForwarderTextBox.Text != "" && physicalAddressForwarderTextBox.Text != "" && geographyAddressForwarderTextBox.Text != "" && workDocumentFlag && taxPayerStatusFlag)
                {
                    var new_Name = nameForwarderTextBox.Text;
                    var new_Director = directorForwarderTextBox.Text;
                    var new_PhysicalAddress = physicalAddressForwarderTextBox.Text;
                    var new_GeographyAddress = geographyAddressForwarderTextBox.Text;
                    var new_WorkDocumentId = workDocument.Id;
                    var new_TaxPayerStatusId = taxPayerStatus.Id;
                    var new_Comment = commentForwarderTextBox.Text;

                    var New_Forwarder = new Forwarder
                    {
                        Name = new_Name,
                        Director = new_Director,
                        PhysicalAddress = new_PhysicalAddress,
                        GeographyAddress = new_GeographyAddress,
                        WorkDocumentId = new_WorkDocumentId,
                        TaxPayerStatusId = new_TaxPayerStatusId,
                        Comment = new_Comment,
                    };
                    try
                    {
                        db.Forwarders.Add(New_Forwarder);
                        db.SaveChanges();
                        MessageBox.Show("Експедитор успішно доданий");

                        if (addForwarderBankDetailsForm != null)
                        {
                            addForwarderBankDetailsForm.AddForwarderBankDetail(New_Forwarder.Id);
                            addClientBankDetailsForm = null;
                        }
                        if (addContactForwarderForm != null)
                        {
                            addContactForwarderForm.AddForwarderContact(New_Forwarder.Id);
                            addContactForwarderForm = null;
                        }
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Обов'язкові поля не заповнені");
                }
            }

        }

    }
}
