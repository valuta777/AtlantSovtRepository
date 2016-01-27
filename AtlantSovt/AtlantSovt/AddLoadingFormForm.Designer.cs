namespace AtlantSovt
{
    partial class AddLoadingFormForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLoadingFormForm));
            this.addLoadingFormButton = new System.Windows.Forms.Button();
            this.addLoadingFormTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addLoadingFormButton
            // 
            resources.ApplyResources(this.addLoadingFormButton, "addLoadingFormButton");
            this.addLoadingFormButton.Name = "addLoadingFormButton";
            this.addLoadingFormButton.UseVisualStyleBackColor = true;
            this.addLoadingFormButton.Click += new System.EventHandler(this.addLoadingFormButton_Click);
            // 
            // addLoadingFormTextBox
            // 
            resources.ApplyResources(this.addLoadingFormTextBox, "addLoadingFormTextBox");
            this.addLoadingFormTextBox.Name = "addLoadingFormTextBox";
            // 
            // AddLoadingFormForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addLoadingFormButton);
            this.Controls.Add(this.addLoadingFormTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLoadingFormForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addLoadingFormButton;
        private System.Windows.Forms.TextBox addLoadingFormTextBox;
    }
}