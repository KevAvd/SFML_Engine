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

        public Game(uint w, uint h, string title, params GameState[] states)
        {
            _window = new RenderWindow(new VideoMode(w, h), title);
            _stateHandler = new StateHandler(states);
            InputHandler.GetInstance().Window = _window;
        }

        public void Run()
        {
            //Load games elements
            _stateHandler.ActualState.Load();

            //Handles event
            _window.Closed += WinClose;

            //Game loop
            while (_window.IsOpen)
            {
                //Get the elapsed time since last frame
                _elapsedTime = GameClock.GetInstance().ElapsedFrame();

                //Dispatch windows events
                _window.DispatchEvents();

                //Update keyboard's keys and mouse's buttons states 
                InputHandler.GetInstance().UpdateOld();
                InputHandler.GetInstance().Update();

                //Update the actual game state
                _stateHandler.ActualState.Update(_elapsedTime);

                //Render the actual game state
                _window.Clear();
                _stateHandler.ActualState.Render(_window);
                _window.Display();

                //Update the state handler
                _stateHandler.Update();
            }
        }

        void WinClose(Object obj, EventArgs e)
        {
            (obj as RenderWindow).Close();
        }
    }
}
