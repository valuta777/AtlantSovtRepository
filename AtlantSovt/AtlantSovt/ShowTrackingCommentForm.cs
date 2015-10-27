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

namespace AtlantSovt
{
    public partial class ShowTrackingCommentForm : Form
    {
        public ShowTrackingCommentForm(MainForm form)
        {
            MainForm = form;
            InitializeComponent();
        }

        TrackingComment trackingComment;
        Order orderComment;

        private MainForm MainForm { get; set; }

        private long id;

        private DateTime date;

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

        public DateTime Date
        {
            get 
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        long? GetCommentId(long id, DateTime dateTime)
        {
            long commentId = 0;
            List<TrackingComment> comments = new List<TrackingComment>();

            try
            {
                using (var db = new AtlantSovtContext())
                {
                    orderComment = db.Orders.Find(id);
                    comments = orderComment.TrackingComments.ToList();
                    foreach (TrackingComment item in comments)
                    {
                        if (item.CreateDate == dateTime)
                        {
                            commentId = item.Id;
                            break;
                        }
                    }
                    return commentId;
                }
            }
            catch(Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        void ShowTrackingUpdateComment(long? id)
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    trackingComment = db.TrackingComments.Find(id);
                    trackingComment.Comment = trackingCommentRichTextBox.Text;
                    trackingComment.LastChangeDate = DateTime.Now;
                    db.Entry(trackingComment).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void LoadComment(long? id)
        {
            using (var db = new AtlantSovtContext())
            {
                var query =
                     from c in db.TrackingComments
                     where c.Id == id
                     select c.Comment;
                trackingCommentRichTextBox.Text = query.FirstOrDefault();
            }
        }

        private void ShowTrackingCommentForm_Load(object sender, EventArgs e)
        {
            trackingCommentRichTextBox.Refresh();
            LoadComment(GetCommentId(id, date));
        }

        private void updateTrackingCommentButton_Click(object sender, EventArgs e)
        {
            ShowTrackingUpdateComment(GetCommentId(id, date));
            MainForm.ShowTrackingInfo();
            this.Close();
        }
    }
}
