using System;

namespace HYYBLO_prog3
{
    /// <summary>
    /// Abstract class of the Building
    /// </summary>
    abstract class Building : MapItem
    {
        /// <summary>
        /// Constructor of the Building
        /// </summary>
        /// <param name="x">X coordinate of the building</param>
        /// <param name="y">Y coordinate of the building</param>
        public Building(int x, int y) : base(x, y)
        {

        }
    }
}
