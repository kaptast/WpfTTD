using System;

namespace HYYBLO_prog3
{
    class House : Building
    {
        public House(int x, int y) : base(x, y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/HYYBLO_prog3/HYYBLO_prog3/Images/house1.png"));
        }
    }
}
