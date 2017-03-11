/* Name: Steven Alford
 * File: Palette.cs
 * Date: 3/5/17
 * Desc: A class containing all of the colors that are available to be used
 *       in the game. Contains two separate palettes, one that is complimentary
 *       and the other that contains multiple other colors with no relation
 */

using RLNET;

namespace Shiv.Core
{
    public class Palette
    {
        //Complimentary/Dark Palette Source: 
        //      http://paletton.com/#uid=6410C0k3V4WaYgf7Lb1ac80gJaQ

        //Primary Color
        public static RLColor PrimaryLightest = new RLColor(25, 25, 27);
        public static RLColor PrimaryLighter  = new RLColor(65, 64, 90);
        public static RLColor Primary         = new RLColor(49, 49, 61);
        public static RLColor PrimaryDarker   = new RLColor(33, 32, 44);
        public static RLColor PrimaryDarkest  = new RLColor(34, 33, 60);

        //Secondary Color (1)
        public static RLColor SecondaryLightest = new RLColor(22, 24, 25);
        public static RLColor SecondaryLighter  = new RLColor(55, 74, 81);
        public static RLColor Secondary         = new RLColor(42, 52, 55);
        public static RLColor SecondaryDarker   = new RLColor(28, 37, 40);
        public static RLColor SecondaryDarkest  = new RLColor(27, 47, 54);

        //Secondary Color (2)
        public static RLColor AlternateLightest = new RLColor(25, 23, 26);
        public static RLColor AlternateLighter  = new RLColor(75, 59, 87);
        public static RLColor Alternate         = new RLColor(53, 46, 59);
        public static RLColor AlternateDarker   = new RLColor(37, 30, 43);
        public static RLColor AlternateDarkest  = new RLColor(46, 30, 58);

        //Complement Color
        public static RLColor ComplementLightest = new RLColor(39, 38, 34);
        public static RLColor ComplementLighter  = new RLColor(129, 119, 85);
        public static RLColor Complement         = new RLColor(88, 83, 67);
        public static RLColor ComplementDarker   = new RLColor(64, 59, 43);
        public static RLColor ComplementDarkest  = new RLColor(86, 76, 41);



        //No Relation/Light Palette Source: 
        //      http://pixeljoint.com/forum/forum_posts.asp?TID=16247
        //      http://hem.bredband.net/ricfha/pic/db32.gpl
        
        public static RLColor Valhalla    = new RLColor(34, 32, 52);
        public static RLColor Loulou      = new RLColor(69, 40, 60);
        public static RLColor OiledCedar  = new RLColor(102, 57, 49);
        public static RLColor Rope        = new RLColor(143, 86, 59);
        public static RLColor TahitiGold  = new RLColor(223, 113, 38);
        public static RLColor Twine       = new RLColor(217, 160, 102);
        public static RLColor Pancho      = new RLColor(238, 195, 154);
        public static RLColor GoldenFizz  = new RLColor(251, 242, 54);
        public static RLColor Atlantis    = new RLColor(153, 229, 80);
        public static RLColor Christi     = new RLColor(106, 190, 48);
        public static RLColor ElfGreen    = new RLColor(55, 148, 110);
        public static RLColor Dell        = new RLColor(75, 105, 47);
        public static RLColor Verdigras   = new RLColor(82, 75, 36);
        public static RLColor Opal        = new RLColor(50, 60, 57);
        public static RLColor DeepKoamaru = new RLColor(63, 63, 116);
        public static RLColor VeniceBlue  = new RLColor(48, 96, 130);
        public static RLColor RoyalBlue   = new RLColor(91, 110, 225);
        public static RLColor Cornflower  = new RLColor(99, 155, 255);
        public static RLColor Viking      = new RLColor(95, 205, 228);
        public static RLColor LightSteel  = new RLColor(203, 219, 252);
        public static RLColor Heather     = new RLColor(155, 173, 183);
        public static RLColor Topaz       = new RLColor(132, 126, 135);
        public static RLColor DimGray     = new RLColor(105, 106, 106);
        public static RLColor Ash         = new RLColor(89, 86, 82);
        public static RLColor Clairvoyant = new RLColor(118, 66, 138);
        public static RLColor Brown       = new RLColor(172, 50, 50);
        public static RLColor Mandy       = new RLColor(217, 87, 99);
        public static RLColor Plum        = new RLColor(215, 123, 186);
        public static RLColor RainForest  = new RLColor(143, 151, 74);
        public static RLColor Stinger     = new RLColor(138, 111, 48);
    }
}
