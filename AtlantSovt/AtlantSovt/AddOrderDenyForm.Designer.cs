namespace AtlantSovt
{
    partial class AddOrderDenyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOrderDenyForm));
            this.addOrderDenyButton = new System.Windows.Forms.Button();
            this.addOrderDenyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addOrderDenyButton
            // 
            resources.ApplyResources(this.addOrderDenyButton, "addOrderDenyButton");
            this.addOrderDenyButton.Name = "addOrderDenyButton";
            this.addOrderDenyButton.UseVisualStyleBackColor = true;
            this.addOrderDenyButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addOrderDenyTextBox
            // 
            resources.ApplyResources(this.addOrderDenyTextBox, "addOrderDenyTextBox");
            this.addOrderDenyTextBox.Name = "addOrderDenyTextBox";
            // 
            // AddOrderDenyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addOrderDenyButton);
            this.Controls.Add(this.addOrderDenyTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOrderDenyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addOrderDenyButton;
        private System.Windows.Forms.TextBox addOrderDenyTextBox;
    }
}