namespace AtlantSovt
{
    partial class AddStaffForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStaffForm));
            this.addStaffButton = new System.Windows.Forms.Button();
            this.addStaffTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addStaffButton
            // 
            resources.ApplyResources(this.addStaffButton, "addStaffButton");
            this.addStaffButton.Name = "addStaffButton";
            this.addStaffButton.UseVisualStyleBackColor = true;
            this.addStaffButton.Click += new System.EventHandler(this.addStaffButton_Click);
            // 
            // addStaffTextBox
            // 
            resources.ApplyResources(this.addStaffTextBox, "addStaffTextBox");
            this.addStaffTextBox.Name = "addStaffTextBox";
            // 
            // AddStaffForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addStaffButton);
            this.Controls.Add(this.addStaffTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddStaffForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addStaffButton;
        private System.Windows.Forms.TextBox addStaffTextBox;
    }
}