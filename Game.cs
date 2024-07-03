using RLNET;

using RogueGame.Core;
using RogueGame.Systems;
using RogueSharp.Random;
using System.Security.Cryptography.X509Certificates;


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

        // New render required flag
        private static bool _renderRequired = true;

        // Interface (as a singleton) to generate random numbers 
        public static IRandom Random { get; private set; }

        // Command system
        public static CommandSystem CommandSystem { get; private set; }

        // Message log
        public static MessageLog MessageLog { get; private set; }

        // Dungeon map
        public static DungeonMap DungeonMap { get; private set; }

        // Player
        public static Player Player { get; set; }

        // Temp test message help variable
        private static int _steps = 0;


        static void Main()
        {
            // Get the seed for random number generator, based off current time
            int seed = (int)DateTime.Now.Ticks;
            // Use the seed to create a random generator engine
            Random = new DotNetRandom(seed);

            // Main font file name
            string fontFileName = "font8x8.png";
            // Title of the game
            string consoleTitle = $"Rogue Game C# (Seed: {seed})";
            // Initialize the main console using main font with size 8x8, scale 1
            _mainConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
                8, 8, 1f, consoleTitle);

            // Initialize other parts of screen, draw them
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _messageConsole = new RLConsole(_messageWidth, _messageHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);

            // Set Message log
            MessageLog = new MessageLog();
            MessageLog.Add("First challange appears");
            MessageLog.Add($"Level seed: '{seed}'");

            // Set Stats console
            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Swatch.OldStone);
            _statConsole.Print(1, 1, "Stats", Colors.TextHeader);

            // Set inventory console
            _inventoryConsole.SetBackColor(0, 0, _inventoryWidth, _inventoryHeight, Swatch.Wood);
            _inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeader);

            // Setup handler for Update events
            _mainConsole.Update += OnRootConsoleUpdate;
            // Set up handler for Render events
            _mainConsole.Update += OnRootConsoleRender;

            // Instantiate command system
            CommandSystem = new CommandSystem();

            // Setup map generetor and create a new map with 20 max rooms with sizes from 7 to 13
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight, 20, 13, 7);
            DungeonMap = mapGenerator.CreateMap();
            // Update player's FOV
            DungeonMap.UpdatePlayerFOV();

            // Begin the game loop
            _mainConsole.Run();
        }


        // Handle Update events
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs args)
        {
            // Player act flag
            bool playerAct = false;
            // Get which key is pressed
            RLKeyPress keyPress = _mainConsole.Keyboard.GetKeyPress();

            // If a key is pressed
            if (keyPress != null)
            {
                // On escape, quit
                if (keyPress.Key == RLKey.Escape)
                    _mainConsole.Close();
                // Move up
                else if (keyPress.Key == RLKey.Up || keyPress.Key == RLKey.W)
                    playerAct = CommandSystem.MovePlayer(Direction.Up);
                // Move down
                else if (keyPress.Key == RLKey.Down || keyPress.Key == RLKey.S)
                    playerAct = CommandSystem.MovePlayer(Direction.Down);
                // Move left
                else if (keyPress.Key == RLKey.Left || keyPress.Key == RLKey.A)
                    playerAct = CommandSystem.MovePlayer(Direction.Left);
                // Move right
                else if (keyPress.Key == RLKey.Right || keyPress.Key == RLKey.D)
                    playerAct = CommandSystem.MovePlayer(Direction.Right);
            }

            // If player acted, rendering is required
            if (playerAct)
            {
                MessageLog.Add($"Step #{++_steps}");
                _renderRequired = true;
            }
                
        }

        // Handle Render events
        private static void OnRootConsoleRender(object sender, UpdateEventArgs args)
        {
            // If rendering is not required, continue the game loop
            if (!_renderRequired)
                return;

            // Blit the sub consoles
            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _mainConsole, 0, _inventoryHeight);
            RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _mainConsole, _mapWidth, 0);
            RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight, _mainConsole, 0, _screenHeight - _messageHeight);
            RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight, _mainConsole, 0, 0);

            // Draw the dungeon map
            DungeonMap.Draw(_mapConsole);

            // Draw the player
            Player.Draw(_mapConsole, DungeonMap);

            // Draw the message log
            MessageLog.Draw(_messageConsole);

            // Draw the root console
            _mainConsole.Draw();

            // Set rendering required to false, since everything needed is rendered
            _renderRequired = false;
        }
    }
}
