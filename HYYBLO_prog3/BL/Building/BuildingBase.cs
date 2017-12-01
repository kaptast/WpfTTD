//-----------------------------------------------------------------------
// <copyright file="BuildingBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;

    /// <summary>
    /// Represents the base of a building
    /// </summary>
    public class BuildingBase : Building
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingBase"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the building</param>
        /// <param name="y">Y coordinate of the building</param>
        public BuildingBase(int x, int y)
            : base(x, y)
        {
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(Hyyblo_View.GameView.GetImage("Images/Buildings/base.png")));
        }
    }
}
