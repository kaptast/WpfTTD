using System;

namespace HYYBLO_prog3
{
    class Road : MapItem
    {
        public Road(int x, int y, int orientation) : base(x, y)
        {
            switch (orientation)
            {
                case 1:
                    Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/cityroadright.png"));
                    break;
                case 2:
                    Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/cityroadleft.png"));
                    break;
                default:
                    Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/cityroadright.png"));
                    break;
            }
        }
    }
}
