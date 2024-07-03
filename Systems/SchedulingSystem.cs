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

        // Remove a schedule from list
        public void Remove(IScheduable schedule)
        {
            KeyValuePair<int, List<IScheduable>> scheduableList = new KeyValuePair<int, List<IScheduable>>(-1, null);

            // Go through each schedule
            foreach (var scheduleList in _schedules)
            {
                // If it contains the argument, save it
                if (scheduleList.Value.Contains(schedule))
                {
                    scheduableList = scheduleList;
                    break;
                }
            }

            // If there was argument found before, remove it
            if (scheduableList.Value != null)
            {
                scheduableList.Value.Remove(schedule);
                // If there are no more schedules at this time, remove the entry
                if (scheduableList.Value.Count <= 0)
                    _schedules.Remove(scheduableList.Key);
            }
        }

        // Get the next object on a schedule, advance time if needed
        public IScheduable Get()
        {
            // Get the first object
            var firstScheduleGroup = _schedules.First();
            var firstSchedule = firstScheduleGroup.Value.First();

            // Remove it from schedules and increase time
            Remove(firstSchedule);
            _time = firstScheduleGroup.Key;
            return firstSchedule;
        }

        // Reset the time, clear any schedules
        public void Clear()
        {
            _time = 0;
            _schedules.Clear();
        }

        // Get current time
        public int GetTime()
        {
            return _time;
        }
    }
}
