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
    public partial class ClientBankDetailsUpdateForm : Form
    {   
        ClientBankDetail bankDetails;
        bool bankNameChanged, MFOChanged, accountNumberChanged, EDRPOUChanged, IPNChanged, certificateSerialChanged, certificateNumberChanged, SWIFTChanged, IBANChanged;

        public ClientBankDetailsUpdateForm()
        {
            InitializeComponent();            
        }
        internal void UpdateBankDetails(ClientBankDetail update_bankDetails)
        {
            bankDetails = update_bankDetails;
            LoadBankDetailsInfo();
        }
        private void LoadBankDetailsInfo()
        {
            if(bankDetails != null)
            {
                bankNameUpdateClientBankDetailsTextBox.Text = bankDetails.BankName;
                MFOUpdateClientBankDetailsTextBox.Text = bankDetails.MFO;
                accountNumberUpdateClientBankDetailsTextBox.Text = bankDetails.AccountNumber;
                EDRPOUUpdateClientBankDetailsTextBox.Text = bankDetails.EDRPOU;
                IPNUpdateClientBankDetailsTextBox.Text = bankDetails.IPN;
                certificateSerialUpdateClientBankDetailsTextBox.Text = bankDetails.CertificateSerial;
                certificateNumberUpdateClientBankDetailsTextBox.Text = bankDetails.CertificateNamber;
                SWIFTUpdateClientBankDetailsTextBox.Text = bankDetails.SWIFT;
                IBANUpdateClientBankDetailsTextBox.Text = bankDetails.IBAN;
            }
            bankNameChanged = MFOChanged = accountNumberChanged = EDRPOUChanged = IPNChanged = certificateNumberChanged = certificateSerialChanged = SWIFTChanged = IBANChanged = false;
        }

        private void UpdateClientBankDetails()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (bankNameChanged || MFOChanged || accountNumberChanged || EDRPOUChanged || IPNChanged || certificateNumberChanged || certificateSerialChanged || SWIFTChanged || IBANChanged)
                {
                    if (bankNameChanged)
                    {
                        bankDetails.BankName = bankNameUpdateClientBankDetailsTextBox.Text;
                    }
                    if (MFOChanged)
                    {
                        bankDetails.MFO = MFOUpdateClientBankDetailsTextBox.Text;
                    }
                    if (accountNumberChanged) 
                    {
                        bankDetails.AccountNumber = accountNumberUpdateClientBankDetailsTextBox.Text;
                    }
                    if (EDRPOUChanged)
                    {
                        bankDetails.EDRPOU = EDRPOUUpdateClientBankDetailsTextBox.Text;
                    }
                    if (IPNChanged)
                    {
                        bankDetails.IPN = IPNUpdateClientBankDetailsTextBox.Text;
                    }
                    if (certificateNumberChanged)
                    {
                        bankDetails.CertificateNamber = certificateNumberUpdateClientBankDetailsTextBox.Text;
                    }
                    if (certificateSerialChanged)
                    {
                        bankDetails.CertificateSerial = certificateSerialUpdateClientBankDetailsTextBox.Text;
                    }
                    if (SWIFTChanged)
                    {
                        bankDetails.SWIFT = SWIFTUpdateClientBankDetailsTextBox.Text;
                    }
                    if (IBANChanged)
                    {
                        bankDetails.IBAN = IBANUpdateClientBankDetailsTextBox.Text;
                    }
                    db.Entry(bankDetails).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Успішно змінено");
                }
                else
                {
                    MessageBox.Show("Змін не знайдено");
                }
            }
        }

        private void updateClientBankDetailsButton_Click(object sender, EventArgs e)
        {
            UpdateClientBankDetails();
        }

        private void bankNameUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            bankNameChanged = true;
        }

        private void MFOUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            MFOChanged = true;
        }

        private void accountNumberUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            accountNumberChanged = true;
        }

        private void EDRPOUUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            EDRPOUChanged = true;
        }

        private void IPNUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            IPNChanged = true;
        }

        private void certificateNumberUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            certificateNumberChanged = true;
        }

        private void SWIFTUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            SWIFTChanged = true;
        }

        private void IBANUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            IBANChanged = true;
        }

        private void certificateSerialUpdateClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            certificateSerialChanged = true;
        }
    }    
}

        