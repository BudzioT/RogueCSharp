using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueGame.Core;
using RogueSharp;


namespace RogueGame.Systems
{
    public class MapGenerator
    {
        // Width and height of the map
        private readonly int _width;
        private readonly int _height;

        // The map itself
        private readonly DungeonMap _map;

        // Initialize dimensions and create a new map
        public MapGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new DungeonMap();
        }

        // Generate a new map
        public DungeonMap CreateMap()
        {
            // Initialize the map
            _map.Initialize(_width, _height);
            // Set all cell properties to true
            foreach (Cell cell in _map.GetAllCells())
            {
                _map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }

            // Set the first and last row in the map to represnt a wall
            foreach (Cell cell in _map.GetCellsInRows(0, _height - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            // Set the first and last column to represnt a wall
            foreach (Cell cell in _map.GetCellsInColumns(0, _width - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            return _map;
        }
    }
}
