﻿namespace AtlantSovt
{
    partial class AddStaffForm
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
            this.addStaffButton = new System.Windows.Forms.Button();
            this.addStaffTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addStaffButton
            // 
            this.addStaffButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addStaffButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStaffButton.Location = new System.Drawing.Point(0, 29);
            this.addStaffButton.Name = "addStaffButton";
            this.addStaffButton.Size = new System.Drawing.Size(384, 52);
            this.addStaffButton.TabIndex = 3;
            this.addStaffButton.Text = "Додати";
            this.addStaffButton.UseVisualStyleBackColor = true;
            this.addStaffButton.Click += new System.EventHandler(this.addStaffButton_Click);
            // 
            // addStaffTextBox
            // 
            this.addStaffTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addStaffTextBox.Location = new System.Drawing.Point(0, 0);
            this.addStaffTextBox.Name = "addStaffTextBox";
            this.addStaffTextBox.Size = new System.Drawing.Size(384, 29);
            this.addStaffTextBox.TabIndex = 2;
            // 
            // addStaffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addStaffButton);
            this.Controls.Add(this.addStaffTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addStaffForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додавання персоналу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addStaffButton;
        private System.Windows.Forms.TextBox addStaffTextBox;
    }
}