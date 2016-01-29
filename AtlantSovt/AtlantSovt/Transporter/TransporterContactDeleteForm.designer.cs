namespace AtlantSovt
{
    partial class TransporterContactDeleteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransporterContactDeleteForm));
            this.label27dt = new System.Windows.Forms.Label();
            this.transporterUpdateSelectDeleteContactComboBox = new System.Windows.Forms.ComboBox();
            this.transporterUpdateContactDeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label27dt
            // 
            resources.ApplyResources(this.label27dt, "label27dt");
            this.label27dt.Name = "label27dt";
            // 
            // transporterUpdateSelectDeleteContactComboBox
            // 
            resources.ApplyResources(this.transporterUpdateSelectDeleteContactComboBox, "transporterUpdateSelectDeleteContactComboBox");
            this.transporterUpdateSelectDeleteContactComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.transporterUpdateSelectDeleteContactComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.transporterUpdateSelectDeleteContactComboBox.Name = "transporterUpdateSelectDeleteContactComboBox";
            this.transporterUpdateSelectDeleteContactComboBox.Sorted = true;
            this.transporterUpdateSelectDeleteContactComboBox.SelectedIndexChanged += new System.EventHandler(this.TransporterUpdateSelectDeleteContactComboBox_SelectedIndexChanged);
            this.transporterUpdateSelectDeleteContactComboBox.TextChanged += new System.EventHandler(this.transporterUpdateSelectDeleteContactComboBox_TextChanged);
            this.transporterUpdateSelectDeleteContactComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TransporterUpdateSelectDeleteContactComboBox_MouseClick);
            // 
            // transporterUpdateContactDeleteButton
            // 
            resources.ApplyResources(this.transporterUpdateContactDeleteButton, "transporterUpdateContactDeleteButton");
            this.transporterUpdateContactDeleteButton.Name = "transporterUpdateContactDeleteButton";
            this.transporterUpdateContactDeleteButton.UseVisualStyleBackColor = true;
            this.transporterUpdateContactDeleteButton.Click += new System.EventHandler(this.DeleteTransporterContactButton_Click);
            // 
            // TransporterContactDeleteForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.transporterUpdateContactDeleteButton);
            this.Controls.Add(this.label27dt);
            this.Controls.Add(this.transporterUpdateSelectDeleteContactComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterContactDeleteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27dt;
        private System.Windows.Forms.ComboBox transporterUpdateSelectDeleteContactComboBox;
        private System.Windows.Forms.Button transporterUpdateContactDeleteButton;
    }
}