namespace AtlantSovt
{
    partial class AddCountryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCountryForm));
            this.addCountryButton = new System.Windows.Forms.Button();
            this.addCountryTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addCountryButton
            // 
            resources.ApplyResources(this.addCountryButton, "addCountryButton");
            this.addCountryButton.Name = "addCountryButton";
            this.addCountryButton.UseVisualStyleBackColor = true;
            this.addCountryButton.Click += new System.EventHandler(this.addCountryButton_Click);
            // 
            // addCountryTextBox
            // 
            resources.ApplyResources(this.addCountryTextBox, "addCountryTextBox");
            this.addCountryTextBox.Name = "addCountryTextBox";
            // 
            // AddCountryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addCountryButton);
            this.Controls.Add(this.addCountryTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCountryForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addCountryButton;
        private System.Windows.Forms.TextBox addCountryTextBox;
    }
}