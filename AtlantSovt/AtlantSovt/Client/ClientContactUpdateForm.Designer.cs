namespace AtlantSovt
{
    partial class ClientContactUpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientContactUpdateForm));
            this.nameClientLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contactPersonUpdateClientContactTextBox = new System.Windows.Forms.TextBox();
            this.telephoneNumberUpdateClientContactTextBox = new System.Windows.Forms.TextBox();
            this.faxNumberUpdateClientContactTextBox = new System.Windows.Forms.TextBox();
            this.emailUpdateClientContactTextBox = new System.Windows.Forms.TextBox();
            this.updateContactButton = new System.Windows.Forms.Button();
            this.clientUpdateContactSelectComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameClientLabel
            // 
            resources.ApplyResources(this.nameClientLabel, "nameClientLabel");
            this.nameClientLabel.Name = "nameClientLabel";
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
            // contactPersonUpdateClientContactTextBox
            // 
            resources.ApplyResources(this.contactPersonUpdateClientContactTextBox, "contactPersonUpdateClientContactTextBox");
            this.contactPersonUpdateClientContactTextBox.Name = "contactPersonUpdateClientContactTextBox";
            this.contactPersonUpdateClientContactTextBox.TextChanged += new System.EventHandler(this.contactPersonUpdateClientContactTextBox_TextChanged);
            // 
            // telephoneNumberUpdateClientContactTextBox
            // 
            resources.ApplyResources(this.telephoneNumberUpdateClientContactTextBox, "telephoneNumberUpdateClientContactTextBox");
            this.telephoneNumberUpdateClientContactTextBox.Name = "telephoneNumberUpdateClientContactTextBox";
            this.telephoneNumberUpdateClientContactTextBox.TextChanged += new System.EventHandler(this.telephoneNumberUpdateClientContactTextBox_TextChanged);
            // 
            // faxNumberUpdateClientContactTextBox
            // 
            resources.ApplyResources(this.faxNumberUpdateClientContactTextBox, "faxNumberUpdateClientContactTextBox");
            this.faxNumberUpdateClientContactTextBox.Name = "faxNumberUpdateClientContactTextBox";
            this.faxNumberUpdateClientContactTextBox.TextChanged += new System.EventHandler(this.faxNumberUpdateClientContactTextBox_TextChanged);
            // 
            // emailUpdateClientContactTextBox
            // 
            resources.ApplyResources(this.emailUpdateClientContactTextBox, "emailUpdateClientContactTextBox");
            this.emailUpdateClientContactTextBox.Name = "emailUpdateClientContactTextBox";
            this.emailUpdateClientContactTextBox.TextChanged += new System.EventHandler(this.emailUpdateClientContactTextBox_TextChanged);
            // 
            // updateContactButton
            // 
            resources.ApplyResources(this.updateContactButton, "updateContactButton");
            this.updateContactButton.Name = "updateContactButton";
            this.updateContactButton.UseVisualStyleBackColor = true;
            this.updateContactButton.Click += new System.EventHandler(this.updateContactButton_Click);
            // 
            // clientUpdateContactSelectComboBox
            // 
            resources.ApplyResources(this.clientUpdateContactSelectComboBox, "clientUpdateContactSelectComboBox");
            this.clientUpdateContactSelectComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.clientUpdateContactSelectComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.clientUpdateContactSelectComboBox.Name = "clientUpdateContactSelectComboBox";
            this.clientUpdateContactSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.clientUpdateContactSelectComboBox_SelectedIndexChanged);
            this.clientUpdateContactSelectComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clientUpdateContactSelectComboBox_MouseClick);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ClientContactUpdateForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clientUpdateContactSelectComboBox);
            this.Controls.Add(this.updateContactButton);
            this.Controls.Add(this.emailUpdateClientContactTextBox);
            this.Controls.Add(this.faxNumberUpdateClientContactTextBox);
            this.Controls.Add(this.telephoneNumberUpdateClientContactTextBox);
            this.Controls.Add(this.contactPersonUpdateClientContactTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameClientLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientContactUpdateForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameClientLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox contactPersonUpdateClientContactTextBox;
        private System.Windows.Forms.TextBox telephoneNumberUpdateClientContactTextBox;
        private System.Windows.Forms.TextBox faxNumberUpdateClientContactTextBox;
        private System.Windows.Forms.TextBox emailUpdateClientContactTextBox;
        private System.Windows.Forms.Button updateContactButton;
        private System.Windows.Forms.ComboBox clientUpdateContactSelectComboBox;
        private System.Windows.Forms.Label label4;
    }
}