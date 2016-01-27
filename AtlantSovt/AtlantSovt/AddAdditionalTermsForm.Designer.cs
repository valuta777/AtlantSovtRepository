namespace AtlantSovt
{
    partial class AddAdditionalTermsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAdditionalTermsForm));
            this.addAdditionalTermsButton = new System.Windows.Forms.Button();
            this.addAdditionalTermsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addAdditionalTermsButton
            // 
            resources.ApplyResources(this.addAdditionalTermsButton, "addAdditionalTermsButton");
            this.addAdditionalTermsButton.Name = "addAdditionalTermsButton";
            this.addAdditionalTermsButton.UseVisualStyleBackColor = true;
            this.addAdditionalTermsButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addAdditionalTermsTextBox
            // 
            resources.ApplyResources(this.addAdditionalTermsTextBox, "addAdditionalTermsTextBox");
            this.addAdditionalTermsTextBox.Name = "addAdditionalTermsTextBox";
            // 
            // AddAdditionalTermsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addAdditionalTermsTextBox);
            this.Controls.Add(this.addAdditionalTermsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAdditionalTermsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addAdditionalTermsButton;
        private System.Windows.Forms.TextBox addAdditionalTermsTextBox;
    }
}