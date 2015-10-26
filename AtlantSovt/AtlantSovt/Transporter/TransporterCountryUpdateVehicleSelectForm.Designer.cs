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
            this.transporterAddVehicleButton = new System.Windows.Forms.Button();
            this.transporterAddCountryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // transporterUpdateFilterSelectCountryCheckedListBox
            // 
            this.transporterUpdateFilterSelectCountryCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transporterUpdateFilterSelectCountryCheckedListBox.CheckOnClick = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterUpdateFilterSelectCountryCheckedListBox.FormattingEnabled = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.Location = new System.Drawing.Point(0, 0);
            this.transporterUpdateFilterSelectCountryCheckedListBox.MultiColumn = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.Name = "transporterUpdateFilterSelectCountryCheckedListBox";
            this.transporterUpdateFilterSelectCountryCheckedListBox.Size = new System.Drawing.Size(701, 148);
            this.transporterUpdateFilterSelectCountryCheckedListBox.TabIndex = 103;
            this.transporterUpdateFilterSelectCountryCheckedListBox.DoubleClick += new System.EventHandler(this.transporterUpdateFilterSelectCountryCheckedListBox_DoubleClick);
            // 
            // transporterUpdateFilterSelectVehicleCheckedListBox
            // 
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transporterUpdateFilterSelectVehicleCheckedListBox.CheckOnClick = true;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterUpdateFilterSelectVehicleCheckedListBox.FormattingEnabled = true;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.HorizontalExtent = 3;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Location = new System.Drawing.Point(0, 152);
            this.transporterUpdateFilterSelectVehicleCheckedListBox.MultiColumn = true;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Name = "transporterUpdateFilterSelectVehicleCheckedListBox";
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Size = new System.Drawing.Size(701, 148);
            this.transporterUpdateFilterSelectVehicleCheckedListBox.TabIndex = 104;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.DoubleClick += new System.EventHandler(this.transporterUpdateFilterSelectVehicleCheckedListBox_DoubleClick);
            // 
            // transporterUpdateFilterSelectButton
            // 
            this.transporterUpdateFilterSelectButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.transporterUpdateFilterSelectButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterUpdateFilterSelectButton.Location = new System.Drawing.Point(0, 303);
            this.transporterUpdateFilterSelectButton.Name = "transporterUpdateFilterSelectButton";
            this.transporterUpdateFilterSelectButton.Size = new System.Drawing.Size(801, 77);
            this.transporterUpdateFilterSelectButton.TabIndex = 105;
            this.transporterUpdateFilterSelectButton.Text = "Додати";
            this.transporterUpdateFilterSelectButton.UseVisualStyleBackColor = true;
            this.transporterUpdateFilterSelectButton.Click += new System.EventHandler(this.transporterFilterSelectButton_Click);
            // 
            // transporterAddVehicleButton
            // 
            this.transporterAddVehicleButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterAddVehicleButton.Location = new System.Drawing.Point(701, 152);
            this.transporterAddVehicleButton.Name = "transporterAddVehicleButton";
            this.transporterAddVehicleButton.Size = new System.Drawing.Size(100, 148);
            this.transporterAddVehicleButton.TabIndex = 109;
            this.transporterAddVehicleButton.Text = "Додати нові типи транспорту";
            this.transporterAddVehicleButton.UseVisualStyleBackColor = true;
            this.transporterAddVehicleButton.Click += new System.EventHandler(this.transporterAddVehicleButton_Click);
            // 
            // transporterAddCountryButton
            // 
            this.transporterAddCountryButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transporterAddCountryButton.Location = new System.Drawing.Point(701, 0);
            this.transporterAddCountryButton.Name = "transporterAddCountryButton";
            this.transporterAddCountryButton.Size = new System.Drawing.Size(100, 148);
            this.transporterAddCountryButton.TabIndex = 108;
            this.transporterAddCountryButton.Text = "Додати нові країни";
            this.transporterAddCountryButton.UseVisualStyleBackColor = true;
            this.transporterAddCountryButton.Click += new System.EventHandler(this.transporterAddCountryButton_Click);
            // 
            // TransporterCountryUpdateVehicleSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(801, 380);
            this.Controls.Add(this.transporterAddVehicleButton);
            this.Controls.Add(this.transporterAddCountryButton);
            this.Controls.Add(this.transporterUpdateFilterSelectButton);
            this.Controls.Add(this.transporterUpdateFilterSelectCountryCheckedListBox);
            this.Controls.Add(this.transporterUpdateFilterSelectVehicleCheckedListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterCountryUpdateVehicleSelectForm";
            this.Text = "Вибір країн і типів транспорту";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox transporterUpdateFilterSelectCountryCheckedListBox;
        private System.Windows.Forms.CheckedListBox transporterUpdateFilterSelectVehicleCheckedListBox;
        private System.Windows.Forms.Button transporterUpdateFilterSelectButton;
        private System.Windows.Forms.Button transporterAddVehicleButton;
        private System.Windows.Forms.Button transporterAddCountryButton;
    }
}