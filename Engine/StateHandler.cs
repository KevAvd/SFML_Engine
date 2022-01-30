using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SFML_Engine
{
    internal class StateHandler
    {
        GameState _state; //Contains the actual state
        List<GameState> _allStates = new List<GameState>(); //Contains all the possible game state

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="states"> All possible states </param>
        public StateHandler(params GameState[] states)
        {
            AddState(states);
        }

        /// <summary>
        /// Add a new possible state
        /// </summary>
        /// <param name="states"> State(s) to add </param>
        public void AddState(params GameState[] states)
        {
            foreach (GameState s in states)
            {
                _allStates.Add(s);
            }
        }

        /// <summary>
        /// Change the current state
        /// </summary>
        /// <param name="name"> Name of the next state </param>
        public void ChangeState(string name)
        {
            foreach(GameState s in _allStates)
            {
                if (s.Name == name)
                {
                    _state = s;
                    return;
                }
            }

            LogHandler.GetInstance().AddLog($"[STATEHANDLER][CHANGESTATE-ERROR] this doesn't contains a state named {name}");
        }

        /// <summary>
        /// Play the actual state
        /// </summary>
        /// <param name="w"> Used window </param>
        /// <param name="dt"> Delta time </param>
        public void PlayState(RenderWindow w, float dt)
        {
            if(_state == null)
            {
                LogHandler.GetInstance().AddLog("[STATEHANDLER][PLAYSTATE-ERROR] the actual state is null");
                return;
            }

            _state.HandleInput();
            _state.HandleEvent(w);
            _state.Update(dt);
            w.Clear();
            _state.Render(w);
            w.Display();
        }
    }
}
