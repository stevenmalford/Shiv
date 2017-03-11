/* Name: Steven Alford
 * File: DungeonMap.cs
 * Date: 3/5/17
 * Desc: A map class that stores the cells and sets their properties based
 *       on the map generator. Also handles field of view for the player and
 *       updates the colors for the cells based on the position of the player
 */

using RLNET;
using RogueSharp;

namespace Shiv.Core
{
    //Creates a class that uses RogueSharp's map class
    //      and expands its functionality
    public class DungeonMap : Map
    {
        //Draws the map to the screen
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

        //Sets the symbol based on what the cell's properties are
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
                //Floor
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                //Wall
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            //If the cell is not in the field of view,
            //      draw it to the screen using darker
            //      colors
            else
            {
                //Floor
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                //Wall
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }

        //Updates the player's field of view in relation to the map
        public void UpdatePlayerFOV()
        {
            //Sets the player to the player object created in Game.cs
            Player player = Game.Player;
            //Computes the field of view using RogueSharp's ComputeFov function
            ComputeFov(player.X, player.Y, player.Fov, true);
            //For each cell, if the cell is in the field of view of the player,
            //      set the properties of the cell and change it to isExplored
            foreach (Cell cell in GetAllCells())
            {
                if(IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
    }
}
