/* Name: Steven Alford
 * File: Item.cs
 * Date: 3/15/17
 * Desc: A generic item class that will generate a random item
 *       for the player
 */

using RLNET;
using RogueSharp;
using Shiv.Interfaces;

namespace Shiv.Core
{
    public class Item : IDrawable
    {
        public RLColor Color
        { get; set; }
        public char Symbol
        { get; set; }
        public int X
        { get; set; }
        public int Y
        { get; set; }
        public bool IsOpened
        { get; set; }

        public int X2
        { get; set; }
        public int Y2
        { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if(!map.GetCell(X, Y).IsExplored)
            { return; }

            Symbol = IsOpened ? (char)9 : (char)10;

            if(map.IsInFov(X, Y))
            {
                Color = Colors.Chest;
            }
            else
            {
                Color = Colors.Floor;
            }

            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
