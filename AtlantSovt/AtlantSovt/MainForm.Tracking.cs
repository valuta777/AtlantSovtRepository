using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    partial class MainForm
    {

        int TrackingClikedId = 0;
        bool isDatePickerEnabled = false;

        //Show
        #region Show

        void ShowTracking()
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                from o in db.Orders
                orderby o.Id
                select
                new
                {
                    Id = o.Id,
                    //TODO OrderNumber
                    OrderNumber = "Номер заявки",
                    YorU = o.YorU,
                    ClientName = o.Client.Name,
                    TransporterName = o.Transporter.FullName,
                    DownloadDate = o.DownloadDate,
                    State = o.State
                };

                trackingShowDataGridView.DataSource = query.ToList();
                trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                trackingShowDataGridView.Columns[6].HeaderText = "Стан";


            } trackingShowDataGridView.Update();
        }

        void ShowTrackingInfo()
        {
            using (var db = new AtlantSovtContext())
            {

                try
                {
                    TrackingClikedId = Convert.ToInt32(trackingShowDataGridView.CurrentRow.Cells[0].Value);

                    var query =
                    from con in db.TransporterContacts
                    where con.TransporterId == 
                    ( 
                        from o in db.Orders
                        where o.Id == TrackingClikedId 
                        select o
                    ).FirstOrDefault().TransporterId
                    select new
                    {
                        Контактна_персона = con.ContactPerson,
                        Номер = con.TelephoneNumber,
                        Факс = con.FaxNumber,
                        Email = con.Email,
                    };
                    trackingShowTransporterContactsDataGridView.DataSource = query.ToList();
                    trackingShowTransporterContactsDataGridView.Columns[0].HeaderText = "Контактна особа";
                    trackingShowTransporterContactsDataGridView.Columns[1].HeaderText = "Телефон";
                    trackingShowTransporterContactsDataGridView.Columns[2].HeaderText = "Факс";
                    trackingShowTransporterContactsDataGridView.Columns[3].HeaderText = "Email";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Немає жодного перевізника");
                }
            }
            trackingShowTransporterContactsDataGridView.Update();
            trackingShowTransporterContactsDataGridView.Visible = true;
        }

        void ShowTrackingSearch()
        {
            trackingShowTransporterContactsDataGridView.Update();
            trackingShowTransporterContactsDataGridView.Visible = false;

            var text = trackingShowSearchTextBox.Text;

            using (var db = new AtlantSovtContext())
            {

                if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text == "")// 0 0 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   select
                   new
                   {
                       Id = o.Id,
                       //TODO OrderNumber
                       OrderNumber = "Номер заявки",
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = o.State

                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text != "") // 0 0 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.YorU.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text)))
                   select
                   new
                   {
                       Id = o.Id,
                       //TODO OrderNumber
                       OrderNumber = "Номер заявки",
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = o.State

                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text == "") // 0 1 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.DownloadDate.Value.Month == showTrackingDateTimePicker.Value.Month) && (o.DownloadDate.Value.Year == showTrackingDateTimePicker.Value.Year)
                   select
                   new
                   {
                       Id = o.Id,
                       //TODO OrderNumber
                       OrderNumber = "Номер заявки",
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = o.State

                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text != "")// 0 1 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.YorU.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) && (o.DownloadDate.Value.Month == showTrackingDateTimePicker.Value.Month) && (o.DownloadDate.Value.Year == showTrackingDateTimePicker.Value.Year)
                   select
                   new
                   {
                       Id = o.Id,
                       //TODO OrderNumber
                       OrderNumber = "Номер заявки",
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = o.State

                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text == "") // 1 0 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.State == true)
                   select
                   new
                   {
                       Id = o.Id,
                       //TODO OrderNumber
                       OrderNumber = "Номер заявки",
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = o.State

                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text != "") // 1 0 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.YorU.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text)) && (o.State == true)) 
                   select
                   new
                   {
                       Id = o.Id,
                       //TODO OrderNumber
                       OrderNumber = "Номер заявки",
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = o.State

                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text == "")// 1 1 0
                {
                    var queryTextAndDate =
                  from o in db.Orders
                  where (o.State == true && (o.DownloadDate.Value.Month == showTrackingDateTimePicker.Value.Month) && (o.DownloadDate.Value.Year == showTrackingDateTimePicker.Value.Year))
                  select
                  new
                  {
                      Id = o.Id,
                      //TODO OrderNumber
                      OrderNumber = "Номер заявки",
                      YorU = o.YorU,
                      ClientName = o.Client.Name,
                      TransporterName = o.Transporter.FullName,
                      DownloadDate = o.DownloadDate,
                      State = o.State

                  };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text != "")// 1 1 1
                {
                     var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.YorU.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) && (o.DownloadDate.Value.Month == showTrackingDateTimePicker.Value.Month) && (o.DownloadDate.Value.Year == showTrackingDateTimePicker.Value.Year && o.State == true)
                   select
                   new
                   {
                       Id = o.Id,
                       //TODO OrderNumber
                       OrderNumber = "Номер заявки",
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = o.State

                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                }

            } trackingShowDataGridView.Update();

        }

        //void ShowClientInfo()
        //{
        //    using (var db = new AtlantSovtContext())
        //    {
        //        try
        //        {

        //            var ClikedId = Convert.ToInt32(clientDataGridView.CurrentRow.Cells[0].Value);
        //            var query =
        //            from con in db.ClientContacts
        //            where con.ClientId == ClikedId
        //            select new
        //            {
        //                Контактна_персона = con.ContactPerson,
        //                Номер = con.TelephoneNumber,
        //                Факс = con.FaxNumber,
        //                Email = con.Email,
        //            };
        //            clientContactsDataGridView.DataSource = query.ToList();
        //            clientContactsDataGridView.Columns[0].HeaderText = "Контактна особа";
        //            clientContactsDataGridView.Columns[1].HeaderText = "Телефон";
        //            clientContactsDataGridView.Columns[2].HeaderText = "Факс";
        //            clientContactsDataGridView.Columns[3].HeaderText = "Email";

        //            var query1 =
        //                from c in db.Clients
        //                where c.Id == ClikedId
        //                select c.Comment;

        //            clientCommentRichTextBox.Text = query1.FirstOrDefault();


        //            var query2 =
        //            from b in db.ClientBankDetails
        //            where b.Id == ClikedId
        //            select new
        //            {
        //                Name = b.BankName,
        //                MFO = b.MFO,
        //                AccountNumber = b.AccountNumber,
        //                EDRPOU = b.EDRPOU,
        //                IPN = b.IPN,
        //                CertificateNumber = b.CertificateNamber,
        //                SWIFT = b.SWIFT,
        //                IBAN = b.IBAN
        //            };

        //            clientBankDetailsDataGridView.DataSource = query2.ToList();
        //            clientBankDetailsDataGridView.Columns[0].HeaderText = "Назва банку";
        //            clientBankDetailsDataGridView.Columns[1].HeaderText = "МФО";
        //            clientBankDetailsDataGridView.Columns[2].HeaderText = "Номер рахунку";
        //            clientBankDetailsDataGridView.Columns[3].HeaderText = "ЕДРПОУ";
        //            clientBankDetailsDataGridView.Columns[4].HeaderText = "IPN";
        //            clientBankDetailsDataGridView.Columns[5].HeaderText = "Номер свідоцтва";
        //            clientBankDetailsDataGridView.Columns[6].HeaderText = "SWIFT";
        //            clientBankDetailsDataGridView.Columns[7].HeaderText = "IBAN";
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Немає жодного клієнта");
        //        }
        //    }
        //    clientContactsDataGridView.Update();
        //    clientBankDetailsDataGridView.Update();
        //    clientContactsDataGridView.Visible = true;
        //    clientBankDetailsDataGridView.Visible = true;
        //}

        //void ShowClientSearch()
        //{

        //    var text = clientShowSearchTextBox.Text;
        //    using (var db = new AtlantSovtContext())
        //    {
        //        var query =
        //        from c in db.Clients
        //        where c.Name.Contains(text) || c.Director.Contains(text) || c.ClientContacts.Any(con => con.TelephoneNumber.Contains(text)) || c.ClientContacts.Any(con => con.Email.Contains(text)) || c.ClientContacts.Any(con => con.ContactPerson.Contains(text))
        //        select
        //        new
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            Director = c.Director,
        //            PhysicalAddress = c.PhysicalAddress,
        //            GeografphyAddress = c.GeografphyAddress,
        //            ContractType = c.ContractType,
        //            TaxPayerStatusId = c.TaxPayerStatu.Status,
        //            WorkDocumentId = c.WorkDocument.Status,
        //        };


        //        clientDataGridView.DataSource = query.ToList();
        //        clientDataGridView.Columns[0].HeaderText = "Порядковий номер";
        //        clientDataGridView.Columns[1].HeaderText = "Назва";
        //        clientDataGridView.Columns[2].HeaderText = "П.І.Б. Директора";
        //        clientDataGridView.Columns[3].HeaderText = "Фізична адреса";
        //        clientDataGridView.Columns[4].HeaderText = "Юридична адреса";
        //        clientDataGridView.Columns[5].HeaderText = "Оригінал договору";
        //        clientDataGridView.Columns[6].HeaderText = "Статус платника податку";
        //        clientDataGridView.Columns[7].HeaderText = "На основі";


        //    } clientDataGridView.Update();

        //}

        #endregion
    }
}
