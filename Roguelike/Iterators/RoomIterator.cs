using RogueSharp;
using System.Collections.Generic;

namespace Roguelike.Iterators
{
    public class RoomIterator : Iterator
    {
        public List<Rectangle> _rooms;

        private int _current = 0;
        // Constructor

        public RoomIterator(List<Rectangle> rooms)
        {
            this._rooms = rooms;
        }

        // Gets first iteration item

        public Rectangle First()
        {
            _current = 0;
            return _rooms[_current] as Rectangle;
        }

        // Gets next iteration item

        public Rectangle Next()
        {
            _current += 1;
            if (!IsDone)
                return _rooms[_current] as Rectangle;
            else

                return null;
        }

        // Gets current iteration item

        public Rectangle CurrentItem
        {
            get { return _rooms[_current] as Rectangle; }
        }

        // Gets whether iterations are complete

        public bool IsDone
        {
            get { return _current >= _rooms.Count; }
        }
    }
}