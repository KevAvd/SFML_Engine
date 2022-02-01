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
        GameState _nextState; //Contains the next state
        List<GameState> _allStates = new List<GameState>(); //Contains all the possible game state
        bool _isChanging; //Indicate if the actual state is going to change

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="states"> All possible states </param>
        public StateHandler(params GameState[] states)
        {
            AddState(states);
            if(_allStates.Count > 0) { _state = _allStates[0]; }
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
        /// Update the actual state
        /// </summary>
        public void Update()
        {
            if (_isChanging)
            {
                _state = _nextState;
                _isChanging = false;
            }
        }

        /// <summary>
        /// Change the to the next state in the next update
        /// </summary>
        /// <param name="nextStateName"></param>
        public void ChangeState(string nextStateName)
        {
            _isChanging = true;
            foreach(GameState gs in _allStates)
            {
                if(gs.Name == nextStateName) { _nextState = gs; break; }
            }
        }

        /// <summary>
        /// Get the actual state
        /// </summary>
        public GameState ActualState
        {
            get { return _state; }
        }

        /// <summary>
        /// Get all the stored states
        /// </summary>
        public List<GameState> AllStates
        {
            get { return _allStates; }
        }
    }
}
