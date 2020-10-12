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

                fileName = nameList[0];
                pictureBox1.Image = Image.FromFile(strPath + fileName);
                LabelFilename.Text = fileName;
            }
        }

        private void ButtonBrowseAnnoPath_Click(object sender, EventArgs e)
        {

        }

        public void Director(string dir, List<string> list)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files)
            {
                list.Add(f.Name);//添加文件名到列表中  
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
                LabelFilename.Text = fileName;
                index++;
            }
            else
            {
                MessageBox.Show("当前图片为最后一张"); 
            }
            
        }
    }
}
