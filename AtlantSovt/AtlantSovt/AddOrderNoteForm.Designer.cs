namespace AtlantSovt
{
    partial class AddOrderNoteForm
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
            this.addOrderNoteButton = new System.Windows.Forms.Button();
            this.addOrderNoteTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addOrderNoteButton
            // 
            this.addOrderNoteButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addOrderNoteButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addOrderNoteButton.Location = new System.Drawing.Point(0, 273);
            this.addOrderNoteButton.Name = "addOrderNoteButton";
            this.addOrderNoteButton.Size = new System.Drawing.Size(517, 59);
            this.addOrderNoteButton.TabIndex = 93;
            this.addOrderNoteButton.Text = "Додати примітку";
            this.addOrderNoteButton.UseVisualStyleBackColor = true;
            this.addOrderNoteButton.Click += new System.EventHandler(this.addOrderNoteButton_Click);
            // 
            // addOrderNoteTextBox
            // 
            this.addOrderNoteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addOrderNoteTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addOrderNoteTextBox.Location = new System.Drawing.Point(1, 0);
            this.addOrderNoteTextBox.Multiline = true;
            this.addOrderNoteTextBox.Name = "addOrderNoteTextBox";
            this.addOrderNoteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.addOrderNoteTextBox.Size = new System.Drawing.Size(515, 274);
            this.addOrderNoteTextBox.TabIndex = 91;
            // 
            // AddOrderNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 332);
            this.Controls.Add(this.addOrderNoteButton);
            this.Controls.Add(this.addOrderNoteTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddOrderNoteForm";
            this.Text = "Додавання примітки";
            this.Shown += new System.EventHandler(this.AddOrderNoteForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addOrderNoteButton;
        private System.Windows.Forms.TextBox addOrderNoteTextBox;
    }
}