using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static ImageAtlas.CommonClass;

namespace ImageAtlas
{
    public partial class MainForm : Form
    {
        public int _atlasWidth  = 2048;
        public int _atlasHeight = 2048;
        public int _currentPage = 1;
        
        public Dictionary<int, Bitmap>  _bitmaps        = new Dictionary<int, Bitmap>();    //미리보기용 이미지들
        public Dictionary<int, Bitmap>  _bitmapsForSave = new Dictionary<int, Bitmap>();    //실제저장할 이미지들
        public Dictionary<int, int>     _remainAreaSize = new Dictionary<int, int>();       //남은 면적
        public Dictionary<int, AtlasImage>       _currentImage  = new Dictionary<int, AtlasImage>();        //페이지별 현재 이미지
        public Dictionary<int, List<AtlasImage>> _images        = new Dictionary<int, List<AtlasImage>>();  //페이지별 모든 이미지들의 정보

        private ButtonList _buttonList = null;

        public MainForm()
        {
            InitializeComponent();
            Open();
        }

        public void ChangeCurrentPage(int page)
        {
            if (page == _currentPage) //이미 해당페이지
            {
                return;
            }

            pictureBox.Image    = _bitmaps[page];
            _currentPage        = page;
        }

        private void CreateImage(string filePath, AtlasImage image)
        {
            image.Open(filePath);    //초기화

            if (_atlasWidth < image.Rect._width ||
                _atlasHeight < image.Rect._height)
            {
                MessageBox.Show(filePath + "\n해당 이미지가 너무 큽니다.");
            }

        }

        private void OldInsertImageToList(string filePath)
        {
            AtlasImage newImage = new AtlasImage();
            newImage.Open(filePath);    //초기화

            if( _atlasWidth < newImage.Rect._width ||
                _atlasHeight < newImage.Rect._height)
            {
                MessageBox.Show(filePath + "\n해당 이미지가 너무 큽니다.");
                return;
            }

            MyRectangle rect = newImage.Rect;
            
            int insertPage = -1;
            int pageCount = _images.Count;
            int areaSize = rect._width * rect._height;   //새로 필요한 공간

            bool hasSpace = false;
            //빈공간을 찾아서 추가한다.
            for (int page = 1; page <= pageCount; page++)
            {
                //각페이지의 남은 면적보다 추가하고자하는 면적이 클 경우 다음으로
                if (_remainAreaSize[page] < areaSize)
                {
                    continue;       //페이지의 남은 공간이 부족하다 다음 페이지로
                }

                foreach (AtlasImage image in _images[page])
                {
                    //기존에 있던 이미지의 우측에 새로운 이미지를 붙일수 있는지 검사
                    rect._posX = image.Rect._posX + image.Rect._width;
                    rect._posY = image.Rect._posY;

                    if (false == CollisionCheckInPage(page, rect))
                    {
                        insertPage = page;
                        hasSpace = true;
                        break;
                    }

                    //기존에 있던 이미지의 하단에 새로운 이미지를 붙일수 있는지 검사
                    rect._posX = image.Rect._posX;
                    rect._posY = image.Rect._posY + image.Rect._height;

                    if (false == CollisionCheckInPage(page, rect))
                    {
                        insertPage = page;
                        hasSpace = true;
                        break;
                    }
                }

                if(true == hasSpace)
                {
                    break;
                }
            }
            
            //적절한 빈공간을 찾지 못한 경우 새로운 페이지를 추가한다.
            if (insertPage < 0)
            {
                // (0,0)으로 위치 지정
                rect._posX = 0;
                rect._posY = 0;

                //새로운 페이지 추가
                insertPage = pageCount + 1;

                _remainAreaSize.Add(insertPage, (_atlasHeight * _atlasWidth));
                _images.Add(insertPage, new List<AtlasImage>());
                _bitmaps.Add(insertPage, new Bitmap(2048, 2048));
                _bitmapsForSave.Add(insertPage, new Bitmap(2048, 2048));
                _currentImage.Add(insertPage, null);

                //buttonList에 새로운 페이지 추가
                _buttonList.AddButton(insertPage.ToString());
            }

            newImage.PrintToImage(false, _bitmaps[insertPage]);
            newImage.PrintImageForSave(_bitmapsForSave[insertPage]);

            _remainAreaSize[insertPage] -= (rect._width * rect._height);
            _images[insertPage].Add(newImage);

            pictureBox.Image = _bitmaps[insertPage];
        }
        private void InsertImageToList(AtlasImage newImage)
        {
            MyRectangle rect = newImage.Rect;

            int insertPage = FindFreeSpace(rect);

            newImage.PrintToImage(false, _bitmaps[insertPage]);
            newImage.PrintImageForSave(_bitmapsForSave[insertPage]);

            _remainAreaSize[insertPage] -= (rect._width * rect._height);
            _images[insertPage].Add(newImage);

            pictureBox.Image = _bitmaps[insertPage];
        }

