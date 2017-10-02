using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HYYBLO_prog3
{
    class GameView : FrameworkElement
    {
        Game game;
        Camera cam;
        int mapLength, mapHeight;
        DispatcherTimer timer, roadTimer;
        const int cellSize = 32;
        int WindowWidth = 800;

        public static BitmapImage[] RoadImages;

        public GameView()
        {
            RoadImages = new BitmapImage[16];
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

        public void GameView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("KeyPressed");
            switch (e.Key)
            {
                case System.Windows.Input.Key.Up:
                    cam.Move(Direction.Up);
                    break;
                case System.Windows.Input.Key.Down:
                    cam.Move(Direction.Down);
                    break;
                case System.Windows.Input.Key.Left:
                    cam.Move(Direction.Left);
                    break;
                case System.Windows.Input.Key.Right:
                    cam.Move(Direction.Right);
                    break;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //System.Diagnostics.Debug.WriteLine("OnRender");
            base.OnRender(drawingContext);
            RenderMap(drawingContext);
        }

        void ViewLoaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            timer.Tick += TimerTick;
            timer.Start();
            roadTimer = new DispatcherTimer();
            roadTimer.Interval = new TimeSpan(0, 0, 0, 1);
            roadTimer.Tick += RoadTimerTick;
            roadTimer.Start();
        }

        private void RoadTimerTick(object sender, EventArgs e)
        {
            game.Map.FireRoadPlaced();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            //DoTurn
            this.InvalidateVisual();
        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowWidth = (int)e.NewSize.Width;
            this.InvalidateVisual();
        }

        public void GameView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            Point map = ScreenToPoint((int)p.X, (int)p.Y);
            game.Map.SetRoad((int)Math.Floor(map.Y), (int)Math.Floor(map.X) - 1);
            this.InvalidateVisual();
        }

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
                    dc.DrawImage(item.Image, new Rect(screenX, screenY, cellSize, cellSize));
                }
            }
        }
    }
}
