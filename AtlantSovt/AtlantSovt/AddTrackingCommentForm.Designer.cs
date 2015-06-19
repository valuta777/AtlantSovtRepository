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
            this.commentForwarderUpdateTextBox = new System.Windows.Forms.TextBox();
            this.addTrackingComentLabel = new System.Windows.Forms.Label();
            this.createContactButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // commentForwarderUpdateTextBox
            // 
            this.commentForwarderUpdateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commentForwarderUpdateTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commentForwarderUpdateTextBox.Location = new System.Drawing.Point(1, 24);
            this.commentForwarderUpdateTextBox.Multiline = true;
            this.commentForwarderUpdateTextBox.Name = "commentForwarderUpdateTextBox";
            this.commentForwarderUpdateTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentForwarderUpdateTextBox.Size = new System.Drawing.Size(515, 250);
            this.commentForwarderUpdateTextBox.TabIndex = 76;
            // 
            // addTrackingComentLabel
            // 
            this.addTrackingComentLabel.AutoSize = true;
            this.addTrackingComentLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addTrackingComentLabel.Location = new System.Drawing.Point(0, 0);
            this.addTrackingComentLabel.Name = "addTrackingComentLabel";
            this.addTrackingComentLabel.Size = new System.Drawing.Size(83, 21);
            this.addTrackingComentLabel.TabIndex = 77;
            this.addTrackingComentLabel.Text = "Коментар:";
            // 
            // createContactButton
            // 
            this.createContactButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.createContactButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createContactButton.Location = new System.Drawing.Point(0, 273);
            this.createContactButton.Name = "createContactButton";
            this.createContactButton.Size = new System.Drawing.Size(517, 59);
            this.createContactButton.TabIndex = 90;
            this.createContactButton.Text = "Додати коментар";
            this.createContactButton.UseVisualStyleBackColor = true;
            this.createContactButton.Click += new System.EventHandler(this.createContactButton_Click);
            // 
            // AddTrackingCommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 332);
            this.Controls.Add(this.createContactButton);
            this.Controls.Add(this.addTrackingComentLabel);
            this.Controls.Add(this.commentForwarderUpdateTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddTrackingCommentForm";
            this.Text = "Додавання коментарів";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox commentForwarderUpdateTextBox;
        private System.Windows.Forms.Label addTrackingComentLabel;
        private System.Windows.Forms.Button createContactButton;
    }
}