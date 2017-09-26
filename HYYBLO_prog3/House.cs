using System;

namespace HYYBLO_prog3
{
    class House : Building
    {
        public House(int x, int y, Random r) : base(x, y)
        {
            int n = r.Next(1, 5);
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/house" + n + ".png"));
        }
    }
}
