using AtlantSovt.Additions;
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
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;

namespace AtlantSovt
{
    public partial class ExportTrackingToExcelForm : Form
    {
        public ExportTrackingToExcelForm(MainForm form)
        {
            InitializeComponent();
            MainForm = form;

            exportTrackingShowDataGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            exportTrackingShowDataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F);

            MainForm.ShowTracking(exportTrackingShowDataGridView);
            AddDataGridViewCheckBox();
            //killExcel();

        }

        void AddDataGridViewCheckBox()
        {
            DataGridViewCheckBoxColumn checkTracking = new DataGridViewCheckBoxColumn();
            checkTracking.Name = "checkTracking";
            checkTracking.HeaderText = AtlantSovt.Properties.Resources.Мітка;
            exportTrackingShowDataGridView.Columns.Add(checkTracking);
            exportTrackingShowDataGridView.Columns[9].HeaderText = AtlantSovt.Properties.Resources.Мітка;
        }

        private MainForm MainForm { get; set; }

        void CheckAllTracking(bool state)
        {
            foreach (DataGridViewRow row in exportTrackingShowDataGridView.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["checkTracking"];
                chk.Value = state;
            }
            exportTrackingShowDataGridView.EndEdit();
        }

        List<int> GetCheckedTracking()
        {
            List<int> orderIdList = new List<int>();
            foreach(DataGridViewRow row in exportTrackingShowDataGridView.Rows)
            {
                if ((bool?)row.Cells["checkTracking"].Value != null )
                {
                    if((bool)row.Cells["checkTracking"].Value == true)
                    orderIdList.Add(Convert.ToInt32(row.Cells["Id"].Value));
                }
            }
            return orderIdList;
        }

        int FieldCounter(List<int> idList)
        {
            int count = 0;
            using(var db = new AtlantSovtContext())
            {
                foreach (int id in idList)
                {
                    var query = from o in db.Orders
                                where o.Id == id
                                select new
                                {
                                    uploadAddressCount = (o.OrderUploadAdresses.Count != 0) ? (int)o.OrderUploadAdresses.Count : 0,
                                    downdloadAddressCount = (o.OrderDownloadAddresses.Count != 0) ? (int)o.OrderDownloadAddresses.Count : 0,
                                    transporterContact = (o.Transporter != null) ? (o.Transporter.TransporterContacts.Count != 0) ? (int)o.Transporter.TransporterContacts.Count : 0 : 0,
                                    comment = (o.TrackingComments.Count != 0) ? o.TrackingComments.Count : 0,
                                    note = (o.Note != null) ? (int)(o.Note.Length / 100) : 0,
                                };

                    count += (query.FirstOrDefault().uploadAddressCount > query.FirstOrDefault().downdloadAddressCount) ? query.FirstOrDefault().uploadAddressCount : query.FirstOrDefault().downdloadAddressCount;
                    count += query.FirstOrDefault().transporterContact + query.FirstOrDefault().comment + query.FirstOrDefault().note;
                    count += 20; // reserved fields
                }
            }

            return count;
        }

