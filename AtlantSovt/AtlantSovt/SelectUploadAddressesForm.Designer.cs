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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectUploadAddressesForm));
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
            resources.ApplyResources(this.addUploadAddressButton, "addUploadAddressButton");
            this.addUploadAddressButton.Name = "addUploadAddressButton";
            this.addUploadAddressButton.UseVisualStyleBackColor = true;
            this.addUploadAddressButton.Click += new System.EventHandler(this.addUploadAddressButton_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addUploadAddressButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addUploadAdressesToOrderButton);
            // 
            // addUploadAdressesToOrderButton
            // 
            resources.ApplyResources(this.addUploadAdressesToOrderButton, "addUploadAdressesToOrderButton");
            this.addUploadAdressesToOrderButton.Name = "addUploadAdressesToOrderButton";
            this.addUploadAdressesToOrderButton.UseVisualStyleBackColor = true;
            this.addUploadAdressesToOrderButton.Click += new System.EventHandler(this.addUploadAdressesToOrderButton_Click);
            // 
            // uploadAddressListBox
            // 
            this.uploadAddressListBox.CheckOnClick = true;
            resources.ApplyResources(this.uploadAddressListBox, "uploadAddressListBox");
            this.uploadAddressListBox.FormattingEnabled = true;
            this.uploadAddressListBox.Name = "uploadAddressListBox";
            this.uploadAddressListBox.DoubleClick += new System.EventHandler(this.uploadAddressListBox_DoubleClick);
            // 
            // SelectUploadAddressesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uploadAddressListBox);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectUploadAddressesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectUploadAddressesForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectUploadAddressesForm_Load);
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
        public System.Windows.Forms.CheckedListBox uploadAddressListBox;
    }
}