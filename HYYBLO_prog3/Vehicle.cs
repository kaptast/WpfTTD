using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HYYBLO_prog3
{
    public class Vehicle : MapItem
    {
        Direction facing;
        BitmapImage[] images;
        MapItem finalTarget, currentTarget;
        int targetIdx = 0;
        double speed = 0.05;
        //private double bound = 1f;

        protected List<MapItem> pathToTarget;

        public Vehicle(int _x, int _y, Map map) : base(_x, _y)
        {
            facing = Direction.Left;
            images = new BitmapImage[4];
            for(int i = 0; i < 4; i++)
            {
                images[i] = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Vehicles/truck" + i + ".png"));
            }
            finalTarget = SearchTarget(map);
            pathToTarget = map.pathfinder.FindPath(this, finalTarget);
            if(pathToTarget != null)
            {
                currentTarget = pathToTarget[0];
                System.Diagnostics.Debug.WriteLine("PathLength: " + pathToTarget.Count);
                //map.SetPath(pathToTarget);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No path found");
            }
            
        }

        private void Move()
        {
            if (currentTarget != null)
            {
                if (ReachedWaypoint())
                {
                    //System.Diagnostics.Debug.WriteLine("Waypoint found");
                    if (targetIdx + 1 < pathToTarget.Count)
                    {
                        currentTarget = pathToTarget[++targetIdx];
                    }
                    ChangeDirection();
                }
                MoveVehicle();
            }
        }

        private bool ReachedWaypoint()
        {
            switch (facing)
            {
                case Direction.Up:
                    if (this.Y >= currentTarget.Y)
                    {
                        this.Y = currentTarget.Y;
                        return true;
                    }
                    break;
                case Direction.Down:
                    if (this.Y <= currentTarget.Y)
                    {
                        this.Y = currentTarget.Y;
                        return true;
                    }
                    break;
                case Direction.Left:
                    if (this.X <= currentTarget.X)
                    {
                        this.X = currentTarget.X;
                        return true;
                    }
                    break;
                case Direction.Right:
                    if (this.X >= currentTarget.X)
                    {
                        this.X = currentTarget.X;
                        return true;
                    }
                    break;
            }
            return false;
        }

        private void MoveVehicle()
        {
            switch (facing)
            {
                case Direction.Up:
                    Y += speed;
                    break;
                case Direction.Down:
                    Y -= speed;
                    break;
                case Direction.Left:
                    X -= speed;
                    break;
                case Direction.Right:
                    X += speed;
                    break;
            }
        }

        private MapItem SearchTarget(Map map)
        {
            foreach(MapItem item in map.map)
            {
                if(item is WarehouseLot21)
                {
                    return item;
                }
            }
            return null;
        }

        private void ChangeDirection()
        {
            if(this.X < currentTarget.X && this.Y == currentTarget.Y)
            {
                facing = Direction.Right;
                //System.Diagnostics.Debug.WriteLine("Direction Changed");
            }
            else if (this.X > currentTarget.X && this.Y == currentTarget.Y)
            {
                facing = Direction.Left;
                //System.Diagnostics.Debug.WriteLine("Direction Changed");
            }
            else if (this.X == currentTarget.X && this.Y < currentTarget.Y)
            {
                facing = Direction.Up;
                //System.Diagnostics.Debug.WriteLine("Direction Changed");
            }
            else if (this.X == currentTarget.X && this.Y > currentTarget.Y)
            {
                facing = Direction.Down;
                //System.Diagnostics.Debug.WriteLine("Direction Changed");
            }
        }

        public void SetTarget(MapItem item)
        {
            currentTarget = item;
        }

        public override BitmapImage Image
        {
            get
            {
                return images[(int)facing];
            }
        }

        public void Update()
        {
            Move();
            //System.Diagnostics.Debug.WriteLine(this);
        }

        /*public override Rect GenerateRect(double x, double y, int cell)
        {
            return new Rect(x + (cell) , y, cell/4, cell/4);
        }*/

        /*public override int CompareTo(MapItem obj)
        {
            if (obj.Y > this.Y + bound)
            {
                return -1;
            }
            else if (obj.Y < this.Y - bound)
            {
                return 1;
            }
            else if (obj.X > this.X + bound)
            {
                return -1;
            }
            else if (obj.X < this.X - bound)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }*/

        public override string ToString()
        {
            return String.Format("x: {0} y: {1} orientation: {2} -> target: {3},{4} -> final target: {5},{6}", X, Y, facing, currentTarget.X, currentTarget.Y, finalTarget.X, finalTarget.Y);
        }
    }
}
