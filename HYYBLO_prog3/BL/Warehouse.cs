//-----------------------------------------------------------------------
// <copyright file="Warehouse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    /// <summary>
    /// Type of a ware that a warehouse can serve
    /// </summary>
    public enum WareType
    {
        /// <summary>
        /// Ware between towns
        /// </summary>
        Mail,

        /// <summary>
        /// Ware between factory and town
        /// </summary>
        Goods,

        /// <summary>
        /// Ware between factory and mine
        /// </summary>
        Ore,

        /// <summary>
        /// No ware
        /// </summary>
        Nothing
    }

    /// <summary>
    /// Represents a warehouse a the given position
    /// </summary>
    public class Warehouse : INotifyPropertyChanged
    {
        /// <summary>
        /// X coordinate of the warehouse
        /// </summary>
        private int x;

        /// <summary>
        /// Y coordinate of the warehouse
        /// </summary>
        private int y;

        /// <summary>
        /// Reference to the other warehouses
        /// </summary>
        private ObservableCollection<Warehouse> warehouses;

        /// <summary>
        /// Target warehouse of this warehouse
        /// </summary>
        private Warehouse target;

        /// <summary>
        /// Type of a ware that the warehouse serves
        /// </summary>
        private WareType type;

        /// <summary>
        /// Number of cars the warehouse sends
        /// </summary>
        private int numberOfCars;

        /// <summary>
        /// Cars sent out since last start
        /// </summary>
        private int carsSent;

        /// <summary>
        /// Timer for sending cars
        /// </summary>
        private DispatcherTimer timer;

        /// <summary>
        /// Reference for the game
        /// </summary>
        private Game game;

        /// <summary>
        /// Surrounding ware types
        /// </summary>
        private Dictionary<WareType, int> waretypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Warehouse"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the warehouse</param>
        /// <param name="y">Y coordinate of the warehouse</param>
        /// <param name="g">Reference to the game</param>
        public Warehouse(int x, int y, Game g)
        {
            this.X = x;
            this.Y = y;
            this.game = g;
            this.warehouses = this.game.Warehouses;
            this.CargoType = WareType.Nothing;
            this.waretypes = this.game.Map.SetType(this.X, this.Y);
            this.NumberOfCars = 1;
        }

        /// <summary>
        /// Event for property changing
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the X coordinate of the Warehouse
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the Warehouse
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets or sets the other warehouse references in the warehouse
        /// </summary>
        public ObservableCollection<Warehouse> Warehouses
        {
            get
            {
                ObservableCollection<Warehouse> temp = new ObservableCollection<Warehouse>();
                foreach (Warehouse w in this.warehouses)
                {
                    if (!(w.X == this.X && w.Y == this.Y))
                    {
                        temp.Add(w);
                    }
                }

                return temp;
            }

            set
            {
                this.warehouses = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of ware that the warehouse serves
        /// </summary>
        public WareType CargoType
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("CargoType"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the target of this warehouse
        /// </summary>
        public Warehouse Target
        {
            get
            {
                return this.target;
            }

            set
            {
                this.target = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Target"));
                }
            }
        }

        /// <summary>
        /// Gets the name of the warehouse
        /// </summary>
        public string Name
        {
            get
            {
                return string.Format("Warehouse at {0},{1}", this.X, this.Y);
            }
        }

        /// <summary>
        /// Gets or sets the number of cars the warehouse sends
        /// </summary>
        public int NumberOfCars
        {
            get
            {
                return this.numberOfCars;
            }

            set
            {
                this.numberOfCars = value;
            }
        }

        /// <summary>
        /// Gets the array for the warehouse types
        /// </summary>
        public List<WareType> WarehouseTypes
        {
            get
            {
                List<WareType> list = new List<WareType>();
                list.Add(WareType.Nothing);

                if (this.waretypes[WareType.Goods] > 0)
                {
                    list.Add(WareType.Goods);
                }

                if (this.waretypes[WareType.Ore] > 0)
                {
                    list.Add(WareType.Ore);
                }

                if (this.waretypes[WareType.Mail] > 0)
                {
                    list.Add(WareType.Mail);
                }

                return list;
            }
        }

        /// <summary>
        /// Converts the object to a string
        /// </summary>
        /// <returns>A string with the warehouse's data</returns>
        public override string ToString()
        {
            return string.Format("X: {0} Y: {1}", this.X, this.Y);
        }

        /// <summary>
        /// Starts a number of cars set in the numberOfCars value
        /// </summary>
        public void StartCars()
        {
            if (this.Target != null)
            {
                this.carsSent = 0;
                this.timer = new DispatcherTimer();
                this.timer.Interval = TimeSpan.FromMilliseconds(100);
                this.timer.Tick += this.Timer_Tick;
                this.timer.Start();
            }
        }

        /// <summary>
        /// Decides if the warehouse accepts the ware
        /// </summary>
        /// <param name="type">Type of the ware</param>
        /// <returns>Returns true if the warehouse accepts the ware</returns>
        public bool AcceptWare(WareType type)
        {
            switch (type)
            {
                case WareType.Goods:
                    return this.WarehouseTypes.Contains(WareType.Mail);
                case WareType.Mail:
                    return this.WarehouseTypes.Contains(WareType.Mail);
                case WareType.Ore:
                    return this.WarehouseTypes.Contains(WareType.Goods);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Sends out a car every tick
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the tick</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.timer.Interval = TimeSpan.FromSeconds(5);
            if (this.carsSent < this.numberOfCars)
            {
                this.game.SetVehicle(this.X + 1, this.Y, this, this.Target);
                this.carsSent++;
            }
            else
            {
                this.timer.Stop();
                this.numberOfCars = 0;
                this.carsSent = 0;
            }
        }
    }
}
