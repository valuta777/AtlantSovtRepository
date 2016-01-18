namespace AtlantSovt
{
    partial class ExportTrackingToExcelForm
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
            this.exportTrackingFormLabel1 = new System.Windows.Forms.Label();
            this.exportTrackingButton = new System.Windows.Forms.Button();
            this.trackingExportSelectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.exportTrackingShowDataGridView = new System.Windows.Forms.DataGridView();
            this.trackingExportOnlyActive = new System.Windows.Forms.CheckBox();
            this.trackingExportDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.trackingExportSearchButton = new System.Windows.Forms.Button();
            this.trackingExportSearchTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.exportTrackingShowDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // exportTrackingFormLabel1
            // 
            this.exportTrackingFormLabel1.AutoSize = true;
            this.exportTrackingFormLabel1.Location = new System.Drawing.Point(0, 0);
            this.exportTrackingFormLabel1.Name = "exportTrackingFormLabel1";
            this.exportTrackingFormLabel1.Size = new System.Drawing.Size(60, 21);
            this.exportTrackingFormLabel1.TabIndex = 0;
            this.exportTrackingFormLabel1.Text = "Заявки";
            // 
            // exportTrackingButton
            // 
            this.exportTrackingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportTrackingButton.Location = new System.Drawing.Point(590, 496);
            this.exportTrackingButton.Name = "exportTrackingButton";
            this.exportTrackingButton.Size = new System.Drawing.Size(194, 65);
            this.exportTrackingButton.TabIndex = 20;
            this.exportTrackingButton.Text = "Продовжити >>";
            this.exportTrackingButton.UseVisualStyleBackColor = true;
            this.exportTrackingButton.Click += new System.EventHandler(this.exportTrackingButton_Click);
            // 
            // trackingExportSelectAllCheckBox
            // 
            this.trackingExportSelectAllCheckBox.AutoSize = true;
            this.trackingExportSelectAllCheckBox.Location = new System.Drawing.Point(672, 68);
            this.trackingExportSelectAllCheckBox.Name = "trackingExportSelectAllCheckBox";
            this.trackingExportSelectAllCheckBox.Size = new System.Drawing.Size(112, 25);
            this.trackingExportSelectAllCheckBox.TabIndex = 21;
            this.trackingExportSelectAllCheckBox.Text = "Вибрати всі";
            this.trackingExportSelectAllCheckBox.UseVisualStyleBackColor = true;
            this.trackingExportSelectAllCheckBox.CheckedChanged += new System.EventHandler(this.trackingExportSelectAllCheckBox_CheckedChanged);
            // 
            // exportTrackingShowDataGridView
            // 
            this.exportTrackingShowDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportTrackingShowDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.exportTrackingShowDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.exportTrackingShowDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.exportTrackingShowDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.exportTrackingShowDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.exportTrackingShowDataGridView.Location = new System.Drawing.Point(0, 99);
            this.exportTrackingShowDataGridView.MultiSelect = false;
            this.exportTrackingShowDataGridView.Name = "exportTrackingShowDataGridView";
            this.exportTrackingShowDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.exportTrackingShowDataGridView.Size = new System.Drawing.Size(784, 391);
            this.exportTrackingShowDataGridView.TabIndex = 22;
            this.exportTrackingShowDataGridView.TabStop = false;
            // 
            // trackingExportOnlyActive
            // 
            this.trackingExportOnlyActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackingExportOnlyActive.AutoSize = true;
            this.trackingExportOnlyActive.Location = new System.Drawing.Point(653, 27);
            this.trackingExportOnlyActive.Name = "trackingExportOnlyActive";
            this.trackingExportOnlyActive.Size = new System.Drawing.Size(131, 25);
            this.trackingExportOnlyActive.TabIndex = 57;
            this.trackingExportOnlyActive.Text = "Тільки активні";
            this.trackingExportOnlyActive.UseVisualStyleBackColor = true;
            this.trackingExportOnlyActive.CheckedChanged += new System.EventHandler(this.trackingExportOnlyActive_CheckedChanged);
            // 
            // trackingExportDateTimePicker
            // 
            this.trackingExportDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackingExportDateTimePicker.Checked = false;
            this.trackingExportDateTimePicker.CustomFormat = "MMMM yyyy";
            this.trackingExportDateTimePicker.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trackingExportDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.trackingExportDateTimePicker.Location = new System.Drawing.Point(459, 25);
            this.trackingExportDateTimePicker.Name = "trackingExportDateTimePicker";
            this.trackingExportDateTimePicker.ShowCheckBox = true;
            this.trackingExportDateTimePicker.Size = new System.Drawing.Size(151, 25);
            this.trackingExportDateTimePicker.TabIndex = 56;
            this.trackingExportDateTimePicker.ValueChanged += new System.EventHandler(this.trackingExportDateTimePicker_ValueChanged);
            // 
            // trackingExportSearchButton
            // 
            this.trackingExportSearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackingExportSearchButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trackingExportSearchButton.Location = new System.Drawing.Point(316, 23);
            this.trackingExportSearchButton.Name = "trackingExportSearchButton";
            this.trackingExportSearchButton.Size = new System.Drawing.Size(138, 30);
            this.trackingExportSearchButton.TabIndex = 55;
            this.trackingExportSearchButton.Text = "Знайти";
            this.trackingExportSearchButton.UseVisualStyleBackColor = true;
            this.trackingExportSearchButton.Click += new System.EventHandler(this.trackingExportSearchButton_Click);
            // 
            // trackingExportSearchTextBox
            // 
            this.trackingExportSearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackingExportSearchTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trackingExportSearchTextBox.Location = new System.Drawing.Point(4, 24);
            this.trackingExportSearchTextBox.Name = "trackingExportSearchTextBox";
            this.trackingExportSearchTextBox.Size = new System.Drawing.Size(306, 29);
            this.trackingExportSearchTextBox.TabIndex = 54;
            this.trackingExportSearchTextBox.TextChanged += new System.EventHandler(this.trackingExportSearchTextBox_TextChanged);
            this.trackingExportSearchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trackingExportSearchTextBox_KeyPress);
            // 
            // ExportTrackingToExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.trackingExportOnlyActive);
            this.Controls.Add(this.trackingExportDateTimePicker);
            this.Controls.Add(this.trackingExportSearchButton);
            this.Controls.Add(this.trackingExportSearchTextBox);
            this.Controls.Add(this.exportTrackingShowDataGridView);
            this.Controls.Add(this.trackingExportSelectAllCheckBox);
            this.Controls.Add(this.exportTrackingButton);
            this.Controls.Add(this.exportTrackingFormLabel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportTrackingToExcelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Експортування в Excel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExportTrackingToExcelForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.exportTrackingShowDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label exportTrackingFormLabel1;
        private System.Windows.Forms.Button exportTrackingButton;
        private System.Windows.Forms.CheckBox trackingExportSelectAllCheckBox;
        private System.Windows.Forms.DataGridView exportTrackingShowDataGridView;
        public System.Windows.Forms.CheckBox trackingExportOnlyActive;
        public System.Windows.Forms.DateTimePicker trackingExportDateTimePicker;
        private System.Windows.Forms.Button trackingExportSearchButton;
        public System.Windows.Forms.TextBox trackingExportSearchTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}