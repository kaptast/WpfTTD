using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HYYBLO_prog3
{
    /// <summary>
    /// Abstract class for items in the map
    /// </summary>
    public class MapItem : IComparable
    {
        double x, y; //x and y coordinates for the item

        public int gCost;
        public int hCost;
        public MapItem parent;

        BitmapImage image; //image for the item which is displayed on the screen

        /// <summary>
        /// X coordinate of the item
        /// </summary>
        public double X
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

        /// <summary>
        /// Y coordinate of the item
        /// </summary>
        public double Y
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

        /// <summary>
        /// Image of the item
        /// </summary>
        public virtual BitmapImage Image
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

        /// <summary>
        /// Generates a rectangle where the image will be rendered
        /// </summary>
        /// <param name="x">Screen X coordinate of the Rectangle</param>
        /// <param name="y">Screen Y coordinate of the Rectangle</param>
        /// <param name="cell">Height and width of the Rectangle</param>
        /// <returns>Rectangel with the parameters</returns>
        public virtual Rect GenerateRect(double x, double y, int cell)
        {
            return new Rect(x, y, cell, cell);
        }

        public int CompareTo(object obj)
        {
            if((obj as MapItem).Y > this.Y)
            {
                return -1;
            }
            else if ((obj as MapItem).Y < this.Y)
            {
                return 1;
            }
            else if ((obj as MapItem).X > this.X)
            {
                return -1;
            }
            else if ((obj as MapItem).X < this.X)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        /// <summary>
        /// Constructor of MapItem
        /// </summary>
        /// <param name="_x">X coordinate of the item</param>
        /// <param name="_y">Y coordinate of the item</param>
        public MapItem(int _x, int _y)
        {
            this.X = _x;
            this.Y = _y;

        }
    }

    public class Sign : MapItem
    {
        public Sign(int _x, int _y) : base(_x, _y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/sign.png"));
        }
    }

}
