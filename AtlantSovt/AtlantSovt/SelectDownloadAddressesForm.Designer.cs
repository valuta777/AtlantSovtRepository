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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDownloadAddressesForm));
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
            resources.ApplyResources(this.addDownloadAddressButton, "addDownloadAddressButton");
            this.addDownloadAddressButton.Name = "addDownloadAddressButton";
            this.addDownloadAddressButton.UseVisualStyleBackColor = true;
            this.addDownloadAddressButton.Click += new System.EventHandler(this.addDownloadAddressButton_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addDownloadAddressButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addDownloadAddressToOrderButton);
            // 
            // addDownloadAddressToOrderButton
            // 
            resources.ApplyResources(this.addDownloadAddressToOrderButton, "addDownloadAddressToOrderButton");
            this.addDownloadAddressToOrderButton.Name = "addDownloadAddressToOrderButton";
            this.addDownloadAddressToOrderButton.UseVisualStyleBackColor = true;
            this.addDownloadAddressToOrderButton.Click += new System.EventHandler(this.addDownloadAddressToOrderButton_Click);
            // 
            // downloadAddresssListBox
            // 
            this.downloadAddresssListBox.CheckOnClick = true;
            resources.ApplyResources(this.downloadAddresssListBox, "downloadAddresssListBox");
            this.downloadAddresssListBox.FormattingEnabled = true;
            this.downloadAddresssListBox.Name = "downloadAddresssListBox";
            this.downloadAddresssListBox.DoubleClick += new System.EventHandler(this.downloadAddressListBox_DoubleClick);
            // 
            // SelectDownloadAddressesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.downloadAddresssListBox);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDownloadAddressesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectDownloadAddressesForm_FormClosing);
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