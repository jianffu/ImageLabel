using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ImageLabel
{
    class Box
    {
        public string img;
        public string label;
        public Point p1, p2;

        public Box(string img, string label, Rectangle rect)
        {
            this.img = img;
            this.label = label;
            p1 = new Point(rect.X, rect.Y);
            p2 = new Point(rect.X + rect.Width, rect.Y + rect.Height);
        }

        public Box(string img, string label, Point upleft, Point bottomright)
        {
            this.img = img;
            this.label = label;
            p1 = upleft;
            p2 = bottomright;
        }

        public override string ToString() => string.Join(" ", new object[] { img, label, p1.X, p1.Y, p2.X, p2.Y });

        public Rectangle ToRectangle() => new Rectangle(p1, new Size(Point.Subtract(p2, new Size(p1))));
    }

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
        public Dictionary<string, Color> colorDict = new Dictionary<string, Color>();
        public Dictionary<string, List<Box>> boxDict = new Dictionary<string, List<Box>>();

        public ImageLib()
        {
            GetClassList();
            InitializeColorDict();
        }

        #region File IO
        public void ImageFromDirector(string dir)
        {
            directorPath = dir;
            if (!directorPath.EndsWith("\\"))//说明对于磁盘如C盘等SelectedPath返回的是C:\\
            {
                directorPath += "\\";
            }
            
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            _ = d.GetDirectories();//文件夹
            string[] legalSuffixs = new string[] { "jpg", "jpeg", "png" };
            foreach (FileInfo f in files)
            {
                foreach (string suffix in legalSuffixs)
                    if (f.Name.EndsWith(suffix))
                    {
                        imgNameList.Add(f.Name);//添加文件名到列表中
                        boxDict.Add(f.Name, new List<Box>());
                        break;
                    }
            }
            imgCount = imgNameList.Count;
        }

        public void AddLabel(string key, string label, Point p1, Point p2)
        {
            if (!boxDict.ContainsKey(key))
                boxDict.Add(key, new List<Box>());

            boxDict[key].Add(new Box(key, label, p1, p2));
        }

        public void AddLabel(string line)
        {
            string[] arr = line.Split(' ');
            string key = arr[0];
            string label = classList.Contains(arr[1]) ? arr[1] : classList[0];
            Point p1 = new Point(int.Parse(arr[2]), int.Parse(arr[3]));
            Point p2 = new Point(int.Parse(arr[4]), int.Parse(arr[5]));

            AddLabel(key, label, p1, p2);
        }

        public void SaveLabel()
        {
            if (labelFilePath is null) return;

            destLabelFilePath = labelFilePath;
            using (StreamWriter sw = new StreamWriter(destLabelFilePath))
            {
                foreach (string imgName in imgNameList)
                {
                    if (!boxDict.ContainsKey(imgName)) continue;
                    boxDict.TryGetValue(imgName, out List<Box> boxList);
                    foreach (Box box in boxList)
                    {
                        sw.WriteLine(box.ToString());
                    }
                }
            }
        }
        #endregion

        public List<Box> GetBoxListByIndex(int index)
        {
            boxDict.TryGetValue(imgNameList[index], out var boxList);
            return !(boxList is null) ? boxList : new List<Box>();
        }

        public void InitializeLabels()
        {
            foreach (var picBoxList in boxDict.Values)
            {
                foreach (var box in picBoxList)
                {
                    box.label = classList[0];
                }
            }
        }

        public void RotateBoxLabelByClickPoint(List<Box> boxList, Point p)
        {
            foreach (Box box in boxList)
            {
                if (box.ToRectangle().Contains(p))
                {
                    box.label = classList[(classList.IndexOf(box.label) + 1) % classList.Count];
                }
            }
        }

        #region private
        private void GetClassList()
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

        private void InitializeColorDict()
        {
            var colors = new Color[] { Color.Red, Color.Yellow, Color.Green, Color.Blue, Color.Purple };
            int idx = 0;
            foreach (string label in classList)
            {
                colorDict.Add(label, colors[idx++]);
            }
        }
        #endregion
    }
}
