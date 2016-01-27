namespace AtlantSovt
{
    partial class AddTrackingCommentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTrackingCommentForm));
            this.addCommentButton = new System.Windows.Forms.Button();
            this.addTrackingCommentTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // addCommentButton
            // 
            resources.ApplyResources(this.addCommentButton, "addCommentButton");
            this.addCommentButton.Name = "addCommentButton";
            this.addCommentButton.UseVisualStyleBackColor = true;
            this.addCommentButton.Click += new System.EventHandler(this.addCommentButton_Click);
            // 
            // addTrackingCommentTextBox
            // 
            resources.ApplyResources(this.addTrackingCommentTextBox, "addTrackingCommentTextBox");
            this.addTrackingCommentTextBox.Name = "addTrackingCommentTextBox";
            // 
            // AddTrackingCommentForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addTrackingCommentTextBox);
            this.Controls.Add(this.addCommentButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTrackingCommentForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addCommentButton;
        private System.Windows.Forms.RichTextBox addTrackingCommentTextBox;
    }
}