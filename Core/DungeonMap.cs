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
        // Rooms of dungeon as rectangles
        public List<Rectangle> Rooms;

        // Initialize the dungeon map
        public DungeonMap()
        {
            // Initialize list of rooms
            Rooms = new List<Rectangle>();
        }

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

        // Update player's FOV when moving him
        public void UpdatePlayerFOV()
        {
            // Reference to game's player
            Player player = Game.Player;

            // Calculate the FOV based off location and awareness
            ComputeFov(player.X, player.Y, player.Awareness, true);

            // Re-mark cells as explored, when they are in FOV
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
            }
        }

        // Place the actor on the cell, return if the action was succesful
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            // Place the actor only when cell is walkable
            if (GetCell(x, y).IsWalkable)
            {
                // Set the tile, that actor was on, as walkable
                SetIsWalkable(actor.X, actor.Y, true);
                // Update actor's position
                actor.X = x;
                actor.Y = y;

                // Set the actor's cell as not walkable (since he is on it)
                SetIsWalkable(x, y, false);

                // Update the FOV, if actor is a player
                if (actor is Player)
                    UpdatePlayerFOV();

                return true;
            }

            return false;
        }

        // Set the IsWalkable property on a cell
        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            // Get the cell by position
            Cell cell = (Cell)GetCell(x, y);
            // Set its properties
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }
    }
}
