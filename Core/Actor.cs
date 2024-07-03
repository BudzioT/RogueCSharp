using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueGame.Interfaces;
using RogueSharp;


namespace RogueGame.Core
{
    public class Actor : IActor, IDrawable
    {
        // IActor variables
        public string Name { get; set; }
        public int Awareness { get; set; }

        // IDrawable variables
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        // Draw the Actor
        public void Draw(RLConsole console, IMap map)
        {
            // Don't draw it, if cell isn't yet explored
            if (!map.GetCell(X, Y).IsExplored)
                return;

            // Draw the actor when in FOV
            if (map.IsInFov(X, Y))
                console.Set(X, Y, Color, Colors.FloorBgFov, Symbol);
            // When not in FOV, draw normal floor
            else
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
        }
    }
}
