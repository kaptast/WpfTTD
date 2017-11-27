//-----------------------------------------------------------------------
// <copyright file="WarehouseBuilding.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;

    /// <summary>
    /// Represents the building of the Warehouse
    /// </summary>
    public sealed class WarehouseBuilding : Building
    {
        /// <summary>
        /// Price of a warehouse
        /// </summary>
        public const int Price = 1000;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseBuilding"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the building</param>
        /// <param name="y">Y coordinate of the building</param>
        public WarehouseBuilding(int x, int y)
            : base(x, y)
        {
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(GameView.GetImage("Images/Buildings/warehouse.png")));
        }
    }
}
