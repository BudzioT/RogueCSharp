using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueGame.Core;
using RogueGame.Monsters;
using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharp.MapCreation;


namespace RogueGame.Systems
{
    public class MapGenerator
    {
        // Width and height of the map
        private readonly int _width;
        private readonly int _height;
        // Maximum and minimum size of a room
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;
        // Maximum number of rooms
        private readonly int _maxRooms;

        // The map itself
        private readonly DungeonMap _map;

        // Initialize dimensions and create a new map
        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize)
        {
            // Set dimensions of the map
            _width = width;
            _height = height;
            // Set properties of rooms
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;

            // Create the map
            _map = new DungeonMap();
        }

        // Generate a new map
        public DungeonMap CreateMap()
        {
            // Initialize the map
            _map.Initialize(_width, _height);

            // Place as much rooms as allowed
            for (int roomNum = _maxRooms; roomNum > 0; roomNum--)
            {
                // Calculate dimensions and position of the room randomly
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomPosX = Game.Random.Next(0, _width - roomWidth - 1);
                int roomPosY = Game.Random.Next(0, _height - roomHeight - 1);
                
                // Create new room as a rectangle
                var newRoom = new Rectangle(roomPosX, roomPosY, roomWidth, roomHeight);

                // Check collisions with other rooms
                bool newRoomCollisions = _map.Rooms.Any(room => newRoom.Intersects(room));

                // If there are no collisions, add new room to the list
                if (!newRoomCollisions)
                    _map.Rooms.Add(newRoom);
            }

            // Create all rooms from the newly created list
            foreach (Rectangle room in _map.Rooms)
                CreateRoom(room);

            // Go through all the rooms generated starting from the second one
            for (int roomNum = 1; roomNum < _map.Rooms.Count; roomNum++)
            {
                // Previous room center coordinates
                int prevRoomCenterX = _map.Rooms[roomNum - 1].Center.X;
                int prevRoomCenterY = _map.Rooms[roomNum - 1].Center.Y;

                // Current room center coordinates
                int currRoomCenterX = _map.Rooms[roomNum].Center.X;
                int currRoomCenterY = _map.Rooms[roomNum].Center.Y;

                // Generate a tunnel connecting previous and current room in shape of 'L', at random rotation
                if (Game.Random.Next(1, 2) == 1)
                {
                    // Focus on the horizontal line first
                    CreateHorizontalTunnel(prevRoomCenterX, currRoomCenterX, prevRoomCenterY);
                    CreateVericalTunnel(prevRoomCenterY, currRoomCenterY, currRoomCenterX);
                }
                else
                {
                    // Focus on the vertical line first
                    CreateVericalTunnel(prevRoomCenterY, currRoomCenterY, prevRoomCenterX);
                    CreateHorizontalTunnel(prevRoomCenterX, currRoomCenterX, currRoomCenterY);
                }
            }

            // Place the player in first of the newly created rooms
            PlacePlayer();

            // Place the monsters
            PlaceMonsters();

            // Return the created map
            return _map;
        }

        // Create a room from rectangle, by setting all cells properties to true
        private void CreateRoom(Rectangle room)
        {
            // Go through each row
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                // Go through each cell in a row and set its properties
                for (int y = room.Top + 1; y < room.Bottom; y++)
                    _map.SetCellProperties(x, y, true, true, true);
            }
        }

        // Place the player on the map
        private void PlacePlayer()
        {
            // Get the reference to the player
            Player player = Game.Player;
            // If he doesn't exist, create him
            if (player == null)
                player = new Player();

            // Place him in the center of the first room
            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;

            // Add the player to the game
            _map.AddPlayer(player);
        }

        // Create a tunnel parallel to x-axis
        private void CreateHorizontalTunnel(int startX, int endX, int posY)
        {
            // Go from the most left cell to the right one and create a tunnel
            for (int x = Math.Min(startX, endX); x <= Math.Max(startX, endX); x++)
                _map.SetCellProperties(x, posY, true, true);
        }

        // Create a tunnel parallel to the y-axis
        private void CreateVericalTunnel(int startY, int endY, int posX)
        {
            // Go from the top cell to the bottom one and create a tunnel
            for (int y = Math.Min(startY, endY); y <= Math.Max(startY, endY); y++)
                _map.SetCellProperties(posX, y, true, true);
        }

        // Place random amount of monsters in every room at random cells
        private void PlaceMonsters()
        {
            // Go through each room on the map
            foreach (var room in _map.Rooms)
            {
                // Roll the dice, there is 60% to get monsters into the room
                if (Dice.Roll("1D10") < 7)
                {
                    // Generate between 1 to 4 monsters
                    var numberOfMonsters = Dice.Roll("1D4");
                    // Place every monster at random location
                    for (int i = 0; i < numberOfMonsters; i++)
                    {
                        // Get random walkable location to place the enemy at
                        Point? randomRoomLocation = _map.GetRandomWalkableLocation(room);
                        // If getting random location was succesful, add the enemy to this location
                        if (randomRoomLocation.HasValue)
                        {
                            var monster = KEnemy.Create(1);
                            monster.X = randomRoomLocation.Value.X;
                            monster.Y = randomRoomLocation.Value.Y;
                            _map.AddMonster(monster);
                        }
                    }
                }
            }
        }
    }
}
