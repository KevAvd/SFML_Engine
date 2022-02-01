using SFML_Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

Menu menu = new Menu("Menu");
Game game = new Game(800, 600, "My game", menu);
game.Run();

class Menu : GameState
{
    AssetManager am = new AssetManager(@"C:\Users\drimi\OneDrive\Bureau\TestDir", @"C:\Users\drimi\OneDrive\Bureau\TestDirImg");
    Button button;

    public Menu(string name) : base(name) { }
    public override void Load()
    {
        am.Loadfiles();
        button = new Button("btn1", new Vector2f(350, 275), new Vector2i(100, 50), Color.Red);
    }
    override public void Update(float dt)
    {
        button.Update();
    }
    override public void Render(RenderWindow w)
    {
        button.Render(w);
    }
}