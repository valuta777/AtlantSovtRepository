namespace AtlantSovt
{
    partial class AddFineForDelayForm
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
            this.addFineForDelayButton = new System.Windows.Forms.Button();
            this.addFineForDelayTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addFineForDelayButton
            // 
            this.addFineForDelayButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addFineForDelayButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addFineForDelayButton.Location = new System.Drawing.Point(0, 27);
            this.addFineForDelayButton.Name = "addFineForDelayButton";
            this.addFineForDelayButton.Size = new System.Drawing.Size(384, 54);
            this.addFineForDelayButton.TabIndex = 3;
            this.addFineForDelayButton.Text = "Додати";
            this.addFineForDelayButton.UseVisualStyleBackColor = true;
            this.addFineForDelayButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addFineForDelayTextBox
            // 
            this.addFineForDelayTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addFineForDelayTextBox.Location = new System.Drawing.Point(0, 0);
            this.addFineForDelayTextBox.Name = "addFineForDelayTextBox";
            this.addFineForDelayTextBox.Size = new System.Drawing.Size(387, 29);
            this.addFineForDelayTextBox.TabIndex = 2;
            // 
            // AddFineForDelayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addFineForDelayButton);
            this.Controls.Add(this.addFineForDelayTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFineForDelayForm";
            this.Text = "Додавання штрафу за затримку";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addFineForDelayButton;
        private System.Windows.Forms.TextBox addFineForDelayTextBox;
    }
}