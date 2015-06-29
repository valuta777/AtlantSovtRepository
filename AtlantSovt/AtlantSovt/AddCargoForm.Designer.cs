namespace AtlantSovt
{
    partial class AddCargoForm
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
            this.addCargoButton = new System.Windows.Forms.Button();
            this.addCargoTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addCargoButton
            // 
            this.addCargoButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addCargoButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCargoButton.Location = new System.Drawing.Point(0, 29);
            this.addCargoButton.Name = "addCargoButton";
            this.addCargoButton.Size = new System.Drawing.Size(384, 52);
            this.addCargoButton.TabIndex = 3;
            this.addCargoButton.Text = "Додати";
            this.addCargoButton.UseVisualStyleBackColor = true;
            this.addCargoButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addCargoTextBox
            // 
            this.addCargoTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCargoTextBox.Location = new System.Drawing.Point(0, 0);
            this.addCargoTextBox.Name = "addCargoTextBox";
            this.addCargoTextBox.Size = new System.Drawing.Size(384, 29);
            this.addCargoTextBox.TabIndex = 2;
            // 
            // AddCargoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addCargoButton);
            this.Controls.Add(this.addCargoTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCargoForm";
            this.Text = "Додавання типу вантажу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addCargoButton;
        private System.Windows.Forms.TextBox addCargoTextBox;
    }
}