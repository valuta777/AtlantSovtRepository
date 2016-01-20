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
            this.nameClientLabel.AutoSize = true;
            this.nameClientLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameClientLabel.Location = new System.Drawing.Point(12, 9);
            this.nameClientLabel.Name = "nameClientLabel";
            this.nameClientLabel.Size = new System.Drawing.Size(130, 21);
            this.nameClientLabel.TabIndex = 2;
            this.nameClientLabel.Text = "Контактна особа";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Номер телефону";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Номер факсу";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email";
            // 
            // contactPersonClientContactTextBox
            // 
            this.contactPersonClientContactTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contactPersonClientContactTextBox.Location = new System.Drawing.Point(12, 33);
            this.contactPersonClientContactTextBox.Name = "contactPersonClientContactTextBox";
            this.contactPersonClientContactTextBox.Size = new System.Drawing.Size(313, 29);
            this.contactPersonClientContactTextBox.TabIndex = 8;
            // 
            // telephoneNumberClientContactTextBox
            // 
            this.telephoneNumberClientContactTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.telephoneNumberClientContactTextBox.Location = new System.Drawing.Point(11, 89);
            this.telephoneNumberClientContactTextBox.Name = "telephoneNumberClientContactTextBox";
            this.telephoneNumberClientContactTextBox.Size = new System.Drawing.Size(313, 29);
            this.telephoneNumberClientContactTextBox.TabIndex = 9;
            // 
            // faxNumberClientContactTextBox
            // 
            this.faxNumberClientContactTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.faxNumberClientContactTextBox.Location = new System.Drawing.Point(11, 145);
            this.faxNumberClientContactTextBox.Name = "faxNumberClientContactTextBox";
            this.faxNumberClientContactTextBox.Size = new System.Drawing.Size(313, 29);
            this.faxNumberClientContactTextBox.TabIndex = 10;
            // 
            // emailClientContactTextBox
            // 
            this.emailClientContactTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.emailClientContactTextBox.Location = new System.Drawing.Point(11, 201);
            this.emailClientContactTextBox.Name = "emailClientContactTextBox";
            this.emailClientContactTextBox.Size = new System.Drawing.Size(313, 29);
            this.emailClientContactTextBox.TabIndex = 11;
            // 
            // addContactClientButton
            // 
            this.addContactClientButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addContactClientButton.Location = new System.Drawing.Point(11, 237);
            this.addContactClientButton.Name = "addContactClientButton";
            this.addContactClientButton.Size = new System.Drawing.Size(313, 48);
            this.addContactClientButton.TabIndex = 12;
            this.addContactClientButton.Text = "Додати контакт";
            this.addContactClientButton.UseVisualStyleBackColor = true;
            this.addContactClientButton.Click += new System.EventHandler(this.addContactClientButton_Click);
            // 
            // ClientContactAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(336, 288);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додавання контакту";
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