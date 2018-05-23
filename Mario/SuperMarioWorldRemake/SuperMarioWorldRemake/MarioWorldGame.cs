using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using static SuperMarioWorldRemake.Program;

namespace SuperMarioWorldRemake
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    public class MarioWorldGame : Game
    {
        private Song Music_Theme;
        private Song Victory_Theme;
        private Song Defeat_Theme;
        private Song Menu_Music;
        private SpriteFont font;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D Background;
        private Rectangle mainFrame;
        private Texture2D Startscreen;
        private Texture2D Endscreen;
        private Texture2D Winningscreen;
        private Mario mario;
        private Camera2D camera;
        private GameObject[,] level;
        private KeyboardState keyBoardState;
        private KeyboardState previousKeyBoardState;
        private float cameraHeight;
        private int _rememberLife;
        private string _selectedLevel;
        public GameStates gameState = GameStates.Start;
        public bool starting = true;
        Finish finish;
        public bool Won = false;
        public MarioWorldGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _selectedLevel = @"Content\Level1.txt";
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            camera = new Camera2D(GraphicsDevice.Viewport);
            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            FileReader reader = new FileReader();
            Music_Theme = Content.Load<Song>("AchtergrondMuziek");
            Victory_Theme = Content.Load<Song>("MarioVictory");
            Defeat_Theme = Content.Load<Song>("MarioLost");
            Menu_Music = Content.Load<Song>("MarioMenu");

            string[,] itemID = reader.Read(_selectedLevel);
            level = new GameObject[itemID.GetLength(0), itemID.GetLength(1)];
            font = Content.Load<SpriteFont>("Score");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Background = Content.Load<Texture2D>("Background");
            Startscreen = Content.Load<Texture2D>("startScreen");
            Endscreen = Content.Load<Texture2D>("GameOverScreen");
            Winningscreen = Content.Load<Texture2D>("MarioVictoryScreen");
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Texture2D grassTile1Texture = Content.Load<Texture2D>("grassTile1");
            Texture2D grassTile9Texture = Content.Load<Texture2D>("grassTile9");
            Texture2D coinTexture = Content.Load<Texture2D>("Coin");
            Texture2D mysteryBlockTexture = Content.Load<Texture2D>("mysteryBox");
            Texture2D mushroomTexture = Content.Load<Texture2D>("Mushroom");
            Texture2D fireFlowerTexture = Content.Load<Texture2D>("FireFlower");
            Texture2D greenPipeTexture = Content.Load<Texture2D>("theGreenPipe");
            Texture2D finishTexture = Content.Load<Texture2D>("Finish");
            Texture2D marioTextureLeft = Content.Load<Texture2D>("marioLeft");
            Texture2D marioTextureRight = Content.Load<Texture2D>("marioRight");
            Texture2D marioTextureBigRight = Content.Load<Texture2D>("marioBigRight");
            Texture2D marioTextureBigLeft = Content.Load<Texture2D>("marioBigLeft");
            Texture2D marioTextureBigFallRight = Content.Load<Texture2D>("marioBigFallRight");
            Texture2D marioTextureBigFallLeft = Content.Load<Texture2D>("marioBigFallLeft");
            Texture2D marioTextureBigJump = Content.Load<Texture2D>("marioBigJumpRight");
            Texture2D marioTextureBigJumpLeft = Content.Load<Texture2D>("marioBigJumpLeft");
            Texture2D marioTextureFalling = Content.Load<Texture2D>("marioFalling");
            Texture2D marioTextureFallingLeft = Content.Load<Texture2D>("marioFallingLeft");
            Texture2D marioTextureJump = Content.Load<Texture2D>("jump");
            Texture2D marioTextureJumpLeft = Content.Load<Texture2D>("jumpLeft");
            Texture2D marioTextureStoop = Content.Load<Texture2D>("stoop");
            Texture2D marioTextureStoopLeft = Content.Load<Texture2D>("stoopLeft");
            Texture2D beachKoopaTextureRight = Content.Load<Texture2D>("beach_koopa_right");
            Texture2D beachKoopaTextureLeft = Content.Load<Texture2D>("beach_koopa_left");
            Texture2D BanzaiBillTexture = Content.Load<Texture2D>("BanzaiBill");
            Texture2D KoopaShellTexture = Content.Load<Texture2D>("KoopaShell");
            Texture2D usedMysteryBlockTexture = Content.Load<Texture2D>("UsedMysteryBlock");
            for (int x = 0; x < itemID.GetLength(0) - 1; x++)
            {
                for (int y = 0; y < itemID.GetLength(1) - 1; y++)
                    switch (itemID[x, y])
                    {
                        case "1":
                            level[x, y] = new Tile(grassTile9Texture, x, y);
                            break;
                        case "2":
                            level[x, y] = new Tile(grassTile1Texture, x, y);
                            break;
                        case "3":
                            level[x, y] = new MysteryBlock(mysteryBlockTexture, mushroomTexture, fireFlowerTexture,usedMysteryBlockTexture , x, y);
                            break;
                        case "4":
                            level[x, y] = new Tile(greenPipeTexture, x, y);
                            break;
                        case "5":
                            level[x, y] = new Coin(coinTexture, x, y);
                            break;
                        case "6":
                            level[x, y] = new Koopa(beachKoopaTextureRight, beachKoopaTextureLeft, KoopaShellTexture, x, y);
                            break;
                        case "7":
                            level[x, y] = new BanzaiBill(BanzaiBillTexture, x, y);
                            break;
                        case "8":
                            finish = new Finish(finishTexture, x, y);
                            level[x, y] = finish;
                            break;
                        default:
                            break;
                    }
            }
            changeMusic();
            mario = new Mario(marioTextureLeft, marioTextureRight, marioTextureFallingLeft, marioTextureFalling, marioTextureJumpLeft, marioTextureJump, marioTextureStoopLeft, marioTextureStoop, marioTextureBigRight, marioTextureBigLeft, marioTextureBigFallLeft, marioTextureBigFallRight, marioTextureBigJumpLeft, marioTextureBigJump, 1, 20);
            //50 is de golden value om een goede camera hoogte te hebben DONTCHANGE
            cameraHeight = mario.GetPosition().Y - 50;
            camera.Position = new Vector2(mario.GetPosition().X + 150, cameraHeight) - camera.Origin;
        }
        /// <summary>
        /// the enum that holds the gamestates
        /// </summary>
        public enum GameStates
        {
            Start,
            Playing,
            GameOver,
            Winning,
        }
        /// <summary>
        /// Changes the gamestate based on specified conditions
        /// </summary>
        public void changeState()
        {
            if (starting)
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0&&Keyboard.GetState().IsKeyUp(Keys.M))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    {
                        _selectedLevel = @"Content\Level1.txt";
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D2))
                    {
                        _selectedLevel = @"Content\Level2.txt";
                    }
                    LoadContent();
                    MediaPlayer.Stop();
                    gameState = GameStates.Playing;
                    LoadContent();
                    starting = false;
                }
                else
                {
                    gameState = GameStates.Start;
                }
            }
            if (mario.lostLife)
            {
                _rememberLife = mario.sc.lives;
                LoadContent();
                mario.sc.lives = _rememberLife;
                mario.lostLife = false;
            }
            if (mario.sc.ending)
            {
                MediaPlayer.Stop();
                gameState = GameStates.GameOver;
                LoadContent();
                mario.sc.ending = false;
            }
            if (finish.touched)
            {
                MediaPlayer.Stop();
                gameState = GameStates.Winning;
                LoadContent();
                finish.touched = false;
            }
            if(gameState == GameStates.Winning || gameState == GameStates.GameOver)
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Thread.Sleep(1500);
                        starting = true;
                        gameState = GameStates.Start;
                        LoadContent();
                    }
                }
            }
        }
        //the music gets changed based on the gamestate
        public void changeMusic()
        {
            if (gameState == GameStates.Start)
            {
                MediaPlayer.Play(Menu_Music);
            }
            if (gameState == GameStates.Playing)
            {
                MediaPlayer.Play(Music_Theme);
            }
            if (gameState == GameStates.GameOver)
            {
                MediaPlayer.Play(Defeat_Theme);
            }
            if (gameState == GameStates.Winning)
            {
                MediaPlayer.Play(Victory_Theme);
            }
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                starting = true;
            }
            changeState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (gameState == GameStates.Playing)
            {
                for (int i = 0; i < level.GetLength(0); i++)
                {
                    for (int j = 0; j < level.GetLength(1); j++)
                    {
                        if (level[i, j] != null)
                            level[i, j].Update(gameTime);
                    }
                }
                mario.Update(gameTime);
                mario.sc.UpdateTime(gameTime.ElapsedGameTime.Milliseconds);
                mario.sc.checkStatus();
                // TODO: Add your update logic here
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                var keyboardState = Keyboard.GetState();
                //als getal int de if statement en in de if niet samen 400 zijn gaat mario stotteren
                if (mario.GetPosition().X > camera.Position.X + 430)
                {
                    camera.Position = new Vector2(mario.GetPosition().X - 30, cameraHeight) - camera.Origin;
                }
                if (mario.GetPosition().X < camera.Position.X + 360)
                {
                    camera.Position = new Vector2(mario.GetPosition().X + 40, cameraHeight) - camera.Origin;
                }
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// checks the collisions and the checked classes do the neccesairy actions
        /// </summary>
        private void CheckCollision()
        {
            mario.checkCollisions(level);
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    if (level[i, j] != null)

                        if (level[i, j] is Koopa || level[i, j] is PowerUp)
                            level[i, j].checkCollisions(level);
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (gameState == GameStates.Start)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Startscreen, mainFrame, Color.White);
                spriteBatch.End();
            }
            else if (gameState == GameStates.Playing)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                var viewMatrix = camera.GetViewMatrix();
                spriteBatch.Begin(transformMatrix: viewMatrix);
                spriteBatch.Draw(Background, mainFrame, Color.White);
                Rectangle mainFrameNext = new Rectangle(mainFrame.X + mainFrame.Width, 0, mainFrame.Width, mainFrame.Height);
                Rectangle mainFramePrevious = new Rectangle(mainFrame.X - mainFrame.Width, 0, mainFrame.Width, mainFrame.Height);
                spriteBatch.Draw(Background, mainFrameNext, Color.White);
                spriteBatch.Draw(Background, mainFramePrevious, Color.White);
                if (mario.GetPosition().X > mainFrameNext.X + mainFrame.Width / 2)
                {
                    mainFrame = mainFrameNext;
                }
                if (mario.GetPosition().X < mainFramePrevious.X + mainFrame.Width / 2)
                {
                    mainFrame = mainFramePrevious;
                }
                for (int i = 0; i < level.GetLength(0); i++)
                {
                    for (int j = 0; j < level.GetLength(1); j++)
                    {
                        if (level[i, j] != null)
                            level[i, j].Draw(spriteBatch);
                    }
                }
                CheckCollision();
                keyBoardState = Keyboard.GetState();
                mario.Move(keyBoardState, previousKeyBoardState);
                mario.Draw(spriteBatch);
                spriteBatch.End();
                mario.sc.Draw(spriteBatch, font);
                previousKeyBoardState = keyBoardState;
                // TODO: Add your drawing code here
                base.Draw(gameTime);
            }
            else if (gameState == GameStates.GameOver)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Endscreen, mainFrame, Color.White);
                spriteBatch.End();
            }
            else if (gameState == GameStates.Winning)
            {
                spriteBatch.Begin();
                mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                spriteBatch.Draw(Winningscreen, mainFrame, Color.White);
                spriteBatch.End();
            }
        }
    }
}
