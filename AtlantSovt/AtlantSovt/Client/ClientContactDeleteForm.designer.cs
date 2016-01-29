namespace AtlantSovt
{
    partial class ClientContactDeleteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientContactDeleteForm));
            this.label27 = new System.Windows.Forms.Label();
            this.ClientUpdateSelectDeleteContactComboBox = new System.Windows.Forms.ComboBox();
            this.DeleteClientContactButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // ClientUpdateSelectDeleteContactComboBox
            // 
            resources.ApplyResources(this.ClientUpdateSelectDeleteContactComboBox, "ClientUpdateSelectDeleteContactComboBox");
            this.ClientUpdateSelectDeleteContactComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ClientUpdateSelectDeleteContactComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ClientUpdateSelectDeleteContactComboBox.Name = "ClientUpdateSelectDeleteContactComboBox";
            this.ClientUpdateSelectDeleteContactComboBox.Sorted = true;
            this.ClientUpdateSelectDeleteContactComboBox.SelectedIndexChanged += new System.EventHandler(this.ClientUpdateSelectDeleteContactComboBox_SelectedIndexChanged);
            this.ClientUpdateSelectDeleteContactComboBox.TextUpdate += new System.EventHandler(this.ClientUpdateSelectDeleteContactComboBox_TextUpdate);
            this.ClientUpdateSelectDeleteContactComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClientUpdateSelectDeleteContactComboBox_MouseClick);
            // 
            // DeleteClientContactButton
            // 
            resources.ApplyResources(this.DeleteClientContactButton, "DeleteClientContactButton");
            this.DeleteClientContactButton.Name = "DeleteClientContactButton";
            this.DeleteClientContactButton.UseVisualStyleBackColor = true;
            this.DeleteClientContactButton.Click += new System.EventHandler(this.DeleteClientContactButton_Click);
            // 
            // ClientContactDeleteForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeleteClientContactButton);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.ClientUpdateSelectDeleteContactComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientContactDeleteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox ClientUpdateSelectDeleteContactComboBox;
        private System.Windows.Forms.Button DeleteClientContactButton;
    }
}