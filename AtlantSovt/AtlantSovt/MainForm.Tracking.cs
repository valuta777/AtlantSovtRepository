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
        public ExportTrackingToExcelForm exportToExcel;
        public AddTrackingCloseDateForm closeDateForm;
        bool isOrderFull;
        bool isOrderLanguageSelected;

        int TrackingClikedId = 0;

        public void ShowTracking(DataGridView dataGridView)
        {
            using (var db = new AtlantSovtContext())
            {
                dataGridView.Columns.Clear();

                var query =
                from o in db.Orders
                where o.IsDeleted == false
                orderby o.Id
                select
                new
                {
                    Id = o.Id,
                    OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                    Staff = o.Staff.Type,
                    ClientName = o.Client.Name,
                    TransporterName = o.Transporter.FullName,
                    DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                    State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                    CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day + "." + o.CloseDate.Value.Month + "." + o.CloseDate.Value.Year,
                    Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька,
                };
                dataGridView.DataSource = query.ToList();
                dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;
            }
            dataGridView.Update();
            dataGridView.ClearSelection();
        }

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
                    trackingShowTransporterContactsDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Контактна_особа;
                    trackingShowTransporterContactsDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Телефон;
                    trackingShowTransporterContactsDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Факс;
                    trackingShowTransporterContactsDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Email;

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
                    trackingShowCommentDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Коментар0;
                    trackingShowCommentDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Дата_додавання;
                    trackingShowCommentDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Дата_останньої_зміни;

                    trackingShowCommentDataGridView.Columns[1].Width = 150;
                    trackingShowCommentDataGridView.Columns[2].Width = 150;

                    var query3 =
                  from add in db.OrderDownloadAddresses
                  where add.OrderId == TrackingClikedId
                  select new
                  {
                      country = add.DownloadAddress.Country.Name,
                      cityCode = add.DownloadAddress.CityCode,
                      cityName = add.DownloadAddress.CityName
                  };
                    trackingShowDownloadAddressDataGridView.DataSource = query3.ToList();
                    trackingShowDownloadAddressDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Країна;
                    trackingShowDownloadAddressDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Код_міста;
                    trackingShowDownloadAddressDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Місто;

                    var query4 =
                 from add in db.OrderUploadAdresses
                 where add.OrderId == TrackingClikedId
                 select new
                 {
                     country = add.UploadAddress.Country.Name,
                     cityCode = add.UploadAddress.CityCode,
                     cityName = add.UploadAddress.CityName
                 };
                    trackingShowUploadAddressDataGridView.DataSource = query4.ToList();
                    trackingShowUploadAddressDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Країна;
                    trackingShowUploadAddressDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Код_міста;
                    trackingShowUploadAddressDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Місто;

                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодної_заявки);
                    trackingShowAddCommentButton.Enabled = false;
                    trackingShowCloseOrderButton.Enabled = false;
                    showTrackingCreateOrderDoc.Enabled = false;
                    trackingShowDeleteOrderButton.Enabled = false;
                    trackingShowAddNoteRichTextBox.Enabled = false;
                    return;
                }

                var query5 = 
                        from o in db.Orders
                        where o.Id == TrackingClikedId
                        select o.Note;
                trackingShowAddNoteRichTextBox.Text = query5.FirstOrDefault();

                var query6 =
                    from f in db.ForwarderOrders
                    where f.OrderId == TrackingClikedId
                    orderby f.IsFirst
                    select new
                    {
                        forwarderNumber = (f.IsFirst == 1) ? AtlantSovt.Properties.Resources.Експедитор_1 : (f.IsFirst == 2) ? AtlantSovt.Properties.Resources.Експедитор_2 : AtlantSovt.Properties.Resources.Експедитор_3,
                        forwarderName = f.Forwarder.Name
                    };
                trackingShowForwardersDataGridView.DataSource = query6.ToList();
                trackingShowForwardersDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Номер_експедитора;
                trackingShowForwardersDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Назва;
            }
            trackingShowTransporterContactsDataGridView.Update();

            trackingShowTransporterContactsDataGridView.Visible = true;
            trackingShowCommentDataGridView.Visible = true;
            trackingShowUploadAddressDataGridView.Visible = true;
            trackingShowDownloadAddressDataGridView.Visible = true;
            trackingShowAddNoteRichTextBox.Visible = true;
            trackingShowForwardersDataGridView.Visible = true;
        }

        public void ShowTrackingSearch(DataGridView dataGridView, TextBox textBox, DateTimePicker dateTime, System.Windows.Forms.CheckBox checkBox)
        {
            var text = textBox.Text;
            string[] orderNumber = text.Split('/');
            int indexNumber = 0;
            int yearCreate = 0;
            int orderId = 0;
            Int32.TryParse(text, out orderId);
            if(orderNumber.Count() == 2)
            {
                Int32.TryParse(orderNumber[0], out indexNumber);
                Int32.TryParse(orderNumber[1], out yearCreate);
            }

            using (var db = new AtlantSovtContext())
            {
                dataGridView.Columns.Clear();
                if (checkBox.Checked != true && dateTime.Checked != true && textBox.Text == "")// 0 0 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where o.IsDeleted == false
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                       CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька
                   };
                    dataGridView.DataSource = queryTextAndDate.ToList();
                    dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                    dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                    dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                    dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;
                }
                else if (checkBox.Checked != true && dateTime.Checked != true && textBox.Text != "") // 0 0 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.Transporter.ShortName.Contains(text)  || o.Staff.Type.Contains(text) || o.Note.Contains(text) || (o.IndexNumber == indexNumber && o.Date.Value.Year == yearCreate) || o.Id == orderId ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) && o.IsDeleted == false
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                        CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька

                   };
                    dataGridView.DataSource = queryTextAndDate.ToList();
                    dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                    dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                    dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                    dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;

                }
                else if (checkBox.Checked != true && dateTime.Checked == true && textBox.Text == "") // 0 1 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where ((o.DownloadDateFrom.Value.Month == dateTime.Value.Month) ||
                   (o.DownloadDateTo.Value.Month == dateTime.Value.Month)) && ((o.DownloadDateFrom.Value.Year == dateTime.Value.Year) || o.DownloadDateTo.Value.Year == dateTime.Value.Year) && o.IsDeleted == false
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                       CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька

                   };
                    dataGridView.DataSource = queryTextAndDate.ToList();
                    dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                    dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                    dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                    dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;

                }
                else if (checkBox.Checked != true && dateTime.Checked == true && textBox.Text != "")// 0 1 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.Transporter.ShortName.Contains(text) || o.Staff.Type.Contains(text) || o.Note.Contains(text) || (o.IndexNumber == indexNumber && o.Date.Value.Year == yearCreate) || o.Id == orderId ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) && ((o.DownloadDateFrom.Value.Month == dateTime.Value.Month) ||
                   (o.DownloadDateTo.Value.Month == dateTime.Value.Month)) && ((o.DownloadDateFrom.Value.Year == dateTime.Value.Year) || o.DownloadDateTo.Value.Year == dateTime.Value.Year) && o.IsDeleted == false
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                       CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька

                   };
                    dataGridView.DataSource = queryTextAndDate.ToList();
                    dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                    dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                    dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                    dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;

                }
                else if (checkBox.Checked == true && dateTime.Checked != true && textBox.Text == "") // 1 0 0
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.State == true) && o.IsDeleted == false
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                       CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька

                   };
                    dataGridView.DataSource = queryTextAndDate.ToList();
                    dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                    dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                    dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                    dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;

                }
                else if (checkBox.Checked == true && dateTime.Checked != true && textBox.Text != "") // 1 0 1
                {
                    var queryTextAndDate =
                   from o in db.Orders
                   where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.Transporter.ShortName.Contains(text)  || o.Staff.Type.Contains(text) || o.Note.Contains(text) || (o.IndexNumber == indexNumber && o.Date.Value.Year == yearCreate) || o.Id == orderId ||
                         o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) && (o.State == true) && o.IsDeleted == false
                   orderby o.Id
                   select
                   new
                   {
                       Id = o.Id,
                       OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                       Staff = o.Staff.Type,
                       ClientName = o.Client.Name,
                       TransporterName = o.Transporter.FullName,
                       DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                       State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                       CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                       Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька

                   };
                    dataGridView.DataSource = queryTextAndDate.ToList();
                    dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                    dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                    dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                    dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;
                }
                else if (checkBox.Checked == true && dateTime.Checked == true && textBox.Text == "")// 1 1 0
                {
                    var queryTextAndDate =
                  from o in db.Orders
                  where (o.State == true && ((o.DownloadDateFrom.Value.Month == dateTime.Value.Month) ||
                   (o.DownloadDateTo.Value.Month == dateTime.Value.Month)) && ((o.DownloadDateFrom.Value.Year == dateTime.Value.Year) || o.DownloadDateTo.Value.Year == dateTime.Value.Year)) && o.IsDeleted == false
                  orderby o.Id
                  select
                  new
                  {
                      Id = o.Id,
                      OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                      Staff = o.Staff.Type,
                      ClientName = o.Client.Name,
                      TransporterName = o.Transporter.FullName,
                      DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                      State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                      CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                      Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька

                  };
                    dataGridView.DataSource = queryTextAndDate.ToList();
                    dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                    dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                    dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                    dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;

                }
                else if (checkBox.Checked == true && dateTime.Checked == true && textBox.Text != "")// 1 1 1
                {
                    var queryTextAndDate =
                  from o in db.Orders
                  where (o.Client.Name.Contains(text) || o.Transporter.FullName.Contains(text) || o.Transporter.ShortName.Contains(text) || o.Staff.Type.Contains(text) || o.Note.Contains(text) || (o.IndexNumber == indexNumber && o.Date.Value.Year == yearCreate) || o.Id == orderId ||
                        o.Transporter.TransporterContacts.Any(c => c.TelephoneNumber.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.Email.Contains(text)) || o.Transporter.TransporterContacts.Any(c => c.ContactPerson.Contains(text))) &&
                        ((o.DownloadDateFrom.Value.Month == dateTime.Value.Month) || (o.DownloadDateTo.Value.Month == dateTime.Value.Month)) && ((o.DownloadDateFrom.Value.Year == dateTime.Value.Year) ||
                        o.DownloadDateTo.Value.Year == dateTime.Value.Year) && o.State == true && o.IsDeleted == false
                  orderby o.Id
                  select
                  new
                  {
                      Id = o.Id,
                      OrderNumber = (!o.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : o.IndexNumber + "/" + o.Date.Value.Year,
                      Staff = o.Staff.Type,
                      ClientName = o.Client.Name,
                      TransporterName = o.Transporter.FullName,
                      DownloadDate = (!o.DownloadDateTo.HasValue) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + "." + o.DownloadDateFrom.Value.Year : (o.DownloadDateFrom.Value.Month != o.DownloadDateTo.Value.Month) ? o.DownloadDateFrom.Value.Day + "." + o.DownloadDateFrom.Value.Month + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year : o.DownloadDateFrom.Value.Day + " - " + o.DownloadDateTo.Value.Day + "." + o.DownloadDateTo.Value.Month + "." + o.DownloadDateTo.Value.Year,
                      State = (!o.State.HasValue) ? AtlantSovt.Properties.Resources.Не_створена : ((o.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита),
                      CloseDate = (!o.CloseDate.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено : o.CloseDate.Value.Day.ToString() + "." + o.CloseDate.Value.Month.ToString() + "." + o.CloseDate.Value.Year.ToString(),
                      Language = (!o.Language.HasValue) ? AtlantSovt.Properties.Resources.Не_вибрано : (o.Language == 0) ? AtlantSovt.Properties.Resources.Українська : (o.Language == 1) ? AtlantSovt.Properties.Resources.Польська : AtlantSovt.Properties.Resources.Німецька

                  };
                    dataGridView.DataSource = queryTextAndDate.ToList();
                    dataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    dataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    dataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Працівник;
                    dataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    dataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    dataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    dataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Стан;
                    dataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Дата_закриття;
                    dataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Мова;

                } dataGridView.Update();
                }
        }

        void CloseOrder()
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
                            closeDateForm = new AddTrackingCloseDateForm(this);
                            closeDateForm.Id = TrackingClikedId;
                            closeDateForm.Show();
                        }
                        else if (order.State == false)
                        {
                            if (MessageBox.Show(AtlantSovt.Properties.Resources.Заявка_вже_закрита_змінити_дату_закриття, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                closeDateForm = new AddTrackingCloseDateForm(this);
                                closeDateForm.Id = TrackingClikedId;
                                closeDateForm.Show();
                            }
                        }
                        else if (!order.State.HasValue)
                        {
                            if (MessageBox.Show(AtlantSovt.Properties.Resources.Заявка_ще_не_створена_все_рівно_закрити, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                closeDateForm = new AddTrackingCloseDateForm(this);
                                closeDateForm.Id = TrackingClikedId;
                                closeDateForm.Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Заявці_не_призначено_номер_спочатку_створіть_документ);
                        return;
                        
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодної_заявки);
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
                        if (orderDocument.CargoWeight == null) isOrderFull = false;
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
                        MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_мову_у_меню_редагування);
                    }
                }
                else
                {
                    isOrderFull = false;
                }
                if (!isOrderFull && isOrderLanguageSelected)
                {
                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Продовжити_без_повного_заповнення_даних, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                    orderDocument = db.Orders.Find(ClikedId);
                    wordDocument = wordApp.Documents.Open((System.AppDomain.CurrentDomain.BaseDirectory + ((orderDocument.Language == 0) ? ((orderDocument.ForwarderOrders.Where(f => f.IsFirst == 3).Count() != 0) ? @"Resources\Orders\ukrOrderFor3.docx" : @"Resources\Orders\ukrOrderFor2.docx") : (orderDocument.Language == 1) ? @"Resources\Orders\polOrder.docx" : @"Resources\gerOrder.docx")).Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""));

                    if (orderDocument != null)
                    {
                        string orderNumber = "";
                        string createDate = "";
                        string forwarderName1 = "";
                        string forwarderName2 = "";
                        string forwarderName3 = "";
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

                        orderNumber = orderDocument.IndexNumber + @"/" + orderDocument.Date.Value.Year;
                        createDate = orderDocument.Date.Value.ToShortDateString();

                        
                        
                       
                        if (orderDocument.DownloadDateFrom.HasValue)
                        {
                            if (orderDocument.DownloadDateTo.HasValue)
                            {
                                if (orderDocument.DownloadDateFrom.Value.Year != orderDocument.DownloadDateTo.Value.Year)
                                {
                                    dateTerms += orderDocument.DownloadDateFrom.Value.Day.ToString().PadLeft(2, '0') +
                                        "." + orderDocument.DownloadDateFrom.Value.Month.ToString().PadLeft(2, '0') +
                                        "." + orderDocument.DownloadDateFrom.Value.Year.ToString() +
                                        " - " + orderDocument.DownloadDateTo.Value.ToShortDateString();
                                }
                                else if (orderDocument.DownloadDateFrom.Value.Month != orderDocument.DownloadDateTo.Value.Month)
                                {
                                    dateTerms += orderDocument.DownloadDateFrom.Value.Day.ToString().PadLeft(2, '0') +
                                        "." + orderDocument.DownloadDateFrom.Value.Month.ToString().PadLeft(2, '0') +
                                        " - " + orderDocument.DownloadDateTo.Value.ToShortDateString();
                                }
                                else
                                {
                                    dateTerms += orderDocument.DownloadDateFrom.Value.Day.ToString().PadLeft(2, '0') +
                                        " - " + orderDocument.DownloadDateTo.Value.ToShortDateString();
                                }
                                downloadDate = dateTerms;
                                downloadDate += " на " + orderDocument.DownloadDateTo.Value.ToShortTimeString();
                            }
                            else
                            {
                                dateTerms += orderDocument.DownloadDateFrom.Value.ToShortDateString();
                                downloadDate = dateTerms;
                                downloadDate += " на " + orderDocument.DownloadDateFrom.Value.ToShortTimeString();
                            }                            
                            dateTerms += " — ";
                        }
                        else
                        {
                            dateTerms = "";
                        }

                        if (orderDocument.UploadDateFrom.HasValue)
                        {
                            if (orderDocument.UploadDateTo.HasValue)
                            {
                                if (orderDocument.UploadDateFrom.Value.Year != orderDocument.UploadDateTo.Value.Year)
                                {
                                    dateTerms += orderDocument.UploadDateFrom.Value.Day.ToString().PadLeft(2, '0') +
                                        "." + orderDocument.UploadDateFrom.Value.Month.ToString().PadLeft(2, '0') +
                                        "." + orderDocument.UploadDateFrom.Value.Year.ToString() +
                                        " - " + orderDocument.UploadDateTo.Value.ToShortDateString();
                                    dateTerms += " до " + orderDocument.UploadDateTo.Value.ToShortTimeString();
                                }
                                else if (orderDocument.UploadDateFrom.Value.Month != orderDocument.UploadDateTo.Value.Month)
                                {
                                    dateTerms += orderDocument.UploadDateFrom.Value.Day.ToString().PadLeft(2, '0') +
                                        "." + orderDocument.UploadDateFrom.Value.Month.ToString().PadLeft(2, '0') +
                                        " - " + orderDocument.UploadDateTo.Value.ToShortDateString();
                                    dateTerms += " до " + orderDocument.UploadDateTo.Value.ToShortTimeString();
                                }
                                else
                                {
                                    dateTerms += orderDocument.UploadDateFrom.Value.Day.ToString().PadLeft(2, '0') +
                                        " - " + orderDocument.UploadDateTo.Value.ToShortDateString();
                                    dateTerms += " до " + orderDocument.UploadDateTo.Value.ToShortTimeString();
                                }
                            }
                            else
                            {
                                dateTerms += orderDocument.UploadDateFrom.Value.ToShortDateString();
                                dateTerms += " до " + orderDocument.UploadDateFrom.Value.ToShortTimeString();
                            }
                        }
                        else
                        {
                            dateTerms = "";
                        }

                      

                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).Count() == 1)
                        {
                            forwarderName1 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == 1).FirstOrDefault().Forwarder.Name;
                        }
                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).Count() == 1)
                        {
                            forwarderName2 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == 2).FirstOrDefault().Forwarder.Name;
                        }
                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 3).Count() == 1)
                        {
                            forwarderName3 = orderDocument.ForwarderOrders.Where(f => f.IsFirst == 3).FirstOrDefault().Forwarder.Name;
                        }
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
                        string cube = (orderDocument.Cube == null || orderDocument.Cube.Type == "") ? "" : orderDocument.Cube.Type + " м³, ";
                        string additionalTerms = (orderDocument.AdditionalTerm == null || orderDocument.AdditionalTerm.Type == "") ? "" : orderDocument.AdditionalTerm.Type;
                        string trailer = (orderDocument.Trailer == null || orderDocument.Trailer.Type == "") ? "" : orderDocument.Trailer.Type + ", ";
                        string paymentTerms = (orderDocument.Payment == null || orderDocument.Payment.Type == "") ? "" : orderDocument.Payment.Type;
                        string freight = (orderDocument.Freight == null || orderDocument.Freight == "") ? "" : orderDocument.Freight;
                        string fineForDelay = (orderDocument.FineForDelay == null || orderDocument.FineForDelay.Type == "") ? "_____________________" : orderDocument.FineForDelay.Type;
                        string weight = (orderDocument.CargoWeight == null || orderDocument.CargoWeight == "") ? "" : orderDocument.CargoWeight;
                        string orderDeny = (orderDocument.OrderDeny == null || orderDocument.OrderDeny.Type == "") ? "____________________" : orderDocument.OrderDeny.Type;
                        string tirCmr = (orderDocument.TirCmr == null || orderDocument.TirCmr.Type == "") ? "" : orderDocument.TirCmr.Type;

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
                            if(address.DownloadAddress != null)
                            {
                                if (address.DownloadAddress.Country != null)
                                {
                                    downloadAddress += (address.DownloadAddress.Country.Name != "" && address.DownloadAddress.Country.Name != null ) ? address.DownloadAddress.Country.Name + ", " : "";
                                }
                                downloadAddress += (address.DownloadAddress.CountryCode != "" && address.DownloadAddress.CountryCode != null) ? address.DownloadAddress.CountryCode + " - " : "";
                                downloadAddress += (address.DownloadAddress.CityCode != "" && address.DownloadAddress.CityCode != null) ? address.DownloadAddress.CityCode + ", " : "";
                                downloadAddress += (address.DownloadAddress.CityName != "" && address.DownloadAddress.CityName != null) ? address.DownloadAddress.CityName + ", " : "";
                                downloadAddress += (address.DownloadAddress.StreetName != "" && address.DownloadAddress.StreetName != null) ? address.DownloadAddress.StreetName + ", " : "";
                                downloadAddress += (address.DownloadAddress.HouseNumber != "" && address.DownloadAddress.HouseNumber != null) ? address.DownloadAddress.HouseNumber + ", " : "";
                                downloadAddress += (address.DownloadAddress.CompanyName != "" && address.DownloadAddress.CompanyName != null) ? address.DownloadAddress.CompanyName + "\v" : "\v";
                            }
                        }

                        foreach (var address in UploaddAddressQuery)
                        {
                            if (address.UploadAddress != null)
                            {
                                if (address.UploadAddress.Country != null)
                                {
                                    uploadAddress += (address.UploadAddress.Country.Name != "" && address.UploadAddress.Country.Name != null) ? address.UploadAddress.Country.Name + ", " : "";
                                }
                                uploadAddress += (address.UploadAddress.CountryCode != "" && address.UploadAddress.CountryCode != null) ? address.UploadAddress.CountryCode + " - " : "";
                                uploadAddress += (address.UploadAddress.CityCode != "" && address.UploadAddress.CityCode != null) ? address.UploadAddress.CityCode + ", " : "";
                                uploadAddress += (address.UploadAddress.CityName != "" && address.UploadAddress.CityName != null) ? address.UploadAddress.CityName + ", " : "";
                                uploadAddress += (address.UploadAddress.StreetName != "" && address.UploadAddress.StreetName != null) ? address.UploadAddress.StreetName + ", " : "";
                                uploadAddress += (address.UploadAddress.HouseNumber != "" && address.UploadAddress.HouseNumber != null) ? address.UploadAddress.HouseNumber + ", " : "";
                                uploadAddress += (address.UploadAddress.CompanyName != "" && address.UploadAddress.CompanyName != null) ? address.UploadAddress.CompanyName + "," : "";
                            }
                        }

                        foreach (var address in CustomAddressQuery)
                        {
                            if (address.CustomsAddress != null)
                            {
                                if (address.CustomsAddress.Country != null)
                                {
                                    customAddress += (address.CustomsAddress.Country.Name != "" && address.CustomsAddress.Country.Name != null) ? address.CustomsAddress.Country.Name + ", " : "";
                                }
                                customAddress += (address.CustomsAddress.CountryCode != "" && address.CustomsAddress.CountryCode != null) ? address.CustomsAddress.CountryCode + " - " : "";
                                customAddress += (address.CustomsAddress.CityCode != "" && address.CustomsAddress.CityCode != null) ? address.CustomsAddress.CityCode + ", " : "";
                                customAddress += (address.CustomsAddress.CityName != "" && address.CustomsAddress.CityName != null) ? address.CustomsAddress.CityName + ", " : "";
                                customAddress += (address.CustomsAddress.StreetName != "" && address.CustomsAddress.StreetName != null) ? address.CustomsAddress.StreetName + ", " : "";
                                customAddress += (address.CustomsAddress.HouseNumber != "" && address.CustomsAddress.HouseNumber != null) ? address.CustomsAddress.HouseNumber + ", " : "";
                                customAddress += (address.CustomsAddress.CompanyName != "" && address.CustomsAddress.CompanyName != null) ? address.CustomsAddress.CompanyName : "";
                            }
                        }

                        foreach (var address in UncustomAddressQuery)
                        {
                            if (address.UnCustomsAddress != null)
                            {
                                if (address.UnCustomsAddress.Country != null)
                                {
                                    uncustomAddress += (address.UnCustomsAddress.Country.Name != "" && address.UnCustomsAddress.Country.Name != null) ? address.UnCustomsAddress.Country.Name + ", " : "";
                                }
                                uncustomAddress += (address.UnCustomsAddress.CountryCode != "" && address.UnCustomsAddress.CountryCode != null) ? address.UnCustomsAddress.CountryCode + " - " : "";
                                uncustomAddress += (address.UnCustomsAddress.CityCode != "" && address.UnCustomsAddress.CityCode != null) ? address.UnCustomsAddress.CityCode + ", " : "";
                                uncustomAddress += (address.UnCustomsAddress.CityName != "" && address.UnCustomsAddress.CityName != null) ? address.UnCustomsAddress.CityName + ", " : "";
                                uncustomAddress += (address.UnCustomsAddress.StreetName != "" && address.UnCustomsAddress.StreetName != null) ? address.UnCustomsAddress.StreetName + ", " : "";
                                uncustomAddress += (address.UnCustomsAddress.HouseNumber != "" && address.UnCustomsAddress.HouseNumber != null) ? address.UnCustomsAddress.HouseNumber + ", " : "";
                                uncustomAddress += (address.UnCustomsAddress.CompanyName != "" && address.UnCustomsAddress.CompanyName != null) ? address.UnCustomsAddress.CompanyName : "";
                            }
                        }

                        ReplaseWordStub("{ForwarderName1}", forwarderName1, wordDocument);
                        ReplaseWordStub("{ForwarderName2}", forwarderName2, wordDocument);
                        ReplaseWordStub("{ForwarderName3}", forwarderName3, wordDocument);
                        ReplaseWordStub("{OrderNumber}", orderNumber, wordDocument);
                        ReplaseWordStub("{CreateDate}", createDate, wordDocument);
                        ReplaseWordStub("{DownloadDate}", downloadDate, wordDocument);
                        ReplaseWordStub("{DownloadAddress}", downloadAddress, wordDocument);
                        ReplaseWordStub("{DownloadAddressContactPerson}", downloadAddressContactPerson, wordDocument);
                        ReplaseWordStub("{CustomAddress}", customAddress, wordDocument);
                        ReplaseWordStub("{CargoType}", cargoType, wordDocument);
                        ReplaseWordStub("{Weight}", weight, wordDocument);
                        ReplaseWordStub("{TIRCMR}", tirCmr, wordDocument);
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
                        if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 3).Count() == 1)
                        {
                            if (orderDocument.ForwarderOrders.Where(f => f.IsFirst == 3).FirstOrDefault().Forwarder.ForwarderStamp.Stamp != null && orderDocument.Language == 0)
                            {
                                AddStamp(wordDocument, UploadForwarderStapm(orderDocument.ForwarderOrders.Where(f => f.IsFirst == 3).FirstOrDefault().Forwarder), "{Stamp3}");
                                Directory.Delete((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp\").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""), true);
                                Directory.CreateDirectory((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Temp").Replace("\\bin\\Release", "").Replace("\\bin\\Debug", ""));
                            }
                        }

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            orderNumber = orderNumber.Replace(@"/", "_");
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
                        MessageBox.Show(AtlantSovt.Properties.Resources.Виникла_помилка_спробуйте_ще_раз);
                    }
                }
            }
            catch (NullReferenceException nullClickedId)
            {
                Log.Write(nullClickedId);
                trackingShowAddCommentButton.Enabled = false;
                trackingShowCloseOrderButton.Enabled = false;
                showTrackingCreateOrderDoc.Enabled = false;
                exportTrackingToExcelButton.Enabled = false;
                trackingShowDeleteOrderButton.Enabled = false;
                MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодної_заявки);
            }
            catch (System.Runtime.InteropServices.COMException wordException)
            {
                Log.Write(wordException);
                if (wordDocument != null)
                {
                wordDocument.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                }
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_спробуйте_ще_раз);
            }
        }

        void ExportTrackingToExcel()
        {
            if (exportToExcel == null)
            {
                exportToExcel = new ExportTrackingToExcelForm(this);
                exportToExcel.Show();
                exportToExcel.Focus();
            }
            else
            {
                exportToExcel.Focus();
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
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
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
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
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
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                }
            }
        }

        void ChangeOrderDeleteState()
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    Order order;
                    TrackingClikedId = Convert.ToInt32(trackingShowDataGridView.CurrentRow.Cells[0].Value);
                    order = db.Orders.Find(TrackingClikedId);
                    if (order != null)
                    {
                        try
                        {
                            if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_заявку + " " + + order.Id + "?", AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                order.IsDeleted = true;
                                db.Entry(order).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Виникла_помилка_спробуйте_ще_раз);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодної_заявки);
                }
                ShowTrackingSearch(trackingShowDataGridView, trackingShowSearchTextBox, showTrackingDateTimePicker, showTrackingOnlyActive);
            }
            trackingShowDataGridView.Update();
        }
    }
}
