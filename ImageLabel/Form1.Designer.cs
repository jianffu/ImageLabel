namespace ImageLabel
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonBrowseAnnoPath = new System.Windows.Forms.Button();
            this.BoxAnnoPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LabelFilename = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BoxPicDir = new System.Windows.Forms.TextBox();
            this.ButtonBrowsePicDir = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonPrePic = new System.Windows.Forms.Button();
            this.ButtonNextPic = new System.Windows.Forms.Button();
            this.ButtonSaveAnno = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.BoxJumpIndex = new System.Windows.Forms.TextBox();
            this.ButtonJumpIndex = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 124);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(778, 495);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ButtonJumpIndex);
            this.groupBox1.Controls.Add(this.BoxJumpIndex);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ButtonSaveAnno);
            this.groupBox1.Controls.Add(this.ButtonBrowseAnnoPath);
            this.groupBox1.Controls.Add(this.BoxAnnoPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.LabelFilename);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BoxPicDir);
            this.groupBox1.Controls.Add(this.ButtonBrowsePicDir);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(984, 112);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件区";
            // 
            // ButtonBrowseAnnoPath
            // 
            this.ButtonBrowseAnnoPath.Location = new System.Drawing.Point(653, 47);
            this.ButtonBrowseAnnoPath.Name = "ButtonBrowseAnnoPath";
            this.ButtonBrowseAnnoPath.Size = new System.Drawing.Size(31, 21);
            this.ButtonBrowseAnnoPath.TabIndex = 9;
            this.ButtonBrowseAnnoPath.Text = "...";
            this.ButtonBrowseAnnoPath.UseVisualStyleBackColor = true;
            this.ButtonBrowseAnnoPath.Click += new System.EventHandler(this.ButtonBrowseAnnoPath_Click);
            // 
            // BoxAnnoPath
            // 
            this.BoxAnnoPath.Location = new System.Drawing.Point(125, 47);
            this.BoxAnnoPath.Name = "BoxAnnoPath";
            this.BoxAnnoPath.ReadOnly = true;
            this.BoxAnnoPath.Size = new System.Drawing.Size(522, 21);
            this.BoxAnnoPath.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 9, 3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "标注文件的路径是：";
            // 
            // LabelFilename
            // 
            this.LabelFilename.Location = new System.Drawing.Point(6, 74);
            this.LabelFilename.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.LabelFilename.Name = "LabelFilename";
            this.LabelFilename.Size = new System.Drawing.Size(200, 12);
            this.LabelFilename.TabIndex = 6;
            this.LabelFilename.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "待标注图片文件夹：";
            // 
            // BoxPicDir
            // 
            this.BoxPicDir.Location = new System.Drawing.Point(125, 20);
            this.BoxPicDir.Name = "BoxPicDir";
            this.BoxPicDir.ReadOnly = true;
            this.BoxPicDir.Size = new System.Drawing.Size(522, 21);
            this.BoxPicDir.TabIndex = 4;
            // 
            // ButtonBrowsePicDir
            // 
            this.ButtonBrowsePicDir.Location = new System.Drawing.Point(653, 20);
            this.ButtonBrowsePicDir.Name = "ButtonBrowsePicDir";
            this.ButtonBrowsePicDir.Size = new System.Drawing.Size(31, 21);
            this.ButtonBrowsePicDir.TabIndex = 3;
            this.ButtonBrowsePicDir.Text = "...";
            this.ButtonBrowsePicDir.UseVisualStyleBackColor = true;
            this.ButtonBrowsePicDir.Click += new System.EventHandler(this.ButtonBrowsePicDir_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ButtonPrePic);
            this.groupBox2.Controls.Add(this.ButtonNextPic);
            this.groupBox2.Location = new System.Drawing.Point(796, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 495);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注区";
            // 
            // ButtonPrePic
            // 
            this.ButtonPrePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPrePic.Location = new System.Drawing.Point(119, 20);
            this.ButtonPrePic.Name = "ButtonPrePic";
            this.ButtonPrePic.Size = new System.Drawing.Size(75, 23);
            this.ButtonPrePic.TabIndex = 6;
            this.ButtonPrePic.Text = "上一张";
            this.ButtonPrePic.UseVisualStyleBackColor = true;
            this.ButtonPrePic.Click += new System.EventHandler(this.ButtonPrePic_Click);
            // 
            // ButtonNextPic
            // 
            this.ButtonNextPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonNextPic.Location = new System.Drawing.Point(119, 49);
            this.ButtonNextPic.Name = "ButtonNextPic";
            this.ButtonNextPic.Size = new System.Drawing.Size(75, 23);
            this.ButtonNextPic.TabIndex = 5;
            this.ButtonNextPic.Text = "下一张";
            this.ButtonNextPic.UseVisualStyleBackColor = true;
            this.ButtonNextPic.Click += new System.EventHandler(this.ButtonNextPic_Click);
            // 
            // ButtonSaveAnno
            // 
            this.ButtonSaveAnno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSaveAnno.Location = new System.Drawing.Point(885, 20);
            this.ButtonSaveAnno.Name = "ButtonSaveAnno";
            this.ButtonSaveAnno.Size = new System.Drawing.Size(93, 23);
            this.ButtonSaveAnno.TabIndex = 10;
            this.ButtonSaveAnno.Text = "保存标注结果";
            this.ButtonSaveAnno.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(212, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "图片序号(从0开始)：";
            // 
            // BoxJumpIndex
            // 
            this.BoxJumpIndex.Location = new System.Drawing.Point(337, 71);
            this.BoxJumpIndex.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.BoxJumpIndex.MaxLength = 6;
            this.BoxJumpIndex.Name = "BoxJumpIndex";
            this.BoxJumpIndex.Size = new System.Drawing.Size(40, 21);
            this.BoxJumpIndex.TabIndex = 12;
            this.BoxJumpIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ButtonJumpIndex
            // 
            this.ButtonJumpIndex.Location = new System.Drawing.Point(383, 71);
            this.ButtonJumpIndex.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ButtonJumpIndex.Name = "ButtonJumpIndex";
            this.ButtonJumpIndex.Size = new System.Drawing.Size(40, 21);
            this.ButtonJumpIndex.TabIndex = 13;
            this.ButtonJumpIndex.Text = "跳转";
            this.ButtonJumpIndex.UseVisualStyleBackColor = true;
            this.ButtonJumpIndex.Click += new System.EventHandler(this.ButtonJumpIndex_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 631);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox BoxPicDir;
        private System.Windows.Forms.Button ButtonBrowsePicDir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ButtonPrePic;
        private System.Windows.Forms.Button ButtonNextPic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelFilename;
        private System.Windows.Forms.TextBox BoxAnnoPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonBrowseAnnoPath;
        private System.Windows.Forms.Button ButtonSaveAnno;
        private System.Windows.Forms.Button ButtonJumpIndex;
        private System.Windows.Forms.TextBox BoxJumpIndex;
        private System.Windows.Forms.Label label3;
    }
}

