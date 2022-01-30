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
        bool _pressed;
        bool _clicked;

        public Button(string n, Vector2f p, Vector2i s, Texture texture)
        {
            _name = n;
            _position = p;
            _size = s;
            _sprite = new Sprite(texture);
        }

        public void Update()
        {
            int mouseX = InputHandler.GetInstance().GetMousePosition(true).X;
            int mouseY = InputHandler.GetInstance().GetMousePosition(true).Y;

            if (InputHandler.GetInstance().IsMousePressed(Mouse.Button.Left))
            {
                if (mouseX > _position.X && mouseX < _position.X + Size.X && mouseY > _position.Y && mouseY < _position.Y + _size.Y)
                {
                    if (InputHandler.GetInstance().IsMouseClicked(Mouse.Button.Left))
                    {
                        Console.WriteLine($"{_name} is pressed");
                    }
                }
            }
            _sprite.Position = _position;
            _sprite.TextureRect = new IntRect(new Vector2i(0,0), _size);
        }

        public void Render(RenderWindow w)
        {
            w.Draw(_sprite);
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
        /// Get/Set size
        /// </summary>
        public Vector2i Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public bool Pressed
        {
            get { return _pressed; }
        }

        public bool Clicked
        {
            get { return _clicked; }
        }
    }
}
