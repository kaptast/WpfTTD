//-----------------------------------------------------------------------
// <copyright file="Prices.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//----------------------------------------------------------------------
namespace Hyyblo_Logic
{
    /// <summary>
    /// Prices of the various cargo
    /// </summary>
    public struct Prices
    {
        /// <summary>
        /// Price of the cargo
        /// </summary>
        private int price;

        /// <summary>
        /// Days of fast delivery
        /// </summary>
        private long day1;

        /// <summary>
        /// Days of slow delivery
        /// </summary>
        private long day2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hyyblo_Logic.Prices"/> struct.
        /// </summary>
        /// <param name="p">Price of the item</param>
        /// <param name="d1">Time of fast transport</param>
        /// <param name="d2">Time of slow transport</param>
        public Prices(int p, long d1, long d2)
        {
            this.price = p;
            this.day1 = d1;
            this.day2 = d2;
        }

        /// <summary>
        /// Gets the price of the cargo type
        /// </summary>
        public int Price
        {
            get
            {
                return this.price;
            }
        }

        /// <summary>
        /// Gets the time of fast transport
        /// </summary>
        public long Day1
        {
            get
            {
                return this.day1;
            }
        }

        /// <summary>
        /// Gets the time of slow transport
        /// </summary>
        public long Day2
        {
            get
            {
                return this.day2;
            }
        }
    }
}
