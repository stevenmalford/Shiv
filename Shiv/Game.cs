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
        //Sets the initial dungeon level and creates a seed variable
        private static int mapLevel = 1;
        private static int mapSeed;

        //Creates a boolean argument to tell the render event to run
        //      if something on the screen has updated, else keep the
        //      same screen
        private static bool renderRequired = true;
        
        //Instantiates a Commands variable that will move the player
        //      from their previous coordinates to the new coords
        public static Commands Commands { get; set; }

        //Declares a Player object
        public static Player Player { get; set; }
        //Declares an inventory object
        public static Inventory Inventory { get; set; }


        public static ItemGenerator ItemGenerator { get; set; }
        
        
        //Declares a DungeonMap object
        public static DungeonMap DungeonMap { get; private set; }

        /// <summary>
        /// Console dimensions and subdivisions
        ///     - (1) Console window size
        ///     - (2) Map subdivision size
        ///     - (3) Message subdivision size
        ///     - (4) Stats subdivison size
        ///     - (5) Inventory subdivision size
        ///     - (6) Armour subdivision size
        /// </summary>

        //(1) Sets the console size in tiles (8x8 pixels)
        private static readonly int screenWidth = 150;
        private static readonly int screenHeight = 100;
        private static RLRootConsole rootConsole;

        //(2) Sets the map size in tiles (8x8 pixels)
        private static readonly int mapWidth = 120;
        private static readonly int mapHeight = 70;
        private static RLConsole mapConsole;

        //(3) Sets the message window size in tiles (8x8 pixels)
        private static readonly int messageWidth = 90;
        private static readonly int messageHeight = 30;
        private static RLConsole messageConsole;

        //(4) Sets the stats window size in tiles (8x8 pixels)
        private static readonly int statsWidth = 30;
        private static readonly int statsHeight = 70;
        private static RLConsole statsConsole;

        //(5) Sets the inventory window size in tiles (8x8 pixels)
        private static readonly int inventoryWidth = 30;
        private static readonly int inventoryHeight = 30;
        private static RLConsole inventoryConsole;

        //(6) Sets the armour window size in tiles (8x8 pixels)
        private static readonly int armourWidth = 38;
        private static readonly int armourHeight = 30;
        private static RLConsole armourConsole;

        //Declares a random object to store a random variable
        public static IRandom Random { get; private set; }

        //Main Game loop
        public static void Main()
        {
            //Generates a semi-random number from the current timestamp
            int seed = (int)DateTime.UtcNow.Ticks;
            mapSeed = seed;
            //Sets the random number to be equal to the current seed
            Random = new DotNetRandom(seed);

            //File name for the custom font (RLNet library requires custom 8x8 font)
            string fontFile = "terminal8x8.png";
            //Title included at the top of the console window
            //      UPDATE LATER TO SHOW CURRENT DUNGEON LEVEL
            string windowTitle = "Shiv - Level 1 - Seed: " + seed + " Level: " + mapLevel;

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
            armourConsole    = new RLConsole(armourWidth, armourHeight);

            //Creates a new map generator
            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight, 40, 14, 7, mapLevel);
            //Creates a new map using the map generator instantiated above
            DungeonMap = mapGenerator.CreateMap();
            //Updates the Player's field of view on the map
            DungeonMap.UpdatePlayerFOV();

            Inventory = new Inventory();

            //Sets background color for each subdivision of the console window
            //Prints string to label each subdivision
            mapConsole.SetBackColor(0, 0, mapWidth, mapHeight, Colors.FloorBackground);

            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, Palette.AlternateLightest);
            messageConsole.Print(1, 1, "Messages", Colors.TextHeading);
            messageConsole.Print(1, 2, "________", Colors.TextHeading);

            statsConsole.SetBackColor(0, 0, statsWidth, statsHeight, Palette.AlternateLightest);
            statsConsole.Print(1, 1, "Stats", Colors.TextHeading);
            statsConsole.Print(1, 2, "_____", Colors.TextHeading);
            statsConsole.Print(1, 5, "Health:   "  + Player.currentHealth + "/" + Player.maxHealth, Colors.TextHeading);
            statsConsole.Print(1, 8, "Defense:  "  + Player.currentDefense, Colors.TextHeading);
            statsConsole.Print(1, 10, "Accuracy: " + Player.currentAccuracy, Colors.TextHeading);
            statsConsole.Print(1, 12, "Damage:   " + Player.damage, Colors.TextHeading);

            statsConsole.Print(1, 21, "Enemies:", Colors.TextHeading);
            statsConsole.Print(1, 22, "________", Colors.TextHeading);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, Palette.PrimaryDarker);
            inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);
            inventoryConsole.Print(1, 2, "_________", Colors.TextHeading);
            inventoryConsole.Print(1, 5, "Slot 1: "  + Inventory.slot1, Colors.TextHeading);
            inventoryConsole.Print(1, 7, "Slot 2: "  + Inventory.slot2, Colors.TextHeading);
            inventoryConsole.Print(1, 9, "Slot 3: "  + Inventory.slot3, Colors.TextHeading);
            inventoryConsole.Print(1, 11, "Slot 4: " + Inventory.slot4, Colors.TextHeading);
            inventoryConsole.Print(1, 13, "Slot 5: " + Inventory.slot5, Colors.TextHeading);

            armourConsole.SetBackColor(0, 0, armourWidth, armourHeight, Palette.PrimaryDarker);
            armourConsole.Print(1, 1, "Armour", Colors.TextHeading);
            armourConsole.Print(1, 2, "______", Colors.TextHeading);
            armourConsole.Print(1, 5, "Head:   "  + Player.Head, Colors.TextHeading);
            armourConsole.Print(1, 7, "Neck:   "  + Player.Neck, Colors.TextHeading);
            armourConsole.Print(1, 9, "Chest:  "  + Player.Chest, Colors.TextHeading);
            armourConsole.Print(1, 11, "Legs:   " + Player.Legs, Colors.TextHeading);
            armourConsole.Print(1, 13, "Gloves: " + Player.Gloves, Colors.TextHeading);
            armourConsole.Print(1, 15, "Boots:  " + Player.Boots, Colors.TextHeading);
            armourConsole.Print(1, 21, "Weapon: " + Player.Weapon, Colors.TextHeading);
            armourConsole.Print(1, 23, "Shield: " + Player.Shield, Colors.TextHeading);

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
            bool didPlayerAct = false;
            //Creates a keypress variable to store the last key pressed by
            //      the user
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();

            //If there is a keypress
            if(keyPress != null)
            {
                //If the player presses up or W
                if(keyPress.Key == RLKey.Up || keyPress.Key == RLKey.W)
                {
                    //Move the player up and update didPlayerMove
                    didPlayerAct = Commands.MovePlayer(Direction.Up);
                }
                //If the player presses down or S
                else if (keyPress.Key == RLKey.Down || keyPress.Key == RLKey.S)
                {
                    //Move the player down and update didPlayerMove
                    didPlayerAct = Commands.MovePlayer(Direction.Down);
                }
                //If the player presses left or A
                else if (keyPress.Key == RLKey.Left || keyPress.Key == RLKey.A)
                {
                    //Move the player left and update didPlayerMove
                    didPlayerAct = Commands.MovePlayer(Direction.Left);
                }
                //If the player presses right or D
                else if (keyPress.Key == RLKey.Right || keyPress.Key == RLKey.D)
                {
                    //Move the player right and update didPlayerMove
                    didPlayerAct = Commands.MovePlayer(Direction.Right);
                }
                //If the player presses E
                else if (keyPress.Key == RLKey.E)
                {
                    //Checks to see if the player is standing on the stairs
                    //      to go down to the next level
                    if(DungeonMap.CanPlayerGoDown())
                    {
                        //Create a new map for the second floor of the dungeon
                        MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight, 40, 14, 7, ++mapLevel);
                        DungeonMap = mapGenerator.CreateMap();
                        //Creates a new command instance to contorl the player
                        Commands = new Commands();
                        //Updates the console title to reflect the dungeon level
                        rootConsole.Title = $"Shiv - Level 1 - Seed: {mapSeed} - Level: {mapLevel}";
                        //Renders the screen
                        didPlayerAct = true;
                    }

                    //checks to see if the player is within 1 tile of the item
                    if(DungeonMap.CanPlayerOpenChest())
                    {
                        //If the player opens the chest, set the icon to be the
                        //      open chest and have open chest properties
                        DungeonMap.ChestClosed = DungeonMap.ChestOpen;

                        ItemGenerator = new ItemGenerator();
                        ItemGenerator.GenerateRandomItem();
                        ItemGenerator.UpdatePlayerStats();
                        
                        //Renders the screen
                        didPlayerAct = true;
                    }
                }
                //If the player presses escape
                else if (keyPress.Key == RLKey.Escape)
                {
                    //Close the game
                    rootConsole.Close();
                }
            }

            //If the player moved
            if(didPlayerAct)
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

                statsConsole.Clear();
                statsConsole.SetBackColor(0, 0, statsWidth, statsHeight, Palette.AlternateLightest);
                statsConsole.Print(1, 1, "Stats", Colors.TextHeading);
                statsConsole.Print(1, 2, "_____", Colors.TextHeading);
                statsConsole.Print(1, 5, "Health:   "  + Player.currentHealth + "/" + Player.maxHealth, Colors.TextHeading);
                statsConsole.Print(1, 8, "Defense:  "  + Player.currentDefense, Colors.TextHeading);
                statsConsole.Print(1, 10, "Accuracy: " + Player.currentAccuracy, Colors.TextHeading);
                statsConsole.Print(1, 12, "Damage:   " + Player.damage, Colors.TextHeading);

                statsConsole.Print(1, 21, "Enemies:", Colors.TextHeading);
                statsConsole.Print(1, 22, "________", Colors.TextHeading);

                inventoryConsole.Clear();
                inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, Palette.PrimaryDarker);
                inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);
                inventoryConsole.Print(1, 2, "_________", Colors.TextHeading);
                inventoryConsole.Print(1, 5, "Slot 1: "  + Inventory.slot1, Colors.TextHeading);
                inventoryConsole.Print(1, 7, "Slot 2: "  + Inventory.slot2, Colors.TextHeading);
                inventoryConsole.Print(1, 9, "Slot 3: "  + Inventory.slot3, Colors.TextHeading);
                inventoryConsole.Print(1, 11, "Slot 4: " + Inventory.slot4, Colors.TextHeading);
                inventoryConsole.Print(1, 13, "Slot 5: " + Inventory.slot5, Colors.TextHeading);

                armourConsole.Clear();
                armourConsole.SetBackColor(0, 0, armourWidth, armourHeight, Palette.PrimaryDarker);
                armourConsole.Print(1, 1, "Armour", Colors.TextHeading);
                armourConsole.Print(1, 2, "______", Colors.TextHeading);
                armourConsole.Print(1, 5, "Head:   "  + Player.Head, Colors.TextHeading);
                armourConsole.Print(1, 7, "Neck:   "  + Player.Neck, Colors.TextHeading);
                armourConsole.Print(1, 9, "Chest:  "  + Player.Chest, Colors.TextHeading);
                armourConsole.Print(1, 11, "Legs:   " + Player.Legs, Colors.TextHeading);
                armourConsole.Print(1, 13, "Gloves: " + Player.Gloves, Colors.TextHeading);
                armourConsole.Print(1, 15, "Boots:  " + Player.Boots, Colors.TextHeading);
                armourConsole.Print(1, 21, "Weapon: " + Player.Weapon, Colors.TextHeading);
                armourConsole.Print(1, 23, "Shield: " + Player.Shield, Colors.TextHeading);

                //Blits the subdivisions to the window
                //(https://en.wikipedia.org/wiki/Bit_blit)
                //Map
                RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, 0);
                //Messages
                RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, inventoryWidth, (screenHeight - messageHeight));
                //Stats
                RLConsole.Blit(statsConsole, 0, 0, statsWidth, statsHeight, rootConsole, mapWidth, 0);
                //Inventory
                RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, mapHeight);
                //Armour
                RLConsole.Blit(armourConsole, 0, 0, armourWidth, armourHeight, rootConsole, screenWidth - armourWidth, screenHeight - armourHeight);

                //Tells RLNet to draw the console that we specified in rootConsole
                rootConsole.Draw();

                //After everything has rendered, don't render again until an update
                renderRequired = false;
            }
        }
    }
}
