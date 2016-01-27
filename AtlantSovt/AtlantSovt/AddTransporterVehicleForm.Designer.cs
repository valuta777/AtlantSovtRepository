namespace AtlantSovt
{
    partial class AddTransporterVehicleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTransporterVehicleForm));
            this.addVehicleButton = new System.Windows.Forms.Button();
            this.addTransporterVehicleTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addVehicleButton
            // 
            resources.ApplyResources(this.addVehicleButton, "addVehicleButton");
            this.addVehicleButton.Name = "addVehicleButton";
            this.addVehicleButton.UseVisualStyleBackColor = true;
            this.addVehicleButton.Click += new System.EventHandler(this.addVehicleButton_Click);
            // 
            // addTransporterVehicleTextBox
            // 
            resources.ApplyResources(this.addTransporterVehicleTextBox, "addTransporterVehicleTextBox");
            this.addTransporterVehicleTextBox.Name = "addTransporterVehicleTextBox";
            // 
            // AddTransporterVehicleForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addVehicleButton);
            this.Controls.Add(this.addTransporterVehicleTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTransporterVehicleForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addVehicleButton;
        private System.Windows.Forms.TextBox addTransporterVehicleTextBox;
    }
}