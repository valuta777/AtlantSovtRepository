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
            this.clientAddContactPersonLabel = new System.Windows.Forms.Label();
            this.clientAddContactNumberLabel = new System.Windows.Forms.Label();
            this.clientAddContactFaxLabel = new System.Windows.Forms.Label();
            this.clientAddContactEmailLabel = new System.Windows.Forms.Label();
            this.clientAddContactPersonTextBox = new System.Windows.Forms.TextBox();
            this.clientAddContactPhoneTextBox = new System.Windows.Forms.TextBox();
            this.clientAddContactFaxTextBox = new System.Windows.Forms.TextBox();
            this.clientAddContactEmailTextBox = new System.Windows.Forms.TextBox();
            this.clientAddContactButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clientAddContactPersonLabel
            // 
            resources.ApplyResources(this.clientAddContactPersonLabel, "clientAddContactPersonLabel");
            this.clientAddContactPersonLabel.Name = "clientAddContactPersonLabel";
            // 
            // clientAddContactNumberLabel
            // 
            resources.ApplyResources(this.clientAddContactNumberLabel, "clientAddContactNumberLabel");
            this.clientAddContactNumberLabel.Name = "clientAddContactNumberLabel";
            // 
            // clientAddContactFaxLabel
            // 
            resources.ApplyResources(this.clientAddContactFaxLabel, "clientAddContactFaxLabel");
            this.clientAddContactFaxLabel.Name = "clientAddContactFaxLabel";
            // 
            // clientAddContactEmailLabel
            // 
            resources.ApplyResources(this.clientAddContactEmailLabel, "clientAddContactEmailLabel");
            this.clientAddContactEmailLabel.Name = "clientAddContactEmailLabel";
            // 
            // clientAddContactPersonTextBox
            // 
            resources.ApplyResources(this.clientAddContactPersonTextBox, "clientAddContactPersonTextBox");
            this.clientAddContactPersonTextBox.Name = "clientAddContactPersonTextBox";
            // 
            // clientAddContactPhoneTextBox
            // 
            resources.ApplyResources(this.clientAddContactPhoneTextBox, "clientAddContactPhoneTextBox");
            this.clientAddContactPhoneTextBox.Name = "clientAddContactPhoneTextBox";
            // 
            // clientAddContactFaxTextBox
            // 
            resources.ApplyResources(this.clientAddContactFaxTextBox, "clientAddContactFaxTextBox");
            this.clientAddContactFaxTextBox.Name = "clientAddContactFaxTextBox";
            // 
            // clientAddContactEmailTextBox
            // 
            resources.ApplyResources(this.clientAddContactEmailTextBox, "clientAddContactEmailTextBox");
            this.clientAddContactEmailTextBox.Name = "clientAddContactEmailTextBox";
            // 
            // clientAddContactButton
            // 
            resources.ApplyResources(this.clientAddContactButton, "clientAddContactButton");
            this.clientAddContactButton.Name = "clientAddContactButton";
            this.clientAddContactButton.UseVisualStyleBackColor = true;
            this.clientAddContactButton.Click += new System.EventHandler(this.addContactClientButton_Click);
            // 
            // ClientContactAddForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.clientAddContactButton);
            this.Controls.Add(this.clientAddContactEmailTextBox);
            this.Controls.Add(this.clientAddContactFaxTextBox);
            this.Controls.Add(this.clientAddContactPhoneTextBox);
            this.Controls.Add(this.clientAddContactPersonTextBox);
            this.Controls.Add(this.clientAddContactEmailLabel);
            this.Controls.Add(this.clientAddContactFaxLabel);
            this.Controls.Add(this.clientAddContactNumberLabel);
            this.Controls.Add(this.clientAddContactPersonLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientContactAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додавання контакту";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientContactAddForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clientAddContactPersonLabel;
        private System.Windows.Forms.Label clientAddContactNumberLabel;
        private System.Windows.Forms.Label clientAddContactFaxLabel;
        private System.Windows.Forms.Label clientAddContactEmailLabel;
        private System.Windows.Forms.TextBox clientAddContactPersonTextBox;
        private System.Windows.Forms.TextBox clientAddContactPhoneTextBox;
        private System.Windows.Forms.TextBox clientAddContactFaxTextBox;
        private System.Windows.Forms.TextBox clientAddContactEmailTextBox;
        private System.Windows.Forms.Button clientAddContactButton;
    }
}