using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        // Monsters that are generated at the dungeon map
        public readonly List<Monster> _monsters;

        // Doors
        public List<Door> Doors { get; set; }

        // Initialize the dungeon map
        public DungeonMap()
        {
            // Initialize list of rooms
            Rooms = new List<Rectangle>();
            // Initialize list of monsters
            _monsters = new List<Monster>();
            // Initialize list of doors
            Doors = new List<Door>();
        }

        // Draw all the symbols and colors to the map console
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            // Set each symbol based on the cell state
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolCell(mapConsole, cell);
            }

            // Draw every monster that exists on the map
            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);
            }

            // Draw each doors that exists
            foreach (Door door in Doors)
            {
                door.Draw(mapConsole, this);
            }

            // Index to help with placement of healthbars
            int i = 0;
            // Go through every monster on the map and draw it
            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);

                // If monster is in player's FOV, draw its healthbar
                if (IsInFov(monster.X, monster.Y))
                    monster.DrawStats(statConsole, i++);
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

                // Try to open doors if there are any
                OpenDoor(actor, x, y);

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

        // Add player to the game
        public void AddPlayer(Player player)
        {
            Game.Player = player;
            // Set the cell that player is on as occupied
            SetIsWalkable(player.X, player.Y, false);
            // Update player's FOV
            UpdatePlayerFOV();

            // Add the player to schedule
            Game.SchedulingSystem.Add(player);
        }

        // Add new monster to the map
        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster);
            // Set the tile that monster occupies to not walkable
            SetIsWalkable(monster.X, monster.Y, false);

            // Add the monster to schedule
            Game.SchedulingSystem.Add(monster);
        }

        // Get a random walkable location in a room
        public Point? GetRandomWalkableLocation(Rectangle room)
        {
            // If there is any walkable cell, search for a random one
            if (IsRoomWalkable(room))
            {
                // Search for a location 100 times
                for (int i = 0; i < 100; i++)
                {
                    // Get random location in the room
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    // If there is a walkable space, return the location
                    if (IsWalkable(x, y))
                        return new Point(x, y);
                }
            }
            // There is no walkable space, return null
            return null;
        }

        // Return if the room is walkable
        public bool IsRoomWalkable(Rectangle room)
        {
            // Go through every row in a room, not counting the walls (starting at 1 up to width - 2, because of the left and right wall)
            for (int x = 1; x <= room.Width - 2; x++)
            {
                // Go through each cell in a row, not counting the walls
                for (int y = 1; y <= room.Height - 2; y++)
                {
                    // If the cell is walkable, return true
                    if (IsWalkable(x + room.X, y + room.Y))
                        return true;
                }
            }
            // No cells in the room are walkable, return false
            return false;
        }

        // Remove the monster
        public void RemoveMonster(Monster monster)
        {
            _monsters.Remove(monster);
            // Set cell back to walkable
            SetIsWalkable(monster.X, monster.Y, true);

            // Delete monster from the schedule
            Game.SchedulingSystem.Remove(monster);
        }

        // Return monster at given postion or null if there isn't any
        public Monster GetMonsterAt(int x, int y)
        {
            return _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }


        private void OpenDoor(Actor actor, int x, int y)
        {
            // Try to get doors at given position, if there are one, open them
            Door door = GetDoor(x, y);
            if (door != null && !door.Open) 
            {
                // Open the doors
                door.Open = true;
                var cell = GetCell(x, y);
                // Once doors are opened, make them transparent and not block FOV anymore
                SetCellProperties(x, y, true, cell.IsWalkable, cell.IsExplored);

                Game.MessageLog.Add($"{actor.Name} kicked through the doors!");
            }
        }

        // Return doors in located position, or null if there's none
        public Door GetDoor(int x, int y)
        {
            return Doors.SingleOrDefault(door => door.X == x || door.Y == y);
        }
    }
}
