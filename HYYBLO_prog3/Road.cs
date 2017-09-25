using System;

namespace HYYBLO_prog3
{
    class Road : MapItem
    {
        int orientation;
        public Road(int x, int y, int _orientation) : base(x, y)
        {
            this.orientation = _orientation;
            /*switch (orientation)
            {
                case 1:
                    Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/HYYBLO_prog3/HYYBLO_prog3/Images/cityroadright.png"));
                    break;
                case 2:
                    Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/HYYBLO_prog3/HYYBLO_prog3/Images/cityroadleft.png"));
                    break;
                default:
                    Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/HYYBLO_prog3/HYYBLO_prog3/Images/cityroadright.png"));
                    break;
            }*/
        }

        public Direction Orientation
        {
            get
            {
                return orientation;
            }

            set
            {
                orientation = value;
            }
        }
    }
}
