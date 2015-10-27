﻿using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        bool forwarderNameChanged, forwarderDirectorChanged, forwarderPhysicalAddressChanged, forwarderGeographyAddressChanged, forwarderCommentChanged, forwarderWorkDocumentChanged, forwarderTaxPayerStatusChanged, forwarderStampChanged ,forwarderIsWorkDocumentExist, forwarderIsTaxPayerStatusExist;


        Forwarder forwarder, deleteForwarder;
        WorkDocument forwarderWorkDocument = null;
        TaxPayerStatu forwarderTaxPayerStatus = null;

        //show
        void ShowForwarder()
        {
            forwarderContactsDataGridView.Visible = false;
            forwarderBankDetailsDataGridView.Visible = false;
            forwarderCommentRichTextBox.Text = "";

            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.Forwarders
                orderby c.Id
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
                        CertificateSerial = b.CertificateSerial,
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
                    forwarderBankDetailsDataGridView.Columns[5].HeaderText = "Серія свідоцтва";
                    forwarderBankDetailsDataGridView.Columns[6].HeaderText = "Номер свідоцтва";
                    forwarderBankDetailsDataGridView.Columns[7].HeaderText = "SWIFT";
                    forwarderBankDetailsDataGridView.Columns[8].HeaderText = "IBAN";
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Немає жодного експедитора");
                }
            }

            forwarderContactsDataGridView.Update();
            forwarderBankDetailsDataGridView.Update();
            forwarderContactsDataGridView.Visible = true;
            forwarderBankDetailsDataGridView.Visible = true;
        }

        //add

        public byte[] imageToByteArray(Image stamp)
        {
            MemoryStream mStream = new MemoryStream();
            stamp.Save(mStream, ImageFormat.Png);
            return  mStream.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
             MemoryStream ms = new MemoryStream(byteArrayIn);
             Image returnImage = Image.FromStream(ms);
             return returnImage;
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

        void SplitLoadWorkDocumentForwarderAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                if (workDocumentForwarderComboBox.Text != "")
                {
                    string comboboxText = workDocumentForwarderComboBox.SelectedItem.ToString();
                    string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedStatus[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarderWorkDocument = db.WorkDocuments.Find(id);
                }
                else
                {
                    forwarderWorkDocument = null;
                }
            }
        }

        void SplitLoadTaxPayerStatusForwarderAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                if (taxPayerStatusForwarderComboBox.Text != "")
                {
                    string comboboxText = taxPayerStatusForwarderComboBox.SelectedItem.ToString();
                    string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedStatus[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarderTaxPayerStatus = db.TaxPayerStatus.Find(id);
                }
                else 
                {
                    forwarderTaxPayerStatus = null;
                }
            }
        }

        void AddForwarderStamp(long id)
        {
            using (var db = new AtlantSovtContext())
            {
                ForwarderStamp New_ForwarderStamp = new ForwarderStamp
                {
                    Id = id,
                    Stamp = forwarderAddStamp != null ? imageToByteArray(forwarderAddStamp) : null

                };
                try
                {
                    db.ForwarderStamps.Add(New_ForwarderStamp);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);

                }
            }
        }

        void AddForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                if (nameForwarderTextBox.Text != "" || directorForwarderTextBox.Text != "" )
                {
                    var new_Name = nameForwarderTextBox.Text;
                    var new_Director = directorForwarderTextBox.Text;
                    var new_PhysicalAddress = physicalAddressForwarderTextBox.Text;
                    var new_GeographyAddress = geographyAddressForwarderTextBox.Text;
                    var new_Comment = commentForwarderTextBox.Text;

                    long new_WorkDocumentId = 0;
                    long new_TaxPayerStatusId = 0;
                    if (forwarderWorkDocument != null)
                    {
                        forwarderIsWorkDocumentExist = true;
                        new_WorkDocumentId = forwarderWorkDocument.Id;
                    }
                    else
                    {
                        forwarderIsWorkDocumentExist = false;
                    }
                    if (forwarderTaxPayerStatus != null)
                    {
                        forwarderIsTaxPayerStatusExist = true;
                        new_TaxPayerStatusId = forwarderTaxPayerStatus.Id;
                    }
                    else
                    {
                        forwarderIsTaxPayerStatusExist = false;
                    }

                    Forwarder New_Forwarder = new Forwarder();

                    if (!forwarderIsWorkDocumentExist && !forwarderIsTaxPayerStatusExist)
                    {
                        New_Forwarder = new Forwarder
                        {
                            Name = new_Name,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeographyAddress = new_GeographyAddress,
                            Comment = new_Comment,
                        };
                    }

                    else if (forwarderIsWorkDocumentExist && forwarderIsTaxPayerStatusExist)
                    {
                        New_Forwarder = new Forwarder
                        {
                            Name = new_Name,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeographyAddress = new_GeographyAddress,
                            WorkDocumentId = new_WorkDocumentId,
                            TaxPayerStatusId = new_TaxPayerStatusId,
                            Comment = new_Comment,                            
                        };
                    }

                    else if (forwarderIsWorkDocumentExist && !forwarderIsTaxPayerStatusExist)
                    {
                        New_Forwarder = new Forwarder
                        {
                            Name = new_Name,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeographyAddress = new_GeographyAddress,
                            WorkDocumentId = new_WorkDocumentId,
                            Comment = new_Comment,                            
                        };
                    }

                    else if (!forwarderIsWorkDocumentExist && forwarderIsTaxPayerStatusExist)
                    {
                        New_Forwarder = new Forwarder
                        {
                            Name = new_Name,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeographyAddress = new_GeographyAddress,
                            TaxPayerStatusId = new_TaxPayerStatusId,
                            Comment = new_Comment,
                        };
                    }

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
                        AddForwarderStamp(New_Forwarder.Id);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Одне з обов'язкових полів не заповнено");
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
                    nameForwarderUpdateTextBox.Text = Convert.ToString(forwarder.Name);
                    directorForwarderUpdateTextBox.Text = Convert.ToString(forwarder.Director);
                    physicalAddressForwarderUpdateTextBox.Text = Convert.ToString(forwarder.PhysicalAddress);
                    geographyAddressForwarderUpdateTextBox.Text = Convert.ToString(forwarder.GeographyAddress);
                    commentForwarderUpdateTextBox.Text = Convert.ToString(forwarder.Comment);
                    if (forwarder.ForwarderStamp.Stamp != null)
                    {
                        updateForwarderStampPictureBox.Image = Image.FromStream(new MemoryStream(forwarder.ForwarderStamp.Stamp));
                    }
                    else
                    {
                        updateForwarderStampPictureBox.Image = null;
                    }
                    if (forwarder.WorkDocument != null)
                    {
                       workDocumentForwarderUpdateComboBox.SelectedIndex = workDocumentForwarderUpdateComboBox.FindString(forwarder.WorkDocument.Status + " [" + forwarder.WorkDocument.Id + ']');
                    }
                    else
                    {
                        workDocumentForwarderUpdateComboBox.Text = "";
                        workDocumentForwarderUpdateComboBox.SelectedIndex = -1;
                    }
                    if (forwarder.TaxPayerStatu != null)
                    {
                        taxPayerStatusForwarderUpdateComboBox.SelectedIndex = taxPayerStatusForwarderUpdateComboBox.FindString(forwarder.TaxPayerStatu.Status + " [" + forwarder.TaxPayerStatu.Id + ']');
                    }
                    else 
                    {
                        taxPayerStatusForwarderUpdateComboBox.Text = "";
                        taxPayerStatusForwarderUpdateComboBox.SelectedIndex = -1;
                    }
                }
                forwarderNameChanged = forwarderDirectorChanged = forwarderPhysicalAddressChanged = forwarderGeographyAddressChanged = forwarderCommentChanged = forwarderWorkDocumentChanged = forwarderTaxPayerStatusChanged = forwarderStampChanged = false;
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
                if (workDocumentForwarderUpdateComboBox.Text != "")
                {
                    string comboboxText = workDocumentForwarderUpdateComboBox.SelectedItem.ToString();
                    string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedStatus[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarderWorkDocument = db.WorkDocuments.Find(id);
                }
                else
                {
                    forwarderWorkDocument = null;
                }
            }
        }

        void SplitLoadTaxPayerStatusForwarderUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                if (taxPayerStatusForwarderUpdateComboBox.Text != "")
                {
                    string comboboxText = taxPayerStatusForwarderUpdateComboBox.SelectedItem.ToString();
                    string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedStatus[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarderTaxPayerStatus = db.TaxPayerStatus.Find(id);
                }
                else
                {
                    forwarderTaxPayerStatus = null;
                }
            }
        }

        void UpdateForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (forwarderNameChanged || forwarderDirectorChanged || forwarderPhysicalAddressChanged || forwarderGeographyAddressChanged || forwarderCommentChanged || forwarderWorkDocumentChanged || forwarderTaxPayerStatusChanged || forwarderStampChanged)
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
                        if (workDocumentForwarderUpdateComboBox.Text != "")
                        {
                            forwarder.WorkDocument = null;
                            forwarder.WorkDocumentId = forwarderWorkDocument.Id;
                        }
                        else
                        {
                            forwarder.WorkDocumentId = null;
                            forwarder.WorkDocument = null;
                        }
                    }
                    if (forwarderTaxPayerStatusChanged)
                    {
                        if (taxPayerStatusForwarderUpdateComboBox.Text != "")
                        {
                            forwarder.TaxPayerStatu = null;
                            forwarder.TaxPayerStatusId = forwarderTaxPayerStatus.Id;
                        }
                        else
                        {
                            forwarder.TaxPayerStatusId = null;
                            forwarder.TaxPayerStatu = null;
                        }
                    }
                    if (forwarderStampChanged)
                    {
                        forwarder.ForwarderStamp.Stamp = (updateForwarderStampPictureBox.Image != null) ? imageToByteArray(updateForwarderStampPictureBox.Image) : null;
                    }

                    db.Entry(forwarder).State = EntityState.Modified;
                    db.Entry(forwarder.ForwarderStamp).State = EntityState.Modified;

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
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show("Помилка!" + Environment.NewLine + ex);
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