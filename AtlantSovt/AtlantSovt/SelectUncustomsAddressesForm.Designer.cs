namespace AtlantSovt
{
    partial class SelectUncustomsAddressesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectUncustomsAddressesForm));
            this.addUncustomsAddressesButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.addUncustomsAddressesToOrderButton = new System.Windows.Forms.Button();
            this.uncustomsAddressesListBox = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addUncustomsAddressesButton
            // 
            resources.ApplyResources(this.addUncustomsAddressesButton, "addUncustomsAddressesButton");
            this.addUncustomsAddressesButton.Name = "addUncustomsAddressesButton";
            this.addUncustomsAddressesButton.UseVisualStyleBackColor = true;
            this.addUncustomsAddressesButton.Click += new System.EventHandler(this.addUncustomsAddressButton_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addUncustomsAddressesButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addUncustomsAddressesToOrderButton);
            // 
            // addUncustomsAddressesToOrderButton
            // 
            resources.ApplyResources(this.addUncustomsAddressesToOrderButton, "addUncustomsAddressesToOrderButton");
            this.addUncustomsAddressesToOrderButton.Name = "addUncustomsAddressesToOrderButton";
            this.addUncustomsAddressesToOrderButton.UseVisualStyleBackColor = true;
            this.addUncustomsAddressesToOrderButton.Click += new System.EventHandler(this.addUncustomsAddressToOrderButton_Click);
            // 
            // uncustomsAddressesListBox
            // 
            this.uncustomsAddressesListBox.CheckOnClick = true;
            resources.ApplyResources(this.uncustomsAddressesListBox, "uncustomsAddressesListBox");
            this.uncustomsAddressesListBox.FormattingEnabled = true;
            this.uncustomsAddressesListBox.Name = "uncustomsAddressesListBox";
            this.uncustomsAddressesListBox.DoubleClick += new System.EventHandler(this.unсustomsAddressListBox_DoubleClick);
            // 
            // SelectUncustomsAddressesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uncustomsAddressesListBox);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectUncustomsAddressesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectUncustomsAddressesForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectUncustomsAddressesForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addUncustomsAddressesButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button addUncustomsAddressesToOrderButton;
        public System.Windows.Forms.CheckedListBox uncustomsAddressesListBox;
    }
}