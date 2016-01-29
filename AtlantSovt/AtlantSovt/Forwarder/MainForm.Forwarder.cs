using AtlantSovt.Additions;
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
            showForwarderDeleteButton.Enabled = false;
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
                forwarderDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                forwarderDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Назва;
                forwarderDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.П_І_Б_Директора;
                forwarderDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Фізична_адреса;
                forwarderDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Юридична_адреса;
                forwarderDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Статус_платника_податку;
                forwarderDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.На_основі;

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
                    forwarderContactsDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Контактна_особа;
                    forwarderContactsDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Телефон;
                    forwarderContactsDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Факс;
                    forwarderContactsDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Email;

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
                    forwarderBankDetailsDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Назва_банку;
                    forwarderBankDetailsDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.МФО;
                    forwarderBankDetailsDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку;
                    forwarderBankDetailsDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.ЄДРПОУ;
                    forwarderBankDetailsDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.ІПН;
                    forwarderBankDetailsDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Серія_свідоцтва;
                    forwarderBankDetailsDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Номер_свідоцтва;
                    forwarderBankDetailsDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.SWIFT;
                    forwarderBankDetailsDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.IBAN;
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_експедитора);
                }
            }

            forwarderContactsDataGridView.Update();
            forwarderBankDetailsDataGridView.Update();
            forwarderContactsDataGridView.Visible = true;
            forwarderBankDetailsDataGridView.Visible = true;
            showForwarderDeleteButton.Enabled = true;
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
                        string massage = AtlantSovt.Properties.Resources.Експедитор_успішно_доданий;


                        if (addForwarderBankDetailsAddForm != null)
                        {
                            massage += addForwarderBankDetailsAddForm.AddForwarderBankDetail(New_Forwarder.Id, true);
                            addForwarderBankDetailsAddForm = null;
                        }
                        if (addForwarderContactAddForm != null)
                        {
                            massage += addForwarderContactAddForm.AddForwarderContact(New_Forwarder.Id, true);
                            addForwarderContactAddForm = null;
                        }
                        AddForwarderStamp(New_Forwarder.Id);
                        MessageBox.Show(massage);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Одне_з_обов_язкових_полів_не_заповнено);
                }
            }
        }

        //Update

        void ClearAllBoxesForwarderUpdate()
        {
            workDocumentForwarderUpdateComboBox.SelectedIndex = -1;
            workDocumentForwarderUpdateComboBox.Items.Clear();
            taxPayerStatusForwarderUpdateComboBox.SelectedIndex = -1;
            taxPayerStatusForwarderUpdateComboBox.Items.Clear();
            nameForwarderUpdateTextBox.Clear();
            directorForwarderUpdateTextBox.Clear();           
            physicalAddressForwarderUpdateTextBox.Clear();
            geographyAddressForwarderUpdateTextBox.Clear();
            commentForwarderUpdateTextBox.Clear();
            updateForwarderStampPictureBox.Image = null;
        }

        void SplitUpdateForwarder()
        {
            using (var db = new AtlantSovtContext())
            {
                if (selectForwarderUpdateComboBox.SelectedIndex != -1 && selectForwarderUpdateComboBox.Text == selectForwarderUpdateComboBox.SelectedItem.ToString())
                {
                    string comboboxText = selectForwarderUpdateComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    forwarder = db.Forwarders.Find(id);
                }
                else
                {
                    forwarder = null;
                    ClearAllBoxesForwarderUpdate();
                }
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
                if (workDocumentForwarderUpdateComboBox.SelectedIndex != -1 && workDocumentForwarderUpdateComboBox.Text == workDocumentForwarderUpdateComboBox.SelectedItem.ToString())
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
                if (taxPayerStatusForwarderUpdateComboBox.SelectedIndex != -1 && taxPayerStatusForwarderUpdateComboBox.Text == taxPayerStatusForwarderUpdateComboBox.SelectedItem.ToString())
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
                if (forwarder != null)
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
                            if (forwarderWorkDocument != null)
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
                            if (forwarderTaxPayerStatus != null)
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
                        MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_змінено);
                    }
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Змін_не_знайдено);
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_експедитора);
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
        void DeleteForwarder(int id)
        {
            using (var db = new AtlantSovtContext())
            {
                deleteForwarder = db.Forwarders.Find(id);
                if (deleteForwarder != null)
                {
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_експедитора + deleteForwarder.Name + "?", AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.Forwarders.Attach(deleteForwarder);
                            db.Forwarders.Remove(deleteForwarder);
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Експедитор_успішно_видалений);
                            forwarderDeleteComboBox.Items.Remove(forwarderDeleteComboBox.SelectedItem);
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
                if (taxPayerStatusForwarderUpdateComboBox.SelectedIndex != -1 && taxPayerStatusForwarderUpdateComboBox.Text == taxPayerStatusForwarderUpdateComboBox.SelectedItem.ToString())
                {
                    string comboboxText = forwarderDeleteComboBox.SelectedItem.ToString();
                    string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedNameAndDirector[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    deleteForwarder = db.Forwarders.Find(id);
                }
                else
                {
                    deleteForwarder = null;
                }
            }
        }

    }
}
