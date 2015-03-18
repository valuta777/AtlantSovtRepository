namespace AtlantSovt
{
    partial class TransporterShowFiltrationForm
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
            this.transporterShowFiltersSelectCountryCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterShowFilterButton = new System.Windows.Forms.Button();
            this.transporterShowFiltersSelectVehicleCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // transporterShowFiltersSelectCountryCheckedListBox
            // 
            this.transporterShowFiltersSelectCountryCheckedListBox.ColumnWidth = 200;
            this.transporterShowFiltersSelectCountryCheckedListBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transporterShowFiltersSelectCountryCheckedListBox.FormattingEnabled = true;
            this.transporterShowFiltersSelectCountryCheckedListBox.Location = new System.Drawing.Point(0, 0);
            this.transporterShowFiltersSelectCountryCheckedListBox.MultiColumn = true;
            this.transporterShowFiltersSelectCountryCheckedListBox.Name = "transporterShowFiltersSelectCountryCheckedListBox";
            this.transporterShowFiltersSelectCountryCheckedListBox.Size = new System.Drawing.Size(801, 148);
            this.transporterShowFiltersSelectCountryCheckedListBox.TabIndex = 0;
            // 
            // transporterShowFilterButton
            // 
            this.transporterShowFilterButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterShowFilterButton.Location = new System.Drawing.Point(559, 307);
            this.transporterShowFilterButton.Name = "transporterShowFilterButton";
            this.transporterShowFilterButton.Size = new System.Drawing.Size(239, 70);
            this.transporterShowFilterButton.TabIndex = 1;
            this.transporterShowFilterButton.Text = "Фільтрувати";
            this.transporterShowFilterButton.UseVisualStyleBackColor = true;
            this.transporterShowFilterButton.Click += new System.EventHandler(this.transporterShowFilterButton_Click);
            // 
            // transporterShowFiltersSelectVehicleCheckedListBox
            // 
            this.transporterShowFiltersSelectVehicleCheckedListBox.ColumnWidth = 200;
            this.transporterShowFiltersSelectVehicleCheckedListBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transporterShowFiltersSelectVehicleCheckedListBox.FormattingEnabled = true;
            this.transporterShowFiltersSelectVehicleCheckedListBox.Location = new System.Drawing.Point(0, 153);
            this.transporterShowFiltersSelectVehicleCheckedListBox.MultiColumn = true;
            this.transporterShowFiltersSelectVehicleCheckedListBox.Name = "transporterShowFiltersSelectVehicleCheckedListBox";
            this.transporterShowFiltersSelectVehicleCheckedListBox.Size = new System.Drawing.Size(801, 148);
            this.transporterShowFiltersSelectVehicleCheckedListBox.TabIndex = 2;
            // 
            // TransporterShowFiltrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 380);
            this.Controls.Add(this.transporterShowFiltersSelectVehicleCheckedListBox);
            this.Controls.Add(this.transporterShowFilterButton);
            this.Controls.Add(this.transporterShowFiltersSelectCountryCheckedListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterShowFiltrationForm";
            this.Text = " Фільтрація";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox transporterShowFiltersSelectCountryCheckedListBox;
        private System.Windows.Forms.Button transporterShowFilterButton;
        private System.Windows.Forms.CheckedListBox transporterShowFiltersSelectVehicleCheckedListBox;
    }
}