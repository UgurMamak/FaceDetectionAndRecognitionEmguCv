namespace FaceRecoEmcv2
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtEigenEsikDegeri = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Faces_Found_Panel = new System.Windows.Forms.Panel();
            this.imgKamera = new System.Windows.Forms.PictureBox();
            this.message_bar = new System.Windows.Forms.Label();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eigneRecogniserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTanimaTur = new System.Windows.Forms.Label();
            this.BtnEigenface = new System.Windows.Forms.Button();
            this.BtnLBPH = new System.Windows.Forms.Button();
            this.BtnFisherface = new System.Windows.Forms.Button();
            this.lblKisi = new System.Windows.Forms.Label();
            this.BtnKisiEkle = new System.Windows.Forms.Button();
            this.CikisMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgKamera)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Eigenface eşik değeri ayarlama";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.TxtEigenEsikDegeri);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 339);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 79);
            this.panel1.TabIndex = 4;
            // 
            // TxtEigenEsikDegeri
            // 
            this.TxtEigenEsikDegeri.Location = new System.Drawing.Point(57, 25);
            this.TxtEigenEsikDegeri.Name = "TxtEigenEsikDegeri";
            this.TxtEigenEsikDegeri.Size = new System.Drawing.Size(75, 20);
            this.TxtEigenEsikDegeri.TabIndex = 1;
            this.TxtEigenEsikDegeri.Text = "2000";
            this.TxtEigenEsikDegeri.TextChanged += new System.EventHandler(this.Eigne_threshold_txtbx_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Faces_Found_Panel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(676, 46);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 421);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // Faces_Found_Panel
            // 
            this.Faces_Found_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Faces_Found_Panel.AutoScroll = true;
            this.Faces_Found_Panel.Location = new System.Drawing.Point(3, 3);
            this.Faces_Found_Panel.Name = "Faces_Found_Panel";
            this.Faces_Found_Panel.Size = new System.Drawing.Size(194, 330);
            this.Faces_Found_Panel.TabIndex = 3;
            // 
            // imgKamera
            // 
            this.imgKamera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.imgKamera.Location = new System.Drawing.Point(12, 68);
            this.imgKamera.Name = "imgKamera";
            this.imgKamera.Size = new System.Drawing.Size(658, 399);
            this.imgKamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgKamera.TabIndex = 7;
            this.imgKamera.TabStop = false;
            // 
            // message_bar
            // 
            this.message_bar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.message_bar.AutoSize = true;
            this.message_bar.Location = new System.Drawing.Point(12, 470);
            this.message_bar.Name = "message_bar";
            this.message_bar.Size = new System.Drawing.Size(53, 13);
            this.message_bar.TabIndex = 6;
            this.message_bar.Text = "Message:";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // eigneRecogniserToolStripMenuItem
            // 
            this.eigneRecogniserToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.eigneRecogniserToolStripMenuItem.Name = "eigneRecogniserToolStripMenuItem";
            this.eigneRecogniserToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eigneRecogniserToolStripMenuItem.Text = "Recogniser";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eigneRecogniserToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.CikisMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1074, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTanimaTur);
            this.groupBox1.Controls.Add(this.BtnEigenface);
            this.groupBox1.Controls.Add(this.BtnLBPH);
            this.groupBox1.Controls.Add(this.BtnFisherface);
            this.groupBox1.Location = new System.Drawing.Point(905, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(139, 159);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algoritmalar";
            // 
            // lblTanimaTur
            // 
            this.lblTanimaTur.AutoSize = true;
            this.lblTanimaTur.Location = new System.Drawing.Point(51, 128);
            this.lblTanimaTur.Name = "lblTanimaTur";
            this.lblTanimaTur.Size = new System.Drawing.Size(35, 13);
            this.lblTanimaTur.TabIndex = 12;
            this.lblTanimaTur.Text = "label2";
            // 
            // BtnEigenface
            // 
            this.BtnEigenface.Location = new System.Drawing.Point(33, 25);
            this.BtnEigenface.Name = "BtnEigenface";
            this.BtnEigenface.Size = new System.Drawing.Size(75, 23);
            this.BtnEigenface.TabIndex = 11;
            this.BtnEigenface.Text = "Eigenface";
            this.BtnEigenface.UseVisualStyleBackColor = true;
            this.BtnEigenface.Click += new System.EventHandler(this.BtnEigenface_Click);
            // 
            // BtnLBPH
            // 
            this.BtnLBPH.Location = new System.Drawing.Point(33, 83);
            this.BtnLBPH.Name = "BtnLBPH";
            this.BtnLBPH.Size = new System.Drawing.Size(75, 23);
            this.BtnLBPH.TabIndex = 11;
            this.BtnLBPH.Text = "LBPH";
            this.BtnLBPH.UseVisualStyleBackColor = true;
            this.BtnLBPH.Click += new System.EventHandler(this.BtnLBPH_Click);
            // 
            // BtnFisherface
            // 
            this.BtnFisherface.Location = new System.Drawing.Point(33, 54);
            this.BtnFisherface.Name = "BtnFisherface";
            this.BtnFisherface.Size = new System.Drawing.Size(75, 23);
            this.BtnFisherface.TabIndex = 11;
            this.BtnFisherface.Text = "Fisherface";
            this.BtnFisherface.UseVisualStyleBackColor = true;
            this.BtnFisherface.Click += new System.EventHandler(this.BtnFisherface_Click);
            // 
            // lblKisi
            // 
            this.lblKisi.AutoSize = true;
            this.lblKisi.Location = new System.Drawing.Point(305, 39);
            this.lblKisi.Name = "lblKisi";
            this.lblKisi.Size = new System.Drawing.Size(76, 13);
            this.lblKisi.TabIndex = 11;
            this.lblKisi.Text = ".......................";
            // 
            // BtnKisiEkle
            // 
            this.BtnKisiEkle.Location = new System.Drawing.Point(909, 57);
            this.BtnKisiEkle.Name = "BtnKisiEkle";
            this.BtnKisiEkle.Size = new System.Drawing.Size(75, 23);
            this.BtnKisiEkle.TabIndex = 12;
            this.BtnKisiEkle.Text = "Yeni Kişi Ekle";
            this.BtnKisiEkle.UseVisualStyleBackColor = true;
            this.BtnKisiEkle.Click += new System.EventHandler(this.BtnKisiEkle_Click);
            // 
            // CikisMenuItem
            // 
            this.CikisMenuItem.Name = "CikisMenuItem";
            this.CikisMenuItem.Size = new System.Drawing.Size(44, 20);
            this.CikisMenuItem.Text = "Çıkış";
            this.CikisMenuItem.Click += new System.EventHandler(this.CikisMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 502);
            this.Controls.Add(this.BtnKisiEkle);
            this.Controls.Add(this.lblKisi);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.imgKamera);
            this.Controls.Add(this.message_bar);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgKamera)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtEigenEsikDegeri;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel Faces_Found_Panel;
        private System.Windows.Forms.PictureBox imgKamera;
        private System.Windows.Forms.Label message_bar;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eigneRecogniserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnEigenface;
        private System.Windows.Forms.Button BtnFisherface;
        private System.Windows.Forms.Button BtnLBPH;
        private System.Windows.Forms.Label lblTanimaTur;
        private System.Windows.Forms.Label lblKisi;
        private System.Windows.Forms.Button BtnKisiEkle;
        private System.Windows.Forms.ToolStripMenuItem CikisMenuItem;
    }
}

