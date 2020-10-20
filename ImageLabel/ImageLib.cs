using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ImageLabel
{
    class ImageLib
    {
        public int imgCount = 0;
        public int isChanged = 0;
        public string classListPath = "ClassList.txt";
        public string destLabelFilePath;
        public string directorPath;
        public string labelFilePath;
        public List<string> imgNameList = new List<string>();
        public List<string> classList = new List<string>();
        public List<Tuple<string, int, int, int, int>> valueList = new List<Tuple<string, int, int, int, int>>();
        public Dictionary<string, List<Tuple<string, int, int, int, int>>> dic = new Dictionary<string, List<Tuple<string, int, int, int, int>>>();

        public void imageFromDirector(string dir)
        {
            directorPath = dir;
            if (!directorPath.EndsWith("\\"))//说明对于磁盘如C盘等SelectedPath返回的是C:\\
            {
                directorPath += "\\";
            }
            
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            string[] legalSuffixs = new string[] { "jpg", "jpeg", "png" };
            foreach (FileInfo f in files)
            {
                foreach (string suffix in legalSuffixs)
                    if (f.Name.EndsWith(suffix))
                    {
                        imgNameList.Add(f.Name);//添加文件名到列表中
                        break;
                    }
            }
            imgCount = imgNameList.Count;
        }

        public void addLabel(string key, List<Tuple<string, int, int, int, int>> value)
        {
            dic.Add(key, value);
        }

        public void savelabel()
        {
            if (labelFilePath is null)
            {
                return;
            }
            int no = labelFilePath.LastIndexOf('\\');
            
            destLabelFilePath = labelFilePath.Substring(0, no + 1) + "labelFile.txt";
            string strLine;
            List<Tuple<string, int, int, int, int>> valueList = new List<Tuple<string, int, int, int, int>>();
            int i;int j;int length; string strPath;
            using (StreamWriter sw = new StreamWriter(destLabelFilePath))
            {
                for ( i = 0; i < imgCount; i++)
                {
                    strPath = imgNameList[i];
                    dic.TryGetValue(strPath, out valueList);
                    if (valueList is null)
                        continue;

                    length = valueList.Count;
                    for (j = 0; j < length; j++)
                    {
                        strLine = strPath + ' ' + valueList[j].Item1 + ' ' + valueList[j].Item2 + ' ' + valueList[j].Item3 + ' ' + valueList[j].Item4 + ' ' + valueList[j].Item5;
                        sw.WriteLine(strLine);
                    }
                }
            }
        }

        public void getClassList()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(classListPath))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        line.Trim();
                        classList.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                MessageBox.Show("The file could not be read:", e.Message);
            }
        }
    }
}
