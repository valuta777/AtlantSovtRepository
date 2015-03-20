namespace AtlantSovt
{
    partial class TransporterCountryUpdateVehicleSelectForm
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
            this.transporterUpdateFilterSelectCountryCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterUpdateFilterSelectVehicleCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterUpdateFilterSelectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // transporterFilterSelectCountryCheckedListBox
            // 
            this.transporterUpdateFilterSelectCountryCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transporterUpdateFilterSelectCountryCheckedListBox.CheckOnClick = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.FormattingEnabled = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.Location = new System.Drawing.Point(12, 12);
            this.transporterUpdateFilterSelectCountryCheckedListBox.MultiColumn = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.Name = "transporterFilterSelectCountryCheckedListBox";
            this.transporterUpdateFilterSelectCountryCheckedListBox.Size = new System.Drawing.Size(634, 109);
            this.transporterUpdateFilterSelectCountryCheckedListBox.TabIndex = 103;
             // 
            // transporterFilterSelectVehicleCheckedListBox
            // 
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transporterUpdateFilterSelectVehicleCheckedListBox.FormattingEnabled = true;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.HorizontalExtent = 3;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Location = new System.Drawing.Point(12, 129);
            this.transporterUpdateFilterSelectVehicleCheckedListBox.MultiColumn = true;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Name = "transporterFilterSelectVehicleCheckedListBox";
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Size = new System.Drawing.Size(634, 109);
            this.transporterUpdateFilterSelectVehicleCheckedListBox.TabIndex = 104;
            // 
            // transporterFilterSelectButton
            // 
            this.transporterUpdateFilterSelectButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterUpdateFilterSelectButton.Location = new System.Drawing.Point(12, 248);
            this.transporterUpdateFilterSelectButton.Name = "transporterFilterSelectButton";
            this.transporterUpdateFilterSelectButton.Size = new System.Drawing.Size(634, 48);
            this.transporterUpdateFilterSelectButton.TabIndex = 105;
            this.transporterUpdateFilterSelectButton.Text = "Додати";
            this.transporterUpdateFilterSelectButton.UseVisualStyleBackColor = true;
            this.transporterUpdateFilterSelectButton.Click += new System.EventHandler(this.transporterFilterSelectButton_Click);
            // 
            // TransporterCoutryAndVehicleSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 308);
            this.Controls.Add(this.transporterUpdateFilterSelectButton);
            this.Controls.Add(this.transporterUpdateFilterSelectCountryCheckedListBox);
            this.Controls.Add(this.transporterUpdateFilterSelectVehicleCheckedListBox);
            this.Name = "TransporterCoutryAndVehicleSelectForm";
            this.Text = "Вибір країн і типів транспорту";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox transporterUpdateFilterSelectCountryCheckedListBox;
        private System.Windows.Forms.CheckedListBox transporterUpdateFilterSelectVehicleCheckedListBox;
        private System.Windows.Forms.Button transporterUpdateFilterSelectButton;
    }
}