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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransporterCountryUpdateVehicleSelectForm));
            this.transporterUpdateFilterSelectCountryCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterUpdateFilterSelectVehicleCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterUpdateFilterSelectButton = new System.Windows.Forms.Button();
            this.transporterAddVehicleButton = new System.Windows.Forms.Button();
            this.transporterAddCountryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // transporterUpdateFilterSelectCountryCheckedListBox
            // 
            resources.ApplyResources(this.transporterUpdateFilterSelectCountryCheckedListBox, "transporterUpdateFilterSelectCountryCheckedListBox");
            this.transporterUpdateFilterSelectCountryCheckedListBox.CheckOnClick = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.FormattingEnabled = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.MultiColumn = true;
            this.transporterUpdateFilterSelectCountryCheckedListBox.Name = "transporterUpdateFilterSelectCountryCheckedListBox";
            // 
            // transporterUpdateFilterSelectVehicleCheckedListBox
            // 
            resources.ApplyResources(this.transporterUpdateFilterSelectVehicleCheckedListBox, "transporterUpdateFilterSelectVehicleCheckedListBox");
            this.transporterUpdateFilterSelectVehicleCheckedListBox.CheckOnClick = true;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.FormattingEnabled = true;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.MultiColumn = true;
            this.transporterUpdateFilterSelectVehicleCheckedListBox.Name = "transporterUpdateFilterSelectVehicleCheckedListBox";
            // 
            // transporterUpdateFilterSelectButton
            // 
            resources.ApplyResources(this.transporterUpdateFilterSelectButton, "transporterUpdateFilterSelectButton");
            this.transporterUpdateFilterSelectButton.Name = "transporterUpdateFilterSelectButton";
            this.transporterUpdateFilterSelectButton.UseVisualStyleBackColor = true;
            this.transporterUpdateFilterSelectButton.Click += new System.EventHandler(this.transporterFilterSelectButton_Click);
            // 
            // transporterAddVehicleButton
            // 
            resources.ApplyResources(this.transporterAddVehicleButton, "transporterAddVehicleButton");
            this.transporterAddVehicleButton.Name = "transporterAddVehicleButton";
            this.transporterAddVehicleButton.UseVisualStyleBackColor = true;
            this.transporterAddVehicleButton.Click += new System.EventHandler(this.transporterAddVehicleButton_Click);
            // 
            // transporterAddCountryButton
            // 
            resources.ApplyResources(this.transporterAddCountryButton, "transporterAddCountryButton");
            this.transporterAddCountryButton.Name = "transporterAddCountryButton";
            this.transporterAddCountryButton.UseVisualStyleBackColor = true;
            this.transporterAddCountryButton.Click += new System.EventHandler(this.transporterAddCountryButton_Click);
            // 
            // TransporterCountryUpdateVehicleSelectForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.transporterAddVehicleButton);
            this.Controls.Add(this.transporterAddCountryButton);
            this.Controls.Add(this.transporterUpdateFilterSelectButton);
            this.Controls.Add(this.transporterUpdateFilterSelectCountryCheckedListBox);
            this.Controls.Add(this.transporterUpdateFilterSelectVehicleCheckedListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterCountryUpdateVehicleSelectForm";
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