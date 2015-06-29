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
                    OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
                    YorU = o.YorU,
                    ClientName = o.Client.Name,
                    TransporterName = o.Transporter.FullName,
                    DownloadDate = o.DownloadDate,
                    State = (o.State == true) ? "Відкрита" : "Закрита"  

                };

                trackingShowDataGridView.DataSource = query.ToList();
                trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                trackingShowDataGridView.Columns[2].HeaderText = "У / І";
                trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                trackingShowDataGridView.Columns[6].HeaderText = "Стан";

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
                    else
                    {
                        MessageBox.Show("Заявка вже закрита");
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
                       OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
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
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
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
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
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
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
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
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
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
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
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
                  orderby o.Id
                  select
                  new
                  {
                      Id = o.Id,
                      OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
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
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = o.IndexNumber + "/" + o.Date.Value.Year,
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
    }
}
