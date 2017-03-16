/* Name: Steven Alford
 * File: Stairs.cs
 * Date: 3/15/17
 * Desc: A class for the stairs object that will move
 *       the player from one level down to the next 
 *       but the player can never go back up
 */

using RLNET;
using RogueSharp;
using Shiv.Interfaces;

namespace Shiv.Core
{
    public class Stairs : IDrawable
    {
        public RLColor Color
        { get; set; }
        public char Symbol
        { get; set; }
        public int X
        { get; set; }
        public int Y
        { get; set; }
        public bool IsUp
        { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if(!map.GetCell(X,Y).IsExplored)
            {
                return;
            }

            Symbol = IsUp ? (char) 24 : (char) 25;

            if(map.IsInFov(X,Y))
            {
                Color = Colors.Player;
            }
            else
            {
                Color = Colors.Floor;
            }

            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
