using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace SFML_Engine
{
    internal class Game
    {
        StateHandler _stateHandler;
        float _elapsedTime;
        RenderWindow _window;

        public Game(uint w, uint h, string title)
        {
            _window = new RenderWindow(new VideoMode(w, h), title);
        }

        public void Run()
        {
            load();
            while (_window.IsOpen)
            {
                _elapsedTime = GameClock.GetInstance().ElapsedFrame();
                _stateHandler.PlayState(_window, _elapsedTime);
            }
        }

        public void load()
        {

        }
    }
}
