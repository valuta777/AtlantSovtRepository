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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowTrackingCommentForm));
            this.trackingCommentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.updateTrackingCommentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trackingCommentRichTextBox
            // 
            resources.ApplyResources(this.trackingCommentRichTextBox, "trackingCommentRichTextBox");
            this.trackingCommentRichTextBox.Name = "trackingCommentRichTextBox";
            this.trackingCommentRichTextBox.TabStop = false;
            // 
            // updateTrackingCommentButton
            // 
            resources.ApplyResources(this.updateTrackingCommentButton, "updateTrackingCommentButton");
            this.updateTrackingCommentButton.Name = "updateTrackingCommentButton";
            this.updateTrackingCommentButton.UseVisualStyleBackColor = true;
            this.updateTrackingCommentButton.Click += new System.EventHandler(this.updateTrackingCommentButton_Click);
            // 
            // ShowTrackingCommentForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.updateTrackingCommentButton);
            this.Controls.Add(this.trackingCommentRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowTrackingCommentForm";
            this.Load += new System.EventHandler(this.ShowTrackingCommentForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox trackingCommentRichTextBox;
        private System.Windows.Forms.Button updateTrackingCommentButton;
    }
}