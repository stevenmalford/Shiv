using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Shiv.Systems
{
    public class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;

        private readonly Core.DungeonMap map;

        public MapGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            map = new Shiv.Core.DungeonMap();
        }

        public Core.DungeonMap CreateMap()
        {
            map.Initialize(_width, _height);
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }

            foreach (Cell cell in map.GetCellsInRows(0, _height - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            foreach (Cell cell in map.GetCellsInColumns(0, _width - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            return map;
        }
    }
}