        void ExportTracking()
        {
            Order orderTracking;
            List<int> orderIdList = GetCheckedTracking();
            int daCounter = 0;
            int uaCounter = 0;
            int tcCounter = 0;
            int transConCounter = 0;
            int resultCounter = 0;
            int startBorder = 1;
            string note = "";
            Microsoft.Office.Interop.Excel.XlRgbColor cellsColor = Microsoft.Office.Interop.Excel.XlRgbColor.rgbOrange;
            Microsoft.Office.Interop.Excel.XlRgbColor subcellsColor = Microsoft.Office.Interop.Excel.XlRgbColor.rgbYellow;

            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

            //Book
            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
            //Table
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
            ObjWorkSheet.Columns.Font.Name = "Arial";
            ObjWorkSheet.Columns.Font.Size = 10;
            try
            {
                if (orderIdList.Count > 0)
                {
                    using (var db = new AtlantSovtContext())
                    {
                        foreach (int id in orderIdList)
                        {
                            //Cell [y - row, x - column]
                            orderTracking = db.Orders.Find(id);

                            resultCounter++;
                            ObjWorkSheet.Cells[resultCounter, 1] = AtlantSovt.Properties.Resources.Номер_заявки;
                            ObjWorkSheet.Cells[resultCounter, 2] = AtlantSovt.Properties.Resources.Працівник;
                            ObjWorkSheet.Cells[resultCounter, 3] = AtlantSovt.Properties.Resources.Клієнт;
                            ObjWorkSheet.Cells[resultCounter, 4] = AtlantSovt.Properties.Resources.Перевізник;
                            ObjWorkSheet.Cells[resultCounter, 5] = AtlantSovt.Properties.Resources.Дата_завантаження;
                            ObjWorkSheet.Cells[resultCounter, 6] = AtlantSovt.Properties.Resources.Стан;
                            ObjWorkSheet.Cells[resultCounter, 7] = AtlantSovt.Properties.Resources.Дата_закриття;
                            ObjWorkSheet.Range[ObjWorkSheet.Cells[resultCounter, 1], ObjWorkSheet.Cells[resultCounter, 7]].Interior.Color = cellsColor;

                            resultCounter++;
                            ObjWorkSheet.Cells[resultCounter, 1] = (!orderTracking.IndexNumber.HasValue) ? AtlantSovt.Properties.Resources.Не_визначено: orderTracking.IndexNumber + @"\" + orderTracking.Date.Value.Year;
                            ObjWorkSheet.Cells[resultCounter, 2] = (orderTracking.Staff == null || orderTracking.Staff.Type == "") ? "" : orderTracking.Staff.Type;
                            ObjWorkSheet.Cells[resultCounter, 3] = (orderTracking.Client == null || orderTracking.Client.Name == "") ? "" : orderTracking.Client.Name;
                            ObjWorkSheet.Cells[resultCounter, 4] = (orderTracking.Transporter == null || orderTracking.Transporter.FullName == "") ? "" : orderTracking.Transporter.FullName;
                            if (orderTracking.DownloadDateTo != null && orderTracking.DownloadDateFrom != null)
                            {
                                ObjWorkSheet.Cells[resultCounter, 5] = (orderTracking.DownloadDateFrom.Value.Month != orderTracking.DownloadDateTo.Value.Month) ? orderTracking.DownloadDateFrom.Value.Day + "." + orderTracking.DownloadDateFrom.Value.Month + " - " + orderTracking.DownloadDateTo.Value.Day + "." + orderTracking.DownloadDateTo.Value.Month + "." + orderTracking.DownloadDateTo.Value.Year : orderTracking.DownloadDateFrom.Value.Day + " - " + orderTracking.DownloadDateTo.Value.Day + "." + orderTracking.DownloadDateTo.Value.Month + "." + orderTracking.DownloadDateTo.Value.Year;
                            }
                            else if (orderTracking.DownloadDateFrom != null)
                            {
                                ObjWorkSheet.Cells[resultCounter, 5] = (orderTracking.DownloadDateFrom.Value.Day + "." + orderTracking.DownloadDateFrom.Value.Month + "." + orderTracking.DownloadDateFrom.Value.Year);
                            }
                            else
                            {
                                ObjWorkSheet.Cells[resultCounter, 5] = AtlantSovt.Properties.Resources.Не_визначено;
                            }
                            ObjWorkSheet.Cells[resultCounter, 6] = (orderTracking.State == null) ? AtlantSovt.Properties.Resources.Не_створена : ((orderTracking.State == false) ? AtlantSovt.Properties.Resources.Закрита : AtlantSovt.Properties.Resources.Відкрита);
                            ObjWorkSheet.Cells[resultCounter, 7] = (orderTracking.CloseDate == null) ? AtlantSovt.Properties.Resources.Не_визначено : orderTracking.CloseDate.Value.Day + "." + orderTracking.CloseDate.Value.Month + "." + orderTracking.CloseDate.Value.Year;

                            resultCounter += 2;
                            ObjWorkSheet.Cells[resultCounter, 1] = AtlantSovt.Properties.Resources.Експедитори;
                            ObjWorkSheet.Cells[resultCounter, 1].Interior.Color = cellsColor;

                            resultCounter++;
                            ObjWorkSheet.Cells[resultCounter, 1] = AtlantSovt.Properties.Resources.Експедитор_1;
                            ObjWorkSheet.Cells[resultCounter, 2] = AtlantSovt.Properties.Resources.Експедитор_2;
                            ObjWorkSheet.Cells[resultCounter, 3] = AtlantSovt.Properties.Resources.Експедитор_3;
                            ObjWorkSheet.Range[ObjWorkSheet.Cells[resultCounter, 1], ObjWorkSheet.Cells[resultCounter, 3]].Interior.Color = subcellsColor;

                            resultCounter++;
                            if (orderTracking.ForwarderOrders.Where(f => f.IsFirst == 1).Count() != 0)
                            {
                                ObjWorkSheet.Cells[resultCounter, 1] = orderTracking.ForwarderOrders.Where(F => F.IsFirst == 1).FirstOrDefault().Forwarder.Name;
                            }
                            if(orderTracking.ForwarderOrders.Where(f=>f.IsFirst == 2).Count() != 0)
                            {
                                ObjWorkSheet.Cells[resultCounter, 2] = orderTracking.ForwarderOrders.Where(F => F.IsFirst == 2).FirstOrDefault().Forwarder.Name;
                            }
                            if (orderTracking.ForwarderOrders.Where(f => f.IsFirst == 3).Count() != 0)
                            {
                                ObjWorkSheet.Cells[resultCounter, 3] = orderTracking.ForwarderOrders.Where(F => F.IsFirst == 3).FirstOrDefault().Forwarder.Name;
                            }

                            resultCounter += 2;
                            ObjWorkSheet.Cells[resultCounter, 1] = AtlantSovt.Properties.Resources.Напрямок;
                            ObjWorkSheet.Cells[resultCounter, 1].Interior.Color = cellsColor;

                            resultCounter++;
                            ObjWorkSheet.Cells[resultCounter, 1] = AtlantSovt.Properties.Resources.Країна;
                            ObjWorkSheet.Cells[resultCounter, 2] = AtlantSovt.Properties.Resources.Код_міста;
                            ObjWorkSheet.Cells[resultCounter, 3] = AtlantSovt.Properties.Resources.Місто;
                            ObjWorkSheet.Cells[resultCounter, 5] = AtlantSovt.Properties.Resources.Країна;
                            ObjWorkSheet.Cells[resultCounter, 6] = AtlantSovt.Properties.Resources.Код_міста;
                            ObjWorkSheet.Cells[resultCounter, 7] = AtlantSovt.Properties.Resources.Місто;

                            ObjWorkSheet.Range[ObjWorkSheet.Cells[resultCounter, 1], ObjWorkSheet.Cells[resultCounter, 7]].Interior.Color = subcellsColor;

                            resultCounter++;
                            if(orderTracking.OrderDownloadAddresses.ToList().Count != 0)
                            {
                                foreach(DownloadAddress da in orderTracking.OrderDownloadAddresses.Select(da => da.DownloadAddress).ToList())
                                {
                                    ObjWorkSheet.Cells[daCounter + resultCounter, 1] = (da.Country != null) ? da.Country.Name : "";
                                    ObjWorkSheet.Cells[daCounter + resultCounter, 2] = (da.CityCode != "") ? da.CityCode : "";
                                    ObjWorkSheet.Cells[daCounter + resultCounter, 3] = (da.CityName != "") ? da.CityName : "";

                                    daCounter++;
                                }
                            }

                            if (orderTracking.OrderUploadAdresses.ToList().Count != 0)
                            { 
                                foreach (UploadAddress ua in orderTracking.OrderUploadAdresses.Select(ua => ua.UploadAddress).ToList())
                                {
                                    ObjWorkSheet.Cells[uaCounter + resultCounter, 5] = (ua.Country != null) ? ua.Country.Name : "";
                                    ObjWorkSheet.Cells[uaCounter + resultCounter, 6] = (ua.CityCode != "") ? ua.CityCode : "";
                                    ObjWorkSheet.Cells[uaCounter + resultCounter, 7] = (ua.CityName != "") ? ua.CityName : "";

                                    uaCounter++;
                                }
                            }

                            resultCounter += (daCounter > uaCounter) ? daCounter : uaCounter;
                            resultCounter++;
                            ObjWorkSheet.Cells[resultCounter, 1] = AtlantSovt.Properties.Resources.Контактні_дані_перевізника;
                            ObjWorkSheet.Range[ObjWorkSheet.Cells[resultCounter, 1], ObjWorkSheet.Cells[resultCounter, 2]].Merge();
                            ObjWorkSheet.Range[ObjWorkSheet.Cells[resultCounter, 1], ObjWorkSheet.Cells[resultCounter, 2]].Interior.Color = cellsColor;


                            if(orderTracking.Transporter != null)
                            {
                                if(orderTracking.Transporter.TransporterContacts.ToList().Count !=0)
                                {
                                    resultCounter++;
                                    foreach(TransporterContact tc in orderTracking.Transporter.TransporterContacts.ToList())
                                    {
                                        ObjWorkSheet.Cells[resultCounter + transConCounter, 1] = tc.ContactPerson;
                                        ObjWorkSheet.Cells[resultCounter + transConCounter, 2] = tc.TelephoneNumber;
                                        ObjWorkSheet.Cells[resultCounter + transConCounter, 3] = tc.Email;
                                        ObjWorkSheet.Cells[resultCounter + transConCounter, 4] = tc.FaxNumber;
                                        transConCounter++;
                                    }
                                }
                            }

                            resultCounter += transConCounter;
                            resultCounter++;
                            ObjWorkSheet.Cells[resultCounter, 1] = AtlantSovt.Properties.Resources.Коментар;
                            ObjWorkSheet.Cells[resultCounter, 1].Interior.Color = cellsColor;

                            if (orderTracking.TrackingComments.ToList().Count != 0)
                            {
                                resultCounter++;
                                foreach (TrackingComment tc in orderTracking.TrackingComments.ToList())
                                {
                                    string comment = (tc.Comment != "") ? tc.Comment : "";

                                    string temp = "";
                                    int newLineLength = 60;
                                    int commentLineCounter = 0;
                                    foreach (string c in comment.Split('\n'))
                                    {
                                        if (c.Length > newLineLength)
                                        {
                                            for (int i = 1; i < c.Length + 1; i++)
                                            {
                                                if (i % newLineLength == 0)
                                                {
                                                    if (c.Length - i + newLineLength > newLineLength)
                                                    {
                                                        temp += c.Substring(i - newLineLength, newLineLength).Insert(newLineLength, "\n");
                                                        commentLineCounter++;
                                                    }
                                                }
                                            }
                                            int remainder = 0;
                                            remainder = c.Length % newLineLength;
                                            if (remainder != 0)
                                            {
                                                temp += c.Substring(c.Length - remainder, remainder).Insert(remainder, "\n");
                                                commentLineCounter++;
                                            }
                                        }
                                        else
                                        {
                                            temp += c + "\n";
                                            commentLineCounter++;
                                        }
                                    }

                                    ObjWorkSheet.Range[ObjWorkSheet.Cells[resultCounter + tcCounter, 1], ObjWorkSheet.Cells[resultCounter + tcCounter + commentLineCounter, 5]].Merge();
                                    ObjWorkSheet.Cells[resultCounter + tcCounter, 1] = temp;
                                    ObjWorkSheet.Range[ObjWorkSheet.Cells[resultCounter + tcCounter, 6], ObjWorkSheet.Cells[resultCounter + tcCounter, 7]].Merge();
                                    ObjWorkSheet.Cells[resultCounter + tcCounter, 6] = (tc.CreateDate != null) ? tc.CreateDate.ToString() : "";
                                    tcCounter += commentLineCounter + 2;
                                }
                            }

                            resultCounter += tcCounter;
                            resultCounter++;
                            ObjWorkSheet.Cells[resultCounter, 1] = AtlantSovt.Properties.Resources.Примітка;
                            ObjWorkSheet.Cells[resultCounter, 1].Interior.Color = cellsColor;

                            resultCounter++;
                            if (orderTracking.Note != null && orderTracking.Note != "")
                            {
                                note = orderTracking.Note;
                                string temp = "";
                                int newLineLength = 80;
                                int noteLineCounter = 0;
                                foreach(string n in note.Split('\n'))
                                {
                                    if (n.Length > newLineLength)
                                    {
                                        for (int i = 1; i < n.Length + 1; i++)
                                        {
                                            if (i % newLineLength == 0)
                                            {
                                                if (n.Length - i + newLineLength > newLineLength )
                                                {
                                                    temp += n.Substring(i - newLineLength, newLineLength).Insert(newLineLength, "\n");
                                                    noteLineCounter++;
                                                }
                                            }
                                        }
                                        int remainder = 0;
                                        remainder = n.Length % newLineLength;
                                        if(remainder != 0)
                                        {
                                            temp += n.Substring(n.Length - remainder, remainder).Insert(remainder, "\n");
                                            noteLineCounter++;
                                        }

                                    }
                                    else
                                    {
                                        temp += n + "\n";
                                        noteLineCounter++;
                                    }
                                }

                                ObjWorkSheet.Range[ObjWorkSheet.Cells[resultCounter + noteLineCounter, 1], ObjWorkSheet.Cells[resultCounter, 7]].Merge();
                                ObjWorkSheet.Cells[resultCounter, 1] = temp;
                                resultCounter += noteLineCounter + 2;
                                noteLineCounter = 0;
                            }
                            else
                            {
                                resultCounter += 2;
                            }
                            daCounter = 0;
                            uaCounter = 0;
                            tcCounter = 0;
                            transConCounter = 0;
                            ObjWorkSheet.Range[ObjWorkSheet.Cells[startBorder, 1], ObjWorkSheet.Cells[resultCounter - 2, 7]].BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick);
                            startBorder = resultCounter + 1;
                        }
                    }
                    ObjWorkSheet.Columns.AutoFit();

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        ObjWorkBook.SaveAs(folderBrowserDialog.SelectedPath + @"\" + DateTime.Now.ToString("dd.MM.yyy-HH.mm.ss") + ".xlsx");
                        //Show Excel
                        ObjExcel.Visible = true;
                        ObjExcel.UserControl = true;
                        GC.Collect();
                    }
                    else
                    {
                        ObjWorkBook.Close(false);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjWorkBook);
                        ObjWorkBook = null;
                        ObjExcel.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjExcel);
                        ObjExcel = null;
                        GC.Collect();
                    }
                }
                else
                {
                    MessageBox.Show(AtlantSovt.Properties.Resources.Не_вибрано_жодного_відстеження);
                    ObjExcel.Quit();
                    return;
                }
            }
            catch(Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                ObjWorkBook.Close(false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjWorkBook);
                ObjWorkBook = null;
                ObjExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjExcel);
                ObjExcel = null;
                GC.Collect();
            }
        }

        private void killExcel()
        {
            System.Diagnostics.Process[] PROC = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process PK in PROC)
            {
                if (PK.MainWindowTitle.Length == 0)
                {
                    PK.Kill();
                }
            }
        }

        private void trackingExportSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (trackingExportSearchTextBox.Text == "")
            {
                exportTrackingShowDataGridView.Visible = false;
                MainForm.ShowTracking(exportTrackingShowDataGridView);
                AddDataGridViewCheckBox();
                exportTrackingShowDataGridView.Visible = true;
                if (trackingExportSelectAllCheckBox.Checked == true)
                {
                    CheckAllTracking(true);
                }
                else
                {
                    CheckAllTracking(false);
                }
            }
        }

        private void trackingExportSearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                exportTrackingShowDataGridView.Visible = false;
                MainForm.ShowTrackingSearch(exportTrackingShowDataGridView, trackingExportSearchTextBox, trackingExportDateTimePicker, trackingExportOnlyActive);
                AddDataGridViewCheckBox();
                exportTrackingShowDataGridView.Visible = true;
                if (trackingExportSelectAllCheckBox.Checked == true)
                {
                    CheckAllTracking(true);
                }
                else
                {
                    CheckAllTracking(false);
                }
            }
        }

        private void trackingExportDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            exportTrackingShowDataGridView.Visible = false;
            MainForm.ShowTrackingSearch(exportTrackingShowDataGridView, trackingExportSearchTextBox, trackingExportDateTimePicker, trackingExportOnlyActive);
            AddDataGridViewCheckBox();
            exportTrackingShowDataGridView.Visible = true;
            if (trackingExportSelectAllCheckBox.Checked == true)
            {
                CheckAllTracking(true);
            }
            else
            {
                CheckAllTracking(false);
            }
        }

        private void trackingExportOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            exportTrackingShowDataGridView.Visible = false;
            MainForm.ShowTrackingSearch(exportTrackingShowDataGridView, trackingExportSearchTextBox, trackingExportDateTimePicker, trackingExportOnlyActive);
            AddDataGridViewCheckBox();
            exportTrackingShowDataGridView.Visible = true;
            if (trackingExportSelectAllCheckBox.Checked == true)
            {
                CheckAllTracking(true);
            }
            else
            {
                CheckAllTracking(false);
            }
        }

        private void trackingExportSearchButton_Click(object sender, EventArgs e)
        {
            exportTrackingShowDataGridView.Visible = false;
            MainForm.ShowTrackingSearch(exportTrackingShowDataGridView, trackingExportSearchTextBox, trackingExportDateTimePicker, trackingExportOnlyActive);
            AddDataGridViewCheckBox();
            exportTrackingShowDataGridView.Visible = true;
            if (trackingExportSelectAllCheckBox.Checked == true)
            {
                CheckAllTracking(true);
            }
            else
            {
                CheckAllTracking(false);
            }
        }

        private void trackingExportSelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(trackingExportSelectAllCheckBox.Checked == true)
            {
                CheckAllTracking(true);
            }
            else
            {
                CheckAllTracking(false);
            }
        }

        private void exportTrackingButton_Click(object sender, EventArgs e)
        {
            ExportTracking();
        }

        private void ExportTrackingToExcelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.exportToExcel = null;
            this.Dispose();
        }
    }
}
