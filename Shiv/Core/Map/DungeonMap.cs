/* Name: Steven Alford
 * File: DungeonMap.cs
 * Date: 3/5/17
 * Desc: A map class that stores the cells and sets their properties based
 *       on the map generator. Also handles field of view for the player and
 *       updates the colors for the cells based on the position of the player.
 *       Also determines actor position based on cell properties.
 */

using System.Collections.Generic;
using RLNET;
using RogueSharp;
using Shiv.Systems;
using System;
using System.Linq;

namespace Shiv.Core
{
    //Creates a class that uses RogueSharp's map class
    //      and expands its functionality
    public class DungeonMap : Map
    {
        //Creates an item for both a closed and open chest
        public ItemChest ChestOpen { get; set; }
        public ItemChest ChestClosed { get; set; }

        //Creates the stairs up and down
        public Stairs StairsUp { get; set; }
        public Stairs StairsDown { get; set; }

        public Colors Colors { get; set; }

        //Creates a linked list of rectangles that will be used
        //      to store room properties
        public List<Rectangle> Rooms;

        private List<Monster> _monsters;

        //A dungeonMap method that creates a dynamic list
        //      of rooms to be used when creating the map
        public DungeonMap()
        {
            Rooms = new List<Rectangle>();
            _monsters = new List<Monster>();
        }

        //Places the actor on the map
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            //If the cell is walkable allow the actor to move to
            //      its position
            if(GetCell(x,y).IsWalkable)
            {
                //The cell that the actor is on currently is not
                //      walkable, and therefore needs to be changed
                //      once the actor moves to allow for movement
                //      to that cell later
                SetIsWalkable(actor.X, actor.Y, true);
                //Update the actor's position
                actor.X = x;
                actor.Y = y;
                //Sets the new cell that the actor is standing on
                //  to not walkable, since two actors cannot stand
                //  in the same cell
                SetIsWalkable(actor.X, actor.Y, false);
                //If the actor is the player, update FOV
                if (actor is Player)
                {
                    UpdatePlayerFOV();
                }
                return true;
            }
            //If the player cannot be moved to the new cell/tile
            return false;
        }

