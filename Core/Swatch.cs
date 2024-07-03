using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogueGame.Core
{
    // General color variables class
    public class Swatch
    {
        // Primary colors
        public static RLColor[] Primary = [ 
            new RLColor(204, 206, 209),
            new RLColor(118, 122, 131),
            new RLColor(73, 77, 87),
            new RLColor(28, 31, 38),
            new RLColor(13, 20, 37)
        ];

        // Secondary colors
        public static RLColor[] Secondary = [
            new RLColor(207, 204, 208),
            new RLColor(126, 117, 131),
            new RLColor(81, 72, 86),
            new RLColor(34, 27, 38),
            new RLColor(28, 11, 37)
        ];

        // Alternate colors
        public static RLColor[] Alternate = [
            new RLColor(255, 255, 249),
            new RLColor(195, 195, 174),
            new RLColor(129, 129, 107),
            new RLColor(57, 57, 40),
            new RLColor(56, 56, 15)
        ];

        // Complement
        public static RLColor[] Complement = [
            new RLColor(255, 253, 249),
            new RLColor(195, 188, 174),
            new RLColor(129, 122, 107),
            new RLColor(57, 51, 40),
            new RLColor(56, 42, 15)
        ];

        // General colors
        public static RLColor Dark = new RLColor(20, 12, 28);
        public static RLColor OldBlood = new RLColor(68, 36, 52);
        public static RLColor DeepWater = new RLColor(48, 52, 109);
        public static RLColor OldStone = new RLColor(78, 74, 78);
        public static RLColor Wood = new RLColor(133, 76, 48);
        public static RLColor Vegetation = new RLColor(52, 101, 36);
        public static RLColor Blood = new RLColor(208, 70, 72);
        public static RLColor Stone = new RLColor(117, 113, 97);
        public static RLColor Water = new RLColor(89, 125, 206);
        public static RLColor BrightWood = new RLColor(210, 125, 44);
        public static RLColor Metal = new RLColor(133, 149, 161);
        public static RLColor Grass = new RLColor(109, 170, 44);
        public static RLColor Skin = new RLColor(210, 170, 153);
        public static RLColor Sky = new RLColor(109, 194, 202);
        public static RLColor Sun = new RLColor(218, 212, 94);
        public static RLColor Light = new RLColor(222, 238, 214);
    }
}
