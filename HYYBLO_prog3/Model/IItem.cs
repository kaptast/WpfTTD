//-----------------------------------------------------------------------
// <copyright file="IItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    /// <summary>
    /// Interface for items with X and Y coordinates
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Gets or sets the X coordinate of the item
        /// </summary>
        double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the item
        /// </summary>
        double Y { get; set; }
    }
}
