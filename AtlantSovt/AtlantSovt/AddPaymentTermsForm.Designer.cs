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
            this.addPaymentTermsButton = new System.Windows.Forms.Button();
            this.addPaymentTermsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addTirCmrButton
            // 
            this.addPaymentTermsButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addPaymentTermsButton.Location = new System.Drawing.Point(101, 37);
            this.addPaymentTermsButton.Name = "addTirCmrButton";
            this.addPaymentTermsButton.Size = new System.Drawing.Size(200, 70);
            this.addPaymentTermsButton.TabIndex = 3;
            this.addPaymentTermsButton.Text = "Додати";
            this.addPaymentTermsButton.UseVisualStyleBackColor = true;
            this.addPaymentTermsButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addTirCmrTextBox
            // 
            this.addPaymentTermsTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addPaymentTermsTextBox.Location = new System.Drawing.Point(3, 5);
            this.addPaymentTermsTextBox.Name = "addTirCmrTextBox";
            this.addPaymentTermsTextBox.Size = new System.Drawing.Size(379, 29);
            this.addPaymentTermsTextBox.TabIndex = 2;
            // 
            // AddWorkDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.addPaymentTermsButton);
            this.Controls.Add(this.addPaymentTermsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCargoForm";
            this.Text = "Додавання типу вантажу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addPaymentTermsButton;
        private System.Windows.Forms.TextBox addPaymentTermsTextBox;
    }
}