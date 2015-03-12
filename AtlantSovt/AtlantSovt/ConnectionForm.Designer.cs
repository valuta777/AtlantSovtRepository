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
            this.connectionAnimation = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.connectionAnimation)).BeginInit();
            this.SuspendLayout();
            // 
            // connectionAnimation
            // 
            this.connectionAnimation.BackColor = System.Drawing.Color.Transparent;
            this.connectionAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionAnimation.Image = global::AtlantSovt.Properties.Resources.con_anim;
            this.connectionAnimation.Location = new System.Drawing.Point(0, 0);
            this.connectionAnimation.Name = "connectionAnimation";
            this.connectionAnimation.Size = new System.Drawing.Size(250, 250);
            this.connectionAnimation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.connectionAnimation.TabIndex = 0;
            this.connectionAnimation.TabStop = false;
            // 
            // ConnectionForm
            // 
            this.ClientSize = new System.Drawing.Size(250, 250);
            this.Controls.Add(this.connectionAnimation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TransparencyKey = this.BackColor;
            ((System.ComponentModel.ISupportInitialize)(this.connectionAnimation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox connectionAnimation;
    }
}