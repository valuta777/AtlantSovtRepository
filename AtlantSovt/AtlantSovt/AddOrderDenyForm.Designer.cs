﻿namespace AtlantSovt
{
    partial class AddOrderDenyForm
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
            this.addOrderDenyButton = new System.Windows.Forms.Button();
            this.addOrderDenyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addTirCmrButton
            // 
            this.addOrderDenyButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addOrderDenyButton.Location = new System.Drawing.Point(101, 37);
            this.addOrderDenyButton.Name = "addOrderDenyButton";
            this.addOrderDenyButton.Size = new System.Drawing.Size(200, 70);
            this.addOrderDenyButton.TabIndex = 3;
            this.addOrderDenyButton.Text = "Додати";
            this.addOrderDenyButton.UseVisualStyleBackColor = true;
            this.addOrderDenyButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addTirCmrTextBox
            // 
            this.addOrderDenyTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addOrderDenyTextBox.Location = new System.Drawing.Point(3, 5);
            this.addOrderDenyTextBox.Name = "addOrderDenyTextBox";
            this.addOrderDenyTextBox.Size = new System.Drawing.Size(379, 29);
            this.addOrderDenyTextBox.TabIndex = 2;
            // 
            // AddWorkDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.addOrderDenyButton);
            this.Controls.Add(this.addOrderDenyTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOrderDenyForm";
            this.Text = "Додавання штрафу за відмову від заявки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addOrderDenyButton;
        private System.Windows.Forms.TextBox addOrderDenyTextBox;
    }
}