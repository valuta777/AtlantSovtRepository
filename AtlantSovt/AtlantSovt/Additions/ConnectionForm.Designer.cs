namespace AtlantSovt
{
    partial class ConnectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // domainUpDown1
            // 
            resources.ApplyResources(this.domainUpDown1, "domainUpDown1");
            this.domainUpDown1.Name = "domainUpDown1";
            // 
            // pictureBox
            // 
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Image = global::AtlantSovt.Properties.Resources.con_anim;
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // ConnectionForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.domainUpDown1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConnectionForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.TransparencyKey = this.BackColor;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DomainUpDown domainUpDown1;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}