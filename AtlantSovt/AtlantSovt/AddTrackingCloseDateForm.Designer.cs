namespace AtlantSovt
{
    partial class AddTrackingCloseDateForm
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
            this.addCloseDateButton = new System.Windows.Forms.Button();
            this.closeDateCalendar = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // addCloseDateButton
            // 
            this.addCloseDateButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addCloseDateButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addCloseDateButton.Location = new System.Drawing.Point(0, 299);
            this.addCloseDateButton.Name = "addCloseDateButton";
            this.addCloseDateButton.Size = new System.Drawing.Size(500, 52);
            this.addCloseDateButton.TabIndex = 4;
            this.addCloseDateButton.Text = "Додати дату закриття";
            this.addCloseDateButton.UseVisualStyleBackColor = true;
            this.addCloseDateButton.Click += new System.EventHandler(this.addCloseDateButton_Click);
            // 
            // closeDateCalendar
            // 
            this.closeDateCalendar.CalendarDimensions = new System.Drawing.Size(3, 2);
            this.closeDateCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeDateCalendar.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeDateCalendar.Location = new System.Drawing.Point(0, 0);
            this.closeDateCalendar.Name = "closeDateCalendar";
            this.closeDateCalendar.TabIndex = 5;
            // 
            // AddTrackingCloseDateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 351);
            this.Controls.Add(this.closeDateCalendar);
            this.Controls.Add(this.addCloseDateButton);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTrackingCloseDateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дата закриття заявки";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addCloseDateButton;
        private System.Windows.Forms.MonthCalendar closeDateCalendar;
    }
}