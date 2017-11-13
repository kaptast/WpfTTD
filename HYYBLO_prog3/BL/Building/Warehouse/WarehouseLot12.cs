﻿//-----------------------------------------------------------------------
// <copyright file="WarehouseLot12.cs" company="PlaceholderCompany">
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
    public class WarehouseLot12 : Building
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseLot12"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the lot</param>
        /// <param name="y">Y coordinate of the lot</param>
        public WarehouseLot12(int x, int y)
            : base(x, y)
        {
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(GameView.GetImage("Images/Buildings/warehouse12.png")));
        }
    }
}