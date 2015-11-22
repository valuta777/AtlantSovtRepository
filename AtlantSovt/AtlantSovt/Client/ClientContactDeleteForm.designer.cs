namespace AtlantSovt
{
    partial class ClientContactDeleteForm
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
            this.ClientUpdateSelectDeleteContactComboBox = new System.Windows.Forms.ComboBox();
            this.DeleteClientContactButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.Location = new System.Drawing.Point(0, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(133, 21);
            this.label27.TabIndex = 65;
            this.label27.Text = "Виберіть контакт";
            // 
            // ClientUpdateSelectDeleteContactComboBox
            // 
            this.ClientUpdateSelectDeleteContactComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientUpdateSelectDeleteContactComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ClientUpdateSelectDeleteContactComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ClientUpdateSelectDeleteContactComboBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientUpdateSelectDeleteContactComboBox.Location = new System.Drawing.Point(0, 24);
            this.ClientUpdateSelectDeleteContactComboBox.Name = "ClientUpdateSelectDeleteContactComboBox";
            this.ClientUpdateSelectDeleteContactComboBox.Size = new System.Drawing.Size(499, 29);
            this.ClientUpdateSelectDeleteContactComboBox.Sorted = true;
            this.ClientUpdateSelectDeleteContactComboBox.TabIndex = 64;
            this.ClientUpdateSelectDeleteContactComboBox.SelectedIndexChanged += new System.EventHandler(this.ClientUpdateSelectDeleteContactComboBox_SelectedIndexChanged);
            this.ClientUpdateSelectDeleteContactComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClientUpdateSelectDeleteContactComboBox_MouseClick);
            // 
            // DeleteClientContactButton
            // 
            this.DeleteClientContactButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DeleteClientContactButton.Enabled = false;
            this.DeleteClientContactButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteClientContactButton.Location = new System.Drawing.Point(0, 52);
            this.DeleteClientContactButton.Name = "DeleteClientContactButton";
            this.DeleteClientContactButton.Size = new System.Drawing.Size(499, 52);
            this.DeleteClientContactButton.TabIndex = 66;
            this.DeleteClientContactButton.Text = "Видалити контакт";
            this.DeleteClientContactButton.UseVisualStyleBackColor = true;
            this.DeleteClientContactButton.Click += new System.EventHandler(this.DeleteClientContactButton_Click);
            // 
            // ClientContactDeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 104);
            this.Controls.Add(this.DeleteClientContactButton);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.ClientUpdateSelectDeleteContactComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientContactDeleteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Видалення контакту";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox ClientUpdateSelectDeleteContactComboBox;
        private System.Windows.Forms.Button DeleteClientContactButton;
    }
}