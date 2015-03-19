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
        WorkDocument transporterWorkDocument;
        TaxPayerStatu transporterTaxPayerStatus;
        bool transporterAddWorkDocumentFlag, transporterAddTaxPayerStatusFlag;

        //Show
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
                   where tc.TransporterId == TransporterClikedId
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
            List<long> countriesIDs = new List<long>();
            List<long> vehiclesIDs = new List<long>();

            countriesIDs = transporterShowFiltrationForm.GetCountries();
            vehiclesIDs = transporterShowFiltrationForm.GetVehicle();

            if (transporterShowFiltrationForm != null)
            {
               
                if (countriesIDs.Count != 0 && vehiclesIDs.Count == 0)
                {
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

                }

            }

            if (transporterShowFiltrationForm != null)
            {

                if (countriesIDs.Count == 0 && vehiclesIDs.Count != 0)
                {

                    using (var db = new AtlantSovtContext())
                    {
                        var getTransporters =
                        from t in db.Transporters
                        where
                        (
                            from v in t.TransporterVehicles
                            where vehiclesIDs.Contains(v.Vehicle.Id)
                            select v
                        ).Count() == vehiclesIDs.Count
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
                }
            }


            if (transporterShowFiltrationForm != null)
            {

                if (countriesIDs.Count != 0 && vehiclesIDs.Count != 0)
                {

                    using (var db = new AtlantSovtContext())
                    {
                        var getTransporters =
                        from t in db.Transporters
                        where
                        (
                            from v in t.TransporterVehicles
                            where vehiclesIDs.Contains(v.Vehicle.Id)
                            select v
                        ).Count() == vehiclesIDs.Count
                         &&
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
                }
            }

            if (transporterShowFiltrationForm != null)
            {

                if (countriesIDs.Count == 0 && vehiclesIDs.Count == 0)
                {
                    ShowTransporter();
                }
            }

            countriesIDs.Clear();
            vehiclesIDs.Clear();
        }

        #endregion

        //Add
        #region Add
        void AddTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                if (nameTransporterAddTextBox.Text != "" && directorTransporterAddTextBox.Text != "" && physicalAddressTransporterAddTextBox.Text != "" && geographyAddressTransporterAddTextBox.Text != "" && transporterAddWorkDocumentFlag && transporterAddTaxPayerStatusFlag)
                {
                    var new_FullName = nameTransporterAddTextBox.Text;
                    var new_ShortName = shortNameTransporterAddTextBox.Text;
                    var new_Director = directorTransporterAddTextBox.Text;
                    var new_PhysicalAddress = physicalAddressTransporterAddTextBox.Text;
                    var new_GeographyAddress = geographyAddressTransporterAddTextBox.Text;
                    var new_WorkDocumentId = transporterWorkDocument.Id;
                    var new_TaxPayerStatusId = transporterTaxPayerStatus.Id;
                    var new_ContractType = originalTransporterAddCheckBox.Checked;
                    var new_Comment = commentTransporterAddTextBox.Text;

                    var new_IfForwarder = filtersTransporterAddCheckedListBox.GetItemChecked(0);
                    var new_TUR = filtersTransporterAddCheckedListBox.GetItemChecked(1);
                    var new_CMR = filtersTransporterAddCheckedListBox.GetItemChecked(2);
                    var new_EKMT = filtersTransporterAddCheckedListBox.GetItemChecked(3);
                    var new_Zborny = filtersTransporterAddCheckedListBox.GetItemChecked(4);
                    var new_AD = filtersTransporterAddCheckedListBox.GetItemChecked(5);


                    var New_Transporter = new Transporter
                    {
                        FullName = new_FullName,
                        ShortName = new_ShortName,
                        Director = new_Director,
                        PhysicalAddress = new_PhysicalAddress,
                        GeographyAddress = new_GeographyAddress,
                        WorkDocumentId = new_WorkDocumentId,
                        TaxPayerStatusId = new_TaxPayerStatusId,
                        ContractType = new_ContractType,                    
                        Comment = new_Comment,
                    };

                    var New_Filters = new Filter
                    {
                        IfForwarder = new_IfForwarder,
                        TUR = new_TUR,
                        CMR = new_CMR,
                        EKMT = new_EKMT,
                        Zborny = new_Zborny,
                        AD = new_AD                        
                    };

                    try
                    {
                        db.Transporters.Add(New_Transporter);
                        db.SaveChanges();
                        MessageBox.Show("Перевізник успішно доданий");

                        New_Filters.Id = New_Transporter.Id;
                        db.Filters.Add(New_Filters);
                        db.SaveChanges();

                        if (addTransporterBankDetailsAddForm != null)
                        {
                            addTransporterBankDetailsAddForm.AddTransporterBankDetail(New_Transporter.Id);
                            addTransporterBankDetailsAddForm = null;
                        }
                        if (addTransporterContactAddForm != null)
                        {
                            addTransporterContactAddForm.AddTransporterContact(New_Transporter.Id);
                            addTransporterContactAddForm = null;
                        }
                        if (transporterCountryAndVehicleSelectForm != null)
                        {
                            transporterCountryAndVehicleSelectForm.CoutriesAndVehiclesSelect(db.Transporters.Find(New_Transporter.Id));
                            transporterCountryAndVehicleSelectForm = null;
                        }
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message);         
                    }

                }
                else
                {
                    MessageBox.Show("Обов'язкові поля не заповнені");
                }
            }

        }

        void LoadTaxPayerStatusTransporterAddInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.TaxPayerStatus
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    taxPayerStatusTransporterAddComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void LoadWorkDocumentTransporterAddInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from w in db.WorkDocuments
                            orderby w.Id
                            select w;
                foreach (var item in query)
                {
                    workDocumentTransporterAddComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadTaxPayerStatusTransporterAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = taxPayerStatusTransporterAddComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterTaxPayerStatus = db.TaxPayerStatus.Find(id);
            }
        }

        void SplitLoadWorkDocumentTransporterAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = workDocumentTransporterAddComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterWorkDocument = db.WorkDocuments.Find(id);
            }
        }
        #endregion

        //Update
        #region Update


        #endregion

    }
}
