namespace WAD_Server
{
    partial class AddMovieForm
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
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImage = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(87, 34);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(229, 35);
            this.txtTitle.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(762, 35);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(96, 35);
            this.txtPrice.TabIndex = 3;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Action",
            "Adventure",
            "Comedy",
            "Drama",
            "Historical",
            "Horror",
            "Sci-Fi",
            "Westerns"});
            this.cbType.Location = new System.Drawing.Point(429, 34);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(202, 37);
            this.cbType.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(662, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "Price: $";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(885, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "Image: ";
            // 
            // txtImage
            // 
            this.txtImage.Location = new System.Drawing.Point(983, 34);
            this.txtImage.Name = "txtImage";
            this.txtImage.Size = new System.Drawing.Size(229, 35);
            this.txtImage.TabIndex = 7;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(1250, 28);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(157, 53);
            this.btnUpload.TabIndex = 8;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(1233, 482);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(174, 55);
            this.btnConfirm.TabIndex = 9;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.SystemColors.Window;
            this.pbPreview.Location = new System.Drawing.Point(19, 136);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(1388, 284);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPreview.TabIndex = 10;
            this.pbPreview.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 29);
            this.label5.TabIndex = 11;
            this.label5.Text = "Image Preview";
            // 
            // AddMovieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 566);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbPreview);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtImage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTitle);
            this.Name = "AddMovieForm";
            this.Text = "AddMovieForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImage;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Label label5;
    }
}