/* Name: Steven Alford
 * File: IActor.cs (interface)
 * Date: 3/9/17
 * Desc: Creates a new interface for objects that are acting
 *       in the game world (map). Player/Enemies/Traps/etc
 */

namespace Shiv.Interfaces
{
    public interface IActor
    {
        //Gets and sets the name of the actor
        string Name { get; set; }
        //Gets and sets the awareness/field of view of the actor
        int Fov { get; set; }
    }
}
