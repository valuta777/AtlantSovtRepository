using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    partial class MainForm
    {
        int ArbeitenClikedId = 0;
        Arbeiten deleteArbeiten;
        Arbeiten addArbeiten;
        Arbeiten updateArbeiten;
        Order addArbeitenOrder;
        Order updateArbeitenOrder;
        public void ShowArbeiten()
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                from ar in db.Arbeitens
                orderby ar.Id
                select
                new
                {
                    Id = ar.Id,
                    OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                    ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                    ClientAcountNumber = ar.ClientAccountNumber,
                    PaymentDate = ar.PaymentDate,
                    ClientPayment = ar.ClientPayment,
                    DownloadDate = ar.DownloadDate,
                    VehicleNumber = ar.VehicleNumber,
                    TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                    TransporterPayment = ar.TransporterPayment
                };
                arbeitenShowMainDataGridView.DataSource = query.ToList();
                arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;

            }
            arbeitenShowMainDataGridView.Update();
            arbeitenShowMainDataGridView.ClearSelection();
        }
        public void ShowArbeitenInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    ArbeitenClikedId = Convert.ToInt32(arbeitenShowMainDataGridView.CurrentRow.Cells[0].Value);
                    deleteArbeiten = db.Arbeitens.Find(ArbeitenClikedId);
                    if (deleteArbeiten != null)
                    {
                        arbeitenDeleteButton.Enabled = true;
                    }
                    else
                    {
                        arbeitenDeleteButton.Enabled = false;
                    }
                    var query2 =
                    from frw in db.ForwarderOrders
                    where frw.Order.Arbeiten.Id == ArbeitenClikedId
                    orderby frw.IsFirst
                    select new
                    {
                        forwarderNumber = (frw.IsFirst == 1) ? AtlantSovt.Properties.Resources.Експедитор_1 : (frw.IsFirst == 2) ? AtlantSovt.Properties.Resources.Експедитор_2 : AtlantSovt.Properties.Resources.Експедитор_3,
                        forwarderName = frw.Forwarder.Name
                    };
                    arbeitenShowForwardersDataGridView.DataSource = query2.ToList();
                    arbeitenShowForwardersDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Номер_експедитора;
                    arbeitenShowForwardersDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Назва;

                    //

                    var query3 =
                        from add in db.OrderDownloadAddresses
                        where add.Order.Arbeiten.Id == ArbeitenClikedId
                        select new
                        {
                            country = add.DownloadAddress.Country.Name,
                            cityCode = add.DownloadAddress.CityCode
                        };
                    arbeitenShowDownloadAddressDataGridView.DataSource = query3.ToList();
                    arbeitenShowDownloadAddressDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Країна;
                    arbeitenShowDownloadAddressDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Код_міста;

                    var query4 =
                        from add in db.OrderUploadAdresses
                        where add.Order.Arbeiten.Id == ArbeitenClikedId
                        select new
                        {
                            country = add.UploadAddress.Country.Name,
                            cityCode = add.UploadAddress.CityCode
                        };
                    arbeitenShowUploadAddressDataGridView.DataSource = query4.ToList();
                    arbeitenShowUploadAddressDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Країна;
                    arbeitenShowUploadAddressDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Код_міста;

                    var query5 =
                        from o in db.Orders
                        where o.Arbeiten.Id == ArbeitenClikedId
                        select o.Note;
                    arbeitenShowNoteTextBox.Text = query5.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
                }
            }
            arbeitenShowForwardersDataGridView.Update();
            arbeitenShowUploadAddressDataGridView.Update();
            arbeitenShowDownloadAddressDataGridView.Update();
            arbeitenShowNoteTextBox.Update();

            arbeitenShowUploadAddressDataGridView.Visible = true;
            arbeitenShowDownloadAddressDataGridView.Visible = true;
            arbeitenShowNoteTextBox.Visible = true;
            arbeitenShowForwardersDataGridView.Visible = true;
        }
        public void DeleteArbeiten()
        {
            using (var db = new AtlantSovtContext())
            {
                if (deleteArbeiten != null)
                {

                    if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_арбайтен, AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            db.Arbeitens.Remove(db.Arbeitens.Find(deleteArbeiten.Id));
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Запис_успішно_видалено);
                            ShowArbeiten();
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виберіть_спочатку_запис);
                }
            }
        }
        public void ShowArbeitenSearch()
        {

            arbeitenShowForwardersDataGridView.DataSource = null;
            arbeitenShowUploadAddressDataGridView.DataSource = null;
            arbeitenShowDownloadAddressDataGridView.DataSource = null;
            arbeitenShowNoteTextBox.Clear();


            var text = arbeitenShowSearchTextBox.Text;

            using (var db = new AtlantSovtContext())
            {
                if (arbeitenShowOnlyActive.Checked != true && arbeitenShowSearchDatePicker.Checked != true && arbeitenShowSearchTextBox.Text == "")// 0 0 0
                {
                    var queryTextAndDate =
                   from ar in db.Arbeitens
                   orderby ar.Id
                   select
                   new
                   {
                       Id = ar.Id,
                       OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                       ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                       ClientAcountNumber = ar.ClientAccountNumber,
                       PaymentDate = ar.PaymentDate,
                       ClientPayment = ar.ClientPayment,
                       DownloadDate = ar.DownloadDate,
                       VehicleNumber = ar.VehicleNumber,
                       TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                       TransporterPayment = ar.TransporterPayment
                   };
                    arbeitenShowMainDataGridView.DataSource = queryTextAndDate.ToList();
                    arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                    arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                    arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                    arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                    arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;


                }
                else if (arbeitenShowOnlyActive.Checked != true && arbeitenShowSearchDatePicker.Checked != true && arbeitenShowSearchTextBox.Text != "") // 0 0 1
                {
                    var queryTextAndDate =
                   from ar in db.Arbeitens
                   where (ar.Order.Client.Name.Contains(text) || ar.VehicleNumber.Contains(text) || ar.Order.OrderDownloadAddresses.Any(c => c.DownloadAddress.Country.Name.Contains(text) || c.DownloadAddress.CityCode.Contains(text))) || ar.Order.OrderUploadAdresses.Any(c => c.UploadAddress.Country.Name.Contains(text) || c.UploadAddress.CityCode.Contains(text))


                   orderby ar.Id
                   select
                   new
                   {
                       Id = ar.Id,
                       OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                       ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                       ClientAcountNumber = ar.ClientAccountNumber,
                       PaymentDate = ar.PaymentDate,
                       ClientPayment = ar.ClientPayment,
                       DownloadDate = ar.DownloadDate,
                       VehicleNumber = ar.VehicleNumber,
                       TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                       TransporterPayment = ar.TransporterPayment
                   };
                    arbeitenShowMainDataGridView.DataSource = queryTextAndDate.ToList();
                    arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                    arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                    arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                    arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                    arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;


                }
                else if (arbeitenShowOnlyActive.Checked != true && arbeitenShowSearchDatePicker.Checked == true && arbeitenShowSearchTextBox.Text == "") // 0 1 0
                {
                    var queryTextAndDate =
                   from ar in db.Arbeitens
                   where ((ar.Order.DownloadDateFrom.Value.Month == arbeitenShowSearchDatePicker.Value.Month) || (ar.Order.DownloadDateTo.Value.Month == arbeitenShowSearchDatePicker.Value.Month)) && ((ar.Order.DownloadDateFrom.Value.Year == arbeitenShowSearchDatePicker.Value.Year) || ar.Order.DownloadDateTo.Value.Year == arbeitenShowSearchDatePicker.Value.Year)
                   orderby ar.Id
                   select
                   new
                   {
                       Id = ar.Id,
                       OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                       ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                       ClientAcountNumber = ar.ClientAccountNumber,
                       PaymentDate = ar.PaymentDate,
                       ClientPayment = ar.ClientPayment,
                       DownloadDate = ar.DownloadDate,
                       VehicleNumber = ar.VehicleNumber,
                       TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                       TransporterPayment = ar.TransporterPayment
                   };
                    arbeitenShowMainDataGridView.DataSource = queryTextAndDate.ToList();
                    arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                    arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                    arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                    arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                    arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;


                }
                else if (arbeitenShowOnlyActive.Checked != true && arbeitenShowSearchDatePicker.Checked == true && arbeitenShowSearchTextBox.Text != "")// 0 1 1
                {
                    var queryTextAndDate =
                   from ar in db.Arbeitens
                   where (ar.Order.Client.Name.Contains(text) || ar.VehicleNumber.Contains(text) ||
                   ar.Order.OrderDownloadAddresses.Any(c => c.DownloadAddress.Country.Name.Contains(text) ||
                   c.DownloadAddress.CityCode.Contains(text))) ||
                   ar.Order.OrderUploadAdresses.Any(c => c.UploadAddress.Country.Name.Contains(text)
                   || c.UploadAddress.CityCode.Contains(text))

                   && ((ar.Order.DownloadDateFrom.Value.Month == arbeitenShowSearchDatePicker.Value.Month) ||
                   (ar.Order.DownloadDateTo.Value.Month == arbeitenShowSearchDatePicker.Value.Month))
                   && ((ar.Order.DownloadDateFrom.Value.Year == arbeitenShowSearchDatePicker.Value.Year) ||
                   ar.Order.DownloadDateTo.Value.Year == arbeitenShowSearchDatePicker.Value.Year)
                   orderby ar.Id
                   select
                   new
                   {
                       Id = ar.Id,
                       OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                       ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                       ClientAcountNumber = ar.ClientAccountNumber,
                       PaymentDate = ar.PaymentDate,
                       ClientPayment = ar.ClientPayment,
                       DownloadDate = ar.DownloadDate,
                       VehicleNumber = ar.VehicleNumber,
                       TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                       TransporterPayment = ar.TransporterPayment
                   };
                    arbeitenShowMainDataGridView.DataSource = queryTextAndDate.ToList();
                    arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                    arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                    arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                    arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                    arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;


                }
                else if (arbeitenShowOnlyActive.Checked == true && arbeitenShowSearchDatePicker.Checked != true && arbeitenShowSearchTextBox.Text == "") // 1 0 0
                {
                    var queryTextAndDate =
                   from ar in db.Arbeitens
                   where (ar.Order.State == true)
                   orderby ar.Id
                   select
                   new
                   {
                       Id = ar.Id,
                       OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                       ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                       ClientAcountNumber = ar.ClientAccountNumber,
                       PaymentDate = ar.PaymentDate,
                       ClientPayment = ar.ClientPayment,
                       DownloadDate = ar.DownloadDate,
                       VehicleNumber = ar.VehicleNumber,
                       TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                       TransporterPayment = ar.TransporterPayment
                   };
                    arbeitenShowMainDataGridView.DataSource = queryTextAndDate.ToList();
                    arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                    arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                    arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                    arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                    arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;


                }
                else if (arbeitenShowOnlyActive.Checked == true && arbeitenShowSearchDatePicker.Checked != true && arbeitenShowSearchTextBox.Text != "") // 1 0 1
                {
                    var queryTextAndDate =
                   from ar in db.Arbeitens
                   where (ar.Order.Client.Name.Contains(text) || ar.VehicleNumber.Contains(text) || ar.Order.OrderDownloadAddresses.Any(c => c.DownloadAddress.Country.Name.Contains(text) || c.DownloadAddress.CityCode.Contains(text))) || ar.Order.OrderUploadAdresses.Any(c => c.UploadAddress.Country.Name.Contains(text) || c.UploadAddress.CityCode.Contains(text))
                   && (ar.Order.State == true)
                   orderby ar.Id
                   select
                   new
                   {
                       Id = ar.Id,
                       OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                       ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                       ClientAcountNumber = ar.ClientAccountNumber,
                       PaymentDate = ar.PaymentDate,
                       ClientPayment = ar.ClientPayment,
                       DownloadDate = ar.DownloadDate,
                       VehicleNumber = ar.VehicleNumber,
                       TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                       TransporterPayment = ar.TransporterPayment
                   };
                    arbeitenShowMainDataGridView.DataSource = queryTextAndDate.ToList();
                    arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                    arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                    arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                    arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                    arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;
                }
                else if (arbeitenShowOnlyActive.Checked == true && arbeitenShowSearchDatePicker.Checked == true && arbeitenShowSearchTextBox.Text == "")// 1 1 0
                {
                    var queryTextAndDate =
                  from ar in db.Arbeitens
                  where (ar.Order.State == true && ((ar.Order.DownloadDateFrom.Value.Month == arbeitenShowSearchDatePicker.Value.Month) ||
                   (ar.Order.DownloadDateTo.Value.Month == arbeitenShowSearchDatePicker.Value.Month)) && ((ar.Order.DownloadDateFrom.Value.Year == arbeitenShowSearchDatePicker.Value.Year) || ar.Order.DownloadDateTo.Value.Year == arbeitenShowSearchDatePicker.Value.Year))
                  orderby ar.Id
                  select
                  new
                  {
                      Id = ar.Id,
                      OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                      ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                      ClientAcountNumber = ar.ClientAccountNumber,
                      PaymentDate = ar.PaymentDate,
                      ClientPayment = ar.ClientPayment,
                      DownloadDate = ar.DownloadDate,
                      VehicleNumber = ar.VehicleNumber,
                      TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                      TransporterPayment = ar.TransporterPayment
                  };
                    arbeitenShowMainDataGridView.DataSource = queryTextAndDate.ToList();
                    arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                    arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                    arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                    arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                    arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;


                }
                else if (arbeitenShowOnlyActive.Checked == true && arbeitenShowSearchDatePicker.Checked == true && arbeitenShowSearchTextBox.Text != "")// 1 1 1
                {
                    var queryTextAndDate =
                  from ar in db.Arbeitens
                  where (ar.Order.Client.Name.Contains(text) || ar.VehicleNumber.Contains(text) || ar.Order.OrderDownloadAddresses.Any(c => c.DownloadAddress.Country.Name.Contains(text) || c.DownloadAddress.CityCode.Contains(text))) || ar.Order.OrderUploadAdresses.Any(c => c.UploadAddress.Country.Name.Contains(text) || c.UploadAddress.CityCode.Contains(text))
                  && ((ar.Order.DownloadDateFrom.Value.Month == arbeitenShowSearchDatePicker.Value.Month) || (ar.Order.DownloadDateTo.Value.Month == arbeitenShowSearchDatePicker.Value.Month)) && ((ar.Order.DownloadDateFrom.Value.Year == arbeitenShowSearchDatePicker.Value.Year) ||
                        ar.Order.DownloadDateTo.Value.Year == arbeitenShowSearchDatePicker.Value.Year)
                  && ar.Order.State == true
                  orderby ar.Id
                  select
                  new
                  {
                      Id = ar.Id,
                      OrderNumber = (!ar.Order.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Ще_не_присвоєно : ar.Order.IndexNumber + "/" + ar.Order.Date.Value.Year,
                      ClientName = (ar.Order.Client == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Client.Name,
                      ClientAcountNumber = ar.ClientAccountNumber,
                      PaymentDate = ar.PaymentDate,
                      ClientPayment = ar.ClientPayment,
                      DownloadDate = ar.DownloadDate,
                      VehicleNumber = ar.VehicleNumber,
                      TransporterName = (ar.Order.Transporter == null) ? AtlantSovt.Properties.Resources.Не_вибрано : ar.Order.Transporter.FullName,
                      TransporterPayment = ar.TransporterPayment
                  };
                    arbeitenShowMainDataGridView.DataSource = queryTextAndDate.ToList();
                    arbeitenShowMainDataGridView.Columns[0].HeaderText = AtlantSovt.Properties.Resources.Порядковий_номер;
                    arbeitenShowMainDataGridView.Columns[1].HeaderText = AtlantSovt.Properties.Resources.Номер_заявки;
                    arbeitenShowMainDataGridView.Columns[2].HeaderText = AtlantSovt.Properties.Resources.Клієнт;
                    arbeitenShowMainDataGridView.Columns[3].HeaderText = AtlantSovt.Properties.Resources.Номер_рахунку_клієнта;
                    arbeitenShowMainDataGridView.Columns[4].HeaderText = AtlantSovt.Properties.Resources.Дата_оплати;
                    arbeitenShowMainDataGridView.Columns[5].HeaderText = AtlantSovt.Properties.Resources.Сума_від_клієнта;
                    arbeitenShowMainDataGridView.Columns[6].HeaderText = AtlantSovt.Properties.Resources.Дата_завантаження;
                    arbeitenShowMainDataGridView.Columns[7].HeaderText = AtlantSovt.Properties.Resources.Номер_авто;
                    arbeitenShowMainDataGridView.Columns[8].HeaderText = AtlantSovt.Properties.Resources.Перевізник;
                    arbeitenShowMainDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Сума_перевізнику;
                }
                arbeitenShowMainDataGridView.Update();
                arbeitenShowMainDataGridView.ClearSelection();
            }
        }
        public void AddArbeiten()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addArbeitenOrder != null)
                {
                    if (arbeitenAddAccountNumberLabelTextBox.Text != "" || arbeitenAddClientPaymentTextBox.Text != "" || arbeitenAddClientPaymentTextBox.Text != "" || arbeitenAddNoteTextBox.Text != ""
                    || arbeitenAddTransporterVehicleTextBox.Text != "" || arbeitenAddTransporterPaymentTextBox.Text != "" || arbeitenAddClientPaymentDateTimePicker.Checked || arbeitenAddDownloadDateTimePicker.Checked)
                    {
                        var new_AccountNumber = arbeitenAddAccountNumberLabelTextBox.Text;
                        var new_ClientPayment = arbeitenAddClientPaymentTextBox.Text;
                        var new_Note = arbeitenAddNoteTextBox.Text;
                        var new_TransporterVehicle = arbeitenAddTransporterVehicleTextBox.Text;
                        var new_TransporterPayment = arbeitenAddTransporterPaymentTextBox.Text;
                        var new_СlientPaymentDate = arbeitenAddClientPaymentDateTimePicker.Value;
                        var new_DownloadDate = arbeitenAddDownloadDateTimePicker.Value;

                        Arbeiten New_Arbeiten = new Arbeiten
                        {
                            Id = addArbeitenOrder.Id,
                            ClientAccountNumber = new_AccountNumber,
                            ClientPayment = new_ClientPayment,
                            Note = new_Note,
                            VehicleNumber = new_TransporterVehicle,
                            TransporterPayment = new_TransporterPayment,
                            PaymentDate = new_СlientPaymentDate,
                            DownloadDate = new_DownloadDate
                        };
                        try
                        {
                            db.Arbeitens.Add( New_Arbeiten);
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Запис_успішно_додано);
                        }
                        catch (Exception ec)
                        {
                            Log.Write(ec);
                            MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ec.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Заповніть_хоча_б_одне_поле);
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Спочатку_виберіть_заявку);
                }
            }
        }
        public void ClearAllArbeitenAdd()
        {
            arbeitenAddAccountNumberLabelTextBox.Clear();
            arbeitenAddClientPaymentTextBox.Clear();
            arbeitenAddNoteTextBox.Clear();
            arbeitenAddTransporterVehicleTextBox.Clear();
            arbeitenAddTransporterPaymentTextBox.Clear();
            arbeitenAddClientPaymentDateTimePicker.Value = DateTime.Now;
            arbeitenAddDownloadDateTimePicker.Value = DateTime.Now;
            arbeitenAddClientPaymentDateTimePicker.Checked = false;
            arbeitenAddDownloadDateTimePicker.Checked = false;
        }
        public void LoadArbeitenAddOrderSelectComboBox()
        {
            if (arbeitenAddOrderFilterCheckBox.CheckState == CheckState.Unchecked)
            {
                using (var db = new AtlantSovtContext())
                {
                    var query = from or in db.Orders
                                orderby or.Id
                                where (or.State != false) && (or.Date.Value.Month == arbeitenAddOrderFilterDateTimePicker.Value.Month)
                                && (or.Date.Value.Year == arbeitenAddOrderFilterDateTimePicker.Value.Year)
                                && or.Arbeiten == null
                                select or;
                    foreach (var item in query)
                    {
                        if (item.Client != null)
                        {
                            arbeitenAddOrderSelectComboBox.Items.Add(item.Client.Name + " ," + item.Date.Value.ToShortDateString() + " " + item.Date.Value.ToShortTimeString() + " [" + item.Id + "]");
                        }
                        else
                        {
                            arbeitenAddOrderSelectComboBox.Items.Add("" + " ," + item.Date.Value.ToShortDateString() + " " + item.Date.Value.ToShortTimeString() + " [" + item.Id + "]");
                        }
                    }
                }
            }
            else
            {
                using (var db = new AtlantSovtContext())
                {
                    var query = from or in db.Orders
                                orderby or.Id
                                where (or.Date.Value.Month == arbeitenAddOrderFilterDateTimePicker.Value.Month)
                                && (or.Date.Value.Year == arbeitenAddOrderFilterDateTimePicker.Value.Year)
                                && or.Arbeiten == null
                                select or;
                    foreach (var item in query)
                    {
                        if (item.Client != null)
                        {
                            arbeitenAddOrderSelectComboBox.Items.Add(item.Client.Name + " ," + item.Date.Value.ToShortDateString() + " " + item.Date.Value.ToShortTimeString() + " [" + item.Id + "]");
                        }
                        else
                        {
                            arbeitenAddOrderSelectComboBox.Items.Add(" ," + item.Date.Value.ToShortDateString() + " " + item.Date.Value.ToShortTimeString() + " [" + item.Id + "]");
                        }
                    }
                }

            }
        }
        public void SplitOrderArbeitenAdd()
        {
            using (var db = new AtlantSovtContext())
            {
                if (arbeitenAddOrderSelectComboBox.SelectedIndex != -1 && arbeitenAddOrderSelectComboBox.Text == arbeitenAddOrderSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = arbeitenAddOrderSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    addArbeitenOrder = db.Orders.Find(id);
                }
                else
                {
                    addArbeitenOrder = null;
                    ClearAllArbeitenAdd();
                }
            }
        }
        public void LoadArbeitenAddClientTextBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addArbeitenOrder != null && addArbeitenOrder.ClientId != 0)
                {

                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id == addArbeitenOrder.ClientId
                                select c;
                    arbeitenAddClientTextBox.Text = (query.FirstOrDefault().Name + " , " + query.FirstOrDefault().Director + " [" + query.FirstOrDefault().Id + "]");
                }
                else
                {
                    arbeitenAddClientTextBox.Clear();
                }
            }
        }
        public void LoadArbeitenAddTransporterTextBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addArbeitenOrder != null && addArbeitenOrder.TransporterId != 0)
                {

                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id == addArbeitenOrder.TransporterId
                                select c;
                    arbeitenAddTransporterTextBox.Text = (query.FirstOrDefault().FullName + " , " + query.FirstOrDefault().Director + " [" + query.FirstOrDefault().Id + "]");

                }
                else
                {
                    arbeitenAddTransporterTextBox.Clear();
                }
            }
        }
        public void UpdateArbeiten()
        {
            using (var db = new AtlantSovtContext())
            {
                if (updateArbeiten != null)
                {
                    try
                    {
                       bool IsModifiedArbeiten = false;

                        //date 1
                        if (arbeitenUpdateClientPaymentDateTimePicker.Checked)
                        {
                            if (updateArbeiten.PaymentDate != arbeitenUpdateClientPaymentDateTimePicker.Value)
                            {
                                updateArbeiten.PaymentDate = arbeitenUpdateClientPaymentDateTimePicker.Value;
                                IsModifiedArbeiten = true;
                            }
                        }
                        else if (updateArbeiten.PaymentDate != null)
                        {
                            updateArbeiten.PaymentDate = null;
                            IsModifiedArbeiten = true;
                        }
                        //date 2
                        if (arbeitenUpdateDownloadDateTimePicker.Checked)
                        {
                            if (updateArbeiten.DownloadDate != arbeitenUpdateDownloadDateTimePicker.Value)
                            {
                                updateArbeiten.DownloadDate = arbeitenUpdateDownloadDateTimePicker.Value;
                                IsModifiedArbeiten = true;
                            }
                        }
                        else if (updateArbeiten.DownloadDate != null)
                        {
                            updateArbeiten.DownloadDate = null;
                            IsModifiedArbeiten = true;
                        }
                        //text 1
                        if (arbeitenUpdateAccountNumberLabelTextBox.Text != "")
                        {
                            if (updateArbeiten.ClientAccountNumber != arbeitenUpdateAccountNumberLabelTextBox.Text)
                            {
                                updateArbeiten.ClientAccountNumber = arbeitenUpdateAccountNumberLabelTextBox.Text;
                                IsModifiedArbeiten = true;
                            }
                        }
                        else if (updateArbeiten.ClientAccountNumber != null)
                        {
                            updateArbeiten.ClientAccountNumber = null;
                            IsModifiedArbeiten = true;
                        }
                        //text 2
                        if (arbeitenUpdateClientPaymentTextBox.Text != "")
                        {
                            if (updateArbeiten.ClientPayment != arbeitenUpdateClientPaymentTextBox.Text)
                            {
                                updateArbeiten.ClientPayment = arbeitenUpdateClientPaymentTextBox.Text;
                                IsModifiedArbeiten = true;
                            }
                        }
                        else if (updateArbeiten.ClientPayment != null)
                        {
                            updateArbeiten.ClientPayment = null;
                            IsModifiedArbeiten = true;
                        }
                        //text 3
                        if (arbeitenUpdateNoteTextBox.Text != "")
                        {
                            if (updateArbeiten.Note != arbeitenUpdateNoteTextBox.Text)
                            {
                                updateArbeiten.Note = arbeitenUpdateNoteTextBox.Text;
                                IsModifiedArbeiten = true;
                            }
                        }
                        else if (updateArbeiten.Note != null)
                        {
                            updateArbeiten.Note = null;
                            IsModifiedArbeiten = true;
                        }
                        //text 4
                        if (arbeitenUpdateTransporterVehicleTextBox.Text != "")
                        {
                            if (updateArbeiten.VehicleNumber != arbeitenUpdateTransporterVehicleTextBox.Text)
                            {
                                updateArbeiten.VehicleNumber = arbeitenUpdateTransporterVehicleTextBox.Text;
                                IsModifiedArbeiten = true;
                            }
                        }
                        else if (updateArbeiten.VehicleNumber != null)
                        {
                            updateArbeiten.VehicleNumber = null;
                            IsModifiedArbeiten = true;
                        }
                        //text 5
                        if (arbeitenUpdateTransporterPaymentTextBox.Text != "")
                        {
                            if (updateArbeiten.TransporterPayment != arbeitenUpdateTransporterPaymentTextBox.Text)
                            {
                                updateArbeiten.TransporterPayment = arbeitenUpdateTransporterPaymentTextBox.Text;
                                IsModifiedArbeiten = true;
                            }
                        }
                        else if (updateArbeiten.TransporterPayment != null)
                        {
                            updateArbeiten.TransporterPayment = null;
                            IsModifiedArbeiten = true;
                        }
                        if (IsModifiedArbeiten)
                        {
                            db.Entry(updateArbeiten).State = EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show(AtlantSovt.Properties.Resources.Зміни_збережено);
                        }
                        else
                        {
                            MessageBox.Show(AtlantSovt.Properties.Resources.Змін_не_знайдено);
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        Log.Write(e);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_при_зміні_запису + e.Message);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_при_зміні_запису + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Спочатку_виберіть_запис);
                }
            }
        }
        public void ClearAllArbeitenUpdate()
        {
            arbeitenUpdateAccountNumberLabelTextBox.Clear();
            arbeitenUpdateClientPaymentTextBox.Clear();
            arbeitenUpdateNoteTextBox.Clear();
            arbeitenUpdateTransporterVehicleTextBox.Clear();
            arbeitenUpdateTransporterPaymentTextBox.Clear();
            arbeitenUpdateClientPaymentDateTimePicker.Value = DateTime.Now;
            arbeitenUpdateDownloadDateTimePicker.Value = DateTime.Now;
            arbeitenUpdateClientPaymentDateTimePicker.Checked = false;
            arbeitenUpdateDownloadDateTimePicker.Checked = false;
        }
        public void LoadAllFieldsArbeitenUpdate()
        {
            if (updateArbeiten != null)
            {
                if (updateArbeiten.ClientAccountNumber != null)
                {
                    arbeitenUpdateAccountNumberLabelTextBox.Text = updateArbeiten.ClientAccountNumber;
                }
                else
                {
                    arbeitenUpdateAccountNumberLabelTextBox.Clear();
                }

                if (updateArbeiten.ClientPayment != null)
                {
                    arbeitenUpdateClientPaymentTextBox.Text = updateArbeiten.ClientPayment;
                }
                else
                {
                    arbeitenUpdateClientPaymentTextBox.Clear();
                }

                if (updateArbeiten.Note != null)
                {
                    arbeitenUpdateNoteTextBox.Text = updateArbeiten.Note;
                }
                else
                {
                    arbeitenUpdateNoteTextBox.Clear();
                }

                if (updateArbeiten.VehicleNumber != null)
                {
                    arbeitenUpdateTransporterVehicleTextBox.Text = updateArbeiten.VehicleNumber;
                }
                else
                {
                    arbeitenUpdateTransporterVehicleTextBox.Clear();
                }

                if (updateArbeiten.TransporterPayment != null)
                {
                    arbeitenUpdateTransporterPaymentTextBox.Text = updateArbeiten.TransporterPayment;
                }
                else
                {
                    arbeitenUpdateTransporterPaymentTextBox.Clear();
                }

                if (updateArbeiten.PaymentDate != null)
                {
                    arbeitenUpdateClientPaymentDateTimePicker.Checked = true;
                    arbeitenUpdateClientPaymentDateTimePicker.Value = updateArbeiten.PaymentDate.Value;
                }
                else
                {
                    arbeitenUpdateClientPaymentDateTimePicker.Checked = false;
                }

                if (updateArbeiten.DownloadDate != null)
                {
                    arbeitenUpdateDownloadDateTimePicker.Checked = true;
                    arbeitenUpdateDownloadDateTimePicker.Value = updateArbeiten.DownloadDate.Value;
                }
                else
                {
                    arbeitenUpdateDownloadDateTimePicker.Checked = false;
                }
            }  
        }
        public void LoadArbeitenUpdateOrderSelectComboBox()
        {
            if (arbeitenUpdateOrderFilterCheckBox.CheckState == CheckState.Unchecked)
            {
                using (var db = new AtlantSovtContext())
                {
                    var query = from or in db.Orders
                                orderby or.Id
                                where (or.State != false) && (or.Date.Value.Month == arbeitenUpdateOrderFilterDateTimePicker.Value.Month)
                                && (or.Date.Value.Year == arbeitenUpdateOrderFilterDateTimePicker.Value.Year)
                                && or.Arbeiten != null
                                select or;
                    foreach (var item in query)
                    {
                        if (item.Client != null)
                        {
                            arbeitenUpdateOrderSelectComboBox.Items.Add(item.Client.Name + " ," + item.Date.Value.ToShortDateString() + " " + item.Date.Value.ToShortTimeString() + " [" + item.Id + "]");
                        }
                        else
                        {
                            arbeitenUpdateOrderSelectComboBox.Items.Add("" + " ," + item.Date.Value.ToShortDateString() + " " + item.Date.Value.ToShortTimeString() + " [" + item.Id + "]");
                        }
                    }
                }
            }
            else
            {
                using (var db = new AtlantSovtContext())
                {
                    var query = from or in db.Orders
                                orderby or.Id
                                where (or.Date.Value.Month == arbeitenUpdateOrderFilterDateTimePicker.Value.Month)
                                && (or.Date.Value.Year == arbeitenUpdateOrderFilterDateTimePicker.Value.Year)
                                && or.Arbeiten != null
                                select or;
                    foreach (var item in query)
                    {
                        if (item.Client != null)
                        {
                            arbeitenUpdateOrderSelectComboBox.Items.Add(item.Client.Name + " ," + item.Date.Value.ToShortDateString() + " " + item.Date.Value.ToShortTimeString() + " [" + item.Id + "]");
                        }
                        else
                        {
                            arbeitenUpdateOrderSelectComboBox.Items.Add(" ," + item.Date.Value.ToShortDateString() + " " + item.Date.Value.ToShortTimeString() + " [" + item.Id + "]");
                        }
                    }
                }

            }
        }
        public void SplitOrderArbeitenUpdate()
        {
            using (var db = new AtlantSovtContext())
            {
                if (arbeitenUpdateOrderSelectComboBox.SelectedIndex != -1 && arbeitenUpdateOrderSelectComboBox.Text == arbeitenUpdateOrderSelectComboBox.SelectedItem.ToString())
                {
                    string comboboxText = arbeitenUpdateOrderSelectComboBox.SelectedItem.ToString();
                    string[] selectedText = comboboxText.Split(new char[] { '[', ']' });
                    string comboBoxSelectedId = selectedText[1];
                    long id = Convert.ToInt64(comboBoxSelectedId);
                    updateArbeitenOrder = db.Orders.Find(id);
                    updateArbeiten = updateArbeitenOrder.Arbeiten;
                }
                else
                {
                    updateArbeitenOrder = null;
                    updateArbeiten = null;
                    ClearAllArbeitenUpdate();
                }
            }
        }
        public void LoadArbeitenUpdateClientTextBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (updateArbeitenOrder != null && updateArbeitenOrder.ClientId != 0)
                {

                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id == updateArbeitenOrder.ClientId
                                select c;
                    arbeitenUpdateClientTextBox.Text = (query.FirstOrDefault().Name + " , " + query.FirstOrDefault().Director + " [" + query.FirstOrDefault().Id + "]");
                }
                else
                {
                    arbeitenUpdateClientTextBox.Clear();
                }
            }
        }
        public void LoadArbeitenUpdateTransporterTextBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (updateArbeitenOrder != null && updateArbeitenOrder.TransporterId != 0)
                {

                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id == updateArbeitenOrder.TransporterId
                                select c;
                    arbeitenUpdateTransporterTextBox.Text = (query.FirstOrDefault().FullName + " , " + query.FirstOrDefault().Director + " [" + query.FirstOrDefault().Id + "]");

                }
                else
                {
                    arbeitenUpdateTransporterTextBox.Clear();
                }
            }
        }
    }
}

