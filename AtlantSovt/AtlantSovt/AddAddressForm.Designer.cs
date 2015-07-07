namespace AtlantSovt
{
    partial class AddAddressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAddressForm));
            this.addressAddCountryCodeTextBox = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.addressAddCountryNameComboBox = new System.Windows.Forms.ComboBox();
            this.addressAddCountryAddButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addressAddCityNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.addressAddCityCodeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addressAddStreetNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addressAddHouseNumberTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.addressAddCompanyNameTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.addressAddContactPersonTextBox = new System.Windows.Forms.TextBox();
            this.addAddressButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addressAddCountryCodeTextBox
            // 
            this.addressAddCountryCodeTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addressAddCountryCodeTextBox.Location = new System.Drawing.Point(12, 89);
            this.addressAddCountryCodeTextBox.Name = "addressAddCountryCodeTextBox";
            this.addressAddCountryCodeTextBox.Size = new System.Drawing.Size(401, 29);
            this.addressAddCountryCodeTextBox.TabIndex = 2;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label50.Location = new System.Drawing.Point(12, 9);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(57, 21);
            this.label50.TabIndex = 38;
            this.label50.Text = "Країна";
            // 
            // addressAddCountryNameComboBox
            // 
            this.addressAddCountryNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addressAddCountryNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.addressAddCountryNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.addressAddCountryNameComboBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addressAddCountryNameComboBox.FormattingEnabled = true;
            this.addressAddCountryNameComboBox.Location = new System.Drawing.Point(12, 33);
            this.addressAddCountryNameComboBox.Name = "addressAddCountryNameComboBox";
            this.addressAddCountryNameComboBox.Size = new System.Drawing.Size(364, 29);
            this.addressAddCountryNameComboBox.TabIndex = 1;
            this.addressAddCountryNameComboBox.SelectedIndexChanged += new System.EventHandler(this.addressAddCountryNameComboBox_SelectedIndexChanged);
            this.addressAddCountryNameComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.addressAddCountryNameComboBox_MouseClick);
            // 
            // addressAddCountryAddButton
            // 
            this.addressAddCountryAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addressAddCountryAddButton.Image = ((System.Drawing.Image)(resources.GetObject("addressAddCountryAddButton.Image")));
            this.addressAddCountryAddButton.Location = new System.Drawing.Point(382, 32);
            this.addressAddCountryAddButton.Name = "addressAddCountryAddButton";
            this.addressAddCountryAddButton.Size = new System.Drawing.Size(31, 30);
            this.addressAddCountryAddButton.TabIndex = 50;
            this.addressAddCountryAddButton.TabStop = false;
            this.addressAddCountryAddButton.UseVisualStyleBackColor = true;
            this.addressAddCountryAddButton.Click += new System.EventHandler(this.addressAddCountryAddButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 21);
            this.label1.TabIndex = 51;
            this.label1.Text = "Код країни";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 21);
            this.label2.TabIndex = 53;
            this.label2.Text = "Місто";
            // 
            // addressAddCityNameTextBox
            // 
            this.addressAddCityNameTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addressAddCityNameTextBox.Location = new System.Drawing.Point(12, 145);
            this.addressAddCityNameTextBox.Name = "addressAddCityNameTextBox";
            this.addressAddCityNameTextBox.Size = new System.Drawing.Size(401, 29);
            this.addressAddCityNameTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 55;
            this.label3.Text = "Код міста";
            // 
            // addressAddCityCodeTextBox
            // 
            this.addressAddCityCodeTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addressAddCityCodeTextBox.Location = new System.Drawing.Point(12, 201);
            this.addressAddCityCodeTextBox.Name = "addressAddCityCodeTextBox";
            this.addressAddCityCodeTextBox.Size = new System.Drawing.Size(401, 29);
            this.addressAddCityCodeTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 21);
            this.label4.TabIndex = 57;
            this.label4.Text = "Вулиця";
            // 
            // addressAddStreetNameTextBox
            // 
            this.addressAddStreetNameTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addressAddStreetNameTextBox.Location = new System.Drawing.Point(12, 257);
            this.addressAddStreetNameTextBox.Name = "addressAddStreetNameTextBox";
            this.addressAddStreetNameTextBox.Size = new System.Drawing.Size(401, 29);
            this.addressAddStreetNameTextBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 21);
            this.label5.TabIndex = 59;
            this.label5.Text = "Будинок";
            // 
            // addressAddHouseNumberTextBox
            // 
            this.addressAddHouseNumberTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addressAddHouseNumberTextBox.Location = new System.Drawing.Point(12, 313);
            this.addressAddHouseNumberTextBox.Name = "addressAddHouseNumberTextBox";
            this.addressAddHouseNumberTextBox.Size = new System.Drawing.Size(401, 29);
            this.addressAddHouseNumberTextBox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 21);
            this.label6.TabIndex = 61;
            this.label6.Text = "Назва фірми";
            // 
            // addressAddCompanyNameTextBox
            // 
            this.addressAddCompanyNameTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addressAddCompanyNameTextBox.Location = new System.Drawing.Point(12, 369);
            this.addressAddCompanyNameTextBox.Name = "addressAddCompanyNameTextBox";
            this.addressAddCompanyNameTextBox.Size = new System.Drawing.Size(401, 29);
            this.addressAddCompanyNameTextBox.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(12, 401);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 21);
            this.label7.TabIndex = 63;
            this.label7.Text = "Контактна особа";
            // 
            // addressAddContactPersonTextBox
            // 
            this.addressAddContactPersonTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addressAddContactPersonTextBox.Location = new System.Drawing.Point(12, 425);
            this.addressAddContactPersonTextBox.Name = "addressAddContactPersonTextBox";
            this.addressAddContactPersonTextBox.Size = new System.Drawing.Size(401, 29);
            this.addressAddContactPersonTextBox.TabIndex = 8;
            // 
            // addAddressButton
            // 
            this.addAddressButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addAddressButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addAddressButton.Location = new System.Drawing.Point(0, 460);
            this.addAddressButton.Name = "addAddressButton";
            this.addAddressButton.Size = new System.Drawing.Size(425, 49);
            this.addAddressButton.TabIndex = 9;
            this.addAddressButton.Text = "Додати адресу";
            this.addAddressButton.UseVisualStyleBackColor = true;
            this.addAddressButton.Click += new System.EventHandler(this.addAddressButton_Click);
            // 
            // AddAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(425, 509);
            this.Controls.Add(this.addAddressButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.addressAddContactPersonTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addressAddCompanyNameTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.addressAddHouseNumberTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addressAddStreetNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addressAddCityCodeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addressAddCityNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addressAddCountryNameComboBox);
            this.Controls.Add(this.addressAddCountryAddButton);
            this.Controls.Add(this.addressAddCountryCodeTextBox);
            this.Controls.Add(this.label50);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddAddressForm";
            this.Text = "Додавання адреси розвантаження";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddAddressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox addressAddCountryCodeTextBox;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ComboBox addressAddCountryNameComboBox;
        private System.Windows.Forms.Button addressAddCountryAddButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox addressAddCityNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox addressAddCityCodeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox addressAddStreetNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox addressAddHouseNumberTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox addressAddCompanyNameTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox addressAddContactPersonTextBox;
        private System.Windows.Forms.Button addAddressButton;
    }
}