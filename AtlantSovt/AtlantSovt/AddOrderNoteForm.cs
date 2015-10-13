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
    public partial class AddOrderNoteForm : Form
    {
        public AddOrderNoteForm(MainForm form)
        {
            MainForm = form;
            InitializeComponent();
        }

        Order orderNote;

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

        void LoadNote(long id)
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                     from o in db.Orders
                     where o.Id == id
                     select o.Note;
                addOrderNoteTextBox.Text = query.FirstOrDefault();
            }
        }

        void UpdateNote(long id)
        {
            using (var db = new AtlantSovtContext())
            {
                orderNote = db.Orders.Find(id);
                try
                {
                    orderNote.Note = addOrderNoteTextBox.Text;
                    db.Entry(orderNote).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Помилка: " + e.Message);
                }
            }
        }

        private void addOrderNoteButton_Click(object sender, EventArgs e)
        {
            UpdateNote(id);
            MainForm.ShowTrackingInfo();
            this.Close();
        }

        private void AddOrderNoteForm_Shown(object sender, EventArgs e)
        {
            LoadNote(id);
        }
    }
}
