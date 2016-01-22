using AtlantSovt.Additions;
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
        string new_CertificateSerial;
        string new_CertificateNumber;
        string new_SWIFT;
        string new_IBAN;
        long ID;
        bool IsAdding;

        public ClientBankDetailsAddForm()
        {
            InitializeComponent();
            ID = 0;
            IsAdding = true;
        }

        private void addClientBankDetailsButton_Click(object sender, EventArgs e)
        {
            if (clientBankNameTextBox.Text != "" || clientMFOTextBox.Text != "" || clientAccountNumberTextBox.Text != "" || clientEDRPOUTextBox.Text != "" ||
                clientIPNTextBox.Text != "" || clientCertificateNumberTextBox.Text != "" || clientCertificateSerialTextBox.Text != "" || clientSWIFTTextBox.Text != "" 
                || clientIBANTextBox.Text != "")
            {
                new_BankName = clientBankNameTextBox.Text;
                new_MFO = clientMFOTextBox.Text;
                new_AccountNumber = clientAccountNumberTextBox.Text;
                new_EDRPOU = clientEDRPOUTextBox.Text;
                new_IPN = clientIPNTextBox.Text;
                new_CertificateNumber = clientCertificateNumberTextBox.Text;
                new_CertificateSerial = clientCertificateSerialTextBox.Text;
                new_SWIFT = clientSWIFTTextBox.Text;
                new_IBAN = clientIBANTextBox.Text;
                this.Hide();
                if (ID != 0)
                {
                    AddClientBankDetail(ID,false);
                }
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Для_збереження_заповніть_хоча_б_одне_поле);
            }
        }
        internal string AddClientBankDetail(long id, bool newIsAdding)
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
                    CertificateSerial = new_CertificateSerial,
                    SWIFT = new_SWIFT,
                    IBAN = new_IBAN                    
                };
                try
                {
                    db.ClientBankDetails.Add(New_ClientBankDetail);                    
                    db.SaveChanges();
                    IsAdding = newIsAdding;
                    if (IsAdding)
                    {
                        return AtlantSovt.Properties.Resources.Банківські_дані_успішно_додані_клієнту + " [" + New_ClientBankDetail.Id+ @"]
";
                    }        
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Банківські_дані_успішно_додані_клієнту);
                        return string.Empty;
                    }         
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);
                    return string.Empty;
                }
            }
        }                    
        internal void AddClientBankDetail2(long id) 
        {
            ID = id;
        }

        private void ClientBankDetailsAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAdding)
            {
                if(clientBankNameTextBox.Text != "" || clientMFOTextBox.Text != "" || clientAccountNumberTextBox.Text != "" || clientEDRPOUTextBox.Text != "" || clientIPNTextBox.Text != "" || clientCertificateNumberTextBox.Text !="" || clientCertificateSerialTextBox.Text != "" || clientSWIFTTextBox.Text != "" || clientIBANTextBox.Text != "")
                {
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Закрити_форму_без_збереження_банкінських_даних, AtlantSovt.Properties.Resources.Підтвердження_закриття, MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }    
}