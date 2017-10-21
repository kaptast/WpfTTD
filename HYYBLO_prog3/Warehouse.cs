using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HYYBLO_prog3
{
    class Warehouse
    {
        public Warehouse(int x, int y, Map map)
        {
            //Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/warehouse.png"));
            MapItem item = map.GetItemByCoord(x, y);
            map.map.Remove(item);
            map.map.Add(new WarehouseBuilding(x, y));

            item = map.GetItemByCoord(x, y + 1);
            map.map.Remove(item);
            map.map.Add(new WarehouseLot12(x, y + 1));

            item = map.GetItemByCoord(x + 1, y);
            map.map.Remove(item);
            map.map.Add(new WarehouseLot21(x + 1, y, map));

            item = map.GetItemByCoord(x + 1, y + 1);
            map.map.Remove(item);
            map.map.Add(new WarehouseLot22(x + 1, y + 1));
        }
    }

    class WarehouseBuilding : Building
    {
        public WarehouseBuilding(int x, int y) : base(x, y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/warehouse.png"));
        }
    }

    class WarehouseLot12 : Building
    {
        public WarehouseLot12(int x, int y) : base(x, y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/warehouse12.png"));
        }
    }

    class WarehouseLot21 : Road
    {
        public WarehouseLot21(int x, int y, Map map) : base(x, y, map)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/warehouse21.png"));
        }

        protected override void RoadChanged()
        {
            //Do nothing
        }
    }

    class WarehouseLot22 : Building
    {
        public WarehouseLot22(int x, int y) : base(x, y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/warehouse22.png"));
        }
    }
}
