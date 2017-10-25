using System;

namespace HYYBLO_prog3
{
    /// <summary>
    /// Abstract class of the Building
    /// </summary>
    public class Building : MapItem
    {
        /// <summary>
        /// Constructor of the Building
        /// </summary>
        /// <param name="x">X coordinate of the building</param>
        /// <param name="y">Y coordinate of the building</param>
        public Building(int x, int y) : base(x, y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/base.png"));
        }
    }
}
