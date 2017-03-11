/* Name: Steven Alford
 * File: Player.cs
 * Date: 3/10/17
 * Desc: A player object for the user to move around on the screen
 *       and complete objectives. Given malleable attributes that
 *       can change during the game loop
 */

namespace Shiv.Core
{
    //Calls the Actor class which uses the IActor and IDrawable interfaces
    public class Player : Actor
    {
        public Player()
        {
            //The name of the player
            Name = "Rogue";
            //The symbol that represents the player on the screen
            Symbol = '@';
            //The color of the player's symbol
            Color = Colors.Player;
            //The players field of view/awareness
            Fov = 15;
            //The x coordinate of the player
            X = 10;
            //The y coordinate of the player
            Y = 10;
        }
    }
}
