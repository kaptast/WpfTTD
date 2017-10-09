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

        public void Move(Direction dir, int step)
        {
            switch (dir)
            {
                case Direction.Up:
                    y -= step / 3;
                    break;
                case Direction.Down:
                    y += step / 3;
                    break;
                case Direction.Right:
                    x += step / 3;
                    break;
                case Direction.Left:
                    x -= step / 3;
                    break;
            }
        }


    }
}
