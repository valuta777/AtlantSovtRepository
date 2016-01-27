namespace AtlantSovt
{
    partial class AddTrailerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTrailerForm));
            this.addTrailerButton = new System.Windows.Forms.Button();
            this.addTrailerTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addTrailerButton
            // 
            resources.ApplyResources(this.addTrailerButton, "addTrailerButton");
            this.addTrailerButton.Name = "addTrailerButton";
            this.addTrailerButton.UseVisualStyleBackColor = true;
            this.addTrailerButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addTrailerTextBox
            // 
            resources.ApplyResources(this.addTrailerTextBox, "addTrailerTextBox");
            this.addTrailerTextBox.Name = "addTrailerTextBox";
            // 
            // AddTrailerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addTrailerButton);
            this.Controls.Add(this.addTrailerTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTrailerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addTrailerButton;
        private System.Windows.Forms.TextBox addTrailerTextBox;
    }
}