namespace AtlantSovt
{
    partial class AddTirCmrForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTirCmrForm));
            this.addTirCmrButton = new System.Windows.Forms.Button();
            this.addTirCmrTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addTirCmrButton
            // 
            resources.ApplyResources(this.addTirCmrButton, "addTirCmrButton");
            this.addTirCmrButton.Name = "addTirCmrButton";
            this.addTirCmrButton.UseVisualStyleBackColor = true;
            this.addTirCmrButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addTirCmrTextBox
            // 
            resources.ApplyResources(this.addTirCmrTextBox, "addTirCmrTextBox");
            this.addTirCmrTextBox.Name = "addTirCmrTextBox";
            // 
            // AddTirCmrForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addTirCmrButton);
            this.Controls.Add(this.addTirCmrTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTirCmrForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addTirCmrButton;
        private System.Windows.Forms.TextBox addTirCmrTextBox;
    }
}