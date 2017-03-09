using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;


namespace Shiv
{
    public class Game
    {
        public static Core.DungeonMap DungeonMap { get; private set; }

        /// <summary>
        /// Console dimensions and subdivisions
        ///     - (1) Console window size
        ///     - (2) Map subdivision size
        ///     - (3) Message subdivision size
        ///     - (4) Stats subdivison size
        ///     - (5) Inventory subdivision size
        /// </summary>

        //(1) Sets the console size in tiles (8x8 pixels)
        private static readonly int screenWidth = 150;
        private static readonly int screenHeight = 100;
        private static RLRootConsole rootConsole;

        //(2) Sets the map size in tiles (8x8 pixels)
        private static readonly int mapWidth = 120;
        private static readonly int mapHeight = 68;
        private static RLConsole mapConsole;

        //(3) Sets the message size in tiles (8x8 pixels)
        private static readonly int messageWidth = 120;
        private static readonly int messageHeight = 16;
        private static RLConsole messageConsole;

        //(4) Sets the stats size in tiles (8x8 pixels)
        private static readonly int statsWidth = 30;
        private static readonly int statsHeight = 100;
        private static RLConsole statsConsole;

        //(5) Sets the inventory size in tiles (8x8 pixels)
        private static readonly int inventoryWidth = 120;
        private static readonly int inventoryHeight = 16;
        private static RLConsole inventoryConsole;

        public static void Main()
        {
            //File name for the custom font (RLNet library requires custom 8x8 font)
            string fontFile = "terminal8x8.png";
            //Title included at the top of the console window
            //      UPDATE LATER TO SHOW SEED# & CURRENT DUNGEON LEVEL
            string windowTitle = "Shiv - Level 1";

            //Creates new console window using below parameters
            //(font file name, screen width, screen height, width per tile,
            //      height per tile, scale of tiles, console title)
            rootConsole = new RLRootConsole(fontFile, screenWidth, screenHeight, 8, 8, 1f, windowTitle);

            //Instantiates console subdivisions
            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statsConsole = new RLConsole(statsWidth, statsHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);

            Systems.MapGenerator mapGenerator = new Systems.MapGenerator(mapWidth, mapHeight);
            DungeonMap = mapGenerator.CreateMap();

            //Sets background color for each subdivision of the console window
            //Prints string to verify that each subdivision is where it belongs
            mapConsole.SetBackColor(0, 0, mapWidth, mapHeight, Core.Colors.FloorBackground);

            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, RLColor.Black);
            messageConsole.Print(1, 1, "Messages", Core.Colors.TextHeading);

            statsConsole.SetBackColor(0, 0, statsWidth, statsHeight, RLColor.Black);
            statsConsole.Print(1, 1, "Stats", Core.Colors.TextHeading);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, RLColor.Black);
            inventoryConsole.Print(1, 1, "Inventory", Core.Colors.TextHeading);

            //Instantiates a handler for RLNet's update event
            rootConsole.Update += OnRootConsoleUpdate;
            //Instantiates a handler for RLNet's render event
            rootConsole.Render += OnRootConsoleRender;

            //Begin the game loop
            rootConsole.Run();
        }

        //Event handler for RLNet update event
        private static void OnRootConsoleUpdate(object Sender, UpdateEventArgs e)
        {

        }
        //Event handler for RLNet render event
        private static void OnRootConsoleRender(object Sender, UpdateEventArgs e)
        {
            //Blits the subdivisions to the window
            //(https://en.wikipedia.org/wiki/Bit_blit)
            //Map
            RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeight);
            //Messages
            RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, (screenHeight - messageHeight));
            //Stats
            RLConsole.Blit(statsConsole, 0, 0, statsWidth, statsHeight, rootConsole, mapWidth, 0);
            //Inventory
            RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, 0);

            //Tells RLNet to draw the console that we specified in rootConsole
            rootConsole.Draw();

            DungeonMap.Draw(mapConsole);
        }
    }
}
