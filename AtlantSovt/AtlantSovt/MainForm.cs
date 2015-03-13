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

        AddContactClientForm addContactForm, ClientUpdateAddContactForm;
        AddContactForwarderForm addContactForwarderForm;
        AddClientBankDetails addClientBankDetailsForm, updateClientAddBankDetailsForm;
        AddForwarderBankDetails addForwarderBankDetailsForm;
        UpdateContactForm UpdateClientUpdateContactForm;
        UpdateClientBankDetailsForm updateClientBankDetailsForm;
        DeleteContactForm deleteContactForm;

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
                private void showClientsStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 1;
                    ShowClient();
                    clientContactsDataGridView.Visible = false;
                    clientBankDetailsDataGridView.Visible = false;
                    clientCommentRichTextBox.Text = "";
                }

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

                private void showForwarderStrip_Click(object sender, EventArgs e)
                {
                    dataControl.SelectedIndex = 5;
                    ShowForwarder();
                    forwarderContactsDataGridView.Visible = false;
                    forwarderBankDetailsDataGridView.Visible = false;
                    forwarderCommentRichTextBox.Text = "";
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
                #endregion

        //Client
        #region Client

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
            addContactForm = new AddContactClientForm();
            addContactForm.Show();
        }
        
        private void addBankDetailsClientButton_Click(object sender, EventArgs e)
        {
            addClientBankDetailsForm = new AddClientBankDetails();
            addClientBankDetailsForm.Show();
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
        
        private void clientDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowClientInfo();
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
                uc1 = true;
                uc2 = false;
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
                uc1 = false;
                uc2 = true;
            }
            else
            {
                originalClientUpdateCheckBox.CheckState = CheckState.Checked;

            }
        }

        private void workDocumentClientUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucb1 = true;
            SplitLoadWorkDocumentClientUpdateInfo();
           
        }

        private void taxPayerStatusClientUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucb2 = true;
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
            utb1 = true;
        }

        private void directorClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            utb2 = true;
        }

        private void contractNumberClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            utb3 = true;
        }

        private void physicalAddressClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            utb4 = true;
        }

        private void geographyAddressClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            utb5 = true;
        }

        private void commentClientUpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            utb6 = true;
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
                    ClientUpdateAddContactForm = new AddContactClientForm();
                    ClientUpdateAddContactForm.Show();
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
                        UpdateClientUpdateContactForm = new UpdateContactForm();
                        UpdateClientUpdateContactForm.Show();
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
                        deleteContactForm = new DeleteContactForm();
                        deleteContactForm.Show();
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
                        updateClientAddBankDetailsForm = new AddClientBankDetails();
                        updateClientAddBankDetailsForm.Show();
                        updateClientAddBankDetailsForm.AddClientBankDetail2(client.Id);
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
                        updateClientBankDetailsForm = new UpdateClientBankDetailsForm();
                        updateClientBankDetailsForm.Show();
                        updateClientBankDetailsForm.UpdateBankDetails(db.Clients.Find(client.Id).ClientBankDetail);
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
                        if (MessageBox.Show("Видалити банківські данні клієнту" + client.Name + "?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                        MessageBox.Show("В клієнта ще немає банківських данни");
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
        }

           
        #endregion        

        #endregion

        //Forwarder
        #region Forwarder   
                //Add
                #region Add

        private void forwarderDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowForwarderInfo();
        }

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
            addContactForwarderForm = new AddContactForwarderForm();
            addContactForwarderForm.Show();
        }

        private void addBankDetailsForwarderButton_Click(object sender, EventArgs e)
        {
            addForwarderBankDetailsForm = new AddForwarderBankDetails();
            addForwarderBankDetailsForm.Show();
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

                #endregion


        #endregion
    }
}
