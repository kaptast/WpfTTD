using System;

namespace HYYBLO_prog3
{
    class Road : MapItem
    {
        Map map;
        int imageNum = 0;
        public Road(int x, int y, Map _map) : base(x, y)
        {
            map = _map;
            Image = GameView.RoadImages[0];
            map.roadPlaced += this.RoadChanged;
        }

        protected virtual void RoadChanged()
        {
            int neighbours = CheckNeighbours();
            if(imageNum != neighbours)
            {
                imageNum = neighbours;
                Image = GameView.RoadImages[neighbours];
            }
        }

        int CheckNeighbours()
        {
            int d = 0;
            int c = 0;
            int b = 0;
            int a = 0;

            if(X >= 0  && X < map.size && Y >= 1 && Y < map.size)
            {
                d = (map.GetItemByCoord((int)X,(int)Y - 1) is Road) ? 1 : 0;
            }

            if (X >= 0 && X < map.size - 1 && Y >= 0 && Y < map.size)
            {
                c = (map.GetItemByCoord((int)X + 1, (int)Y) is Road) ? 1 : 0;
            }

            if (X >= 0 && X < map.size && Y >= 0 && Y < map.size - 1)
            {
                b = (map.GetItemByCoord((int)X, (int)Y + 1) is Road) ? 1 : 0;
            }

            if (X >= 1 && X < map.size && Y >= 0 && Y < map.size)
            {
                a = (map.GetItemByCoord((int)X - 1, (int)Y) is Road) ? 1 : 0;
            }
            return 8 * d + 4 * c + 2 * b + 1 * a;
        }
    }
}
