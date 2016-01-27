namespace AtlantSovt
{
    partial class ForwarderContactDeleteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForwarderContactDeleteForm));
            this.label27 = new System.Windows.Forms.Label();
            this.forwarderUpdateSelectDeleteContactComboBox = new System.Windows.Forms.ComboBox();
            this.forwarderUpdateContactDeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // forwarderUpdateSelectDeleteContactComboBox
            // 
            resources.ApplyResources(this.forwarderUpdateSelectDeleteContactComboBox, "forwarderUpdateSelectDeleteContactComboBox");
            this.forwarderUpdateSelectDeleteContactComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.forwarderUpdateSelectDeleteContactComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.forwarderUpdateSelectDeleteContactComboBox.Name = "forwarderUpdateSelectDeleteContactComboBox";
            this.forwarderUpdateSelectDeleteContactComboBox.Sorted = true;
            this.forwarderUpdateSelectDeleteContactComboBox.SelectedIndexChanged += new System.EventHandler(this.ForwarderUpdateSelectDeleteContactComboBox_SelectedIndexChanged);
            this.forwarderUpdateSelectDeleteContactComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ForwarderUpdateSelectDeleteContactComboBox_MouseClick);
            // 
            // forwarderUpdateContactDeleteButton
            // 
            resources.ApplyResources(this.forwarderUpdateContactDeleteButton, "forwarderUpdateContactDeleteButton");
            this.forwarderUpdateContactDeleteButton.Name = "forwarderUpdateContactDeleteButton";
            this.forwarderUpdateContactDeleteButton.UseVisualStyleBackColor = true;
            this.forwarderUpdateContactDeleteButton.Click += new System.EventHandler(this.DeleteForwarderContactButton_Click);
            // 
            // ForwarderContactDeleteForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.forwarderUpdateContactDeleteButton);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.forwarderUpdateSelectDeleteContactComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForwarderContactDeleteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox forwarderUpdateSelectDeleteContactComboBox;
        private System.Windows.Forms.Button forwarderUpdateContactDeleteButton;
    }
}