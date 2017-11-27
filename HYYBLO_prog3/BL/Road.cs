//-----------------------------------------------------------------------
// <copyright file="Road.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represent a Road tile
    /// </summary>
    public class Road : MapItem
    {
        /// <summary>
        /// Price of a road
        /// </summary>
        public const int Price = 100;

        /// <summary>
        /// A value to store the index of the current image of the road collection
        /// </summary>
        private int imageNum;

        /// <summary>
        /// Reference to the map of the game
        /// </summary>
        private Map map;

        /// <summary>
        /// Initializes a new instance of the <see cref="Road"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the road</param>
        /// <param name="y">Y coordinate of the road</param>
        /// <param name="map">Map of the game</param>
        public Road(int x, int y, Map map)
            : base(x, y)
        {
            this.map = map;
            this.map.RoadPlaced += this.RoadChanged;
            this.ImageNum = 0;
        }

        /// <summary>
        /// Gets or sets the image of the road
        /// </summary>
        public override BitmapImage Image
        {
            get
            {
                return GameView.RoadImages[this.ImageNum];
            }

            protected set
            {
                base.Image = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of the road Image
        /// </summary>
        protected int ImageNum
        {
            get
            {
                return this.imageNum;
            }

            set
            {
                this.imageNum = value;
            }
        }

        /// <summary>
        /// Callback for the event, changes the images by the number of neighbouring road tiles
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Arguments of the event</param>
        protected virtual void RoadChanged(object sender, EventArgs e)
        {
            int neighbours = this.CheckNeighbours();
            if (this.ImageNum != neighbours)
            {
                this.ImageNum = neighbours;
            }
        }

        /// <summary>
        /// Checks the neighbouring road tiles
        /// </summary>
        /// <returns>Returns a number between 0 and 15 according to the number of neighbouring roads</returns>
        private int CheckNeighbours()
        {
            int d = 0;
            int c = 0;
            int b = 0;
            int a = 0;

            d = (this.map.GetItemByCoord((int)this.X, (int)this.Y - 1) is Road) ? 1 : 0;
            c = (this.map.GetItemByCoord((int)this.X + 1, (int)this.Y) is Road) ? 1 : 0;
            b = (this.map.GetItemByCoord((int)this.X, (int)this.Y + 1) is Road) ? 1 : 0;
            a = (this.map.GetItemByCoord((int)this.X - 1, (int)this.Y) is Road) ? 1 : 0;

            return (8 * d) + (4 * c) + (2 * b) + (1 * a);
        }
    }
}
