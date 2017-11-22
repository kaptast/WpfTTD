//-----------------------------------------------------------------------
// <copyright file="WarehouseLot21.cs" company="PlaceholderCompany">
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
    public sealed class WarehouseLot21 : Road
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseLot21"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the lot</param>
        /// <param name="y">Y coordinate of the lot</param>
        /// <param name="map">Map of the game</param>
        public WarehouseLot21(int x, int y, Map map)
            : base(x, y, map)
        {
            this.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(GameView.GetImage("Images/Buildings/warehouse21.png")));
        }

        /// <summary>
        /// Overrides the Road's function, it doesn't change the image
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the event</param>
        protected override void RoadChanged(object sender, EventArgs e)
        {
            // Do nothing
        }
    }
}
