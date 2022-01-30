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
        bool[] _oldState = new bool[(int)Keyboard.Key.KeyCount]; //Contains the old state of keys
        bool[] _actState = new bool[(int)Keyboard.Key.KeyCount]; //Contains the actual state of keys
        static InputHandler _instance = null; //Singleton instance

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
            }
        }

        /// <summary>
        /// Updates the old states of keys
        /// </summary>
        public void UpdateOld()
        {
            Array.Copy(_actState, _oldState, _actState.Length);
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
    }
}
