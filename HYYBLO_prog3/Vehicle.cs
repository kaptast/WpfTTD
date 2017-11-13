//-----------------------------------------------------------------------
// <copyright file="Vehicle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Represents a moving vehicle
    /// </summary>
    public class Vehicle : MapItem
    {
        private List<MapItem> pathToTarget;
        private Direction facing;
        private BitmapImage[] images;
        private MapItem finalTarget;
        private MapItem currentTarget;
        private int targetIdx = 0;
        private double speed = 0.05;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the vehicle</param>
        /// <param name="y">Y coordinate of the vehicle</param>
        /// <param name="map">Map of the game</param>
        public Vehicle(int x, int y, Map map)
            : base(x, y)
        {
            this.facing = Direction.Left;
            this.images = new BitmapImage[4];
            for (int i = 0; i < 4; i++)
            {
                this.images[i] = new BitmapImage(new Uri(GameView.GetImage("Images/Vehicles/truck" + i + ".png")));
            }

            this.finalTarget = this.SearchTarget(map);
            this.pathToTarget = map.Pathfinder.FindPath(this, this.finalTarget);
            if (this.pathToTarget != null)
            {
                this.currentTarget = this.pathToTarget[0];
                System.Diagnostics.Debug.WriteLine("PathLength: " + this.pathToTarget.Count);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No path found");
            }
        }

        /// <summary>
        /// Gets the image of the vehicle
        /// </summary>
        public override BitmapImage Image
        {
            get
            {
                return this.images[(int)this.facing];
            }
        }

        /// <summary>
        /// Gets or sets the target to the vehicle where it has to move
        /// </summary>
        public MapItem Target
        {
            get
            {
                return this.currentTarget;
            }

            set
            {
                this.currentTarget = value;
            }
        }

        /// <summary>
        /// Updates the vehicle every frame
        /// </summary>
        public void Update()
        {
            this.Move();
        }

        /// <summary>
        /// Returns a string with the vehicle's data
        /// </summary>
        /// <returns>A string with the data of the vehicle</returns>
        public override string ToString()
        {
            return string.Format("x: {0} y: {1} orientation: {2} -> target: {3},{4} -> final target: {5},{6}", this.X, this.Y, this.facing, this.Target.X, this.Target.Y, this.finalTarget.X, this.finalTarget.Y);
        }

        /// <summary>
        /// Moves the vehiche to it's destination, changes direction and target if the target is reached
        /// </summary>
        private void Move()
        {
            if (this.Target != null)
            {
                if (this.ReachedWaypoint())
                {
                    if (this.targetIdx + 1 < this.pathToTarget.Count)
                    {
                        this.Target = this.pathToTarget[++this.targetIdx];
                    }

                    this.ChangeDirection();
                }

                this.MoveVehicle();
            }
        }

        /// <summary>
        /// Checks if the waypoint is reached or not
        /// </summary>
        /// <returns>Returns true if the waypoint is reached</returns>
        private bool ReachedWaypoint()
        {
            switch (this.facing)
            {
                case Direction.Up:
                    if (this.Y >= this.Target.Y)
                    {
                        this.Y = this.Target.Y;
                        return true;
                    }

                    break;
                case Direction.Down:
                    if (this.Y <= this.Target.Y)
                    {
                        this.Y = this.Target.Y;
                        return true;
                    }

                    break;
                case Direction.Left:
                    if (this.X <= this.Target.X)
                    {
                        this.X = this.Target.X;
                        return true;
                    }

                    break;
                case Direction.Right:
                    if (this.X >= this.Target.X)
                    {
                        this.X = this.Target.X;
                        return true;
                    }

                    break;
            }

            return false;
        }

        /// <summary>
        /// Moves the vehicles according to it's direction
        /// </summary>
        private void MoveVehicle()
        {
            switch (this.facing)
            {
                case Direction.Up:
                    this.Y += this.speed;
                    break;
                case Direction.Down:
                    this.Y -= this.speed;
                    break;
                case Direction.Left:
                    this.X -= this.speed;
                    break;
                case Direction.Right:
                    this.X += this.speed;
                    break;
            }
        }

        /// <summary>
        /// Searches a warehouse on the map
        /// </summary>
        /// <param name="map">Map of the game</param>
        /// <returns>A found warehouse</returns>
        private MapItem SearchTarget(Map map)
        {
            foreach (MapItem item in map.MapContainer)
            {
                if (item is WarehouseLot21)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Changes the vehicle's direction according to it's target
        /// </summary>
        private void ChangeDirection()
        {
            if (this.X < this.Target.X && this.Y == this.Target.Y)
            {
                this.facing = Direction.Right;
            }
            else if (this.X > this.Target.X && this.Y == this.Target.Y)
            {
                this.facing = Direction.Left;
            }
            else if (this.X == this.Target.X && this.Y < this.Target.Y)
            {
                this.facing = Direction.Up;
            }
            else if (this.X == this.Target.X && this.Y > this.Target.Y)
            {
                this.facing = Direction.Down;
            }
        }
    }
}
