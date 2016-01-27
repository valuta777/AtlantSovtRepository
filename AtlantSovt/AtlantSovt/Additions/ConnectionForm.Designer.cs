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
            this.connectionAnimation = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.connectionAnimation)).BeginInit();
            this.SuspendLayout();
            // 
            // connectionAnimation
            // 
            this.connectionAnimation.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.connectionAnimation, "connectionAnimation");
            this.connectionAnimation.Image = global::AtlantSovt.Properties.Resources.con_anim;
            this.connectionAnimation.Name = "connectionAnimation";
            this.connectionAnimation.TabStop = false;
            // 
            // ConnectionForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.connectionAnimation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConnectionForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.TransparencyKey = this.BackColor;
            ((System.ComponentModel.ISupportInitialize)(this.connectionAnimation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox connectionAnimation;
    }
}