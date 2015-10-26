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
            this.nameClientLabel.AutoSize = true;
            this.nameClientLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameClientLabel.Location = new System.Drawing.Point(12, 73);
            this.nameClientLabel.Name = "nameClientLabel";
            this.nameClientLabel.Size = new System.Drawing.Size(130, 21);
            this.nameClientLabel.TabIndex = 2;
            this.nameClientLabel.Text = "Контактна особа";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Номер телефону";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Номер факсу";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email";
            // 
            // contactPersonUpdateClientContactTextBox
            // 
            this.contactPersonUpdateClientContactTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contactPersonUpdateClientContactTextBox.Location = new System.Drawing.Point(11, 97);
            this.contactPersonUpdateClientContactTextBox.Name = "contactPersonUpdateClientContactTextBox";
            this.contactPersonUpdateClientContactTextBox.Size = new System.Drawing.Size(313, 29);
            this.contactPersonUpdateClientContactTextBox.TabIndex = 8;
            this.contactPersonUpdateClientContactTextBox.TextChanged += new System.EventHandler(this.contactPersonUpdateClientContactTextBox_TextChanged);
            // 
            // telephoneNumberUpdateClientContactTextBox
            // 
            this.telephoneNumberUpdateClientContactTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.telephoneNumberUpdateClientContactTextBox.Location = new System.Drawing.Point(11, 153);
            this.telephoneNumberUpdateClientContactTextBox.Name = "telephoneNumberUpdateClientContactTextBox";
            this.telephoneNumberUpdateClientContactTextBox.Size = new System.Drawing.Size(313, 29);
            this.telephoneNumberUpdateClientContactTextBox.TabIndex = 9;
            this.telephoneNumberUpdateClientContactTextBox.TextChanged += new System.EventHandler(this.telephoneNumberUpdateClientContactTextBox_TextChanged);
            // 
            // faxNumberUpdateClientContactTextBox
            // 
            this.faxNumberUpdateClientContactTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.faxNumberUpdateClientContactTextBox.Location = new System.Drawing.Point(11, 209);
            this.faxNumberUpdateClientContactTextBox.Name = "faxNumberUpdateClientContactTextBox";
            this.faxNumberUpdateClientContactTextBox.Size = new System.Drawing.Size(313, 29);
            this.faxNumberUpdateClientContactTextBox.TabIndex = 10;
            this.faxNumberUpdateClientContactTextBox.TextChanged += new System.EventHandler(this.faxNumberUpdateClientContactTextBox_TextChanged);
            // 
            // emailUpdateClientContactTextBox
            // 
            this.emailUpdateClientContactTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.emailUpdateClientContactTextBox.Location = new System.Drawing.Point(11, 265);
            this.emailUpdateClientContactTextBox.Name = "emailUpdateClientContactTextBox";
            this.emailUpdateClientContactTextBox.Size = new System.Drawing.Size(313, 29);
            this.emailUpdateClientContactTextBox.TabIndex = 11;
            this.emailUpdateClientContactTextBox.TextChanged += new System.EventHandler(this.emailUpdateClientContactTextBox_TextChanged);
            // 
            // updateContactButton
            // 
            this.updateContactButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updateContactButton.Location = new System.Drawing.Point(11, 301);
            this.updateContactButton.Name = "updateContactButton";
            this.updateContactButton.Size = new System.Drawing.Size(313, 48);
            this.updateContactButton.TabIndex = 12;
            this.updateContactButton.Text = "Змінити контакт";
            this.updateContactButton.UseVisualStyleBackColor = true;
            this.updateContactButton.Click += new System.EventHandler(this.updateContactButton_Click);
            // 
            // clientUpdateContactSelectComboBox
            // 
            this.clientUpdateContactSelectComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clientUpdateContactSelectComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.clientUpdateContactSelectComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.clientUpdateContactSelectComboBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientUpdateContactSelectComboBox.Location = new System.Drawing.Point(11, 41);
            this.clientUpdateContactSelectComboBox.Name = "clientUpdateContactSelectComboBox";
            this.clientUpdateContactSelectComboBox.Size = new System.Drawing.Size(313, 29);
            this.clientUpdateContactSelectComboBox.TabIndex = 43;
            this.clientUpdateContactSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.clientUpdateContactSelectComboBox_SelectedIndexChanged);
            this.clientUpdateContactSelectComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clientUpdateContactSelectComboBox_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 21);
            this.label4.TabIndex = 44;
            this.label4.Text = "Оберіть контакт";
            // 
            // ClientContactUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(336, 361);
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
            this.Text = "Зміна контакту";
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