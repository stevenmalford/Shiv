/* Name: Steven Alford
 * File: MapGenerator.cs
 * Date: 3/4/17
 * Desc: Generates a traversable map using a height and width integer.
 *       Initializes each cell to be walkable and transparent except
 *       for the outermost walls
 */

using RogueSharp;

namespace Shiv.Systems
{
    public class MapGenerator
    {
        //Instantiates height and width variables to generate the map with
        private readonly int _width;
        private readonly int _height;

        //Instantiates a DungeonMap variable to store the map
        private readonly Core.DungeonMap map;

        //Map Generator function taking height and width variables
        public MapGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            map = new Shiv.Core.DungeonMap();
        }

        //Create Map function that initializes a map, and then sets the
        //      properties of certain cells in the map space
        public Core.DungeonMap CreateMap()
        {
            //Initializes the map using the provided height and width
            map.Initialize(_width, _height);

            //For each cell in the map, set all cells to be walkable/transparent/explored
            foreach (Cell cell in map.GetAllCells())
            {
                map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }

            //For each cell in the first and last row, set all cells to not be 
            //      walkable/transparent but explored
            foreach (Cell cell in map.GetCellsInRows(0, _height - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            //For each cell in the first and last column, set all cells to not be
            //      walkable/transparent but explored
            foreach (Cell cell in map.GetCellsInColumns(0, _width - 1))
            {
                map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            //Return the generated map
            return map;
        }
    }
}
