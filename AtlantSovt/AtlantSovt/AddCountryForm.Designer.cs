namespace AtlantSovt
{
    partial class AddCountryForm
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
            this.addCountryButton = new System.Windows.Forms.Button();
            this.addCountryTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addCountryButton
            // 
            this.addCountryButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addCountryButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCountryButton.Location = new System.Drawing.Point(0, 29);
            this.addCountryButton.Name = "addCountryButton";
            this.addCountryButton.Size = new System.Drawing.Size(384, 52);
            this.addCountryButton.TabIndex = 3;
            this.addCountryButton.Text = "Додати";
            this.addCountryButton.UseVisualStyleBackColor = true;
            this.addCountryButton.Click += new System.EventHandler(this.addCountryButton_Click);
            // 
            // addCountryTextBox
            // 
            this.addCountryTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCountryTextBox.Location = new System.Drawing.Point(0, 0);
            this.addCountryTextBox.Name = "addCountryTextBox";
            this.addCountryTextBox.Size = new System.Drawing.Size(385, 29);
            this.addCountryTextBox.TabIndex = 2;
            // 
            // AddCountryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addCountryButton);
            this.Controls.Add(this.addCountryTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCountryForm";
            this.Text = "Додавання країни";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addCountryButton;
        private System.Windows.Forms.TextBox addCountryTextBox;
    }
}