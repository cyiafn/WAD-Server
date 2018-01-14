namespace WAD_Server
{
    partial class ViewMovieBookingForm
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
            this.btn12PM = new System.Windows.Forms.Button();
            this.btn2PM = new System.Windows.Forms.Button();
            this.btn4PM = new System.Windows.Forms.Button();
            this.btn6PM = new System.Windows.Forms.Button();
            this.btn8PM = new System.Windows.Forms.Button();
            this.btn10PM = new System.Windows.Forms.Button();
            this.btn12AM = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnViewBookingList = new System.Windows.Forms.Button();
            this.btnViewSeats = new System.Windows.Forms.Button();
            this.dgvBookingList = new System.Windows.Forms.DataGridView();
            this.lblTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookingList)).BeginInit();
            this.SuspendLayout();
            // 
            // btn12PM
            // 
            this.btn12PM.Location = new System.Drawing.Point(466, 12);
            this.btn12PM.Name = "btn12PM";
            this.btn12PM.Size = new System.Drawing.Size(118, 50);
            this.btn12PM.TabIndex = 1;
            this.btn12PM.Text = "12PM";
            this.btn12PM.UseVisualStyleBackColor = true;
            this.btn12PM.Click += new System.EventHandler(this.btn12PM_Click);
            // 
            // btn2PM
            // 
            this.btn2PM.Location = new System.Drawing.Point(590, 14);
            this.btn2PM.Name = "btn2PM";
            this.btn2PM.Size = new System.Drawing.Size(118, 50);
            this.btn2PM.TabIndex = 3;
            this.btn2PM.Text = "2PM";
            this.btn2PM.UseVisualStyleBackColor = true;
            this.btn2PM.Click += new System.EventHandler(this.btn2PM_Click);
            // 
            // btn4PM
            // 
            this.btn4PM.Location = new System.Drawing.Point(714, 12);
            this.btn4PM.Name = "btn4PM";
            this.btn4PM.Size = new System.Drawing.Size(118, 50);
            this.btn4PM.TabIndex = 4;
            this.btn4PM.Text = "4PM";
            this.btn4PM.UseVisualStyleBackColor = true;
            this.btn4PM.Click += new System.EventHandler(this.btn4PM_Click);
            // 
            // btn6PM
            // 
            this.btn6PM.Location = new System.Drawing.Point(838, 12);
            this.btn6PM.Name = "btn6PM";
            this.btn6PM.Size = new System.Drawing.Size(118, 50);
            this.btn6PM.TabIndex = 5;
            this.btn6PM.Text = "6PM";
            this.btn6PM.UseVisualStyleBackColor = true;
            this.btn6PM.Click += new System.EventHandler(this.btn6PM_Click);
            // 
            // btn8PM
            // 
            this.btn8PM.Location = new System.Drawing.Point(962, 12);
            this.btn8PM.Name = "btn8PM";
            this.btn8PM.Size = new System.Drawing.Size(118, 50);
            this.btn8PM.TabIndex = 6;
            this.btn8PM.Text = "8PM";
            this.btn8PM.UseVisualStyleBackColor = true;
            this.btn8PM.Click += new System.EventHandler(this.btn8PM_Click);
            // 
            // btn10PM
            // 
            this.btn10PM.Location = new System.Drawing.Point(1086, 12);
            this.btn10PM.Name = "btn10PM";
            this.btn10PM.Size = new System.Drawing.Size(118, 50);
            this.btn10PM.TabIndex = 7;
            this.btn10PM.Text = "10PM";
            this.btn10PM.UseVisualStyleBackColor = true;
            this.btn10PM.Click += new System.EventHandler(this.btn10PM_Click);
            // 
            // btn12AM
            // 
            this.btn12AM.Location = new System.Drawing.Point(1210, 12);
            this.btn12AM.Name = "btn12AM";
            this.btn12AM.Size = new System.Drawing.Size(118, 50);
            this.btn12AM.TabIndex = 8;
            this.btn12AM.Text = "12AM";
            this.btn12AM.UseVisualStyleBackColor = true;
            this.btn12AM.Click += new System.EventHandler(this.btn12AM_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(337, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 29);
            this.label1.TabIndex = 9;
            this.label1.Text = "Time Slot:";
            // 
            // cbDate
            // 
            this.cbDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDate.FormattingEnabled = true;
            this.cbDate.Location = new System.Drawing.Point(113, 17);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(205, 37);
            this.cbDate.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "Date:";
            // 
            // btnViewBookingList
            // 
            this.btnViewBookingList.Location = new System.Drawing.Point(216, 451);
            this.btnViewBookingList.Name = "btnViewBookingList";
            this.btnViewBookingList.Size = new System.Drawing.Size(190, 88);
            this.btnViewBookingList.TabIndex = 12;
            this.btnViewBookingList.Text = "View Booking List";
            this.btnViewBookingList.UseVisualStyleBackColor = true;
            this.btnViewBookingList.Click += new System.EventHandler(this.btnViewBookingList_Click);
            // 
            // btnViewSeats
            // 
            this.btnViewSeats.Location = new System.Drawing.Point(20, 451);
            this.btnViewSeats.Name = "btnViewSeats";
            this.btnViewSeats.Size = new System.Drawing.Size(190, 88);
            this.btnViewSeats.TabIndex = 13;
            this.btnViewSeats.Text = "View Seats";
            this.btnViewSeats.UseVisualStyleBackColor = true;
            this.btnViewSeats.Click += new System.EventHandler(this.btnViewSeats_Click);
            // 
            // dgvBookingList
            // 
            this.dgvBookingList.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvBookingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookingList.Location = new System.Drawing.Point(26, 80);
            this.dgvBookingList.Name = "dgvBookingList";
            this.dgvBookingList.RowTemplate.Height = 37;
            this.dgvBookingList.Size = new System.Drawing.Size(1302, 349);
            this.dgvBookingList.TabIndex = 14;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(448, 481);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(242, 29);
            this.lblTime.TabIndex = 15;
            this.lblTime.Text = "Selected Time: None";
            // 
            // ViewMovieBookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 551);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.dgvBookingList);
            this.Controls.Add(this.btnViewSeats);
            this.Controls.Add(this.btnViewBookingList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn12AM);
            this.Controls.Add(this.btn10PM);
            this.Controls.Add(this.btn8PM);
            this.Controls.Add(this.btn6PM);
            this.Controls.Add(this.btn4PM);
            this.Controls.Add(this.btn2PM);
            this.Controls.Add(this.btn12PM);
            this.Name = "ViewMovieBookingForm";
            this.Text = "Movie\'s Booking Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookingList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn12PM;
        private System.Windows.Forms.Button btn2PM;
        private System.Windows.Forms.Button btn4PM;
        private System.Windows.Forms.Button btn6PM;
        private System.Windows.Forms.Button btn8PM;
        private System.Windows.Forms.Button btn10PM;
        private System.Windows.Forms.Button btn12AM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnViewBookingList;
        private System.Windows.Forms.Button btnViewSeats;
        private System.Windows.Forms.DataGridView dgvBookingList;
        private System.Windows.Forms.Label lblTime;
    }
}