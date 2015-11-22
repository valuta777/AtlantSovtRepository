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
            this.updateTrackingCommentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trackingCommentRichTextBox
            // 
            this.trackingCommentRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackingCommentRichTextBox.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackingCommentRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.trackingCommentRichTextBox.Name = "trackingCommentRichTextBox";
            this.trackingCommentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.trackingCommentRichTextBox.Size = new System.Drawing.Size(515, 274);
            this.trackingCommentRichTextBox.TabIndex = 0;
            this.trackingCommentRichTextBox.TabStop = false;
            this.trackingCommentRichTextBox.Text = "";
            // 
            // updateTrackingCommentButton
            // 
            this.updateTrackingCommentButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.updateTrackingCommentButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updateTrackingCommentButton.Location = new System.Drawing.Point(0, 273);
            this.updateTrackingCommentButton.Name = "updateTrackingCommentButton";
            this.updateTrackingCommentButton.Size = new System.Drawing.Size(517, 59);
            this.updateTrackingCommentButton.TabIndex = 94;
            this.updateTrackingCommentButton.Text = "Змінити коментар";
            this.updateTrackingCommentButton.UseVisualStyleBackColor = true;
            this.updateTrackingCommentButton.Click += new System.EventHandler(this.updateTrackingCommentButton_Click);
            // 
            // ShowTrackingCommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 332);
            this.Controls.Add(this.updateTrackingCommentButton);
            this.Controls.Add(this.trackingCommentRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowTrackingCommentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Коментар";
            this.Load += new System.EventHandler(this.ShowTrackingCommentForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox trackingCommentRichTextBox;
        private System.Windows.Forms.Button updateTrackingCommentButton;
    }
}