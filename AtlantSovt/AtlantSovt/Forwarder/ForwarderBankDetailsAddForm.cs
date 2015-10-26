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
    public partial class ForwarderBankDetailsAddForm : Form
    {
        string new_BankName;
        string new_MFO;
        string new_AccountNumber;
        string new_EDRPOU;
        string new_IPN;
        string new_CertificateNumber;
        string new_CertificateSerial;
        string new_SWIFT;
        string new_IBAN;
        long ID;

        public ForwarderBankDetailsAddForm()
        {
            InitializeComponent();
            ID = 0;
        }

        private void addForwarderBankDetailsButton_Click(object sender, EventArgs e)
        {
            if (forwarderBankNameTextBox.Text != "" && forwarderMFOTextBox.Text != "" && forwarderAccountNumberTextBox.Text != "" && forwarderEDRPOUTextBox.Text != ""
                && forwarderIPNTextBox.Text != "")
            {
                new_BankName = forwarderBankNameTextBox.Text;
                new_MFO = forwarderMFOTextBox.Text;
                new_AccountNumber = forwarderAccountNumberTextBox.Text;
                new_EDRPOU = forwarderEDRPOUTextBox.Text;
                new_IPN = forwarderIPNTextBox.Text;
                new_CertificateSerial = forwarderCertificateSerialTextBox.Text;
                new_CertificateNumber = forwarderCertificateNumberTextBox.Text;
                new_SWIFT = forwarderSWIFTTextBox.Text;
                new_IBAN = forwarderIBANTextBox.Text;
                this.Hide();
                if (ID != 0)
                {
                    AddForwarderBankDetail(ID);
                }
            }
            else
            {
                MessageBox.Show("Заповніть обов'язкові поля!");
            }

        }
        internal void AddForwarderBankDetail(long id)
        {
            using (var db = new AtlantSovtContext())
            {
                ForwarderBankDetail New_ForwarderBankDetail = new ForwarderBankDetail
                {
                    Id = id,
                    BankName = new_BankName,
                    MFO = new_MFO,
                    AccountNumber = new_AccountNumber,
                    EDRPOU = new_EDRPOU,
                    IPN = new_IPN,
                    CertificateSerial = new_CertificateSerial,
                    CertificateNamber = new_CertificateNumber,
                    SWIFT = new_SWIFT,
                    IBAN = new_IBAN
                };
                try
                {
                    db.ForwarderBankDetails.Add(New_ForwarderBankDetail);
                    db.SaveChanges();
                    MessageBox.Show("Банківські данні успішно додані експедитору");
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message);

                }
            }
        }
        internal void AddForwarderBankDetail2(long id)
        {
            ID = id;
        }
    }
}