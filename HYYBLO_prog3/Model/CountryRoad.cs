//-----------------------------------------------------------------------
// <copyright file="CountryRoad.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Windows.Media.Imaging;
    using Hyyblo_Logic;

    /// <summary>
    /// Represent a Road tile
    /// </summary>
    public class CountryRoad : Road
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRoad"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the road</param>
        /// <param name="y">Y coordinate of the road</param>
        /// <param name="map">Map of the game</param>
        public CountryRoad(int x, int y, Map map)
            : base(x, y, map)
        {
            this.SpeedMultiplier = 1.5;
        }

        /// <summary>
        /// Gets or sets the image of the road
        /// </summary>
        public override BitmapImage Image
        {
            get
            {
                return Hyyblo_View.GameView.CountryRoadImages[this.ImageNum];
            }

            protected set
            {
                base.Image = value;
            }
        }
    }
}
