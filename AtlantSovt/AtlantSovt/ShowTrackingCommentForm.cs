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
        public ShowTrackingCommentForm()
        {
            InitializeComponent();
        }

        private string _comment;

        public string comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }

        private void ShowTrackingCommentForm_Load(object sender, EventArgs e)
        {
            trackingCommentRichTextBox.Refresh();
            trackingCommentRichTextBox.Text = _comment;
        }
    }
}
