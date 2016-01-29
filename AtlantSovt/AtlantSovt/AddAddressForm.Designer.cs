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
            resources.ApplyResources(this.addressAddCountryCodeTextBox, "addressAddCountryCodeTextBox");
            this.addressAddCountryCodeTextBox.Name = "addressAddCountryCodeTextBox";
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.Name = "label50";
            // 
            // addressAddCountryNameComboBox
            // 
            resources.ApplyResources(this.addressAddCountryNameComboBox, "addressAddCountryNameComboBox");
            this.addressAddCountryNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.addressAddCountryNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.addressAddCountryNameComboBox.FormattingEnabled = true;
            this.addressAddCountryNameComboBox.Name = "addressAddCountryNameComboBox";
            this.addressAddCountryNameComboBox.SelectedIndexChanged += new System.EventHandler(this.addressAddCountryNameComboBox_SelectedIndexChanged);
            this.addressAddCountryNameComboBox.TextUpdate += new System.EventHandler(this.addressAddCountryNameComboBox_TextUpdate);
            this.addressAddCountryNameComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.addressAddCountryNameComboBox_MouseClick);
            // 
            // addressAddCountryAddButton
            // 
            resources.ApplyResources(this.addressAddCountryAddButton, "addressAddCountryAddButton");
            this.addressAddCountryAddButton.Image = global::AtlantSovt.Properties.Resources.addButton;
            this.addressAddCountryAddButton.Name = "addressAddCountryAddButton";
            this.addressAddCountryAddButton.TabStop = false;
            this.addressAddCountryAddButton.UseVisualStyleBackColor = true;
            this.addressAddCountryAddButton.Click += new System.EventHandler(this.addressAddCountryAddButton_Click);
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
            // addressAddCityNameTextBox
            // 
            resources.ApplyResources(this.addressAddCityNameTextBox, "addressAddCityNameTextBox");
            this.addressAddCityNameTextBox.Name = "addressAddCityNameTextBox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // addressAddCityCodeTextBox
            // 
            resources.ApplyResources(this.addressAddCityCodeTextBox, "addressAddCityCodeTextBox");
            this.addressAddCityCodeTextBox.Name = "addressAddCityCodeTextBox";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // addressAddStreetNameTextBox
            // 
            resources.ApplyResources(this.addressAddStreetNameTextBox, "addressAddStreetNameTextBox");
            this.addressAddStreetNameTextBox.Name = "addressAddStreetNameTextBox";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // addressAddHouseNumberTextBox
            // 
            resources.ApplyResources(this.addressAddHouseNumberTextBox, "addressAddHouseNumberTextBox");
            this.addressAddHouseNumberTextBox.Name = "addressAddHouseNumberTextBox";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // addressAddCompanyNameTextBox
            // 
            resources.ApplyResources(this.addressAddCompanyNameTextBox, "addressAddCompanyNameTextBox");
            this.addressAddCompanyNameTextBox.Name = "addressAddCompanyNameTextBox";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // addressAddContactPersonTextBox
            // 
            resources.ApplyResources(this.addressAddContactPersonTextBox, "addressAddContactPersonTextBox");
            this.addressAddContactPersonTextBox.Name = "addressAddContactPersonTextBox";
            // 
            // addAddressButton
            // 
            resources.ApplyResources(this.addAddressButton, "addAddressButton");
            this.addAddressButton.Name = "addAddressButton";
            this.addAddressButton.UseVisualStyleBackColor = true;
            this.addAddressButton.Click += new System.EventHandler(this.addAddressButton_Click);
            // 
            // AddAddressForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAddressForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddAddressForm_FormClosed);
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