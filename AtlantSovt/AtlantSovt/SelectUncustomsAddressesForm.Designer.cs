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
            this.addUncustomsAddressesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addUncustomsAddressesButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addUncustomsAddressesButton.Location = new System.Drawing.Point(0, 0);
            this.addUncustomsAddressesButton.Name = "addUncustomsAddressesButton";
            this.addUncustomsAddressesButton.Size = new System.Drawing.Size(281, 46);
            this.addUncustomsAddressesButton.TabIndex = 1;
            this.addUncustomsAddressesButton.Text = "Додати нову адресу";
            this.addUncustomsAddressesButton.UseVisualStyleBackColor = true;
            this.addUncustomsAddressesButton.Click += new System.EventHandler(this.addUncustomsAddressButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 228);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addUncustomsAddressesButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addUncustomsAddressesToOrderButton);
            this.splitContainer1.Size = new System.Drawing.Size(564, 46);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // addUncustomsAddressesToOrderButton
            // 
            this.addUncustomsAddressesToOrderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addUncustomsAddressesToOrderButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.addUncustomsAddressesToOrderButton.Location = new System.Drawing.Point(0, 0);
            this.addUncustomsAddressesToOrderButton.Name = "addUncustomsAddressesToOrderButton";
            this.addUncustomsAddressesToOrderButton.Size = new System.Drawing.Size(281, 46);
            this.addUncustomsAddressesToOrderButton.TabIndex = 2;
            this.addUncustomsAddressesToOrderButton.Text = "Додати до заявки";
            this.addUncustomsAddressesToOrderButton.UseVisualStyleBackColor = true;
            this.addUncustomsAddressesToOrderButton.Click += new System.EventHandler(this.addUncustomsAddressToOrderButton_Click);
            // 
            // uncustomsAddressesListBox
            // 
            this.uncustomsAddressesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uncustomsAddressesListBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.uncustomsAddressesListBox.FormattingEnabled = true;
            this.uncustomsAddressesListBox.Location = new System.Drawing.Point(0, 0);
            this.uncustomsAddressesListBox.Name = "uncustomsAddressesListBox";
            this.uncustomsAddressesListBox.Size = new System.Drawing.Size(564, 228);
            this.uncustomsAddressesListBox.TabIndex = 4;
            this.uncustomsAddressesListBox.DoubleClick += new System.EventHandler(this.unсustomsAddressListBox_DoubleClick);
            // 
            // SelectUncustomsAddressesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 274);
            this.Controls.Add(this.uncustomsAddressesListBox);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SelectUncustomsAddressesForm";
            this.Text = "Виберіть адреси розмитнення";
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
        private System.Windows.Forms.CheckedListBox uncustomsAddressesListBox;
    }
}