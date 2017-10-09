using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYYBLO_prog3
{
    /// <summary>
    /// Game object, contains the current game's values
    /// </summary>
    class Game
    {
        Map map; //Map of the game

        /// <summary>
        /// Constructor of the Game
        /// </summary>
        public Game()
        {
            map = new Map(40, 40);
        }

        /// <summary>
        /// Map of the game
        /// </summary>
        public Map Map
        {
            get
            {
                return map;
            }

            set
            {
                map = value;
            }
        }
    }
}
