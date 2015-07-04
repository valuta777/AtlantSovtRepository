using AtlantSovt.AtlantSovtDb;
using Microsoft.Office.Interop.Word;
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
        OrderCounter orderCount;
        Order orderDocument;
        bool isOrderFull;
        bool isOrderLanguageSelected;
        bool isDatePickerEnabled = false;

        int TrackingClikedId = 0;

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
                    OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                    YorU = o.YorU,
                    ClientName = o.Client.Name,
                    TransporterName = o.Transporter.FullName,
                    DownloadDate = o.DownloadDate,
                    State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                    Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                };

                trackingShowDataGridView.DataSource = query.ToList();
                trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                trackingShowDataGridView.Columns[7].HeaderText = "Мова";


            }
            trackingShowDataGridView.Update();
            trackingShowDataGridView.ClearSelection();
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

                    var query1 =
                        from com in db.TrackingComments
                        where com.OrderId == TrackingClikedId
                        select new
                    {
                        comment = com.Comment,
                        createDate = com.CreateDate,
                        lastChangeDate = com.LastChangeDate
                    };

                    trackingShowCommentDataGridView.DataSource = query1.ToList();
                    trackingShowCommentDataGridView.Columns[0].HeaderText = "Коментар";
                    trackingShowCommentDataGridView.Columns[1].HeaderText = "Дата додавання";
                    trackingShowCommentDataGridView.Columns[2].HeaderText = "Дата останньої зміни";

                    trackingShowCommentDataGridView.Columns[1].Width = 150;
                    trackingShowCommentDataGridView.Columns[2].Width = 150;

                    var query3 =
                  from add in db.OrderDownloadAddresses
                  where add.OrderId == TrackingClikedId
                  select new
                  {
                      country = add.DownloadAddress.Country.Name,
                      cityCode = add.DownloadAddress.CityCode
                  };
                    trackingShowDownloadAddressDataGridView.DataSource = query3.ToList();
                    trackingShowDownloadAddressDataGridView.Columns[0].HeaderText = "Країна";
                    trackingShowDownloadAddressDataGridView.Columns[1].HeaderText = "Код міста";

                    var query4 =
                 from add in db.OrderUploadAdresses
                 where add.OrderId == TrackingClikedId
                 select new
                 {
                     country = add.UploadAddress.Country.Name,
                     cityCode = add.UploadAddress.CityCode
                 };
                    trackingShowUploadAddressDataGridView.DataSource = query4.ToList();
                    trackingShowUploadAddressDataGridView.Columns[0].HeaderText = "Країна";
                    trackingShowUploadAddressDataGridView.Columns[1].HeaderText = "Код міста";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Немає жодної заявки");
                }
            }
            trackingShowTransporterContactsDataGridView.Update();

            trackingShowTransporterContactsDataGridView.Visible = true;
            trackingShowCommentDataGridView.Visible = true;
            trackingShowUploadAddressDataGridView.Visible = true;
            trackingShowDownloadAddressDataGridView.Visible = true;
        }

        void ShowTrackingCloseOrder()
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    Order order;
                    TrackingClikedId = Convert.ToInt32(trackingShowDataGridView.CurrentRow.Cells[0].Value);
                    order = db.Orders.Find(TrackingClikedId);

                    if (order.State == true)
                    {
                        order.State = false;
                        db.Entry(order).State = EntityState.Modified;
                        db.SaveChanges();
                        ShowTrackingSearch();
                    }
                    else if (order.State == false)
                    {
                        MessageBox.Show("Заявка вже закрита");
                    }
                    else if (!order.State.HasValue)
                    {
                        if (MessageBox.Show("Заявка ще не створена, все рівно закрити?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            order.State = false;
                            db.Entry(order).State = EntityState.Modified;
                            db.SaveChanges();
                            ShowTrackingSearch();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Немає жодної заявки");
                }
            }
            trackingShowDataGridView.Update();

        }

        void ShowTrackingSearch()
        {
            trackingShowTransporterContactsDataGridView.Update();

            trackingShowTransporterContactsDataGridView.Visible = false;
            trackingShowCommentDataGridView.Visible = false;
            trackingShowUploadAddressDataGridView.Visible = false;
            trackingShowDownloadAddressDataGridView.Visible = false;

            var text = trackingShowSearchTextBox.Text;

            using (var db = new AtlantSovtContext())
            {

                if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text == "")// 0 0 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text != "") // 0 0 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.YorU.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text)))
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"


                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text == "") // 0 1 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.DownloadDate.Value.Month == showTrackingDateTimePicker.Value.Month) && (o.DownloadDate.Value.Year == showTrackingDateTimePicker.Value.Year)
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"


                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text != "")// 0 1 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.YorU.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) && (o.DownloadDate.Value.Month == showTrackingDateTimePicker.Value.Month) && (o.DownloadDate.Value.Year == showTrackingDateTimePicker.Value.Year)
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"


                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text == "") // 1 0 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.State == true)
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"


                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text != "") // 1 0 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.YorU.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text)) && (o.State == true))
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       YorU = o.YorU,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = o.DownloadDate,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"


                   };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text == "")// 1 1 0
                {
                    var queryTextAndDate =
                  from o in db.Orders
                  where (o.State == true && (o.DownloadDate.Value.Month == showTrackingDateTimePicker.Value.Month) && (o.DownloadDate.Value.Year == showTrackingDateTimePicker.Value.Year))
                  orderby o.Id
                  select
                  new
                  {
                      Id = o.Id,
                      OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                      YorU = o.YorU,
                      ClientName = o.Client.Name,
                      TransporterName = o.Transporter.FullName,
                      DownloadDate = o.DownloadDate,
                      State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                      Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"


                  };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text != "")// 1 1 1
                {
                    var queryTextAndDate =
                  from o in db.Orders
                  where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.YorU.Contains(text) ||
                        o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) && (o.DownloadDate.Value.Month == showTrackingDateTimePicker.Value.Month) && (o.DownloadDate.Value.Year == showTrackingDateTimePicker.Value.Year && o.State == true)
                  orderby o.Id
                  select
                  new
                  {
                      Id = o.Id,
                      OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                      YorU = o.YorU,
                      ClientName = o.Client.Name,
                      TransporterName = o.Transporter.FullName,
                      DownloadDate = o.DownloadDate,
                      State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                      Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"


                  };

                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Мова";

                }

            } trackingShowDataGridView.Update();

        }

        void CreateOrderDocument()
        {
            var wordApp = new Microsoft.Office.Interop.Word.Application();
            Document wordDocument = null;
            try
            {
                var ClikedId = Convert.ToInt32(trackingShowDataGridView.CurrentRow.Cells[0].Value);

                using (var db = new AtlantSovtContext())
                {

                    orderDocument = db.Orders.Find(ClikedId);
                    wordApp.Visible = false;
                    wordDocument = wordApp.Documents.Open((System.AppDomain.CurrentDomain.BaseDirectory + ((orderDocument.Language == 0) ? @"Resources\ukrOrder.docx" : (orderDocument.Language == 1) ? @"Resources\polOrder.docx" : @"Resources\gerOrder.docx")).Replace("\\bin\\Debug", ""));

                    if (orderDocument != null)
                    {
                        string createDate = orderDocument.Date.Value.ToShortDateString();
                        string orderNumber = orderDocument.IndexNumber + @"\" + orderDocument.Date.Value.Year;

                        string downloadDate = ((orderDocument.DownloadDate.Value.ToShortDateString() == null || orderDocument.DownloadDate.Value.ToShortDateString() == "") ? "" : orderDocument.DownloadDate.Value.ToShortDateString()) +" на ";
                            downloadDate += (orderDocument.DownloadDate.Value.ToShortTimeString() == null || orderDocument.DownloadDate.Value.ToShortTimeString() == "") ? "" : orderDocument.DownloadDate.Value.ToShortTimeString();
                      
                        string dateTerms = (orderDocument.DownloadDate.Value.ToString("dd.mm") == null || orderDocument.DownloadDate.Value.ToString("dd.mm") == "") ? "" : orderDocument.DownloadDate.Value.ToString("dd.mm") + " - ";
                            dateTerms += (orderDocument.UploadDate.Value.ToShortDateString() == null || orderDocument.UploadDate.Value.ToShortDateString() == "") ? "" : orderDocument.UploadDate.Value.ToShortDateString() + " до ";
                            dateTerms +=(orderDocument.UploadDate.Value.ToShortTimeString() == null || orderDocument.UploadDate.Value.ToShortTimeString() == "") ? "" : orderDocument.UploadDate.Value.ToShortTimeString();

                        string downloadAddress = "";
                        string uploadAddress = "";
                        string customAddress = "";
                        string uncustomAddress = "";
                        string forwarderName1 = "";
                        string forwarderName2 = "";
                        string loadingForm1 = "";
                        string loadingForm2 = "";
                        string[] regularyDelay;

                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == true).Count() == 1)
                        {
                            forwarderName1 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == true).FirstOrDefault().Forwarder.Name;
                        }
                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == false).Count() == 1)
                        {
                            forwarderName2 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == false).FirstOrDefault().Forwarder.Name;
                        }


                        if (orderDocument.OrderLoadingForms.Where(l => l.IsFirst == true).Count() == 1)
                        {
                            loadingForm1 = orderDocument.OrderLoadingForms.Where(l => l.IsFirst == true).FirstOrDefault().LoadingForm.Type;

                        }
                        if (orderDocument.OrderLoadingForms.Where(l => l.IsFirst == false).Count() == 1)
                        {
                            loadingForm2 = orderDocument.OrderLoadingForms.Where(l => l.IsFirst == false).FirstOrDefault().LoadingForm.Type;
                        }


                        string transporterName = (orderDocument.Transporter.FullName == null || orderDocument.Transporter.FullName == "") ? "" : orderDocument.Transporter.FullName;
                        string cargoType = (orderDocument.Cargo.Type == null || orderDocument.Cargo.Type == "") ? "" : orderDocument.Cargo.Type + ", ";
                        string cube = (orderDocument.Cube.Type == null || orderDocument.Cube.Type == "") ? "" : orderDocument.Cube.Type;
                        string additionalTerms = (orderDocument.AdditionalTerm.Type == null || orderDocument.AdditionalTerm.Type == "") ? "" : orderDocument.AdditionalTerm.Type;
                        string trailer = (orderDocument.Trailer.Type == null || orderDocument.Trailer.Type == "") ? "" : orderDocument.Trailer.Type + ", ";
                        string paymentTerms = (orderDocument.Payment.Type == null || orderDocument.Payment.Type == "") ? "" : orderDocument.Payment.Type;
                        string freight = (orderDocument.Freight == null || orderDocument.Freight == "") ? "" : orderDocument.Freight;
                        string fineForDelay = (orderDocument.FineForDelay.Type == null || orderDocument.FineForDelay.Type == "") ? "_____________________" : orderDocument.FineForDelay.Type;
                        string weight = (Convert.ToString(orderDocument.CargoWeight) == null) ? "" : Convert.ToString(orderDocument.CargoWeight) + " т";
                        string orderDeny = (orderDocument.OrderDeny.Type == null || orderDocument.OrderDeny.Type == "") ? "____________________" : orderDocument.OrderDeny.Type;

                        if (orderDocument.RegularyDelay.Type == null || orderDocument.RegularyDelay.Type == "")
                        {
                            regularyDelay = new string[4] { "___", "___", "___", "___" };
                        }
                        else
                        {
                            regularyDelay = orderDocument.RegularyDelay.Type.Split(new char[] { '-' });
                        }
                        var DownloaddAddressQuery =
                            from da in db.OrderDownloadAddresses
                            where da.OrderId == ClikedId
                            select da;

                        var UploaddAddressQuery =
                           from ua in db.OrderUploadAdresses
                           where ua.OrderId == ClikedId
                           select ua;

                        var CustomAddressQuery =
                           from ca in db.OrderCustomsAddresses
                           where ca.OrderId == ClikedId
                           select ca;

                        var UncustomAddressQuery =
                           from uca in db.OrderUnCustomsAddresses
                           where uca.OrderId == ClikedId
                           select uca;

                        string downloadAddressContactPerson = DownloaddAddressQuery.FirstOrDefault().DownloadAddress.ContactPerson;

                        foreach(var address in DownloaddAddressQuery)
                        {
                            downloadAddress += ((address.DownloadAddress.StreetName != "" && address.DownloadAddress.StreetName != null) ? address.DownloadAddress.StreetName + " " : "");
                            downloadAddress += ((address.DownloadAddress.HouseNumber != "" && address.DownloadAddress.HouseNumber != null) ? address.DownloadAddress.HouseNumber + ", " : "");
                            downloadAddress += ((address.DownloadAddress.CityName != "" && address.DownloadAddress.CityName != null) ? address.DownloadAddress.CityName + ", " : "");
                            downloadAddress += address.DownloadAddress.Country.Name + "\n";
                        }

                        foreach (var address in UploaddAddressQuery)
                        {
                            uploadAddress += ((address.UploadAddress.StreetName != "" && address.UploadAddress.StreetName != null) ? address.UploadAddress.StreetName + " " : "");
                            uploadAddress += ((address.UploadAddress.HouseNumber != "" && address.UploadAddress.HouseNumber != null) ? address.UploadAddress.HouseNumber + ", " : "");
                            uploadAddress += ((address.UploadAddress.CityName != "" && address.UploadAddress.CityName != null) ? address.UploadAddress.CityName + ", " : "");
                            uploadAddress += address.UploadAddress.Country.Name + "\n";
                        }

                        foreach (var address in CustomAddressQuery)
                        {
                            customAddress += ((address.CustomsAddress.StreetName != "" && address.CustomsAddress.StreetName != null) ? address.CustomsAddress.StreetName + " " : "");
                            customAddress += ((address.CustomsAddress.HouseNumber != "" && address.CustomsAddress.HouseNumber != null) ? address.CustomsAddress.HouseNumber + ", " : "");
                            customAddress += ((address.CustomsAddress.CityName != "" && address.CustomsAddress.CityName != null) ? address.CustomsAddress.CityName + ", " : "");
                            customAddress += address.CustomsAddress.Country.Name + "\n";
                        }

                        foreach (var address in UncustomAddressQuery)
                        {
                            uncustomAddress += ((address.UnCustomsAddress.StreetName != "" && address.UnCustomsAddress.StreetName != null) ? address.UnCustomsAddress.StreetName + " " : "");
                            uncustomAddress += ((address.UnCustomsAddress.HouseNumber != "" && address.UnCustomsAddress.HouseNumber != null) ? address.UnCustomsAddress.HouseNumber + ", " : "");
                            uncustomAddress += ((address.UnCustomsAddress.CityName != "" && address.UnCustomsAddress.CityName != null) ? address.UnCustomsAddress.CityName + ", " : "");
                            uncustomAddress += address.UnCustomsAddress.Country.Name + "\n";
                        }

                        ReplaseWordStub("{ForwarderName1}", forwarderName1, wordDocument);
                        ReplaseWordStub("{OrderNumber}", orderNumber, wordDocument);
                        ReplaseWordStub("{CreateDate}", createDate, wordDocument);
                        ReplaseWordStub("{DownloadDate}", downloadDate, wordDocument);
                        ReplaseWordStub("{DownloadAddress}", downloadAddress, wordDocument);
                        ReplaseWordStub("{DownloadAddressContactPerson}", downloadAddressContactPerson, wordDocument);
                        ReplaseWordStub("{CustomAddress}", customAddress, wordDocument);
                        ReplaseWordStub("{CargoType}", cargoType, wordDocument);
                        ReplaseWordStub("{Weight}", weight, wordDocument);
                        ReplaseWordStub("{LoadingForm1}", loadingForm1, wordDocument);
                        ReplaseWordStub("{LoadingForm2}", loadingForm2, wordDocument);
                        ReplaseWordStub("{DateTerms}", dateTerms, wordDocument);
                        ReplaseWordStub("{UncustomAddress}", uncustomAddress, wordDocument);
                        ReplaseWordStub("{UploadAddress}", uploadAddress, wordDocument);
                        ReplaseWordStub("{PaymentTerms}", paymentTerms, wordDocument);
                        ReplaseWordStub("{Cube}", cube, wordDocument);
                        ReplaseWordStub("{Trailer}", trailer, wordDocument);
                        ReplaseWordStub("{AdditionalTerms}", additionalTerms , wordDocument);
                        ReplaseWordStub("{RegularyDelay1}", regularyDelay[0], wordDocument);
                        ReplaseWordStub("{RegularyDelay2}", regularyDelay[1], wordDocument);
                        ReplaseWordStub("{RegularyDelay3}", regularyDelay[2], wordDocument);
                        ReplaseWordStub("{RegularyDelay4}", regularyDelay[3], wordDocument);
                        ReplaseWordStub("{OrderDeny}", orderDeny, wordDocument);
                        ReplaseWordStub("{ForwarderName2}", forwarderName2, wordDocument);
                        ReplaseWordStub("{Freight}", freight, wordDocument);
                        ReplaseWordStub("{FineForDelay}", fineForDelay, wordDocument);
                        ReplaseWordStub("{TransporterName}", transporterName, wordDocument);

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            orderNumber = orderNumber.Replace(@"\", "_");
                            wordDocument.SaveAs(folderBrowserDialog.SelectedPath + "\\" + orderNumber + ".docx");
                            wordApp.Visible = true;
                        }
                        else
                        {
                            wordDocument.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                        }
                    }
                }
            }
            catch (NullReferenceException nullClickedId)
            {
                contractShowOpenDocButton.Enabled = false;
                contractShowDeleteContractButton.Enabled = false;
                contractShowTransporterContactDataGridView.Visible = false;
                MessageBox.Show("Немає жодної заявки" + nullClickedId.Message);
            }
            catch (System.Runtime.InteropServices.COMException wordException)
            {
                wordDocument.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                MessageBox.Show("Помилка, спробуйте ще раз");
            }
        }

        void IsOrderFull()
        {
            isOrderFull = true;
            isOrderLanguageSelected = true;

            var ClikedId = Convert.ToInt32(trackingShowDataGridView.CurrentRow.Cells[0].Value);

            using (var db = new AtlantSovtContext())
            {
                orderDocument = db.Orders.Find(ClikedId);
                if (orderDocument != null)
                {
                    if (orderDocument.Language != null)
                    {

                        if (orderDocument.DownloadDate == null) isOrderFull = false;
                        if (orderDocument.UploadDate == null) isOrderFull = false;

                        string downloadAddress = "";
                        string uploadAddress = "";
                        string customAddress = "";
                        string uncustomAddress = "";
                        string forwarderName1 = "";
                        string forwarderName2 = "";
                        string loadingForm1 = "";
                        string loadingForm2 = "";
                        string[] regularyDelay;

                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == true).Count() == 1)
                        {
                            forwarderName1 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == true).FirstOrDefault().Forwarder.Name;
                        }
                        else
                        {
                            isOrderFull = false;
                        }
                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == false).Count() == 1)
                        {
                            forwarderName2 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == false).FirstOrDefault().Forwarder.Name;
                        }
                        else
                        {
                            isOrderFull = false;
                        }


                        if (orderDocument.OrderLoadingForms.Where(l => l.IsFirst == true).Count() == 1)
                        {
                            loadingForm1 = orderDocument.OrderLoadingForms.Where(l => l.IsFirst == true).FirstOrDefault().LoadingForm.Type;
                        }
                        else
                        {
                            isOrderFull = false;
                        }
                        if (orderDocument.OrderLoadingForms.Where(l => l.IsFirst == false).Count() == 1)
                        {
                            loadingForm2 = orderDocument.OrderLoadingForms.Where(l => l.IsFirst == false).FirstOrDefault().LoadingForm.Type;
                        }
                        else
                        {
                            isOrderFull = false;
                        }

                        if(orderDocument.Transporter.FullName == null || orderDocument.Transporter.FullName == "") isOrderFull = false;
                        if(orderDocument.Cargo.Type == null || orderDocument.Cargo.Type == "") isOrderFull = false;
                        if(orderDocument.Cube.Type == null || orderDocument.Cube.Type == "") isOrderFull = false;
                        if(orderDocument.AdditionalTerm.Type == null || orderDocument.AdditionalTerm.Type == "")isOrderFull = false;
                        if(orderDocument.Trailer.Type == null || orderDocument.Trailer.Type == "") isOrderFull = false;
                        if(orderDocument.Payment.Type == null || orderDocument.Payment.Type == "")isOrderFull = false;
                        if(orderDocument.Freight == null || orderDocument.Freight == "") isOrderFull = false;
                        if(orderDocument.FineForDelay.Type == null || orderDocument.FineForDelay.Type == "")isOrderFull = false;
                        if(Convert.ToString(orderDocument.CargoWeight) == null) isOrderFull = false;
                        if (orderDocument.OrderDeny.Type == null || orderDocument.OrderDeny.Type == "") isOrderFull = false;

                        if (orderDocument.RegularyDelay.Type == null || orderDocument.RegularyDelay.Type == "")
                        {
                            regularyDelay = new string[4] { "___", "___", "___", "___" };
                            isOrderFull = false;
                        }
                        else
                        {
                            regularyDelay = orderDocument.RegularyDelay.Type.Split(new char[] { '-' });
                        }
                        var DownloaddAddressQuery =
                            from da in db.OrderDownloadAddresses
                            where da.OrderId == ClikedId
                            select da;

                        var UploaddAddressQuery =
                           from ua in db.OrderUploadAdresses
                           where ua.OrderId == ClikedId
                           select ua;

                        var CustomAddressQuery =
                           from ca in db.OrderCustomsAddresses
                           where ca.OrderId == ClikedId
                           select ca;

                        var UncustomAddressQuery =
                           from uca in db.OrderUnCustomsAddresses
                           where uca.OrderId == ClikedId
                           select uca;

                        string downloadAddressContactPerson = DownloaddAddressQuery.FirstOrDefault().DownloadAddress.ContactPerson;

                        foreach (var address in DownloaddAddressQuery)
                        {
                            downloadAddress += ((address.DownloadAddress.StreetName != "" && address.DownloadAddress.StreetName != null) ? address.DownloadAddress.StreetName + " " : "");
                            downloadAddress += ((address.DownloadAddress.HouseNumber != "" && address.DownloadAddress.HouseNumber != null) ? address.DownloadAddress.HouseNumber + ", " : "");
                            downloadAddress += ((address.DownloadAddress.CityName != "" && address.DownloadAddress.CityName != null) ? address.DownloadAddress.CityName + ", " : "");
                            downloadAddress += address.DownloadAddress.Country.Name + "\n";
                        }

                        foreach (var address in UploaddAddressQuery)
                        {
                            uploadAddress += ((address.UploadAddress.StreetName != "" && address.UploadAddress.StreetName != null) ? address.UploadAddress.StreetName + " " : "");
                            uploadAddress += ((address.UploadAddress.HouseNumber != "" && address.UploadAddress.HouseNumber != null) ? address.UploadAddress.HouseNumber + ", " : "");
                            uploadAddress += ((address.UploadAddress.CityName != "" && address.UploadAddress.CityName != null) ? address.UploadAddress.CityName + ", " : "");
                            uploadAddress += address.UploadAddress.Country.Name + "\n";
                        }

                        foreach (var address in CustomAddressQuery)
                        {
                            customAddress += ((address.CustomsAddress.StreetName != "" && address.CustomsAddress.StreetName != null) ? address.CustomsAddress.StreetName + " " : "");
                            customAddress += ((address.CustomsAddress.HouseNumber != "" && address.CustomsAddress.HouseNumber != null) ? address.CustomsAddress.HouseNumber + ", " : "");
                            customAddress += ((address.CustomsAddress.CityName != "" && address.CustomsAddress.CityName != null) ? address.CustomsAddress.CityName + ", " : "");
                            customAddress += address.CustomsAddress.Country.Name + "\n";
                        }

                        foreach (var address in UncustomAddressQuery)
                        {
                            uncustomAddress += ((address.UnCustomsAddress.StreetName != "" && address.UnCustomsAddress.StreetName != null) ? address.UnCustomsAddress.StreetName + " " : "");
                            uncustomAddress += ((address.UnCustomsAddress.HouseNumber != "" && address.UnCustomsAddress.HouseNumber != null) ? address.UnCustomsAddress.HouseNumber + ", " : "");
                            uncustomAddress += ((address.UnCustomsAddress.CityName != "" && address.UnCustomsAddress.CityName != null) ? address.UnCustomsAddress.CityName + ", " : "");
                            uncustomAddress += address.UnCustomsAddress.Country.Name + "\n";
                        }

                    }
                    else
                    {
                        isOrderLanguageSelected = false;
                        MessageBox.Show("Виберіть мову у меню редагування");
                    }
                }
                else
                {
                    isOrderFull = false;
                }
                if (!isOrderFull)
                {
                    if (MessageBox.Show("Продовжити без повного заповнення даних?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        isOrderFull = true;
                    }
                }
            }
        }

        void OrderCounter()
        {
            var ClikedId = Convert.ToInt32(trackingShowDataGridView.CurrentRow.Cells[0].Value);

            using (var db = new AtlantSovtContext())
            {
                var query =
                    from o in db.OrderCounters
                    select o;

                try
                {
                    if (query.Count() == 0)
                    {
                        orderCount = new OrderCounter
                        {
                            Id = 1,
                            ForeignOrder = 0,
                            LocalOrder = 0
                        };
                        db.OrderCounters.Add(orderCount);
                        db.SaveChanges();
                        documentCount = null;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Помилка: " + e.Message);
                }

                orderCount = new OrderCounter();
                orderDocument = db.Orders.Find(ClikedId);

                try
                {
                    if (orderDocument.Language == 0)
                    {
                        orderCount = db.OrderCounters.Find(1);
                        orderCount.LocalOrder += 1;
                        db.Entry(orderCount).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        orderCount = db.OrderCounters.Find(1);
                        orderCount.ForeignOrder += 1;
                        db.Entry(orderCount).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Помилка: " + e.Message);
                }

                try
                {
                    orderDocument = new Order();
                    orderDocument = db.Orders.Find(ClikedId);
                    if (orderDocument.Language == 0)
                    {
                        orderDocument.IndexNumber = orderCount.LocalOrder;
                    }
                    else if (contractLanguage == 1)
                    {
                        orderDocument.IndexNumber = orderCount.ForeignOrder;
                    }

                    orderDocument.Date = DateTime.Now;
                    if(orderDocument.State == null)
                    {
                        orderDocument.State = true;
                    }
                    db.Entry(orderDocument).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Помилка: " + e.Message);
                }
            }
        }
    }
}
