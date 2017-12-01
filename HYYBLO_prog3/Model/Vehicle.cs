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
    using System.Diagnostics;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Hyyblo_Logic;

    /// <summary>
    /// Represents a moving vehicle
    /// </summary>
    public class Vehicle : MapItem
    {
        /// <summary>
        /// A current path of the vehicle to the final target
        /// </summary>
        private List<MapItem> pathToTarget;

        /// <summary>
        /// A current direction of the vehicle
        /// </summary>
        private Direction facing;

        /// <summary>
        /// A collection for the vehicles images
        /// </summary>
        private BitmapImage[] images;

        /// <summary>
        /// The final target item of the path
        /// </summary>
        private MapItem finalTarget;

        /// <summary>
        /// The current target item of the vehicle
        /// </summary>
        private MapItem currentTarget;

        /// <summary>
        /// Index of the current target in the path
        /// </summary>
        private int targetIdx = 0;

        /// <summary>
        /// Moving speed of the vehicle
        /// </summary>
        private double speed = 0.05;

        /// <summary>
        /// Starting warehouse of the current route
        /// </summary>
        private Warehouse startWarehouse;

        /// <summary>
        /// Final warehouse of the current route
        /// </summary>
        private Warehouse finalWarehouse;

        /// <summary>
        /// Type of the vehicles current ware
        /// </summary>
        private WareType type;

        /// <summary>
        /// Reference to the game
        /// </summary>
        private Game game;

        /// <summary>
        /// Timer for timing the path's time
        /// </summary>
        private Stopwatch timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the vehicle</param>
        /// <param name="y">Y coordinate of the vehicle</param>
        /// <param name="start">Start warehouse of the vehicle</param>
        /// <param name="target">Target of the vehicle</param>
        /// <param name="g">Reference to the game</param>
        public Vehicle(int x, int y, Warehouse start, Warehouse target, Game g)
            : base(x, y)
        {
            this.facing = Direction.Left;
            this.images = new BitmapImage[4];
            for (int i = 0; i < 4; i++)
            {
                this.images[i] = new BitmapImage(new Uri(Hyyblo_View.GameView.GetImage("Images/Vehicles/truck" + i + ".png")));
            }

            this.finalWarehouse = target;
            this.startWarehouse = start;

            this.game = g;

            this.timer = new Stopwatch();

            this.finalTarget = this.SearchTarget(this.finalWarehouse);
            this.pathToTarget = this.game.Map.Pathfinder.FindPath(this, this.finalTarget);
            if (this.pathToTarget != null)
            {
                this.currentTarget = this.pathToTarget[0];
            }

            this.timer.Start();
        }

        /// <summary>
        /// Gets or sets the image of the vehicle
        /// </summary>
        public override BitmapImage Image
        {
            get
            {
                return this.images[(int)this.facing];
            }

            protected set
            {
                this.Image = value;
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
        /// Gets or sets the vehicle's ware type on the current route
        /// </summary>
        public WareType CargoType
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
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
        /// Moves the vehicle to it's destination, changes direction and target if the target is reached
        /// </summary>
        private void Move()
        {
            if (this.pathToTarget != null)
            {
                if (this.Target != null)
                {
                    if (this.ReachedWaypoint())
                    {
                        if (this.targetIdx + 1 < this.pathToTarget.Count)
                        {
                            this.Target = this.pathToTarget[++this.targetIdx];
                        }
                        else
                        {
                            this.ReachedTarget();
                        }

                        this.ChangeDirection();
                    }

                    this.MoveVehicle();
                }
            }
            else
            {
                this.pathToTarget = this.game.Map.Pathfinder.FindPath(this, this.finalTarget);
                if (this.pathToTarget != null)
                {
                    this.currentTarget = this.pathToTarget[0];
                }
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
        /// <param name="w">Target warehouse</param>
        /// <returns>A found warehouse</returns>
        private MapItem SearchTarget(Warehouse w)
        {
            foreach (MapItem item in this.game.Map.MapContainer)
            {
                if (item.X == w.X + 1 && item.Y == w.Y)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// If the vehicle reached it's final target, it is deleted from the Vehicles container in the map
        /// </summary>
        private void ReachedTarget()
        {
            if (this.pathToTarget.Count - 1 <= this.targetIdx)
            {
                this.timer.Stop();
                this.SetPrice(this.CargoType, this.pathToTarget.Count, this.timer.ElapsedMilliseconds);
                Warehouse temp = this.startWarehouse;
                this.startWarehouse = this.finalWarehouse;
                this.finalWarehouse = temp;
                this.finalTarget = this.SearchTarget(this.finalWarehouse);
                this.pathToTarget = this.game.Map.Pathfinder.FindPath(this, this.finalTarget);
                this.targetIdx = 0;
                this.CargoType = this.startWarehouse.CargoType;
                this.timer.Restart();
            }
        }

        /// <summary>
        /// Calculates the route's price
        /// </summary>
        /// <param name="type">Cargo type of the route</param>
        /// <param name="length">Length of the route</param>
        /// <param name="time">Time of the route</param>
        private void SetPrice(WareType type, int length, long time)
        {
            int cost = length * 5;
            if (this.finalWarehouse.AcceptWare(this.CargoType))
            {
                double profit = (Game.Prices[type] * Math.Pow(length, 2) / (time / 500)) - cost;
                this.game.Money += (int)profit;
            }
            else
            {
                this.game.Money -= cost;
            }
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
