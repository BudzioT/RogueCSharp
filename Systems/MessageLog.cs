using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueGame.Systems
{
    // Group of messages
    public class MessageLog
    {
        // Maxiumum number of stored lines
        private static readonly int _maxLines = 7;
        // Messages, stored in queue to easily remove the oldest ones when it is full
        private readonly Queue<string> _lines;

        // Initialize the message log
        public MessageLog()
        {
            _lines = new Queue<string>();
        }

        // Add the given message to the log
        public void Add(string message)
        {
            _lines.Enqueue(message);

            // If limit is exceeded, remove the oldest message
            if (_lines.Count > _maxLines)
                _lines.Dequeue();
        }

        // Draw all messages to the console
        public void Draw(RLConsole console)
        {
            string[] lines = _lines.ToArray();
            int margin = 1;
            // Print all the messages
            for (int i = 0; i < lines.Length; i++, margin += 1)
                console.Print(1, i + 1 + margin, lines[i], RLColor.White);
        }
    }
}
