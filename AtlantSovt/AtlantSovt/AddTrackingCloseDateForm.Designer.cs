namespace AtlantSovt
{
    partial class AddTrackingCloseDateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTrackingCloseDateForm));
            this.addCloseDateButton = new System.Windows.Forms.Button();
            this.closeDateCalendar = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // addCloseDateButton
            // 
            resources.ApplyResources(this.addCloseDateButton, "addCloseDateButton");
            this.addCloseDateButton.Name = "addCloseDateButton";
            this.addCloseDateButton.UseVisualStyleBackColor = true;
            this.addCloseDateButton.Click += new System.EventHandler(this.addCloseDateButton_Click);
            // 
            // closeDateCalendar
            // 
            resources.ApplyResources(this.closeDateCalendar, "closeDateCalendar");
            this.closeDateCalendar.Name = "closeDateCalendar";
            // 
            // AddTrackingCloseDateForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.closeDateCalendar);
            this.Controls.Add(this.addCloseDateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTrackingCloseDateForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddTrackingCloseDateForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addCloseDateButton;
        private System.Windows.Forms.MonthCalendar closeDateCalendar;
    }
}