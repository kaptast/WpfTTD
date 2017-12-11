//-----------------------------------------------------------------------
// <copyright file="WaretypeToColor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_View
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Data;
    using Hyyblo_Logic;

    /// <summary>
    /// Converts the waretype to a brush
    /// </summary>
    public class WaretypeToColor : IValueConverter
    {
        /// <summary>
        /// Converts the value to a color
        /// </summary>
        /// <param name="value">Type of the warehouse</param>
        /// <param name="targetType">Type of the target</param>
        /// <param name="parameter">Parameter of the type</param>
        /// <param name="culture">Info about the environment</param>
        /// <returns>A brush with the specific color</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WareType t = (WareType)value;

            Brush b = null;
            switch (t)
            {
                case WareType.Goods:
                    b = Brushes.LightBlue;
                    break;
                case WareType.Mail:
                    b = Brushes.LightGreen;
                    break;
                case WareType.Ore:
                    b = Brushes.LightCoral;
                    break;
                case WareType.Nothing:
                    b = Brushes.Transparent;
                    break;
            }

            return b;
        }

        /// <summary>
        /// Converts the brush to a waretype, Not implemented
        /// </summary>
        /// <param name="value">A brush</param>
        /// <param name="targetType">Type of the target</param>
        /// <param name="parameter">Parameter of the type</param>
        /// <param name="culture">Info about the environment</param>
        /// <returns>A specific waretype</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
