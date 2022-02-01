using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace SFML_Engine
{
    abstract class GameState
    {
        protected string _name;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> Name of the game state </param>
        public GameState(string name)
        {
            _name = name;
        }
        /// <summary>
        /// Loads all elements necessary
        /// </summary>
        public abstract void Load();
        /// <summary>
        /// Update state
        /// </summary>
        /// <param name="dt"> Delta time </param>
        public abstract void Update(float dt);
        /// <summary>
        /// Render state
        /// </summary>
        /// <param name="w"> Used window </param>
        public abstract void Render(RenderWindow w);

        public string Name
        {
            get { return _name; }
        }
    }
}
