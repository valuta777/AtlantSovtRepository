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
            this.transporterShowAdditionalDetailsGridView = new System.Windows.Forms.DataGridView();
            this.transporterShowVehicleAdditionalDetailsGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowAdditionalDetailsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowVehicleAdditionalDetailsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // transporterShowAdditionalDetailsGridView
            // 
            this.transporterShowAdditionalDetailsGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.transporterShowAdditionalDetailsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.transporterShowAdditionalDetailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transporterShowAdditionalDetailsGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.transporterShowAdditionalDetailsGridView.Location = new System.Drawing.Point(0, 0);
            this.transporterShowAdditionalDetailsGridView.Name = "transporterShowAdditionalDetailsGridView";
            this.transporterShowAdditionalDetailsGridView.Size = new System.Drawing.Size(645, 105);
            this.transporterShowAdditionalDetailsGridView.TabIndex = 20;
            // 
            // transporterShowVehicleAdditionalDetailsGridView
            // 
            this.transporterShowVehicleAdditionalDetailsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.transporterShowVehicleAdditionalDetailsGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.transporterShowVehicleAdditionalDetailsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.transporterShowVehicleAdditionalDetailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transporterShowVehicleAdditionalDetailsGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.transporterShowVehicleAdditionalDetailsGridView.Location = new System.Drawing.Point(0, 114);
            this.transporterShowVehicleAdditionalDetailsGridView.Name = "transporterShowVehicleAdditionalDetailsGridView";
            this.transporterShowVehicleAdditionalDetailsGridView.Size = new System.Drawing.Size(645, 105);
            this.transporterShowVehicleAdditionalDetailsGridView.TabIndex = 21;
            // 
            // TransporterShowAdditionalDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 219);
            this.Controls.Add(this.transporterShowVehicleAdditionalDetailsGridView);
            this.Controls.Add(this.transporterShowAdditionalDetailsGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterShowAdditionalDetailsForm";
            this.Text = "Додаткові параметри";
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowAdditionalDetailsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowVehicleAdditionalDetailsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView transporterShowAdditionalDetailsGridView;
        private System.Windows.Forms.DataGridView transporterShowVehicleAdditionalDetailsGridView;

    }
}