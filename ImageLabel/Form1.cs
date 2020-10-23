using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace ImageLabel
{
    public partial class Form1 : Form
    {
        int index = 0;  // 当前图片 index
        ImageLib imglib = new ImageLib();

        public Form1()
        {
            InitializeComponent();

            InitDataGridView();
        }

        #region File Read
        private void ButtonBrowsePicDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBDialog = new FolderBrowserDialog();//创建FolderBrowserDialog对象
            if (FBDialog.ShowDialog() == DialogResult.OK)//判断是否选择了文件夹
            {
                string path= FBDialog.SelectedPath;//记录选择的文件夹
                imglib.ImageFromDirector(path);
                BoxPicDir.Text = imglib.directorPath;//用textBox记录获取的路径
                BoxJumpIndex.Text = "0";
                PictureBox.Image = Image.FromFile(imglib.directorPath + imglib.imgNameList[0]);
            }
            if (imglib.isChanged == 1)
            {
                imglib.SaveLabel();
                PictureBox_Draw(index);
            }
        }
        
        private void ButtonBrowseAnnoPath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    imglib.labelFilePath = openFileDialog.FileName;
                    BoxAnnoPath.Text = imglib.labelFilePath;
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string line;

                        // 从文件读取并显示行，直到文件的末尾 
                        while ((line = reader.ReadLine()) != null)
                        {
                            imglib.AddLabel(line);
                        }
                    }
                    imglib.isChanged = 1;
                    if (imglib.imgCount != 0)
                    {
                        imglib.SaveLabel();
                        BoxJumpIndex.Text = 0.ToString();
                        ButtonJumpIndex_Click(sender, e);
                        StartDataGridView();
                    }
                }
            }
        }
        #endregion

        #region Button Event
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ButtonSaveAnno_Click(sender, e);
        }

        private void ButtonNextPic_Click(object sender, EventArgs e)
        {
            if (index < imglib.imgCount)
            {
                BoxJumpIndex.Text = (index + 1).ToString();
                ButtonJumpIndex_Click(sender, e);
            }
            else
            {
                MessageBox.Show("当前图片为最后一张"); 
            }
        }

        private void ButtonPrePic_Click(object sender, EventArgs e)
        {
            if (index >= 1)
            {
                BoxJumpIndex.Text = (index - 1).ToString();
                ButtonJumpIndex_Click(sender, e);
            }
            else
            {
                MessageBox.Show("当前图片为第一张");
            }
        }

        private void ButtonJumpIndex_Click(object sender, EventArgs e)
        {
            imglib.SaveLabel();
            try
            {
                int jump = int.Parse(BoxJumpIndex.Text);
                if (jump >= 0 && jump < imglib.imgCount)
                {
                    index = jump;
                    ReloadPicture();
                }
                else
                {
                    throw new IndexOutOfRangeException("标号超出限制！");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void ButtonSaveAnno_Click(object sender, EventArgs e)
        {
            imglib.SaveLabel();
        }

        private void ButtonInitAnno_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("此举将使所有标签重置！确认吗？", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                imglib.InitializeLabels();
            }
        }

        private void HideBoxBox_CheckedChanged(object sender, EventArgs e)
        {
            ReloadPicture();
        }
        #endregion

        #region Picture Draw
        private void ReloadPicture()
        {
            if (index >= imglib.imgCount || index < 0)
                return;
            PictureBox.Image = Image.FromFile(imglib.directorPath + imglib.imgNameList[index]);
            int imgCount = imglib.imgNameList.Count;
            float percent = index / (float)imgCount * 100;
            string process = index.ToString() + "/" + imgCount.ToString() + "（" + percent.ToString("0.0") + "%)  ";
            LabelFilename.Text = process + "./" + imglib.imgNameList[index];
            if (!HideBoxBox.Checked)
                PictureBox_Draw(index);
            StartDataGridView();
        }

        /// <summary>
        /// 更新 PictureBox 的图片.
        /// </summary>
        /// <param name="picIndex">图片编号</param>
        private void PictureBox_Draw(int picIndex)
        {
            if (!imglib.boxDict.TryGetValue(imglib.imgNameList[picIndex], out var boxList))
                return;
            for (int i=0; i < boxList.Count; i++)
            {
                Box box = boxList[i];
                PictureBox_DrawRect(i, box.label, box.p1.X, box.p1.Y, box.p2.X, box.p2.Y, imglib.colorDict[box.label]);
            }
        }

        /// <summary>
        /// 在 PictureBox 中绘制一个盒.
        /// </summary>
        /// <param name="no">在盒中显示的编号</param>
        /// <param name="label">盒标签</param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        private void PictureBox_DrawRect(int no, string label, int x0, int y0, int x1, int y1, Color color)
        {
            Image img = PictureBox.Image;
            Graphics g = Graphics.FromImage(img);
            Pen pen = new Pen(color);
            g.DrawRectangle(pen, new Rectangle(x0, y0, x1 - x0, y1 - y0));

            // Create string to draw.
            string drawString = no.ToString();
            //string drawString = no.ToString() + label;

            // Create font and brush.
            Font drawFont = new Font("Arial", 14);
            SolidBrush drawBrush = new SolidBrush(Color.Red);

            // Create point for upper-left corner of drawing.
            float x = (float)(x0 + 1.0);
            float y = (float)(y0 + 1.0);

            g.DrawString(drawString, drawFont, drawBrush, x, y);
            g.Dispose();
            PictureBox.Image = img;
        }

        /// <summary>
        /// 捕获点击位置并更改所在位置的框之类
        /// ref: https://blog.csdn.net/lysc_forever/article/details/39530451
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (PictureBox.Image is null) return;
            if (HideBoxBox.Checked) return;

            //int originalWidth = this.PictureBox.Image.Width;
            int originalHeight = this.PictureBox.Image.Height;

            PropertyInfo rectangleProperty = this.PictureBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle rectangle = (Rectangle)rectangleProperty.GetValue(this.PictureBox, null);

            int currentWidth = rectangle.Width;
            int currentHeight = rectangle.Height;

            double rate = (double)currentHeight / (double)originalHeight;

            int black_left_width = (currentWidth == this.PictureBox.Width) ? 0 : (this.PictureBox.Width - currentWidth) / 2;
            int black_top_height = (currentHeight == this.PictureBox.Height) ? 0 : (this.PictureBox.Height - currentHeight) / 2;

            int zoom_x = e.X - black_left_width;
            int zoom_y = e.Y - black_top_height;

            double original_x = (double)zoom_x / rate;
            double original_y = (double)zoom_y / rate;

            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("原始尺寸{0}/{1}(宽/高)\r\n", originalWidth, originalHeight);
            //sb.AppendFormat("缩放状态图片尺寸{0}/{1}(宽/高)\r\n", currentWidth, currentHeight);
            //sb.AppendFormat("缩放比率{0}\r\n", rate);
            //sb.AppendFormat("左留白宽度{0}\r\n", black_left_width);
            //sb.AppendFormat("上留白高度{0}\r\n", black_top_height);
            //sb.AppendFormat("当前鼠标坐标{0}/{1}(X/Y)\r\n", e.X, e.Y);
            //sb.AppendFormat("缩放图中鼠标坐标{0}/{1}(X/Y)\r\n", zoom_x, zoom_y);
            //sb.AppendFormat("原始图中鼠标坐标{0}/{1}(X/Y)\r\n", original_x, original_y);
            //Console.WriteLine(sb.ToString());

            imglib.RotateBoxLabelByClickPoint(imglib.GetBoxListByIndex(index), new Point((int)original_x, (int)original_y));
            ButtonJumpIndex_Click(sender, e);
        }
        #endregion

        #region DataGrid Event
        private void InitDataGridView()
        {
            DataGridView.AllowUserToAddRows = false;
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn
            {
                Name = "No",
                HeaderText = "ID",
                Width = 45,
                ReadOnly = true
            };
            DataGridView.Columns.Insert(0, col1);

            DataGridViewComboBoxColumn col2 = new DataGridViewComboBoxColumn
            {
                Name = "label",
                HeaderText = "标签",
                Width = 100
            };
            foreach (string label in imglib.classList)
            {
                col2.Items.Add(label);
            }

            col2.DisplayIndex = 1;
            DataGridView.Columns.Insert(1, col2);
            
            DataGridView.CellValueChanged -= DataGridView_CellValueChanged;//line added after solution given
            DataGridView.CellValueChanged += DataGridView_CellValueChanged;//line added after solution given
        }

        private void StartDataGridView()
        {
            var boxList = imglib.GetBoxListByIndex(index);
            DataGridView.Rows.Clear();
            for (int i = 0; i < boxList.Count; i++)
            {
                string label = boxList[i].label;
                DataGridView.Rows.Add(i, imglib.classList.Contains(label) ? label : imglib.classList[0]);
            }
        }

        /// <summary>
        /// datagridview内嵌控件值修改事件
        /// </summary>
        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            var currentBox = imglib.GetBoxListByIndex(index)[rowIndex];
            string newValue = DataGridView.Rows[rowIndex].Cells[columnIndex].Value.ToString();
            
            if (DataGridView.Columns[columnIndex].Name == "label" && !currentBox.label.Equals(newValue))
            {
                imglib.boxDict[imglib.imgNameList[index]][rowIndex].label = newValue;
            }
            PictureBox_Draw(index);
        }
        #endregion
    }
}
