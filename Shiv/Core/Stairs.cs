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
        //Color
        public RLColor Color
        { get; set; }
        //Symbol on map
        public char Symbol
        { get; set; }
        //X coordinate
        public int X
        { get; set; }
        //Y coordinate
        public int Y
        { get; set; }
        //Is the staircase going up?
        public bool IsUp
        { get; set; }

        //Draw the stairs
        public void Draw(RLConsole console, IMap map)
        {
            //If the stairs have not been explored
            if(!map.GetCell(X,Y).IsExplored)
            {
                return;
            }
            
            //If the stairs are up, render the first character
            //If the stairs are down, render the second
            Symbol = IsUp ? (char) 24 : (char) 25;

            //If the stairs are in the player's FOV
            if(map.IsInFov(X,Y))
            {
                Color = Colors.Player;
            }
            //Else, color it to the color of the floor
            else
            {
                Color = Colors.Floor;
            }

            //Sets the cell to the specifications of the stairs
            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
