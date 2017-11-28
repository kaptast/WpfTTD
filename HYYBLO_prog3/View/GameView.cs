//-----------------------------------------------------------------------
// <copyright file="GameView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    /// <summary>
    /// Visual representation of the game
    /// </summary>
    public class GameView : FrameworkElement
    {
        /// <summary>
        /// Collection of road images
        /// </summary>
        private static BitmapImage[] roadImages;

        /// <summary>
        /// Path of the runtime
        /// </summary>
        private static string exepath = System.Reflection.Assembly.GetEntryAssembly().Location;

        /// <summary>
        /// The current game
        /// </summary>
        private Game game;

        /// <summary>
        /// camera of the field of view
        /// </summary>
        private Camera cam;

        /// <summary>
        /// Timer which refreshes the screen
        /// </summary>
        private DispatcherTimer timer;

        /// <summary>
        /// Current cell size
        /// </summary>
        private int cellSize = 32;

        /// <summary>
        /// Current width of the window
        /// </summary>
        private int windowWidth = 800;

        /// <summary>
        /// Reference to the label on the window
        /// </summary>
        private Label moneyLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        public GameView()
        {
            RoadImages = new BitmapImage[17]; // Loading the images of the roads
            for (int i = 0; i < 17; i++)
            {
                RoadImages[i] = new BitmapImage(new Uri(GetImage("Images/Roads/cityroad" + i + ".png")));
            }

            this.game = new Game();
            this.cam = new Camera(0, 0);
            this.InvalidateVisual();
            this.Loaded += this.ViewLoaded;
            this.SizeChanged += this.OnWindowSizeChanged;
        }

        /// <summary>
        /// Gets or sets the array with the images of the roads
        /// </summary>
        public static BitmapImage[] RoadImages
        {
            get
            {
                return roadImages;
            }

            set
            {
                roadImages = value;
            }
        }

        /// <summary>
        /// Return the path of file in the current environment
        /// </summary>
        /// <param name="filename">Name of the file</param>
        /// <returns>Full path of the file</returns>
        public static string GetImage(string filename)
        {
            string tmp = Path.Combine(exepath, @"..\..\..\");
            return Path.GetFullPath(Path.Combine(tmp, filename));
        }

        /// <summary>
        /// Handles key presses in the Game
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Key press parameters</param>
        public void GameView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Up);
                    break;
                case Key.Down:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Down);
                    break;
                case Key.Left:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Left);
                    break;
                case Key.Right:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Right);
                    break;
            }
        }

        /// <summary>
        /// Sets the camera's state
        /// </summary>
        /// <param name="dir">Direction of the camera movement</param>
        public void GameView_SetCamState(Direction dir)
        {
            this.cam.ChangeState(true);
            this.cam.SetDir(dir);
        }

        /// <summary>
        /// Stops the camera movement if a button is lifted up
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Parameters of the keyboard event</param>
        public void GameView_KeyUp(object sender, KeyEventArgs e)
        {
            this.cam.ChangeState(false);
        }

        /// <summary>
        /// Left click event, places a MapItem according to the Build type
        /// </summary>
        /// <param name="e">Parameters of the left click</param>
        /// <param name="type">Type of the building</param>
        public void GameView_MouseLeftButtonDown(MouseEventArgs e, Hyyblo_View.BuildType type)
        {
            Point p = e.GetPosition(this); // Click position relative to the screen
            Point map = this.ScreenToPoint((int)p.X, (int)p.Y); // Screen coordinate to map coordinate
            switch (type)
            {
                case Hyyblo_View.BuildType.Road:
                    this.game.SetRoad((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1); // Place a road on the coordinates
                    break;
                case Hyyblo_View.BuildType.Warehouse:
                    this.game.SetWarehouse((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    break;
                case Hyyblo_View.BuildType.Delete:
                    this.game.Map.SetDelete((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    this.game.Map.FireRoadPlaced(this, new EventArgs());
                    break;
                case Hyyblo_View.BuildType.Vehicle:
                    this.game.Map.AddVehicle((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    break;
                default:
                    break;
            }

            this.game.Map.MapContainer.Sort();
            this.InvalidateVisual(); // Redraw the screen
        }

        /// <summary>
        /// Zoom in the screen
        /// </summary>
        public void ZoomIn()
        {
            if (this.cellSize < 128)
            {
                this.cellSize *= 2;
                this.cam.Reset();
            }
        }

        /// <summary>
        /// Zoom out the screen
        /// </summary>
        public void ZoomOut()
        {
            if (this.cellSize > 4)
            {
                this.cellSize /= 2;
                this.cam.Reset();
            }
        }

        /// <summary>
        /// Calculate map position from screen position
        /// </summary>
        /// <param name="x">X coordinate on the screen</param>
        /// <param name="y">Y coordinate on the screen</param>
        /// <returns>Point with the map coordinates</returns>
        public Point ScreenToPoint(double x, double y)
        {
            double pointX;
            double pointY;
            double halfCell = this.cellSize / 2;
            double centerX = x + this.cam.X;
            double isoY = y + this.cam.Y;
            double isoX = centerX + halfCell - (this.windowWidth / 2);
            pointX = (Math.Floor(isoX / halfCell) + Math.Floor(isoY / (halfCell / 2))) / 2;
            pointY = (Math.Floor(isoY / (halfCell / 2)) - Math.Floor(isoX / halfCell)) / 2;
            return new Point(pointX, pointY);
        }

        /// <summary>
        /// Redraws the view
        /// </summary>
        /// <param name="drawingContext">Drawer of the view</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (this.moneyLabel != null)
            {
                this.moneyLabel.Content = this.game;
            }

            this.RenderMap(drawingContext);
        }

        /// <summary>
        /// Sets the width of the window in a parameter
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Parameters of the changed window</param>
        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.windowWidth = (int)e.NewSize.Width;
            this.InvalidateVisual();
        }

        /// <summary>
        /// Refresh event, redraws the screen
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Tick event parameters</param>
        private void TimerTick(object sender, EventArgs e)
        {
            // DoTurn
            this.cam.Turn(this.cellSize);
            this.game.Update();
            this.InvalidateVisual();
        }

        /// <summary>
        /// Event for starting the refresh timer
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Loaded event parameters</param>
        private void ViewLoaded(object sender, RoutedEventArgs e)
        {
            this.moneyLabel = (Label)this.FindName("lblMoney");
            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            this.timer.Tick += this.TimerTick;
            this.timer.Start();
        }

        /// <summary>
        /// Draws the items of the map on the screen
        /// </summary>
        /// <param name="dc">Drawer of the view</param>
        private void RenderMap(DrawingContext dc)
        {
            foreach (MapItem item in this.game.Map.MapContainer)
            {
                Point p = this.PointToScreen(item);
                dc.DrawImage(item.Image, item.GenerateRect(p.X, p.Y, this.cellSize));
            }

            foreach (Vehicle item in this.game.Map.Vehicles)
            {
                Point p = this.PointToScreen(item);
                dc.DrawImage(item.Image, item.GenerateRect(p.X, p.Y, this.cellSize));
            }

            foreach (Building item in this.game.Map.Buildings)
            {
                Point p = this.PointToScreen(item);
                dc.DrawImage(item.Image, item.GenerateRect(p.X, p.Y, this.cellSize));
            }
        }

        /// <summary>
        /// Generates a point with isometric coords
        /// </summary>
        /// <param name="item">Item to generate it's position</param>
        /// <returns>Point with the isometric coords</returns>
        private Point PointToScreen(MapItem item)
        {
            double isoX = (item.X - item.Y) * (this.cellSize / 2);
            double isoY = (item.X + item.Y) * (this.cellSize / 4);
            double centerX = (this.windowWidth / 2) - isoX - (this.cellSize / 2);
            double screenX = centerX - this.cam.X;
            double screenY = isoY - this.cam.Y;

            return new Point(screenX, screenY);
        }
    }
}
