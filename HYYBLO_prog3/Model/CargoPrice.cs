//-----------------------------------------------------------------------
// <copyright file="CargoPrice.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Represents a floating text with the profit
    /// </summary>
    public class CargoPrice : IItem
    {
        /// <summary>
        /// X coordinate of the item
        /// </summary>
        private double x;

        /// <summary>
        /// Y coordinate of the item
        /// </summary>
        private double y;

        /// <summary>
        /// Price of the item
        /// </summary>
        private int price;

        /// <summary>
        /// Cycles since the item was created
        /// </summary>
        private int life;

        /// <summary>
        /// Speed of the text floating upwards
        /// </summary>
        private double speed = 0.01;

        /// <summary>
        /// Initializes a new instance of the <see cref="CargoPrice"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the item</param>
        /// <param name="y">Y coordinate of the item</param>
        /// <param name="p">Price of the item</param>
        public CargoPrice(double x, double y, int p)
        {
            this.X = x;
            this.Y = y;
            this.price = p;

            this.life = 0;
        }

        /// <summary>
        /// Gets a value indicating whether the price is positive or not
        /// </summary>
        public bool Positive
        {
            get
            {
                return this.price > 0;
            }
        }

        /// <summary>
        /// Gets a string with the item's value
        /// </summary>
        public string Price
        {
            get
            {
                return "$" + Math.Abs(this.price).ToString();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the item
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the item
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Updates the price text of the map
        /// </summary>
        /// <returns>Returns true if the item should be deleted</returns>
        public bool Update()
        {
            if (++this.life > 60)
            {
                return true;
            }

            this.Y -= this.speed;
            this.x -= this.speed;
            return false;
        }
    }
}
