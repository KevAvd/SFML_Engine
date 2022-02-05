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
    abstract class Widget
    {
        protected string _name; //Widget's name
        protected Vector2f _size; //Widget's size
        protected Vector2f _position; //Widget's position
        protected Color _color; //Widget's color
        protected InputHandler _inputHandler; // Handles input

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="n"> Name </param>
        /// <param name="s"> Size </param>
        /// <param name="p"> Position </param>
        /// <param name="c"> Color </param>
        /// <param name="ih"> Input handler </param>
        public Widget(string n, Vector2f s, Vector2f p, Color c, InputHandler ih)
        {
            _name = n;
            _size = s;
            _position = p;
            _color = c;
            _inputHandler = ih;
        }

        /// <summary>
        /// Update widget
        /// </summary>
        /// <param name="dt"> Delta time </param>
        public abstract void Update(float dt);
        /// <summary>
        /// Render widget
        /// </summary>
        /// <param name="w"> Used window </param>
        public abstract void Render(RenderWindow w);

        /// <summary>
        /// Get/Set name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Get/Set position
        /// </summary>
        public Vector2f Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Get/Set color
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
    }
}
