using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageLabel
{
    class ImageLib
    {
        public string directorPath;
        public List<string> imgNameList = new List<string>();
        public int imgCount = 0;

        
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

        public string destLabelFilePath;
        public string labelFilePath;
        public int isChanged = 0;
        public Dictionary<string, List<Tuple<String, int, int, int, int>>> dic = new Dictionary<string, List<Tuple<String, int, int, int, int>>>();
        public void addLabel(string key, List<Tuple<String, int, int, int, int>> value)
        {
            dic.Add(key, value);
        }
        public void savelabel()
        {
            int no = labelFilePath.LastIndexOf('\\');
            
            destLabelFilePath = labelFilePath.Substring(0, no + 1) + "labelFile.txt";
            string strLine;
            List<Tuple<String, int, int, int, int>> valueList = new List<Tuple<String, int, int, int, int>>();
            int i;int j;int length; string strPath;
            using (StreamWriter sw = new StreamWriter(destLabelFilePath))
            {
                for ( i = 0; i < imgCount; i++)
                {
                    strPath = imgNameList[i];
                    dic.TryGetValue(strPath, out valueList);
                    length = valueList.Count;
                    for (j = 0; j < length; j++)
                    {
                        strLine = strPath + ' ' + valueList[j].Item1 + ' ' + valueList[j].Item2 + ' ' + valueList[j].Item3 + ' ' + valueList[j].Item4 + ' ' + valueList[j].Item5;
                        sw.WriteLine(strLine);
                    }
                }

            }
        }
    }
}
