namespace AtlantSovt
{
    partial class AddTransporterVehicleForm
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
            this.addVehicleButton = new System.Windows.Forms.Button();
            this.addTransporterVehicleTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addVehicleButton
            // 
            this.addVehicleButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addVehicleButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addVehicleButton.Location = new System.Drawing.Point(0, 29);
            this.addVehicleButton.Name = "addVehicleButton";
            this.addVehicleButton.Size = new System.Drawing.Size(384, 52);
            this.addVehicleButton.TabIndex = 3;
            this.addVehicleButton.Text = "Додати";
            this.addVehicleButton.UseVisualStyleBackColor = true;
            this.addVehicleButton.Click += new System.EventHandler(this.addVehicleButton_Click);
            // 
            // addTransporterVehicleTextBox
            // 
            this.addTransporterVehicleTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTransporterVehicleTextBox.Location = new System.Drawing.Point(0, 0);
            this.addTransporterVehicleTextBox.Name = "addTransporterVehicleTextBox";
            this.addTransporterVehicleTextBox.Size = new System.Drawing.Size(385, 29);
            this.addTransporterVehicleTextBox.TabIndex = 2;
            // 
            // AddTransporterVehicleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addVehicleButton);
            this.Controls.Add(this.addTransporterVehicleTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTransporterVehicleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додавання типів транспорту";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addVehicleButton;
        private System.Windows.Forms.TextBox addTransporterVehicleTextBox;
    }
}