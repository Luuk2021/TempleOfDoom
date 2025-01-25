using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.UI.Views
{
    public class HealthView : IObserver<int>
    {
        private (int x, int y) _offset;
        private int _lives;
        public HealthView(IDamageable healthObservable, (int x, int y) offset)
        {
            _offset = offset;
            _lives = healthObservable.Health;
            healthObservable.Subscribe(this);
        }
        public int Display()
        {
            Console.SetCursorPosition(_offset.x, _offset.y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Lives: {_lives}");
            return 1;
        }
        public void OnNext(int value)
        {
            _lives = value;
            Display();
        }
        public void OnCompleted()
        {
            // We do nothing here
        }

        public void OnError(Exception error)
        {
            // We do nothing here
        }
    }
}
