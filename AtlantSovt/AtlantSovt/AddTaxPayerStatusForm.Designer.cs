namespace AtlantSovt
{
    partial class AddTaxPayerStatusForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTaxPayerStatusForm));
            this.addTaxPayerStatusTextBox = new System.Windows.Forms.TextBox();
            this.addTaxPayerStatusButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addTaxPayerStatusTextBox
            // 
            resources.ApplyResources(this.addTaxPayerStatusTextBox, "addTaxPayerStatusTextBox");
            this.addTaxPayerStatusTextBox.Name = "addTaxPayerStatusTextBox";
            // 
            // addTaxPayerStatusButton
            // 
            resources.ApplyResources(this.addTaxPayerStatusButton, "addTaxPayerStatusButton");
            this.addTaxPayerStatusButton.Name = "addTaxPayerStatusButton";
            this.addTaxPayerStatusButton.UseVisualStyleBackColor = true;
            this.addTaxPayerStatusButton.Click += new System.EventHandler(this.addTaxPayerStatusButton_Click);
            // 
            // AddTaxPayerStatusForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addTaxPayerStatusButton);
            this.Controls.Add(this.addTaxPayerStatusTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTaxPayerStatusForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox addTaxPayerStatusTextBox;
        private System.Windows.Forms.Button addTaxPayerStatusButton;
    }
}