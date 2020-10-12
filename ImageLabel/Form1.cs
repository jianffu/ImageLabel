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

        string strPath;
        string path;
        List<string> nameList;
        string fileName;
        private void ButtonBrowsePicDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBDialog = new FolderBrowserDialog();//创建FolderBrowserDialog对象
            if (FBDialog.ShowDialog() == DialogResult.OK)//判断是否选择了文件夹
            {
                strPath = FBDialog.SelectedPath;//记录选择的文件夹
                if (strPath.EndsWith("\\"))//说明对于磁盘如C盘等SelectedPath返回的是C:\\
                {
                    BoxPicDir.Text = strPath;//用textBox记录获取的路径

                }
                else//对于一般磁盘下的文件返回的是如C:\\user没有\\结尾的文件夹路径
                {
                    BoxPicDir.Text = strPath + "\\";
                    strPath += "\\";
                }

                path = strPath;
                nameList = new List<string>();
                Director(path, nameList);

                try
                {
                    fileName = nameList[0];
                    pictureBox1.Image = Image.FromFile(strPath + fileName);
                    LabelFilename.Text = "./" + fileName;
                }
                catch (Exception) { }
            }
        }


        List<Tuple<String, String, int, int, int, int>> tupleList = new List<Tuple<String, String, int, int, int, int>>();
        string labelDirectory;
        string destLabelFilePath;
        private void ButtonBrowseAnnoPath_Click(object sender, EventArgs e)
        {
            var labelFileContent = string.Empty;
            var labelFilePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    labelFilePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string line;
                        string[] strArr;
                        string defalutLabel = "未打电话";
                        
                        
                        // 从文件读取并显示行，直到文件的末尾 
                        while ((line = reader.ReadLine()) != null)
                        {
                            strArr = line.Split(' ');
                            tupleList.Add(Tuple.Create(strArr[0], defalutLabel, int.Parse(strArr[2]), int.Parse(strArr[3]), int.Parse(strArr[4]), int.Parse(strArr[5])));
                        }
                    }
                    int no = labelFilePath.LastIndexOf('\\');
                    labelDirectory = labelFilePath.Substring(0, no + 1);
                    destLabelFilePath = labelDirectory + "labelFile.txt";
                    string strLine;
                    int length;
                    using (StreamWriter sw = new StreamWriter(destLabelFilePath))
                    {
                        length = tupleList.Count;
                        for (int i = 0; i < length; i++)
                        {
                            strLine = tupleList[i].Item1 + ' ' + tupleList[i].Item2 + ' ' + tupleList[i].Item3 + ' ' + tupleList[i].Item4 + ' ' + tupleList[i].Item5 + ' ' + tupleList[i].Item6;
                            sw.WriteLine(strLine);
                        }
                        
                    }
                }
            }
            
            MessageBox.Show(labelDirectory, "File Content at path: " + labelFilePath, MessageBoxButtons.OK);
        }

        public void Director(string dir, List<string> list)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            string[] legalSuffixs = new string[] { "jpg", "jpeg", "png" };
            foreach (FileInfo f in files)
            {
                foreach (string suffix in legalSuffixs)
                    if (f.Name.EndsWith(suffix))
                    {
                        list.Add(f.Name);//添加文件名到列表中
                        break;
                    }
            }
            //获取子文件夹内的文件列表，递归遍历  
            //foreach (DirectoryInfo dd in directs)
            //{
            //    Director(dd.FullName, list);
            //}
        }

        int index = 0;
        private void ButtonNextPic_Click(object sender, EventArgs e)
        {
            if (index < nameList.Count)
            {
                fileName = nameList[++index];
                pictureBox1.Image = Image.FromFile(strPath + fileName);
                LabelFilename.Text = "./" + fileName;
                index++;
            }
            else
            {
                MessageBox.Show("当前图片为最后一张"); 
            }
            
        }
    }
}
