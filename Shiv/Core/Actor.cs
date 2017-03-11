/* Name: Steven Alford
 * File: Actor.cs
 * Date: 3/9/17
 * Desc: Creates an actor class that draws from the IActor and IDrawable
 *       interfaces to encapsulate a class that can be used by game objects
 *       such as the player/enemies/traps/etc
 */

using RLNET;
using RogueSharp;

namespace Shiv.Core
{
    public class Actor : Interfaces.IActor, Interfaces.IDrawable
    {
        //IActor inheritance
        public string Name { get; set; }
        public int Fov { get; set; }

        //IDrawable inheritance
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        //Defines the draw function from IDrawable
        public void Draw(RLConsole console, IMap map)
        {
            //If the cell is not explored, don't draw it to the screen
            if(!map.GetCell(X, Y).IsExplored)
            { return; }

            //If the cell is in the field of view, draw it to the screen
            //      using field of view colors
            if(map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            //Else, draw it with darker colors
            else
            {
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
            }
        }
    }
}
