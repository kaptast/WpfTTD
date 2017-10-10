using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HYYBLO_prog3
{
    /// <summary>
    /// Visual representation of the game
    /// </summary>
    class GameView : FrameworkElement
    {
        Game game; //the current game
        Camera cam; //camera of the field of view
        int mapLength, mapHeight;
        DispatcherTimer timer; //Timer which refreshes the screen
        int cellSize = 32; //current cell size
        int WindowWidth = 800; //current width of the window
        public static BitmapImage[] RoadImages;

        /// <summary>
        /// Constructor of GameView
        /// </summary>
        public GameView()
        {
            RoadImages = new BitmapImage[16];
            //Loading the images of the roads
            for(int i = 0; i < 16; i++)
            {
                RoadImages[i] = new BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Roads/cityroad" + i + ".png"));
            }
            game = new Game();
            cam = new Camera(0, 0);
            mapLength = game.Map.map.GetLength(0);
            mapHeight = game.Map.map.GetLength(1);
            this.InvalidateVisual();
            this.Loaded += ViewLoaded;
            this.SizeChanged += OnWindowSizeChanged;
            //this.KeyDown += GameView_KeyDown;
        }

        /// <summary>
        /// Handles key presses in the Game
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Key press parameters</param>
        public void GameView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Up:
                    cam.Move(Direction.Up, cellSize);
                    break;
                case System.Windows.Input.Key.Down:
                    cam.Move(Direction.Down, cellSize);
                    break;
                case System.Windows.Input.Key.Left:
                    cam.Move(Direction.Left, cellSize);
                    break;
                case System.Windows.Input.Key.Right:
                    cam.Move(Direction.Right, cellSize);
                    break;
            }
        }

        /// <summary>
        /// Event for starting the refresh timer
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Loaded event parameters</param>
        void ViewLoaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            timer.Tick += TimerTick;
            timer.Start();
        }

        /// <summary>
        /// Refresh event, redraws the screen
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Tick event parameters</param>
        private void TimerTick(object sender, EventArgs e)
        {
            //DoTurn
            this.InvalidateVisual();
        }

        /// <summary>
        /// Sets the width of the window in a paramter
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Parameters of the changed window</param>
        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowWidth = (int)e.NewSize.Width;
            this.InvalidateVisual();
        }

        /// <summary>
        /// Left click event, places a MapItem according to the Build type
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Parameters of the left click</param>
        public void GameView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, BuildType type)
        {
            Point p = e.GetPosition(this); //click position relative to the screen
            Point map = ScreenToPoint((int)p.X, (int)p.Y); //Screen coordinate to map coordinate
            switch (type)
            {
                case BuildType.Road:
                    game.Map.SetRoad((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1); //Place a road on the coordinates
                    game.Map.FireRoadPlaced(); //Check road tiles
                    break;
                case BuildType.Warehouse:
                    game.Map.SetWarehouse((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    break;
                case BuildType.Delete:
                    game.Map.SetDelete((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
                    game.Map.FireRoadPlaced();
                    break;
                default:
                    break;
            }
            this.InvalidateVisual(); //Redraw the screen
        }

        /// <summary>
        /// Zoom in the screen
        /// </summary>
        public void ZoomIn()
        {
            if (cellSize < 128)
            {
                cellSize *= 2;
            }
        }

        /// <summary>
        /// Zoom out the screen
        /// </summary>
        public void ZoomOut()
        {
            if (cellSize > 4)
            {
                cellSize /= 2;
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
            Point p = new Point();
            double halfCell = (cellSize / 2);
            double centerX = x + cam.X;
            double isoY = y + cam.Y;
            double isoX = centerX + halfCell - (WindowWidth / 2);;
            p.X = (Math.Floor(isoX / halfCell) + (Math.Floor(isoY / (halfCell / 2)))) / 2;
            p.Y = ((Math.Floor(isoY / (halfCell / 2))) - (Math.Floor(isoX / halfCell))) / 2;
            return p;
        }

        /// <summary>
        /// Redraws the view
        /// </summary>
        /// <param name="drawingContext">Drawer of the view</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            //System.Diagnostics.Debug.WriteLine("OnRender");
            base.OnRender(drawingContext);
            RenderMap(drawingContext);
        }

        /// <summary>
        /// Draws the items of the map on the screen
        /// </summary>
        /// <param name="dc">Drawer of the view</param>
        private void RenderMap(DrawingContext dc)
        {
            for (int i = 0; i < mapLength; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    MapItem item = game.Map.map[i, j];
                    int isoX = (item.X - item.Y) * (cellSize / 2);
                    int isoY = (item.X + item.Y) * (cellSize / 4);
                    int centerX = (WindowWidth / 2) - isoX - (cellSize / 2);
                    int screenX = centerX - cam.X;
                    int screenY = isoY - cam.Y;
                    dc.DrawImage(item.Image, item.GenerateRect(screenX, screenY, cellSize));
                }
            }
        }
    }
}
