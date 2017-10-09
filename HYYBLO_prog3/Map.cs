using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HYYBLO_prog3
{
    delegate void RoadPlaced();
    class Map
    {
        public event RoadPlaced roadPlaced;
        MapItem[,] mapContainer;
        static Random r;

        /// <summary>
        /// Generates a random number along a curve
        /// https://stackoverflow.com/questions/18807812/adding-an-average-parameter-to-nets-random-next-to-curve-results
        /// </summary>
        /// <param name="min">Minimum random value</param>
        /// <param name="max">Maximum random value</param>
        /// <param name="tightness"></param>
        /// <param name="exp"></param>
        /// <returns>Random number in range which gravitates towards the given value</returns>
        public static double RandomNormalDist(double min, double max, int tightness, double exp)
        {
            double total = 0.0;
            for (int i = 1; i <= tightness; i++)
            {
                total += Math.Pow(r.NextDouble(), exp);
            }

            return ((total / tightness) * (max - min)) + min;
        }

        public Map(int sizeX, int sizeY)
        {
            mapContainer = new MapItem[sizeX, sizeY];
            r = new Random();
            GenerateMap(sizeX, sizeY);
        }

        private void GenerateMap(int x, int y)
        {
            GenerateGrass(x, y);

            GenerateTowns();

            GenerateBuildings(x,y);

            FireRoadPlaced();
        }

        private void GenerateTowns()
        {
            int townNum = r.Next(5, 10);
            for(int i = 0; i < townNum; i++)
            {
                GenerateTown();
            }
        }

        private void GenerateTown()
        {
            int roadNum = r.Next(4,8);
            Point townCenter = new Point();
            townCenter.X = r.Next(mapContainer.GetLength(0));
            townCenter.Y = r.Next(mapContainer.GetLength(1));
            SetRoad((int)townCenter.X, (int)townCenter.Y);
            for(int i = 0; i < roadNum; i++)
            {
                int centerDist = r.Next(-4, 4);
                int orientation = (i % 2) == 0?1:2;
                int roadLength = r.Next(3, 5);
                if(orientation == 1)
                {
                    GenerateRoad((int)townCenter.X, (int)townCenter.Y + centerDist, (int)townCenter.X, (int)townCenter.Y + roadLength);
                }
                else
                {
                    GenerateRoad((int)townCenter.X + centerDist, (int)townCenter.Y, (int)townCenter.X + roadLength, (int)townCenter.Y);
                }
            }
        }

        private void GenerateGrass(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    mapContainer[i, j] = new Grass(i, j);
                }
            }
        }

        private void GenerateBuildings(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (RoadIsNeighbour(i, j))
                    {
                        GenHouse(i, j);
                    }
                }
            }
        }

        private bool RoadIsNeighbour(int x, int y)
        {
            if(x > 0 && x < mapContainer.GetLength(0) - 1 && y > 0 && y < mapContainer.GetLength(1) - 1)
            {
                if(!(mapContainer[x,y] is Road) && (mapContainer[x - 1,y] is Road || mapContainer[x, y - 1] is Road || mapContainer[x + 1, y] is Road || mapContainer[x, y + 1] is Road))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private void GenerateRoad(int x1, int y1, int x2, int y2)
        {
            if (x1 < x2 && y1 == y2)
            {
                //x1-től x2-ig
                for(int i = x1; i < x2; i++)
                {
                    SetRoad(i, y1);
                    //mapContainer[i, y1] = new Road(i, y1, 1);
                }
            }
            else if(x1 > x2 && y1 == y2)
            {
                //x2-től x1-ig
                for (int i = x1; i > x2; i--)
                {
                    SetRoad(i, y1);
                    //mapContainer[i, y1] = new Road(i, y1, 1);
                }
            }
            else if(y1 < y2 && x1 == x2)
            {
                //y1-től y2-ig
                for (int i = y1; i < y2; i++)
                {
                    SetRoad(x1, i);
                    //mapContainer[x1, i] = new Road(x1, i, 2);
                }
            }
            else if(y1 > y2 && x1 == x2)
            {
                //y2-től y1-ig
                for (int i = y1; i > y2; i--)
                {
                    SetRoad(x1, i);
                    //mapContainer[x2, i] = new Road(x2, i, 2);
                }
            }
        }

        public void SetRoad(int x, int y)
        {
            if (RightCoord(x,y))
            {
                map[x, y] = new Road(x, y, this);
            }
        }

        public bool RightCoord(int x, int y)
        {
            return (x >= 0 && x < mapContainer.GetLength(0) && y >= 0 && y < mapContainer.GetLength(1));
        }

        private void GenHouse(int x, int y)
        {
            if (RightCoord(x, y))
            {
                int percent = r.Next(101);
                if (percent < 40)
                {
                    mapContainer[x, y] = new House(x, y, r);
                }
            }
        }

        public void FireRoadPlaced()
        {
            if(roadPlaced != null)
            {
                roadPlaced.Invoke();
            }
        }

        public MapItem[,] map
        {
            get
            {
                return mapContainer;
            }
        }
    }
}
