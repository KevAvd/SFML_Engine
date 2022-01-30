using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Engine
{
    internal class InputHandler
    {
        bool[] _oldMouseState = new bool[(int)Mouse.Button.ButtonCount];
        bool[] _actMouseState = new bool[(int)Mouse.Button.ButtonCount];
        bool[] _oldState = new bool[(int)Keyboard.Key.KeyCount]; //Contains the old state of keys
        bool[] _actState = new bool[(int)Keyboard.Key.KeyCount]; //Contains the actual state of keys
        static InputHandler _instance = null; //Singleton instance
        RenderWindow _window;

        /// <summary>
        /// Updates the actual states of keys
        /// </summary>
        public void Update()
        {
            for(int i = 0; i < (int)Keyboard.Key.KeyCount; i++)
            {
                if (Keyboard.IsKeyPressed((Keyboard.Key)i))
                {
                    _actState[i] = true;
                }
                else
                {
                    _actState[i] = false;
                }

                if (i < (int)Mouse.Button.ButtonCount)
                {
                    if (Mouse.IsButtonPressed((Mouse.Button)i))
                    {
                        _actMouseState[i] = true;
                    }
                    else
                    {
                        _actMouseState[i] = false;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the old states of keys
        /// </summary>
        public void UpdateOld()
        {
            Array.Copy(_actState, _oldState, _actState.Length);
            Array.Copy(_actMouseState, _oldMouseState, _actMouseState.Length);
        }

        /// <summary>
        /// Verify if a key is pressed
        /// </summary>
        /// <param name="key"> Key to verify </param>
        /// <returns> True if key is pressed </returns>
        public bool IsKeyPressed(Keyboard.Key key)
        {
            if (_actState[(int)key])
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verify if a key is clicked
        /// </summary>
        /// <param name="key"> Key to verify </param>
        /// <returns> True if key is clicked </returns>
        public bool IsKeyClicked(Keyboard.Key key)
        {
            if(!_oldState[(int)key] && _actState[(int)key])
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verify if a key is released
        /// </summary>
        /// <param name="key"> Key to verify </param>
        /// <returns> True if key is released </returns>
        public bool IsKeyReleased(Keyboard.Key key)
        {
            if (_oldState[(int)key] && !_actState[(int)key])
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verify if a mouse button is pressed
        /// </summary>
        /// <param name="button"> Button to verify </param>
        /// <returns> True if button is pressed </returns>
        public bool IsMousePressed(Mouse.Button button)
        {
            if (_actMouseState[(int)button])
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verify if a mouse button is clicked
        /// </summary>
        /// <param name="button"> Button to verify </param>
        /// <returns> True if button is clicked </returns>
        public bool IsMouseClicked(Mouse.Button button)
        {
            if (!_oldMouseState[(int)button] && _actMouseState[(int)button])
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verify if a mouse button is released
        /// </summary>
        /// <param name="button"> Button to verify </param>
        /// <returns> True if button is released </returns>
        public bool IsMouseReleased(Mouse.Button button)
        {
            if (_oldState[(int)button] && !_actState[(int)button])
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get mouse position
        /// </summary>
        /// <param name="relative"> Determine if the returned position is relative to the window </param>
        /// <returns> Mouse position </returns>
        public Vector2i GetMousePosition(bool relative)
        {
            if (relative)
            {
                return Mouse.GetPosition(_window);
            }
            else
            {
                return Mouse.GetPosition();
            }
        }

        /// <summary>
        /// Get the instance of this singleton class
        /// </summary>
        /// <returns> Instance of this class </returns>
        static public InputHandler GetInstance()
        {
            if(_instance == null)
            {
                _instance = new InputHandler();
            }

            return _instance;
        }

        /// <summary>
        /// Get/Set the used window
        /// </summary>
        public RenderWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }
    }
}
