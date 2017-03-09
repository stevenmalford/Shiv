using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Shiv.Core
{
    //Creates a class that uses RogueSharp's map class
    //      and expands its functionality
    public class DungeonMap : Map
    {
        public void Draw(RLConsole mapConsole)
        {
            //Clears the map completely so we don't draw over it
            mapConsole.Clear();
            //For each cell that exists, set the symbol based on
            //      logic provided in SetSymbolForCell
            foreach (Cell cell in GetAllCells())
            {
                SetSymbolForCell(mapConsole, cell);
            }
        }

        private void SetSymbolForCell(RLConsole console, Cell cell)
        {
            //If the cell hasn't been explored/seen, 
            //      don't draw it to the screen
            if(!cell.IsExplored)
            { return; }

            //If the cell/tile is in the field of view,
            //      draw it to the screen using lighter
            //      colors than an explored, non-FOV tile
            if (IsInFov(cell.X, cell.Y))
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }
    }
}
