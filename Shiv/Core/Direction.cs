/* Name: Steven Alford
 * File: Direction.cs (Enum)
 * Date: 3/10/17
 * Desc: Enumerator defining the multiple directions that the player
 *       can move in relation to numpad numbers
 */

namespace Shiv.Core
{
    public enum Direction
    {
        //Since RogueSharp does not support diagonal movement,
        //  I have decided not to include the enumeration for
        //  those movements
        None = 0,
        Down = 2,
        Left = 4,
        Center = 5,
        Right = 6,
        Up = 8,
    }
}
