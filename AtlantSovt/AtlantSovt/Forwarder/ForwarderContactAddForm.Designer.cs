namespace AtlantSovt
{
    partial class ForwarderContactAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForwarderContactAddForm));
            this.nameForwarderLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contactPersonForwarderContactTextBox = new System.Windows.Forms.TextBox();
            this.telephoneNumberForwarderContactTextBox = new System.Windows.Forms.TextBox();
            this.faxNumberForwarderContactTextBox = new System.Windows.Forms.TextBox();
            this.emailForwarderContactTextBox = new System.Windows.Forms.TextBox();
            this.addContactForwarderButton = new System.Windows.Forms.Button();
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
            // contactPersonForwarderContactTextBox
            // 
            resources.ApplyResources(this.contactPersonForwarderContactTextBox, "contactPersonForwarderContactTextBox");
            this.contactPersonForwarderContactTextBox.Name = "contactPersonForwarderContactTextBox";
            // 
            // telephoneNumberForwarderContactTextBox
            // 
            resources.ApplyResources(this.telephoneNumberForwarderContactTextBox, "telephoneNumberForwarderContactTextBox");
            this.telephoneNumberForwarderContactTextBox.Name = "telephoneNumberForwarderContactTextBox";
            // 
            // faxNumberForwarderContactTextBox
            // 
            resources.ApplyResources(this.faxNumberForwarderContactTextBox, "faxNumberForwarderContactTextBox");
            this.faxNumberForwarderContactTextBox.Name = "faxNumberForwarderContactTextBox";
            // 
            // emailForwarderContactTextBox
            // 
            resources.ApplyResources(this.emailForwarderContactTextBox, "emailForwarderContactTextBox");
            this.emailForwarderContactTextBox.Name = "emailForwarderContactTextBox";
            // 
            // addContactForwarderButton
            // 
            resources.ApplyResources(this.addContactForwarderButton, "addContactForwarderButton");
            this.addContactForwarderButton.Name = "addContactForwarderButton";
            this.addContactForwarderButton.UseVisualStyleBackColor = true;
            this.addContactForwarderButton.Click += new System.EventHandler(this.addContactForwarderButton_Click);
            // 
            // ForwarderContactAddForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.addContactForwarderButton);
            this.Controls.Add(this.emailForwarderContactTextBox);
            this.Controls.Add(this.faxNumberForwarderContactTextBox);
            this.Controls.Add(this.telephoneNumberForwarderContactTextBox);
            this.Controls.Add(this.contactPersonForwarderContactTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameForwarderLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForwarderContactAddForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ForwarderContactAddForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameForwarderLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox contactPersonForwarderContactTextBox;
        private System.Windows.Forms.TextBox telephoneNumberForwarderContactTextBox;
        private System.Windows.Forms.TextBox faxNumberForwarderContactTextBox;
        private System.Windows.Forms.TextBox emailForwarderContactTextBox;
        private System.Windows.Forms.Button addContactForwarderButton;
    }
}