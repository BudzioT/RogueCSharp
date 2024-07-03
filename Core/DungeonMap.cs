using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;


namespace RogueGame.Core
{
    // Dungeon map, extended version of basic RogueSharp map
    public class DungeonMap : Map
    {
        // Draw all the symbols and colors to the map console
        public void Draw(RLConsole mapConsole)
        {
            // Clear the map
            mapConsole.Clear();

            // Set each symbol based on the cell state
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolCell(mapConsole, cell);
            }
        }

        // Set symbols for cells
        private void SetConsoleSymbolCell(RLConsole mapConsole, Cell cell)
        {
            // If the cell isn't explored, don't draw it
            if (!cell.IsExplored)
            {
                return;
            }

            // When cell is in FOV, draw it in lighter colors
            if (IsInFov(cell.X, cell.Y))
            {
                // If cell is walkable, set it's symbol to '.'
                if (cell.IsWalkable)
                    mapConsole.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBgFov, '.');
                // Otherwise set it to '#'
                else
                    mapConsole.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBgFov, '#');
            }

            // When cell is outside of FOE, draw it darker
            else
            {
                if (cell.IsWalkable)
                    mapConsole.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                else
                    mapConsole.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
            }
        }
    }
}
