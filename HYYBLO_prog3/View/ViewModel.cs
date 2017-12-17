//-----------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_View
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyyblo_Logic;
    using Hyyblo_Model;

    /// <summary>
    /// View model for the cars window
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// Vehicles of the game
        /// </summary>
        private ObservableCollection<Vehicle> vehicles;

        /// <summary>
        /// Selected vehicle of the list box
        /// </summary>
        private Vehicle selectedVehicle;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        /// <param name="v">List with vehicles</param>
        public ViewModel(ObservableCollection<Vehicle> v)
        {
            this.vehicles = v;
        }

        /// <summary>
        /// Gets the list of the vehicles
        /// </summary>
        public ObservableCollection<Vehicle> Vehicles
        {
            get
            {
                return this.vehicles;
            }
        }

        /// <summary>
        /// Gets or sets the selected vehicle
        /// </summary>
        public Vehicle SelectedVehicle
        {
            get
            {
                return this.selectedVehicle;
            }

            set
            {
                this.selectedVehicle = value;
            }
        }
    }
}
