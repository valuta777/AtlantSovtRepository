namespace AtlantSovt
{
    partial class AddWorkDocumentForm
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
            this.addWorkDocumentButton = new System.Windows.Forms.Button();
            this.addWorkDocumentTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addWorkDocumentButton
            // 
            this.addWorkDocumentButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addWorkDocumentButton.Location = new System.Drawing.Point(101, 37);
            this.addWorkDocumentButton.Name = "addWorkDocumentButton";
            this.addWorkDocumentButton.Size = new System.Drawing.Size(200, 70);
            this.addWorkDocumentButton.TabIndex = 3;
            this.addWorkDocumentButton.Text = "Додати";
            this.addWorkDocumentButton.UseVisualStyleBackColor = true;
            this.addWorkDocumentButton.Click += new System.EventHandler(this.addWorkDocumentButton_Click);
            // 
            // addWorkDocumentTextBox
            // 
            this.addWorkDocumentTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addWorkDocumentTextBox.Location = new System.Drawing.Point(3, 5);
            this.addWorkDocumentTextBox.Name = "addWorkDocumentTextBox";
            this.addWorkDocumentTextBox.Size = new System.Drawing.Size(379, 29);
            this.addWorkDocumentTextBox.TabIndex = 2;
            // 
            // AddWorkDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.addWorkDocumentButton);
            this.Controls.Add(this.addWorkDocumentTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWorkDocumentForm";
            this.Text = "Додавання документу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addWorkDocumentButton;
        private System.Windows.Forms.TextBox addWorkDocumentTextBox;
    }
}