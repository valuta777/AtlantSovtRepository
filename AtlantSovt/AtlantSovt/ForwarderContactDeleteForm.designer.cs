namespace AtlantSovt
{
    partial class ForwarderContactDeleteForm
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
            this.label27 = new System.Windows.Forms.Label();
            this.forwarderUpdateSelectDeleteContactComboBox = new System.Windows.Forms.ComboBox();
            this.forwarderUpdateContactDeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.Location = new System.Drawing.Point(12, 9);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(138, 21);
            this.label27.TabIndex = 65;
            this.label27.Text = "Виберіть контакт";
            // 
            // transporterUpdateSelectDeleteContactComboBox
            // 
            this.forwarderUpdateSelectDeleteContactComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.forwarderUpdateSelectDeleteContactComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.forwarderUpdateSelectDeleteContactComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.forwarderUpdateSelectDeleteContactComboBox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.forwarderUpdateSelectDeleteContactComboBox.Location = new System.Drawing.Point(10, 33);
            this.forwarderUpdateSelectDeleteContactComboBox.Name = "forwarderUpdateSelectDeleteContactComboBox";
            this.forwarderUpdateSelectDeleteContactComboBox.Size = new System.Drawing.Size(562, 29);
            this.forwarderUpdateSelectDeleteContactComboBox.Sorted = true;
            this.forwarderUpdateSelectDeleteContactComboBox.TabIndex = 64;
            this.forwarderUpdateSelectDeleteContactComboBox.SelectedIndexChanged += new System.EventHandler(this.ForwarderUpdateSelectDeleteContactComboBox_SelectedIndexChanged);
            this.forwarderUpdateSelectDeleteContactComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ForwarderUpdateSelectDeleteContactComboBox_MouseClick);
            // 
            // forwarderUpdateContactDeleteButton
            // 
            this.forwarderUpdateContactDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.forwarderUpdateContactDeleteButton.Enabled = false;
            this.forwarderUpdateContactDeleteButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.forwarderUpdateContactDeleteButton.Location = new System.Drawing.Point(161, 100);
            this.forwarderUpdateContactDeleteButton.Name = "forwarderUpdateContactDeleteButton";
            this.forwarderUpdateContactDeleteButton.Size = new System.Drawing.Size(237, 30);
            this.forwarderUpdateContactDeleteButton.TabIndex = 66;
            this.forwarderUpdateContactDeleteButton.Text = "Видалити контакт";
            this.forwarderUpdateContactDeleteButton.UseVisualStyleBackColor = true;
            this.forwarderUpdateContactDeleteButton.Click += new System.EventHandler(this.DeleteForwarderContactButton_Click);
            // 
            // ForwarderContactDeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 142);
            this.Controls.Add(this.forwarderUpdateContactDeleteButton);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.forwarderUpdateSelectDeleteContactComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForwarderContactDeleteForm";
            this.Text = "Видалення контакту";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox forwarderUpdateSelectDeleteContactComboBox;
        private System.Windows.Forms.Button forwarderUpdateContactDeleteButton;
    }
}