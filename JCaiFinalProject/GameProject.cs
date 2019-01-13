/*  Name : Junyan Cai
 *  Section : 5
 *  Date : 2018 Dec 10
 *  Assignment # : Final Assignment
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;


namespace JCaiFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameProject : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        Song backgroundmusic;

        MenuPage menuPage;
        PlayPage playPage;

        private bool isMute = false;
        public bool IsMute { get { return isMute; } set { isMute = value; } }

        private KeyboardState oldState;

        private bool showHelp = true;
        public bool ShowHelp { get { return showHelp; } set { showHelp = value; } }

        public GameProject()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1260;
            graphics.PreferredBackBufferHeight = 840;

            this.backgroundmusic = Content.Load<Song>("Sounds/one_0");

            MediaPlayer.Play(backgroundmusic);
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menuPage = new MenuPage(this);
            this.Components.Add(menuPage);
            menuPage.show();

            playPage = new PlayPage(this);
            this.Components.Add(playPage);

            // TODO: use this.Content to load your game content here
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
            int selectedIndex = 0;

            KeyboardState keyState = Keyboard.GetState();

            if (menuPage.Enabled)
            {
                selectedIndex = menuPage.Menu.SelectedIndex;

                if (selectedIndex == 0 && keyState.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    playPage.show();
                }
                else if (selectedIndex == 1 && keyState.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }

            }

            if (playPage.Enabled)
            {
                if (keyState.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    menuPage.show();
                }
            }

            if (keyState.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))
            {
                if (isMute)
                {
                    isMute = false;
                    MediaPlayer.IsMuted = false;
                }
                else if(!isMute)
                {
                    isMute = true;
                    MediaPlayer.IsMuted = true;
                }                
            }

            if (keyState.IsKeyDown(Keys.H) && oldState.IsKeyUp(Keys.H))
            {
                if (showHelp)
                {
                    showHelp = false;
                }
                else if (!showHelp)
                {
                    showHelp = true;
                }
            }
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            oldState = keyState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void hideAllScenes()
        {
            ScenesComponent gameScenes = null;
            foreach (GameComponent item in Components)
            {
                if (item is ScenesComponent)
                {
                    gameScenes = (ScenesComponent)item;
                    gameScenes.hide();
                }
            }
        }
    }
}
