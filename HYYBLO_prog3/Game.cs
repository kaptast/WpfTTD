using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYYBLO_prog3
{
    class Game
    {
        Map map;

        public Game()
        {
            map = new Map(40, 40);
        }

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
