using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    public partial class MainForm
    {

        int TransporterClikedId = 0;

        #region Show
        void ShowTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                from t in db.Transporters
                select
                new
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    ShortName = t.ShortName,
                    Director = t.Director,
                    PhysicalAddress = t.PhysicalAddress,
                    GeographyAddress = t.GeographyAddress,
                    TaxPayerStatusId = t.TaxPayerStatu.Status,
                    WorkDocumentId = t.WorkDocument.Status,
                    Date = t.ContractEndDay
                };

                transporterShowDataGridView.DataSource = query.ToList();
                transporterShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                transporterShowDataGridView.Columns[1].HeaderText = "Повна Назва";
                transporterShowDataGridView.Columns[2].HeaderText = "Скорочена Назва";
                transporterShowDataGridView.Columns[3].HeaderText = "П.І.Б. Директора";
                transporterShowDataGridView.Columns[4].HeaderText = "Фізична адреса";
                transporterShowDataGridView.Columns[5].HeaderText = "Юридична адреса";
                transporterShowDataGridView.Columns[6].HeaderText = "Статус платника податку";
                transporterShowDataGridView.Columns[7].HeaderText = "На основі";
                transporterShowDataGridView.Columns[8].HeaderText = "Дата завершення договору";

            } transporterShowDataGridView.Update();
        }

        void ShowTransporterInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    TransporterClikedId = Convert.ToInt32(transporterShowDataGridView.CurrentRow.Cells[0].Value);
                    var query =
                    from con in db.TransporterContacts
                    where con.TransporterId == TransporterClikedId
                    select new
                    {
                        Контактна_персона = con.ContactPerson,
                        Номер = con.TelephoneNumber,
                        Факс = con.FaxNumber,
                        Email = con.Email,
                    };
                    transporterShowContactsDataGridView.DataSource = query.ToList();
                    transporterShowContactsDataGridView.Columns[0].HeaderText = "Контактна особа";
                    transporterShowContactsDataGridView.Columns[1].HeaderText = "Телефон";
                    transporterShowContactsDataGridView.Columns[2].HeaderText = "Факс";
                    transporterShowContactsDataGridView.Columns[3].HeaderText = "Email";

                    var query1 =
                        from f in db.Transporters
                        where f.Id == TransporterClikedId
                        select f.Comment;

                    transporterShowCommentRichTextBox.Text = query1.FirstOrDefault();

                    var query2 =
                    from b in db.TransporterBankDetails
                    where b.Id == TransporterClikedId
                    select new
                    {
                        Name = b.BankName,
                        MFO = b.MFO,
                        AccountNumber = b.AccountNumber,
                        EDRPOU = b.EDRPOU,
                        IPN = b.IPN,
                        CertificateNumber = b.CertificateNamber,
                        SWIFT = b.SWIFT,
                        IBAN = b.IBAN
                    };

                    transporterShowBankDetailsDataGridView.DataSource = query2.ToList();
                    transporterShowBankDetailsDataGridView.Columns[0].HeaderText = "Назва банку";
                    transporterShowBankDetailsDataGridView.Columns[1].HeaderText = "МФО";
                    transporterShowBankDetailsDataGridView.Columns[2].HeaderText = "Номер рахунку";
                    transporterShowBankDetailsDataGridView.Columns[3].HeaderText = "ЕДРПОУ";
                    transporterShowBankDetailsDataGridView.Columns[4].HeaderText = "IPN";
                    transporterShowBankDetailsDataGridView.Columns[5].HeaderText = "Номер свідоцтва";
                    transporterShowBankDetailsDataGridView.Columns[6].HeaderText = "SWIFT";
                    transporterShowBankDetailsDataGridView.Columns[7].HeaderText = "IBAN";

                    var query3 =
                   from tc in db.TransporterCountries
                   where tc.Id == TransporterClikedId
                   select new
                   {
                       country = tc.Country.Name
                   };

                    transporterShowCountryDataGridView.DataSource = query3.ToList();
                    transporterShowCountryDataGridView.Columns[0].HeaderText = "Країна";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Немає жодного перевізника");
                }
            }

            transporterShowContactsDataGridView.Update();
            transporterShowBankDetailsDataGridView.Update();
            transporterShowCountryDataGridView.Update();
            transporterShowContactsDataGridView.Visible = true;
            transporterShowBankDetailsDataGridView.Visible = true;
            transporterShowCountryDataGridView.Visible = true;
        }

        public void ShowTransporterFilter()
        {
           

        //    if(transporterShowFiltrationForm != null)
        //    {
                List<long> countriesIDs = new List<long> { 1, 2, 3 };

                using (var db = new AtlantSovtContext())
                {
                    var getTransporters =
                    from t in db.Transporters
                    where
                    (
                        from c in t.TransporterCountries
                        where countriesIDs.Contains(c.Country.Id)
                        select c
                    ).Count() == countriesIDs.Count
                    select
                new
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    ShortName = t.ShortName,
                    Director = t.Director,
                    PhysicalAddress = t.PhysicalAddress,
                    GeographyAddress = t.GeographyAddress,
                    TaxPayerStatusId = t.TaxPayerStatu.Status,
                    WorkDocumentId = t.WorkDocument.Status,
                    Date = t.ContractEndDay
                };

                    transporterShowDataGridView.DataSource = getTransporters.ToList();
                    transporterShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    transporterShowDataGridView.Columns[1].HeaderText = "Повна Назва";
                    transporterShowDataGridView.Columns[2].HeaderText = "Скорочена Назва";
                    transporterShowDataGridView.Columns[3].HeaderText = "П.І.Б. Директора";
                    transporterShowDataGridView.Columns[4].HeaderText = "Фізична адреса";
                    transporterShowDataGridView.Columns[5].HeaderText = "Юридична адреса";
                    transporterShowDataGridView.Columns[6].HeaderText = "Статус платника податку";
                    transporterShowDataGridView.Columns[7].HeaderText = "На основі";
                    transporterShowDataGridView.Columns[8].HeaderText = "Дата завершення договору";

                } transporterShowDataGridView.Update();
         //   }
        }

        #endregion

    }
}
