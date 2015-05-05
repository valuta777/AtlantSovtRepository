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
using System.Data.Entity.SqlServer;

using System.Linq.Expressions;

namespace AtlantSovt
{
    public partial class MainForm
    {
        Transporter transporter, deleteTransporter;
        TransporterContact contact;
        WorkDocument transporterWorkDocument = null;
        TaxPayerStatu transporterTaxPayerStatus = null;
        Filter filters;


        int TransporterClikedId = 0;
        
        bool transporterAddWorkDocumentFlag, transporterAddTaxPayerStatusFlag;

        bool transporterFullNameChanged, transporterShortNameChanged, transporterDirectorChanged,
            transporterFiltersChanged, transporterPhysicalAddressChanged,
            transporterGeographyAddressChanged, transporterCommentChanged, transporterWorkDocumentChanged,
            transporterTaxPayerStatusChanged, transporterOriginalChanged, transporterFaxChanged, transporterIsWorkDocumentExist, transporterIsTaxPayerStatusExist;
        
        //Show
        #region Show

        void ShowTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                from t in db.Transporters
                orderby t.Id
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
            List<Nullable<bool>> filtersStates = new List<Nullable<bool>>();


            countriesIDs = transporterShowFiltrationForm.GetCountries();
            vehiclesIDs = transporterShowFiltrationForm.GetVehicle();
            filtersStates = transporterShowFiltrationForm.GetFilters();

            //bool list to bools
            #region FiltersResorses
            bool IfForwarderChecked, TURChecked, CMRChecked, EKMTChecked, ZbornyChecked, ADChecked;
            IfForwarderChecked = TURChecked = CMRChecked = EKMTChecked = ZbornyChecked = ADChecked = false;

            if (filtersStates != null)
            {
                int index = 0;
                foreach (bool? b1 in filtersStates)
                {
                    if (!b1.HasValue)
                    {

                    }
                    else
                    {
                        switch (index)
                        {
                            case 0: IfForwarderChecked = b1.Value; break;
                            case 1: TURChecked = b1.Value; break;
                            case 2: CMRChecked = b1.Value; break;
                            case 3: EKMTChecked = b1.Value; break;
                            case 4: ZbornyChecked = b1.Value; break;
                            case 5: ADChecked = b1.Value; break;
                        }
                    }
                    index++;
                }
            }
            #endregion
            
            if (transporterShowFiltrationForm != null)
            {
                using (var db = new AtlantSovtContext())
                {
                    // Filter only Countries
                    #region OnlyCountries
                    if (countriesIDs.Count != 0 && vehiclesIDs.Count == 0 && filtersStates == null)
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
                            transporterShowDataGridView.Update();

                    }

                    
                #endregion

                    // Filter only Vehicles
                    #region OnlyVehicles
                    if (countriesIDs.Count == 0 && vehiclesIDs.Count != 0 && filtersStates == null)
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

                            transporterShowDataGridView.Update();
                    }
                    
                #endregion

