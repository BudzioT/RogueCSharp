using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RLNET;


namespace RogueGame.Core
{
    // Game object colors class
    public class Colors
    {
        // Floor colors
        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = Swatch.Alternate[4];
        public static RLColor FloorBgFov = Swatch.Dark;
        public static RLColor FloorFov = Swatch.Alternate[0];

        // Wall colors
        public static RLColor WallBackground = Swatch.Secondary[4];
        public static RLColor Wall = Swatch.Secondary[0];
        public static RLColor WallBgFov = Swatch.Secondary[3];
        public static RLColor WallFov = Swatch.Secondary[1];

        // Text colors
        public static RLColor TextHeader = Swatch.Light;
    }
}
