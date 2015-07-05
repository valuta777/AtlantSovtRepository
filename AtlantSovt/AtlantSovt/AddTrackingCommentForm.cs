﻿using AtlantSovt.AtlantSovtDb;
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
    public partial class AddTrackingCommentForm : Form
    {
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

        private MainForm MainForm { get; set; }

        public AddTrackingCommentForm(MainForm form)
        {
            InitializeComponent();
            MainForm = form;
        }

        void ShowTrackingAddComment(long id)
        {
            using (var db = new AtlantSovtContext())
            {
                var New_TrackingComment = new TrackingComment
                {
                    OrderId = id,
                    Comment = addTrackingCommentTextBox.Text,
                    CreateDate = DateTime.Now,
                    LastChangeDate = DateTime.Now
                };
                try
                {
                    db.TrackingComments.Add(New_TrackingComment);
                    db.SaveChanges();
                    MessageBox.Show("Коментар успішно доданий заявці " + New_TrackingComment.OrderId);
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message);
                }

                
            }
        }

        private void addCommentButton_Click(object sender, EventArgs e)
        {
            ShowTrackingAddComment(id);
            MainForm.ShowTrackingInfo();
            this.Close();
        }

    }
}
