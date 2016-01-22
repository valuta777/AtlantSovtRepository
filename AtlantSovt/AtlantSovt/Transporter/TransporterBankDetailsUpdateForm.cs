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
    public partial class TransporterBankDetailsUpdateForm : Form
    {
        TransporterBankDetail bankDetails;
        bool bankNameChanged, MFOChanged, accountNumberChanged, EDRPOUChanged, IPNChanged, certificateSerialChanged, certificateNumberChanged, SWIFTChanged, IBANChanged;

        public TransporterBankDetailsUpdateForm()
        {
            InitializeComponent();            
        }
        internal void UpdateTransporterBankDetails(TransporterBankDetail update_bankDetails)
        {
            bankDetails = update_bankDetails;
            LoadTransporterBankDetailsInfo();
        }
        private void LoadTransporterBankDetailsInfo()
        {
            if(bankDetails != null)
            {
                bankNameUpdateTransporterBankDetailsTextBox.Text = bankDetails.BankName;
                MFOUpdateTransporterBankDetailsTextBox.Text = bankDetails.MFO;
                accountNumberUpdateTransporterBankDetailsTextBox.Text = bankDetails.AccountNumber;
                EDRPOUUpdateTransporterBankDetailsTextBox.Text = bankDetails.EDRPOU;
                IPNUpdateTransporterBankDetailsTextBox.Text = bankDetails.IPN;
                certificateSerialUpdateTransporterBankDetailsTextBox.Text = bankDetails.CertificateSerial;
                certificateNumberUpdateTransporterBankDetailsTextBox.Text = bankDetails.CertificateNamber;
                SWIFTUpdateTransporterBankDetailsTextBox.Text = bankDetails.SWIFT;
                IBANUpdateTransporterBankDetailsTextBox.Text = bankDetails.IBAN;
            }
            bankNameChanged = MFOChanged = accountNumberChanged = EDRPOUChanged = IPNChanged = certificateNumberChanged = certificateSerialChanged = SWIFTChanged = IBANChanged = false;
        }

        private void UpdateTransporterBankDetails()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (bankNameChanged || MFOChanged || accountNumberChanged || EDRPOUChanged || IPNChanged || certificateNumberChanged || certificateSerialChanged || SWIFTChanged || IBANChanged)
                {
                    if (bankNameChanged)
                    {
                        bankDetails.BankName = bankNameUpdateTransporterBankDetailsTextBox.Text;
                    }
                    if (MFOChanged)
                    {
                        bankDetails.MFO = MFOUpdateTransporterBankDetailsTextBox.Text;
                    }
                    if (accountNumberChanged) 
                    {
                        bankDetails.AccountNumber = accountNumberUpdateTransporterBankDetailsTextBox.Text;
                    }
                    if (EDRPOUChanged)
                    {
                        bankDetails.EDRPOU = EDRPOUUpdateTransporterBankDetailsTextBox.Text;
                    }
                    if (IPNChanged)
                    {
                        bankDetails.IPN = IPNUpdateTransporterBankDetailsTextBox.Text;
                    }
                    if (certificateSerialChanged)
                    {
                        bankDetails.CertificateSerial = certificateSerialUpdateTransporterBankDetailsTextBox.Text;
                    }
                    if (certificateNumberChanged)
                    {
                        bankDetails.CertificateNamber = certificateNumberUpdateTransporterBankDetailsTextBox.Text;
                    }
                    if (SWIFTChanged)
                    {
                        bankDetails.SWIFT = SWIFTUpdateTransporterBankDetailsTextBox.Text;
                    }
                    if (IBANChanged)
                    {
                        bankDetails.IBAN = IBANUpdateTransporterBankDetailsTextBox.Text;
                    }
                    db.Entry(bankDetails).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show(AtlantSovt.Properties.Resources.Успішно_змінено);
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Змін_не_знайдено);
                }
            }
        }

        private void updateTransporterBankDetailsButton_Click(object sender, EventArgs e)
        {
            UpdateTransporterBankDetails();
        }

        private void bankNameTransporterClientBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            bankNameChanged = true;
        }

        private void MFOUpdateTransporterBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            MFOChanged = true;
        }

        private void accountNumberUpdateTransporterBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            accountNumberChanged = true;
        }

        private void EDRPOUUpdateTransporterBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            EDRPOUChanged = true;
        }

        private void IPNUpdateTransporterBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            IPNChanged = true;
        }

        private void certificateNumberUpdateTransporterBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            certificateNumberChanged = true;
        }

        private void SWIFTUpdateTransporterBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            SWIFTChanged = true;
        }

        private void IBANUpdateTransporterBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            IBANChanged = true;
        }

        private void certificateSerialUpdateTransporterBankDetailsTextBox_TextChanged(object sender, EventArgs e)
        {
            certificateSerialChanged = true;
        }               
    }    
}

        