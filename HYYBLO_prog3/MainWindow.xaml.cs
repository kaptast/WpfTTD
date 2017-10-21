using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HYYBLO_prog3
{
    public enum BuildType { Nothing, Road, Warehouse, Delete, Park, Vehicle }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameView view;
        BuildType selectedItem;

        public MainWindow()
        {
            InitializeComponent();
            selectedItem = BuildType.Nothing;
            view = new GameView();
            this.PlayArea.Content = view;
            this.KeyDown += MainWindow_KeyDown;
            this.KeyUp += MainWindow_KeyUp;
            this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            view.GameView_MouseLeftButtonDown(sender, e, selectedItem);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            view.GameView_KeyDown(sender, e);
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            view.GameView_KeyUp(sender, e);
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(e.Delta > 0)
            {
                view.ZoomIn();
            }
            else
            {
                view.ZoomOut();
            }
        }

        private void btnRoad_Click(object sender, RoutedEventArgs e)
        {
            if(selectedItem != BuildType.Road)
            {
                selectedItem = BuildType.Road;
            }
            else
            {
                selectedItem = BuildType.Nothing;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem != BuildType.Delete)
            {
                selectedItem = BuildType.Delete;
            }
            else
            {
                selectedItem = BuildType.Nothing;
            }
        }

        private void btnVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem != BuildType.Vehicle)
            {
                selectedItem = BuildType.Vehicle;
            }
            else
            {
                selectedItem = BuildType.Nothing;
            }
        }

        private void btnWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem != BuildType.Warehouse)
            {
                selectedItem = BuildType.Warehouse;
            }
            else
            {
                selectedItem = BuildType.Nothing;
            }
        }
    }
}
