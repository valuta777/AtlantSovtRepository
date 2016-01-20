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
        bool IsAdding;

        public ForwarderBankDetailsAddForm()
        {
            InitializeComponent();
            ID = 0;
            IsAdding = true;
        }

        private void addForwarderBankDetailsButton_Click(object sender, EventArgs e)
        {
            if (forwarderBankNameTextBox.Text != "" || forwarderMFOTextBox.Text != "" || forwarderAccountNumberTextBox.Text != "" || forwarderEDRPOUTextBox.Text != "" ||
                forwarderIPNTextBox.Text != "" || forwarderCertificateSerialTextBox.Text != "" || forwarderCertificateNumberTextBox.Text != "" || forwarderSWIFTTextBox.Text != ""
                || forwarderIBANTextBox.Text != "")
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
                    AddForwarderBankDetail(ID, false);
                }
            }
            else
            {
                MessageBox.Show("Для збереження заповніть хочаб одне поле");
            }

        }
        internal string AddForwarderBankDetail(long id, bool newIsAdding)
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
                    IsAdding = newIsAdding;
                    if (IsAdding)
                    {
                        return "Банківські данні успішно додані експедитору [" + New_ForwarderBankDetail.Id + "]\n";
                    }
                    else
                    {
                        MessageBox.Show("Банківські данні успішно додані експедитору");
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
        internal void AddForwarderBankDetail2(long id)
        {
            ID = id;
        }

        private void ForwarderBankDetailsAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAdding)
            {
                if (forwarderBankNameTextBox.Text != "" || forwarderMFOTextBox.Text != "" || forwarderAccountNumberTextBox.Text != "" || forwarderEDRPOUTextBox.Text != "" ||
               forwarderIPNTextBox.Text != "" || forwarderCertificateSerialTextBox.Text != "" || forwarderCertificateNumberTextBox.Text != "" || forwarderSWIFTTextBox.Text != ""
               || forwarderIBANTextBox.Text != "")
                {
                    if (MessageBox.Show("Закрити форму без збереження?\nБанківські данні НЕ збережуться.\n Для збереження натисніть <Отмена> та <Додати Банківські данні>", "Підтвердження закриття", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}