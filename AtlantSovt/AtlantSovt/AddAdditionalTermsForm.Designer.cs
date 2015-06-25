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
            this.addAdditionalTermsButton = new System.Windows.Forms.Button();
            this.addAdditionalTermsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addTirCmrButton
            // 
            this.addAdditionalTermsButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addAdditionalTermsButton.Location = new System.Drawing.Point(101, 37);
            this.addAdditionalTermsButton.Name = "addAdditionalTermsButton";
            this.addAdditionalTermsButton.Size = new System.Drawing.Size(200, 70);
            this.addAdditionalTermsButton.TabIndex = 3;
            this.addAdditionalTermsButton.Text = "Додати";
            this.addAdditionalTermsButton.UseVisualStyleBackColor = true;
            this.addAdditionalTermsButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addTirCmrTextBox
            // 
            this.addAdditionalTermsTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addAdditionalTermsTextBox.Location = new System.Drawing.Point(3, 5);
            this.addAdditionalTermsTextBox.Name = "addAdditionalTermsTextBox";
            this.addAdditionalTermsTextBox.Size = new System.Drawing.Size(379, 29);
            this.addAdditionalTermsTextBox.TabIndex = 2;
            // 
            // AddWorkDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.addAdditionalTermsButton);
            this.Controls.Add(this.addAdditionalTermsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCargoForm";
            this.Text = "Додавання додаткових умов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addAdditionalTermsButton;
        private System.Windows.Forms.TextBox addAdditionalTermsTextBox;
    }
}