using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace SFML_Engine
{
    internal class Button
    {
        string _name;
        Vector2f _position;
        Vector2i _size;
        Sprite _sprite;
        Color _color;
        bool _texture;
        bool _oldState = false;
        bool _state = false;
        public event EventHandler ClickedEvent;
        public event EventHandler PressedEvent;
        public event EventHandler ReleasedEvent;

        public Button(string n, Vector2f p, Vector2i s, Texture texture)
        {
            _name = n;
            _position = p;
            _size = s;
            _texture = true;
            _sprite = new Sprite(texture);
            _sprite.Position = _position;
            _sprite.Scale = new Vector2f((float)s.X/ (float)_sprite.Texture.Size.X, (float)s.Y/ (float)_sprite.Texture.Size.Y);
        }

        public Button(string n, Vector2f p, Vector2i s, Color c)
        {
            _name = n;
            _position = p;
            _texture = false;
            _color = c;
            _size = s;
        }

        /// <summary>
        /// Update button
        /// </summary>
        public void Update()
        {
            //Get mouse position
            int mouseX = InputHandler.GetInstance().GetMousePosition(true).X;
            int mouseY = InputHandler.GetInstance().GetMousePosition(true).Y;

            //Change button state if clicked
            if (mouseX >= _position.X && mouseX <= _position.X + _size.X && mouseY >= _position.Y && mouseY <= _position.Y + _size.Y)
            {
                if(InputHandler.GetInstance().IsMousePressed(Mouse.Button.Left))
                {
                    _state = true;
                }
                else
                {
                    _state = false;
                }
            }

            //Trigger event when button is clicked
            if(!_oldState && _state)
            {
                ClickedEvent?.Invoke(this, EventArgs.Empty);
            }
            //Trigger event when button is released
            else if(_oldState && !_state)
            {
                ReleasedEvent?.Invoke(this, EventArgs.Empty);
            }
            //Trigger event when button is pressed
            if(_state)
            {
                PressedEvent?.Invoke(this, EventArgs.Empty);
            }

            //Update old button state
            _oldState = _state;
        }

        public void Render(RenderWindow w)
        {
            //Render texture if button has one
            if (_texture)
            {
                w.Draw(_sprite);
            }
            //Render a rectangle if button has no texture
            else
            {
                RectangleShape shape = new RectangleShape(new Vector2f(_size.X,_size.Y));
                shape.Position = _position;
                shape.FillColor = _color;
                w.Draw(shape);
            }
        }

        /// <summary>
        /// Get/Set button position
        /// </summary>
        public Vector2f Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Get/Set button size
        /// </summary>
        public Vector2i Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
}
