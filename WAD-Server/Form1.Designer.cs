namespace WAD_Server
{
    partial class Form1
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnAddMovie = new System.Windows.Forms.Button();
            this.btnUploadBooking = new System.Windows.Forms.Button();
            this.btnSaveBooking = new System.Windows.Forms.Button();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.btnListBooking = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddMovie
            // 
            this.btnAddMovie.Location = new System.Drawing.Point(12, 391);
            this.btnAddMovie.Name = "btnAddMovie";
            this.btnAddMovie.Size = new System.Drawing.Size(190, 88);
            this.btnAddMovie.TabIndex = 0;
            this.btnAddMovie.Text = "Add Movie";
            this.btnAddMovie.UseVisualStyleBackColor = true;
            this.btnAddMovie.Click += new System.EventHandler(this.btnAddMovie_Click);
            // 
            // btnUploadBooking
            // 
            this.btnUploadBooking.Location = new System.Drawing.Point(208, 391);
            this.btnUploadBooking.Name = "btnUploadBooking";
            this.btnUploadBooking.Size = new System.Drawing.Size(190, 88);
            this.btnUploadBooking.TabIndex = 1;
            this.btnUploadBooking.Text = "Upload Booking";
            this.btnUploadBooking.UseVisualStyleBackColor = true;
            this.btnUploadBooking.Click += new System.EventHandler(this.btnUploadBooking_Click);
            // 
            // btnSaveBooking
            // 
            this.btnSaveBooking.Location = new System.Drawing.Point(404, 391);
            this.btnSaveBooking.Name = "btnSaveBooking";
            this.btnSaveBooking.Size = new System.Drawing.Size(190, 88);
            this.btnSaveBooking.TabIndex = 2;
            this.btnSaveBooking.Text = "Save Booking";
            this.btnSaveBooking.UseVisualStyleBackColor = true;
            this.btnSaveBooking.Click += new System.EventHandler(this.btnSaveBooking_Click);
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(12, 12);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDisplay.Size = new System.Drawing.Size(1046, 352);
            this.txtDisplay.TabIndex = 3;
            // 
            // btnListBooking
            // 
            this.btnListBooking.Location = new System.Drawing.Point(600, 391);
            this.btnListBooking.Name = "btnListBooking";
            this.btnListBooking.Size = new System.Drawing.Size(190, 88);
            this.btnListBooking.TabIndex = 4;
            this.btnListBooking.Text = "List All Booking";
            this.btnListBooking.UseVisualStyleBackColor = true;
            this.btnListBooking.Click += new System.EventHandler(this.btnListBooking_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 491);
            this.Controls.Add(this.btnListBooking);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.btnSaveBooking);
            this.Controls.Add(this.btnUploadBooking);
            this.Controls.Add(this.btnAddMovie);
            this.Name = "Form1";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnAddMovie;
        private System.Windows.Forms.Button btnUploadBooking;
        private System.Windows.Forms.Button btnSaveBooking;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button btnListBooking;
    }
}

