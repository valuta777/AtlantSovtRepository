using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    public partial class MainForm : Form
    {
        public MainForm()
        {            
            InitializeComponent();
        }
            //Client Forms

            //Contact
        ClientContactAddForm addClientContactAddForm, updateClientContactAddForm;
        ClientContactUpdateForm updateClientContactUpdateForm;
        ClientContactDeleteForm deleteClientContactDeleteForm;
            //Bank Details
        ClientBankDetailsAddForm addClientBankDetailsAddForm, updateClientBankDetailsAddForm;
        ClientBankDetailsUpdateForm updateClientBankDetailsUpdateForm;
        
            //Forwarder Forms

            //Contact
        ForwarderContactAddForm addForwarderContactAddForm, updateForwarderContactAddForm;
        ForwarderContactUpdateForm updateForwarderContactUpdateForm;
        ForwarderContactDeleteForm deleteForwarderContactDeleteForm;       
            //Bank Details
        ForwarderBankDetailsAddForm addForwarderBankDetailsAddForm, updateForwarderBankDetailsAddForm;
        ForwarderBankDetailsUpdateForm updateForwarderBankDetailsUpdateForm;

            //Transporter Forms
        TransporterShowAdditionalDetailsForm transporterShowAdditionalDetailsForm;
        TransporterShowFiltrationForm transporterShowFiltrationForm;
            //Transporter Forms

            //Countries and Vehicles
        TransporterCountryAndVehicleSelectForm transporterCountryAndVehicleSelectForm;
            //Contact
        TransporterContactAddForm addTransporterContactAddForm, updateTransporterContactAddForm;
        //ForwarderContactUpdateForm updateForwarderContactUpdateForm;
        //ForwarderContactDeleteForm deleteForwarderContactDeleteForm;
            //Bank Details
        TransporterBankDetailsAddForm addTransporterBankDetailsAddForm, updateTransporterBankDetailsAddForm;
        //ForwarderBankDetailsUpdateForm updateForwarderBankDetailsUpdateForm;


        //Load / Animaton / Test connection
        #region Load
        void Connecting()
        {
            Thread animationThread = new Thread(new ThreadStart(PlayAnimation));
            animationThread.Start();
            using (var db = new AtlantSovtContext())
            {
                var query =
                from testConnection in db.WorkDocuments
                select testConnection;
                db.SaveChanges();
            }
            Thread.Sleep(3000);
            if(animationThread.IsAlive) 
            animationThread.Abort();
            animationThread.Join(500);
        }

        void PlayAnimation()
        {
            ConnectionForm connectionForm = new ConnectionForm();
            connectionForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Connecting();
        }
        #endregion

        //MenuStrips
        #region MenuStrips               

                private void addClientsStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 2;
                }

                private void updateClientsStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 3;
                }

                private void deleteClientsStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 4;
                }               

                private void addForwarderStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 6;
                }

                private void updateForwarderStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 7;
                }

                private void deleteForwarderStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 8;
                }

                private void addTransporterStrip_Click(object sender, EventArgs e)
                {
                     dataControl.SelectedIndex = 10;
                }

                private void updateTransporterStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 11;
                }

                private void deleteTransporterStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 12;
                }
                #endregion

        //Client
        #region Client
                //Show
                #region Show
        private void showClientsStrip_Click(object sender, EventArgs e)
        {
            dataControl.SelectedIndex = 1;
            ShowClient();
            clientContactsDataGridView.Visible = false;
            clientBankDetailsDataGridView.Visible = false;
            clientCommentRichTextBox.Text = "";
        }

        private void clientDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowClientInfo();
        }
                #endregion

                //Add
                #region Add

        private void addWorkDocumentClientButton_Click(object sender, EventArgs e)
        {
            AddWorkDocumentForm addWorkDocument = new AddWorkDocumentForm();
            addWorkDocument.Show();
        }
        
        private void addTaxPayerStatusClientButton_Click(object sender, EventArgs e)
        {
            AddTaxPayerStatusForm addTaxPayerStatus = new AddTaxPayerStatusForm();
            addTaxPayerStatus.Show();
        }
        
        private void addContactClientButton_Click(object sender, EventArgs e)
        {
            addClientContactAddForm = new ClientContactAddForm();
            addClientContactAddForm.Show();
        }
        
        private void addBankDetailsClientButton_Click(object sender, EventArgs e)
        {
            addClientBankDetailsAddForm = new ClientBankDetailsAddForm();
            addClientBankDetailsAddForm.Show();
        }
        
        private void addClientButton_Click(object sender, EventArgs e)
        {
            AddClient();
            nameClientTextBox.Text = "";
            directorClientTextBox.Text = "";
            physicalAddressClientTextBox.Text = "";
            geographyAddressClientTextBox.Text = "";
            workDocumentClientComboBox.Text = "";
            taxPayerStatusClientComboBox.Text = "";
            originalClientCheckBox.Checked = false;
            geographyAddressClientTextBox.Text = "";
            commentClientTextBox.Text = "";
            
        }
        
        private void workDocumentClientComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            workDocumentClientComboBox.Items.Clear();
            LoadWorkDocumentClientAddInfoComboBox();
            workDocumentClientComboBox.DroppedDown = true;
        }
        
        private void taxPayerStatusClientComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            taxPayerStatusClientComboBox.Items.Clear();
            LoadTaxPayerStatusClientAddInfoComboBox();
            taxPayerStatusClientComboBox.DroppedDown = true;
        }
        
        private void workDocumentClientAddComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientWorkDocumentFlag = true;
            SplitLoadWorkDocumentClientAddInfo();
            
        }
        
        private void TaxPayerStatusClientAddComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientTaxPayerStatusFlag = true;
            SplitLoadTaxPayerStatusClientAddInfo();
        }

        private void originalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (originalClientCheckBox.Checked)
            {
                faxClientCheckBox.CheckState = CheckState.Unchecked;
            }
            else
            {
                faxClientCheckBox.CheckState = CheckState.Checked;
            }
        }

        private void faxCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (faxClientCheckBox.Checked)
            {
                originalClientCheckBox.CheckState = CheckState.Unchecked;
            }
            else
            {
                originalClientCheckBox.CheckState = CheckState.Checked;
            }
        }
        #endregion

                //Update 
                #region Update

        private void selectClientUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllBoxesClientUpdate();
            LoadWorkDocumentClientUpdateInfoComboBox();
            LoadTaxPayerStatusClientUpdateInfoComboBox();
            SplitUpdateClient();
        }

        private void selectClientUpdateComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            selectClientUpdateComboBox.Items.Clear();
            LoadSelectClientUpdateInfoComboBox();
            selectClientUpdateComboBox.DroppedDown = true;
        }

        private void workDocumentClientUpdateComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            workDocumentClientUpdateComboBox.Items.Clear();
            LoadWorkDocumentClientUpdateInfoComboBox();
            workDocumentClientUpdateComboBox.DroppedDown = true;

        }

        private void taxPayerStatusClientUpdateComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            taxPayerStatusClientUpdateComboBox.Items.Clear();
            LoadTaxPayerStatusClientUpdateInfoComboBox();
            taxPayerStatusClientUpdateComboBox.DroppedDown = true;

        }

        private void originalClientUpdateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
            if (originalClientUpdateCheckBox.Checked)
            {
                faxClientUpdateCheckBox.CheckState = CheckState.Unchecked;
                clientOriginalChanged = true;
                clientFaxChanged = false;
            }
            else
            {
                faxClientUpdateCheckBox.CheckState = CheckState.Checked;
            }
        }

        private void faxClientUpdateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
            if (faxClientUpdateCheckBox.Checked)
            {
                originalClientUpdateCheckBox.CheckState = CheckState.Unchecked;
                clientOriginalChanged = false;
                clientFaxChanged = true;
            }
            else
            {
                originalClientUpdateCheckBox.CheckState = CheckState.Checked;

            }
        }

        private void workDocumentClientUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientWorkDocumentChanged = true;
            SplitLoadWorkDocumentClientUpdateInfo();
           
        }

        private void taxPayerStatusClientUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientTaxPayerStatusChanged = true;
            SplitLoadTaxPayerStatusClientUpdateInfo();
        }

        private void clientUpdateWorkDocumentAddButton_Click(object sender, EventArgs e)
        {
            AddWorkDocumentForm addWorkDocument = new AddWorkDocumentForm();
            addWorkDocument.Show();
        }

        private void clientUpdateTaxPayerStatusAddButton_Click(object sender, EventArgs e)
        {
            AddTaxPayerStatusForm addTaxPayerStatus = new AddTaxPayerStatusForm();
            addTaxPayerStatus.Show();
        }

        private void nameClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            clientNameChanged = true;
        }

        private void directorClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            clientDirectorChanged = true;
        }

        private void contractNumberClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            clientContractNumberChanged = true;
        }

        private void physicalAddressClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            clientPhysicalAddressChanged = true;
        }

        private void geographyAddressClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            clientGeographyAddressChanged = true;
        }

        private void commentClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            clientCommentChanged = true;
        }

        private void updateClientButton_Click(object sender, EventArgs e)
        {
            UpdateClient();
        }

                //Contact
                #region Contact
        //Add
        private void clientUpdateAddContactButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (client != null)
                {
                    updateClientContactAddForm = new ClientContactAddForm();
                    updateClientContactAddForm.Show();
                    AddNewContact();
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку клієнта");
                }
            }
        }
        
        //Update
        private void updateClientContactButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (client != null)
                {
                    if (db.Clients.Find(client.Id).ClientContacts.Count != 0)
                    {
                        updateClientContactUpdateForm = new ClientContactUpdateForm();
                        updateClientContactUpdateForm.Show();
                        UpdateContact();
                    }
                    else
                    {
                        MessageBox.Show("В клієнта немає контактів");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку клієнта");
                }
            }
        }
        
        //Delete
        private void clientUpdateContactDeleteButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (client != null)
                {
                    if (db.Clients.Find(client.Id).ClientContacts.Count != 0)
                    {
                        deleteClientContactDeleteForm = new ClientContactDeleteForm();
                        deleteClientContactDeleteForm.Show();
                        DeleteContact();
                    }
                    else
                    {
                        MessageBox.Show("В клієнта немає контактів");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку клієнта");
                }
            }
        }
        #endregion

                //Bank Details
                #region Bank Details
        //Add
        private void clientUpdateAddClientBankDetailsButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (client != null)
                {
                    if (db.Clients.Find(client.Id).ClientBankDetail == null)
                    {
                        updateClientBankDetailsAddForm = new ClientBankDetailsAddForm();
                        updateClientBankDetailsAddForm.Show();
                        updateClientBankDetailsAddForm.AddClientBankDetail2(client.Id);
                    }
                    else
                    {
                        MessageBox.Show("В клієнта вже є банківські данні");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку клієнта");
                }
            }
        }

        //Update
        private void clientUpdateBankDetailsUpdateButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (client != null)
                {
                    if (db.Clients.Find(client.Id).ClientBankDetail != null)
                    {
                        updateClientBankDetailsUpdateForm = new ClientBankDetailsUpdateForm();
                        updateClientBankDetailsUpdateForm.Show();
                        updateClientBankDetailsUpdateForm.UpdateBankDetails(db.Clients.Find(client.Id).ClientBankDetail);
                    }
                    else
                    {
                        MessageBox.Show("В клієнта ще немає банківських данни");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку клієнта");
                }
            }
        }

        //Delete
        private void clientUpdateBankDetaitsDeleteButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (client != null)
                {
                    if (db.Clients.Find(client.Id).ClientBankDetail != null)
                    {
                        if (MessageBox.Show("Видалити банківські данні клієнту " + client.Name + " ?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                db.ClientBankDetails.Remove(db.Clients.Find(client.Id).ClientBankDetail);
                                db.SaveChanges();
                                MessageBox.Show("Банківські данні успішно видалено");
                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show("Помилка!" + Environment.NewLine + ee);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("В клієнта ще немає банківських данних");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку клієнта");
                }
            }
        }
        #endregion 

                #endregion

                //Delete
                #region Delete
        private void deleteClientButton_Click(object sender, EventArgs e)
        {
            deleteClientComboBox.Text = "";
            deleteClientComboBox.Items.Clear();
            deleteClientButton.Enabled = false;
            DeleteClient();
        }

        private void deleteClientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplitDeleteClient();
        }

        private void deleteClientComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            deleteClientComboBox.Items.Clear();
            LoadClientDeleteInfoComboBox();
            deleteClientComboBox.DroppedDown = true;
            if (deleteClientComboBox.Items.Count == 0)
            {
                deleteClientButton.Enabled = false;
            }
            else
            {
                deleteClientButton.Enabled = true;

            }
        }

           
        #endregion        

        #endregion

        //Forwarder
        #region Forwarder

                //Show
                #region Show
        private void showForwarderStrip_Click(object sender, EventArgs e)
        {
            dataControl.SelectedIndex = 5;
            ShowForwarder();
            forwarderContactsDataGridView.Visible = false;
            forwarderBankDetailsDataGridView.Visible = false;
            forwarderCommentRichTextBox.Text = "";
        }
        private void forwarderDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowForwarderInfo();
        }
        #endregion

                //Add
                #region Add
        private void workDocumentForwarderComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            workDocumentForwarderComboBox.Items.Clear();
            LoadWorkDocumentForwarderAddInfoComboBox();
            workDocumentForwarderComboBox.DroppedDown = true;
        }

        private void workDocumentForwarderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            forwarderWorkDocumentAdded = true;
            SplitLoadWorkDocumentForwarderAddInfo();
        }

        private void taxPayerStatusForwarderComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            taxPayerStatusForwarderComboBox.Items.Clear();
            LoadTaxPayerStatusForwarderAddInfoComboBox();
            taxPayerStatusForwarderComboBox.DroppedDown = true;
        }

        private void taxPayerStatusForwarderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            forwarderTaxPayerStatusAdded = true;
            SplitLoadTaxPayerStatusForwarderAddInfo();
        }

        private void addWorkDocumentForwarderButton_Click(object sender, EventArgs e)
        {
            AddWorkDocumentForm addWorkDocument = new AddWorkDocumentForm();
            addWorkDocument.Show();
        }

        private void addTaxPayerStatusForwarderButton_Click(object sender, EventArgs e)
        {
            AddTaxPayerStatusForm addTaxPayerStatus = new AddTaxPayerStatusForm();
            addTaxPayerStatus.Show();
        }

        private void addContactForwarderButton_Click(object sender, EventArgs e)
        {
            addForwarderContactAddForm = new ForwarderContactAddForm();
            addForwarderContactAddForm.Show();
        }

        private void addBankDetailsForwarderButton_Click(object sender, EventArgs e)
        {
            addForwarderBankDetailsAddForm = new ForwarderBankDetailsAddForm();
            addForwarderBankDetailsAddForm.Show();
        }

        private void addForwarderButton_Click(object sender, EventArgs e)
        {
            AddForwarder();
            nameForwarderTextBox.Text = "";
            directorForwarderTextBox.Text = "";
            physicalAddressForwarderTextBox.Text = "";
            geographyAddressForwarderTextBox.Text = "";
            workDocumentForwarderComboBox.Text = "";
            taxPayerStatusForwarderComboBox.Text = "";
            geographyAddressForwarderTextBox.Text = "";
            commentForwarderTextBox.Text = "";
        }
        #endregion

                //Update
                #region Update
        private void selectForwarderUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllBoxesForwarderUpdate();
            LoadWorkDocumentForwarderUpdateInfoComboBox();
            LoadTaxPayerStatusForwarderUpdateInfoComboBox();
            SplitUpdateForwarder();
        }

        private void selectForwarderUpdateComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            selectForwarderUpdateComboBox.Items.Clear();
            LoadSelectForwarderUpdateInfoComboBox();
            selectForwarderUpdateComboBox.DroppedDown = true;
        }

        private void workDocumentForwarderUpdateComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            workDocumentForwarderUpdateComboBox.Items.Clear();
            LoadWorkDocumentForwarderUpdateInfoComboBox();
            workDocumentForwarderUpdateComboBox.DroppedDown = true;
        }

        private void taxPayerStatusForwarderUpdateComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            taxPayerStatusForwarderUpdateComboBox.Items.Clear();
            LoadTaxPayerStatusForwarderUpdateInfoComboBox();
            taxPayerStatusForwarderUpdateComboBox.DroppedDown = true;
        }
        
        private void workDocumentForwarderUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            forwarderWorkDocumentChanged = true;
            SplitLoadWorkDocumentForwarderUpdateInfo();

        }

        private void taxPayerStatusForwarderUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            forwarderTaxPayerStatusChanged = true;
            SplitLoadTaxPayerStatusForwarderUpdateInfo();
        }
      
        private void forwarderUpdateWorkDocumentAddButton_Click(object sender, EventArgs e)
        {
            AddWorkDocumentForm addWorkDocument = new AddWorkDocumentForm();
            addWorkDocument.Show();
        }

        private void forwarderUpdateTaxPayerStatusAddButton_Click(object sender, EventArgs e)
        {
            AddTaxPayerStatusForm addTaxPayerStatus = new AddTaxPayerStatusForm();
            addTaxPayerStatus.Show();
        }

        private void nameForwarderUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            forwarderNameChanged = true;
        }

        private void directorForwarderUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            forwarderDirectorChanged = true;
        }

        private void physicalAddressForwarderUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            forwarderPhysicalAddressChanged = true;
        }

        private void geographyAddressForwarderUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            forwarderGeographyAddressChanged = true;
        }

        private void commentForwarderUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            forwarderCommentChanged = true;
        }

        private void updateForwarderButton_Click(object sender, EventArgs e)
        {
            UpdateForwarder();
        }
        //Contact
        #region Contact

        //Add
        private void forwarderUpdateContactAddButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (forwarder != null)
                {
                    updateForwarderContactAddForm = new ForwarderContactAddForm();
                    updateForwarderContactAddForm.Show();
                    AddNewForwarderContact();
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку експедитора");
                }
            }
        }

        //Update
        private void forwarderUpdateContactUpdateButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (forwarder != null)
                {
                    if (db.Forwarders.Find(forwarder.Id).ForwarderContacts.Count != 0)
                    {
                        updateForwarderContactUpdateForm = new ForwarderContactUpdateForm();
                        updateForwarderContactUpdateForm.Show();
                        UpdateForwarderContact();
                    }
                    else
                    {
                        MessageBox.Show("В експедитора немає контактів");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку експедитора");
                }
            }
        }

        //Delete
        private void forwarderUpdateContactDeleteButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (forwarder != null)
                {
                    if (db.Forwarders.Find(forwarder.Id).ForwarderContacts.Count != 0)
                    {
                        deleteForwarderContactDeleteForm = new ForwarderContactDeleteForm();
                        deleteForwarderContactDeleteForm.Show();
                        DeleteForwarderContact();
                    }
                    else
                    {
                        MessageBox.Show("В експедитора немає контактів");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку експедитора");
                }
            }
        }
        #endregion

        //Bank Details
        #region Bank Details
        //Add
        private void forwarderUpdateClientBankDetailsAddButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (forwarder != null)
                {
                    if (db.Forwarders.Find(forwarder.Id).ForwarderBankDetail == null)
                    {
                        updateForwarderBankDetailsAddForm = new ForwarderBankDetailsAddForm();
                        updateForwarderBankDetailsAddForm.Show();
                        updateForwarderBankDetailsAddForm.AddForwarderBankDetail2(forwarder.Id);
                    }
                    else
                    {
                        MessageBox.Show("В експедитора вже є банківські данні");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку експедитора");
                }
            }
        }

        //Update
        private void forwarderUpdateBankDetailsUpdateButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (forwarder != null)
                {
                    if (db.Forwarders.Find(forwarder.Id).ForwarderBankDetail != null)
                    {
                        updateForwarderBankDetailsUpdateForm = new ForwarderBankDetailsUpdateForm();
                        updateForwarderBankDetailsUpdateForm.Show();
                        updateForwarderBankDetailsUpdateForm.UpdateForwarderBankDetails(db.Forwarders.Find(forwarder.Id).ForwarderBankDetail);
                    }
                    else
                    {
                        MessageBox.Show("В експедитора ще немає банківських данних");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку експедитора");
                }
            }
        }

        //Delete
        private void forwarderUpdateBankDetaitsDeleteButton_Click(object sender, EventArgs e)
        {
            using (var db = new AtlantSovtContext())
            {
                if (forwarder != null)
                {
                    if (db.Forwarders.Find(forwarder.Id).ForwarderBankDetail != null)
                    {
                        if (MessageBox.Show("Видалити банківські данні експедитору " + forwarder.Name + " ?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                db.ForwarderBankDetails.Remove(db.Forwarders.Find(forwarder.Id).ForwarderBankDetail);
                                db.SaveChanges();
                                MessageBox.Show("Банківські данні успішно видалено");
                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show("Помилка!" + Environment.NewLine + ee);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("В експедитора ще немає банківських данних");
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть спочатку експедитора");
                }
            }
        }
        #endregion 

                #endregion


                //Delete
                #region Delete

        private void forwarderDeleteButton_Click(object sender, EventArgs e)
        {
            DeleteForwarder();
            forwarderDeleteComboBox.Text = "";
            forwarderDeleteComboBox.Items.Clear();
            forwarderDeleteButton.Enabled = false;

        }

        private void forwarderDeleteComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplitDeleteForwarder();
        }

        private void forwarderDeleteComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            forwarderDeleteComboBox.Items.Clear();
            LoadForwarderDeleteInfoComboBox();
            forwarderDeleteComboBox.DroppedDown = true;
            if(forwarderDeleteComboBox.Items.Count == 0)
            {
                forwarderDeleteButton.Enabled = false;
            }
            else
            {
                forwarderDeleteButton.Enabled = true;

            }
        }
                #endregion

        #endregion

        //Transporter
        #region Transporter

            //Show
            #region Show

            private void showTransporterStrip_Click(object sender, EventArgs e)
            {
                dataControl.SelectedIndex = 9;
                ShowTransporter();
                transporterShowContactsDataGridView.Visible = false;
                transporterShowBankDetailsDataGridView.Visible = false;
                transporterShowCountryDataGridView.Visible = false;
                transporterShowCountryDataGridView.Visible = false;
                transporterShowCommentRichTextBox.Text = "";
            }

            private void transporterShowDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
            {
                ShowTransporterInfo();
            }

            private void transporterShowAdditionalDetailsButton_Click(object sender, EventArgs e)
            {
                transporterShowAdditionalDetailsForm = new TransporterShowAdditionalDetailsForm();
                if (TransporterClikedId != 0)
                {
                    transporterShowAdditionalDetailsForm.ShowTransporterAdditionalDetails(TransporterClikedId);
                    transporterShowAdditionalDetailsForm.Show();

                }
                else
                {
                    MessageBox.Show("Ви не вибрали перевізника!");
                    transporterShowAdditionalDetailsForm.Hide();
                }
            }

            private void transporterShowFilterSelectButton_Click(object sender, EventArgs e)
            {
                transporterShowFiltrationForm = new TransporterShowFiltrationForm(this);

                transporterShowFiltrationForm.LoadCoutriesToTransporterShowChechedBoxList();

                transporterShowFiltrationForm.LoadVehicleToTransporterShowChechedBoxList();

                transporterShowFiltrationForm.Show();
            }
            #endregion

            //Add
            #region Add

        private void transporterAddWorkDocumentAddButton_Click(object sender, EventArgs e)
        {
            AddWorkDocumentForm addWorkDocument = new AddWorkDocumentForm();
            addWorkDocument.Show();
        }

        private void transporterAddTaxPayerStatusAddButton_Click(object sender, EventArgs e)
        {
            AddTaxPayerStatusForm addTaxPayerStatus = new AddTaxPayerStatusForm();
            addTaxPayerStatus.Show();
        }

        private void transporterAddContactAddButton_Click(object sender, EventArgs e)
        {
            addTransporterContactAddForm = new TransporterContactAddForm();
            addTransporterContactAddForm.Show();
        }

        private void transporterAddBankDetailsAddButton_Click(object sender, EventArgs e)
        {
            addTransporterBankDetailsAddForm = new TransporterBankDetailsAddForm();
            addTransporterBankDetailsAddForm.Show();
        }

        private void transporterAddCountryAndVehicleSelectButton_Click(object sender, EventArgs e)
        {
            transporterCountryAndVehicleSelectForm = new TransporterCountryAndVehicleSelectForm();
            transporterCountryAndVehicleSelectForm.Show();
        }

        private void workDocumentTransporterAddComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            workDocumentTransporterAddComboBox.Items.Clear();
            LoadWorkDocumentTransporterAddInfoComboBox();
            workDocumentTransporterAddComboBox.DroppedDown = true;
        }

        private void taxPayerStatusTransporterAddComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            taxPayerStatusTransporterAddComboBox.Items.Clear();
            LoadTaxPayerStatusTransporterAddInfoComboBox();
            taxPayerStatusTransporterAddComboBox.DroppedDown = true;
        }

        private void workDocumentTransporterAddComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            transporterAddWorkDocumentFlag = true;
            SplitLoadWorkDocumentTransporterAddInfo();

        }

        private void taxPayerStatusTransporterAddComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            transporterAddTaxPayerStatusFlag = true;
            SplitLoadTaxPayerStatusTransporterAddInfo();
        }

        private void originalTransporterAddCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (originalTransporterAddCheckBox.Checked)
            {
                faxTransporterAddCheckBox.CheckState = CheckState.Unchecked;
            }
            else
            {
                faxTransporterAddCheckBox.CheckState = CheckState.Checked;
            }
        }

        private void faxTransporterAddCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (faxTransporterAddCheckBox.Checked)
            {
                originalTransporterAddCheckBox.CheckState = CheckState.Unchecked;
            }
            else
            {
                originalTransporterAddCheckBox.CheckState = CheckState.Checked;
            }
        }

        private void transporterAddButton_Click(object sender, EventArgs e)
        {
            AddTransporter();
            nameTransporterAddTextBox.Text = "";
            shortNameTransporterAddTextBox.Text = "";
            directorTransporterAddTextBox.Text = "";
            physicalAddressTransporterAddTextBox.Text = "";
            geographyAddressTransporterAddTextBox.Text = "";
            workDocumentTransporterAddComboBox.Text = "";
            taxPayerStatusTransporterAddComboBox.Text = "";
            originalTransporterAddCheckBox.Checked = true;

            commentTransporterAddTextBox.Text = "";
            filtersTransporterAddCheckedListBox.ClearSelected();
            foreach (int i in filtersTransporterAddCheckedListBox.CheckedIndices)
            {
                filtersTransporterAddCheckedListBox.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
            #endregion

            #region Update
            #endregion

            #region Delete
            #endregion

        #endregion


    }
}
