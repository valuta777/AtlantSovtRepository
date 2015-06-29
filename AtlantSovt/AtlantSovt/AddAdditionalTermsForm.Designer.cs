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
            // addAdditionalTermsButton
            // 
            this.addAdditionalTermsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addAdditionalTermsButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addAdditionalTermsButton.Location = new System.Drawing.Point(0, 29);
            this.addAdditionalTermsButton.Name = "addAdditionalTermsButton";
            this.addAdditionalTermsButton.Size = new System.Drawing.Size(384, 52);
            this.addAdditionalTermsButton.TabIndex = 3;
            this.addAdditionalTermsButton.Text = "Додати";
            this.addAdditionalTermsButton.UseVisualStyleBackColor = true;
            this.addAdditionalTermsButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addAdditionalTermsTextBox
            // 
            this.addAdditionalTermsTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addAdditionalTermsTextBox.Location = new System.Drawing.Point(0, 0);
            this.addAdditionalTermsTextBox.Name = "addAdditionalTermsTextBox";
            this.addAdditionalTermsTextBox.Size = new System.Drawing.Size(386, 29);
            this.addAdditionalTermsTextBox.TabIndex = 2;
            // 
            // AddAdditionalTermsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addAdditionalTermsTextBox);
            this.Controls.Add(this.addAdditionalTermsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAdditionalTermsForm";
            this.Text = "Додавання додаткових умов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addAdditionalTermsButton;
        private System.Windows.Forms.TextBox addAdditionalTermsTextBox;
    }
}