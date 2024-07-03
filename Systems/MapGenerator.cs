using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueGame.Core;
using RogueSharp;
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
            }

            // Return the created map
            return _map;
        }
    }
}
