using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYYBLO_prog3
{
    class Camera
    {
        int x, y;
        int step = 10;

        public Camera(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    y -= step;
                    break;
                case Direction.Down:
                    y += step;
                    break;
                case Direction.Right:
                    x += step;
                    break;
                case Direction.Left:
                    x -= step;
                    break;
            }
        }


    }
}
