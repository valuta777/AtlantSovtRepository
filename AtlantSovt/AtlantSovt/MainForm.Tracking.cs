using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
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

        public void ShowTrackingInfo()
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
                    Log.Write(ex);
                    MessageBox.Show("Немає жодної заявки");
                }

                var query5 = 
                        from o in db.Orders
                        where o.Id == TrackingClikedId
                        select o.Note;
                trackingShowAddNoteRichTextBox.Text = query5.FirstOrDefault();
            }
            trackingShowTransporterContactsDataGridView.Update();



            trackingShowTransporterContactsDataGridView.Visible = true;
            trackingShowCommentDataGridView.Visible = true;
            trackingShowUploadAddressDataGridView.Visible = true;
            trackingShowDownloadAddressDataGridView.Visible = true;
            trackingShowAddNoteRichTextBox.Visible = true;
        }

        public void ShowTrackingSearch()
        {
            trackingShowTransporterContactsDataGridView.Update();

            trackingShowTransporterContactsDataGridView.DataSource = null;

            trackingShowCommentDataGridView.DataSource = null;
            trackingShowUploadAddressDataGridView.DataSource = null;
            trackingShowDownloadAddressDataGridView.DataSource = null;

            trackingShowAddNoteRichTextBox.Clear();


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
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                   };
                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                    trackingShowDataGridView.Columns[8].HeaderText = "Мова";
                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text != "") // 0 0 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.Staff.Type.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text)))
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                        CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                   };
                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                    trackingShowDataGridView.Columns[8].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text == "") // 0 1 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where ((o.DownloadDateFrom.Value.Month == showTrackingDateTimePicker.Value.Month) ||
                   (o.DownloadDateTo.Value.Month == showTrackingDateTimePicker.Value.Month)) && ((o.DownloadDateFrom.Value.Year == showTrackingDateTimePicker.Value.Year) || o.DownloadDateTo.Value.Year == showTrackingDateTimePicker.Value.Year)
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                   };
                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                    trackingShowDataGridView.Columns[8].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked != true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text != "")// 0 1 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.Staff.Type.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) && ((o.DownloadDateFrom.Value.Month == showTrackingDateTimePicker.Value.Month) ||
                   (o.DownloadDateTo.Value.Month == showTrackingDateTimePicker.Value.Month)) && ((o.DownloadDateFrom.Value.Year == showTrackingDateTimePicker.Value.Year) || o.DownloadDateTo.Value.Year == showTrackingDateTimePicker.Value.Year)
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                   };
                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                    trackingShowDataGridView.Columns[8].HeaderText = "Мова";

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
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                   };
                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                    trackingShowDataGridView.Columns[8].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled != true && trackingShowSearchTextBox.Text != "") // 1 0 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.Staff.Type.Contains(text) ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text)) && (o.State == true))
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                       CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                   };
                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                    trackingShowDataGridView.Columns[8].HeaderText = "Мова";
                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text == "")// 1 1 0
                {
                    var queryTextAndDate =
                  from o in db.Orders
                  where (o.State == true && ((o.DownloadDateFrom.Value.Month == showTrackingDateTimePicker.Value.Month) ||
                   (o.DownloadDateTo.Value.Month == showTrackingDateTimePicker.Value.Month)) && ((o.DownloadDateFrom.Value.Year == showTrackingDateTimePicker.Value.Year) || o.DownloadDateTo.Value.Year == showTrackingDateTimePicker.Value.Year))
                  orderby o.Id
                  select
                  new
                  {
                      Id = o.Id,
                      OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                      Staff = o.Staff.Type,
                      ClientName = o.Client.Name,
                      TransporterName = o.Transporter.FullName,
                      DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                      State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                      CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                      Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                  };
                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                    trackingShowDataGridView.Columns[8].HeaderText = "Мова";

                }
                else if (showTrackingOnlyActive.Checked == true && isDatePickerEnabled == true && trackingShowSearchTextBox.Text != "")// 1 1 1
                {
                    var queryTextAndDate =
                  from o in db.Orders
                  where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.Staff.Type.Contains(text) ||
                        o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) &&
                        ((o.DownloadDateFrom.Value.Month == showTrackingDateTimePicker.Value.Month) || (o.DownloadDateTo.Value.Month == showTrackingDateTimePicker.Value.Month)) && ((o.DownloadDateFrom.Value.Year == showTrackingDateTimePicker.Value.Year) ||
                        o.DownloadDateTo.Value.Year == showTrackingDateTimePicker.Value.Year) && o.State == true
                  orderby o.Id
                  select
                  new
                  {
                      Id = o.Id,
                      OrderNumber = (!o.IndexNumber.HasValue) ? "Ще не присвоєно" : o.IndexNumber + "/" + o.Date.Value.Year,
                      Staff = o.Staff.Type,
                      ClientName = o.Client.Name,
                      TransporterName = o.Transporter.FullName,
                      DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                      State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                      CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                      Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                  };
                    trackingShowDataGridView.DataSource = queryTextAndDate.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                    trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                    trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                    trackingShowDataGridView.Columns[8].HeaderText = "Мова";

                } trackingShowDataGridView.Update();
            }
        }

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
                    Staff = o.Staff.Type,
                    ClientName = o.Client.Name,
                    TransporterName = o.Transporter.FullName,
                    DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day +"."+ o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "-" + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + "-" +o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                    State = (!o.State.HasValue) ? "Не створена" : ((o.State == false) ? "Закрита" : "Відкрита"),
                    CloseDate = (!o.CloseDate.HasValue) ? "Не визначено" : o.CloseDate.Value.Day + "." + o.CloseDate.Value.Month + "." + o.CloseDate.Value.Year,
                    Language = (!o.Language.HasValue) ? "Не вибрано" : (o.Language == 0) ? "Українська" : (o.Language == 1) ? "Польська" : "Німецька"

                  };
                trackingShowDataGridView.DataSource = query.ToList();
                    trackingShowDataGridView.Columns[0].HeaderText = "Порядковий номер";
                    trackingShowDataGridView.Columns[1].HeaderText = "Номер заявки";
                trackingShowDataGridView.Columns[2].HeaderText = "Працівник";
                    trackingShowDataGridView.Columns[3].HeaderText = "Клієнт";
                    trackingShowDataGridView.Columns[4].HeaderText = "Перевізник";
                    trackingShowDataGridView.Columns[5].HeaderText = "Дата завантаження";
                    trackingShowDataGridView.Columns[6].HeaderText = "Стан";
                trackingShowDataGridView.Columns[7].HeaderText = "Дата закриття";
                trackingShowDataGridView.Columns[8].HeaderText = "Мова";

                }
            trackingShowDataGridView.Update();
            trackingShowDataGridView.ClearSelection();
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

                    if (order.IndexNumber != null)
                    {
                        if (order.State == true)
                        {
                            AddTrackingCloseDateForm closeDateForm = new AddTrackingCloseDateForm(this);
                            closeDateForm.Id = TrackingClikedId;
                            closeDateForm.Show();
                        }
                        else if (order.State == false)
                        {
                            if (MessageBox.Show("Заявка вже закрита, змінити дату закриття?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                AddTrackingCloseDateForm closeDateForm = new AddTrackingCloseDateForm(this);
                                closeDateForm.Id = TrackingClikedId;
                                closeDateForm.Show();
                            }
                        }
                        else if (!order.State.HasValue)
                        {
                            if (MessageBox.Show("Заявка ще не створена, все рівно закрити?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                AddTrackingCloseDateForm closeDateForm = new AddTrackingCloseDateForm(this);
                                closeDateForm.Id = TrackingClikedId;
                                closeDateForm.Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Заявці не призначено номер, спочатку створіть документ");
                        return;
                        
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Немає жодної заявки");
                }
            }
            trackingShowDataGridView.Update();

        }

        void IsOrderFull()
        {
            isOrderFull = true;
            isOrderLanguageSelected = true;

            using (var db = new AtlantSovtContext())
            {
                var ClikedId = Convert.ToInt32(trackingShowDataGridView.CurrentRow.Cells[0].Value);

                orderDocument = db.Orders.Find(ClikedId);

                if (orderDocument != null)
                {
                    if (orderDocument.Language != null)
                    {
                        string loadingForm1 = "";
                        string loadingForm2 = "";
                        string[] regularyDelay;

                        if (orderDocument.DownloadDateFrom == null) isOrderFull = false;
                        if (orderDocument.UploadDateFrom == null) isOrderFull = false;

                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).Count() == 1)
                        {
                            if(orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder.ForwarderStamp.Stamp == null)
                            {
                                isOrderFull = false;
                            }
                        }
                        else
                        {
                            isOrderFull = false;
                        }
                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).Count() == 1)
                        {
                            if(orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).FirstOrDefault().Forwarder.ForwarderStamp.Stamp == null)
                            {
                                isOrderFull = false;
                            }
                        }
                        else
                        {
                            isOrderFull = false;
                        }

                        //TODO 3 forwarder

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

                        if (orderDocument.Transporter == null || orderDocument.Transporter.FullName == "") isOrderFull = false;
                        if (orderDocument.Cargo == null || orderDocument.Cargo.Type == "") isOrderFull = false;
                        if (orderDocument.Cube == null || orderDocument.Cube.Type == "") isOrderFull = false;
                        if (orderDocument.AdditionalTerm == null || orderDocument.AdditionalTerm.Type == "") isOrderFull = false;
                        if (orderDocument.Trailer == null || orderDocument.Trailer.Type == "") isOrderFull = false;
                        if (orderDocument.Payment == null || orderDocument.Payment.Type == "") isOrderFull = false;
                        if (orderDocument.Freight == null || orderDocument.Freight == "") isOrderFull = false;
                        if (orderDocument.FineForDelay == null || orderDocument.FineForDelay.Type == "") isOrderFull = false;
                        if (Convert.ToString(orderDocument.CargoWeight) == null) isOrderFull = false;
                        if (orderDocument.OrderDeny == null || orderDocument.OrderDeny.Type == "") isOrderFull = false;

                        if (orderDocument.RegularyDelay == null || orderDocument.RegularyDelay.Type == "")
                        {
                            isOrderFull = false;
                        }
                        else
                        {
                            regularyDelay = orderDocument.RegularyDelay.Type.Split(new char[] { '-' });
                        }
                        var DownloadAddressQuery =
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

                        if(DownloadAddressQuery == null)
                        {
                            isOrderFull = false;
                        }
                        if (UploaddAddressQuery == null)
                        {
                            isOrderFull = false;
                        }
                        if (CustomAddressQuery == null)
                        {
                            isOrderFull = false;
                        }
                        if (UncustomAddressQuery == null)
                        {
                            isOrderFull = false;
                        }
                    }
                    else
                    {
                        isOrderLanguageSelected = false;
                        isOrderFull = false;
                        MessageBox.Show("Виберіть мову у меню редагування");
                    }
                }
                else
                {
                    isOrderFull = false;
                }
                if (!isOrderFull && isOrderLanguageSelected)
                {
                    if (MessageBox.Show("Продовжити без повного заповнення даних?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        isOrderFull = true;
                    }
                }
            }
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
                    wordApp.Visible = false;
                    wordDocument = wordApp.Documents.Open((System.AppDomain.CurrentDomain.BaseDirectory + ((orderDocument.Language == 0) ? @"Resources\ukrOrder.docx" : (orderDocument.Language == 1) ? @"Resources\polOrder.docx" : @"Resources\gerOrder.docx")).Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""));
                    orderDocument = db.Orders.Find(ClikedId);

                    if (orderDocument != null)
                    {
                        string orderNumber = "";
                        string createDate = "";
                        string forwarderName1 = "";
                        string forwarderName2 = "";
                        string loadingForm1 = "";
                        string loadingForm2 = "";
                        string downloadDate = "";
                        string dateTerms = "";
                        string downloadAddress = "";
                        string uploadAddress = "";
                        string customAddress = "";
                        string uncustomAddress = "";
                        string downloadAddressContactPerson = "";
                        string[] regularyDelay;

                        orderNumber = orderDocument.IndexNumber + @"\" + orderDocument.Date.Value.Year;
                        createDate = orderDocument.Date.Value.ToShortDateString();

                        
                        downloadDate = (!orderDocument.DownloadDateFrom.HasValue) ? "" : (!orderDocument.DownloadDateTo.HasValue) ? orderDocument.DownloadDateFrom.Value.ToShortDateString() : (orderDocument.DownloadDateFrom.Value.Month != orderDocument.DownloadDateTo.Value.Month) ? orderDocument.DownloadDateFrom.Value.Day + "." + orderDocument.DownloadDateFrom.Value.Month + "-" + orderDocument.DownloadDateTo.Value : orderDocument.DownloadDateFrom.Value.Day + "-" + orderDocument.DownloadDateTo.Value + " на ";
                        downloadDate += (!orderDocument.DownloadDateFrom.HasValue) ? "" : orderDocument.DownloadDateFrom.Value.ToShortTimeString();
                      
                        dateTerms = (!orderDocument.DownloadDateFrom.HasValue) ? "" : (!orderDocument.DownloadDateTo.HasValue) ? orderDocument.DownloadDateFrom.Value.ToShortDateString() : (orderDocument.DownloadDateFrom.Value.Month != orderDocument.DownloadDateTo.Value.Month) ? orderDocument.DownloadDateFrom.Value.Day.ToString().PadLeft(2,'0') + "." + orderDocument.DownloadDateFrom.Value.Month.ToString().PadLeft(2, '0') + "-" + orderDocument.DownloadDateTo.Value.ToShortDateString() : orderDocument.DownloadDateFrom.Value.Day.ToString().PadLeft(2, '0') + "-" + orderDocument.DownloadDateTo.Value.ToShortDateString() + " — ";
                        dateTerms +=(!orderDocument.UploadDateFrom.HasValue) ? "" : (!orderDocument.UploadDateTo.HasValue) ? orderDocument.UploadDateFrom.Value.ToShortDateString() : (orderDocument.UploadDateFrom.Value.Month != orderDocument.UploadDateTo.Value.Month) ? orderDocument.UploadDateFrom.Value.Day.ToString().PadLeft(2, '0') + "." + orderDocument.UploadDateFrom.Value.Month.ToString().PadLeft(2, '0') + "-" + orderDocument.UploadDateTo.Value.ToShortDateString() : orderDocument.UploadDateFrom.Value.Day.ToString().PadLeft(2, '0') + "-" + orderDocument.UploadDateTo.Value.ToShortDateString() + "до " + orderDocument.UploadDateTo.Value.ToShortTimeString();

                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).Count() == 1)
                        {
                            forwarderName1 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder.Name;
                        }
                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).Count() == 1)
                        {
                            forwarderName2 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).FirstOrDefault().Forwarder.Name;
                        }
                        //TODO 3 forwarder
                        if (orderDocument.OrderLoadingForms.Where(l => l.IsFirst == true).Count() == 1)
                        {
                            loadingForm1 = orderDocument.OrderLoadingForms.Where(l => l.IsFirst == true).FirstOrDefault().LoadingForm.Type;

                        }
                        if (orderDocument.OrderLoadingForms.Where(l => l.IsFirst == false).Count() == 1)
                        {
                            loadingForm2 = orderDocument.OrderLoadingForms.Where(l => l.IsFirst == false).FirstOrDefault().LoadingForm.Type;
                        }


                        string transporterName = (orderDocument.Transporter == null || orderDocument.Transporter.FullName == "") ? "" : orderDocument.Transporter.FullName;
                        string cargoType = (orderDocument.Cargo == null || orderDocument.Cargo.Type == "") ? "" : orderDocument.Cargo.Type + ", ";
                        string cube = (orderDocument.Cube == null || orderDocument.Cube.Type == "") ? "" : orderDocument.Cube.Type + ", ";
                        string additionalTerms = (orderDocument.AdditionalTerm == null || orderDocument.AdditionalTerm.Type == "") ? "" : orderDocument.AdditionalTerm.Type;
                        string trailer = (orderDocument.Trailer == null || orderDocument.Trailer.Type == "") ? "" : orderDocument.Trailer.Type + ", ";
                        string paymentTerms = (orderDocument.Payment == null || orderDocument.Payment.Type == "") ? "" : orderDocument.Payment.Type;
                        string freight = (orderDocument.Freight == null || orderDocument.Freight == "") ? "" : orderDocument.Freight;
                        string fineForDelay = (orderDocument.FineForDelay == null || orderDocument.FineForDelay.Type == "") ? "_____________________" : orderDocument.FineForDelay.Type;
                        string weight = (Convert.ToString(orderDocument.CargoWeight) == null) ? "" : Convert.ToString(orderDocument.CargoWeight) + " т";
                        string orderDeny = (orderDocument.OrderDeny == null || orderDocument.OrderDeny.Type == "") ? "____________________" : orderDocument.OrderDeny.Type;

                        if (orderDocument.RegularyDelay == null || orderDocument.RegularyDelay.Type == "" || orderDocument.RegularyDelay.Type.Split(new char[] { '-' }).Count() < 4 )
                        {
                            regularyDelay = new string[] { "___", "___", "___", "___" };
                        }
                        else
                        {
                            regularyDelay = orderDocument.RegularyDelay.Type.Split(new char[] { '-' });
                        }
                        var DownloadAddressQuery =
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

                        if (DownloadAddressQuery.ToList().Count != 0)
                        {
                            downloadAddressContactPerson = (DownloadAddressQuery.FirstOrDefault().DownloadAddress.ContactPerson == null || DownloadAddressQuery.FirstOrDefault().DownloadAddress.ContactPerson == "") ? "" : DownloadAddressQuery.FirstOrDefault().DownloadAddress.ContactPerson;
                        }

                        foreach(var address in DownloadAddressQuery)
                        {
                            downloadAddress += ((address.DownloadAddress.StreetName != "" && address.DownloadAddress != null) ? address.DownloadAddress.StreetName + " " : "");
                            downloadAddress += ((address.DownloadAddress.HouseNumber != "" && address.DownloadAddress != null) ? address.DownloadAddress.HouseNumber + ", " : "");
                            downloadAddress += ((address.DownloadAddress.CityName != "" && address.DownloadAddress != null) ? address.DownloadAddress.CityName + ", " : "");
                            downloadAddress += address.DownloadAddress.Country.Name + Environment.NewLine;
                        }

                        foreach (var address in UploaddAddressQuery)
                        {
                            uploadAddress += ((address.UploadAddress.StreetName != "" && address.UploadAddress != null) ? address.UploadAddress.StreetName + " " : "");
                            uploadAddress += ((address.UploadAddress.HouseNumber != "" && address.UploadAddress != null) ? address.UploadAddress.HouseNumber + ", " : "");
                            uploadAddress += ((address.UploadAddress.CityName != "" && address.UploadAddress != null) ? address.UploadAddress.CityName + ", " : "");
                            uploadAddress += address.UploadAddress.Country.Name + Environment.NewLine;
                        }

                        foreach (var address in CustomAddressQuery)
                        {
                            customAddress += ((address.CustomsAddress.StreetName != "" && address.CustomsAddress != null) ? address.CustomsAddress.StreetName + " " : "");
                            customAddress += ((address.CustomsAddress.HouseNumber != "" && address.CustomsAddress != null) ? address.CustomsAddress.HouseNumber + ", " : "");
                            customAddress += ((address.CustomsAddress.CityName != "" && address.CustomsAddress != null) ? address.CustomsAddress.CityName + ", " : "");
                            customAddress += address.CustomsAddress.Country.Name + Environment.NewLine;
                        }

                        foreach (var address in UncustomAddressQuery)
                        {
                            uncustomAddress += ((address.UnCustomsAddress.StreetName != "" && address.UnCustomsAddress != null) ? address.UnCustomsAddress.StreetName + " " : "");
                            uncustomAddress += ((address.UnCustomsAddress.HouseNumber != "" && address.UnCustomsAddress != null) ? address.UnCustomsAddress.HouseNumber + ", " : "");
                            uncustomAddress += ((address.UnCustomsAddress.CityName != "" && address.UnCustomsAddress != null) ? address.UnCustomsAddress.CityName + ", " : "");
                            uncustomAddress += address.UnCustomsAddress.Country.Name + Environment.NewLine;
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


                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).Count() == 1)
                        {
                            if(orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder.ForwarderStamp.Stamp != null)
                            {
                                AddStamp(wordDocument, UploadForwarderStapm(orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder), "{Stamp1}");
                                Directory.Delete((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp\").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""), true);
                                Directory.CreateDirectory((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""));
                            }
                        }
                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).Count() == 1)
                        {
                            if(orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).FirstOrDefault().Forwarder.ForwarderStamp.Stamp != null && orderDocument.Language == 0)
                            {
                                AddStamp(wordDocument, UploadForwarderStapm(orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).FirstOrDefault().Forwarder), "{Stamp2}");
                                Directory.Delete((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp\").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""), true);
                                Directory.CreateDirectory((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""));
                            }
                        }
                        //TODO 3 forwarder

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
                    else
                    {
                        wordDocument.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                        MessageBox.Show("Помилка!");
                    }
                }
            }
            catch (NullReferenceException nullClickedId)
            {
                Log.Write(nullClickedId);
                contractShowOpenDocButton.Enabled = false;
                contractShowDeleteContractButton.Enabled = false;
                contractShowTransporterContactDataGridView.Visible = false;
                MessageBox.Show("Немає жодної заявки" + nullClickedId.Message);
            }
            catch (System.Runtime.InteropServices.COMException wordException)
            {
                Log.Write(wordException);
                if (wordDocument != null)
                {
                wordDocument.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                }
                MessageBox.Show("Помилка, спробуйте ще раз");
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
                        contractCount = null;
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: " + ex.Message);
                }

                orderCount = new OrderCounter();
                orderDocument = db.Orders.Find(ClikedId);
                try
                {
                    if (orderDocument.State == null)
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
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: " + ex.Message);
                }

                try
                {

                    if (orderDocument.State == null)
                    {
                        if (orderDocument.Language == 0)
                        {
                            orderDocument.IndexNumber = orderCount.LocalOrder;
                        }
                        else if (orderDocument.Language == 1 || orderDocument.Language == 2)
                        {
                            orderDocument.IndexNumber = orderCount.ForeignOrder;
                        }
                        orderDocument.Date = DateTime.Now;

                        orderDocument.State = true;
                    }
                    db.Entry(orderDocument).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }
    }
}
