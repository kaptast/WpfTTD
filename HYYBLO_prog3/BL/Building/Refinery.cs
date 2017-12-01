//-----------------------------------------------------------------------
// <copyright file="Refinery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Windows;

    /// <summary>
    /// Represents a refinery of the game
    /// </summary>
    public class Refinery : Building
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Refinery"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the refinery</param>
        /// <param name="y">Y coordinate of the refinery</param>
        public Refinery(int x, int y)
            : base(x, y)
        {
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(Hyyblo_View.GameView.GetImage("Images/Buildings/refinery.png")));
        }
    }
}
