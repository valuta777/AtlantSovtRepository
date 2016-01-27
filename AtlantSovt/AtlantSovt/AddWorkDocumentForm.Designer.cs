namespace AtlantSovt
{
    partial class AddWorkDocumentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWorkDocumentForm));
            this.addWorkDocumentButton = new System.Windows.Forms.Button();
            this.addWorkDocumentTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addWorkDocumentButton
            // 
            resources.ApplyResources(this.addWorkDocumentButton, "addWorkDocumentButton");
            this.addWorkDocumentButton.Name = "addWorkDocumentButton";
            this.addWorkDocumentButton.UseVisualStyleBackColor = true;
            this.addWorkDocumentButton.Click += new System.EventHandler(this.addWorkDocumentButton_Click);
            // 
            // addWorkDocumentTextBox
            // 
            resources.ApplyResources(this.addWorkDocumentTextBox, "addWorkDocumentTextBox");
            this.addWorkDocumentTextBox.Name = "addWorkDocumentTextBox";
            // 
            // AddWorkDocumentForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addWorkDocumentButton);
            this.Controls.Add(this.addWorkDocumentTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWorkDocumentForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addWorkDocumentButton;
        private System.Windows.Forms.TextBox addWorkDocumentTextBox;
    }
}