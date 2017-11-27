//-----------------------------------------------------------------------
// <copyright file="Map.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Delegate for a placed road
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Arguments of the road placing</param>
    public delegate void ItemPlacedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Represents the map of the game
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Random generator of the map
        /// </summary>
        private static Random r;

        /// <summary>
        /// Collection of MapItems on the map
        /// </summary>
        private List<MapItem> mapContainer;

        /// <summary>
        /// Collection of Buildings on the map
        /// </summary>
        private List<Building> buildings;

        /// <summary>
        /// Collection of vehicles on the map
        /// </summary>
        private List<Vehicle> vehicles;

        /// <summary>
        /// Pathfinder object for the map
        /// </summary>
        private Pathfinding pathfinder;

        /// <summary>
        /// The width and height of the map
        /// </summary>
        private int size;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="size">Size of the map</param>
        public Map(int size)
        {
            this.mapContainer = new List<MapItem>();
            this.vehicles = new List<Vehicle>();
            this.buildings = new List<Building>();
            r = new Random();
            this.size = size;
            this.Pathfinder = new Pathfinding(this);
            this.GenerateMap(size, size);

            this.mapContainer.Sort();
        }

        /// <summary>
        /// Event for a placed road
        /// </summary>
        public event ItemPlacedEventHandler RoadPlaced;

        /// <summary>
        /// Event for a placed warehouse
        /// </summary>
        public event ItemPlacedEventHandler WarehousePlaced;

        /// <summary>
        /// Gets the Container of the MapItems in the Map
        /// </summary>
        public List<MapItem> MapContainer
        {
            get
            {
                return this.mapContainer;
            }
        }

        /// <summary>
        /// Gets or sets the Container of the Vehicles
        /// </summary>
        public List<Vehicle> Vehicles
        {
            get
            {
                return this.vehicles;
            }

            set
            {
                this.vehicles = value;
            }
        }

        /// <summary>
        /// Gets or sets the Container of the Buildings
        /// </summary>
        public List<Building> Buildings
        {
            get
            {
                return this.buildings;
            }

            set
            {
                this.buildings = value;
            }
        }

        /// <summary>
        /// Gets size of the map
        /// </summary>
        public int Size
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// Gets or sets the Pathfinder of the map
        /// </summary>
        public Pathfinding Pathfinder
        {
            get
            {
                return this.pathfinder;
            }

            set
            {
                this.pathfinder = value;
            }
        }

        /// <summary>
        /// Generates a random number along a curve
        /// </summary>
        /// <param name="min">Minimum random value</param>
        /// <param name="max">Maximum random value</param>
        /// <param name="tightness">Tightness of the value</param>
        /// <param name="exp">Power of the random value</param>
        /// <returns>Random number in range which gravitates towards the given value</returns>
        public static double RandomNormalDist(double min, double max, int tightness, double exp)
        {
            double total = 0.0;
            for (int i = 1; i <= tightness; i++)
            {
                total += Math.Pow(r.NextDouble(), exp);
            }

            return ((total / tightness) * (max - min)) + min;
        }

        /// <summary>
        /// Placed a road on the map
        /// </summary>
        /// <param name="x">X coordinate of the road</param>
        /// <param name="y">Y coordinate of the road</param>
        public void SetRoad(int x, int y)
        {
            if (this.RightCoord(x, y))
            {
                MapItem item = this.GetItemByCoord(x, y);
                this.MapContainer.Remove(item);
                item = this.GetBuildingByCoord(x, y);
                this.Buildings.Remove((Building)item);
                this.MapContainer.Add(new Road(x, y, this));
            }
        }

        /// <summary>
        /// Places a Warehouse on the map
        /// </summary>
        /// <param name="x">X coordinate of the warehouse</param>
        /// <param name="y">Y coordinate of the warehouse</param>
        public void SetWarehouse(int x, int y)
        {
            if (this.RightCoord(x, y))
            {
                MapItem item = this.GetItemByCoord(x, y);
                this.MapContainer.Remove(item);
                this.MapContainer.Add(new BuildingBase(x, y));
                this.Buildings.Add(new WarehouseBuilding(x, y));

                item = this.GetItemByCoord(x, y + 1);
                this.MapContainer.Remove(item);
                this.MapContainer.Add(new WarehouseLot12(x, y + 1));

                item = this.GetItemByCoord(x + 1, y);
                this.MapContainer.Remove(item);
                this.MapContainer.Add(new WarehouseLot21(x + 1, y, this));

                item = this.GetItemByCoord(x + 1, y + 1);
                this.MapContainer.Remove(item);
                this.MapContainer.Add(new WarehouseLot22(x + 1, y + 1));

                if (this.WarehousePlaced != null)
                {
                    this.WarehousePlaced.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Deletes an item, and places a grass item in it's place
        /// </summary>
        /// <param name="x">X coordinate of the delete</param>
        /// <param name="y">Y coordinate of the delete</param>
        public void SetDelete(int x, int y)
        {
            if (this.RightCoord(x, y))
            {
                MapItem item = this.GetItemByCoord(x, y);
                this.MapContainer.Remove(item);
                item = this.GetBuildingByCoord(x, y);
                this.Buildings.Remove((Building)item);
                this.MapContainer.Add(new Grass(x, y));
            }
        }

        /// <summary>
        /// Checks if the requested coordinates are in the bounds
        /// </summary>
        /// <param name="x">Requested X coordinate</param>
        /// <param name="y">Requested Y coordinate</param>
        /// <returns>Returns true if the coordinates are in the bounds</returns>
        public bool RightCoord(int x, int y)
        {
            return x >= 0 && x < this.Size && y >= 0 && y < this.Size;
        }

        /// <summary>
        /// Fires the RoadPlaced event
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Arguments of the event</param>
        public void FireRoadPlaced(object sender, EventArgs e)
        {
            if (this.RoadPlaced != null)
            {
                this.RoadPlaced.Invoke(sender, e);
            }
        }

        /// <summary>
        /// Searches for an item with the right coordinates
        /// </summary>
        /// <param name="x">X coordinate of the item</param>
        /// <param name="y">Y coordinate of the item</param>
        /// <returns>The item at the requested position</returns>
        public MapItem GetItemByCoord(double x, double y)
        {
            foreach (MapItem item in this.MapContainer)
            {
                if (item.X == x && item.Y == y)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Searches for a building with the right coordinates
        /// </summary>
        /// <param name="x">X coordinate of the building</param>
        /// <param name="y">Y coordinate of the building</param>
        /// <returns>The building at the requested position</returns>
        public MapItem GetBuildingByCoord(double x, double y)
        {
            foreach (MapItem item in this.Buildings)
            {
                if (item.X == x && item.Y == y)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds a vehicle at the position
        /// </summary>
        /// <param name="x">X coordinate of the vehicle</param>
        /// <param name="y">Y coordinate of the vehicle</param>
        public void AddVehicle(int x, int y)
        {
            if (this.RightCoord(x, y))
            {
                Vehicle v = new Vehicle(x, y, this);
                this.Vehicles.Add(v);
            }
        }

        /// <summary>
        /// Updates all vehicles on the map
        /// </summary>
        public void UpdateVehicles()
        {
            for (int i = 0; i < this.Vehicles.Count; i++)
            {
                this.Vehicles[i].Update();
            }
        }

        /// <summary>
        /// Gets the neighbours of an item
        /// </summary>
        /// <param name="item">Item to search</param>
        /// <returns>List with the neighbours</returns>
        public List<MapItem> GetNeighbours(MapItem item)
        {
            List<MapItem> list = new List<MapItem>();
            list.Add(this.GetItemByCoord(item.X, item.Y + 1));
            list.Add(this.GetItemByCoord(item.X, item.Y - 1));
            list.Add(this.GetItemByCoord(item.X + 1, item.Y));
            list.Add(this.GetItemByCoord(item.X - 1, item.Y));
            return list;
        }

        /// <summary>
        /// Generates a map with the given size
        /// </summary>
        /// <param name="x">Width of the map</param>
        /// <param name="y">Height of the map</param>
        private void GenerateMap(int x, int y)
        {
            this.GenerateGrass(x, y);

            this.GenerateTowns();

            this.GenerateBuildings(x, y);

            this.FireRoadPlaced(this, new EventArgs());
        }

        /// <summary>
        /// Generates 5 to 10 towns
        /// </summary>
        private void GenerateTowns()
        {
            int townNum = r.Next(5, 10);
            for (int i = 0; i < townNum; i++)
            {
                this.GenerateTown();
            }
        }

        /// <summary>
        /// Generates a town on the map at a random location
        /// </summary>
        private void GenerateTown()
        {
            int roadNum = r.Next(4, 8);
            Point townCenter = new Point(r.Next(this.Size), r.Next(this.Size));

            this.SetRoad((int)townCenter.X, (int)townCenter.Y);

            for (int i = 0; i < roadNum; i++)
            {
                int centerDist = r.Next(-4, 4);
                int orientation = (i % 2) == 0 ? 1 : 2;
                int roadLength = r.Next(3, 5);
                if (orientation == 1)
                {
                    this.GenerateRoad((int)townCenter.X, (int)townCenter.Y + centerDist, (int)townCenter.X, (int)townCenter.Y + roadLength);
                }
                else
                {
                    this.GenerateRoad((int)townCenter.X + centerDist, (int)townCenter.Y, (int)townCenter.X + roadLength, (int)townCenter.Y);
                }
            }
        }

        /// <summary>
        /// Fills the map Grass tiles
        /// </summary>
        /// <param name="x">Width of the map</param>
        /// <param name="y">Height of the map</param>
        private void GenerateGrass(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    this.MapContainer.Add(new Grass(i, j));
                }
            }
        }

        /// <summary>
        /// Generates houses near the roads
        /// </summary>
        /// <param name="x">Width of the map</param>
        /// <param name="y">Height of the map</param>
        private void GenerateBuildings(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (this.RoadIsNeighbour(i, j))
                    {
                        this.GenHouse(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Generates a house at the given location with a 40% chance
        /// </summary>
        /// <param name="x">X coordinate of the house</param>
        /// <param name="y">Y coordinate of the house</param>
        private void GenHouse(int x, int y)
        {
            if (this.RightCoord(x, y))
            {
                int percent = r.Next(101);
                if (percent < 40)
                {
                    MapItem item = this.GetItemByCoord(x, y);
                    this.MapContainer.Remove(item);
                    this.MapContainer.Add(new BuildingBase(x, y));
                    this.Buildings.Add(new House(x, y, r));
                }
            }
        }

        /// <summary>
        /// Checks if there is a neighbouring road
        /// </summary>
        /// <param name="x">X coordinate to check</param>
        /// <param name="y">Y coordinate to check</param>
        /// <returns>Returns true if there is a neighbouring road, and false if there isn't</returns>
        private bool RoadIsNeighbour(int x, int y)
        {
            if (x > 0 && x < this.Size - 1 && y > 0 && y < this.Size - 1)
            {
                if (!(this.GetItemByCoord(x, y) is Road) && (this.GetItemByCoord(x - 1, y) is Road || this.GetItemByCoord(x, y - 1) is Road || this.GetItemByCoord(x + 1, y) is Road || this.GetItemByCoord(x, y + 1) is Road))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Generates road between two points
        /// </summary>
        /// <param name="x1">X coordinate of the first point</param>
        /// <param name="y1">Y coordinate of the first point</param>
        /// <param name="x2">X coordinate of the second point</param>
        /// <param name="y2">Y coordinate of the second point</param>
        private void GenerateRoad(int x1, int y1, int x2, int y2)
        {
            if (x1 < x2 && y1 == y2)
            {
                for (int i = x1; i < x2; i++)
                {
                    this.SetRoad(i, y1);
                }
            }
            else if (x1 > x2 && y1 == y2)
            {
                for (int i = x1; i > x2; i--)
                {
                    this.SetRoad(i, y1);
                }
            }
            else if (y1 < y2 && x1 == x2)
            {
                for (int i = y1; i < y2; i++)
                {
                    this.SetRoad(x1, i);
                }
            }
            else if (y1 > y2 && x1 == x2)
            {
                for (int i = y1; i > y2; i--)
                {
                    this.SetRoad(x1, i);
                }
            }
        }
    }
}
