﻿namespace AtlantSovt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCargoForm));
            this.addCargoButton = new System.Windows.Forms.Button();
            this.addCargoTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addCargoButton
            // 
            resources.ApplyResources(this.addCargoButton, "addCargoButton");
            this.addCargoButton.Name = "addCargoButton";
            this.addCargoButton.UseVisualStyleBackColor = true;
            this.addCargoButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addCargoTextBox
            // 
            resources.ApplyResources(this.addCargoTextBox, "addCargoTextBox");
            this.addCargoTextBox.Name = "addCargoTextBox";
            // 
            // AddCargoForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addCargoButton);
            this.Controls.Add(this.addCargoTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCargoForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addCargoButton;
        private System.Windows.Forms.TextBox addCargoTextBox;
    }
}