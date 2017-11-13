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
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            this.map = new Map(40);
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
    }
}
