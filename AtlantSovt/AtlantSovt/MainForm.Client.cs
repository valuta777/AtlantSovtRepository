using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    partial class MainForm
    {

        long id;
        bool workDocumentFlag;
        bool taxPayerStatusFlag;
        bool utb1, utb2, utb3, utb4, utb5, utb6, ucb1, ucb2, uc1, uc2;

        Client client, deleteClient;
        WorkDocument workDocument;
        TaxPayerStatu taxPayerStatus;

        // add
        void AddClient()
        {
            using (var db = new AtlantSovtContext())
            {
                if (nameClientTextBox.Text != "" && directorClientTextBox.Text != "" && physicalAddressClientTextBox.Text != "" && geographyAddressClientTextBox.Text != "" && workDocumentFlag && taxPayerStatusFlag)
                {
                    var new_Name = nameClientTextBox.Text;
                    var new_Director = directorClientTextBox.Text;
                    var new_PhysicalAddress = physicalAddressClientTextBox.Text;
                    var new_GeografphyAddress = geographyAddressClientTextBox.Text;
                    var new_WorkDocumentId = workDocument.Id;
                    var new_TaxPayerStatusId = taxPayerStatus.Id;
                    var new_ContractType = originalClientCheckBox.Checked;
                  //  var new_ContractNumber = geographyAddressClientTextBox.Text;
                    var new_Comment = commentClientTextBox.Text;

                    var New_Client = new Client
                    {
                        Name = new_Name,
                        Director = new_Director,
                        PhysicalAddress = new_PhysicalAddress,
                        GeografphyAddress = new_GeografphyAddress,
                        WorkDocumentId = new_WorkDocumentId,
                        TaxPayerStatusId = new_TaxPayerStatusId,
                        ContractType = new_ContractType,
                    //  ContractNumber = new_ContractNumber,
                        Comment = new_Comment,
                    };
                    try
                    {
                        db.Clients.Add(New_Client);
                        db.SaveChanges();
                        MessageBox.Show("Клієнт успішно доданий");

                        if (addClientBankDetailsForm != null)
                        {
                            addClientBankDetailsForm.AddClientBankDetail(New_Client.Id);
                            addClientBankDetailsForm = null;
                        }
                        if (addContactForm != null)
                        {
                            addContactForm.AddClientContact(New_Client.Id);
                            addContactForm = null;
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
        
        void ShowClient()
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.Clients
                select
                new
                {
                    Id = c.Id,
                    Name = c.Name,
                    Director = c.Director,
                    PhysicalAddress = c.PhysicalAddress,
                    GeografphyAddress = c.GeografphyAddress,
                    ContractType = c.ContractType,
                    TaxPayerStatusId = c.TaxPayerStatu.Status,
                    WorkDocumentId = c.WorkDocument.Status,
                };

                clientDataGridView.DataSource = query.ToList();
                clientDataGridView.Columns[0].HeaderText = "Порядковий номер";
                clientDataGridView.Columns[1].HeaderText = "Назва";
                clientDataGridView.Columns[2].HeaderText = "П.І.Б. Директора";
                clientDataGridView.Columns[3].HeaderText = "Фізична адреса";
                clientDataGridView.Columns[4].HeaderText = "Юридична адреса";
                clientDataGridView.Columns[5].HeaderText = "Оригінал договору";
                clientDataGridView.Columns[6].HeaderText = "Статус платника податку";
                clientDataGridView.Columns[7].HeaderText = "На основі";


            } clientDataGridView.Update();

        }
        
        void ShowClientInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {

                    var ClikedId = Convert.ToInt32(clientDataGridView.CurrentRow.Cells[0].Value);
                    var query =
                    from con in db.ClientContacts
                    where con.ClientId == ClikedId
                    select new
                    {
                        Контактна_персона = con.ContactPerson,
                        Номер = con.TelephoneNumber,
                        Факс = con.FaxNumber,
                        Email = con.Email,
                    };
                    clientContactsDataGridView.DataSource = query.ToList();
                    clientContactsDataGridView.Columns[0].HeaderText = "Контактна особа";
                    clientContactsDataGridView.Columns[1].HeaderText = "Телефон";
                    clientContactsDataGridView.Columns[2].HeaderText = "Факс";
                    clientContactsDataGridView.Columns[3].HeaderText = "Email";

                    var query1 =
                        from c in db.Clients
                        where c.Id == ClikedId
                        select c.Comment;

                    clientCommentRichTextBox.Text = query1.FirstOrDefault();


                    var query2 =
                    from b in db.ClientBankDetails
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

                    clientBankDetailsDataGridView.DataSource = query2.ToList();
                    clientBankDetailsDataGridView.Columns[0].HeaderText = "Назва банку";
                    clientBankDetailsDataGridView.Columns[1].HeaderText = "МФО";
                    clientBankDetailsDataGridView.Columns[2].HeaderText = "Номер рахунку";
                    clientBankDetailsDataGridView.Columns[3].HeaderText = "ЕДРПОУ";
                    clientBankDetailsDataGridView.Columns[4].HeaderText = "IPN";
                    clientBankDetailsDataGridView.Columns[5].HeaderText = "Номер свідоцтва";
                    clientBankDetailsDataGridView.Columns[6].HeaderText = "SWIFT";
                    clientBankDetailsDataGridView.Columns[7].HeaderText = "IBAN";
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Немає жодного клієнта");
                }
            } 
            clientContactsDataGridView.Update();
            clientBankDetailsDataGridView.Update();
            clientContactsDataGridView.Visible = true;
            clientBankDetailsDataGridView.Visible = true;
        }
        
        void LoadTaxPayerStatusClientAddInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.TaxPayerStatus
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    taxPayerStatusClientComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }
        
        void LoadWorkDocumentClientAddInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from w in db.WorkDocuments
                            orderby w.Id
                            select w;
                foreach (var item in query)
                {
                    workDocumentClientComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }
        
        void SplitLoadTaxPayerStatusClientAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = taxPayerStatusClientComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                taxPayerStatus = db.TaxPayerStatus.Find(id);
            }
        }
        
        void SplitLoadWorkDocumentClientAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = workDocumentClientComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                workDocument = db.WorkDocuments.Find(id);
            }
        }
        
        //update

        void ClearAllBoxesClientUpdate()
        {
            workDocumentClientUpdateComboBox.Items.Clear();
            taxPayerStatusClientUpdateComboBox.Items.Clear();
            nameClientUpdateTextBox.Clear();
            directorClientUpdateTextBox.Clear();
            contractNumberClientUpdateTextBox.Clear();
            physicalAddressClientUpdateTextBox.Clear();
            geographyAddressClientUpdateTextBox.Clear();
            commentClientUpdateTextBox.Clear();
        }

        void SplitUpdateClient()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = selectClientUpdateComboBox.SelectedItem.ToString();                
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                client = db.Clients.Find(id);
                if (client != null)
                {
                    nameClientUpdateTextBox.Text = client.Name.ToString();
                    directorClientUpdateTextBox.Text = client.Director.ToString();
                    physicalAddressClientUpdateTextBox.Text = client.PhysicalAddress.ToString();
                    geographyAddressClientUpdateTextBox.Text = client.GeografphyAddress.ToString();
                    commentClientUpdateTextBox.Text = client.Comment.ToString();
                    workDocumentClientUpdateComboBox.SelectedIndex = Convert.ToInt32(client.WorkDocumentId - 1);
                    taxPayerStatusClientUpdateComboBox.SelectedIndex = Convert.ToInt32(client.TaxPayerStatusId - 1);
                    originalClientUpdateCheckBox.Checked = client.ContractType.Value;
                    faxClientUpdateCheckBox.Checked = !client.ContractType.Value;
                }
                utb1 =  utb2 = utb3 = utb4 =  utb5 =  utb6 = ucb1 =  ucb2 =  uc1 =  uc2 = false;
            }
        }

        void LoadSelectClientUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from c in db.Clients
                            orderby c.Id
                            select c;
                foreach (var item in query)
                {
                    selectClientUpdateComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }

        void LoadWorkDocumentClientUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from w in db.WorkDocuments
                            orderby w.Id
                            select w;
                foreach (var item in query)
                {
                    workDocumentClientUpdateComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void LoadTaxPayerStatusClientUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.TaxPayerStatus
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    taxPayerStatusClientUpdateComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadWorkDocumentClientUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = workDocumentClientUpdateComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                workDocument = db.WorkDocuments.Find(id);
            }
        }

        void SplitLoadTaxPayerStatusClientUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = taxPayerStatusClientUpdateComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                taxPayerStatus = db.TaxPayerStatus.Find(id);
            }
        }

        void UpdateClient()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (utb1 || utb2 || utb3 || utb4 || utb5 || utb6 || ucb1 || ucb2 || ucb1 || ucb2 || uc1 || uc2)
                {
                    if (utb1)
                    {
                        client.Name = nameClientUpdateTextBox.Text;
                    }
                    if (utb2)
                    {
                        client.Director = directorClientUpdateTextBox.Text;
                    }
                    if (utb3)
                    {
                        client.ContractNumber = contractNumberClientUpdateTextBox.Text;
                    }
                    if (utb4)
                    {
                        client.PhysicalAddress = physicalAddressClientUpdateTextBox.Text;
                    }
                    if (utb5)
                    {
                        client.GeografphyAddress = geographyAddressClientUpdateTextBox.Text;
                    }
                    if (utb6)
                    {
                        client.Comment = commentClientUpdateTextBox.Text;
                    }
                    if (ucb1)
                    {
                        client.WorkDocumentId = workDocument.Id;
                    }
                    if (ucb2)
                    {
                        client.TaxPayerStatusId = taxPayerStatus.Id;
                    }
                    if (uc1)
                    {
                        client.ContractType = true;
                    }
                    else 
                    {
                        client.ContractType = false;
                    }

                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Успішно змінено");
                }
                else
                {
                    MessageBox.Show("Змін не знайдено");
                }
            }
        }

        // ClientContact

        void AddNewContact()
        {
            if (client != null)
            {
                ClientUpdateAddContactForm.AddClientContact2(client.Id);
                ClientUpdateAddContactForm = null;
            }
        }
        
        void UpdateContact()
        {
            if (client != null) 
            {
                UpdateClientUpdateContactForm.UpdateContact(client);
            }           
        }

        void DeleteContact()
        {
            if (client != null)
            {
                deleteContactForm.DeleteContact(client);
            }
        }

        //delete

        void DeleteClient()
        {
            using (var db = new AtlantSovtContext())
            {
                if (deleteClient != null)
                {
                    if (MessageBox.Show("Видалити клієнта " + deleteClient.Name + "?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.Clients.Attach(deleteClient);
                            db.Clients.Remove(deleteClient);
                            db.SaveChanges();
                            MessageBox.Show("Клієнта успішно видалено");
                            deleteClientComboBox.Items.Remove(deleteClientComboBox.SelectedItem);
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Помилка!" + Environment.NewLine + e);
                        }
                    }
                }
            }
        }

        void LoadClientDeleteInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from c in db.Clients
                            orderby c.Id
                            select c;
                foreach (var item in query)
                {
                    deleteClientComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }

        void SplitDeleteClient()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = deleteClientComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                deleteClient = db.Clients.Find(id);
            }
        }
    }
}
