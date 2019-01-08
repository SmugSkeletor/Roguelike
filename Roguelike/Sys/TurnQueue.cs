using Roguelike.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Systems
{
    public class TurnQueue
    {
        private int _time;
        private readonly SortedDictionary<int, List<ITurnQueue>> _queueContent;

        public TurnQueue()
        {
            _time = 0;
            _queueContent = new SortedDictionary<int, List<ITurnQueue>>();
        }

        public void Add(ITurnQueue queueContent)
        {
            int key = _time + queueContent.Time;
            if (!_queueContent.ContainsKey(key))
            {
                _queueContent.Add(key, new List<ITurnQueue>());
            }
            _queueContent[key].Add(queueContent);
        }

        public void Remove(ITurnQueue scheduleable)
        {
            KeyValuePair<int, List<ITurnQueue>> scheduleableListFound
              = new KeyValuePair<int, List<ITurnQueue>>(-1, null);

            foreach (var scheduleablesList in _queueContent)
            {
                if (scheduleablesList.Value.Contains(scheduleable))
                {
                    scheduleableListFound = scheduleablesList;
                    break;
                }
            }
            if (scheduleableListFound.Value != null)
            {
                scheduleableListFound.Value.Remove(scheduleable);
                if (scheduleableListFound.Value.Count <= 0)
                {
                    _queueContent.Remove(scheduleableListFound.Key);
                }
            }
        }

        public ITurnQueue Get()
        {
            var firstScheduleableGroup = _queueContent.First();
            var firstScheduleable = firstScheduleableGroup.Value.First();
            Remove(firstScheduleable);
            _time = firstScheduleableGroup.Key;
            return firstScheduleable;
        }

        public int GetTime()
        {
            return _time;
        }

        public void Clear()
        {
            _time = 0;
            _queueContent.Clear();
        }
    }
}