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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportTrackingToExcelForm));
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
            resources.ApplyResources(this.exportTrackingFormLabel1, "exportTrackingFormLabel1");
            this.exportTrackingFormLabel1.Name = "exportTrackingFormLabel1";
            // 
            // exportTrackingButton
            // 
            resources.ApplyResources(this.exportTrackingButton, "exportTrackingButton");
            this.exportTrackingButton.Name = "exportTrackingButton";
            this.exportTrackingButton.UseVisualStyleBackColor = true;
            this.exportTrackingButton.Click += new System.EventHandler(this.exportTrackingButton_Click);
            // 
            // trackingExportSelectAllCheckBox
            // 
            resources.ApplyResources(this.trackingExportSelectAllCheckBox, "trackingExportSelectAllCheckBox");
            this.trackingExportSelectAllCheckBox.Name = "trackingExportSelectAllCheckBox";
            this.trackingExportSelectAllCheckBox.UseVisualStyleBackColor = true;
            this.trackingExportSelectAllCheckBox.CheckedChanged += new System.EventHandler(this.trackingExportSelectAllCheckBox_CheckedChanged);
            // 
            // exportTrackingShowDataGridView
            // 
            resources.ApplyResources(this.exportTrackingShowDataGridView, "exportTrackingShowDataGridView");
            this.exportTrackingShowDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.exportTrackingShowDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.exportTrackingShowDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.exportTrackingShowDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.exportTrackingShowDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.exportTrackingShowDataGridView.MultiSelect = false;
            this.exportTrackingShowDataGridView.Name = "exportTrackingShowDataGridView";
            this.exportTrackingShowDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.exportTrackingShowDataGridView.TabStop = false;
            // 
            // trackingExportOnlyActive
            // 
            resources.ApplyResources(this.trackingExportOnlyActive, "trackingExportOnlyActive");
            this.trackingExportOnlyActive.Name = "trackingExportOnlyActive";
            this.trackingExportOnlyActive.UseVisualStyleBackColor = true;
            this.trackingExportOnlyActive.CheckedChanged += new System.EventHandler(this.trackingExportOnlyActive_CheckedChanged);
            // 
            // trackingExportDateTimePicker
            // 
            resources.ApplyResources(this.trackingExportDateTimePicker, "trackingExportDateTimePicker");
            this.trackingExportDateTimePicker.Checked = false;
            this.trackingExportDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.trackingExportDateTimePicker.Name = "trackingExportDateTimePicker";
            this.trackingExportDateTimePicker.ShowCheckBox = true;
            this.trackingExportDateTimePicker.ValueChanged += new System.EventHandler(this.trackingExportDateTimePicker_ValueChanged);
            // 
            // trackingExportSearchButton
            // 
            resources.ApplyResources(this.trackingExportSearchButton, "trackingExportSearchButton");
            this.trackingExportSearchButton.Name = "trackingExportSearchButton";
            this.trackingExportSearchButton.UseVisualStyleBackColor = true;
            this.trackingExportSearchButton.Click += new System.EventHandler(this.trackingExportSearchButton_Click);
            // 
            // trackingExportSearchTextBox
            // 
            resources.ApplyResources(this.trackingExportSearchTextBox, "trackingExportSearchTextBox");
            this.trackingExportSearchTextBox.Name = "trackingExportSearchTextBox";
            this.trackingExportSearchTextBox.TextChanged += new System.EventHandler(this.trackingExportSearchTextBox_TextChanged);
            this.trackingExportSearchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trackingExportSearchTextBox_KeyPress);
            // 
            // ExportTrackingToExcelForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.trackingExportOnlyActive);
            this.Controls.Add(this.trackingExportDateTimePicker);
            this.Controls.Add(this.trackingExportSearchButton);
            this.Controls.Add(this.trackingExportSearchTextBox);
            this.Controls.Add(this.exportTrackingShowDataGridView);
            this.Controls.Add(this.trackingExportSelectAllCheckBox);
            this.Controls.Add(this.exportTrackingButton);
            this.Controls.Add(this.exportTrackingFormLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportTrackingToExcelForm";
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