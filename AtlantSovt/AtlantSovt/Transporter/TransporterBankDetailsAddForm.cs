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
        bool IsAdding;

        public TransporterBankDetailsAddForm()
        {
            InitializeComponent();
            ID = 0;
            IsAdding = true;
        }

        private void addTransporterBankDetailsButton_Click(object sender, EventArgs e)
        {
            if (transporterBankNameTextBox.Text != "" || transporterMFOTextBox.Text != "" || transporterAccountNumberTextBox.Text != "" || transporterEDRPOUTextBox.Text != "" ||
                   transporterIPNTextBox.Text != "" || transporterCertificateNumberTextBox.Text != "" || transporterCertificateSerialTextBox.Text != "" || transporterSWIFTTextBox.Text != ""
                   || transporterIBANTextBox.Text != "")
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
                    AddTransporterBankDetail(ID, false);
                }
            }
            else
            {
                MessageBox.Show(AtlantSovt.Properties.Resources.Для_збереження_заповніть_хоча_б_одне_поле);
            }

        }
        internal string AddTransporterBankDetail(long id, bool newIsAdding)
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
                    IsAdding = newIsAdding;
                    if (IsAdding)
                    {
                        return AtlantSovt.Properties.Resources.Банківські_дані_успішно_додані_перевізнику + "[" + New_TransporterBankDetail.Id + @"]";
                    }
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Банківські_дані_успішно_додані_перевізнику);
                        return string.Empty;
                    }               
                }
                catch (Exception ec)
                {
                    Log.Write(ec);
                    MessageBox.Show(ec.Message);
                    return string.Empty;

                }

            }
        }
        internal void AddTransporterBankDetail2(long id) 
        {
            ID = id;
        }

        private void TransporterBankDetailsAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAdding)
            {
                if (transporterBankNameTextBox.Text != "" || transporterMFOTextBox.Text != "" || transporterAccountNumberTextBox.Text != "" || transporterEDRPOUTextBox.Text != "" ||
                    transporterIPNTextBox.Text != "" || transporterCertificateNumberTextBox.Text != "" || transporterCertificateSerialTextBox.Text != "" || transporterSWIFTTextBox.Text != "" || transporterIBANTextBox.Text != "")
                {
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
}