//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    /// <summary>
    /// Game object, contains the current game's values
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Map of the game
        /// </summary>
        private Map map;

        /// <summary>
        /// Money of the player
        /// </summary>
        private int money;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.map = new Map(40);
            this.map.RoadPlaced += this.Map_RoadPlaced;
            this.map.WarehousePlaced += this.Map_WarehousePlaced;
            this.Money = 5000;
        }

        /// <summary>
        /// Gets or sets map of the game
        /// </summary>
        public Map Map
        {
            get
            {
                return this.map;
            }

            set
            {
                this.map = value;
            }
        }

        /// <summary>
        /// Gets or sets the money of the player
        /// </summary>
        public int Money
        {
            get
            {
                return this.money;
            }

            set
            {
                this.money = value;
            }
        }

        /// <summary>
        /// Substracts the price of the road from the players money
        /// </summary>
        /// <param name="sender">Sender object of the event</param>
        /// <param name="e">Arguments of the event</param>
        private void Map_RoadPlaced(object sender, System.EventArgs e)
        {
            this.Money -= Road.Price;
        }

        /// <summary>
        /// Substracts the price of the warehouse from the players money
        /// </summary>
        /// <param name="sender">Sender object of the event</param>
        /// <param name="e">Arguments of the event</param>
        private void Map_WarehousePlaced(object sender, System.EventArgs e)
        {
            this.Money -= WarehouseBuilding.Price;
        }

    }
}
