using RLNET;

namespace RogueGame
{
    public class Game
    {
        // Screen width and height in number of tiles
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;

        // Create RLNET console
        private static RLRootConsole _rootConsole;

        static void Main()
        {
            // Main font file name
            string fontFileName = "font8x8.png";
            // Title of the game
            string consoleTitle = "Rogue Game C#";
            // Initialize the main console using main font with size 8x8, scale 1
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
                8, 8, 1f, consoleTitle);

            // Set up handler for Update events
            _rootConsole.Update += OnRootConsoleUpdate;
            // Set up handler for Render events
            _rootConsole.Update += OnRootConsoleRender;

            // Begin the game loop
            _rootConsole.Run();
        }


        // Handle Update events
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs args)
        {
            _rootConsole.Print(10, 10, "Worked", RLColor.White);
        }

        // Handle Render events
        private static void OnRootConsoleRender(object sender, UpdateEventArgs args)
        {
            // Draw the root console
            _rootConsole.Draw();
        }
    }
}
