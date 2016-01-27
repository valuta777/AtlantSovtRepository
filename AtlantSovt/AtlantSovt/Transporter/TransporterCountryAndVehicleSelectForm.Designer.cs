namespace AtlantSovt
{
    partial class TransporterCountryAndVehicleSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransporterCountryAndVehicleSelectForm));
            this.transporterFilterSelectCountryCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterFilterSelectVehicleCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.transporterFilterSelectButton = new System.Windows.Forms.Button();
            this.transporterAddCountryButton = new System.Windows.Forms.Button();
            this.transporterAddVehicleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // transporterFilterSelectCountryCheckedListBox
            // 
            resources.ApplyResources(this.transporterFilterSelectCountryCheckedListBox, "transporterFilterSelectCountryCheckedListBox");
            this.transporterFilterSelectCountryCheckedListBox.CheckOnClick = true;
            this.transporterFilterSelectCountryCheckedListBox.FormattingEnabled = true;
            this.transporterFilterSelectCountryCheckedListBox.MultiColumn = true;
            this.transporterFilterSelectCountryCheckedListBox.Name = "transporterFilterSelectCountryCheckedListBox";
            // 
            // transporterFilterSelectVehicleCheckedListBox
            // 
            resources.ApplyResources(this.transporterFilterSelectVehicleCheckedListBox, "transporterFilterSelectVehicleCheckedListBox");
            this.transporterFilterSelectVehicleCheckedListBox.CheckOnClick = true;
            this.transporterFilterSelectVehicleCheckedListBox.FormattingEnabled = true;
            this.transporterFilterSelectVehicleCheckedListBox.MultiColumn = true;
            this.transporterFilterSelectVehicleCheckedListBox.Name = "transporterFilterSelectVehicleCheckedListBox";
            // 
            // transporterFilterSelectButton
            // 
            resources.ApplyResources(this.transporterFilterSelectButton, "transporterFilterSelectButton");
            this.transporterFilterSelectButton.Name = "transporterFilterSelectButton";
            this.transporterFilterSelectButton.UseVisualStyleBackColor = true;
            this.transporterFilterSelectButton.Click += new System.EventHandler(this.transporterFilterSelectButton_Click);
            // 
            // transporterAddCountryButton
            // 
            resources.ApplyResources(this.transporterAddCountryButton, "transporterAddCountryButton");
            this.transporterAddCountryButton.Name = "transporterAddCountryButton";
            this.transporterAddCountryButton.UseVisualStyleBackColor = true;
            this.transporterAddCountryButton.Click += new System.EventHandler(this.transporterAddCountryButton_Click);
            // 
            // transporterAddVehicleButton
            // 
            resources.ApplyResources(this.transporterAddVehicleButton, "transporterAddVehicleButton");
            this.transporterAddVehicleButton.Name = "transporterAddVehicleButton";
            this.transporterAddVehicleButton.UseVisualStyleBackColor = true;
            this.transporterAddVehicleButton.Click += new System.EventHandler(this.transporterAddVehicleButton_Click);
            // 
            // TransporterCountryAndVehicleSelectForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.transporterAddVehicleButton);
            this.Controls.Add(this.transporterAddCountryButton);
            this.Controls.Add(this.transporterFilterSelectButton);
            this.Controls.Add(this.transporterFilterSelectCountryCheckedListBox);
            this.Controls.Add(this.transporterFilterSelectVehicleCheckedListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterCountryAndVehicleSelectForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TransporterCountryAndVehicleSelectForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox transporterFilterSelectCountryCheckedListBox;
        private System.Windows.Forms.CheckedListBox transporterFilterSelectVehicleCheckedListBox;
        private System.Windows.Forms.Button transporterFilterSelectButton;
        private System.Windows.Forms.Button transporterAddCountryButton;
        private System.Windows.Forms.Button transporterAddVehicleButton;
    }
}