        private int FindFreeSpace(MyRectangle rect)
        {
            int insertPage  = -1;
            int pageCount   = _images.Count;
            int areaSize    = rect._width * rect._height;   //새로 필요한 공간

            //빈공간을 찾아서 추가한다.
            for (int page = 1; page <= pageCount; page++)
            {
                //각페이지의 남은 면적보다 추가하고자하는 면적이 클 경우 다음으로
                if (_remainAreaSize[page] < areaSize)
                {
                    continue;       //페이지의 남은 공간이 부족하다 다음 페이지로
                }

                foreach (AtlasImage image in _images[page])
                {
                    //기존에 있던 이미지의 우측에 새로운 이미지를 붙일수 있는지 검사
                    rect._posX = image.Rect._posX + image.Rect._width;
                    rect._posY = image.Rect._posY;

                    if (false == CollisionCheckInPage(page, rect))
                    {
                        return page;
                    }

                    //기존에 있던 이미지의 하단에 새로운 이미지를 붙일수 있는지 검사
                    rect._posX = image.Rect._posX;
                    rect._posY = image.Rect._posY + image.Rect._height;

                    if (false == CollisionCheckInPage(page, rect))
                    {
                        return page;
                    }
                }
            }

            //적절한 빈공간을 찾지 못한 경우 새로운 페이지를 추가한다.
            // (0,0)으로 위치 지정
            rect._posX = 0;
            rect._posY = 0;

            //새로운 페이지 추가
            insertPage = pageCount + 1;

            _remainAreaSize.Add(insertPage, (_atlasHeight * _atlasWidth));
            _images.Add(insertPage, new List<AtlasImage>());
            _bitmaps.Add(insertPage, new Bitmap(2048, 2048));
            _bitmapsForSave.Add(insertPage, new Bitmap(2048, 2048));
            _currentImage.Add(insertPage, null);

            //buttonList에 새로운 페이지 추가
            _buttonList.AddButton(insertPage.ToString());
            return insertPage;
        }

        //아틀라스 텍스쳐의 범위 밖으로 나가지 않는지 검사
        private bool ValidateRect(MyRectangle rect)
        {
            if (_atlasWidth < rect._posX + rect._width ||
                _atlasHeight < rect._posY + rect._height)
            {
                return false;
            }

            return true;
        }

        //해방페이지에 사각형이 들어갈 공간이 있는지 검사
        private bool CollisionCheckInPage(int page, MyRectangle rect)
        {
            foreach (var image in _images[page])
            {
                if (true == rect.CollisionCheck(image.Rect))
                {
                    return true;
                }
            }

            if (false == ValidateRect(rect))
            {
                return true;
            }

            return false;
        }

        private void Open()
        {
            menuStrip.Dock = DockStyle.Top;
            this.Controls.Add(menuStrip);
            _buttonList = new ButtonList();
            _buttonList.Dock = DockStyle.Fill;
            _buttonList.MyEvent += _buttonList_MyEvent;
            panelButtonList.Controls.Add(_buttonList);
            
            this.AllowDrop  = true;
            this.DragEnter  += MainForm_DragEnter;
            this.DragDrop   += MainForm_DragDrop;
            panelAtlasImage.AllowDrop = true;
            panelAtlasImage.DragEnter   += PanelAtlasImage_DragEnter;
            panelAtlasImage.DragDrop    += PanelAtlasImage_DragDrop;
            panelAtlasImage.MouseClick  += PanelAtlasImage_MouseClick;
            pictureBox.MouseClick       += PictureBox_MouseClick;
        }

        //미리보기에서 눌린 이미지 찾아 활성/비활성화 처리
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int x = e.Location.X;
            int y = e.Location.Y;
            
            if(false == _images.ContainsKey(_currentPage))
            {
                return;
            }

