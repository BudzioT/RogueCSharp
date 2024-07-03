using RLNET;
using RogueGame.Interfaces;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RogueGame.Core
{
    // Doors, that can be opened, block FOV
    public class Door : IDrawable
    {
        // Open flag
        public bool Open { get; set; }

        // IDrawable variables
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }

        // Colors
        public RLColor Color { get; set; }
        public RLColor BgColor { get; set; }

        // Initialize Door
        public Door() 
        {
            Symbol = '+';
            Color = Colors.Door;
            BgColor = Colors.DoorBackground;
        }

        // Draw doors as opened or closed
        public void Draw(RLConsole console, IMap map)
        {
            // If doors weren't seen, don't draw them
            if (!map.GetCell(X, Y).IsExplored)
                return;
            // Set the symbol depending on whether doors are open
            Symbol = Open ? '/' : '|';

            // If doors are in player's FOV, set them in FOV colors
            if (map.IsInFov(X, Y))
            {
                Color = Colors.DoorFov;
                BgColor = Colors.DoorBgFov;
            }
            // Otherwise set them normally
            else
            {
                Color = Colors.Door;
                BgColor = Colors.DoorBackground;
            }

            // Draw the doors
            console.Set(X, Y, Color, BgColor, Symbol);
        }
    }
}
