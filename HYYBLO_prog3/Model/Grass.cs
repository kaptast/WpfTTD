//-----------------------------------------------------------------------
// <copyright file="Grass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;

    /// <summary>
    /// Grass item
    /// </summary>
    public sealed class Grass : MapItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Grass"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the item</param>
        /// <param name="y">Y coordinate of the item</param>
        public Grass(int x, int y)
            : base(x, y)
        {
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(Hyyblo_View.GameView.GetImage("Images/grass_sm.png")));
        }
    }
}
