using System.Windows;
using System.Windows.Media.Imaging;

namespace HYYBLO_prog3
{
    class MapItem
    {
        int x, y;
        BitmapImage image;

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public BitmapImage Image
        {
            get
            {
                return image;
            }

            protected set
            {
                image = value;
            }
        }

        public virtual Rect GenerateRect(int x, int y, int cell)
        {
            return new Rect(x, y, cell, cell);
        }

        public MapItem(int _x, int _y)
        {
            this.X = _x;
            this.Y = _y;

        }
    }
}
