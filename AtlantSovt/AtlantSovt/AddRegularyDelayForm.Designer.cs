namespace AtlantSovt
{
    partial class AddRegularyDelayForm
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
            this.addRegularyDelayButton = new System.Windows.Forms.Button();
            this.addRegularyDelayTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addRegularyDelayButton
            // 
            this.addRegularyDelayButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addRegularyDelayButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addRegularyDelayButton.Location = new System.Drawing.Point(0, 29);
            this.addRegularyDelayButton.Name = "addRegularyDelayButton";
            this.addRegularyDelayButton.Size = new System.Drawing.Size(384, 52);
            this.addRegularyDelayButton.TabIndex = 3;
            this.addRegularyDelayButton.Text = "Додати";
            this.addRegularyDelayButton.UseVisualStyleBackColor = true;
            this.addRegularyDelayButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addRegularyDelayTextBox
            // 
            this.addRegularyDelayTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addRegularyDelayTextBox.Location = new System.Drawing.Point(0, 0);
            this.addRegularyDelayTextBox.Name = "addRegularyDelayTextBox";
            this.addRegularyDelayTextBox.Size = new System.Drawing.Size(384, 29);
            this.addRegularyDelayTextBox.TabIndex = 2;
            // 
            // AddRegularyDelayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addRegularyDelayButton);
            this.Controls.Add(this.addRegularyDelayTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRegularyDelayForm";
            this.Text = "Додавання нормативних простоїв";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addRegularyDelayButton;
        private System.Windows.Forms.TextBox addRegularyDelayTextBox;
    }
}