/* Name: Steven Alford
 * File: IDrawable.cs (interface)
 * Date: 3/9/17
 * Desc: Creates a new interface for objects that are drawable
 *       to the game screen
 */

using RLNET;
using RogueSharp;

namespace Shiv.Interfaces
{
    public interface IDrawable
    {
        //Gets and sets the color of the object
        RLColor Color { get; set; }
        //Gets and set the symbol of the object
        char Symbol { get; set; }
        //Gets and sets the x coordinate of the object
        int X { get; set; }
        //Gets and sets the y coordinate of the object
        int Y { get; set; }

        //Draws the object to the map
        void Draw(RLConsole console, IMap map);
    }
}
