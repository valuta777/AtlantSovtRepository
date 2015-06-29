namespace AtlantSovt
{
    partial class SelectUploadAddressesForm
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
            this.addUploadAddressButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.addUploadAdressesToOrderButton = new System.Windows.Forms.Button();
            this.uploadAddressListBox = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addUploadAddressButton
            // 
            this.addUploadAddressButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addUploadAddressButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addUploadAddressButton.Location = new System.Drawing.Point(0, 0);
            this.addUploadAddressButton.Name = "addUploadAddressButton";
            this.addUploadAddressButton.Size = new System.Drawing.Size(281, 46);
            this.addUploadAddressButton.TabIndex = 1;
            this.addUploadAddressButton.Text = "Додати нову адресу";
            this.addUploadAddressButton.UseVisualStyleBackColor = true;
            this.addUploadAddressButton.Click += new System.EventHandler(this.addUploadAddressButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 228);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addUploadAddressButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addUploadAdressesToOrderButton);
            this.splitContainer1.Size = new System.Drawing.Size(564, 46);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // addUploadAdressesToOrderButton
            // 
            this.addUploadAdressesToOrderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addUploadAdressesToOrderButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addUploadAdressesToOrderButton.Location = new System.Drawing.Point(0, 0);
            this.addUploadAdressesToOrderButton.Name = "addUploadAdressesToOrderButton";
            this.addUploadAdressesToOrderButton.Size = new System.Drawing.Size(281, 46);
            this.addUploadAdressesToOrderButton.TabIndex = 2;
            this.addUploadAdressesToOrderButton.Text = "Додати до заявки";
            this.addUploadAdressesToOrderButton.UseVisualStyleBackColor = true;
            this.addUploadAdressesToOrderButton.Click += new System.EventHandler(this.addUploadAdressesToOrderButton_Click);
            // 
            // uploadAddressListBox
            // 
            this.uploadAddressListBox.CheckOnClick = true;
            this.uploadAddressListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadAddressListBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.uploadAddressListBox.FormattingEnabled = true;
            this.uploadAddressListBox.Location = new System.Drawing.Point(0, 0);
            this.uploadAddressListBox.Name = "uploadAddressListBox";
            this.uploadAddressListBox.Size = new System.Drawing.Size(564, 228);
            this.uploadAddressListBox.TabIndex = 4;
            this.uploadAddressListBox.DoubleClick += new System.EventHandler(this.uploadAddressListBox_DoubleClick);
            // 
            // SelectUploadAddressesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 274);
            this.Controls.Add(this.uploadAddressListBox);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectUploadAddressesForm";
            this.Text = "Виберіть адреси розвантаження";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addUploadAddressButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button addUploadAdressesToOrderButton;
        private System.Windows.Forms.CheckedListBox uploadAddressListBox;
    }
}