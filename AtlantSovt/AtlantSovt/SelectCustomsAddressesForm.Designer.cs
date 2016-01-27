namespace AtlantSovt
{
    partial class SelectCustomsAddressesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectCustomsAddressesForm));
            this.addCustomsAddressesButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.addCustomsAddressesToOrderButton = new System.Windows.Forms.Button();
            this.customsAddressesListBox = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addCustomsAddressesButton
            // 
            resources.ApplyResources(this.addCustomsAddressesButton, "addCustomsAddressesButton");
            this.addCustomsAddressesButton.Name = "addCustomsAddressesButton";
            this.addCustomsAddressesButton.UseVisualStyleBackColor = true;
            this.addCustomsAddressesButton.Click += new System.EventHandler(this.addCustomsAddressButton_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addCustomsAddressesButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addCustomsAddressesToOrderButton);
            // 
            // addCustomsAddressesToOrderButton
            // 
            resources.ApplyResources(this.addCustomsAddressesToOrderButton, "addCustomsAddressesToOrderButton");
            this.addCustomsAddressesToOrderButton.Name = "addCustomsAddressesToOrderButton";
            this.addCustomsAddressesToOrderButton.UseVisualStyleBackColor = true;
            this.addCustomsAddressesToOrderButton.Click += new System.EventHandler(this.addCustomsAddressToOrderButton_Click);
            // 
            // customsAddressesListBox
            // 
            this.customsAddressesListBox.CheckOnClick = true;
            resources.ApplyResources(this.customsAddressesListBox, "customsAddressesListBox");
            this.customsAddressesListBox.FormattingEnabled = true;
            this.customsAddressesListBox.Name = "customsAddressesListBox";
            this.customsAddressesListBox.DoubleClick += new System.EventHandler(this.сustomsAddressListBox_DoubleClick);
            // 
            // SelectCustomsAddressesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customsAddressesListBox);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectCustomsAddressesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectCustomsAddressesForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectCustomsAddressesForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addCustomsAddressesButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button addCustomsAddressesToOrderButton;
        public System.Windows.Forms.CheckedListBox customsAddressesListBox;
    }
}