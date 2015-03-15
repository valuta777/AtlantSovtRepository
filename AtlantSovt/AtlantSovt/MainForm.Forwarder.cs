using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    partial class MainForm
    {
        bool forwarderWorkDocumentAdded;
        bool forwarderTaxPayerStatusAdded;
        bool forwarderNameChanged, forwarderDirectorChanged, forwarderPhysicalAddressChanged, forwarderGeographyAddressChanged, forwarderCommentChanged, forwarderWorkDocumentChanged, forwarderTaxPayerStatusChanged;


        Forwarder forwarder, deleteForwarder;
        WorkDocument forwarderWorkDocument;
        TaxPayerStatu forwarderTaxPayerStatus;

        //show
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


        //add
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
                forwarderWorkDocument = db.WorkDocuments.Find(id);
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
                forwarderTaxPayerStatus = db.TaxPayerStatus.Find(id);
            }
        }

        void AddForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                if (nameForwarderTextBox.Text != "" && directorForwarderTextBox.Text != "" && physicalAddressForwarderTextBox.Text != "" && geographyAddressForwarderTextBox.Text != "" && forwarderWorkDocumentAdded && forwarderTaxPayerStatusAdded)
                {
                    var new_Name = nameForwarderTextBox.Text;
                    var new_Director = directorForwarderTextBox.Text;
                    var new_PhysicalAddress = physicalAddressForwarderTextBox.Text;
                    var new_GeographyAddress = geographyAddressForwarderTextBox.Text;
                    var new_WorkDocumentId = forwarderWorkDocument.Id;
                    var new_TaxPayerStatusId = forwarderTaxPayerStatus.Id;
                    var new_Comment = commentForwarderTextBox.Text;

                    Forwarder New_Forwarder = new Forwarder
                    {
                        Name = new_Name,
                        Director = new_Director,
                        PhysicalAddress = new_PhysicalAddress,
                        GeographyAddress = new_GeographyAddress,
                        WorkDocumentId = new_WorkDocumentId,
                        TaxPayerStatusId = new_TaxPayerStatusId,
                        Comment = new_Comment                        
                    };
                    try
                    {
                        db.Forwarders.Add(New_Forwarder);
                        db.SaveChanges();
                        MessageBox.Show("Експедитор успішно доданий");

                        if (addForwarderBankDetailsAddForm != null)
                        {
                            addForwarderBankDetailsAddForm.AddForwarderBankDetail(New_Forwarder.Id);
                            addForwarderBankDetailsAddForm = null;
                        }
                        if (addForwarderContactAddForm != null)
                        {
                            addForwarderContactAddForm.AddForwarderContact(New_Forwarder.Id);
                            addForwarderContactAddForm = null;
                        }
                    }
                    catch (Exception fe)
                    {
                        MessageBox.Show(fe.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Обов'язкові поля не заповнені");
                }
            }
        }

        //Update

        void ClearAllBoxesForwarderUpdate()
        {
            workDocumentForwarderUpdateComboBox.Items.Clear();
            taxPayerStatusForwarderUpdateComboBox.Items.Clear();
            nameForwarderUpdateTextBox.Clear();
            directorForwarderUpdateTextBox.Clear();           
            physicalAddressForwarderUpdateTextBox.Clear();
            geographyAddressForwarderUpdateTextBox.Clear();
            commentForwarderUpdateTextBox.Clear();
        }

        void SplitUpdateForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = selectForwarderUpdateComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarder = db.Forwarders.Find(id);
                if (forwarder != null)
                {
                    nameForwarderUpdateTextBox.Text = forwarder.Name.ToString();
                    directorForwarderUpdateTextBox.Text = forwarder.Director.ToString();
                    physicalAddressForwarderUpdateTextBox.Text = forwarder.PhysicalAddress.ToString();
                    geographyAddressForwarderUpdateTextBox.Text = forwarder.GeographyAddress.ToString();
                    commentForwarderUpdateTextBox.Text = forwarder.Comment.ToString();
                    workDocumentForwarderUpdateComboBox.SelectedIndex = Convert.ToInt32(forwarder.WorkDocumentId - 1);
                    taxPayerStatusForwarderUpdateComboBox.SelectedIndex = Convert.ToInt32(forwarder.TaxPayerStatusId - 1);                    
                }
                forwarderNameChanged = forwarderDirectorChanged = forwarderPhysicalAddressChanged = forwarderGeographyAddressChanged = forwarderCommentChanged = forwarderWorkDocumentChanged = forwarderTaxPayerStatusChanged = false;
            }
        }

        void LoadSelectForwarderUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    selectForwarderUpdateComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }

        void LoadWorkDocumentForwarderUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from w in db.WorkDocuments
                            orderby w.Id
                            select w;
                foreach (var item in query)
                {
                    workDocumentForwarderUpdateComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void LoadTaxPayerStatusForwarderUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.TaxPayerStatus
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    taxPayerStatusForwarderUpdateComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadWorkDocumentForwarderUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = workDocumentForwarderUpdateComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarderWorkDocument = db.WorkDocuments.Find(id);
            }
        }

        void SplitLoadTaxPayerStatusForwarderUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = taxPayerStatusForwarderUpdateComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                forwarderTaxPayerStatus = db.TaxPayerStatus.Find(id);
            }
        }

        void UpdateForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (forwarderNameChanged || forwarderDirectorChanged || forwarderPhysicalAddressChanged || forwarderGeographyAddressChanged || forwarderCommentChanged || forwarderWorkDocumentChanged || forwarderTaxPayerStatusChanged )
                {
                    if (forwarderNameChanged)
                    {
                        forwarder.Name = nameForwarderUpdateTextBox.Text;
                    }
                    if (forwarderDirectorChanged)
                    {
                        forwarder.Director = directorForwarderUpdateTextBox.Text;
                    }
                    if (forwarderPhysicalAddressChanged)
                    {
                        forwarder.PhysicalAddress = physicalAddressForwarderUpdateTextBox.Text;
                    }
                    if (forwarderGeographyAddressChanged)
                    {
                        forwarder.GeographyAddress = geographyAddressForwarderUpdateTextBox.Text;
                    }                   
                    if (forwarderCommentChanged)
                    {
                        forwarder.Comment = commentForwarderUpdateTextBox.Text;
                    }
                    if (forwarderWorkDocumentChanged)
                    {
                        forwarder.WorkDocumentId = forwarderWorkDocument.Id;
                    }
                    if (forwarderTaxPayerStatusChanged)
                    {
                        forwarder.TaxPayerStatusId = forwarderTaxPayerStatus.Id;
                    }

                    db.Entry(forwarder).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Успішно змінено");
                }
                else
                {
                    MessageBox.Show("Змін не знайдено");
                }
            }
        }
        //Forwarder Contact

        //Add
        void AddNewForwarderContact()
        {
            if (forwarder != null)
            {
                updateForwarderContactAddForm.AddForwarderContact2(forwarder.Id);
                updateForwarderContactAddForm = null;
            }
        }

        //Update
        void UpdateForwarderContact()
        {
            if (forwarder != null)
            {
                updateForwarderContactUpdateForm.UpdateForwarderContact(forwarder);
            }
        }

        //Delete
        void DeleteForwarderContact()
        {
            if (forwarder != null)
            {
                deleteForwarderContactDeleteForm.DeleteForwarderContact(forwarder);
            }
        }



        //Delete
        void DeleteForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                if (deleteForwarder != null)
                {
                    if (MessageBox.Show("Видалити експедитора " + deleteForwarder.Name + "?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.Forwarders.Attach(deleteForwarder);
                            db.Forwarders.Remove(deleteForwarder);
                            db.SaveChanges();
                            MessageBox.Show("Експедитор успішно видалений");
                            forwarderDeleteComboBox.Items.Remove(forwarderDeleteComboBox.SelectedItem);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Помилка!" + Environment.NewLine + e);
                        }
                    }
                }
            }
        }

        void LoadForwarderDeleteInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from c in db.Forwarders
                            orderby c.Id
                            select c;
                foreach (var item in query)
                {
                    forwarderDeleteComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }

        void SplitDeleteForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = forwarderDeleteComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                deleteForwarder = db.Forwarders.Find(id);
            }
        }

    }
}
