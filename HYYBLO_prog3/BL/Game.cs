//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Game object, contains the current game's values
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Stores the prices of the different types of wares
        /// </summary>
        private static Dictionary<WareType, int> prices;

        /// <summary>
        /// Map of the game
        /// </summary>
        private Map map;

        /// <summary>
        /// Money of the player
        /// </summary>
        private int money;

        /// <summary>
        /// Collection to store warehouses built in the game
        /// </summary>
        private List<Warehouse> warehouses;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.map = new Map(40);
            this.map.RoadPlaced += this.Map_RoadPlaced;
            this.map.WarehousePlaced += this.Map_WarehousePlaced;
            this.Money = 20000;
            this.warehouses = new List<Warehouse>();

            prices = new Dictionary<WareType, int>();
            prices.Add(WareType.Goods, 100);
            prices.Add(WareType.Nothing, 0);
            prices.Add(WareType.Mail, 20);
            prices.Add(WareType.Ore, 70);
        }

        /// <summary>
        /// Gets the Price table
        /// </summary>
        public static Dictionary<WareType, int> Prices
        {
            get
            {
                return prices;
            }
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
        /// Gets the warehouse list of the game
        /// </summary>
        public List<Warehouse> Warehouses
        {
            get
            {
                return this.warehouses;
            }
        }

        /// <summary>
        /// Finds a warehouse with correct position
        /// </summary>
        /// <param name="x">X coordinate of the requested item</param>
        /// <param name="y">Y coordinate of the requested item</param>
        /// <returns>A found requested warehouse</returns>
        public Warehouse FindWarehouseByPosition(int x, int y)
        {
            foreach (Warehouse wh in this.Warehouses)
            {
                if (wh.X == x && wh.Y == y)
                {
                    return wh;
                }
            }

            return null;
        }

        /// <summary>
        /// Updates the elements in the game
        /// </summary>
        public void Update()
        {
            this.Map.UpdateVehicles();
        }

        /// <summary>
        /// Places a road on the map
        /// </summary>
        /// <param name="x">X coordinate of the road</param>
        /// <param name="y">Y coordinate of the road</param>
        public void SetRoad(int x, int y)
        {
            if (this.Money > 0)
            {
                this.Map.SetRoad(x, y);
                this.Map.FireRoadPlaced(this, new EventArgs()); // Check road tiles
            }
        }

        /// <summary>
        /// Adds a vehicle to the map
        /// </summary>
        /// <param name="x">X coordinate of the vehicle</param>
        /// <param name="y">Y coordinate of the vehicle</param>
        /// <param name="start">Staring warehouse of the vehicle</param>
        /// <param name="final">Target warehouse of the vehicle</param>
        public void SetVehicle(int x, int y, Warehouse start, Warehouse final)
        {
            this.Map.AddVehicle(x, y, start, final, this);
        }

        /// <summary>
        /// Places a warehouse on the map
        /// </summary>
        /// <param name="x">X coordinate of the warehouse</param>
        /// <param name="y">Y coordinate of the warehouse</param>
        public void SetWarehouse(int x, int y)
        {
            if (this.Money > 0)
            {
                this.Map.SetWarehouse(x, y);
                this.Warehouses.Add(new Warehouse(x, y, this));
            }
        }

        /// <summary>
        /// Creates a string with game's information
        /// </summary>
        /// <returns>A string with the game's information</returns>
        public override string ToString()
        {
            return string.Format("Money: {0}", this.Money);
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
