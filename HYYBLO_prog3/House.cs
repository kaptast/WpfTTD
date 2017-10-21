using System;
using System.Windows;

namespace HYYBLO_prog3
{
    class House : Building
    {
        int floors;
        public House(int x, int y, Random r) : base(x, y)
        {
            int n = r.Next(1, 5);
            floors = (int)Map.RandomNormalDist(1, 6, 2, 2);
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/house" + n + "-" + floors + ".png"));
        }

        public override Rect GenerateRect(double x, double y, int cell)
        {
            return new Rect(x, y - (floors - 1) * (cell/4), cell, cell + (cell/4) * (floors - 1));
        }
    }
}
