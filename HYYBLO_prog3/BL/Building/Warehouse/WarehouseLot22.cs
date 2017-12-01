//-----------------------------------------------------------------------
// <copyright file="WarehouseLot22.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;

    /// <summary>
    /// Represents a parking lot of the Warehouse
    /// </summary>
    public sealed class WarehouseLot22 : MapItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseLot22"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the lot</param>
        /// <param name="y">Y coordinate of the lot</param>
        public WarehouseLot22(int x, int y)
            : base(x, y)
        {
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(Hyyblo_View.GameView.GetImage("Images/Buildings/warehouse22.png")));
        }
    }
}
