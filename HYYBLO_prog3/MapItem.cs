//-----------------------------------------------------------------------
// <copyright file="MapItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Windows;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Abstract class for items in the map
    /// </summary>
    public class MapItem : IComparable<MapItem>
    {
        private double x; // X coordinate of the item
        private double y; // Y coordinate of the item
        private int gCost;
        private int hCost;
        private MapItem parent;

        private BitmapImage image; // image for the item which is displayed on the screen

        /// <summary>
        /// Initializes a new instance of the <see cref="MapItem"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the item</param>
        /// <param name="y">Y coordinate of the item</param>
        public MapItem(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.gCost = 0;
            this.hCost = 0;
            this.parent = null;
        }

        /// <summary>
        /// Gets or sets x coordinate of the item
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets y coordinate of the item
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets image of the item
        /// </summary>
        public int FCost
        {
            get
            {
                return this.GCost + this.HCost;
            }
        }

        /// <summary>
        /// Gets or sets image of the item
        /// </summary>
        public virtual BitmapImage Image
        {
            get
            {
                return this.image;
            }

            protected set
            {
                this.image = value;
            }
        }

        /// <summary>
        /// Gets or sets gCost of the item
        /// </summary>
        public int GCost
        {
            get
            {
                return this.gCost;
            }

            set
            {
                this.gCost = value;
            }
        }

        /// <summary>
        /// Gets or sets hCost of the item
        /// </summary>
        public int HCost
        {
            get
            {
                return this.hCost;
            }

            set
            {
                this.hCost = value;
            }
        }

        /// <summary>
        /// Gets or sets parent of the item
        /// </summary>
        public MapItem Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                this.parent = value;
            }
        }

        /// <summary>
        /// Checks if the two items aren't equals
        /// </summary>
        /// <param name="m1">First Item to compare</param>
        /// <param name="m2">Second Item to compare</param>
        /// <returns>Returns true if the objects aren't equals</returns>
        public static bool operator !=(MapItem m1, MapItem m2)
        {
            return m1.X != m2.X || m1.Y != m2.Y;
        }

        /// <summary>
        /// Checks if the two items are euqals
        /// </summary>
        /// <param name="m1">First Item to compare</param>
        /// <param name="m2">Second Item to compare</param>
        /// <returns>Returns true if the objects are equals</returns>
        public static bool operator ==(MapItem m1, MapItem m2)
        {
            return m1.X == m2.X && m1.Y == m2.Y;
        }

        /// <summary>
        /// Checks if the first item is smaller than the second
        /// </summary>
        /// <param name="m1">First Item to compare</param>
        /// <param name="m2">Second Item to compare</param>
        /// <returns>Returns true if the first object is smaller than the second</returns>
        public static bool operator <(MapItem m1, MapItem m2)
        {
            if (m1.Y < m2.Y)
            {
                return true;
            }
            else if (m1.X < m2.X)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the first item is larger than the second
        /// </summary>
        /// <param name="m1">First Item to compare</param>
        /// <param name="m2">Second Item to compare</param>
        /// <returns>Returns true if the first object is larger than the second</returns>
        public static bool operator >(MapItem m1, MapItem m2)
        {
            if (m1.Y > m2.Y)
            {
                return true;
            }
            else if (m1.X > m2.X)
            {
                return true;
            }
            else
            {
                return false;
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

        /// <summary>
        /// Compares the object to the parameter
        /// </summary>
        /// <param name="other">Object to be compared to</param>
        /// <returns>Returns -1 if the object is smaller, returns 1 if the object is bigger, otherwise returns 0</returns>
        public virtual int CompareTo(MapItem other)
        {
            if (other.Y > this.Y)
            {
                return -1;
            }
            else if (other.Y < this.Y)
            {
                return 1;
            }
            else if (other.X > this.X)
            {
                return -1;
            }
            else if (other.X < this.X)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Checks if the other item is equal to this
        /// </summary>
        /// <param name="o">other item</param>
        /// <returns>Returns true if the coordinates are equal</returns>
        public override bool Equals(object o)
        {
            return this.X == ((MapItem)o).X && this.Y == ((MapItem)o).Y;
        }

        /// <summary>
        /// The hash code of the item
        /// </summary>
        /// <returns>Returns the hash code</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
