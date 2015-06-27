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
    public partial class ForwarderBankDetailsUpdateForm : Form
    {
        ForwarderBankDetail bankDetails;
        bool bankNameChanged, MFOChanged, accountNumberChanged, EDRPOUChanged, IPNChanged, certificateSerialChanged, certificateNumberChanged, SWIFTChanged, IBANChanged;

        public ForwarderBankDetailsUpdateForm()
        {
            InitializeComponent();            
        }
        internal void UpdateForwarderBankDetails(ForwarderBankDetail update_bankDetails)
        {
            bankDetails = update_bankDetails;
            LoadForwarderBankDetailsInfo();
        }
        private void LoadForwarderBankDetailsInfo()
        {
            if(bankDetails != null)
            {
                bankNameUpdateForwarderBankDetailsTextBox.Text = bankDetails.BankName;
                MFOUpdateForwarderBankDetailsTextBox.Text = bankDetails.MFO;
                accountNumberUpdateForwarderBankDetailsTextBox.Text = bankDetails.AccountNumber;
                EDRPOUUpdateForwarderBankDetailsTextBox.Text = bankDetails.EDRPOU;
                IPNUpdateForwarderBankDetailsTextBox.Text = bankDetails.IPN;
                certificateSerialUpdateForwarderBankDetailsTextBox.Text = bankDetails.CertificateSerial;
                certificateNumberUpdateForwarderBankDetailsTextBox.Text = bankDetails.CertificateNamber;
                SWIFTUpdateForwarderBankDetailsTextBox.Text = bankDetails.SWIFT;
                IBANUpdateForwarderBankDetailsTextBox.Text = bankDetails.IBAN;
            }
            bankNameChanged =  MFOChanged = accountNumberChanged = EDRPOUChanged = IPNChanged = certificateNumberChanged = certificateSerialChanged = SWIFTChanged = IBANChanged = false;
        }

        private void UpdateForwarderBankDetails()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (bankNameChanged ||  MFOChanged || accountNumberChanged || EDRPOUChanged || IPNChanged || certificateNumberChanged || certificateSerialChanged || SWIFTChanged || IBANChanged)
                {
                    if (bankNameChanged)
                    {
                        bankDetails.BankName = bankNameUpdateForwarderBankDetailsTextBox.Text;
                    }
                    if (MFOChanged)
                    {
                        bankDetails.MFO = MFOUpdateForwarderBankDetailsTextBox.Text;
                    }
                    if (accountNumberChanged) 
                    {
                        bankDetails.AccountNumber = accountNumberUpdateForwarderBankDetailsTextBox.Text;
                    }
                    if (EDRPOUChanged)
                    {
                        bankDetails.EDRPOU = EDRPOUUpdateForwarderBankDetailsTextBox.Text;
                    }
                    if (IPNChanged)
                    {
                        bankDetails.IPN = IPNUpdateForwarderBankDetailsTextBox.Text;
                    }
                    if (certificateNumberChanged)
                    {
                        bankDetails.CertificateNamber = certificateNumberUpdateForwarderBankDetailsTextBox.Text;
                    }
                    if (certificateSerialChanged)
                    {
                        bankDetails.CertificateSerial = certificateSerialUpdateForwarderBankDetailsTextBox.Text;
                    }
                    if (SWIFTChanged)
                    {
                        bankDetails.SWIFT = SWIFTUpdateForwarderBankDetailsTextBox.Text;
                    }
                    if (IBANChanged)
                    {
                        bankDetails.IBAN = IBANUpdateForwarderBankDetailsTextBox.Text;
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

        private void updateForwarderBankDetailsButton_Click(object sender, EventArgs e)
        {
            UpdateForwarderBankDetails();
        }

        private void bankNameForwarderClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            bankNameChanged = true;
        }

        private void MFOUpdateForwarderBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            MFOChanged = true;
        }

        private void accountNumberUpdateForwarderBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            accountNumberChanged = true;
        }

        private void EDRPOUUpdateForwarderBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            EDRPOUChanged = true;
        }

        private void IPNUpdateForwarderBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            IPNChanged = true;
        }

        private void certificateNumberUpdateForwarderBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            certificateNumberChanged = true;
        }

        private void SWIFTUpdateForwarderBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            SWIFTChanged = true;
        }

        private void IBANUpdateForwarderBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            IBANChanged = true;
        }

        private void certificateSerialUpdateForwarderBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            certificateSerialChanged = true;
        }                    

    }    
}

        