using RLNET;
using RogueSharp;

namespace Shiv.Systems
{
    public class Commands
    {
        //If the player is able to move to the desired cell,
        //  return true. Otherwise, return false such as if
        //  the player tries to move to an invalid cell
        public bool MovePlayer(Core.Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch(direction)
            {
                case Core.Direction.Up:
                    {
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Core.Direction.Down:
                    {
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Core.Direction.Left:
                    {
                        x = Game.Player.X - 1;
                        break;
                    }
                case Core.Direction.Right:
                    {
                        x = Game.Player.X + 1;
                        break;
                    }
                default:
                    return false;
            }

            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true;
            }

            return false;
        }
    }
}
