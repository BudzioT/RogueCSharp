using RLNET;
using RogueGame.Interfaces;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueGame.Core
{
    public class Stairs : IDrawable
    {
        // IDrawable variables
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public RLColor Color { get; set; }
        
        // Flag for stairs going up
        public bool Up { get; set; }

        // Draw the stairs
        public void Draw(RLConsole console, IMap map)
        {
            // If cell is already explored, don't draw them
            if (!map.GetCell(X, Y).IsExplored)
                return;

            Symbol = Up ? '<' : '>';

            // Set color to player's, if stairs are in FOV
            if (map.IsInFov(X, Y))
                Color = Colors.Player;
            // Otherwise, set them to floor color
            else
                Color = Colors.Floor;

            // Draw them
            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
