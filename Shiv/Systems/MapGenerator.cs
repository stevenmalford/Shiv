/* Name: Steven Alford
 * File: MapGenerator.cs
 * Date: 3/4/17
 * Desc: Generates a traversable map using a height and width integer.
 *       Initializes each cell to be walkable and transparent except
 *       for the outermost walls
 */

using RogueSharp;
using Shiv.Core;
using System;
using System.Linq;

namespace Shiv.Systems
{
    public class MapGenerator
    {
        //Instantiates height and width variables to generate the map with,
        //      along with max # of rooms, as well as a room's minimum and 
        //      maximum size possible
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;

        //Instantiates a DungeonMap variable to store the map
        private readonly DungeonMap map;

        //Map Generator function taking height and width variables,
        //      and also the room specifications (also includes 
        //      which dungeon level the player is on to determine
        //      how strong the enemies will be)
        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, int mapLevel)
        {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;

            map = new DungeonMap();
        }

        //Create Map function that initializes a map, and then sets the
        //      properties of certain cells in the map space
        public DungeonMap CreateMap()
        {
            //Initializes the map using the provided height and width
            map.Initialize(_width, _height);

            //Once the room is created, if it does not intersect with another
            //      room on the map, it will be added
            for (int r = _maxRooms; r > 0; r--)
            {
                //Randomly generates room specifications using RNG instantiated
                //      in the main game loop. This determines how big and where
                //      the room is located on the map
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomX = Game.Random.Next(0, _width - roomWidth - 1);
                int roomY = Game.Random.Next(0, _height - roomHeight - 1);

                //Sets a new room variable equal to the room that was just created
                var newRoom = new Rectangle(roomX, roomY, roomWidth, roomHeight);
                //Checks to see if the new room intersects with any of the old rooms
                //      and sets a boolean value accordingly
                bool newRoomIntersects = map.Rooms.Any(room => newRoom.Intersects(room));

                //If the new room does not intersect, add the new room to the map
                if (!newRoomIntersects)
                {
                    map.Rooms.Add(newRoom);
                }
            }

            //Creates hallways between rooms
            for (int r = 1; r < map.Rooms.Count; r++)
            {
                //Starting from the first room generated, determines
                //      the center of the previous and current rooms
                //      to calculate where the hallways will originate
                //      and where they are connecting to
                int previousRoomCenterX = map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = map.Rooms[r].Center.X;
                int currentRoomCenterY = map.Rooms[r].Center.Y;

                //Randomly chooses which L shape hallway to generate
                //      The L is determined using the centers stored above
                //      (see README for detail)
                if (Game.Random.Next(1, 2) == 1)
                {
                    CreateHorizontalHall(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalHall(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                }
                else
                {
                    CreateVerticalHall(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalHall(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }

            //For each rectangle in the list, create a room
            foreach (Rectangle room in map.Rooms)
            {
                CreateRoom(room);
            }

            //Create the stairs up and down
            CreateStairs();
            //Create the item in the item room
            CreateItem();

            //Place the player on the map
            PlacePlayer();

            //Return generated map
            return map;
        }

        //Creates a room using the variables stored in the list of
        //      Rectangles
        private void CreateRoom(Rectangle room)
        {
            //Loops through the inside of the room
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    //Sets all the cells in the room to be walkable
                    //      by the player
                    map.SetCellProperties(x, y, true, true, false);
                }
            }
        }

        //Place the player inside the first room generated
        private void PlacePlayer()
        {
            //Stores the game's player as a player variable
            Player player = Game.Player;
            //If the player doesn't exist, create one
            if (player == null)
            {
                player = new Player();
            }

            //Place the player inside the center of the first
            //      room generated
            player.X = map.Rooms[0].Center.X;
            player.Y = map.Rooms[0].Center.Y;

            //Add the new player to the map
            map.AddPlayer(player);
        }

        //Method to create a horizontal hallway between rooms
        private void CreateHorizontalHall(int xStart, int xEnd, int yPos)
        {
            //Starting at the minimum x value between the 
            //      previous room and current room, create
            //      a horizontal path from the first room
            //      to the x position of the second room
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                //Set each cell along the way to be walkable
                map.SetCellProperties(x, yPos, true, true);
            }
        }

        //Method to create a vertical hallway between rooms
        private void CreateVerticalHall(int yStart, int yEnd, int xPos)
        {
            //Starting at the minimum y value between the
            //      previous room and current room, create
            //      a vertical path from the first room
            //      to the y position of the second room
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                //Set each cell along the way to be walkable
                map.SetCellProperties(xPos, y, true, true);
            }
        }

        //Method to create stairs up (shows the player where
        //      they started) and down (shows the player where
        //      the exit is and lets the player descend)
        private void CreateStairs()
        {
            //Create a new stairs instance that "goes up"
            map.StairsUp = new Stairs
            {
                //Create the stairs to the right of the player's
                //      starting position in the first room
                X = map.Rooms.First().Center.X + 1,
                Y = map.Rooms.First().Center.Y,
                IsUp = true
            };
            //Create a new stairs instance that goes down
            map.StairsDown = new Stairs
            {
                //Create the stairs in the last room that
                //      was generated
                X = map.Rooms.Last().Center.X,
                Y = map.Rooms.Last().Center.Y,
                IsUp = false
            };
        }

        //Method to create an item that will benefit the player in
        //      some way, generally increasing their stats
        private void CreateItem()
        {
            //Set the room that holds the item to be the
            //      room that is half the total number of
            //      rooms on the map (middle generated)
            int itemRoom = map.Rooms.Count / 2;

            //Create a new chest that is opened
            map.ChestOpen = new ItemChest
            {
                //Place the chest inside the item room
                X = map.Rooms[itemRoom].Center.X,
                Y = map.Rooms[itemRoom].Center.Y,
                IsOpened = true
            };
            //Create a new chest that is closed
            map.ChestClosed = new ItemChest
            {
                //Place the chest inside the item room
                X = map.Rooms[itemRoom].Center.X,
                Y = map.Rooms[itemRoom].Center.Y,
                IsOpened = false
            };
        }
    }
}