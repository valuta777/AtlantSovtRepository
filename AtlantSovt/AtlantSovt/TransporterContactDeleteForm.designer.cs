namespace AtlantSovt
{
    partial class TransporterContactDeleteForm
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
            this.label27dt = new System.Windows.Forms.Label();
            this.transporterUpdateSelectDeleteContactComboBox = new System.Windows.Forms.ComboBox();
            this.transporterUpdateContactDeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label27dt
            // 
            this.label27dt.AutoSize = true;
            this.label27dt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27dt.Location = new System.Drawing.Point(12, 9);
            this.label27dt.Name = "label27dt";
            this.label27dt.Size = new System.Drawing.Size(133, 21);
            this.label27dt.TabIndex = 65;
            this.label27dt.Text = "Виберіть контакт";
            // 
            // transporterUpdateSelectDeleteContactComboBox
            // 
            this.transporterUpdateSelectDeleteContactComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transporterUpdateSelectDeleteContactComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.transporterUpdateSelectDeleteContactComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.transporterUpdateSelectDeleteContactComboBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterUpdateSelectDeleteContactComboBox.Location = new System.Drawing.Point(10, 33);
            this.transporterUpdateSelectDeleteContactComboBox.Name = "transporterUpdateSelectDeleteContactComboBox";
            this.transporterUpdateSelectDeleteContactComboBox.Size = new System.Drawing.Size(562, 29);
            this.transporterUpdateSelectDeleteContactComboBox.Sorted = true;
            this.transporterUpdateSelectDeleteContactComboBox.TabIndex = 64;
            this.transporterUpdateSelectDeleteContactComboBox.SelectedIndexChanged += new System.EventHandler(this.TransporterUpdateSelectDeleteContactComboBox_SelectedIndexChanged);
            this.transporterUpdateSelectDeleteContactComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TransporterUpdateSelectDeleteContactComboBox_MouseClick);
            // 
            // transporterUpdateContactDeleteButton
            // 
            this.transporterUpdateContactDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.transporterUpdateContactDeleteButton.Enabled = false;
            this.transporterUpdateContactDeleteButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterUpdateContactDeleteButton.Location = new System.Drawing.Point(172, 77);
            this.transporterUpdateContactDeleteButton.Name = "transporterUpdateContactDeleteButton";
            this.transporterUpdateContactDeleteButton.Size = new System.Drawing.Size(237, 53);
            this.transporterUpdateContactDeleteButton.TabIndex = 66;
            this.transporterUpdateContactDeleteButton.Text = "Видалити контакт";
            this.transporterUpdateContactDeleteButton.UseVisualStyleBackColor = true;
            this.transporterUpdateContactDeleteButton.Click += new System.EventHandler(this.DeleteTransporterContactButton_Click);
            // 
            // TransporterContactDeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 142);
            this.Controls.Add(this.transporterUpdateContactDeleteButton);
            this.Controls.Add(this.label27dt);
            this.Controls.Add(this.transporterUpdateSelectDeleteContactComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterContactDeleteForm";
            this.Text = "Видалення контакту";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27dt;
        private System.Windows.Forms.ComboBox transporterUpdateSelectDeleteContactComboBox;
        private System.Windows.Forms.Button transporterUpdateContactDeleteButton;
    }
}