//-----------------------------------------------------------------------
// <copyright file="Warehouse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    /// <summary>
    /// Wrapper class for a warehouse
    /// </summary>
    public class Warehouse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Warehouse"/> class.
        /// </summary>
        /// <param name="x">X coordinate of the warehouse</param>
        /// <param name="y">Y coordinate of the warehouse</param>
        /// <param name="map">Map where the warehouse is placed</param>
        public Warehouse(int x, int y, Map map)
        {
            MapItem item = map.GetItemByCoord(x, y);
            map.MapContainer.Remove(item);
            map.MapContainer.Add(new WarehouseBuilding(x, y));

            item = map.GetItemByCoord(x, y + 1);
            map.MapContainer.Remove(item);
            map.MapContainer.Add(new WarehouseLot12(x, y + 1));

            item = map.GetItemByCoord(x + 1, y);
            map.MapContainer.Remove(item);
            map.MapContainer.Add(new WarehouseLot21(x + 1, y, map));

            item = map.GetItemByCoord(x + 1, y + 1);
            map.MapContainer.Remove(item);
            map.MapContainer.Add(new WarehouseLot22(x + 1, y + 1));
        }
    }
}
