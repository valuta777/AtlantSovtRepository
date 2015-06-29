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
            this.addCustomsAddressesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addCustomsAddressesButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addCustomsAddressesButton.Location = new System.Drawing.Point(0, 0);
            this.addCustomsAddressesButton.Name = "addCustomsAddressesButton";
            this.addCustomsAddressesButton.Size = new System.Drawing.Size(281, 46);
            this.addCustomsAddressesButton.TabIndex = 1;
            this.addCustomsAddressesButton.Text = "Додати нову адресу";
            this.addCustomsAddressesButton.UseVisualStyleBackColor = true;
            this.addCustomsAddressesButton.Click += new System.EventHandler(this.addCustomsAddressButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 228);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addCustomsAddressesButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addCustomsAddressesToOrderButton);
            this.splitContainer1.Size = new System.Drawing.Size(564, 46);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // addCustomsAddressesToOrderButton
            // 
            this.addCustomsAddressesToOrderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addCustomsAddressesToOrderButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addCustomsAddressesToOrderButton.Location = new System.Drawing.Point(0, 0);
            this.addCustomsAddressesToOrderButton.Name = "addCustomsAddressesToOrderButton";
            this.addCustomsAddressesToOrderButton.Size = new System.Drawing.Size(281, 46);
            this.addCustomsAddressesToOrderButton.TabIndex = 2;
            this.addCustomsAddressesToOrderButton.Text = "Додати до заявки";
            this.addCustomsAddressesToOrderButton.UseVisualStyleBackColor = true;
            this.addCustomsAddressesToOrderButton.Click += new System.EventHandler(this.addCustomsAddressToOrderButton_Click);
            // 
            // customsAddressesListBox
            // 
            this.customsAddressesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customsAddressesListBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.customsAddressesListBox.FormattingEnabled = true;
            this.customsAddressesListBox.Location = new System.Drawing.Point(0, 0);
            this.customsAddressesListBox.Name = "customsAddressesListBox";
            this.customsAddressesListBox.Size = new System.Drawing.Size(564, 228);
            this.customsAddressesListBox.TabIndex = 4;
            this.customsAddressesListBox.DoubleClick += new System.EventHandler(this.сustomsAddressListBox_DoubleClick);
            // 
            // SelectCustomsAddressesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 274);
            this.Controls.Add(this.customsAddressesListBox);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SelectCustomsAddressesForm";
            this.Text = "Виберіть адреси замитнення";
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
        private System.Windows.Forms.CheckedListBox customsAddressesListBox;
    }
}