using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic
{
    public class GameLoop
    {
        private bool _isRunning = false;
        private IInputReader _inputReader;
        private IWalkable _player;

        public GameLoop(IInputReader inputParser) {
            _inputReader = inputParser;
            _player = new Player()
            {
                X = 0,
                Y = 0
            };
        }
        public void Run()
        {
            _isRunning = true;
            while (_isRunning)
            {
                var action = _inputReader.GetNextAction();
                //todo: chain of responsibility
                if (action == GameAction.None)
                {
                    continue;
                }
                else if (action == GameAction.Quit)
                {
                    _isRunning = false;
                }
                else if (action == GameAction.MoveUp || action == GameAction.MoveDown || action == GameAction.MoveLeft || action == GameAction.MoveRight)
                {
                    _player.Move(action);
                }
            }
        }
    }
}
