namespace AtlantSovt
{
    partial class TransporterContactAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransporterContactAddForm));
            this.nameTransporterLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contactPersonTransporterContactTextBox = new System.Windows.Forms.TextBox();
            this.telephoneNumberTransporterContactTextBox = new System.Windows.Forms.TextBox();
            this.faxNumberTransporterContactTextBox = new System.Windows.Forms.TextBox();
            this.emailTransporterContactTextBox = new System.Windows.Forms.TextBox();
            this.addContactTransporterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameTransporterLabel
            // 
            resources.ApplyResources(this.nameTransporterLabel, "nameTransporterLabel");
            this.nameTransporterLabel.Name = "nameTransporterLabel";
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
            // contactPersonTransporterContactTextBox
            // 
            resources.ApplyResources(this.contactPersonTransporterContactTextBox, "contactPersonTransporterContactTextBox");
            this.contactPersonTransporterContactTextBox.Name = "contactPersonTransporterContactTextBox";
            // 
            // telephoneNumberTransporterContactTextBox
            // 
            resources.ApplyResources(this.telephoneNumberTransporterContactTextBox, "telephoneNumberTransporterContactTextBox");
            this.telephoneNumberTransporterContactTextBox.Name = "telephoneNumberTransporterContactTextBox";
            // 
            // faxNumberTransporterContactTextBox
            // 
            resources.ApplyResources(this.faxNumberTransporterContactTextBox, "faxNumberTransporterContactTextBox");
            this.faxNumberTransporterContactTextBox.Name = "faxNumberTransporterContactTextBox";
            // 
            // emailTransporterContactTextBox
            // 
            resources.ApplyResources(this.emailTransporterContactTextBox, "emailTransporterContactTextBox");
            this.emailTransporterContactTextBox.Name = "emailTransporterContactTextBox";
            // 
            // addContactTransporterButton
            // 
            resources.ApplyResources(this.addContactTransporterButton, "addContactTransporterButton");
            this.addContactTransporterButton.Name = "addContactTransporterButton";
            this.addContactTransporterButton.UseVisualStyleBackColor = true;
            this.addContactTransporterButton.Click += new System.EventHandler(this.addContactTransporterButton_Click);
            // 
            // TransporterContactAddForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.addContactTransporterButton);
            this.Controls.Add(this.emailTransporterContactTextBox);
            this.Controls.Add(this.faxNumberTransporterContactTextBox);
            this.Controls.Add(this.telephoneNumberTransporterContactTextBox);
            this.Controls.Add(this.contactPersonTransporterContactTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTransporterLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransporterContactAddForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TransporterContactAddForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameTransporterLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox contactPersonTransporterContactTextBox;
        private System.Windows.Forms.TextBox telephoneNumberTransporterContactTextBox;
        private System.Windows.Forms.TextBox faxNumberTransporterContactTextBox;
        private System.Windows.Forms.TextBox emailTransporterContactTextBox;
        private System.Windows.Forms.Button addContactTransporterButton;
    }
}