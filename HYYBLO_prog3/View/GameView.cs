//-----------------------------------------------------------------------
// <copyright file="GameView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//----------------------------------------------------------------------
namespace Hyyblo_View
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
    using Hyyblo_Logic;
    using Hyyblo_Model;

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
        /// Collection of road images
        /// </summary>
        private static BitmapImage[] countryRoadImages;

        /// <summary>
        /// Path of the runtime
        /// </summary>
        private static string exepath = System.Reflection.Assembly.GetEntryAssembly().Location;

        /// <summary>
        /// Image for signaling mouse position
        /// </summary>
        private BitmapImage signalImage;

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
        /// Whether to show mouse position or not
        /// </summary>
        private bool signal;

        /// <summary>
        /// Position of the mouse on the map
        /// </summary>
        private Point signalPoint;

        /// <summary>
        /// Parent GameWindow of the framework element
        /// </summary>
        private GameWindow parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        /// <param name="size">Size of the map</param>
        public GameView(int size)
        {
            RoadImages = new BitmapImage[17]; // Loading the images of the roads
            CountryRoadImages = new BitmapImage[17];
            for (int i = 0; i < 17; i++)
            {
                RoadImages[i] = new BitmapImage(new Uri(GetImage("Images/Roads/cityroad" + i + ".png")));

                if (i < 16)
                {
                    CountryRoadImages[i] = new BitmapImage(new Uri(GetImage("Images/Roads/countryroad" + i + ".png")));
                }
            }

            this.signal = false;
            this.signalImage = new BitmapImage(new Uri(GetImage("Images/signal.png")));

            this.Game = new Game(size);
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
        /// Gets or sets the array with the images of the roads
        /// </summary>
        public static BitmapImage[] CountryRoadImages
        {
            get
            {
                return countryRoadImages;
            }

            set
            {
                countryRoadImages = value;
            }
        }

        /// <summary>
        /// Gets or sets the game of the view
        /// </summary>
        public Game Game
        {
            get
            {
                return this.game;
            }

            set
            {
                this.game = value;
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
                case Key.W:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Up);
                    break;
                case Key.Down:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Down);
                    break;
                case Key.S:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Down);
                    break;
                case Key.Left:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Left);
                    break;
                case Key.A:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Left);
                    break;
                case Key.Right:
                    this.cam.ChangeState(true);
                    this.cam.SetDir(Direction.Right);
                    break;
                case Key.D:
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
        public void GameView_MouseLeftButtonDown(MouseEventArgs e, BuildType type)
        {
            Point p = e.GetPosition(this); // Click position relative to the screen
            Point map = this.ScreenToPoint((int)p.X, (int)p.Y); // Screen coordinate to map coordinate
            Warehouse wh;
            switch (type)
            {
                case BuildType.Road:
                    this.Game.SetRoad((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1); // Place a road on the coordinates
                    break;
                case BuildType.Warehouse:
                    this.Game.SetWarehouse((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    break;
                case BuildType.Delete:
                    this.Game.Map.SetDelete((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    wh = this.Game.FindWarehouseByPosition((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    if (wh != null)
                    {
                        this.Game.Warehouses.Remove(wh);
                    }

                    this.Game.Map.FireRoadPlaced(this, new EventArgs());
                    break;
                case BuildType.Nothing:
                    wh = this.Game.FindWarehouseByPosition((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    if (wh != null)
                    {
                        EditWarehouse window = new EditWarehouse(wh);
                        window.ShowDialog();
                    }

                    break;
                default:
                    break;
            }

            this.Game.Map.MapContainer.Sort();
            this.Game.Map.Buildings.Sort();
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
                this.moneyLabel.Content = this.Game.ToString();
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
            this.Game.Update();
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
            this.parent = (GameWindow)this.FindName("wGame");
            this.MouseMove += this.GameView_MouseMove;
            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            this.timer.Tick += this.TimerTick;
            this.timer.Start();
        }

        /// <summary>
        /// Callback function for moving the mouse on the framework element
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments for the mouse move event</param>
        private void GameView_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.parent.SelectedItem != BuildType.Nothing)
            {
                this.signal = true;
                Point p = e.GetPosition(this);
                this.signalPoint = this.ScreenToPoint((int)p.X, (int)p.Y);
                this.signalPoint.X--;
            }
            else
            {
                this.signal = false;
            }
        }

        /// <summary>
        /// Draws the items of the map on the screen
        /// </summary>
        /// <param name="dc">Drawer of the view</param>
        private void RenderMap(DrawingContext dc)
        {
            foreach (MapItem item in this.Game.Map.MapContainer)
            {
                Point p = this.PointToScreen(item);
                dc.DrawImage(item.Image, item.GenerateRect(p.X, p.Y, this.cellSize));
            }

            foreach (Vehicle item in this.Game.Map.Vehicles)
            {
                Point p = this.PointToScreen(item);
                dc.DrawImage(item.Image, item.GenerateRect(p.X, p.Y, this.cellSize));
            }

            foreach (Building item in this.Game.Map.Buildings)
            {
                Point p = this.PointToScreen(item);
                dc.DrawImage(item.Image, item.GenerateRect(p.X, p.Y, this.cellSize));
            }

            foreach (CargoPrice item in this.Game.Map.Prices)
            {
                Point p = this.PointToScreen(item);
                dc.DrawText(new FormattedText((item.Positive ? string.Empty : "-") + item.Price, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 16, item.Positive ? Brushes.DarkGreen : Brushes.DarkRed), p);
            }

            if (this.signal)
            {
                this.signalPoint.X = Math.Floor(this.signalPoint.X);
                this.signalPoint.Y = Math.Floor(this.signalPoint.Y);
                double isoX = (this.signalPoint.Y - this.signalPoint.X) * (this.cellSize / 2);
                double isoY = (this.signalPoint.Y + this.signalPoint.X) * (this.cellSize / 4);
                double centerX = (this.windowWidth / 2) - isoX - (this.cellSize / 2);
                double screenX = centerX - this.cam.X;
                double screenY = isoY - this.cam.Y;
                dc.DrawImage(this.signalImage, new Rect((int)Math.Floor(screenX), (int)Math.Floor(screenY), this.cellSize, this.cellSize));
            }
        }

        /// <summary>
        /// Generates a point with isometric coordinates
        /// </summary>
        /// <param name="item">Item to generate it's position</param>
        /// <returns>Point with the isometric coordinates</returns>
        private Point PointToScreen(IItem item)
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
