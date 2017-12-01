//-----------------------------------------------------------------------
// <copyright file="EditWarehouse.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_View
{
    using System;
    using System.Windows;
    using Hyyblo_Model;

    /// <summary>
    /// Interaction logic for EditWarehouse
    /// </summary>
    public partial class EditWarehouse : Window
    {
        /// <summary>
        /// Reference to the current warehouse
        /// </summary>
        private Warehouse w;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditWarehouse"/> class.
        /// </summary>
        /// <param name="wh">Warehouse of the window</param>
        public EditWarehouse(Warehouse wh)
        {
            this.InitializeComponent();
            this.w = wh;
            this.DataContext = wh;
        }

        /// <summary>
        /// Click call back for starting the cars
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments for the button click event</param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            this.w.StartCars();
        }
    }
}