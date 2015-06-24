namespace AtlantSovt
{
    partial class ShowTrackingCommentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackingCommentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // trackingCommentRichTextBox
            // 
            this.trackingCommentRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackingCommentRichTextBox.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackingCommentRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.trackingCommentRichTextBox.Name = "trackingCommentRichTextBox";
            this.trackingCommentRichTextBox.ReadOnly = true;
            this.trackingCommentRichTextBox.Size = new System.Drawing.Size(500, 300);
            this.trackingCommentRichTextBox.TabIndex = 0;
            this.trackingCommentRichTextBox.TabStop = false;
            this.trackingCommentRichTextBox.Text = "";
            // 
            // ShowTrackingCommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.trackingCommentRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowTrackingCommentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.ShowTrackingCommentForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox trackingCommentRichTextBox;
    }
}