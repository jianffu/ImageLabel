using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace ImageLabel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ImageLib imglib = new ImageLib();
        private void ButtonBrowsePicDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBDialog = new FolderBrowserDialog();//创建FolderBrowserDialog对象
            if (FBDialog.ShowDialog() == DialogResult.OK)//判断是否选择了文件夹
            {
                string path= FBDialog.SelectedPath;//记录选择的文件夹
                imglib.imageFromDirector(path);
                BoxPicDir.Text = imglib.directorPath;//用textBox记录获取的路径
                BoxJumpIndex.Text = "0";
                PictureBox.Image = Image.FromFile(imglib.directorPath + imglib.imgNameList[0]);
            }
            if (imglib.isChanged == 1)
            {
                imglib.savelabel();
                pictureBox_Draw(index);
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
                        string[] strArr;
                        string defalutLabel = "未打电话";
                        string lastFileName = "";
                        List<Tuple<String, int, int, int, int>> tupleList = new List<Tuple<String, int, int, int, int>>();
                        // 从文件读取并显示行，直到文件的末尾 
                        while ((line = reader.ReadLine()) != null)
                        {
                            strArr = line.Split(' ');
                            if (!lastFileName.Equals("") && !strArr[0].Equals(lastFileName))
                            {
                                imglib.addLabel(lastFileName, tupleList);
                                tupleList = new List<Tuple<String, int, int, int, int>>();
                            }
                            tupleList.Add(Tuple.Create(defalutLabel, int.Parse(strArr[2]), int.Parse(strArr[3]), int.Parse(strArr[4]), int.Parse(strArr[5])));
                            lastFileName = strArr[0];

                        }
                        imglib.addLabel(lastFileName, tupleList);
                        tupleList = new List<Tuple<String, int, int, int, int>>();
                    }
                    imglib.isChanged = 1;
                    if (imglib.imgCount != 0)
                    {
                        imglib.savelabel();
                        pictureBox_Draw(index);
                    }
                }
            }
        }

        int index = 0;
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
            try
            {
                int jump = int.Parse(BoxJumpIndex.Text);
                if (jump >= 0 && jump < imglib.imgCount)
                {
                    PictureBox.Image = Image.FromFile(imglib.directorPath + imglib.imgNameList[jump]);
                    
                    LabelFilename.Text = "./" + imglib.imgNameList[jump];
                    index = jump;
                    pictureBox_Draw(index);
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

        /// <summary>
        /// 更新 PictureBox 的图片.
        /// </summary>
        /// <param name="picIndex">图片编号</param>
        private void pictureBox_Draw(int picIndex)
        {
            List<Tuple<String, int, int, int, int>> valueList = new List<Tuple<String, int, int, int, int>>();
            imglib.dic.TryGetValue(imglib.imgNameList[picIndex], out valueList);
            if (valueList is null)
                return;

            for (int i=0; i<valueList.Count; i++)
            {
                PictureBox_DrawRect(i, valueList[i].Item1, valueList[i].Item2, valueList[i].Item3, valueList[i].Item4, valueList[i].Item5);
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
        private void PictureBox_DrawRect(int no, string label, int x0, int y0, int x1, int y1)
        {
            Image img = PictureBox.Image;
            Graphics g = Graphics.FromImage(img);
            Pen pen = new Pen(Color.Yellow);
            g.DrawRectangle(pen, new Rectangle(x0, y0, x1 - x0, y1 - y0));

            // Create string to draw.
            string drawString = no.ToString() + label;

            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Red);

            // Create point for upper-left corner of drawing.
            float x = (float)(x0 + 1.0);
            float y = (float)(y0 + 1.0);

            g.DrawString(drawString, drawFont, drawBrush, x, y);
            g.Dispose();
            PictureBox.Image = img;
        }

        private void ButtonSaveAnno_Click(object sender, EventArgs e)
        {
            imglib.savelabel();
        }
    }
}
