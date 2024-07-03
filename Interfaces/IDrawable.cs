using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueGame.Interfaces
{
    // Interface representing drawable object
    public interface IDrawable
    {
        // Color of the object
        RLColor Color { get; set; }
        // Symbol drawn
        char Symbol { get; set; }
        // Placement coordinates
        int X { get; set; }
        int Y { get; set; }

        // Draw the object
        void Draw(RLConsole console, IMap map);
    }
}
