﻿//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_View
{
    using System.Windows;
    using System.Windows.Input;
    using Hyyblo_Model;

    /// <summary>
    /// Enum for different build types
    /// </summary>
    public enum BuildType
    {
        /// <summary>
        /// No type selected
        /// </summary>
        Nothing,

        /// <summary>
        /// Road type
        /// </summary>
        Road,

        /// <summary>
        /// Warehouse type
        /// </summary>
        Warehouse,

        /// <summary>
        /// Deleting selected
        /// </summary>
        Delete,

        /// <summary>
        /// Park type
        /// </summary>
        Park,

        /// <summary>
        /// Vehicle type
        /// </summary>
        Vehicle
    }

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Reference to the drawing interface
        /// </summary>
        private GameView view;

        /// <summary>
        /// Currently selected BuildType
        /// </summary>
        private BuildType selectedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.selectedItem = BuildType.Nothing;
            this.view = new GameView();
            this.PlayArea.Content = this.view;
            this.KeyDown += this.MainWindow_KeyDown;
            this.KeyUp += this.MainWindow_KeyUp;
            this.MouseLeftButtonDown += this.MainWindow_MouseLeftButtonDown;
        }

        /// <summary>
        /// Callback function for a left mouse click, calls the View's GameView_MouseLeftButtonDown function
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the left click</param>
        private void MainWindow_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            this.view.GameView_MouseLeftButtonDown(e, this.selectedItem);
        }

        /// <summary>
        /// Callback function for a key press, calls the View's GameView_KeyDown function
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the key press</param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            this.view.GameView_KeyDown(sender, e);
        }

        /// <summary>
        /// Callback function for a key lift, calls the View's GameView_KeyUp function, which stops the camera movement
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the key lift</param>
        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            this.view.GameView_KeyUp(sender, e);
        }

        /// <summary>
        /// Zooms in or out the view according to the mouse wheel's delta in the event for using the mouse wheel
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Argument of the mouse wheel event</param>
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.view.ZoomIn();
            }
            else
            {
                this.view.ZoomOut();
            }
        }

        /// <summary>
        /// Callback function for a menu bar click, sets the current build type to Road
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the button click</param>
        private void BtnRoad_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedItem != BuildType.Road)
            {
                this.selectedItem = BuildType.Road;
            }
            else
            {
                this.selectedItem = BuildType.Nothing;
            }
        }

        /// <summary>
        /// Callback function for a menu bar click, sets the current build type to delete
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the button click</param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedItem != BuildType.Delete)
            {
                this.selectedItem = BuildType.Delete;
            }
            else
            {
                this.selectedItem = BuildType.Nothing;
            }
        }

        /// <summary>
        /// Callback function for a menu bar click, sets the current build type to Vehicle
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the button click</param>
        private void BtnVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedItem != BuildType.Vehicle)
            {
                this.selectedItem = BuildType.Vehicle;
            }
            else
            {
                this.selectedItem = BuildType.Nothing;
            }
        }

        /// <summary>
        /// Callback function for a menu bar click, sets the current build type to Warehouse
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the button click</param>
        private void BtnWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectedItem != BuildType.Warehouse)
            {
                this.selectedItem = BuildType.Warehouse;
            }
            else
            {
                this.selectedItem = BuildType.Nothing;
            }
        }
    }
}
