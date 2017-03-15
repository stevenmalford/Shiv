/* Name: Steven Alford
 * File: Game.cs
 * Date: 3/3/17
 * Desc: Contains the window setup/subdivisions and the main game loop.
 *       Also handles updating the game loop and rendering objects to
 *       the screen.
 */

using RLNET;
using RogueSharp;
using RogueSharp.Random;
using Shiv.Core;
using Shiv.Systems;
using System;

namespace Shiv
{
    public class Game
    {
        //Creates a boolean argument to tell the render event to run
        //      if something on the screen has updated, else keep the
        //      same screen
        private static bool renderRequired = true;
        //Instantiates a Commands variable that will move the player
        //      from their previous coordinates to the new coords
        public static Commands Commands { get; set; }

        //Declares a Player object
        public static Player Player { get; set; }
        //Declares a DungeonMap object
        public static DungeonMap DungeonMap { get; private set; }

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

        //(3) Sets the message window size in tiles (8x8 pixels)
        private static readonly int messageWidth = 120;
        private static readonly int messageHeight = 16;
        private static RLConsole messageConsole;

        //(4) Sets the stats window size in tiles (8x8 pixels)
        private static readonly int statsWidth = 30;
        private static readonly int statsHeight = 100;
        private static RLConsole statsConsole;

        //(5) Sets the inventory window size in tiles (8x8 pixels)
        private static readonly int inventoryWidth = 120;
        private static readonly int inventoryHeight = 16;
        private static RLConsole inventoryConsole;

        //Declares a random object to store a random variable
        public static IRandom Random { get; private set; }

        //Main Game loop
        public static void Main()
        {
            //Generates a semi-random number from the current timestamp
            int seed = (int)DateTime.UtcNow.Ticks;
            //Sets the random number to be equal to the current seed
            Random = new DotNetRandom(seed);

            //File name for the custom font (RLNet library requires custom 8x8 font)
            string fontFile = "terminal8x8.png";
            //Title included at the top of the console window
            //      UPDATE LATER TO SHOW CURRENT DUNGEON LEVEL
            string windowTitle = "Shiv - Level 1 - Seed: " + seed;

            //Creates a new commands to control the player
            Commands = new Commands();

            //Welcome Screen
            //Console.WriteLine("Welcome to Shiv! Please enter your name: ");
            //Player.Name = Console.ReadLine();

            //Creates new console window using below parameters
            //(font file name, screen width, screen height, width per tile,
            //      height per tile, scale of tiles, console title)
            rootConsole = new RLRootConsole(fontFile, screenWidth, screenHeight, 8, 8, 1f, windowTitle);

            //Instantiates console subdivisions
            mapConsole       = new RLConsole(mapWidth, mapHeight);
            messageConsole   = new RLConsole(messageWidth, messageHeight);
            statsConsole     = new RLConsole(statsWidth, statsHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);

            //Creates a new map generator
            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight, 40, 14, 7);
            //Creates a new map using the map generator instantiated above
            DungeonMap = mapGenerator.CreateMap();
            //Updates the Player's field of view on the map
            DungeonMap.UpdatePlayerFOV();

            //Sets background color for each subdivision of the console window
            //Prints string to label each subdivision
            mapConsole.SetBackColor(0, 0, mapWidth, mapHeight, Colors.FloorBackground);

            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, Palette.PrimaryLightest);
            messageConsole.Print(1, 1, "Messages", Colors.TextHeading);

            statsConsole.SetBackColor(0, 0, statsWidth, statsHeight, Palette.PrimaryDarker);
            statsConsole.Print(1, 1, "Stats", Colors.TextHeading);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, Palette.PrimaryLightest);
            inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);

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
            //Creates a new variable to store whether the player has moved
            //      Used to determine where to move the player's symbol if
            //      the player has moved
            bool didPlayerMove = false;
            //Creates a keypress variable to store the last key pressed by
            //      the user
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();

            //If there is a keypress
            if(keyPress != null)
            {
                //If the player presses up
                if(keyPress.Key == RLKey.Up)
                {
                    //Move the player up and update didPlayerMove
                    didPlayerMove = Commands.MovePlayer(Core.Direction.Up);
                }
                //If the player presses down
                else if (keyPress.Key == RLKey.Down)
                {
                    //Move the player down and update didPlayerMove
                    didPlayerMove = Commands.MovePlayer(Core.Direction.Down);
                }
                //If the player presses left
                else if (keyPress.Key == RLKey.Left)
                {
                    //Move the player left and update didPlayerMove
                    didPlayerMove = Commands.MovePlayer(Core.Direction.Left);
                }
                //If the player presses right
                else if (keyPress.Key == RLKey.Right)
                {
                    //Move the player right and update didPlayerMove
                    didPlayerMove = Commands.MovePlayer(Core.Direction.Right);
                }
                //If the player presses escape
                else if (keyPress.Key == RLKey.Escape)
                {
                    //Close the game
                    rootConsole.Close();
                }
            }

            //If the player moved
            if(didPlayerMove)
            {
                //Render the game screen
                renderRequired = true;
            }
        }
        //Event handler for RLNet render event
        private static void OnRootConsoleRender(object Sender, UpdateEventArgs e)
        {
            if (renderRequired)
            {
                //Draw the map subdivision to the screen
                DungeonMap.Draw(mapConsole);
                //Draw the player to the dungeon map
                Player.Draw(mapConsole, DungeonMap);

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

                //After everything has rendered, don't render again until an update
                renderRequired = false;
            }
        }
    }
}
