using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MapItem(int _x, int _y)
        {
            this.X = _x;
            this.Y = _y;
        }
    }
}
