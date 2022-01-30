using SFML_Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

Menu menu = new Menu("Menu");
Game game = new Game(800, 600, "My game", "Menu", menu);
game.Run();
Console.ReadLine();

class Menu : GameState
{
    AssetManager am = new AssetManager(@"C:\Users\drimi\OneDrive\Bureau\TestDir", @"C:\Users\drimi\OneDrive\Bureau\TestDirImg");
    Text text;
    Sprite sprite;
    Button button;
    public Menu(string name) : base(name)
    {
        am.Loadfiles();
        button = new Button("btn1", new Vector2f(300, 400), new Vector2i(400, 200), am.GetTexture("button.PNG"));
        Texture texture = new Texture(am.GetTexture("IMG_20190717_022949.jpg"));
        sprite = new Sprite(texture);
        sprite.Position = new Vector2f(0, 0);
        text = new Text("TEXT", am.GetFont("Roboto-Black.ttf"));
        text.Position = new Vector2f(400, 300);
    }

    override public void HandleInput()
    {
        InputHandler.GetInstance().UpdateOld();
        InputHandler.GetInstance().Update();
        if (InputHandler.GetInstance().IsKeyClicked(Keyboard.Key.Q))
        {
            LogHandler.GetInstance().ShowLogs();
        }
    }
    override public void Update(float dt)
    {
        button.Update();
    }
    override public void Render(RenderWindow w)
    {
        w.Draw(sprite);
        w.Draw(text);
        button.Render(w);
    }
}