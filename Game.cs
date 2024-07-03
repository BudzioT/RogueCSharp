using RLNET;

using RogueGame.Core;
using RogueGame.Systems;


namespace RogueGame
{
    
    public class Game
    {
        // Screen width and height in number of tiles
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        // Create RLNET main console
        private static RLRootConsole _mainConsole;

        // Part of the screen with the map
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
        private static RLConsole _mapConsole;

        // Message console for attack rolls and more (below the map)
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 11;
        private static RLConsole _messageConsole;

        // Statistics console (right of the map)
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static RLConsole _statConsole;

        // Inventory console (above the map)
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;
        private static RLConsole _inventoryConsole;

        // Dungeon map
        public static DungeonMap DungeonMap { get; private set; }


        static void Main()
        {
            // Main font file name
            string fontFileName = "font8x8.png";
            // Title of the game
            string consoleTitle = "Rogue Game C#";
            // Initialize the main console using main font with size 8x8, scale 1
            _mainConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
                8, 8, 1f, consoleTitle);

            // Initialize other parts of screen, draw them
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _messageConsole = new RLConsole(_messageWidth, _messageHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);

            // Setup handler for Update events
            _mainConsole.Update += OnRootConsoleUpdate;
            // Set up handler for Render events
            _mainConsole.Update += OnRootConsoleRender;

            // Setup map generetor and create a new map
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight);

            // Begin the game loop
            _mainConsole.Run();
        }


        // Handle Update events
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs args)
        {
            // Set Main console
            _mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.FloorBackground);
            _mainConsole.Print(1, 1, "Map", Colors.TextHeader);

            // Set Message console
            _messageConsole.SetBackColor(0, 0, _messageWidth, _messageHeight, Swatch.DeepWater);
            _messageConsole.Print(1, 1, "Messages", Colors.TextHeader);

            // Set Stats console
            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Swatch.OldStone);
            _statConsole.Print(1, 1, "Stats", Colors.TextHeader);

            // Set inventory console
            _inventoryConsole.SetBackColor(0, 0, _inventoryWidth, _inventoryHeight, Swatch.Wood);
            _inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeader);
        }

        // Handle Render events
        private static void OnRootConsoleRender(object sender, UpdateEventArgs args)
        {
            // Blit the sub consoles
            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _mainConsole, 0, _inventoryHeight);
            RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _mainConsole, _mapWidth, 0);
            RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight, _mainConsole, 0, _screenHeight - _messageHeight);
            RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight, _mainConsole, 0, 0);

            // Draw the root console
            _mainConsole.Draw();
        }
    }
}
