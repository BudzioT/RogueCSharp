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

        // Door colors
        public static RLColor DoorBackground = Swatch.Complement[4];
        public static RLColor Door = Swatch.Complement[1];
        public static RLColor DoorBgFov = Swatch.Complement[3];
        public static RLColor DoorFov = Swatch.Complement[0];
            
        // Text colors
        public static RLColor TextHeader = RLColor.White;
        public static RLColor Text = Swatch.Light;
        public static RLColor Gold = Swatch.Sun;

        // Player color
        public static RLColor Player = Swatch.Light;

        // Enemies colors
        public static RLColor KEnemy = Swatch.Metal;
    }
}
