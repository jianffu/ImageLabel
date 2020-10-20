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
        int index = 0;
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
                        string lastFileName = "";
                        List<Tuple<string, int, int, int, int>> tupleList = new List<Tuple<string, int, int, int, int>>();
                        // 从文件读取并显示行，直到文件的末尾 
                        while ((line = reader.ReadLine()) != null)
                        {
                            strArr = line.Split(' ');
                            if (!lastFileName.Equals("") && !strArr[0].Equals(lastFileName))
                            {
                                imglib.AddLabel(lastFileName, tupleList);
                                tupleList = new List<Tuple<string, int, int, int, int>>();
                            }
                            tupleList.Add(Tuple.Create(strArr[1], int.Parse(strArr[2]), int.Parse(strArr[3]), int.Parse(strArr[4]), int.Parse(strArr[5])));
                            lastFileName = strArr[0];
                        }
                        imglib.AddLabel(lastFileName, tupleList);
                        tupleList = new List<Tuple<string, int, int, int, int>>();
                    }
                    imglib.isChanged = 1;
                    if (imglib.imgCount != 0)
                    {
                        imglib.SaveLabel();
                        pictureBox_Draw(index);
                        startDataGridView();
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
                    PictureBox.Image = Image.FromFile(imglib.directorPath + imglib.imgNameList[jump]);
                    
                    LabelFilename.Text = "./" + imglib.imgNameList[jump];
                    index = jump;
                    pictureBox_Draw(index);
                    startDataGridView();
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
            imglib.InitializeLabels();
        }
        #endregion

        #region Picture Draw
        /// <summary>
        /// 更新 PictureBox 的图片.
        /// </summary>
        /// <param name="picIndex">图片编号</param>
        private void pictureBox_Draw(int picIndex)
        {
            //List<Tuple<String, int, int, int, int>> valueList = new List<Tuple<String, int, int, int, int>>();
            imglib.dict.TryGetValue(imglib.imgNameList[picIndex], out imglib.valueList);
            if (imglib.valueList is null)
                return;
            for (int i=0; i < imglib.valueList.Count; i++)
            {
                PictureBox_DrawRect(i, 
                    imglib.valueList[i].Item1, 
                    imglib.valueList[i].Item2, 
                    imglib.valueList[i].Item3, 
                    imglib.valueList[i].Item4, 
                    imglib.valueList[i].Item5,
                    imglib.colorDict[imglib.valueList[i].Item1]);
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
        #endregion

        #region DataGrid Event
        private void InitDataGridView()
        {
            
            dataGridView1.AllowUserToAddRows = false;
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn
            {
                Name = "No",
                HeaderText = "序号",
                Width = 45,
                ReadOnly = true
            };
            dataGridView1.Columns.Insert(0, col1);

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
            dataGridView1.Columns.Insert(1, col2);
            //dataGridView1.Rows[1].Cells[1].Value = imglib.classList[2];
            dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;//line added after solution given
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;//line added after solution given
        }
        private void startDataGridView()
        {
            if (imglib.valueList is null)
                return;
            int rowsNum = imglib.valueList.Count;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < rowsNum; i++)
            {
                string label = imglib.valueList[i].Item1;
                dataGridView1.Rows.Add(i, imglib.classList.Contains(label) ? label : imglib.classList[0]);
            }
        }

        /// <summary>
        /// datagridview内嵌控件值修改事件
        /// </summary>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            string newValue = dataGridView1.Rows[rowIndex].Cells[columnIndex].Value.ToString();
            
            if (dataGridView1.Columns[columnIndex].Name == "label" && !imglib.valueList[rowIndex].Item1.Equals(newValue))
            {
                imglib.valueList[rowIndex] = Tuple.Create(newValue, imglib.valueList[rowIndex].Item2, imglib.valueList[rowIndex].Item3, imglib.valueList[rowIndex].Item4, imglib.valueList[rowIndex].Item5);
                imglib.dict[imglib.imgNameList[index]][rowIndex] = Tuple.Create(newValue, imglib.valueList[rowIndex].Item2, imglib.valueList[rowIndex].Item3, imglib.valueList[rowIndex].Item4, imglib.valueList[rowIndex].Item5);
            }
            pictureBox_Draw(index);
        }
        #endregion

    }
}
