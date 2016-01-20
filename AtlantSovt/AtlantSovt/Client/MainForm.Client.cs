using AtlantSovt.Additions;
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
        bool clientWorkDocumentFlag;
        bool clientTaxPayerStatusFlag;
        bool clientNameChanged, clientDirectorChanged, clientPhysicalAddressChanged, clientGeographyAddressChanged,clientCommentChanged, clientWorkDocumentChanged, clientTaxPayerStatusChanged, clientOriginalChanged, clientFaxChanged, clientIsWorkDocumentExist, clientIsTaxPayerStatusExist;
        Client client, deleteClient;
        WorkDocument clientWorkDocument = null;
        TaxPayerStatu clientTaxPayerStatus = null;
        
        //Show
        #region Show

        void ShowClient()
        {
            clientContactsDataGridView.Visible = false;
            clientBankDetailsDataGridView.Visible = false;
            showClientDeleteButton.Enabled = false;
            clientCommentRichTextBox.Text = "";

            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.Clients
                orderby c.Id
                select
                new
                {
                    Id = c.Id,
                    Name = c.Name,
                    Director = c.Director,
                    PhysicalAddress = c.PhysicalAddress,
                    GeografphyAddress = c.GeografphyAddress,
                    ContractType = (c.ContractType == true) ? "Оригінал" : "Факс",
                    TaxPayerStatusId = c.TaxPayerStatu.Status,
                    WorkDocumentId = c.WorkDocument.Status,
                };

                clientDataGridView.DataSource = query.ToList();
                clientDataGridView.Columns[0].HeaderText = "Порядковий номер";
                clientDataGridView.Columns[1].HeaderText = "Назва";
                clientDataGridView.Columns[2].HeaderText = "П.І.Б. Директора";
                clientDataGridView.Columns[3].HeaderText = "Фізична адреса";
                clientDataGridView.Columns[4].HeaderText = "Юридична адреса";
                clientDataGridView.Columns[5].HeaderText = "Стан договору";
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
                        CertificateSerial = b.CertificateSerial,
                        CertificateNumber = b.CertificateNamber,
                        SWIFT = b.SWIFT,
                        IBAN = b.IBAN
                    };

                    clientBankDetailsDataGridView.DataSource = query2.ToList();
                    clientBankDetailsDataGridView.Columns[0].HeaderText = "Назва банку";
                    clientBankDetailsDataGridView.Columns[1].HeaderText = "МФО";
                    clientBankDetailsDataGridView.Columns[2].HeaderText = "Номер рахунку";
                    clientBankDetailsDataGridView.Columns[3].HeaderText = "ЕДРПОУ";
                    clientBankDetailsDataGridView.Columns[4].HeaderText = "ІПН";
                    clientBankDetailsDataGridView.Columns[5].HeaderText = "Серія свідоцтва";
                    clientBankDetailsDataGridView.Columns[6].HeaderText = "Номер свідоцтва";
                    clientBankDetailsDataGridView.Columns[7].HeaderText = "SWIFT";
                    clientBankDetailsDataGridView.Columns[8].HeaderText = "IBAN";
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Немає жодного клієнта");
                }
            }
            clientContactsDataGridView.Update();
            clientBankDetailsDataGridView.Update();
            clientContactsDataGridView.Visible = true;
            clientBankDetailsDataGridView.Visible = true;
            showClientDeleteButton.Enabled = true;
        }

        void ShowClientSearch()
        {

            clientContactsDataGridView.Visible = false;
            clientBankDetailsDataGridView.Visible = false;
            clientCommentRichTextBox.Text = "";

            var text = clientShowSearchTextBox.Text;
            using (var db = new AtlantSovtContext())
            {
                var query =
                from c in db.Clients
                where c.Name.Contains(text) || c.Director.Contains(text) || c.ClientContacts.Any(con => con.TelephoneNumber.Contains(text)) || c.ClientContacts.Any(con => con.Email.Contains(text)) || c.ClientContacts.Any(con => con.ContactPerson.Contains(text))
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

        #endregion

        //Add
        #region Add

        void AddClient()
        {
            using (var db = new AtlantSovtContext())
            {
                if (nameClientTextBox.Text != "" || directorClientTextBox.Text != "")
                {
                    var new_Name = nameClientTextBox.Text;
                    var new_Director = directorClientTextBox.Text;
                    var new_PhysicalAddress = physicalAddressClientTextBox.Text;
                    var new_GeografphyAddress = geographyAddressClientTextBox.Text;
                    
                    var new_Comment = commentClientTextBox.Text;

                    long new_WorkDocumentId = 0;
                    long new_TaxPayerStatusId = 0;
                    if (clientWorkDocument != null)
                    {
                        clientIsWorkDocumentExist = true;
                        new_WorkDocumentId = clientWorkDocument.Id;
                    }
                    else
                    {
                        clientIsWorkDocumentExist = false;
                    }
                    if (clientTaxPayerStatus != null)
                    {
                        clientIsTaxPayerStatusExist = true;
                        new_TaxPayerStatusId = clientTaxPayerStatus.Id;
                    }
                    else
                    {
                        clientIsTaxPayerStatusExist = false;
                    }

                    Client New_Client = new Client();

                    if (!clientIsWorkDocumentExist && !clientIsTaxPayerStatusExist)
                    {
                        New_Client = new Client
                        {
                            Name = new_Name,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeografphyAddress = new_GeografphyAddress,
                            Comment = new_Comment,
                        };
                    }

                    else if (clientIsWorkDocumentExist && clientIsTaxPayerStatusExist)
                    {
                        New_Client = new Client
                        {
                            Name = new_Name,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeografphyAddress = new_GeografphyAddress,
                            WorkDocumentId = new_WorkDocumentId,
                            TaxPayerStatusId = new_TaxPayerStatusId,
                            Comment = new_Comment,
                        };
                    }

                    else if (clientIsWorkDocumentExist && !clientIsTaxPayerStatusExist)
                    {
                        New_Client = new Client
                        {
                            Name = new_Name,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeografphyAddress = new_GeografphyAddress,
                            WorkDocumentId = new_WorkDocumentId,
                            Comment = new_Comment,
                        };
                    }

                    else if (!clientIsWorkDocumentExist && clientIsTaxPayerStatusExist)
                    {
                        New_Client = new Client
                        {
                            Name = new_Name,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeografphyAddress = new_GeografphyAddress,
                            TaxPayerStatusId = new_TaxPayerStatusId,
                            Comment = new_Comment,
                        };
                    }
                    try
                    {
                        db.Clients.Add(New_Client);
                        db.SaveChanges();
                        string borodakyrka = "Клієнт успішно доданий ["+ New_Client.Id + "]\n";                        

                        if (addClientBankDetailsAddForm != null)
                        {
                            borodakyrka += addClientBankDetailsAddForm.AddClientBankDetail(New_Client.Id, true);
                            addClientBankDetailsAddForm = null;
                        }
                        if (addClientContactAddForm != null)
                        {
                            borodakyrka += addClientContactAddForm.AddClientContact(New_Client.Id, true);
                            addClientContactAddForm = null;
                        }
                        MessageBox.Show(borodakyrka);
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
                if (taxPayerStatusClientComboBox.Text != "")
                {
                    string comboboxText = taxPayerStatusClientComboBox.SelectedItem.ToString();
                    string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedStatus[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    clientTaxPayerStatus = db.TaxPayerStatus.Find(id);
                }
                else 
                {
                    clientTaxPayerStatus = null;
                }
            }
        }
        
        void SplitLoadWorkDocumentClientAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                if (workDocumentClientComboBox.Text != "")
                {
                    string comboboxText = workDocumentClientComboBox.SelectedItem.ToString();
                    string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedStatus[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    clientWorkDocument = db.WorkDocuments.Find(id);
                }
                else 
                {
                    clientWorkDocument = null;
                }
            }
        }
        #endregion

        //Update
        #region Update

        void ClearAllBoxesClientUpdate()
        {
            workDocumentClientUpdateComboBox.Items.Clear();
            taxPayerStatusClientUpdateComboBox.Items.Clear();
            nameClientUpdateTextBox.Clear();
            directorClientUpdateTextBox.Clear();
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
                    nameClientUpdateTextBox.Text = Convert.ToString(client.Name);
                    directorClientUpdateTextBox.Text = Convert.ToString(client.Director);
                    physicalAddressClientUpdateTextBox.Text = Convert.ToString(client.PhysicalAddress);
                    geographyAddressClientUpdateTextBox.Text = Convert.ToString(client.GeografphyAddress);
                    commentClientUpdateTextBox.Text = Convert.ToString(client.Comment);

                    if (client.WorkDocument != null)
                    {
                        workDocumentClientUpdateComboBox.SelectedIndex = workDocumentClientUpdateComboBox.FindString(client.WorkDocument.Status + " [" + client.WorkDocument.Id+']');      
                    }
                    else 
                    {
                        workDocumentClientUpdateComboBox.Text = "";
                        workDocumentClientUpdateComboBox.SelectedIndex = -1;
                    }
                    if (client.TaxPayerStatu != null)
                    {
                        taxPayerStatusClientUpdateComboBox.SelectedIndex = taxPayerStatusClientUpdateComboBox.FindString(client.TaxPayerStatu.Status+ " [" + client.TaxPayerStatu.Id + ']');
                    }
                    else 
                    {
                        taxPayerStatusClientUpdateComboBox.Text = "";
                        taxPayerStatusClientUpdateComboBox.SelectedIndex = -1;
                    }
                    
                }
                clientNameChanged =  clientDirectorChanged = clientPhysicalAddressChanged =  clientGeographyAddressChanged =  clientCommentChanged = clientWorkDocumentChanged =  clientTaxPayerStatusChanged =  clientOriginalChanged =  clientFaxChanged = false;
            }
        }

        void LoadSelectClientUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (selectClientDiapasoneUpdateComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = selectClientDiapasoneUpdateComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        selectClientUpdateComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadDiasoneClientUpdateInfoCombobox()
        {
            selectClientDiapasoneUpdateComboBox.Items.Clear();
            selectClientUpdateComboBox.Items.Clear();
            selectClientUpdateComboBox.Text = "";
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
                        selectClientDiapasoneUpdateComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    selectClientDiapasoneUpdateComboBox.DroppedDown = true;
                    selectClientUpdateComboBox.Enabled = true;

                }
                else 
                {
                    MessageBox.Show("Немає жодних записів");
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
                if (workDocumentClientUpdateComboBox.Text != "")
                {
                    string comboboxText = workDocumentClientUpdateComboBox.SelectedItem.ToString();
                    string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedStatus[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    clientWorkDocument = db.WorkDocuments.Find(id);
                }
                else 
                {
                    clientWorkDocument = null;
                }
            }
        }

        void SplitLoadTaxPayerStatusClientUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                if (taxPayerStatusClientUpdateComboBox.Text != "")
                {
                    string comboboxText = taxPayerStatusClientUpdateComboBox.SelectedItem.ToString();
                    string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedStatus[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    clientTaxPayerStatus = db.TaxPayerStatus.Find(id);
                }
                else
                {
                    clientTaxPayerStatus = null;
                }
            }
        }

        void UpdateClient()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (clientNameChanged || clientDirectorChanged || clientPhysicalAddressChanged || clientGeographyAddressChanged || clientCommentChanged || clientWorkDocumentChanged || clientTaxPayerStatusChanged || clientWorkDocumentChanged || clientTaxPayerStatusChanged || clientOriginalChanged || clientFaxChanged)
                {
                    if (clientNameChanged)
                    {
                        client.Name = nameClientUpdateTextBox.Text;
                    }
                    if (clientDirectorChanged)
                    {
                        client.Director = directorClientUpdateTextBox.Text;
                    }
   
                    if (clientPhysicalAddressChanged)
                    {
                        client.PhysicalAddress = physicalAddressClientUpdateTextBox.Text;
                    }
                    if (clientGeographyAddressChanged)
                    {
                        client.GeografphyAddress = geographyAddressClientUpdateTextBox.Text;
                    }
                    if (clientCommentChanged)
                    {
                        client.Comment = commentClientUpdateTextBox.Text;
                    }
                    if (clientWorkDocumentChanged)
                    {
                        if (workDocumentClientUpdateComboBox.Text != "")
                        {
                            client.WorkDocument = null;
                            client.WorkDocumentId = clientWorkDocument.Id;
                        }
                        else
                        {
                            client.WorkDocumentId = null;
                            client.WorkDocument = null;
                        }
                    }
                    if (clientTaxPayerStatusChanged)
                    {
                        if (taxPayerStatusClientUpdateComboBox.Text != "")
                        {
                            client.TaxPayerStatu = null;
                            client.TaxPayerStatusId = clientTaxPayerStatus.Id;
                        }
                        else 
                        {
                            client.TaxPayerStatusId = null;
                            client.TaxPayerStatu = null;
                        }
                    }
                    if (clientOriginalChanged)
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

        // Contact
        #region Contact

        void AddNewContact()
        {
            if (client != null)
            {
                updateClientContactAddForm.AddClientContact2(client.Id);
                updateClientContactAddForm = null;
            }
        }
        
        void UpdateContact()
        {
            if (client != null) 
            {
                updateClientContactUpdateForm.UpdateContact(client);
            }           
        }

        void DeleteContact()
        {
            if (client != null)
            {
                deleteClientContactDeleteForm.DeleteContact(client);
            }
        }

        #endregion

        #endregion

        //Delete
        #region Delete

        void DeleteClient(int id)
        {
            using (var db = new AtlantSovtContext())
            {
                deleteClient = db.Clients.Find(id);
                if (deleteClient != null)
                {
                    if (MessageBox.Show("Видалити клієнта " + deleteClient.Name + "?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.Clients.Attach(deleteClient);
                            db.Clients.Remove(deleteClient);
                            db.SaveChanges();
                            MessageBox.Show("Клієнт успішно видалений");
                            deleteClientComboBox.Items.Remove(deleteClientComboBox.SelectedItem);
                        }
                        catch(Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show("Помилка!" + Environment.NewLine + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Виберіть клієнта");
                }
            }
        }

        void LoadClientDeleteInfoComboBox()
        {
            if (deleteClientSelectDiapasoneComboBox.Text == "")
            {
                MessageBox.Show("Ви не вибрали діапазон");
            }
            else
            {
                string text = deleteClientSelectDiapasoneComboBox.SelectedItem.ToString();
                string[] diapasone = text.Split(new char[] { ' ' });
                int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                int diapasoneTo = Convert.ToInt32(diapasone[2]);
                using (var db = new AtlantSovtContext())
                {
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        deleteClientComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadDiasoneClientDeleteInfoCombobox()
        {
            deleteClientSelectDiapasoneComboBox.Items.Clear();
            deleteClientComboBox.Items.Clear();
            deleteClientComboBox.Text = "";
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
                        deleteClientSelectDiapasoneComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    deleteClientSelectDiapasoneComboBox.DroppedDown = true;
                    deleteClientComboBox.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                    deleteClientComboBox.Enabled = false;
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

        #endregion
    }
}
