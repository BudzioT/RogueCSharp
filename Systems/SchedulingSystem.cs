using RogueGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueGame.Systems
{
    // System to manage schedules
    public class SchedulingSystem
    {
        // Time
        private int _time;
        // Schedules list, sorted by time
        private readonly SortedDictionary<int, List<IScheduable>> _schedules;

        // Initialize the Scheduling system
        public SchedulingSystem()
        {
            _time = 0;
            _schedules = new SortedDictionary<int, List<IScheduable>>();
        }

        // Add new object to the schedules, assign it a time
        public void Add(IScheduable schedule)
        {
            // Calculate time by adding schedule time to the current one
            int time = _time + schedule.Time;
            // If there isn't any schedules at this time, add it
            if (!_schedules.ContainsKey(time))
            {
                _schedules.Add(time, new List<IScheduable>());
            }
            // Add the schedule to the calculated time
            _schedules[time].Add(schedule);
        }

        // Get current time
        public int GetTime()
        {
            return _time;
        }
    }
}
