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
            showClientContactsDataGridView.Visible = false;
            showClientBankDetailsDataGridView.Visible = false;
            showClientDeleteButton.Enabled = false;
            showClientNoteRichTextBox.Text = "";

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
                    ContractType = (c.ContractType == true) ? AtlantSovt.Properties.Resources.Оригінал : AtlantSovt.Properties.Resources.Факс,
                    TaxPayerStatusId = c.TaxPayerStatu.Status,
                    WorkDocumentId = c.WorkDocument.Status,
                };

                showClientDataGridView.DataSource = query.ToList();
                showClientDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                showClientDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Назва;
                showClientDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.П_І_Б_Директора;
                showClientDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Фізична_адреса;
                showClientDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Юридична_адреса;
                showClientDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Стан_договору;
                showClientDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Статус_платника_податку;
                showClientDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.На_основі;


            } showClientDataGridView.Update();

        }

        void ShowClientInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    var ClikedId = Convert.ToInt32(showClientDataGridView.CurrentRow.Cells[0].Value);
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
                    showClientContactsDataGridView.DataSource = query.ToList();
                    showClientContactsDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Контактна_особа;
                    showClientContactsDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Телефон;
                    showClientContactsDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Факс;
                    showClientContactsDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Email;

                    var query1 =
                        from c in db.Clients
                        where c.Id == ClikedId
                        select c.Comment;

                    showClientNoteRichTextBox.Text = query1.FirstOrDefault();

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

                    showClientBankDetailsDataGridView.DataSource = query2.ToList();
                    showClientBankDetailsDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Назва_банку;
                    showClientBankDetailsDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.МФО;
                    showClientBankDetailsDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку;
                    showClientBankDetailsDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.ЄДРПОУ;
                    showClientBankDetailsDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.ІПН;
                    showClientBankDetailsDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Серія_свідоцтва;
                    showClientBankDetailsDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Номер_свідоцтва;
                    showClientBankDetailsDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.SWIFT;
                    showClientBankDetailsDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.IBAN;
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_клієнта);
                }
            }
            showClientContactsDataGridView.Update();
            showClientBankDetailsDataGridView.Update();
            showClientContactsDataGridView.Visible = true;
            showClientBankDetailsDataGridView.Visible = true;
            showClientDeleteButton.Enabled = true;
        }

        void ShowClientSearch()
        {

            showClientContactsDataGridView.Visible = false;
            showClientBankDetailsDataGridView.Visible = false;
            showClientNoteRichTextBox.Text = "";

            var text = showClientSearchTextBox.Text;
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


                showClientDataGridView.DataSource = query.ToList();
                showClientDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                showClientDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Назва;
                showClientDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.П_І_Б_Директора;
                showClientDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Фізична_адреса;
                showClientDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Юридична_адреса;
                showClientDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Оригінал_договору;
                showClientDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Статус_платника_податку;
                showClientDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.На_основі;


            } showClientDataGridView.Update();

        }

        #endregion

        //Add
        #region Add

        void AddClient()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addClientNameTextBox.Text != "" || addClientDirectorTextBox.Text != "")
                {
                    var new_Name = addClientNameTextBox.Text;
                    var new_Director = addClientDirectorTextBox.Text;
                    var new_PhysicalAddress = addClientPhysicalAddressTextBox.Text;
                    var new_GeografphyAddress = addClientGeographyAddressTextBox.Text;
                    
                    var new_Comment = addClientNoteTextBox.Text;

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
                        string borodakyrka = AtlantSovt.Properties.Resources.Клієнт_успішно_доданий + " [" + New_Client.Id + @"]";                      

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
                    MessageBox.Show(AtlantSovt.Properties.Resources.Одне_з_обов_язкових_полів_не_заповнено);
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
                    addClientTaxPayerStatusComboBox.Items.Add(item.Status + " [" + item.Id + "]");
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
                    addClientWorkDocumentComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }
        
        void SplitLoadTaxPayerStatusClientAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addClientTaxPayerStatusComboBox.Text != "")
                {
                    string comboboxText = addClientTaxPayerStatusComboBox.SelectedItem.ToString();
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
                if (addClientWorkDocumentComboBox.Text != "")
                {
                    string comboboxText = addClientWorkDocumentComboBox.SelectedItem.ToString();
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
            updateClientWorkDocumentComboBox.Items.Clear();
            updateClientTaxPayerStatusComboBox.Items.Clear();
            updateClientNameTextBox.Clear();
            updateClientDirectorTextBox.Clear();
            updateClientPhysicalAddressTextBox.Clear();
            updateClientGeorgaphyAddressTextBox.Clear();
            updateClientNoteTextBox.Clear();
        }

        void SplitUpdateClient()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = updateClientSelectClientComboBox.SelectedItem.ToString();                
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                client = db.Clients.Find(id);
                if (client != null)
                {
                    updateClientNameTextBox.Text = Convert.ToString(client.Name);
                    updateClientDirectorTextBox.Text = Convert.ToString(client.Director);
                    updateClientPhysicalAddressTextBox.Text = Convert.ToString(client.PhysicalAddress);
                    updateClientGeorgaphyAddressTextBox.Text = Convert.ToString(client.GeografphyAddress);
                    updateClientNoteTextBox.Text = Convert.ToString(client.Comment);

                    if (client.WorkDocument != null)
                    {
                        updateClientWorkDocumentComboBox.SelectedIndex = updateClientWorkDocumentComboBox.FindString(client.WorkDocument.Status + " [" + client.WorkDocument.Id+']');      
                    }
                    else 
                    {
                        updateClientWorkDocumentComboBox.Text = "";
                        updateClientWorkDocumentComboBox.SelectedIndex = -1;
                    }
                    if (client.TaxPayerStatu != null)
                    {
                        updateClientTaxPayerStatusComboBox.SelectedIndex = updateClientTaxPayerStatusComboBox.FindString(client.TaxPayerStatu.Status+ " [" + client.TaxPayerStatu.Id + ']');
                    }
                    else 
                    {
                        updateClientTaxPayerStatusComboBox.Text = "";
                        updateClientTaxPayerStatusComboBox.SelectedIndex = -1;
                    }
                    
                }
                clientNameChanged =  clientDirectorChanged = clientPhysicalAddressChanged =  clientGeographyAddressChanged =  clientCommentChanged = clientWorkDocumentChanged =  clientTaxPayerStatusChanged =  clientOriginalChanged =  clientFaxChanged = false;
            }
        }

        void LoadSelectClientUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (updateClientSelectDiapasonComboBox.Text == "")
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
                }
                else
                {
                    string text = updateClientSelectDiapasonComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        updateClientSelectClientComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadDiasoneClientUpdateInfoCombobox()
        {
            updateClientSelectDiapasonComboBox.Items.Clear();
            updateClientSelectClientComboBox.Items.Clear();
            updateClientSelectClientComboBox.Text = "";
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
                        updateClientSelectDiapasonComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    updateClientSelectDiapasonComboBox.DroppedDown = true;
                    updateClientSelectClientComboBox.Enabled = true;

                }
                else 
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
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
                    updateClientWorkDocumentComboBox.Items.Add(item.Status + " [" + item.Id + "]");
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
                    updateClientTaxPayerStatusComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadWorkDocumentClientUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                if (updateClientWorkDocumentComboBox.Text != "")
                {
                    string comboboxText = updateClientWorkDocumentComboBox.SelectedItem.ToString();
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
                if (updateClientTaxPayerStatusComboBox.Text != "")
                {
                    string comboboxText = updateClientTaxPayerStatusComboBox.SelectedItem.ToString();
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
                        client.Name = updateClientNameTextBox.Text;
                    }
                    if (clientDirectorChanged)
                    {
                        client.Director = updateClientDirectorTextBox.Text;
                    }
   
                    if (clientPhysicalAddressChanged)
                    {
                        client.PhysicalAddress = updateClientPhysicalAddressTextBox.Text;
                    }
                    if (clientGeographyAddressChanged)
                    {
                        client.GeografphyAddress = updateClientGeorgaphyAddressTextBox.Text;
                    }
                    if (clientCommentChanged)
                    {
                        client.Comment = updateClientNoteTextBox.Text;
                    }
                    if (clientWorkDocumentChanged)
                    {
                        if (updateClientWorkDocumentComboBox.Text != "")
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
                        if (updateClientTaxPayerStatusComboBox.Text != "")
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
                    MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_змінено);
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Змін_не_знайдено);
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
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_клієнта + deleteClient.Name + "?", AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.Clients.Attach(deleteClient);
                            db.Clients.Remove(deleteClient);
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Клієнт_успішно_видалений);
                            deleteClientComboBox.Items.Remove(deleteClientComboBox.SelectedItem);
                        }
                        catch(Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + Environment.NewLine + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_клієнта);
                }
            }
        }

        void LoadClientDeleteInfoComboBox()
        {
            if (deleteClientSelectDiapasoneComboBox.Text == "")
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
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
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
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
