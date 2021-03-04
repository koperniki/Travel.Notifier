using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Travel.Notifier.Models;

namespace Travel.Notifier.Services
{
    public class GameStateService
    {
        private ConcurrentDictionary<int, Button> _buttons;

        public GameStateService()
        {
            _buttons = new ConcurrentDictionary<int, Button>();
        }

        public IEnumerable<Button> GetButtons()
        {
            return _buttons.Values;
        }

        public void UpdateState(int number, bool state)
        {
            if (_buttons.Values.Any(t => t.Time < DateTime.UtcNow))
            {
                return;
            }
            _buttons[number] = new Button
            {
                Number = number,
                State = state,
                Time = DateTime.UtcNow,
            };
        }

        public void ClearState()
        {
            foreach (var buttonsValue in _buttons.Values)
            {
                buttonsValue.Time = DateTime.MaxValue;
                buttonsValue.State = false;
            }
        }

    }
}