        //Sets the cell to be walkable or not
        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        //Draws the map to the screen
        public void Draw(RLConsole mapConsole, RLConsole statsConsole)
        {
            //For each cell that exists, set the symbol based on
            //      logic provided in SetSymbolForCell
            foreach (Cell cell in GetAllCells())
            {
                SetSymbolForCell(mapConsole, cell);
            }

            //Draws the stairs up
            StairsUp.Draw(mapConsole, this);
            //Draws the stairs down and instantiates the interactive
            //      object
            StairsDown.Draw(mapConsole, this);

            //Draws the open chest
            ChestOpen.Draw(mapConsole, this);
            //Draws the closed chest and instantiates the interactive
            //      object
            ChestClosed.Draw(mapConsole, this);

            int i = 0;
            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);
                if (IsInFov(monster.X, monster.Y))
                {
                    monster.DrawStats(statsConsole, i);
                    i++;
                }
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
                    //First set of levels
                    if (Game.mapLevel <= 5)
                    {
                        console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, (char)177);
                    }
                    //Second set of levels
                    else if (Game.mapLevel <= 10)
                    {
                        //Colors.FloorFov = ;
                        //Colors.FloorBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, (char)177);
                    }
                    //This set of levels
                    else if (Game.mapLevel <= 15)
                    {
                        //Colors.FloorFov = ;
                        //Colors.FloorBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, (char)177);
                    }
                    //Fourth set of levels
                    else if (Game.mapLevel <= 20)
                    {
                        //Colors.FloorFov = ;
                        //Colors.FloorBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, (char)177);
                    }
                    //Fifth and final set of levels
                    else if (Game.mapLevel <= 25)
                    {
                        //Colors.FloorFov = ;
                        //Colors.FloorBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, (char)177);
                    }
                }
                //Walls
                else
                {
                    //First set of levels
                    if (Game.mapLevel <= 5)
                    {
                        console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                    }
                    //Second set of levels
                    else if (Game.mapLevel <= 10)
                    {
                        //Colors.WallFov = ;
                        //Colors.WallBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                    }
                    //Third set of levels
                    else if (Game.mapLevel <= 15)
                    {
                        //Colors.WallFov = ;
                        //Colors.WallBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                    }
                    //Fourth set of levels
                    else if (Game.mapLevel <= 20)
                    {
                        //Colors.WallFov = ;
                        //Colors.WallBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                    }
                    //Fifth and final set of levels
                    else if (Game.mapLevel <= 25)
                    {
                        //Colors.WallFov = ;
                        //Colors.WallBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                    }
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
                    //First set of levels
                    if (Game.mapLevel <= 5)
                    {
                        console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, (char)177);
                    }
                    //Second set of levels
                    else if (Game.mapLevel <= 10)
                    {
                        //Colors.FloorFov = ;
                        //Colors.FloorBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, (char)177);
                    }
                    //This set of levels
                    else if (Game.mapLevel <= 15)
                    {
                        //Colors.FloorFov = ;
                        //Colors.FloorBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, (char)177);
                    }
                    //Fourth set of levels
                    else if (Game.mapLevel <= 20)
                    {
                        //Colors.FloorFov = ;
                        //Colors.FloorBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, (char)177);
                    }
                    //Fifth and final set of levels
                    else if (Game.mapLevel <= 25)
                    {
                        //Colors.FloorFov = ;
                        //Colors.FloorBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, (char)177);
                    }
                }
                //Walls
                else
                {
                    
                    //First set of levels
                    if (Game.mapLevel <= 5)
                    {
                        console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                    }
                    //Second set of levels
                    else if (Game.mapLevel <= 10)
                    {
                        //Colors.WallFov = ;
                        //Colors.WallBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                    }
                    //Third set of levels
                    else if (Game.mapLevel <= 15)
                    {
                        //Colors.WallFov = ;
                        //Colors.WallBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                    }
                    //Fourth set of levels
                    else if (Game.mapLevel <= 20)
                    {
                        //Colors.WallFov = ;
                        //Colors.WallBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                    }
                    //Fifth and final set of levels
                    else if (Game.mapLevel <= 25)
                    {
                        //Colors.WallFov = ;
                        //Colors.WallBackgroundFov = ;
                        console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                    }
                }
            }
        }

        //Add the player to the DungeonMap and set the cell that the player
        //      spawns on to not walkable, then update the player's starting
        //      field of view
        public void AddPlayer(Player player)
        {
            Game.Player = player;            
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFOV();
            Game.SchedulingSystem.Add(player);
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

        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster);
            SetIsWalkable(monster.X, monster.Y, false);

            Game.SchedulingSystem.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            _monsters.Remove(monster);
            // After removing the monster from the map, make sure the cell is walkable again
            SetIsWalkable(monster.X, monster.Y, true);

            Game.SchedulingSystem.Remove(monster);
        }

        public Monster GetMonsterAt(int x, int y)
        {
            return _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        public Point GetRandomWalkableCell(Rectangle room)
        {
            if(DoesRoomHaveSpace(room))
            {
                for(int i = 0; i < 100;  i++)
                {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;

                    if(IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }

            return null;
        }

        public bool DoesRoomHaveSpace(Rectangle room)
        {
            for(int x = 1; x <= room.Width - 2; x++)
            {
                for(int y = 1; y <= room.Height - 2; y++)
                {
                    if(IsWalkable(x + room.X, y + room.Y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //Checks to see if the player is standing on the stairs going down
        public bool CanPlayerGoDown()
        {
            Player player = Game.Player;
            return StairsDown.X == player.X && StairsDown.Y == player.Y;
        }

        //Checks to see if the player is within 1 tile of the item
        public bool CanPlayerOpenChest()
        {
            Player player = Game.Player;
            if (ChestClosed.IsOpened == false)
            {
                return ChestClosed.X == player.X && ChestClosed.Y == player.Y ||
                       ChestClosed.X == player.X + 1 && ChestClosed.Y == player.Y ||
                       ChestClosed.X == player.X + 1 && ChestClosed.Y == player.Y + 1 ||
                       ChestClosed.X == player.X && ChestClosed.Y == player.Y + 1 ||
                       ChestClosed.X == player.X - 1 && ChestClosed.Y == player.Y + 1 ||
                       ChestClosed.X == player.X - 1 && ChestClosed.Y == player.Y ||
                       ChestClosed.X == player.X - 1 && ChestClosed.Y == player.Y - 1 ||
                       ChestClosed.X == player.X && ChestClosed.Y == player.Y - 1 ||
                       ChestClosed.X == player.X + 1 && ChestClosed.Y == player.Y - 1;
            }

            return false;
        }
    }
}
