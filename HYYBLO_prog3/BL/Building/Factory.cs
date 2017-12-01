//-----------------------------------------------------------------------
// <copyright file="WarehouseBuilding.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Windows;

    /// <summary>
    /// Represents a factory of the game
    /// </summary>
    public class Factory : Building
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Factory"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the factory</param>
        /// <param name="y">Y coordinate of the factory</param>
        public Factory(int x, int y)
            : base(x, y)
        {
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(GameView.GetImage("Images/Buildings/factory.png")));
        }

        /// <summary>
        /// Generates a rectangle for the house by the GameView's actual state
        /// </summary>
        /// <param name="x">X coordinate of the rectangle's top left corner</param>
        /// <param name="y">Y coordinate of the rectangle's top left corner</param>
        /// <param name="cell">Current cell size of the view</param>
        /// <returns>A rectangle with the buildings size at the correct position</returns>
        public override Rect GenerateRect(double x, double y, int cell)
        {
            return new Rect(x, y - (cell / 4), cell, cell + (cell / 4));
        }
    }
}