            foreach (var image in _images[_currentPage])
            {
                if (true == image.Rect.CollisionCheck(x, y))
                {
                    if(null != _currentImage[_currentPage])
                    {
                        _currentImage[_currentPage].PrintToImage(false, _bitmaps[_currentPage]);
                    }

                    textBoxInfo.ResetText();
                    textBoxInfo.Text = image.GetImageInfo();
                    image.PrintToImage(true, _bitmaps[_currentPage]);
                    _currentImage[_currentPage] = image;
                }
            }
            pictureBox.Image = _bitmaps[_currentPage];
            Cursor.Current = Cursors.Default;
        }

        private void PanelAtlasImage_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox_MouseClick(sender, e);
        }

        private void PanelAtlasImage_DragDrop(object sender, DragEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string[] files = (string[])(e.Data.GetData(DataFormats.FileDrop));

            string extension = "";

            List<Thread> threads = new List<Thread>();
            List<AtlasImage> newImages = new List<AtlasImage>();

            foreach (string file in files)
            {
                extension = new System.IO.FileInfo(file).Extension;
                if(".png" != extension)
                {
                    MessageBox.Show(".png 파일을 이용해주세요.");
                    continue;
                }
                //InsertImageToList(file);

                AtlasImage atlasImage = new AtlasImage();
                Thread thread = new Thread(() => CreateImage(file, atlasImage));
                thread.Start();
                threads.Add(thread);

                newImages.Add(atlasImage);
            }

            foreach(var thread in threads)
            {
                thread.Join();
            }

            foreach(AtlasImage image in newImages)
            {
                InsertImageToList(image);
            }

            Cursor.Current = Cursors.Default;
        }

        private void PanelAtlasImage_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            PanelAtlasImage_DragDrop(sender, e);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            PanelAtlasImage_DragEnter(sender, e);
        }

        private void _buttonList_MyEvent(object sender, EventArgs e)
        {
            Button button = sender as Button;
            
            int page = Convert.ToInt32(button.Tag);
            ChangeCurrentPage(page);
        }

        private void SaveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement body = doc.CreateElement(string.Empty, "body", string.Empty);
            doc.AppendChild(body);

            foreach (var image in _images)
            {
                //아틀라스 이미지 .bmp로 저장
                string path = Environment.CurrentDirectory;
                path += ("\\AtlasImage" + image.Key + ".bmp");
                _bitmapsForSave[image.Key].Save(path, ImageFormat.Bmp);


                XmlElement page = doc.CreateElement(string.Empty, "page", string.Empty);
                XmlText pageNumber = doc.CreateTextNode(image.Key.ToString());
                page.AppendChild(pageNumber);
                body.AppendChild(page);
                //정보를 xml로 저장
                foreach (AtlasImage atlasImage in image.Value)
                {
                    //atlasImage.PrintImageForSave(_bitmapsForSave[image.Key]);
                    XmlElement part = doc.CreateElement("Image");
                    page.AppendChild(part);

                    string imagePath = atlasImage.Path;
                    XmlElement pathElement = doc.CreateElement(string.Empty, "path", string.Empty);
                    XmlText pathText = doc.CreateTextNode(imagePath);
                    part.AppendChild(pathElement);
                    pathElement.AppendChild(pathText);

                    var rect = atlasImage.Rect;
                    
                    XmlElement widthElement = doc.CreateElement(string.Empty, "Width", string.Empty);
                    XmlText widthText = doc.CreateTextNode(rect._width.ToString());
                    part.AppendChild(widthElement);
                    widthElement.AppendChild(widthText);
                    
                    XmlElement heightElement = doc.CreateElement(string.Empty, "Height", string.Empty);
                    XmlText heightText = doc.CreateTextNode(rect._height.ToString());
                    part.AppendChild(heightElement);
                    heightElement.AppendChild(heightText);
                    
                    XmlElement locationElement = doc.CreateElement(string.Empty, "Location", string.Empty);
                    part.AppendChild(locationElement);
                    
                    XmlElement xElement = doc.CreateElement(string.Empty, "X", string.Empty);
                    XmlText xText = doc.CreateTextNode(rect._posX.ToString());
                    locationElement.AppendChild(xElement);
                    xElement.AppendChild(xText);

                    XmlElement yElement = doc.CreateElement(string.Empty, "Y", string.Empty);
                    XmlText yText = doc.CreateTextNode(rect._posY.ToString());
                    locationElement.AppendChild(yElement);
                    yElement.AppendChild(yText);
                }
            }

            string xmlPaht = Environment.CurrentDirectory;
            xmlPaht += ("\\atlasInfo.xml");
            doc.Save(xmlPaht);

            System.Diagnostics.Process.Start(Environment.CurrentDirectory);
        }
    }
}
