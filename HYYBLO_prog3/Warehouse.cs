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
            map.map[x, y] = new WarehouseBuilding(x, y);
            map.map[x, y + 1] = new WarehouseLot12(x, y);
            map.map[x + 1, y] = new WarehouseLot21(x, y);
            map.map[x + 1, y + 1] = new WarehouseLot22(x, y);
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
        public WarehouseLot12(int x, int y) : base(x, y + 1)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/warehouse12.png"));
        }
    }

    class WarehouseLot21 : Building
    {
        public WarehouseLot21(int x, int y) : base(x + 1, y)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/warehouse21.png"));
        }
    }

    class WarehouseLot22 : Building
    {
        public WarehouseLot22(int x, int y) : base(x + 1, y + 1)
        {
            Image = new System.Windows.Media.Imaging.BitmapImage(new Uri("D:/Dokumentumok/Visual Studio 2015/Projects/oenik_prog3_2017osz_hyyblo/HYYBLO_prog3/Images/Buildings/warehouse22.png"));
        }
    }
}
