using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYYBLO_prog3
{
    /// <summary>
    /// A camera which represents the field of view
    /// </summary>
    class Camera
    {
        int x, y; //x and y coordinates of the camera
        Direction dir;
        bool pressed = false;

        /// <summary>
        /// Constructor for the Camera
        /// </summary>
        /// <param name="_x">X coordinate of the camera</param>
        /// <param name="_y">Y coordinate of the camera</param>
        public Camera(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }

        /// <summary>
        /// X coordinate of the camera
        /// </summary>
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

        /// <summary>
        /// Y coordinate of the camera
        /// </summary>
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

        public void SetDir(Direction dir)
        {
            this.dir = dir;
        }

        public void ChangeState(bool state)
        {
            pressed = state;
        }

        public void Turn(int step)
        {
            Move(dir, step);
        }

        /// <summary>
        /// Moves the camera to the specified direction
        /// </summary>
        /// <param name="dir">Direction of the requested movement</param>
        /// <param name="step">Step size of the requested movement</param>
        public void Move(Direction direction, int step)
        {
            if (pressed)
            {
                switch (direction)
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
}
