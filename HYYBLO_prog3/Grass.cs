using System;

namespace HYYBLO_prog3
{
    class Grass : MapItem
    {
        public Grass(int _x, int _y) : base(_x, _y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(GameView.GetImage("Images/grass_sm.png")));
        }
    }
}
