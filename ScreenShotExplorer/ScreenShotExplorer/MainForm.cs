using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotExplorer
{
    public enum eSort
    {
        eSortByName = 0,
        eSortByLastWrite = 1,
        eSortByFileSize = 2,
        eSortByDate = 3,
    }

    public partial class MainForm : Form
    {
        public string       _folderPath         = "";
        public List<string> _screenShotFiles    = null;
        public eSort        _currentSort        = eSort.eSortByName;

        public MainForm()
        {
            InitializeComponent();
            Init();
            Open();
        }

        void Init()
        {
            var path            = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            _folderPath         = Path.Combine(path, "던전앤파이터\\ScreenShot");

            _screenShotFiles    = new List<string>();
        }

        void Open()
        {
            CreateScreenShotButtons();
        }

        public bool CreateScreenShotButtons(eSort sort = eSort.eSortByLastWrite)
        {
            var files = Directory.GetFiles(_folderPath);

            if (0 == files.Count())
            {
                //폴더가 비어있다.
                return false;
            }

            flowLayoutPanel1.Controls.Clear();
            _screenShotFiles.Clear();   //기존 내용 초기화

            foreach (string file in files)
            {
                string extension = new System.IO.FileInfo(file).Extension;
                if (".jpg" == extension ||
                    ".png" == extension)
                {
                    _screenShotFiles.Add(file);
                }
            }

            switch (sort)
            {
                case eSort.eSortByName:
                    _screenShotFiles.Sort(String.Compare);
                    break;
                case eSort.eSortByLastWrite:
                    _screenShotFiles.Sort(CompareLastWriteTime);
                    break;
                case eSort.eSortByFileSize:
                    _screenShotFiles.Sort(CompareFileSize);
                    break;
                case eSort.eSortByDate:
                    _screenShotFiles.Sort(CompareFileDate);
                    break;
            }

            pictureBox.Image = Image.FromFile(_screenShotFiles[0]);
            foreach (string file in _screenShotFiles)
            {
                var picButton   = new Button();

                picButton.BackgroundImage       = Image.FromFile(file);
                picButton.BackgroundImageLayout = ImageLayout.Stretch;

                picButton.MouseClick += PicButton_MouseClick;

                Label label = new Label();
                label.Text  = file.Substring(file.LastIndexOf('\\') + 1);   //파일 이름만
                label.Dock  = DockStyle.Bottom;

                picButton.Controls.Add(label);
                flowLayoutPanel1.Controls.Add(picButton);
            }

            ChangeIconSize();

            _currentSort = sort;
            return true;
        }

        private void PicButton_MouseClick(object sender, MouseEventArgs e)
        {
            var button          = sender as Button;
            pictureBox.Image    = button.BackgroundImage;
        }

        //마지막 수정일 비교후 A가 더 최신이면 음수 B가 더 최신이면 양수 동일하면 0
        private static int CompareLastWriteTime(string pathA, string pathB)
        {
            DateTime timeA = File.GetLastWriteTime(pathA);
            DateTime timeB = File.GetLastWriteTime(pathB);

            if (timeB <= timeA)
            {
                return -1;
            }

            if (timeB == timeA)
            {
                return 0;
            }

            return 1;
        }

        private static int CompareFileSize(string pathA, string pathB)
        {
            var sizeA = new System.IO.FileInfo(pathA).Length;
            var sizeB = new System.IO.FileInfo(pathB).Length;

            if (sizeA < sizeB)
            {
                return 1;
            }
            else if (sizeA == sizeB)
            {
                return 0;
            }

            return -1;
        }

        private static int CompareFileDate(string pathA, string pathB)
        {
            long timeA = FileNameToLong(new System.IO.FileInfo(pathA).Name);
            long timeB = FileNameToLong(new System.IO.FileInfo(pathB).Name);


            if (timeB < timeA)
            {
                return -1;
            }

            if (timeB == timeA)
            {
                return 0;
            }

            return 1;
        }

        private static long FileNameToLong(string fileName)
        {
            if (false == fileName.Contains("ScreenShot"))
            {
                return 0;
            }

            string date = fileName.Substring("ScreenShot".Length);
            if (0 == date.Length)
            {
                return 0;
            }

            date = date.Replace("_", "");
            date = date.Remove(date.IndexOf("."));
            try
            {
                return Convert.ToInt64(date);
            }
            catch
            {
                return 0;
            }
        }
        private void 크기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateScreenShotButtons(eSort.eSortByFileSize);
        }

        private void 날짜ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateScreenShotButtons(eSort.eSortByDate);
        }

        private void 이름ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateScreenShotButtons(eSort.eSortByName);
        }

        private void 최종수정일ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateScreenShotButtons(eSort.eSortByLastWrite);
        }

        private void 새로고침ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateScreenShotButtons(_currentSort);
        }

        private void 크게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeIconSize(1.25f);
        }

        private void 중간ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeIconSize();
        }

        private void 작게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeIconSize(0.75f);
        }

        //스크린샷의 미리보기 아이콘의 크기를 변경한다.
        private void ChangeIconSize(float time = 1)
        {
            var controls = flowLayoutPanel1.Controls;
            foreach(var control in controls)
            {
                Button button = control as Button;
                if(null == button)
                {
                    continue;
                }

                int width   = Convert.ToInt32(128 * time);
                int height  = Convert.ToInt32(102 * time);
                button.Size = new Size(width, height);
            }
        }
    }
}
