using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Shiv.Core
{
    public class Colors
    {
        //Instantiates colors for certain objects in the game window
        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = Palette.ComplementDarker;
        public static RLColor FloorBackgroundFov = Palette.Complement;
        public static RLColor FloorFov = Palette.ComplementLighter;

        public static RLColor WallBackground = Palette.Primary;
        public static RLColor Wall = RLColor.Gray;
        public static RLColor WallBackgroundFov = Palette.PrimaryDarker;
        public static RLColor WallFov = RLColor.LightGray;

        public static RLColor TextHeading = RLColor.White;
    }
}
