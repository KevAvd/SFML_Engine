using SFML_Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

Game game = new Game(800, 600, "My game");
game.Run();
LogHandler.GetInstance().ShowLogs();
Console.ReadLine();

class Menu : GameState
{
    float radius;
    Color color;
    bool visible = false;
    Slider rad;
    Slider sldRed;
    Slider sldGreen;
    Slider sldBlue;
    public override void Load()
    {
        _assetManager.Loadfiles(@"C:\Users\drimi\OneDrive\Bureau\Asset");
        Button btn1 = _widgetHandler.CreateButton("btn1", new Vector2f(100, 50), new Vector2f(0, 0), Color.Red, _assetManager.GetFont("ARIAL.TTF"));
        btn1.ClickedEvent += btn1_click;
        rad = _widgetHandler.CreateSlider("sldr1", new Vector2f(300, 10), new Vector2f(450, 20), Color.White, _assetManager.GetFont("ARIAL.TTF"));
        sldRed = _widgetHandler.CreateSlider("sldr2", new Vector2f(300, 10), new Vector2f(450, 80), Color.White, _assetManager.GetFont("ARIAL.TTF"));
        sldGreen = _widgetHandler.CreateSlider("sldr3", new Vector2f(300, 10), new Vector2f(450, 140), Color.White, _assetManager.GetFont("ARIAL.TTF"));
        sldBlue = _widgetHandler.CreateSlider("sldr4", new Vector2f(300, 10), new Vector2f(450, 200), Color.White, _assetManager.GetFont("ARIAL.TTF"));
        rad.Min = 1;
        rad.Max = 500;
        sldRed.Min = 0;
        sldRed.Max = 255;
        sldGreen.Min = 0;
        sldGreen.Max = 255;
        sldBlue.Min = 0;
        sldBlue.Max = 255;

    }
    public override void Update()
    {
        radius = rad.Value;
        color = new Color((byte)sldRed.Value, (byte)sldGreen.Value, (byte)sldBlue.Value);
    }
    public override void Render()
    {
        if (visible)
        {
            CircleShape shape = new CircleShape(radius);
            shape.Origin = new Vector2f(radius, radius);
            shape.Position = new Vector2f(400, 300);
            shape.FillColor = color;
            _window.Draw(shape);
        }
    }

    void btn1_click (object sender, EventArgs e)
    {
        visible = !visible;
    }

    public Menu(string name, StateHandler sh, AssetManager am, InputHandler ih, GameClock gc, RenderWindow w, WidgetHandler wh) : base(name, sh, am, ih, gc, w, wh) { }
}