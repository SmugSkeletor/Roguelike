using RLNET;
using System.Collections.Generic;

namespace Roguelike.Systems
{
    public class Log
    {
        private static readonly int _maxLines = 9;

        private readonly Queue<string> _lines;

        public Log()
        {
            _lines = new Queue<string>();
        }

        public void Add(string message)
        {
            _lines.Enqueue(message);

            if (_lines.Count > _maxLines)
            {
                _lines.Dequeue();
            }
        }

        public void Draw(RLConsole console)
        {
            console.Clear();
            string[] lines = _lines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                console.Print(1, i + 1, lines[i], RLColor.White);
            }
        }
    }
}