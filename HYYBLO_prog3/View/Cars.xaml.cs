//-----------------------------------------------------------------------
// <copyright file="Cars.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_View
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using Hyyblo_Logic;
    using Hyyblo_Model;

    /// <summary>
    /// Interaction logic for Cars
    /// </summary>
    public partial class Cars : Window
    {
        /// <summary>
        /// ViewModel of the window
        /// </summary>
        private ViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cars"/> class
        /// </summary>
        /// <param name="v">List of the vehicles</param>
        public Cars(ObservableCollection<Vehicle> v)
        {
            this.vm = new ViewModel(v);
            this.DataContext = this.vm;
            this.InitializeComponent();
        }

        /// <summary>
        /// Double click event of the window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the double click</param>
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.vm.SelectedVehicle != null)
            {
                this.vm.Vehicles.Remove(this.vm.SelectedVehicle);
            }
        }
    }
}
