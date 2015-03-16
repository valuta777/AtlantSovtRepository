namespace AtlantSovt
{
    partial class TransporterCoutryAndVehicleSelectForm
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
            this.transporterFilterSelectCountryCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterFilterSelectVehicleCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterFilterSelectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // transporterFilterSelectCountryCheckedListBox
            // 
            this.transporterFilterSelectCountryCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transporterFilterSelectCountryCheckedListBox.CheckOnClick = true;
            this.transporterFilterSelectCountryCheckedListBox.FormattingEnabled = true;
            this.transporterFilterSelectCountryCheckedListBox.Location = new System.Drawing.Point(12, 12);
            this.transporterFilterSelectCountryCheckedListBox.MultiColumn = true;
            this.transporterFilterSelectCountryCheckedListBox.Name = "transporterFilterSelectCountryCheckedListBox";
            this.transporterFilterSelectCountryCheckedListBox.Size = new System.Drawing.Size(634, 109);
            this.transporterFilterSelectCountryCheckedListBox.TabIndex = 103;
            this.transporterFilterSelectCountryCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.transporterFilterSelectCountryCheckedListBox_SelectedIndexChanged);
            // 
            // transporterFilterSelectVehicleCheckedListBox
            // 
            this.transporterFilterSelectVehicleCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transporterFilterSelectVehicleCheckedListBox.FormattingEnabled = true;
            this.transporterFilterSelectVehicleCheckedListBox.HorizontalExtent = 3;
            this.transporterFilterSelectVehicleCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.transporterFilterSelectVehicleCheckedListBox.Location = new System.Drawing.Point(12, 129);
            this.transporterFilterSelectVehicleCheckedListBox.MultiColumn = true;
            this.transporterFilterSelectVehicleCheckedListBox.Name = "transporterFilterSelectVehicleCheckedListBox";
            this.transporterFilterSelectVehicleCheckedListBox.Size = new System.Drawing.Size(634, 109);
            this.transporterFilterSelectVehicleCheckedListBox.TabIndex = 104;
            // 
            // transporterFilterSelectButton
            // 
            this.transporterFilterSelectButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterFilterSelectButton.Location = new System.Drawing.Point(12, 248);
            this.transporterFilterSelectButton.Name = "transporterFilterSelectButton";
            this.transporterFilterSelectButton.Size = new System.Drawing.Size(634, 48);
            this.transporterFilterSelectButton.TabIndex = 105;
            this.transporterFilterSelectButton.Text = "Додати";
            this.transporterFilterSelectButton.UseVisualStyleBackColor = true;
            this.transporterFilterSelectButton.Click += new System.EventHandler(this.transporterFilterSelectButton_Click);
            // 
            // TransporterCoutryAndVehicleSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 308);
            this.Controls.Add(this.transporterFilterSelectButton);
            this.Controls.Add(this.transporterFilterSelectCountryCheckedListBox);
            this.Controls.Add(this.transporterFilterSelectVehicleCheckedListBox);
            this.Name = "TransporterCoutryAndVehicleSelectForm";
            this.Text = "Вибір країн і типів транспорту";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox transporterFilterSelectCountryCheckedListBox;
        private System.Windows.Forms.CheckedListBox transporterFilterSelectVehicleCheckedListBox;
        private System.Windows.Forms.Button transporterFilterSelectButton;
    }
}