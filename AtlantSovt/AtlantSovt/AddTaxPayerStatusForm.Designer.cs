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
            this.addTaxPayerStatusTextBox = new System.Windows.Forms.TextBox();
            this.addTaxPayerStatusButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addTaxPayerStatusTextBox
            // 
            this.addTaxPayerStatusTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTaxPayerStatusTextBox.Location = new System.Drawing.Point(0, 0);
            this.addTaxPayerStatusTextBox.Name = "addTaxPayerStatusTextBox";
            this.addTaxPayerStatusTextBox.Size = new System.Drawing.Size(386, 29);
            this.addTaxPayerStatusTextBox.TabIndex = 0;
            // 
            // addTaxPayerStatusButton
            // 
            this.addTaxPayerStatusButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addTaxPayerStatusButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addTaxPayerStatusButton.Location = new System.Drawing.Point(0, 29);
            this.addTaxPayerStatusButton.Name = "addTaxPayerStatusButton";
            this.addTaxPayerStatusButton.Size = new System.Drawing.Size(384, 52);
            this.addTaxPayerStatusButton.TabIndex = 1;
            this.addTaxPayerStatusButton.Text = "Додати";
            this.addTaxPayerStatusButton.UseVisualStyleBackColor = true;
            this.addTaxPayerStatusButton.Click += new System.EventHandler(this.addTaxPayerStatusButton_Click);
            // 
            // AddTaxPayerStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addTaxPayerStatusButton);
            this.Controls.Add(this.addTaxPayerStatusTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTaxPayerStatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додавання статусу платника податку";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox addTaxPayerStatusTextBox;
        private System.Windows.Forms.Button addTaxPayerStatusButton;
    }
}