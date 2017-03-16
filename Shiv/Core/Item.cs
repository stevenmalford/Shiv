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
        //Color
        public RLColor Color
        { get; set; }
        //Symbol of the object
        public char Symbol
        { get; set; }
        //X coordinate
        public int X
        { get; set; }
        //Y coordinate
        public int Y
        { get; set; }
        //Is the chest opened?
        public bool IsOpened
        { get; set; }
        
        //Draw the item chest to the screen
        public void Draw(RLConsole console, IMap map)
        {
            //If the cell is not explored, don't draw
            if(!map.GetCell(X, Y).IsExplored)
            { return; }

            //If the chest is opened, draw the first character
            //If the chest is closed, draw the second
            Symbol = IsOpened ? (char)9 : (char)10;

            //If the cell is in the player's view
            if(map.IsInFov(X, Y))
            {
                Color = Colors.Chest;
            }
            //Else, color it the same as the floor
            else
            {
                Color = Colors.Floor;
            }

            //Set the cell to the chest's properties
            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
