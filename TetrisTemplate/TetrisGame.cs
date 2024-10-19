using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

class TetrisGame : Game
{
    SpriteBatch spriteBatch;

    InputHelper inputHelper;

    GameWorld gameWorld;

    // A static reference to the ContentManager object, used for loading assets.
    public static ContentManager ContentManager { get; private set; }
    
    // A static reference to the width and height of the screen.
    public static Point ScreenSize { get; private set; }

    [STAThread]

    static void Main()
    {
        TetrisGame game = new TetrisGame();
        game.Run();
    }

    // constructor method
    public TetrisGame()
    {        
        // initialize the graphics device
        GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);

        // store a static reference to the content manager, so other objects can use it
        ContentManager = Content;
        
        // set the directory where game assets are located
        Content.RootDirectory = "Content";

        // set the desired window size
        ScreenSize = new Point(1920, 1080);
        graphics.PreferredBackBufferWidth = ScreenSize.X;
        graphics.PreferredBackBufferHeight = ScreenSize.Y;

        // create the input helper object
        inputHelper = new InputHelper();
        IsMouseVisible = true;
    }

   // Mehod used to load all the content
    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(Content.Load<Song>("Sound/TetrisSong"));
        MediaPlayer.Volume = 0.005f;
        gameWorld = new GameWorld();
    }
   
    
    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update(gameTime);
        gameWorld.HandleInput(gameTime, inputHelper);
        gameWorld.Update(gameTime);
        if(inputHelper.KeyPressed(Keys.Escape)) { Exit(); }
        base.Update(gameTime);

        
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        gameWorld.Draw(gameTime, spriteBatch);
    }
}

