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
            this.addTrackingCommentTextBox = new System.Windows.Forms.TextBox();
            this.addCommentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addTrackingCommentTextBox
            // 
            this.addTrackingCommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addTrackingCommentTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTrackingCommentTextBox.Location = new System.Drawing.Point(1, -1);
            this.addTrackingCommentTextBox.Multiline = true;
            this.addTrackingCommentTextBox.Name = "addTrackingCommentTextBox";
            this.addTrackingCommentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.addTrackingCommentTextBox.Size = new System.Drawing.Size(515, 275);
            this.addTrackingCommentTextBox.TabIndex = 76;
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
            // AddTrackingCommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 332);
            this.Controls.Add(this.addCommentButton);
            this.Controls.Add(this.addTrackingCommentTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddTrackingCommentForm";
            this.Text = "Додавання коментарів";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox addTrackingCommentTextBox;
        private System.Windows.Forms.Button addCommentButton;
    }
}