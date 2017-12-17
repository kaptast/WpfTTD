namespace Hyyblo_View
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using Hyyblo_Logic;
    using Hyyblo_Model;

    /// <summary>
    /// Interaction logic for Cars.xaml
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

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.vm.SelectedVehicle != null)
            {
                this.vm.Vehicles.Remove(this.vm.SelectedVehicle);
            }
        }
    }
}
