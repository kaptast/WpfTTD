//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_View
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the click event</param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            int size;

            if (int.TryParse(this.txtSize.Text, out size))
            {
                if (size >= 10)
                {
                    new GameWindow(size).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Map size too small");
                }
            }
            else
            {
                MessageBox.Show("Invalid parameter");
            }
        }

        /// <summary>
        /// Exits the program
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Arguments of the click event</param>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
