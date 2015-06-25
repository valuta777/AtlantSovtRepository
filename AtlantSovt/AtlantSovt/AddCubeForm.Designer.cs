namespace AtlantSovt
{
    partial class AddCubeForm
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
            this.addCubeButton = new System.Windows.Forms.Button();
            this.addCubeTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addTirCmrButton
            // 
            this.addCubeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCubeButton.Location = new System.Drawing.Point(101, 37);
            this.addCubeButton.Name = "addCubeButton";
            this.addCubeButton.Size = new System.Drawing.Size(200, 70);
            this.addCubeButton.TabIndex = 3;
            this.addCubeButton.Text = "Додати";
            this.addCubeButton.UseVisualStyleBackColor = true;
            this.addCubeButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addTirCmrTextBox
            // 
            this.addCubeTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCubeTextBox.Location = new System.Drawing.Point(3, 5);
            this.addCubeTextBox.Name = "addCubeTextBox";
            this.addCubeTextBox.Size = new System.Drawing.Size(379, 29);
            this.addCubeTextBox.TabIndex = 2;
            // 
            // AddWorkDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.addCubeButton);
            this.Controls.Add(this.addCubeTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCubeForm";
            this.Text = "Додавання Кубів";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addCubeButton;
        private System.Windows.Forms.TextBox addCubeTextBox;
    }
}