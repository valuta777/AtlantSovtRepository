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
    public partial class TransporterBankDetailsAddForm : Form
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

        public TransporterBankDetailsAddForm()
        {
            InitializeComponent();
            ID = 0;
        }

        private void addTransporterBankDetailsButton_Click(object sender, EventArgs e)
        {
            if (transporterBankNameTextBox.Text != "" && transporterMFOTextBox.Text != "" && transporterAccountNumberTextBox.Text != "" && transporterEDRPOUTextBox.Text != ""
                && transporterIPNTextBox.Text != "")
            {
                new_BankName = transporterBankNameTextBox.Text;
                new_MFO = transporterMFOTextBox.Text;
                new_AccountNumber = transporterAccountNumberTextBox.Text;
                new_EDRPOU = transporterEDRPOUTextBox.Text;
                new_IPN = transporterIPNTextBox.Text;
                new_CertificateNumber = transporterCertificateNumberTextBox.Text;
                new_CertificateSerial = transporterCertificateSerialTextBox.Text;
                new_SWIFT = transporterSWIFTTextBox.Text;
                new_IBAN = transporterIBANTextBox.Text;
                this.Hide();
                if (ID != 0)
                {
                    AddTransporterBankDetail(ID);
                }
            }
            else
            {
                MessageBox.Show("Заповніть обов'язкові поля!");
            }

        }
        internal void AddTransporterBankDetail(long id)
        {
            using (var db = new AtlantSovtContext())
            {
                TransporterBankDetail New_TransporterBankDetail = new TransporterBankDetail
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
                    db.TransporterBankDetails.Add(New_TransporterBankDetail);                    
                    db.SaveChanges();
                    MessageBox.Show("Банківські данні успішно додані перевізнику");                   
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message);
                    
                }

            }
        }
        internal void AddTransporterBankDetail2(long id) 
        {
            ID = id;
        }
    }    
}