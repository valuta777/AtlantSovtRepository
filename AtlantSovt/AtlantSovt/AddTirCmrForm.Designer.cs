namespace AtlantSovt
{
    partial class AddTirCmrForm
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
            this.addTirCmrButton = new System.Windows.Forms.Button();
            this.addTirCmrTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addTirCmrButton
            // 
            this.addTirCmrButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTirCmrButton.Location = new System.Drawing.Point(101, 37);
            this.addTirCmrButton.Name = "addTirCmrButton";
            this.addTirCmrButton.Size = new System.Drawing.Size(200, 70);
            this.addTirCmrButton.TabIndex = 3;
            this.addTirCmrButton.Text = "Додати";
            this.addTirCmrButton.UseVisualStyleBackColor = true;
            this.addTirCmrButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addTirCmrTextBox
            // 
            this.addTirCmrTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTirCmrTextBox.Location = new System.Drawing.Point(3, 5);
            this.addTirCmrTextBox.Name = "addTirCmrTextBox";
            this.addTirCmrTextBox.Size = new System.Drawing.Size(379, 29);
            this.addTirCmrTextBox.TabIndex = 2;
            // 
            // AddWorkDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.addTirCmrButton);
            this.Controls.Add(this.addTirCmrTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCargoForm";
            this.Text = "Додавання типу вантажу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addTirCmrButton;
        private System.Windows.Forms.TextBox addTirCmrTextBox;
    }
}