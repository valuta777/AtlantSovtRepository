namespace AtlantSovt
{
    partial class TransporterShowAdditionalDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransporterShowAdditionalDetailsForm));
            this.transporterShowAdditionalDetailsGridView = new System.Windows.Forms.DataGridView();
            this.transporterShowVehicleAdditionalDetailsGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowAdditionalDetailsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowVehicleAdditionalDetailsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // transporterShowAdditionalDetailsGridView
            // 
            this.transporterShowAdditionalDetailsGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.transporterShowAdditionalDetailsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.transporterShowAdditionalDetailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.transporterShowAdditionalDetailsGridView, "transporterShowAdditionalDetailsGridView");
            this.transporterShowAdditionalDetailsGridView.Name = "transporterShowAdditionalDetailsGridView";
            // 
            // transporterShowVehicleAdditionalDetailsGridView
            // 
            this.transporterShowVehicleAdditionalDetailsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.transporterShowVehicleAdditionalDetailsGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.transporterShowVehicleAdditionalDetailsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.transporterShowVehicleAdditionalDetailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.transporterShowVehicleAdditionalDetailsGridView, "transporterShowVehicleAdditionalDetailsGridView");
            this.transporterShowVehicleAdditionalDetailsGridView.Name = "transporterShowVehicleAdditionalDetailsGridView";
            // 
            // TransporterShowAdditionalDetailsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.transporterShowVehicleAdditionalDetailsGridView);
            this.Controls.Add(this.transporterShowAdditionalDetailsGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterShowAdditionalDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowAdditionalDetailsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowVehicleAdditionalDetailsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView transporterShowAdditionalDetailsGridView;
        private System.Windows.Forms.DataGridView transporterShowVehicleAdditionalDetailsGridView;

    }
}