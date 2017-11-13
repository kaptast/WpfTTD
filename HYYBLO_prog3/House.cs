//-----------------------------------------------------------------------
// <copyright file="House.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Windows;

    /// <summary>
    /// Represents a house building
    /// </summary>
    public class House : Building
    {
        private int floors;

        /// <summary>
        /// Initializes a new instance of the <see cref="House"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the house</param>
        /// <param name="y">Y coordinate of the house</param>
        /// <param name="r">Random generator</param>
        public House(int x, int y, Random r)
            : base(x, y)
        {
            int n = r.Next(1, 5);
            this.floors = (int)Map.RandomNormalDist(1, 6, 2, 2);
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(GameView.GetImage("Images/Buildings/house" + n + "-" + this.floors + ".png")));
        }

        /// <summary>
        /// Generates a rectangle for the house by the GameView's actual state
        /// </summary>
        /// <param name="x">X coordinate of the rect's top left corner</param>
        /// <param name="y">Y coordinate of the rect's top left corner</param>
        /// <param name="cell">Current cell size of the view</param>
        /// <returns>A rect with the buildings size at the correct position</returns>
        public override Rect GenerateRect(double x, double y, int cell)
        {
            return new Rect(x, y - ((this.floors - 1) * (cell / 4)), cell, cell + ((cell / 4) * (this.floors - 1)));
        }
    }
}
