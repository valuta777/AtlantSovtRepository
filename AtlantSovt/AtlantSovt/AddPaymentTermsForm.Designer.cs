namespace AtlantSovt
{
    partial class AddPaymentTermsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPaymentTermsForm));
            this.addPaymentTermsButton = new System.Windows.Forms.Button();
            this.addPaymentTermsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addPaymentTermsButton
            // 
            resources.ApplyResources(this.addPaymentTermsButton, "addPaymentTermsButton");
            this.addPaymentTermsButton.Name = "addPaymentTermsButton";
            this.addPaymentTermsButton.UseVisualStyleBackColor = true;
            this.addPaymentTermsButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addPaymentTermsTextBox
            // 
            resources.ApplyResources(this.addPaymentTermsTextBox, "addPaymentTermsTextBox");
            this.addPaymentTermsTextBox.Name = "addPaymentTermsTextBox";
            // 
            // AddPaymentTermsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addPaymentTermsButton);
            this.Controls.Add(this.addPaymentTermsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPaymentTermsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addPaymentTermsButton;
        private System.Windows.Forms.TextBox addPaymentTermsTextBox;
    }
}