using System.Windows;
using System.Windows.Media.Imaging;

namespace HYYBLO_prog3
{
    /// <summary>
    /// Abstract class for items in the map
    /// </summary>
    class MapItem
    {
        int x, y; //x and y coordinates for the item
        BitmapImage image; //image for the item which is displayed on the screen

        /// <summary>
        /// X coordinate of the item
        /// </summary>
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

        /// <summary>
        /// Y coordinate of the item
        /// </summary>
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

        /// <summary>
        /// Image of the item
        /// </summary>
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

        /// <summary>
        /// Generates a rectangle where the image will be rendered
        /// </summary>
        /// <param name="x">Screen X coordinate of the Rectangle</param>
        /// <param name="y">Screen Y coordinate of the Rectangle</param>
        /// <param name="cell">Height and width of the Rectangle</param>
        /// <returns>Rectangel with the parameters</returns>
        public virtual Rect GenerateRect(int x, int y, int cell)
        {
            return new Rect(x, y, cell, cell);
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
}
