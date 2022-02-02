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
    public Menu(string name, StateHandler sh, AssetManager am, InputHandler ih, GameClock gc, RenderWindow w) : base(name, sh, am, ih, gc, w)
    {
    }

    public override void Load()
    {
        _clearColor = Color.Yellow;
    }
    public override void Update()
    {
        if (_inputHandler.IsClicked(Keyboard.Key.Q))
        {
            _stateHandler.SetNextState(typeof(Gaming).Name);
        }
    }
    public override void Render()
    {

    }
}

class Gaming : GameState
{
    public Gaming(string name, StateHandler sh, AssetManager am, InputHandler ih, GameClock gc, RenderWindow w) : base(name, sh, am, ih, gc, w)
    {
    }

    public override void Load()
    {
        _clearColor = Color.Red;
    }
    public override void Update()
    {
        if (_inputHandler.IsClicked(Keyboard.Key.Q))
        {
            _stateHandler.SetNextState(typeof(Menu).Name);
        }
    }
    public override void Render()
    {

    }
}