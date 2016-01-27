namespace AtlantSovt
{
    partial class AddOrderNoteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOrderNoteForm));
            this.addOrderNoteButton = new System.Windows.Forms.Button();
            this.addOrderNoteTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // addOrderNoteButton
            // 
            resources.ApplyResources(this.addOrderNoteButton, "addOrderNoteButton");
            this.addOrderNoteButton.Name = "addOrderNoteButton";
            this.addOrderNoteButton.UseVisualStyleBackColor = true;
            this.addOrderNoteButton.Click += new System.EventHandler(this.addOrderNoteButton_Click);
            // 
            // addOrderNoteTextBox
            // 
            resources.ApplyResources(this.addOrderNoteTextBox, "addOrderNoteTextBox");
            this.addOrderNoteTextBox.Name = "addOrderNoteTextBox";
            // 
            // AddOrderNoteForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addOrderNoteTextBox);
            this.Controls.Add(this.addOrderNoteButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOrderNoteForm";
            this.Shown += new System.EventHandler(this.AddOrderNoteForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addOrderNoteButton;
        private System.Windows.Forms.RichTextBox addOrderNoteTextBox;
    }
}