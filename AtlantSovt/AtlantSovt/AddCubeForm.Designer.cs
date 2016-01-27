namespace AtlantSovt
{
    partial class AddCubeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCubeForm));
            this.addCubeButton = new System.Windows.Forms.Button();
            this.addCubeTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addCubeButton
            // 
            resources.ApplyResources(this.addCubeButton, "addCubeButton");
            this.addCubeButton.Name = "addCubeButton";
            this.addCubeButton.UseVisualStyleBackColor = true;
            this.addCubeButton.Click += new System.EventHandler(this.addCargoButton_Click);
            // 
            // addCubeTextBox
            // 
            resources.ApplyResources(this.addCubeTextBox, "addCubeTextBox");
            this.addCubeTextBox.Name = "addCubeTextBox";
            // 
            // AddCubeForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addCubeButton);
            this.Controls.Add(this.addCubeTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCubeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addCubeButton;
        private System.Windows.Forms.TextBox addCubeTextBox;
    }
}