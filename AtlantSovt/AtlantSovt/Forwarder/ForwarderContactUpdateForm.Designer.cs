namespace AtlantSovt
{
    partial class ForwarderContactUpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForwarderContactUpdateForm));
            this.nameForwarderLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contactPersonUpdateForwarderContactTextBox = new System.Windows.Forms.TextBox();
            this.telephoneNumberUpdateForwarderContactTextBox = new System.Windows.Forms.TextBox();
            this.faxNumberUpdateForwarderContactTextBox = new System.Windows.Forms.TextBox();
            this.emailUpdateForwarderContactTextBox = new System.Windows.Forms.TextBox();
            this.updateForwarderContactButton = new System.Windows.Forms.Button();
            this.forwarderUpdateContactSelectComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameForwarderLabel
            // 
            resources.ApplyResources(this.nameForwarderLabel, "nameForwarderLabel");
            this.nameForwarderLabel.Name = "nameForwarderLabel";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // contactPersonUpdateForwarderContactTextBox
            // 
            resources.ApplyResources(this.contactPersonUpdateForwarderContactTextBox, "contactPersonUpdateForwarderContactTextBox");
            this.contactPersonUpdateForwarderContactTextBox.Name = "contactPersonUpdateForwarderContactTextBox";
            this.contactPersonUpdateForwarderContactTextBox.TextChanged += new System.EventHandler(this.contactPersonUpdateClientContactTextBox_TextChanged);
            // 
            // telephoneNumberUpdateForwarderContactTextBox
            // 
            resources.ApplyResources(this.telephoneNumberUpdateForwarderContactTextBox, "telephoneNumberUpdateForwarderContactTextBox");
            this.telephoneNumberUpdateForwarderContactTextBox.Name = "telephoneNumberUpdateForwarderContactTextBox";
            this.telephoneNumberUpdateForwarderContactTextBox.TextChanged += new System.EventHandler(this.telephoneNumberUpdateClientContactTextBox_TextChanged);
            // 
            // faxNumberUpdateForwarderContactTextBox
            // 
            resources.ApplyResources(this.faxNumberUpdateForwarderContactTextBox, "faxNumberUpdateForwarderContactTextBox");
            this.faxNumberUpdateForwarderContactTextBox.Name = "faxNumberUpdateForwarderContactTextBox";
            this.faxNumberUpdateForwarderContactTextBox.TextChanged += new System.EventHandler(this.faxNumberUpdateClientContactTextBox_TextChanged);
            // 
            // emailUpdateForwarderContactTextBox
            // 
            resources.ApplyResources(this.emailUpdateForwarderContactTextBox, "emailUpdateForwarderContactTextBox");
            this.emailUpdateForwarderContactTextBox.Name = "emailUpdateForwarderContactTextBox";
            this.emailUpdateForwarderContactTextBox.TextChanged += new System.EventHandler(this.emailUpdateClientContactTextBox_TextChanged);
            // 
            // updateForwarderContactButton
            // 
            resources.ApplyResources(this.updateForwarderContactButton, "updateForwarderContactButton");
            this.updateForwarderContactButton.Name = "updateForwarderContactButton";
            this.updateForwarderContactButton.UseVisualStyleBackColor = true;
            this.updateForwarderContactButton.Click += new System.EventHandler(this.updateContactButton_Click);
            // 
            // forwarderUpdateContactSelectComboBox
            // 
            resources.ApplyResources(this.forwarderUpdateContactSelectComboBox, "forwarderUpdateContactSelectComboBox");
            this.forwarderUpdateContactSelectComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.forwarderUpdateContactSelectComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.forwarderUpdateContactSelectComboBox.Name = "forwarderUpdateContactSelectComboBox";
            this.forwarderUpdateContactSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.clientUpdateContactSelectComboBox_SelectedIndexChanged);
            this.forwarderUpdateContactSelectComboBox.TextChanged += new System.EventHandler(this.forwarderUpdateContactSelectComboBox_TextChanged);
            this.forwarderUpdateContactSelectComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clientUpdateContactSelectComboBox_MouseClick);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ForwarderContactUpdateForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.forwarderUpdateContactSelectComboBox);
            this.Controls.Add(this.updateForwarderContactButton);
            this.Controls.Add(this.emailUpdateForwarderContactTextBox);
            this.Controls.Add(this.faxNumberUpdateForwarderContactTextBox);
            this.Controls.Add(this.telephoneNumberUpdateForwarderContactTextBox);
            this.Controls.Add(this.contactPersonUpdateForwarderContactTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameForwarderLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForwarderContactUpdateForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameForwarderLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox contactPersonUpdateForwarderContactTextBox;
        private System.Windows.Forms.TextBox telephoneNumberUpdateForwarderContactTextBox;
        private System.Windows.Forms.TextBox faxNumberUpdateForwarderContactTextBox;
        private System.Windows.Forms.TextBox emailUpdateForwarderContactTextBox;
        private System.Windows.Forms.Button updateForwarderContactButton;
        private System.Windows.Forms.ComboBox forwarderUpdateContactSelectComboBox;
        private System.Windows.Forms.Label label4;
    }
}