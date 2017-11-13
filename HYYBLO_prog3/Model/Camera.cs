//-----------------------------------------------------------------------
// <copyright file="Camera.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    /// <summary>
    /// A camera which represents the field of view
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// x coordinate of the camera
        /// </summary>
        private int x;

        /// <summary>
        /// y coordinate of the camera
        /// </summary>
        private int y;

        /// <summary>
        /// Moving direction of the camera
        /// </summary>
        private Direction dir;

        /// <summary>
        /// Moving state of the camera
        /// </summary>
        private bool pressed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the camera</param>
        /// <param name="y">Y coordinate of the camera</param>
        public Camera(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the X coordinate of the camera
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets Y coordinate of the camera
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Changes the direction of the camera's movement
        /// </summary>
        /// <param name="dir">Requested direction of the camera</param>
        public void SetDir(Direction dir)
        {
            this.dir = dir;
        }

        /// <summary>
        /// Resets the camera to the (0,0) coordinate
        /// </summary>
        public void Reset()
        {
            this.X = 0;
            this.Y = 0;
        }

        /// <summary>
        /// Changes whether the camera should move or not
        /// </summary>
        /// <param name="state">The state whether the camera moves or not</param>
        public void ChangeState(bool state)
        {
            this.pressed = state;
        }

        /// <summary>
        /// Moves the camera every frame
        /// </summary>
        /// <param name="step">Step size of the requested movement</param>
        public void Turn(int step)
        {
            this.Move(this.dir, step);
        }

        /// <summary>
        /// Moves the camera to the specified direction
        /// </summary>
        /// <param name="direction">Direction of the requested movement</param>
        /// <param name="step">Step size of the requested movement</param>
        public void Move(Direction direction, int step)
        {
            if (this.pressed)
            {
                switch (direction)
                {
                    case Direction.Up:
                        this.Y -= step / 3;
                        break;
                    case Direction.Down:
                        this.Y += step / 3;
                        break;
                    case Direction.Right:
                        this.X += step / 3;
                        break;
                    case Direction.Left:
                        this.X -= step / 3;
                        break;
                }
            }
        }
    }
}
