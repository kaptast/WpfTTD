using System;

namespace HYYBLO_prog3
{
    class Grass : MapItem
    {
        public Grass(int _x, int _y) : base(_x, _y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/HYYBLO_prog3/HYYBLO_prog3/Images/grass_sm.png"));
        }
    }
}
