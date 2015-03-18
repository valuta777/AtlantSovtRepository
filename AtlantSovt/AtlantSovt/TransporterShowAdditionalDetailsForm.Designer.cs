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
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowAdditionalDetailsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // transporterShowAdditionalDetailsGridView
            // 
            this.transporterShowAdditionalDetailsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transporterShowAdditionalDetailsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.transporterShowAdditionalDetailsGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.transporterShowAdditionalDetailsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.transporterShowAdditionalDetailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transporterShowAdditionalDetailsGridView.Location = new System.Drawing.Point(0, 0);
            this.transporterShowAdditionalDetailsGridView.Name = "transporterShowAdditionalDetailsGridView";
            this.transporterShowAdditionalDetailsGridView.Size = new System.Drawing.Size(636, 112);
            this.transporterShowAdditionalDetailsGridView.TabIndex = 20;
            // 
            // TransporterShowAdditionalDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 111);
            this.Controls.Add(this.transporterShowAdditionalDetailsGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterShowAdditionalDetailsForm";
            this.Text = "Додаткові параметри";
            ((System.ComponentModel.ISupportInitialize)(this.transporterShowAdditionalDetailsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView transporterShowAdditionalDetailsGridView;

    }
}