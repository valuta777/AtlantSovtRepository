namespace AtlantSovt
{
    partial class SelectDownloadAddressesForm
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
            this.addDownloadAddressButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.addDownloadAddressToOrderButton = new System.Windows.Forms.Button();
            this.downloadAddresssListBox = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addDownloadAddressButton
            // 
            this.addDownloadAddressButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addDownloadAddressButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addDownloadAddressButton.Location = new System.Drawing.Point(0, 0);
            this.addDownloadAddressButton.Name = "addDownloadAddressButton";
            this.addDownloadAddressButton.Size = new System.Drawing.Size(281, 46);
            this.addDownloadAddressButton.TabIndex = 1;
            this.addDownloadAddressButton.Text = "Додати нову адресу";
            this.addDownloadAddressButton.UseVisualStyleBackColor = true;
            this.addDownloadAddressButton.Click += new System.EventHandler(this.addDownloadAddressButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 228);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addDownloadAddressButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addDownloadAddressToOrderButton);
            this.splitContainer1.Size = new System.Drawing.Size(564, 46);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // addDownloadAddressToOrderButton
            // 
            this.addDownloadAddressToOrderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addDownloadAddressToOrderButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addDownloadAddressToOrderButton.Location = new System.Drawing.Point(0, 0);
            this.addDownloadAddressToOrderButton.Name = "addDownloadAddressToOrderButton";
            this.addDownloadAddressToOrderButton.Size = new System.Drawing.Size(281, 46);
            this.addDownloadAddressToOrderButton.TabIndex = 2;
            this.addDownloadAddressToOrderButton.Text = "Додати до заявки";
            this.addDownloadAddressToOrderButton.UseVisualStyleBackColor = true;
            this.addDownloadAddressToOrderButton.Click += new System.EventHandler(this.addDownloadAddressToOrderButton_Click);
            // 
            // downloadAddresssListBox
            // 
            this.downloadAddresssListBox.CheckOnClick = true;
            this.downloadAddresssListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadAddresssListBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.downloadAddresssListBox.FormattingEnabled = true;
            this.downloadAddresssListBox.Location = new System.Drawing.Point(0, 0);
            this.downloadAddresssListBox.Name = "downloadAddresssListBox";
            this.downloadAddresssListBox.Size = new System.Drawing.Size(564, 228);
            this.downloadAddresssListBox.TabIndex = 4;
            this.downloadAddresssListBox.DoubleClick += new System.EventHandler(this.downloadAddressListBox_DoubleClick);
            // 
            // SelectDownloadAddressesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 274);
            this.Controls.Add(this.downloadAddresssListBox);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDownloadAddressesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Виберіть адреси завантаження";
            this.Load += new System.EventHandler(this.SelectDownloadAddressesForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addDownloadAddressButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button addDownloadAddressToOrderButton;
        public System.Windows.Forms.CheckedListBox downloadAddresssListBox;
    }
}