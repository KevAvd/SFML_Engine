using SFML_Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

Game game = new Game(1920, 1080, "My game");
game.Run();
LogHandler.GetInstance().ShowLogs();
Console.ReadLine();

class Menu : GameState
{
    Button btn1;
    Button btn2;
    Slider radius;
    Slider gravity;
    Slider bounciness;
    Particle p;
    Label info;
    public override void Load()
    {
        _assetManager.Loadfiles(@"C:\Users\drimi\OneDrive\Bureau\Asset");

        //Init font
        _widgetHandler.Font = _assetManager.GetFont("ARIAL.TTF");

        //Init particle
        p = new Particle(50, Color.Red, new Vector2f(1920f / 2f, 1080f / 2f), new Vector2f(0, 0));

        //Create button
        btn1 = _widgetHandler.CreateButton();
        btn1.Color = Color.Red;
        btn1.Position = new Vector2f(100, 1000);
        btn1.Size = new Vector2f(100, 50);
        btn1.Text = "Position";
        btn1.ClickedEvent += rndPos;

        //Create button
        btn2 = _widgetHandler.CreateButton();
        btn2.Color = Color.Green;
        btn2.Position = new Vector2f(100, 850);
        btn2.Size = new Vector2f(100, 50);
        btn2.Text = "Velocity";
        btn2.ClickedEvent += rndVel;

        //Create label
        info = _widgetHandler.CreateLabel();

        //Creates slider
        radius = _widgetHandler.CreateSlider("Radius");
        radius.Min = 10;
        radius.Max = 500;
        radius.Position = new Vector2f(100, 60);

        //Creates slider
        gravity = _widgetHandler.CreateSlider("Gravity");
        gravity.Min = 0;
        gravity.Max = 100;
        gravity.Position = new Vector2f(100, 130);

        //Create slider
        bounciness = _widgetHandler.CreateSlider("Bounciness");
        bounciness.Min = 0;
        bounciness.Max = 100;
        bounciness.Position = new Vector2f(100, 270);
    }
    public override void Update()
    {
        info.Text = $"Velocity: X:{p.Velocity.X:0.00} Y:{p.Velocity.Y:0.00}\nFPS: {1/_gameClock.ElapsedFrame():0.00}";
        info.Position = new Vector2f(1500, 0);
        p.Radius = radius.Value;
        p.Velocity += new Vector2f(0, gravity.Value);
        p.Update(_gameClock.ElapsedFrame());

        if (p.Position.X + p.Radius > _window.Size.X)
        {
            p.Position = new Vector2f(_window.Size.X - p.Radius, p.Position.Y);
            p.Velocity = new Vector2f(-p.Velocity.X * (bounciness.Value / 100), p.Velocity.Y);
        }
        else if (p.Position.X - p.Radius < 0)
        {
            p.Position = new Vector2f(p.Radius, p.Position.Y);
            p.Velocity = new Vector2f(-p.Velocity.X * (bounciness.Value / 100), p.Velocity.Y);
        }
        if (p.Position.Y + p.Radius > _window.Size.Y)
        {
            p.Position = new Vector2f(p.Position.X, _window.Size.Y - p.Radius);
            p.Velocity = new Vector2f(p.Velocity.X, -p.Velocity.Y * (bounciness.Value / 100));
        }
        else if (p.Position.Y - p.Radius < 0)
        {
            p.Position = new Vector2f(p.Position.X, p.Radius);
            p.Velocity = new Vector2f(p.Velocity.X, -p.Velocity.Y * (bounciness.Value/100));
        }
    }
    public override void Render()
    {
        p.Render(_window);
    }

    void rndVel (object sender, EventArgs e)
    {
        Random rnd = new Random();
        p.Velocity = new Vector2f(rnd.Next(10000), rnd.Next(10000));
    }

    void rndPos(object sender, EventArgs e)
    {
        Random rnd = new Random();
        p.Position = new Vector2f(rnd.Next(1920), rnd.Next(1080));
    }

    public Menu(string name, StateHandler sh, AssetManager am, InputHandler ih, GameClock gc, RenderWindow w, WidgetHandler wh) : base(name, sh, am, ih, gc, w, wh) { }
}

class Particle
{
    public float Radius { get; set; }
    public Color Color { get; set; }
    public Vector2f Position { get; set; }
    public Vector2f Velocity { get; set; }
    public Particle(float r, Color c, Vector2f p, Vector2f v)
    {
        Radius = r;
        Color = c;
        Position = p;
        Velocity = v;
    }

    public void Update(float dt)
    {
        Position += Velocity * dt;
    }

    public void Render(RenderWindow w)
    {
        CircleShape c = new CircleShape(Radius);
        c.Origin = new Vector2f(Radius, Radius);
        c.FillColor = Color;
        c.Position = Position;
        w.Draw(c);
    }
}