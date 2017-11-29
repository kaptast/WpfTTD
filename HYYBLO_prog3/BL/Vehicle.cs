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
        /// Reference to the map of the game
        /// </summary>
        private Map map;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the vehicle</param>
        /// <param name="y">Y coordinate of the vehicle</param>
        /// <param name="map">Map of the game</param>
        /// <param name="target">Target of the vehicle</param>
        public Vehicle(int x, int y, Map map, Warehouse target)
            : base(x, y)
        {
            this.facing = Direction.Left;
            this.images = new BitmapImage[4];
            for (int i = 0; i < 4; i++)
            {
                this.images[i] = new BitmapImage(new Uri(GameView.GetImage("Images/Vehicles/truck" + i + ".png")));
            }

            this.map = map;
            this.finalTarget = this.SearchTarget(target);
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
            foreach (MapItem item in this.map.MapContainer)
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
                this.map.Vehicles.Remove(this);
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
