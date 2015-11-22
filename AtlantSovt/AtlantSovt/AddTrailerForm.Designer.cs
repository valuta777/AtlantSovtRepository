namespace AtlantSovt
{
    partial class AddTrailerForm
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
            this.addTrailerButton = new System.Windows.Forms.Button();
            this.addTrailerTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addTrailerButton
            // 
            this.addTrailerButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addTrailerButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTrailerButton.Location = new System.Drawing.Point(0, 29);
            this.addTrailerButton.Name = "addTrailerButton";
            this.addTrailerButton.Size = new System.Drawing.Size(384, 52);
            this.addTrailerButton.TabIndex = 3;
            this.addTrailerButton.Text = "Додати";
            this.addTrailerButton.UseVisualStyleBackColor = true;
            this.addTrailerButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addTrailerTextBox
            // 
            this.addTrailerTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTrailerTextBox.Location = new System.Drawing.Point(0, 0);
            this.addTrailerTextBox.Name = "addTrailerTextBox";
            this.addTrailerTextBox.Size = new System.Drawing.Size(384, 29);
            this.addTrailerTextBox.TabIndex = 2;
            // 
            // AddTrailerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.addTrailerButton);
            this.Controls.Add(this.addTrailerTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTrailerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додавання типу вантажу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addTrailerButton;
        private System.Windows.Forms.TextBox addTrailerTextBox;
    }
}