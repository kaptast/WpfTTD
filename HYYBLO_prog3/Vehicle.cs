using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HYYBLO_prog3
{
    class Vehicle : MapItem
    {
        Direction facing;
        BitmapImage[] images;
        MapItem finalTarget, currentTarget;
        int targetIdx = 0;
        double speed = 0.05;

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
                map.SetPath(pathToTarget);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No path found");
            }
            
        }

        private void Move()
        {
            /*if (currentTarget != null)
            {
                if (currentTarget.CompareTo(this) == 0)
                {
                    if (targetIdx + 1 < pathToTarget.Count)
                    {
                        currentTarget = pathToTarget[++targetIdx];
                    }
                }
                ChangeDirection();
                switch (facing)
                {
                    case Direction.Up:
                        Y += speed;
                        break;
                    case Direction.Down:
                        Y -= speed;
                        break;
                    case Direction.Left:
                        X += speed;
                        break;
                    case Direction.Right:
                        X -= speed;
                        break;
                }
            }*/
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
            if(this.X < currentTarget.X && this.Y == currentTarget.Y && facing != Direction.Right)
            {
                facing = Direction.Right;
            }
            else if (this.X > currentTarget.X && this.Y == currentTarget.Y && facing != Direction.Left)
            {
                facing = Direction.Left;
            }
            else if (this.X == currentTarget.X && this.Y < currentTarget.Y && facing != Direction.Up)
            {
                facing = Direction.Up;
            }
            else if (this.X == currentTarget.X && this.Y > currentTarget.Y && facing != Direction.Down)
            {
                facing = Direction.Down;
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
        }

        public override Rect GenerateRect(double x, double y, int cell)
        {
            return new Rect(x + (cell) , y, cell/4, cell/4);
        }
    }
}
