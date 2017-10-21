using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HYYBLO_prog3
{
    public delegate void RoadPlaced();
    public class Map
    {
        public event RoadPlaced roadPlaced;
        //MapItem[,] mapContainer;
        List<MapItem> mapContainer;
        List<Vehicle> vehicles;
        public Pathfinding pathfinder;
        static Random r;
        public int size;

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
            //mapContainer = new MapItem[sizeX, sizeY];
            mapContainer = new List<MapItem>();
            Vehicles = new List<Vehicle>();
            r = new Random();
            size = sizeX;
            pathfinder = new Pathfinding(this);
            GenerateMap(sizeX, sizeY);

            mapContainer.Sort();
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
            townCenter.X = r.Next(size);
            townCenter.Y = r.Next(size);
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
                    mapContainer.Add(new Grass(i, j));
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
            if(x > 0 && x < size - 1 && y > 0 && y < size - 1)
            {
                if(!(GetItemByCoord(x,y) is Road) && (GetItemByCoord(x - 1, y) is Road || GetItemByCoord(x, y - 1) is Road || GetItemByCoord(x + 1, y) is Road || GetItemByCoord(x, y + 1) is Road))
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
                MapItem item = GetItemByCoord(x, y);
                mapContainer.Remove(item);
                mapContainer.Add(new Road(x, y, this));
            }
        }

        public void SetWarehouse(int x, int y)
        {
            if (RightCoord(x, y))
            {
                Warehouse wh = new Warehouse(x, y, this);
            }
        }

        public void SetDelete(int x, int y)
        {
            if (RightCoord(x, y))
            {
                MapItem item = GetItemByCoord(x, y);
                mapContainer.Remove(item);
                mapContainer.Add(new Grass(x, y));
            }
        }


        public bool RightCoord(int x, int y)
        {
            return (x >= 0 && x < size && y >= 0 && y < size);
        }

        private void GenHouse(int x, int y)
        {
            if (RightCoord(x, y))
            {
                int percent = r.Next(101);
                if (percent < 40)
                {
                    MapItem item = GetItemByCoord(x, y);
                    mapContainer.Remove(item);
                    mapContainer.Add(new House(x, y, r));
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

        public MapItem GetItemByCoord(double x, double y)
        {
            foreach(MapItem item in mapContainer)
            {
                if(item.X == x && item.Y == y)
                {
                    return item;
                }
            }
            return null;
        }

        public List<MapItem> map
        {
            get
            {
                return mapContainer;
            }
        }

        public void AddVehicle(int x, int y)
        {
            if (RightCoord(x, y))
            {
                Vehicle v = new Vehicle(x, y, this);
                mapContainer.Add(v);
                vehicles.Add(v);
            }
        }

        public void SetPath(List<MapItem> path)
        {
            foreach(MapItem i in path)
            {
                MapItem item = GetItemByCoord(i.X, i.Y);
                mapContainer.Remove(item);
                mapContainer.Add(new Sign((int)i.X, (int)i.Y));
            }
        }

        public void UpdateVehicles()
        {
            foreach(Vehicle v in Vehicles)
            {
                v.Update();
            }
        }

        internal List<Vehicle> Vehicles
        {
            get
            {
                return vehicles;
            }

            set
            {
                vehicles = value;
            }
        }

        public List<MapItem> GetNeighbours(MapItem item)
        {
            List<MapItem> list = new List<MapItem>();
            list.Add(GetItemByCoord(item.X, item.Y + 1));
            list.Add(GetItemByCoord(item.X, item.Y - 1));
            list.Add(GetItemByCoord(item.X + 1, item.Y));
            list.Add(GetItemByCoord(item.X - 1, item.Y ));
            return list;
        }
    }
}