                    // Filter only Filters
                    #region OnlyFilters
                    if (countriesIDs.Count == 0 && vehiclesIDs.Count == 0 && filtersStates != null)
                    {
                        //All only Filters Queries
                        #region Queries


                        //1
                        var OnlyFiltersQueryADChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked
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
                                };
                        //2
                        var OnlyFiltersQueryZbornyChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked
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
                               };
                        //3
                        var OnlyFiltersQueryADAndZbornyChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked
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
                              };
                        //4
                        var OnlyFiltersQueryEKMTChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked
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
                              };
                        //5
                        var OnlyFiltersQueryADAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked
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
                             };
                        //6
                        var OnlyFiltersQueryZbornyAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked
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
                             };
                        //7
                        var OnlyFiltersQueryADAndZbornyAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked
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
                             };
                        //8
                        var OnlyFiltersQueryCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked
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
                              };
                        //9
                        var OnlyFiltersQueryADAndCMRChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked
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
                               };
                        //10
                        var OnlyFiltersQueryZbornyAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked
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
                              };
                        //11
                        var OnlyFiltersQueryADAndZbornyAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked
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
                              };

                        //12
                        var OnlyFiltersQueryEKMTAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
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
                              };
                        //13
                        var OnlyFiltersQueryADAndEKMTAndCMRChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
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
                             };

                        //14
                        var OnlyFiltersQueryZbornyAndEKMTAndCMRChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
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
                               };
                        //15
                        var OnlyFiltersQueryADAndZbornyAndEKMTAndCMRChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
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
                             };

                        //TUR
                        //16
                        var OnlyFiltersQueryTURChecked =
                                    from t in db.Transporters
                                    where t.Filter.TUR == TURChecked
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
                                };

                        //17
                        var OnlyFiltersQueryADAndTURChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.TUR == TURChecked
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
                                };
                        //18
                        var OnlyFiltersQueryZbornyAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked
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
                               };
                        //19
                        var OnlyFiltersQueryADAndZbornyAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked
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
                              };
                        //20
                        var OnlyFiltersQueryEKMTAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
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
                              };
                        //21
                        var OnlyFiltersQueryADAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
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
                             };
                        //22
                        var OnlyFiltersQueryZbornyAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
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
                             };
                        //23
                        var OnlyFiltersQueryADAndZbornyAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
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
                             };
                        //24
                        var OnlyFiltersQueryCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                              };
                        //25
                        var OnlyFiltersQueryADAndCMRAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                               };
                        //26
                        var OnlyFiltersQueryZbornyAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                              };
                        //27
                        var OnlyFiltersQueryADAndZbornyAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                              };

                        //28
                        var OnlyFiltersQueryEKMTAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                              };
                        //29
                        var OnlyFiltersQueryADAndEKMTAndCMRAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                             };

                        //30
                        var OnlyFiltersQueryZbornyAndEKMTAndCMRAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                               };
                        //31
                        var OnlyFiltersQueryADAndZbornyAndEKMTAndCMRAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                             };

                        //IFforwarder
                        //32
                        var OnlyFiltersQueryIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.IfForwarder == IfForwarderChecked
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
                                };

                        //33
                        var OnlyFiltersQueryADAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                                };
                        //34
                        var OnlyFiltersQueryZbornyAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //35
                        var OnlyFiltersQueryADAndZbornyAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //36
                        var OnlyFiltersQueryEKMTAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //37
                        var OnlyFiltersQueryADAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //38
                        var OnlyFiltersQueryZbornyAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //39
                        var OnlyFiltersQueryADAndZbornyAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //40
                        var OnlyFiltersQueryCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //41
                        var OnlyFiltersQueryADAndCMRAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //42
                        var OnlyFiltersQueryZbornyAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //43
                        var OnlyFiltersQueryADAndZbornyAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };

                        //44
                        var OnlyFiltersQueryEKMTAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //45
                        var OnlyFiltersQueryADAndEKMTAndCMRAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };

                        //46
                        var OnlyFiltersQueryZbornyAndEKMTAndCMRAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //47
                        var OnlyFiltersQueryADAndZbornyAndEKMTAndCMRAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //48
                        var OnlyFiltersQueryTURAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                                };

                        //49
                        var OnlyFiltersQueryADAndTURAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                                };
                        //50
                        var OnlyFiltersQueryZbornyAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //51
                        var OnlyFiltersQueryADAndZbornyAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //52
                        var OnlyFiltersQueryEKMTAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //53
                        var OnlyFiltersQueryADAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //54
                        var OnlyFiltersQueryZbornyAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //55
                        var OnlyFiltersQueryADAndZbornyAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //56
                        var OnlyFiltersQueryCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //57
                        var OnlyFiltersQueryADAndCMRAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //58
                        var OnlyFiltersQueryZbornyAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //59
                        var OnlyFiltersQueryADAndZbornyAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };

                        //60
                        var OnlyFiltersQueryEKMTAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //61
                        var OnlyFiltersQueryADAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };

                        //62
                        var OnlyFiltersQueryZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //63
                        var OnlyFiltersQueryADAndZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };

                        #endregion

                        if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)        // 0 0 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADChecked.ToList();                        //1
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyChecked.ToList();                       //2
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyChecked.ToList();              //3
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryEKMTChecked.ToList();                     //4
                        }   
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndEKMTChecked.ToList();                 //5
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndEKMTChecked.ToList();       //6
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndEKMTChecked.ToList();       //7
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryCMRChecked.ToList();                      //8
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndCMRChecked.ToList();                 //9
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndCMRChecked.ToList();             //10
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndCMRChecked.ToList();            //11
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryEKMTAndCMRChecked.ToList();            //12
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndEKMTAndCMRChecked.ToList();            //13
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndEKMTAndCMRChecked.ToList();            //14
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndEKMTAndCMRChecked.ToList();            //15
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryTURChecked.ToList();            //16
                        }            
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)        // 0 1 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndTURChecked.ToList();                        //17
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndTURChecked.ToList();                       //18
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndTURChecked.ToList();              //19
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryEKMTAndTURChecked.ToList();                     //20
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndEKMTAndTURChecked.ToList();                 //21
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndEKMTAndTURChecked.ToList();       //22
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndEKMTAndTURChecked.ToList();       //23
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryCMRChecked.ToList();                      //24
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndCMRAndTURChecked.ToList();                 //25
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndCMRAndTURChecked.ToList();             //26
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndCMRAndTURChecked.ToList();            //27
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryEKMTAndCMRChecked.ToList();                        //28
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndEKMTAndCMRAndTURChecked.ToList();            //29
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndEKMTAndCMRAndTURChecked.ToList();            //30
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndEKMTAndCMRAndTURChecked.ToList();            //31
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryIfForwarderChecked.ToList(); //32
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)  // 1 0 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndIfForwarderChecked.ToList();                        //33
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndIfForwarderChecked.ToList();                       //34
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndIfForwarderChecked.ToList();              //35
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryEKMTAndIfForwarderChecked.ToList();                     //36
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndEKMTAndIfForwarderChecked.ToList();                 //37
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndEKMTAndIfForwarderChecked.ToList();       //38
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndEKMTAndIfForwarderChecked.ToList();       //39
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryCMRAndIfForwarderChecked.ToList();                      //40
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndCMRAndIfForwarderChecked.ToList();                 //41
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndCMRAndIfForwarderChecked.ToList();             //42
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndCMRAndIfForwarderChecked.ToList();            //43
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryEKMTAndCMRAndIfForwarderChecked.ToList();            //44
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndEKMTAndCMRAndIfForwarderChecked.ToList();            //45
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndEKMTAndCMRAndIfForwarderChecked.ToList();            //46
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndEKMTAndCMRAndIfForwarderChecked.ToList();            //47
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryTURAndIfForwarderChecked.ToList();            //48
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndTURAndIfForwarderChecked.ToList();                        //49
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndTURAndIfForwarderChecked.ToList();                       //50
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndTURAndIfForwarderChecked.ToList();              //51
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryEKMTAndTURAndIfForwarderChecked.ToList();                     //52
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndEKMTAndTURAndIfForwarderChecked.ToList();                 //53
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndEKMTAndTURAndIfForwarderChecked.ToList();       //54
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndEKMTAndTURAndIfForwarderChecked.ToList();       //55
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryCMRAndIfForwarderChecked.ToList();                      //56
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndCMRAndTURAndIfForwarderChecked.ToList();                 //57
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndCMRAndTURAndIfForwarderChecked.ToList();             //58
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndCMRAndTURAndIfForwarderChecked.ToList();            //59
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryEKMTAndCMRAndIfForwarderChecked.ToList();                        //60
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();            //61
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();            //62
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = OnlyFiltersQueryADAndZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();       //63
                        }
                       
                        transporterShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                        transporterShowDataGridView.Columns[1].HeaderText = "Повна Назва";
                        transporterShowDataGridView.Columns[2].HeaderText = "Скорочена Назва";
                        transporterShowDataGridView.Columns[3].HeaderText = "П.І.Б. Директора";
                        transporterShowDataGridView.Columns[4].HeaderText = "Фізична адреса";
                        transporterShowDataGridView.Columns[5].HeaderText = "Юридична адреса";
                        transporterShowDataGridView.Columns[6].HeaderText = "Статус платника податку";
                        transporterShowDataGridView.Columns[7].HeaderText = "На основі";

                        transporterShowDataGridView.Update();

                    }
                    #endregion

                    // Filter Countries And Vehicles
                    #region  CountriesAndVehicles
                    if (countriesIDs.Count != 0 && vehiclesIDs.Count != 0 && filtersStates == null)
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
                        transporterShowDataGridView.Update();
                    }
                    #endregion

                    // Filter Filters And Countries
                    #region FiltersAndCountries
                    if (countriesIDs.Count != 0 && vehiclesIDs.Count == 0 && filtersStates != null)
                    {
                        //All only Filters Queries
                        #region Queries


                        //1
                        var FiltersAndCountriesQueryADChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked
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
                                };
                        //2
                        var FiltersAndCountriesQueryZbornyChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked
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
                               };
                        //3
                        var FiltersAndCountriesQueryADAndZbornyChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked
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
                              };
                        //4
                        var FiltersAndCountriesQueryEKMTChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked
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
                              };
                        //5
                        var FiltersAndCountriesQueryADAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked
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
                             };
                        //6
                        var FiltersAndCountriesQueryZbornyAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked
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
                             };
                        //7
                        var FiltersAndCountriesQueryADAndZbornyAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked
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
                             };
                        //8
                        var FiltersAndCountriesQueryCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked
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
                              };
                        //9
                        var FiltersAndCountriesQueryADAndCMRChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked
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
                               };
                        //10
                        var FiltersAndCountriesQueryZbornyAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked
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
                              };
                        //11
                        var FiltersAndCountriesQueryADAndZbornyAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked
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
                              };

                        //12
                        var FiltersAndCountriesQueryEKMTAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
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
                              };
                        //13
                        var FiltersAndCountriesQueryADAndEKMTAndCMRChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
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
                             };

                        //14
                        var FiltersAndCountriesQueryZbornyAndEKMTAndCMRChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
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
                               };
                        //15
                        var FiltersAndCountriesQueryADAndZbornyAndEKMTAndCMRChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
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
                             };

                        //TUR
                        //16
                        var FiltersAndCountriesQueryTURChecked =
                                    from t in db.Transporters
                                    where t.Filter.TUR == TURChecked
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
                                };

                        //17
                        var FiltersAndCountriesQueryADAndTURChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.TUR == TURChecked
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
                                };
                        //18
                        var FiltersAndCountriesQueryZbornyAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked
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
                               };
                        //19
                        var FiltersAndCountriesQueryADAndZbornyAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked
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
                              };
                        //20
                        var FiltersAndCountriesQueryEKMTAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
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
                              };
                        //21
                        var FiltersAndCountriesQueryADAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
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
                             };
                        //22
                        var FiltersAndCountriesQueryZbornyAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked 
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
                             };
                        //23
                        var FiltersAndCountriesQueryADAndZbornyAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
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
                             };
                        //24
                        var FiltersAndCountriesQueryCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                              };
                        //25
                        var FiltersAndCountriesQueryADAndCMRAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                               };
                        //26
                        var FiltersAndCountriesQueryZbornyAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                              };
                        //27
                        var FiltersAndCountriesQueryADAndZbornyAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                              };

                        //28
                        var FiltersAndCountriesQueryEKMTAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                              };
                        //29
                        var FiltersAndCountriesQueryADAndEKMTAndCMRAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                             };

                        //30
                        var FiltersAndCountriesQueryZbornyAndEKMTAndCMRAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                               };
                        //31
                        var FiltersAndCountriesQueryADAndZbornyAndEKMTAndCMRAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
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
                             };

                        //IFforwarder
                        //32
                        var FiltersAndCountriesQueryIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.IfForwarder == IfForwarderChecked
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
                                };

                        //33
                        var FiltersAndCountriesQueryADAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                                };
                        //34
                        var FiltersAndCountriesQueryZbornyAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //35
                        var FiltersAndCountriesQueryADAndZbornyAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //36
                        var FiltersAndCountriesQueryEKMTAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //37
                        var FiltersAndCountriesQueryADAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //38
                        var FiltersAndCountriesQueryZbornyAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //39
                        var FiltersAndCountriesQueryADAndZbornyAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //40
                        var FiltersAndCountriesQueryCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //41
                        var FiltersAndCountriesQueryADAndCMRAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //42
                        var FiltersAndCountriesQueryZbornyAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //43
                        var FiltersAndCountriesQueryADAndZbornyAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };

                        //44
                        var FiltersAndCountriesQueryEKMTAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //45
                        var FiltersAndCountriesQueryADAndEKMTAndCMRAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };

                        //46
                        var FiltersAndCountriesQueryZbornyAndEKMTAndCMRAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //47
                        var FiltersAndCountriesQueryADAndZbornyAndEKMTAndCMRAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //48
                        var FiltersAndCountriesQueryTURAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                                };

                        //49
                        var FiltersAndCountriesQueryADAndTURAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                                };
                        //50
                        var FiltersAndCountriesQueryZbornyAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //51
                        var FiltersAndCountriesQueryADAndZbornyAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //52
                        var FiltersAndCountriesQueryEKMTAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //53
                        var FiltersAndCountriesQueryADAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //54
                        var FiltersAndCountriesQueryZbornyAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //55
                        var FiltersAndCountriesQueryADAndZbornyAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };
                        //56
                        var FiltersAndCountriesQueryCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //57
                        var FiltersAndCountriesQueryADAndCMRAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //58
                        var FiltersAndCountriesQueryZbornyAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //59
                        var FiltersAndCountriesQueryADAndZbornyAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };

                        //60
                        var FiltersAndCountriesQueryEKMTAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                              };
                        //61
                        var FiltersAndCountriesQueryADAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };

                        //62
                        var FiltersAndCountriesQueryZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                               };
                        //63
                        var FiltersAndCountriesQueryADAndZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
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
                             };

                        #endregion

                        if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)        // 0 0 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADChecked.ToList();                        //1
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyChecked.ToList();                       //2
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyChecked.ToList();              //3
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryEKMTChecked.ToList();                     //4
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndEKMTChecked.ToList();                 //5
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndEKMTChecked.ToList();       //6
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndEKMTChecked.ToList();       //7
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryCMRChecked.ToList();                      //8
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndCMRChecked.ToList();                 //9
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndCMRChecked.ToList();             //10
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndCMRChecked.ToList();            //11
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryEKMTAndCMRChecked.ToList();            //12
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndEKMTAndCMRChecked.ToList();            //13
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndEKMTAndCMRChecked.ToList();            //14
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndEKMTAndCMRChecked.ToList();            //15
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryTURChecked.ToList();            //16
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)        // 0 1 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndTURChecked.ToList();                        //17
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndTURChecked.ToList();                       //18
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndTURChecked.ToList();              //19
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryEKMTAndTURChecked.ToList();                     //20
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndEKMTAndTURChecked.ToList();                 //21
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndEKMTAndTURChecked.ToList();       //22
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndEKMTAndTURChecked.ToList();       //23
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryCMRChecked.ToList();                      //24
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndCMRAndTURChecked.ToList();                 //25
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndCMRAndTURChecked.ToList();             //26
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndCMRAndTURChecked.ToList();            //27
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryEKMTAndCMRChecked.ToList();                        //28
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndEKMTAndCMRAndTURChecked.ToList();            //29
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndEKMTAndCMRAndTURChecked.ToList();            //30
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndEKMTAndCMRAndTURChecked.ToList();            //31
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryIfForwarderChecked.ToList(); //32
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)  // 1 0 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndIfForwarderChecked.ToList();                        //33
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndIfForwarderChecked.ToList();                       //34
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndIfForwarderChecked.ToList();              //35
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryEKMTAndIfForwarderChecked.ToList();                     //36
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndEKMTAndIfForwarderChecked.ToList();                 //37
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndEKMTAndIfForwarderChecked.ToList();       //38
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndEKMTAndIfForwarderChecked.ToList();       //39
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryCMRAndIfForwarderChecked.ToList();                      //40
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndCMRAndIfForwarderChecked.ToList();                 //41
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndCMRAndIfForwarderChecked.ToList();             //42
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndCMRAndIfForwarderChecked.ToList();            //43
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryEKMTAndCMRAndIfForwarderChecked.ToList();            //44
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndEKMTAndCMRAndIfForwarderChecked.ToList();            //45
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndEKMTAndCMRAndIfForwarderChecked.ToList();            //46
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndEKMTAndCMRAndIfForwarderChecked.ToList();            //47
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryTURAndIfForwarderChecked.ToList();            //48
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndTURAndIfForwarderChecked.ToList();                        //49
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndTURAndIfForwarderChecked.ToList();                       //50
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndTURAndIfForwarderChecked.ToList();              //51
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryEKMTAndTURAndIfForwarderChecked.ToList();                     //52
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndEKMTAndTURAndIfForwarderChecked.ToList();                 //53
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndEKMTAndTURAndIfForwarderChecked.ToList();       //54
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndEKMTAndTURAndIfForwarderChecked.ToList();       //55
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryCMRAndIfForwarderChecked.ToList();                      //56
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndCMRAndTURAndIfForwarderChecked.ToList();                 //57
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndCMRAndTURAndIfForwarderChecked.ToList();             //58
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndCMRAndTURAndIfForwarderChecked.ToList();            //59
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryEKMTAndCMRAndIfForwarderChecked.ToList();                        //60
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();            //61
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();            //62
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesQueryADAndZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();       //63
                        }

                        transporterShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                        transporterShowDataGridView.Columns[1].HeaderText = "Повна Назва";
                        transporterShowDataGridView.Columns[2].HeaderText = "Скорочена Назва";
                        transporterShowDataGridView.Columns[3].HeaderText = "П.І.Б. Директора";
                        transporterShowDataGridView.Columns[4].HeaderText = "Фізична адреса";
                        transporterShowDataGridView.Columns[5].HeaderText = "Юридична адреса";
                        transporterShowDataGridView.Columns[6].HeaderText = "Статус платника податку";
                        transporterShowDataGridView.Columns[7].HeaderText = "На основі";

                        transporterShowDataGridView.Update();

                    }
                    #endregion

                    // Filter Filters And Vehicles
                    #region FiltersAndVehicles
                    if (countriesIDs.Count == 0 && vehiclesIDs.Count != 0 && filtersStates != null)
                    {
                        //All only Filters Queries
                        #region Queries
                        //1
                        var FiltersAndVehiclesQueryADChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked
                                    &&
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
                                };
                        //2
                        var FiltersAndVehiclesQueryZbornyChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked
                                    &&
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
                               };
                        //3
                        var FiltersAndVehiclesQueryADAndZbornyChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked
                                    &&
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
                              };
                        //4
                        var FiltersAndVehiclesQueryEKMTChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked
                                    &&
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
                              };
                        //5
                        var FiltersAndVehiclesQueryADAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked
                                    &&
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
                             };
                        //6
                        var FiltersAndVehiclesQueryZbornyAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked
                                    &&
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
                             };
                        //7
                        var FiltersAndVehiclesQueryADAndZbornyAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked
                                    &&
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
                             };
                        //8
                        var FiltersAndVehiclesQueryCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked
                                    &&
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
                              };
                        //9
                        var FiltersAndVehiclesQueryADAndCMRChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked
                                    &&
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
                               };
                        //10
                        var FiltersAndVehiclesQueryZbornyAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked
                                    &&
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
                              };
                        //11
                        var FiltersAndVehiclesQueryADAndZbornyAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked
                                    &&
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
                              };

                        //12
                        var FiltersAndVehiclesQueryEKMTAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
                                    &&
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
                              };
                        //13
                        var FiltersAndVehiclesQueryADAndEKMTAndCMRChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
                                    &&
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
                             };

                        //14
                        var FiltersAndVehiclesQueryZbornyAndEKMTAndCMRChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
                                    &&
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
                               };
                        //15
                        var FiltersAndVehiclesQueryADAndZbornyAndEKMTAndCMRChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
                                    &&
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
                             };

                        //TUR
                        //16
                        var FiltersAndVehiclesQueryTURChecked =
                                    from t in db.Transporters
                                    where t.Filter.TUR == TURChecked
                                    &&
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
                                };

                        //17
                        var FiltersAndVehiclesQueryADAndTURChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.TUR == TURChecked
                                    &&
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
                                };
                        //18
                        var FiltersAndVehiclesQueryZbornyAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked
                                    &&
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
                               };
                        //19
                        var FiltersAndVehiclesQueryADAndZbornyAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked
                                    &&
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
                              };
                        //20
                        var FiltersAndVehiclesQueryEKMTAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
                                    &&
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
                              };
                        //21
                        var FiltersAndVehiclesQueryADAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
                                    &&
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
                             };
                        //22
                        var FiltersAndVehiclesQueryZbornyAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
                                    &&
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
                             };
                        //23
                        var FiltersAndVehiclesQueryADAndZbornyAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
                                    &&
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
                             };
                        //24
                        var FiltersAndVehiclesQueryCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                    &&
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
                              };
                        //25
                        var FiltersAndVehiclesQueryADAndCMRAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                    &&
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
                               };
                        //26
                        var FiltersAndVehiclesQueryZbornyAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                    &&
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
                              };
                        //27
                        var FiltersAndVehiclesQueryADAndZbornyAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                    &&
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
                              };

                        //28
                        var FiltersAndVehiclesQueryEKMTAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                    &&
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
                              };
                        //29
                        var FiltersAndVehiclesQueryADAndEKMTAndCMRAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                    &&
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
                             };

                        //30
                        var FiltersAndVehiclesQueryZbornyAndEKMTAndCMRAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                    &&
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
                               };
                        //31
                        var FiltersAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                    &&
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
                             };

                        //IFforwarder
                        //32
                        var FiltersAndVehiclesQueryIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                                };

                        //33
                        var FiltersAndVehiclesQueryADAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                                };
                        //34
                        var FiltersAndVehiclesQueryZbornyAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                               };
                        //35
                        var FiltersAndVehiclesQueryADAndZbornyAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //36
                        var FiltersAndVehiclesQueryEKMTAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //37
                        var FiltersAndVehiclesQueryADAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };
                        //38
                        var FiltersAndVehiclesQueryZbornyAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };
                        //39
                        var FiltersAndVehiclesQueryADAndZbornyAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };
                        //40
                        var FiltersAndVehiclesQueryCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //41
                        var FiltersAndVehiclesQueryADAndCMRAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                               };
                        //42
                        var FiltersAndVehiclesQueryZbornyAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //43
                        var FiltersAndVehiclesQueryADAndZbornyAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };

                        //44
                        var FiltersAndVehiclesQueryEKMTAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //45
                        var FiltersAndVehiclesQueryADAndEKMTAndCMRAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };

                        //46
                        var FiltersAndVehiclesQueryZbornyAndEKMTAndCMRAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                               };
                        //47
                        var FiltersAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };
                        //48
                        var FiltersAndVehiclesQueryTURAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                                };

                        //49
                        var FiltersAndVehiclesQueryADAndTURAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                                };
                        //50
                        var FiltersAndVehiclesQueryZbornyAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                               };
                        //51
                        var FiltersAndVehiclesQueryADAndZbornyAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //52
                        var FiltersAndVehiclesQueryEKMTAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //53
                        var FiltersAndVehiclesQueryADAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };
                        //54
                        var FiltersAndVehiclesQueryZbornyAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };
                        //55
                        var FiltersAndVehiclesQueryADAndZbornyAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };
                        //56
                        var FiltersAndVehiclesQueryCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //57
                        var FiltersAndVehiclesQueryADAndCMRAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                               };
                        //58
                        var FiltersAndVehiclesQueryZbornyAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //59
                        var FiltersAndVehiclesQueryADAndZbornyAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };

                        //60
                        var FiltersAndVehiclesQueryEKMTAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                              };
                        //61
                        var FiltersAndVehiclesQueryADAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };

                        //62
                        var FiltersAndVehiclesQueryZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                               };
                        //63
                        var FiltersAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                             };

                        #endregion

                        if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)        // 0 0 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADChecked.ToList();                        //1
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyChecked.ToList();                       //2
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyChecked.ToList();              //3
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryEKMTChecked.ToList();                     //4
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndEKMTChecked.ToList();                 //5
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndEKMTChecked.ToList();       //6
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndEKMTChecked.ToList();       //7
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryCMRChecked.ToList();                      //8
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndCMRChecked.ToList();                 //9
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndCMRChecked.ToList();             //10
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndCMRChecked.ToList();            //11
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryEKMTAndCMRChecked.ToList();            //12
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndEKMTAndCMRChecked.ToList();            //13
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndEKMTAndCMRChecked.ToList();            //14
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndEKMTAndCMRChecked.ToList();            //15
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryTURChecked.ToList();            //16
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)        // 0 1 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndTURChecked.ToList();                        //17
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndTURChecked.ToList();                       //18
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndTURChecked.ToList();              //19
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryEKMTAndTURChecked.ToList();                     //20
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndEKMTAndTURChecked.ToList();                 //21
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndEKMTAndTURChecked.ToList();       //22
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndEKMTAndTURChecked.ToList();       //23
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryCMRChecked.ToList();                      //24
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndCMRAndTURChecked.ToList();                 //25
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndCMRAndTURChecked.ToList();             //26
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndCMRAndTURChecked.ToList();            //27
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryEKMTAndCMRChecked.ToList();                        //28
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndEKMTAndCMRAndTURChecked.ToList();            //29
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndEKMTAndCMRAndTURChecked.ToList();            //30
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndTURChecked.ToList();            //31
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryIfForwarderChecked.ToList(); //32
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)  // 1 0 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndIfForwarderChecked.ToList();                        //33
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndIfForwarderChecked.ToList();                       //34
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndIfForwarderChecked.ToList();              //35
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryEKMTAndIfForwarderChecked.ToList();                     //36
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndEKMTAndIfForwarderChecked.ToList();                 //37
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndEKMTAndIfForwarderChecked.ToList();       //38
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndEKMTAndIfForwarderChecked.ToList();       //39
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryCMRAndIfForwarderChecked.ToList();                      //40
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndCMRAndIfForwarderChecked.ToList();                 //41
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndCMRAndIfForwarderChecked.ToList();             //42
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndCMRAndIfForwarderChecked.ToList();            //43
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryEKMTAndCMRAndIfForwarderChecked.ToList();            //44
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndEKMTAndCMRAndIfForwarderChecked.ToList();            //45
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndEKMTAndCMRAndIfForwarderChecked.ToList();            //46
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndIfForwarderChecked.ToList();            //47
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryTURAndIfForwarderChecked.ToList();            //48
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndTURAndIfForwarderChecked.ToList();                        //49
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndTURAndIfForwarderChecked.ToList();                       //50
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndTURAndIfForwarderChecked.ToList();              //51
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryEKMTAndTURAndIfForwarderChecked.ToList();                     //52
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndEKMTAndTURAndIfForwarderChecked.ToList();                 //53
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndEKMTAndTURAndIfForwarderChecked.ToList();       //54
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndEKMTAndTURAndIfForwarderChecked.ToList();       //55
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryCMRAndIfForwarderChecked.ToList();                      //56
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndCMRAndTURAndIfForwarderChecked.ToList();                 //57
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndCMRAndTURAndIfForwarderChecked.ToList();             //58
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndCMRAndTURAndIfForwarderChecked.ToList();            //59
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryEKMTAndCMRAndIfForwarderChecked.ToList();                        //60
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();            //61
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();            //62
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();       //63
                        }

                        transporterShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                        transporterShowDataGridView.Columns[1].HeaderText = "Повна Назва";
                        transporterShowDataGridView.Columns[2].HeaderText = "Скорочена Назва";
                        transporterShowDataGridView.Columns[3].HeaderText = "П.І.Б. Директора";
                        transporterShowDataGridView.Columns[4].HeaderText = "Фізична адреса";
                        transporterShowDataGridView.Columns[5].HeaderText = "Юридична адреса";
                        transporterShowDataGridView.Columns[6].HeaderText = "Статус платника податку";
                        transporterShowDataGridView.Columns[7].HeaderText = "На основі";

                        transporterShowDataGridView.Update();

                    }
                    #endregion

                    // Filter Filters And Countries And Vehicles
                    #region FiltersAndCountriesAndVehicles
                    if (countriesIDs.Count != 0 && vehiclesIDs.Count != 0 && filtersStates != null)
                    {
                        //All only Filters Queries
                        #region Queries

                        //1
                        var FiltersAndCountriesAndVehiclesQueryADChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked
                                    &&
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
                                };
                        //2
                        var FiltersAndCountriesAndVehiclesQueryZbornyChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked
                                   &&
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
                               };
                        //3
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked
                                  &&
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
                              };
                        //4
                        var FiltersAndCountriesAndVehiclesQueryEKMTChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked
                                  &&
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
                              };
                        //5
                        var FiltersAndCountriesAndVehiclesQueryADAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked
                                 &&
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
                             };
                        //6
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked
                                 &&
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
                             };
                        //7
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked
                                 &&
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
                             };
                        //8
                        var FiltersAndCountriesAndVehiclesQueryCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked
                                  &&
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
                              };
                        //9
                        var FiltersAndCountriesAndVehiclesQueryADAndCMRChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked
                                   &&
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
                               };
                        //10
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked
                                  &&
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
                              };
                        //11
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked
                                  &&
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
                              };

                        //12
                        var FiltersAndCountriesAndVehiclesQueryEKMTAndCMRChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
                                  &&
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
                              };
                        //13
                        var FiltersAndCountriesAndVehiclesQueryADAndEKMTAndCMRChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
                                 &&
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
                             };

                        //14
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndCMRChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
                                   &&
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
                               };
                        //15
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndCMRChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked
                                 &&
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
                             };

                        //TUR
                        //16
                        var FiltersAndCountriesAndVehiclesQueryTURChecked =
                                    from t in db.Transporters
                                    where t.Filter.TUR == TURChecked
                                    &&
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
                                };

                        //17
                        var FiltersAndCountriesAndVehiclesQueryADAndTURChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.TUR == TURChecked
                                    &&
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
                                };
                        //18
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked
                                   &&
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
                               };
                        //19
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked
                                  &&
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
                              };
                        //20
                        var FiltersAndCountriesAndVehiclesQueryEKMTAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
                                  &&
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
                              };
                        //21
                        var FiltersAndCountriesAndVehiclesQueryADAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
                                 &&
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
                             };
                        //22
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
                                 &&
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
                             };
                        //23
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked
                                 &&
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
                             };
                        //24
                        var FiltersAndCountriesAndVehiclesQueryCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                  &&
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
                              };
                        //25
                        var FiltersAndCountriesAndVehiclesQueryADAndCMRAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                   &&
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
                               };
                        //26
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                  &&
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
                              };
                        //27
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                  &&
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
                              };

                        //28
                        var FiltersAndCountriesAndVehiclesQueryEKMTAndCMRAndTURChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                  &&
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
                              };
                        //29
                        var FiltersAndCountriesAndVehiclesQueryADAndEKMTAndCMRAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                 &&
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
                             };

                        //30
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndCMRAndTURChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                   &&
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
                               };
                        //31
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndTURChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked
                                 &&
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
                             };

                        //IFforwarder
                        //32
                        var FiltersAndCountriesAndVehiclesQueryIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                                };

                        //33
                        var FiltersAndCountriesAndVehiclesQueryADAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                                };
                        //34
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.IfForwarder == IfForwarderChecked
                                   &&
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
                               };
                        //35
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //36
                        var FiltersAndCountriesAndVehiclesQueryEKMTAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //37
                        var FiltersAndCountriesAndVehiclesQueryADAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };
                        //38
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };
                        //39
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };
                        //40
                        var FiltersAndCountriesAndVehiclesQueryCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //41
                        var FiltersAndCountriesAndVehiclesQueryADAndCMRAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                   &&
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
                               };
                        //42
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //43
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };

                        //44
                        var FiltersAndCountriesAndVehiclesQueryEKMTAndCMRAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //45
                        var FiltersAndCountriesAndVehiclesQueryADAndEKMTAndCMRAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };

                        //46
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndCMRAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                   &&
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
                               };
                        //47
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };
                        //48
                        var FiltersAndCountriesAndVehiclesQueryTURAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                                };

                        //49
                        var FiltersAndCountriesAndVehiclesQueryADAndTURAndIfForwarderChecked =
                                    from t in db.Transporters
                                    where t.Filter.AD == ADChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                    &&
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
                                };
                        //50
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                   &&
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
                               };
                        //51
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //52
                        var FiltersAndCountriesAndVehiclesQueryEKMTAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //53
                        var FiltersAndCountriesAndVehiclesQueryADAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };
                        //54
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };
                        //55
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };
                        //56
                        var FiltersAndCountriesAndVehiclesQueryCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //57
                        var FiltersAndCountriesAndVehiclesQueryADAndCMRAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.AD == ADChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                   &&
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
                               };
                        //58
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //59
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };

                        //60
                        var FiltersAndCountriesAndVehiclesQueryEKMTAndCMRAndTURAndIfForwarderChecked =
                                  from t in db.Transporters
                                  where t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                  &&
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
                              };
                        //61
                        var FiltersAndCountriesAndVehiclesQueryADAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };

                        //62
                        var FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                   from t in db.Transporters
                                   where t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                   &&
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
                               };
                        //63
                        var FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked =
                                 from t in db.Transporters
                                 where t.Filter.AD == ADChecked && t.Filter.Zborny == ZbornyChecked && t.Filter.EKMT == EKMTChecked && t.Filter.CMR == CMRChecked && t.Filter.TUR == TURChecked && t.Filter.IfForwarder == IfForwarderChecked
                                 &&
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
                             };

                        #endregion

                        if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)        // 0 0 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADChecked.ToList();                        //1
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyChecked.ToList();                       //2
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyChecked.ToList();              //3
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryEKMTChecked.ToList();                     //4
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndEKMTChecked.ToList();                 //5
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTChecked.ToList();       //6
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTChecked.ToList();       //7
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryCMRChecked.ToList();                      //8
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndCMRChecked.ToList();                 //9
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndCMRChecked.ToList();             //10
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndCMRChecked.ToList();            //11
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 0 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryEKMTAndCMRChecked.ToList();            //12
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 0 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndEKMTAndCMRChecked.ToList();            //13
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 0 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndCMRChecked.ToList();            //14
                        }
                        else if (filtersStates[0] == null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 0 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndCMRChecked.ToList();            //15
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryTURChecked.ToList();            //16
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)        // 0 1 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndTURChecked.ToList();                        //17
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndTURChecked.ToList();                       //18
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndTURChecked.ToList();              //19
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryEKMTAndTURChecked.ToList();                     //20
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndEKMTAndTURChecked.ToList();                 //21
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndTURChecked.ToList();       //22
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndTURChecked.ToList();       //23
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryCMRChecked.ToList();                      //24
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndCMRAndTURChecked.ToList();                 //25
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndCMRAndTURChecked.ToList();             //26
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndCMRAndTURChecked.ToList();            //27
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 0 1 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryEKMTAndCMRChecked.ToList();                        //28
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 0 1 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndEKMTAndCMRAndTURChecked.ToList();            //29
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 0 1 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndCMRAndTURChecked.ToList();            //30
                        }
                        else if (filtersStates[0] == null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 0 1 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndTURChecked.ToList();            //31
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryIfForwarderChecked.ToList(); //32
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)  // 1 0 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndIfForwarderChecked.ToList();                        //33
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndIfForwarderChecked.ToList();                       //34
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndIfForwarderChecked.ToList();              //35
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryEKMTAndIfForwarderChecked.ToList();                     //36
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndEKMTAndIfForwarderChecked.ToList();                 //37
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndIfForwarderChecked.ToList();       //38
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndIfForwarderChecked.ToList();       //39
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryCMRAndIfForwarderChecked.ToList();                      //40
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndCMRAndIfForwarderChecked.ToList();                 //41
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndCMRAndIfForwarderChecked.ToList();             //42
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndCMRAndIfForwarderChecked.ToList();            //43
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 0 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryEKMTAndCMRAndIfForwarderChecked.ToList();            //44
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 0 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndEKMTAndCMRAndIfForwarderChecked.ToList();            //45
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 0 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndCMRAndIfForwarderChecked.ToList();            //46
                        }
                        else if (filtersStates[0] != null && filtersStates[1] == null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 0 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndIfForwarderChecked.ToList();            //47
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 0 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryTURAndIfForwarderChecked.ToList();            //48
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 0 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndTURAndIfForwarderChecked.ToList();                        //49
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 0 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndTURAndIfForwarderChecked.ToList();                       //50
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 0 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndTURAndIfForwarderChecked.ToList();              //51
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 0 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryEKMTAndTURAndIfForwarderChecked.ToList();                     //52
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 0 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndEKMTAndTURAndIfForwarderChecked.ToList();                 //53
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 0 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndTURAndIfForwarderChecked.ToList();       //54
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] == null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 0 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndTURAndIfForwarderChecked.ToList();       //55
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 1 0 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryCMRAndIfForwarderChecked.ToList();                      //56
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 1 0 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndCMRAndTURAndIfForwarderChecked.ToList();                 //57
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 1 0 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndCMRAndTURAndIfForwarderChecked.ToList();             //58
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] == null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 1 0 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndCMRAndTURAndIfForwarderChecked.ToList();            //59
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] == null)   // 1 1 1 1 0 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryEKMTAndCMRAndIfForwarderChecked.ToList();                        //60
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] == null && filtersStates[5] != null)   // 1 1 1 1 0 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();            //61
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] == null)   // 1 1 1 1 1 0
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();            //62
                        }
                        else if (filtersStates[0] != null && filtersStates[1] != null && filtersStates[2] != null && filtersStates[3] != null && filtersStates[4] != null && filtersStates[5] != null)   // 1 1 1 1 1 1
                        {
                            transporterShowDataGridView.DataSource = FiltersAndCountriesAndVehiclesQueryADAndZbornyAndEKMTAndCMRAndTURAndIfForwarderChecked.ToList();       //63
                        }

                        transporterShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                        transporterShowDataGridView.Columns[1].HeaderText = "Повна Назва";
                        transporterShowDataGridView.Columns[2].HeaderText = "Скорочена Назва";
                        transporterShowDataGridView.Columns[3].HeaderText = "П.І.Б. Директора";
                        transporterShowDataGridView.Columns[4].HeaderText = "Фізична адреса";
                        transporterShowDataGridView.Columns[5].HeaderText = "Юридична адреса";
                        transporterShowDataGridView.Columns[6].HeaderText = "Статус платника податку";
                        transporterShowDataGridView.Columns[7].HeaderText = "На основі";

                        transporterShowDataGridView.Update();

                    }
                    #endregion

                    // Nothing to filter 
                    //GET ALL TRANSPORTER
                    #region GetAllTransporters
                    if (countriesIDs.Count == 0 && vehiclesIDs.Count == 0 && filtersStates == null)
                    {
                        ShowTransporter();
                    }
                    #endregion
                }
                countriesIDs.Clear();
                vehiclesIDs.Clear();
                filtersStates.Clear();
            }
        }

        void ShowTransporterSearch()
        {

            var text = transporterShowSearchTextBox.Text;
            using (var db = new AtlantSovtContext())
            {
                var query =
                from t in db.Transporters
                where t.FullName.Contains(text) || t.ShortName.Contains(text) || t.Director.Contains(text) || t.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || t.TransporterContacts.Any(c => c.Email.Contains(text)) || t.TransporterContacts.Any(c => c.ContactPerson.Contains(text))
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

            } transporterShowDataGridView.Update();

        }

        #endregion

        //Add
        #region Add
        void AddTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                if (nameTransporterAddTextBox.Text != "" || directorTransporterAddTextBox.Text != "" )
                {
                    var new_FullName = nameTransporterAddTextBox.Text;
                    var new_ShortName = shortNameTransporterAddTextBox.Text;
                    var new_Director = directorTransporterAddTextBox.Text;
                    var new_PhysicalAddress = physicalAddressTransporterAddTextBox.Text;
                    var new_GeographyAddress = geographyAddressTransporterAddTextBox.Text;
                    var new_ContractType = originalTransporterAddCheckBox.Checked;
                    var new_Comment = commentTransporterAddTextBox.Text;

                    bool? new_IfForwarder = null;
                    bool? new_TUR = null;
                    bool? new_CMR = null;
                    bool? new_EKMT = null;
                    bool? new_Zborny = null;
                    bool? new_AD = null;

                    if (transporterAddFiltersSelectIfForwarderCheckBox.CheckState != CheckState.Indeterminate)
                    {
                        new_IfForwarder = transporterAddFiltersSelectIfForwarderCheckBox.Checked;
                    }
                    if (transporterAddFiltersSelectTURCheckBox.CheckState != CheckState.Indeterminate)
                    {
                        new_TUR = transporterAddFiltersSelectTURCheckBox.Checked;
                    }
                    if (transporterAddFiltersSelectCMRCheckBox.CheckState != CheckState.Indeterminate)
                    {
                        new_CMR = transporterAddFiltersSelectCMRCheckBox.Checked;
                    }
                    if (transporterAddFiltersSelectEKMTCheckBox.CheckState != CheckState.Indeterminate)
                    {
                        new_EKMT = transporterAddFiltersSelectEKMTCheckBox.Checked;
                    }
                    if (transporterAddFiltersSelectZbornyCheckBox.CheckState != CheckState.Indeterminate)
                    {
                        new_Zborny = transporterAddFiltersSelectZbornyCheckBox.Checked;
                    }
                    if (transporterAddFiltersSelectADCheckBox.CheckState != CheckState.Indeterminate)
                    {
                        new_AD = transporterAddFiltersSelectADCheckBox.Checked;
                    }  
                    long new_WorkDocumentId = 0;
                    long new_TaxPayerStatusId = 0;

                    if (transporterWorkDocument != null)
                    {
                        transporterIsWorkDocumentExist = true;
                        new_WorkDocumentId = transporterWorkDocument.Id;
                    }
                    else
                    {
                        transporterIsWorkDocumentExist = false;
                    }
                    if (transporterTaxPayerStatus != null)
                    {
                        transporterIsTaxPayerStatusExist = true;
                        new_TaxPayerStatusId = transporterTaxPayerStatus.Id;
                    }
                    else
                    {
                        transporterIsTaxPayerStatusExist = false;
                    }

                    Transporter New_Transporter = new Transporter();

                    if (!transporterIsWorkDocumentExist && !transporterIsTaxPayerStatusExist)
                    {
                        New_Transporter = new Transporter
                        {
                            FullName = new_FullName,
                            ShortName = new_ShortName,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeographyAddress = new_GeographyAddress,
                            ContractType = new_ContractType,
                            Comment = new_Comment,
                        };
                    }

                    else if (transporterIsWorkDocumentExist && transporterIsTaxPayerStatusExist)
                    {
                        New_Transporter = new Transporter
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
                    }

                    else if (transporterIsWorkDocumentExist && !transporterIsTaxPayerStatusExist)
                    {
                        New_Transporter = new Transporter
                        {
                            FullName = new_FullName,
                            ShortName = new_ShortName,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeographyAddress = new_GeographyAddress,
                            WorkDocumentId = new_WorkDocumentId,
                            ContractType = new_ContractType,
                            Comment = new_Comment,
                        };
                    }

                    else if (!transporterIsWorkDocumentExist && transporterIsTaxPayerStatusExist)
                    {
                        New_Transporter = new Transporter
                        {
                            FullName = new_FullName,
                            ShortName = new_ShortName,
                            Director = new_Director,
                            PhysicalAddress = new_PhysicalAddress,
                            GeographyAddress = new_GeographyAddress,
                            TaxPayerStatusId = new_TaxPayerStatusId,
                            ContractType = new_ContractType,
                            Comment = new_Comment,
                        };
                    }

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
                    MessageBox.Show("Одне з обов'язкових полів не заповнено");
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
            if (taxPayerStatusTransporterAddComboBox.Text != "")
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
            else 
            {
                transporterTaxPayerStatus = null;
            }
        }

        void SplitLoadWorkDocumentTransporterAddInfo()
        {
            if (workDocumentTransporterAddComboBox.Text != "")
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = workDocumentTransporterAddComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterWorkDocument = db.WorkDocuments.Find(id);
            }
            else
            {
                transporterWorkDocument = null;
            }
        }
        #endregion

        //Update
        #region Update

        void ClearAllBoxesTransporterUpdate()
        {
            workDocumentTransporterUpdateComboBox.Items.Clear();
            taxPayerStatusTransporterUpdateComboBox.Items.Clear();
            nameTransporterUpdateTextBox.Clear();
            directorTransporterUpdateTextBox.Clear();            
            physicalAddressTransporterUpdateTextBox.Clear();
            geographyAddressTransporterUpdateTextBox.Clear();
            commentTransporterUpdateTextBox.Clear();
            shortNameTransporterUpdateTextBox.Clear();

            transporterUpdateFiltersSelectIfForwarderCheckBox.CheckState = CheckState.Indeterminate;
            transporterUpdateFiltersSelectADCheckBox.CheckState = CheckState.Indeterminate;
            transporterUpdateFiltersSelectTURCheckBox.CheckState = CheckState.Indeterminate;
            transporterUpdateFiltersSelectZbornyCheckBox.CheckState = CheckState.Indeterminate;
            transporterUpdateFiltersSelectCMRCheckBox.CheckState = CheckState.Indeterminate;
            transporterUpdateFiltersSelectEKMTCheckBox.CheckState = CheckState.Indeterminate;
        }

        void SplitUpdateTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = selectTransporterUpdateComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporter = db.Transporters.Find(id);
                filters = db.Filters.Find(transporter.Id);
                if (transporter != null)
                {
                    nameTransporterUpdateTextBox.Text = Convert.ToString(transporter.FullName);
                    shortNameTransporterUpdateTextBox.Text = Convert.ToString(transporter.ShortName);
                    directorTransporterUpdateTextBox.Text = Convert.ToString(transporter.Director);
                    physicalAddressTransporterUpdateTextBox.Text = Convert.ToString(transporter.PhysicalAddress);
                    geographyAddressTransporterUpdateTextBox.Text = Convert.ToString(transporter.GeographyAddress);
                    commentTransporterUpdateTextBox.Text = Convert.ToString(transporter.Comment);

                    if (transporter.WorkDocument != null) 
                    {
                        workDocumentTransporterUpdateComboBox.SelectedIndex = Convert.ToInt32(transporter.WorkDocumentId - 1);
                    }
                    else 
                    {
                        workDocumentTransporterUpdateComboBox.Text = "";
                    }
                    if (transporter.TaxPayerStatu != null)
                    {
                        taxPayerStatusTransporterUpdateComboBox.SelectedIndex = Convert.ToInt32(transporter.TaxPayerStatusId - 1);
                    }
                    else
                    {
                        taxPayerStatusTransporterUpdateComboBox.Text = "";
                    }

                    originalTransporterUpdateCheckBox.Checked = transporter.ContractType.Value;
                    faxTransporterUpdateCheckBox.Checked = !transporter.ContractType.Value;

                    if (transporter.Filter.IfForwarder != null)
                    {
                        transporterUpdateFiltersSelectIfForwarderCheckBox.Checked = Convert.ToBoolean(transporter.Filter.IfForwarder.Value);
                        if (transporter.Filter.IfForwarder.Value)
                        {
                            transporterUpdateFiltersSelectIfForwarderCheckBox.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            transporterUpdateFiltersSelectIfForwarderCheckBox.CheckState = CheckState.Unchecked;
                        }
                    }
                    if (transporter.Filter.TUR != null)
                    {
                        transporterUpdateFiltersSelectTURCheckBox.Checked = Convert.ToBoolean(transporter.Filter.TUR.Value);
                        if (transporter.Filter.TUR.Value)
                        {
                            transporterUpdateFiltersSelectTURCheckBox.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            transporterUpdateFiltersSelectTURCheckBox.CheckState = CheckState.Unchecked;
                        }
                    }
                    if (transporter.Filter.CMR != null)
                    {
                        transporterUpdateFiltersSelectCMRCheckBox.Checked = Convert.ToBoolean(transporter.Filter.CMR.Value);
                        if (transporter.Filter.CMR.Value)
                        {
                            transporterUpdateFiltersSelectCMRCheckBox.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            transporterUpdateFiltersSelectCMRCheckBox.CheckState = CheckState.Unchecked;
                        }
                    }
                    if (transporter.Filter.EKMT != null)
                    {
                        transporterUpdateFiltersSelectEKMTCheckBox.Checked = Convert.ToBoolean(transporter.Filter.EKMT.Value);
                        if (transporter.Filter.EKMT.Value)
                        {
                            transporterUpdateFiltersSelectEKMTCheckBox.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            transporterUpdateFiltersSelectEKMTCheckBox.CheckState = CheckState.Unchecked;
                        }

                    }
                    if (transporter.Filter.Zborny != null)
                    {
                        transporterUpdateFiltersSelectZbornyCheckBox.Checked = Convert.ToBoolean(transporter.Filter.Zborny.Value);
                        if (transporter.Filter.Zborny.Value)
                        {
                            transporterUpdateFiltersSelectZbornyCheckBox.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            transporterUpdateFiltersSelectZbornyCheckBox.CheckState = CheckState.Unchecked;
                        }
                    }
                    if (transporter.Filter.AD != null)
                    {
                        transporterUpdateFiltersSelectADCheckBox.Checked = Convert.ToBoolean(transporter.Filter.AD.Value);
                        if (transporter.Filter.AD.Value)
                        {
                            transporterUpdateFiltersSelectADCheckBox.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            transporterUpdateFiltersSelectADCheckBox.CheckState = CheckState.Unchecked;
                        }
                    }
                }
                transporterFullNameChanged = transporterDirectorChanged = transporterPhysicalAddressChanged = transporterGeographyAddressChanged = transporterCommentChanged = transporterWorkDocumentChanged = transporterTaxPayerStatusChanged = transporterFiltersChanged = transporterOriginalChanged = transporterFaxChanged = false;
            }
        }

        void LoadSelectTransporterUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (selectTransporterDiapasoneUpdateComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = selectTransporterDiapasoneUpdateComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        selectTransporterUpdateComboBox.Items.Add(item.FullName + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadDiasoneTransporterUpdateInfoCombobox()
        {
            selectTransporterDiapasoneUpdateComboBox.Items.Clear();
            selectTransporterUpdateComboBox.Items.Clear();
            selectTransporterUpdateComboBox.Text = "";
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double transporterPart = 0;
                if ((from c in db.Transporters select c.Id).Count() != 0)
                {
                    long transporterCount = (from c in db.Transporters select c.Id).Max();
                    if (transporterCount % part == 0)
                    {
                        transporterPart = transporterCount / part;
                    }
                    else
                    {
                        transporterPart = (transporterCount / part) + 1;
                    }

                    for (int i = 0; i < transporterPart; i++)
                    {
                        selectTransporterDiapasoneUpdateComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    selectTransporterDiapasoneUpdateComboBox.DroppedDown = true;
                    selectTransporterUpdateComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void LoadWorkDocumentTransporterUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from w in db.WorkDocuments
                            orderby w.Id
                            select w;
                foreach (var item in query)
                {
                    workDocumentTransporterUpdateComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void LoadTaxPayerStatusTransporterUpdateInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.TaxPayerStatus
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    taxPayerStatusTransporterUpdateComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadWorkDocumentTransporterUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = workDocumentTransporterUpdateComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterWorkDocument = db.WorkDocuments.Find(id);
            }
        }

        void SplitLoadTaxPayerStatusTransporterUpdateInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = taxPayerStatusTransporterUpdateComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterTaxPayerStatus = db.TaxPayerStatus.Find(id);
            }
        }

        void UpdateTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                //якщо хоча б один з флагів = true
                if (transporterFullNameChanged || transporterDirectorChanged || transporterPhysicalAddressChanged || transporterGeographyAddressChanged || transporterCommentChanged || transporterWorkDocumentChanged || transporterTaxPayerStatusChanged || transporterWorkDocumentChanged || transporterTaxPayerStatusChanged || transporterOriginalChanged || transporterFaxChanged || transporterFiltersChanged)
                {
                    if (transporterFullNameChanged)
                    {
                        transporter.FullName = nameTransporterUpdateTextBox.Text;
                    }
                    if (transporterShortNameChanged)
                    {
                        transporter.ShortName = shortNameTransporterUpdateTextBox.Text;
                    }
                    if (transporterDirectorChanged)
                    {
                        transporter.Director = directorTransporterUpdateTextBox.Text;
                    }                    
                    if (transporterPhysicalAddressChanged)
                    {
                        transporter.PhysicalAddress = physicalAddressTransporterUpdateTextBox.Text;
                    }
                    if (transporterGeographyAddressChanged)
                    {
                        transporter.GeographyAddress = geographyAddressTransporterUpdateTextBox.Text;
                    }
                    if (transporterCommentChanged)
                    {
                        transporter.Comment = commentTransporterUpdateTextBox.Text;
                    }
                    if (transporterWorkDocumentChanged)
                    {
                        if (workDocumentTransporterUpdateComboBox.Text != "")
                        {
                            transporter.WorkDocument = null;
                            transporter.WorkDocumentId = transporterWorkDocument.Id;
                        }
                        else 
                        {
                            transporter.WorkDocumentId = null;
                            transporter.WorkDocument = null;
                        }
                    }
                    if (transporterTaxPayerStatusChanged)
                    {
                        if (taxPayerStatusTransporterUpdateComboBox.Text != "")
                        {
                            transporter.TaxPayerStatu = null;
                            transporter.TaxPayerStatusId = transporterTaxPayerStatus.Id;
                        }
                        else 
                        {                           
                            transporter.TaxPayerStatusId = null;
                            transporter.TaxPayerStatu = null;
                        }
                    }
                    if (transporterFiltersChanged)
                    {                         

                        if (transporterUpdateFiltersSelectIfForwarderCheckBox.CheckState != CheckState.Indeterminate)
                        {
                            filters.IfForwarder = transporterUpdateFiltersSelectIfForwarderCheckBox.Checked;
                        }
                        else 
                        {
                            filters.IfForwarder = null;
                        }

                        if (transporterUpdateFiltersSelectTURCheckBox.CheckState != CheckState.Indeterminate)
                        {
                            filters.TUR = transporterUpdateFiltersSelectTURCheckBox.Checked;
                        }
                        else 
                        {
                            filters.TUR = null;
                        }

                        if (transporterUpdateFiltersSelectCMRCheckBox.CheckState != CheckState.Indeterminate)
                        {
                            filters.CMR = transporterUpdateFiltersSelectCMRCheckBox.Checked;
                        }
                        else 
                        {
                            filters.CMR = null;
                        }

                        if (transporterUpdateFiltersSelectEKMTCheckBox.CheckState != CheckState.Indeterminate)
                        {
                            filters.EKMT = transporterUpdateFiltersSelectEKMTCheckBox.Checked;
                        }
                        else 
                        {
                            filters.EKMT = null;
                        }

                        if (transporterUpdateFiltersSelectZbornyCheckBox.CheckState != CheckState.Indeterminate)
                        {
                            filters.Zborny = transporterUpdateFiltersSelectZbornyCheckBox.Checked;
                        }
                        else 
                        {
                           filters.Zborny = null;
                        }

                        if (transporterUpdateFiltersSelectADCheckBox.CheckState != CheckState.Indeterminate)
                        {
                            filters.AD = transporterUpdateFiltersSelectADCheckBox.Checked;
                        }
                        else 
                        {
                            filters.AD = null;
                        }
                        db.Entry(filters).State = EntityState.Modified;
                    }                    
                    if (transporterOriginalChanged)
                    {
                        transporter.ContractType = true;
                    }
                    else
                    {
                        transporter.ContractType = false;
                    }
                    db.Entry(transporter).State = EntityState.Modified;
                    db.SaveChanges();
                    
                    MessageBox.Show("Успішно змінено");
                }
                else
                {
                    MessageBox.Show("Змін не знайдено");
                }
            }
        }

        // Contact
        #region Contact

        void AddNewTransporterContact()
        {
            if (transporter != null)
            {
                updateTransporterContactAddForm.AddTransporterContact2(transporter.Id);
                updateTransporterContactAddForm = null;
            }
        }

        void UpdateTransporterContact()
        {
            if (transporter != null)
            {
                updateTransporterContactUpdateForm.UpdateTransporterContact(transporter);
            }
        }

        void DeleteTransporterContact()
        {
            if (transporter != null)
            {
                deleteTransporterContactDeleteForm.DeleteTransporterContact(transporter);
            }
        }

        #endregion

        #endregion

        //Delete
        #region Delete

        void DeleteTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                if (deleteTransporter != null)
                {
                    if (MessageBox.Show("Видалити перевізника " + deleteTransporter.FullName + "?", "Підтвердіть видалення!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.Transporters.Attach(deleteTransporter);
                            db.Transporters.Remove(deleteTransporter);
                            db.SaveChanges();
                            MessageBox.Show("Перевізник успішно видалений");
                            transporterDeleteComboBox.Items.Remove(transporterDeleteComboBox.SelectedItem);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Помилка!" + Environment.NewLine + e);
                        }
                    }
                }
            }
        }

        void LoadTransporterDeleteInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (deleteTransporterSelectDiapasoneComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = deleteTransporterSelectDiapasoneComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        transporterDeleteComboBox.Items.Add(item.FullName + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadDiapasoneTransporterDeleteInfoCombobox()
        {
            deleteTransporterSelectDiapasoneComboBox.Items.Clear();
            transporterDeleteComboBox.Items.Clear();
            transporterDeleteComboBox.Text = "";
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double transporterPart = 0;
                if ((from c in db.Transporters select c.Id).Count() != 0)
                {
                    long transporterCount = (from c in db.Transporters select c.Id).Max();
                    if (transporterCount % part == 0)
                    {
                        transporterPart = transporterCount / part;
                    }
                    else
                    {
                        transporterPart = (transporterCount / part) + 1;
                    }

                    for (int i = 0; i < transporterPart; i++)
                    {
                        deleteTransporterSelectDiapasoneComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    deleteTransporterSelectDiapasoneComboBox.DroppedDown = true;
                    transporterDeleteComboBox.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void SplitDeleteTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = transporterDeleteComboBox.SelectedItem.ToString();
                string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedNameAndDirector[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                deleteTransporter = db.Transporters.Find(id);
            }
        }
        #endregion

    }
}
