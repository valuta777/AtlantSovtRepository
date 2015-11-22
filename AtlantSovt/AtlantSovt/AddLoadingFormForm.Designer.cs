namespace AtlantSovt
{
    partial class AddLoadingFormForm
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
            this.addLoadingFormButton = new System.Windows.Forms.Button();
            this.addLoadingFormTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addLoadingFormButton
            // 
            this.addLoadingFormButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addLoadingFormButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addLoadingFormButton.Location = new System.Drawing.Point(0, 29);
            this.addLoadingFormButton.Name = "addLoadingFormButton";
            this.addLoadingFormButton.Size = new System.Drawing.Size(384, 52);
            this.addLoadingFormButton.TabIndex = 3;
            this.addLoadingFormButton.Text = "Додати";
            this.addLoadingFormButton.UseVisualStyleBackColor = true;
            this.addLoadingFormButton.Click += new System.EventHandler(this.addLoadingFormButton_Click);
            // 
            // addLoadingFormTextBox
            // 
            this.addLoadingFormTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addLoadingFormTextBox.Location = new System.Drawing.Point(0, 0);
            this.addLoadingFormTextBox.Name = "addLoadingFormTextBox";
            this.addLoadingFormTextBox.Size = new System.Drawing.Size(387, 29);
            this.addLoadingFormTextBox.TabIndex = 2;
            // 
            // AddLoadingFormForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addLoadingFormButton);
            this.Controls.Add(this.addLoadingFormTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLoadingFormForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додавання форм завантаження";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addLoadingFormButton;
        private System.Windows.Forms.TextBox addLoadingFormTextBox;
    }
}