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
            this.clientAddContactPersonLabel.AutoSize = true;
            this.clientAddContactPersonLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactPersonLabel.Location = new System.Drawing.Point(12, 9);
            this.clientAddContactPersonLabel.Name = "clientAddContactPersonLabel";
            this.clientAddContactPersonLabel.Size = new System.Drawing.Size(130, 21);
            this.clientAddContactPersonLabel.TabIndex = 2;
            this.clientAddContactPersonLabel.Text = "Контактна особа";
            // 
            // clientAddContactNumberLabel
            // 
            this.clientAddContactNumberLabel.AutoSize = true;
            this.clientAddContactNumberLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactNumberLabel.Location = new System.Drawing.Point(12, 65);
            this.clientAddContactNumberLabel.Name = "clientAddContactNumberLabel";
            this.clientAddContactNumberLabel.Size = new System.Drawing.Size(130, 21);
            this.clientAddContactNumberLabel.TabIndex = 3;
            this.clientAddContactNumberLabel.Text = "Номер телефону";
            // 
            // clientAddContactFaxLabel
            // 
            this.clientAddContactFaxLabel.AutoSize = true;
            this.clientAddContactFaxLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactFaxLabel.Location = new System.Drawing.Point(12, 121);
            this.clientAddContactFaxLabel.Name = "clientAddContactFaxLabel";
            this.clientAddContactFaxLabel.Size = new System.Drawing.Size(104, 21);
            this.clientAddContactFaxLabel.TabIndex = 4;
            this.clientAddContactFaxLabel.Text = "Номер факсу";
            // 
            // clientAddContactEmailLabel
            // 
            this.clientAddContactEmailLabel.AutoSize = true;
            this.clientAddContactEmailLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactEmailLabel.Location = new System.Drawing.Point(12, 177);
            this.clientAddContactEmailLabel.Name = "clientAddContactEmailLabel";
            this.clientAddContactEmailLabel.Size = new System.Drawing.Size(48, 21);
            this.clientAddContactEmailLabel.TabIndex = 5;
            this.clientAddContactEmailLabel.Text = "Email";
            // 
            // clientAddContactPersonTextBox
            // 
            this.clientAddContactPersonTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactPersonTextBox.Location = new System.Drawing.Point(12, 33);
            this.clientAddContactPersonTextBox.Name = "clientAddContactPersonTextBox";
            this.clientAddContactPersonTextBox.Size = new System.Drawing.Size(313, 29);
            this.clientAddContactPersonTextBox.TabIndex = 8;
            // 
            // clientAddContactPhoneTextBox
            // 
            this.clientAddContactPhoneTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactPhoneTextBox.Location = new System.Drawing.Point(11, 89);
            this.clientAddContactPhoneTextBox.Name = "clientAddContactPhoneTextBox";
            this.clientAddContactPhoneTextBox.Size = new System.Drawing.Size(313, 29);
            this.clientAddContactPhoneTextBox.TabIndex = 9;
            // 
            // clientAddContactFaxTextBox
            // 
            this.clientAddContactFaxTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactFaxTextBox.Location = new System.Drawing.Point(11, 145);
            this.clientAddContactFaxTextBox.Name = "clientAddContactFaxTextBox";
            this.clientAddContactFaxTextBox.Size = new System.Drawing.Size(313, 29);
            this.clientAddContactFaxTextBox.TabIndex = 10;
            // 
            // clientAddContactEmailTextBox
            // 
            this.clientAddContactEmailTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactEmailTextBox.Location = new System.Drawing.Point(11, 201);
            this.clientAddContactEmailTextBox.Name = "clientAddContactEmailTextBox";
            this.clientAddContactEmailTextBox.Size = new System.Drawing.Size(313, 29);
            this.clientAddContactEmailTextBox.TabIndex = 11;
            // 
            // clientAddContactButton
            // 
            this.clientAddContactButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientAddContactButton.Location = new System.Drawing.Point(11, 237);
            this.clientAddContactButton.Name = "clientAddContactButton";
            this.clientAddContactButton.Size = new System.Drawing.Size(313, 48);
            this.clientAddContactButton.TabIndex = 12;
            this.clientAddContactButton.Text = "Додати контакт";
            this.clientAddContactButton.UseVisualStyleBackColor = true;
            this.clientAddContactButton.Click += new System.EventHandler(this.addContactClientButton_Click);
            // 
            // ClientContactAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(336, 288);
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