namespace AtlantSovt
{
    partial class AddFineForDelayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFineForDelayForm));
            this.addFineForDelayButton = new System.Windows.Forms.Button();
            this.addFineForDelayTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addFineForDelayButton
            // 
            resources.ApplyResources(this.addFineForDelayButton, "addFineForDelayButton");
            this.addFineForDelayButton.Name = "addFineForDelayButton";
            this.addFineForDelayButton.UseVisualStyleBackColor = true;
            this.addFineForDelayButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addFineForDelayTextBox
            // 
            resources.ApplyResources(this.addFineForDelayTextBox, "addFineForDelayTextBox");
            this.addFineForDelayTextBox.Name = "addFineForDelayTextBox";
            // 
            // AddFineForDelayForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addFineForDelayButton);
            this.Controls.Add(this.addFineForDelayTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFineForDelayForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addFineForDelayButton;
        private System.Windows.Forms.TextBox addFineForDelayTextBox;
    }
}