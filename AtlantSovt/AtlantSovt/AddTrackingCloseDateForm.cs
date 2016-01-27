using AtlantSovt.Additions;
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

namespace AtlantSovt
{
    public partial class AddTrackingCloseDateForm : Form
    {

        public AddTrackingCloseDateForm(MainForm form)
        {
            InitializeComponent();
            MainForm = form;
        }

        private MainForm MainForm { get; set; }

        private long id;
        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public void AddCloseDate(long id)
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    Order order;
                    order = db.Orders.Find(id);
                    order.State = false;
                    order.CloseDate = closeDateCalendar.SelectionStart;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Виникла_помилка_спробуйте_ще_раз);
                }
            }
        }

        private void addCloseDateButton_Click(object sender, EventArgs e)
        {
            AddCloseDate(id);
            MainForm.trackingShowTransporterContactsDataGridView.Update();
            MainForm.trackingShowForwardersDataGridView.DataSource = null;
            MainForm.trackingShowTransporterContactsDataGridView.DataSource = null;
            MainForm.trackingShowCommentDataGridView.DataSource = null;
            MainForm.trackingShowUploadAddressDataGridView.DataSource = null;
            MainForm.trackingShowDownloadAddressDataGridView.DataSource = null;
            MainForm.trackingShowAddNoteRichTextBox.Clear();
            MainForm.ShowTrackingSearch(MainForm.trackingShowDataGridView, MainForm.trackingShowSearchTextBox, MainForm.showTrackingDateTimePicker, MainForm.showTrackingOnlyActive);
            this.Close();
        }

        private void AddTrackingCloseDateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.closeDateForm = null;
            this.Dispose();
        }
    }
}
