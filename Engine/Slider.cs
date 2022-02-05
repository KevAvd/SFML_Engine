using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine
{
    internal class Slider : Widget
    {
        float _t;
        float _min;
        float _max;
        float _value;
        bool _drag;
        Text _text;
        Vector2f _sliderPos;
        Vector2f _sliderSize;

        public Slider(string n, Vector2f s, Vector2f p, Color c, InputHandler ih, Font f) : base(n, s, p, c, ih)
        {
            _text = new Text("", f);
            _text.Scale = new Vector2f(0.5f, 0.5f);
            _sliderSize = new Vector2f(10, 30);
            _sliderPos = new Vector2f(_position.X, _position.Y - _sliderSize.Y / 2f);
        }

        public override void Render(RenderWindow w)
        {
            RectangleShape bar = new RectangleShape(new Vector2f(_size.X, 2));
            RectangleShape slider = new RectangleShape(_sliderSize);
            bar.FillColor = _color;
            bar.Position = new Vector2f(_position.X,_position.Y - _size.Y/2f);
            bar.OutlineColor = Color.Black;
            bar.OutlineThickness = 2;
            slider.FillColor = _color;
            slider.Position = _sliderPos;
            slider.OutlineColor = Color.Black;
            slider.OutlineThickness = 2;
            w.Draw(bar);
            w.Draw(slider);
            _text.DisplayedString = $"{_value:0.0}";
            _text.Position = new Vector2f(_sliderPos.X - _text.CharacterSize / 2f, _sliderPos.Y + 30);
            w.Draw(_text);
        }

        public override void Update(float dt)
        {
            float mouseX = _inputHandler.GetMousePosition(true).X;
            float mouseY = _inputHandler.GetMousePosition(true).Y;

            if (_inputHandler.IsPressed(Mouse.Button.Left))
            {
                if (mouseX >= _sliderPos.X && mouseX <= _sliderPos.X + _sliderSize.X && mouseY >= _sliderPos.Y && mouseY <= _sliderPos.Y + _sliderSize.Y)
                {
                    _drag = true;
                }
            }
            else
            {
                _drag = false;
            }

            if (_drag)
            {
                _sliderPos.X = Math.Clamp(mouseX, _position.X, _position.X + _size.X);
            }

            _t = (_sliderPos.X - _position.X) / (_size.X);
            _value = _t * (_max - _min) + _min;
        }

        /// <summary>
        /// Get the slider percentage
        /// </summary>
        public float T
        {
            get { return _t; }
        }

        /// <summary>
        /// Get the slider value
        /// </summary>
        public float Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Get the min value
        /// </summary>
        public float Min
        {
            get { return _min; }
            set { _min = value; }
        }

        /// <summary>
        /// Get the max value
        /// </summary>
        public float Max
        {
            get { return _max; }
            set { _max = value; }
        }
    }
}
