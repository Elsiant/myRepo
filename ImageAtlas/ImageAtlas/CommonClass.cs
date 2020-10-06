using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageAtlas
{
    public class CommonClass
    {
        public class MyRectangle
        {
            public int _posX;
            public int _posY;
            public int _width;
            public int _height;

            //사각형간의 충돌 검사
            public bool CollisionCheck(MyRectangle rect)
            {
                if (this._posX < rect._posX + rect._width &&
                    rect._posX < this._posX + this._width &&
                    this._posY < rect._posY + rect._height &&
                    rect._posY < this._posY + this._height)
                {
                    return true;
                }

                return false;
            }

            //점과의 충돌 검사
            public bool CollisionCheck(int x, int y)
            {
                return CollisionCheck(new Point(x, y));
            }

            public bool CollisionCheck(Point point)
            {
                if( _posX <= point.X && point.X <= _posX + _width &&
                    _posY <= point.Y && point.Y <= _posY + _height)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public class AtlasImage
        {
            string _path;
            Bitmap _oriImage;           //원래의 이미지
            Bitmap _grayScaleImage;     //비활성화시 보여줄 이미지
            MyRectangle _rect;

            public string Path
            {
                get => _path;
                set => _path = value;
            }
            
            public MyRectangle Rect
            {
                get => _rect;
                set => _rect = value;
            }
            
            public bool Open(string filePath)
            {
                _path = filePath;
                
                _oriImage       = new Bitmap(filePath);
                _rect           = new MyRectangle();
                _rect._width    = _oriImage.Width;
                _rect._height   = _oriImage.Height;

                _grayScaleImage = ImageToGrayScale(_oriImage);

                if(_grayScaleImage == null)
                {
                    return false;
                }

                return true;
            }

            //이미지에 대한 정보를 스트링으로 리턴
            public string GetImageInfo()
            {
                StringBuilder info = new StringBuilder();
                info.Append("경로 : ");
                info.Append(_path);
                info.AppendLine();

                info.Append("너비 : ");
                info.Append(_rect._width);
                info.AppendLine();
                info.Append("높이 : ");
                info.Append(_rect._height);
                info.AppendLine();
                info.Append("위치 : (");
                info.Append(_rect._posX);
                info.Append(", ");
                info.Append(_rect._posY);
                info.Append(")");

                return info.ToString();
            }
            
            public void PrintToImage(bool isActive, Bitmap bitmap)
            {
                Image image;
                if(false == isActive)
                {
                    image = _grayScaleImage;
                }
                else
                {
                    image = _oriImage;
                }

                Bitmap map = (Bitmap)image; //인쇄할 이미지
                for (int x = 0; x < image.Width; x++)
                {
                    for(int y = 0; y < image.Height; y++)
                    {
                        //활성화된 이미지는 테두리를 칠해줌
                        if(true == isActive)
                        {
                            if(x == 0 || y == 0 ||
                               x == image.Width - 1 || y == image.Height - 1 )
                            {
                                bitmap.SetPixel(_rect._posX + x,
                                        _rect._posY + y,
                                        Color.Red);

                                continue;
                            }
                        }

                        bitmap.SetPixel(_rect._posX + x,
                                        _rect._posY + y,
                                        map.GetPixel(x, y));
                    }
                }
            }

            //파일로 저장할 비트맵에 인쇄
            public void PrintImageForSave(Bitmap bitmap)
            {
                Bitmap map = (Bitmap)_oriImage;
                for (int x = 0; x < _oriImage.Width; x++)
                {
                    for (int y = 0; y < _oriImage.Height; y++)
                    {
                        bitmap.SetPixel(_rect._posX + x,
                                        _rect._posY + y,
                                        map.GetPixel(x, y));
                    }
                }
            }
        }

        //비활성화 상태의 이미지를 만들어서 리턴
        public static Bitmap ImageToGrayScale(Bitmap image)
        {
            return ChangeImageARGB(image, 122, 0.2f, 0.5f, 0.1f);
        }

        //이미지의 ARGB값을 수정
        public static Bitmap ChangeImageARGB(Bitmap image, int alpha, float red, float green, float blue)
        {
            Bitmap returnImage = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color   oriColor  = image.GetPixel(x, y);
                    int     grayScale = (int)((oriColor.R * red) + (oriColor.G * green) + (oriColor.B * blue));
                    Color   newColor  = Color.FromArgb(alpha, grayScale, grayScale, grayScale);

                    returnImage.SetPixel(x, y, newColor);
                }
            }

            return returnImage;
        }
    }
}
