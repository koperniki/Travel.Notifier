using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Travel.Notifier.Models;

namespace Travel.Notifier.Services
{
    public class ButtonStateService
    {
        private ConcurrentDictionary<int, Button> _buttons;

        public ButtonStateService()
        {
            _buttons = new ConcurrentDictionary<int, Button>();
        }

        public IEnumerable<Button> GetButtons()
        {
            return _buttons.Values;
        }

        public void UpdateState(int number, bool state)
        {
            _buttons[number] = new Button
            {
                Number = number,
                State = state,
                Time = DateTime.UtcNow,
                Location = Locations.GetLocation(number)
            };
        }

    }
}