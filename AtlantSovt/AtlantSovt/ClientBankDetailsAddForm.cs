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
    public partial class ClientBankDetailsAddForm : Form
    {
        string new_BankName;
        string new_MFO;
        string new_AccountNumber;
        string new_EDRPOU;
        string new_IPN;
        string new_CertificateNumber;
        string new_SWIFT;
        string new_IBAN;
        long ID;

        public ClientBankDetailsAddForm()
        {
            InitializeComponent();
            ID = 0;
        }

        private void addClientBankDetailsButton_Click(object sender, EventArgs e)
        {
            if (clientBankNameTextBox.Text != "" && clientMFOTextBox.Text != "" && clientAccountNumberTextBox.Text != "" && clientEDRPOUTextBox.Text != ""
                && clientIPNTextBox.Text != "")
            {
                new_BankName = clientBankNameTextBox.Text;
                new_MFO = clientMFOTextBox.Text;
                new_AccountNumber = clientAccountNumberTextBox.Text;
                new_EDRPOU = clientEDRPOUTextBox.Text;
                new_IPN = clientIPNTextBox.Text;
                new_CertificateNumber = clientCertificateNumberTextBox.Text;
                new_SWIFT = clientSWIFTTextBox.Text;
                new_IBAN = clientIBANTextBox.Text;
                this.Hide();
                if (ID != 0)
                {
                    AddClientBankDetail(ID);
                }
            }
            else
            {
                MessageBox.Show("Заповніть всі поля!");
            }

        }
        internal void AddClientBankDetail(long id)
        {
            using (var db = new AtlantSovtContext())
            {
                ClientBankDetail New_ClientBankDetail = new ClientBankDetail
                {
                    Id = id,         
                    BankName = new_BankName,
                    MFO = new_MFO,
                    AccountNumber = new_AccountNumber,
                    EDRPOU = new_EDRPOU,
                    IPN = new_IPN,
                    CertificateNamber = new_CertificateNumber,
                    SWIFT = new_SWIFT,
                    IBAN = new_IBAN                    
                };
                try
                {
                    db.ClientBankDetails.Add(New_ClientBankDetail);                    
                    db.SaveChanges();
                    MessageBox.Show("Банківські данні успішно додані клієнту");                   
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message);
                    
                }

            }
        }                    
        internal void AddClientBankDetail2(long id) 
        {
            ID = id;
        }
    }    
}