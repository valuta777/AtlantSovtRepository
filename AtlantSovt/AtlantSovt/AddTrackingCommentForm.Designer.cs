namespace AtlantSovt
{
    partial class AddTrackingCommentForm
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
            this.addCommentButton = new System.Windows.Forms.Button();
            this.addTrackingCommentTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // addCommentButton
            // 
            this.addCommentButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addCommentButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCommentButton.Location = new System.Drawing.Point(0, 273);
            this.addCommentButton.Name = "addCommentButton";
            this.addCommentButton.Size = new System.Drawing.Size(517, 59);
            this.addCommentButton.TabIndex = 90;
            this.addCommentButton.Text = "Додати коментар";
            this.addCommentButton.UseVisualStyleBackColor = true;
            this.addCommentButton.Click += new System.EventHandler(this.addCommentButton_Click);
            // 
            // addTrackingCommentTextBox
            // 
            this.addTrackingCommentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addTrackingCommentTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTrackingCommentTextBox.Location = new System.Drawing.Point(0, 0);
            this.addTrackingCommentTextBox.Name = "addTrackingCommentTextBox";
            this.addTrackingCommentTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.addTrackingCommentTextBox.Size = new System.Drawing.Size(517, 273);
            this.addTrackingCommentTextBox.TabIndex = 91;
            this.addTrackingCommentTextBox.Text = "";
            // 
            // AddTrackingCommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 332);
            this.Controls.Add(this.addTrackingCommentTextBox);
            this.Controls.Add(this.addCommentButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddTrackingCommentForm";
            this.Text = "Додавання коментарів";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addCommentButton;
        private System.Windows.Forms.RichTextBox addTrackingCommentTextBox;
    }
}