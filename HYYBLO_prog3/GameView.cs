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
        DispatcherTimer timer;
        const int cellSize = 32;
        int WindowWidth = 800;

        public GameView()
        {
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
            System.Diagnostics.Debug.WriteLine(ScreenToPoint((int)p.X, (int)p.Y));
        }

        public Point ScreenToPoint(int x, int y)
        {
            Point p = new Point();
            int halfCell = (cellSize / 2);
            x = x + cam.X;
            y = y + cam.Y;
            x = (WindowWidth / 2) + x;
            p.X = (x / halfCell + y / halfCell) / 2;
            p.Y = (y / halfCell - (x / halfCell)) / 2;
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
