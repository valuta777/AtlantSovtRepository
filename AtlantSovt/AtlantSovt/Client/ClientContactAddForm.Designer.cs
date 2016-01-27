namespace AtlantSovt
{
    partial class ClientContactAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientContactAddForm));
            this.nameClientLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contactPersonClientContactTextBox = new System.Windows.Forms.TextBox();
            this.telephoneNumberClientContactTextBox = new System.Windows.Forms.TextBox();
            this.faxNumberClientContactTextBox = new System.Windows.Forms.TextBox();
            this.emailClientContactTextBox = new System.Windows.Forms.TextBox();
            this.addContactClientButton = new System.Windows.Forms.Button();
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
            // contactPersonClientContactTextBox
            // 
            resources.ApplyResources(this.contactPersonClientContactTextBox, "contactPersonClientContactTextBox");
            this.contactPersonClientContactTextBox.Name = "contactPersonClientContactTextBox";
            // 
            // telephoneNumberClientContactTextBox
            // 
            resources.ApplyResources(this.telephoneNumberClientContactTextBox, "telephoneNumberClientContactTextBox");
            this.telephoneNumberClientContactTextBox.Name = "telephoneNumberClientContactTextBox";
            // 
            // faxNumberClientContactTextBox
            // 
            resources.ApplyResources(this.faxNumberClientContactTextBox, "faxNumberClientContactTextBox");
            this.faxNumberClientContactTextBox.Name = "faxNumberClientContactTextBox";
            // 
            // emailClientContactTextBox
            // 
            resources.ApplyResources(this.emailClientContactTextBox, "emailClientContactTextBox");
            this.emailClientContactTextBox.Name = "emailClientContactTextBox";
            // 
            // addContactClientButton
            // 
            resources.ApplyResources(this.addContactClientButton, "addContactClientButton");
            this.addContactClientButton.Name = "addContactClientButton";
            this.addContactClientButton.UseVisualStyleBackColor = true;
            this.addContactClientButton.Click += new System.EventHandler(this.addContactClientButton_Click);
            // 
            // ClientContactAddForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.addContactClientButton);
            this.Controls.Add(this.emailClientContactTextBox);
            this.Controls.Add(this.faxNumberClientContactTextBox);
            this.Controls.Add(this.telephoneNumberClientContactTextBox);
            this.Controls.Add(this.contactPersonClientContactTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameClientLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientContactAddForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientContactAddForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameClientLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox contactPersonClientContactTextBox;
        private System.Windows.Forms.TextBox telephoneNumberClientContactTextBox;
        private System.Windows.Forms.TextBox faxNumberClientContactTextBox;
        private System.Windows.Forms.TextBox emailClientContactTextBox;
        private System.Windows.Forms.Button addContactClientButton;
    }
}