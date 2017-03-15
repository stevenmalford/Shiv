/* Name: Steven Alford
 * File: Colors.cs
 * Date: 3/5/17
 * Desc: A class containing the set of colors for common objects on the game screen
 */

using RLNET;

namespace Shiv.Core
{
    public class Colors
    {
        //Instantiates colors for certain objects in the game window
        //Floor
        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = Palette.Opal;
        public static RLColor FloorBackgroundFov = Palette.Opal;
        public static RLColor FloorFov = Palette.Topaz;

        //Wall
        public static RLColor WallBackground = Palette.Primary;
        public static RLColor Wall = RLColor.Gray;
        public static RLColor WallBackgroundFov = Palette.PrimaryDarker;
        public static RLColor WallFov = RLColor.LightGray;

        //Text
        public static RLColor TextHeading = RLColor.White;

        //Player object
        public static RLColor Player = RLColor.White;
    }
}
