using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace SFML_Engine
{
    internal class WidgetHandler
    {
        List<Widget> _widgets = new List<Widget>(); //List of widget
        InputHandler _inputHandler; // Handles input

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ih"> Input handler </param>
        public WidgetHandler(InputHandler ih)
        {
            _inputHandler = ih;
        }

        /// <summary>
        /// Add a button
        /// </summary>
        /// <param name="n"> Name </param>
        /// <param name="s"> Size </param>
        /// <param name="p"> Position </param>
        /// <param name="c"> Color </param>
        /// <param name="f"> Font </param>
        public Button CreateButton(string n, Vector2f s, Vector2f p, Color c, Font f)
        {
            Button btn = new Button(n, s, p, c, _inputHandler, f);
            _widgets.Add(btn);
            return btn;
        }

        /// <summary>
        /// Add a slider
        /// </summary>
        /// <param name="n"> Name </param>
        /// <param name="s"> Size </param>
        /// <param name="p"> Position </param>
        /// <param name="c"> Color </param>
        /// <param name="f"> Font </param>
        public Slider CreateSlider(string n, Vector2f s, Vector2f p, Color c, Font f)
        {
            Slider slider = new Slider(n, s, p, c, _inputHandler, f);
            _widgets.Add(slider);
            return slider;
        }

        /// <summary>
        /// Update all widgets
        /// </summary>
        /// <param name="dt"> Delta time </param>
        public void Update(float dt)
        {
            foreach (Widget widget in _widgets)
            {
                widget.Update(dt);
            }
        }

        /// <summary>
        /// Render all widget
        /// </summary>
        /// <param name="w"> Used window </param>
        public void Render(RenderWindow w)
        {
            foreach (Widget widget in _widgets)
            {
                widget.Render(w);
            }
        }

        /// <summary>
        /// Return widget with searched name
        /// </summary>
        /// <param name="name"> Name of the widget </param>
        /// <returns> Widget </returns>
        public Widget GetWidget(string name)
        {
            foreach (Widget w in _widgets)
            {
                if(w.Name == name) { return w; }
            }

            LogHandler.GetInstance().AddLog($"[WIDGETHANDLER][GETWIDGET-ERROR] no widget with name {name} found");
            return null;
        }
    }
}
