namespace FaceRecoEmcv2
{
    partial class FrmKisiEkle
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
            this.TxtAdSoyad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnYuzEkle = new System.Windows.Forms.Button();
            this.imgYuz = new System.Windows.Forms.PictureBox();
            this.imgKamera = new System.Windows.Forms.PictureBox();
            this.lblAdet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgYuz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgKamera)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtAdSoyad
            // 
            this.TxtAdSoyad.Location = new System.Drawing.Point(639, 228);
            this.TxtAdSoyad.Name = "TxtAdSoyad";
            this.TxtAdSoyad.Size = new System.Drawing.Size(158, 20);
            this.TxtAdSoyad.TabIndex = 22;
            this.TxtAdSoyad.Text = "Ad Soyad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(560, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Ad Soyad:";
            // 
            // BtnYuzEkle
            // 
            this.BtnYuzEkle.Location = new System.Drawing.Point(639, 254);
            this.BtnYuzEkle.Name = "BtnYuzEkle";
            this.BtnYuzEkle.Size = new System.Drawing.Size(158, 23);
            this.BtnYuzEkle.TabIndex = 18;
            this.BtnYuzEkle.Text = "Yüzü Ekle";
            this.BtnYuzEkle.UseVisualStyleBackColor = true;
            this.BtnYuzEkle.Click += new System.EventHandler(this.BtnYuzEkle_Click);
            // 
            // imgYuz
            // 
            this.imgYuz.Location = new System.Drawing.Point(563, 12);
            this.imgYuz.Name = "imgYuz";
            this.imgYuz.Size = new System.Drawing.Size(209, 196);
            this.imgYuz.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgYuz.TabIndex = 17;
            this.imgYuz.TabStop = false;
            // 
            // imgKamera
            // 
            this.imgKamera.Location = new System.Drawing.Point(32, 12);
            this.imgKamera.Name = "imgKamera";
            this.imgKamera.Size = new System.Drawing.Size(525, 330);
            this.imgKamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgKamera.TabIndex = 16;
            this.imgKamera.TabStop = false;
            // 
            // lblAdet
            // 
            this.lblAdet.AutoSize = true;
            this.lblAdet.Location = new System.Drawing.Point(660, 317);
            this.lblAdet.Name = "lblAdet";
            this.lblAdet.Size = new System.Drawing.Size(35, 13);
            this.lblAdet.TabIndex = 23;
            this.lblAdet.Text = "label2";
            // 
            // FrmKisiEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 354);
            this.Controls.Add(this.lblAdet);
            this.Controls.Add(this.TxtAdSoyad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnYuzEkle);
            this.Controls.Add(this.imgYuz);
            this.Controls.Add(this.imgKamera);
            this.Name = "FrmKisiEkle";
            this.Text = "KİŞİ EKLE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Training_Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.imgYuz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgKamera)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TxtAdSoyad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnYuzEkle;
        private System.Windows.Forms.PictureBox imgYuz;
        private System.Windows.Forms.PictureBox imgKamera;
        private System.Windows.Forms.Label lblAdet;
    }
}