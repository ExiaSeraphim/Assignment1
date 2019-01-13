using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using C3.XNA;
using PROG2370CollisionLibrary;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace JCaiFinalProject
{
    public class Player : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D playerTexture;
        Background background;

        GameProject g;
        AllCheckClass allCheckClass;
        ItemKeys itemKeys;
        Doors doors;
        Enemies enemies;

        const int STANDFRAMEWIDTH = 66;     //all values from spritesheet.txt
        const int STANDFRAMEHEIGHT = 92;
        const int WALKFRAMEWIDTH = 72;
        const int WALKFRAMEHEIGHT = 97;
        const int JUMPFRAMEWIDTH = 67;
        const int JUMPFRAMEHEIGHT = 94;
        const int HURTFRAMEWIDTH = 69;
        const float SCALE = 0.70f;

        const int STANDFRAME = 0;
        const int FIRSTWALKFRAME = 1;
        const int WALKFRAMES = 11;
        const int JUMPFRAME = 12;
        const int HURTFRAME = 13;
        
        private int currentFrame = STANDFRAME;      //our initial frame (when game starts)
        List<Rectangle> playerFrames;
        SpriteEffects spriteDirection;  //determines what direction the texture is pointing

        const int FRAMEDELAYMAXCOUNT = 3;  //key change every 3 frames
        int currentFrameDelayCount = 0;          // increments every frame, resets every FRAMEDELAYMAXCOUNT


        const float GRAVITY = 0.02f;        //some constant that adds a force downward.

        bool isJumping = false;
        bool isGrounded = false;
        int currentJumpPower = 0;
        const int JUMPPOWER = -11;   //negative because of inverted co-oridinates (change to suit your max jump height)
        const float JUMPSTEP = 1.3f;  // the amount of movement per frame (gravity reduces this)

        const float SPEED = 2.3f;
        Vector2 velocity;

        List<Rectangle> player;

        //List<Color> hurtColorFrame;
        //int colorFrame = 0;

        Rectangle differentLevelPlayer;

        private KeyboardState oldKeyState;

        private bool fallout = false;
        private bool isHurt = false;

        const int FALLDELAY = 5;
        int currentDelay = 0;
        //public int level = 0;
        //public int Level { get { return level; } }

        SoundEffect jumpSound;
        SoundEffect keyPicked;
        SoundEffect doorOpen;
        SoundEffect falloutSound;
        SoundEffect gameOverSound;

        bool falloutSoundPlayed = false;
        bool keyPickedSoundPlayed = false;

        const int SOUNDPLAYDELAY = 10;
        int currentSoundPlayDelay = 0;

        string[] gameInfo = { "Game Over", "Press \"R\" to Restart", "Press \"ESC\" to back to menu" };
        SpriteFont gameInfoFont;

        bool gameOver = false;
        bool gameOverSoundPlayed = false;

        List<Vector2> gameInfoPos;

        public Player(Game game, SpriteBatch spriteBatch, Background background, AllCheckClass allCheckClass, ItemKeys itemKeys, Doors doors, Enemies enemies) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.background = background;
            this.allCheckClass = allCheckClass;
            this.itemKeys = itemKeys;
            this.doors = doors;
            this.enemies = enemies;

            g = (GameProject)game;

            keyPicked = this.g.Content.Load<SoundEffect>("Sounds/key2 pickup");
            doorOpen = this.g.Content.Load<SoundEffect>("Sounds/Chest Creak");
            jumpSound = this.g.Content.Load<SoundEffect>("Sounds/jump_10");
            falloutSound = this.g.Content.Load<SoundEffect>("Sounds/death");
            gameOverSound = this.g.Content.Load<SoundEffect>("Sounds/media.io_pixie-go");

            gameInfoFont = this.g.Content.Load<SpriteFont>("Fonts/selectedFont");

            playerTexture = this.g.Content.Load<Texture2D>("Player/p3_spritesheet");

            player = new List<Rectangle>();
            player.Add(new Rectangle(70, 700, (int)(SCALE * STANDFRAMEWIDTH), (int)(SCALE * STANDFRAMEHEIGHT)));
            player.Add(new Rectangle(1190, 700, (int)(SCALE * STANDFRAMEWIDTH), (int)(SCALE * STANDFRAMEHEIGHT)));

            differentLevelPlayer = player.ElementAt<Rectangle>(allCheckClass.Level);

            velocity = new Vector2(0);

            playerFrames = new List<Rectangle>();

            //add the stand frame
            playerFrames.Add(new Rectangle(67, 196, STANDFRAMEWIDTH, STANDFRAMEHEIGHT));

            //the walk frames
            playerFrames.Add(new Rectangle(0, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(73, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(146, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(0, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(73, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(146, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(219, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(292, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(219, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(365, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            playerFrames.Add(new Rectangle(292, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));

            //the jump frame
            playerFrames.Add(new Rectangle(438, 93, JUMPFRAMEWIDTH, JUMPFRAMEHEIGHT));

            //The hurt frame
            playerFrames.Add(new Rectangle(438, 0, HURTFRAMEWIDTH, STANDFRAMEHEIGHT));

            //hurtColorFrame = new List<Color>();

            //hurtColorFrame.Add(Color.White);
            //hurtColorFrame.Add(Color.Gold);

            spriteDirection = SpriteEffects.None;

            gameInfoPos = new List<Vector2>();

            gameInfoPos.Add(new Vector2(490, 280));
            gameInfoPos.Add(new Vector2(490, 320));
            gameInfoPos.Add(new Vector2(490, 360));

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            if (!gameOver)
            {
                if (fallout)
                {
                    differentLevelPlayer = player.ElementAt<Rectangle>(allCheckClass.Level);
                    fallout = false;
                    falloutSoundPlayed = false;

                }
                spriteBatch.Draw(playerTexture, differentLevelPlayer, playerFrames.ElementAt<Rectangle>(currentFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0.1f);

            }

            if (gameOver)
            {
                for (int i = 0; i < gameInfo.Length; i++)
                {
                    spriteBatch.DrawString(gameInfoFont, gameInfo[i], gameInfoPos[i], Color.Red);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            velocity.X = 0;
            
            //get input
            KeyboardState keyState = Keyboard.GetState();

            if (!gameOver)
            {
                if (!isHurt)
                {
                    velocity.Y += GRAVITY * deltaTime;
                    //add SPEED to velocity (as required)
                    if (keyState.IsKeyDown(Keys.Right))
                    {
                        velocity.X = SPEED;
                        if (differentLevelPlayer.X + differentLevelPlayer.Width + velocity.X > g.graphics.PreferredBackBufferWidth)
                        {
                            velocity.X = 0;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Left))
                    {
                        velocity.X = -SPEED;
                        if (differentLevelPlayer.X + velocity.X < 0)
                        {
                            velocity.X = 0;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Space))
                    {
                        if (!isJumping && isGrounded)  // ready to jump
                        {
                            isJumping = true;
                            isGrounded = false;
                            currentJumpPower = JUMPPOWER;  // this is maximum "thrust" at the very beginning of jump

                            if (!g.IsMute)
                            {
                                SoundEffectInstance instance = jumpSound.CreateInstance();
                                instance.IsLooped = false;
                                instance.Volume = 0.3f;
                                instance.Play();
                            }
                        }
                    }

                    if (isJumping)
                    {
                        if (currentJumpPower < 0)  // we still have upward thrust 
                        {
                            velocity.Y -= JUMPSTEP;
                            currentJumpPower++;
                        }
                        else
                        {
                            isJumping = false;   //now we are falling
                        }
                    }

                    // our new location becomes the "proposed" location
                    Rectangle proposedLocation = new Rectangle(differentLevelPlayer.X + (int)velocity.X,
                                                            differentLevelPlayer.Y + (int)velocity.Y,
                                                            differentLevelPlayer.Width,
                                                            differentLevelPlayer.Height);

                    // check if move is ok
                    Sides collisionSides = proposedLocation.CheckCollisions(background.GroundList.ElementAt<List<Rectangle>>(allCheckClass.Level));

                    if ((collisionSides & Sides.RIGHT) == Sides.RIGHT)
                        if (velocity.X > 0)
                            velocity.X = 0;

                    if ((collisionSides & Sides.LEFT) == Sides.LEFT)
                        if (velocity.X < 0)
                            velocity.X = 0;

                    if ((collisionSides & Sides.TOP) == Sides.TOP)
                    {
                        isJumping = false;
                        velocity.Y = SPEED;
                    }


                    if ((collisionSides & Sides.BOTTOM) == Sides.BOTTOM && (currentJumpPower != JUMPPOWER))
                    {
                        velocity.Y = 0;
                        isGrounded = true;
                    }

                    //Check if have the key
                    Sides keySides = proposedLocation.CheckCollisions(itemKeys.KeyLocation.ElementAt<Rectangle>(allCheckClass.Level));
                    if (((keySides & Sides.RIGHT) == Sides.RIGHT) ||
                        ((keySides & Sides.LEFT) == Sides.LEFT) ||
                        ((keySides & Sides.TOP) == Sides.TOP) ||
                        ((keySides & Sides.BOTTOM) == Sides.BOTTOM))
                    {
                        if (!keyPickedSoundPlayed)
                        {
                            if (!g.IsMute)
                            {
                                SoundEffectInstance instance = keyPicked.CreateInstance();
                                instance.IsLooped = false;
                                instance.Volume = 0.3f;
                                instance.Play();
                                keyPickedSoundPlayed = true;
                            }
                        }


                        allCheckClass.IsPicked = true;
                    }


                    if (!g.IsMute)
                    {
                        currentSoundPlayDelay++;
                        if (currentSoundPlayDelay == SOUNDPLAYDELAY)
                        {
                            keyPickedSoundPlayed = false;
                            currentSoundPlayDelay = 0;
                        }
                    }

                    //Check the door
                    Sides doorSides = proposedLocation.CheckCollisions(doors.Door.ElementAt<Rectangle>(allCheckClass.Level));

                    if (((doorSides & Sides.RIGHT) == Sides.RIGHT) ||
                        ((doorSides & Sides.LEFT) == Sides.LEFT) ||
                        ((doorSides & Sides.TOP) == Sides.TOP) ||
                        ((doorSides & Sides.BOTTOM) == Sides.BOTTOM))
                    {
                        if (allCheckClass.IsPicked)
                        {
                            if (allCheckClass.IsOpen)
                            {
                                if (keyState.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up))
                                {
                                    allCheckClass.Level++;
                                    if (allCheckClass.Level > 1)
                                    {
                                        allCheckClass.Level = 0;
                                    }
                                    differentLevelPlayer = player.ElementAt<Rectangle>(allCheckClass.Level);
                                    allCheckClass.IsOpen = false;
                                    allCheckClass.IsPicked = false;
                                }
                            }
                            else if (!allCheckClass.IsOpen)
                            {
                                if (keyState.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up))
                                {
                                    allCheckClass.IsOpen = true;

                                    if (!g.IsMute)
                                    {                                       
                                        SoundEffectInstance instance = doorOpen.CreateInstance();
                                        instance.IsLooped = false;
                                        instance.Volume = 0.3f;
                                        instance.Play();
                                    }
                                }
                            }
                        }
                    }

                    //Check if hit by enemy
                    Sides enemySides = proposedLocation.CheckCollisions(enemies.CurrentPosition);

                    if (((enemySides & Sides.RIGHT) == Sides.RIGHT) ||
                       ((enemySides & Sides.LEFT) == Sides.LEFT) ||
                       ((enemySides & Sides.TOP) == Sides.TOP) ||
                       ((enemySides & Sides.BOTTOM) == Sides.BOTTOM))
                    {                       
                        if (!g.IsMute)
                        {
                            SoundEffectInstance instance = falloutSound.CreateInstance();
                            instance.IsLooped = false;
                            instance.Volume = 0.3f;
                            instance.Play();
                            falloutSoundPlayed = true;                            
                        }
                        isHurt = true;
                    }
                    oldKeyState = keyState;

                    //absolutely ok, update location to "proposed"
                    //anim
                    if (velocity.X < 0)
                        spriteDirection = SpriteEffects.FlipHorizontally;
                    else if (velocity.X > 0)
                        spriteDirection = SpriteEffects.None;

                    // when jumping
                    if (isJumping)
                    {
                        currentFrame = JUMPFRAME;
                        //player.Width = JUMPFRAMEWIDTH, JUMPFRAMEHEIGHT)
                    }
                    //else
                    //{
                    //    currentFrame = STANDFRAME;
                    //    //STANDFRAMEWIDTH, STANDFRAMEHEIGHT
                    //}

                    //when standing
                    // equivalent to  if(isGrounded == TRUE)
                    if (isGrounded)
                    {
                        //and standing
                        if (nearlyZero(velocity.X))
                        {
                            //STANDFRAMEWIDTH, STANDFRAMEHEIGHT
                            currentFrame = STANDFRAME;
                        }
                        // or walking
                        else
                        {
                            //WALKFRAMEWIDTH, WALKFRAMEHEIGHT
                            currentFrameDelayCount++;
                            if (currentFrameDelayCount > FRAMEDELAYMAXCOUNT)
                            {
                                currentFrameDelayCount = 0;
                                currentFrame++;  //advance to the next frame
                            }
                            if (currentFrame > WALKFRAMES)
                                currentFrame = FIRSTWALKFRAME;
                        }
                    }
                    //done anim                
                }

                if (isHurt)
                {
                    currentDelay++;
                    if (currentDelay == FALLDELAY)
                    {
                        velocity.Y += GRAVITY * deltaTime;
                        currentDelay = 0;

                    }
                    currentFrame = HURTFRAME;
                    allCheckClass.IsPicked = false;
                }

                //Check if drop out of the screen
                if (differentLevelPlayer.Y > g.graphics.PreferredBackBufferHeight)
                {
                    fallout = true;
                    isHurt = false;
                    allCheckClass.LifeCount--;
                    //allCheckClass.EmptyHeartCount++;
                    if (!falloutSoundPlayed)
                    {
                        if (!g.IsMute)
                        {
                            falloutSoundPlayed = true;
                            SoundEffectInstance instance = falloutSound.CreateInstance();
                            instance.IsLooped = false;
                            instance.Volume = 0.3f;
                            instance.Play();
                        }                        
                    }
                }
            }
            //player.X = player.X + (int)velocity.X;
            //player.Y = player.Y + (int)velocity.Y;


            if (allCheckClass.LifeCount == 0)
            {
                gameOver = true;

                if (differentLevelPlayer.Y > g.graphics.PreferredBackBufferHeight)
                {
                    velocity.Y = 0;
                }
                if (!gameOverSoundPlayed)
                {
                    if (!g.IsMute)
                    {
                        SoundEffectInstance instance = gameOverSound.CreateInstance();
                        instance.IsLooped = false;
                        instance.Volume = 0.3f;
                        instance.Play();
                        gameOverSoundPlayed = true;
                    }                   
                }               
            }

            // press R to reset the game
            if (gameOver)
            {
                if (keyState.IsKeyDown(Keys.R))
                {
                    currentFrame = STANDFRAME;
                    isJumping = false;
                    isGrounded = false;
                    currentJumpPower = 0;
                    allCheckClass.Level = 0;
                    allCheckClass.IsOpen = false;
                    allCheckClass.IsPicked = false;
                    allCheckClass.LifeCount = 3;
                    //allCheckClass.EmptyHeartCount = 0;
                    spriteDirection = SpriteEffects.None;
                    velocity = new Vector2(0);
                    currentFrameDelayCount = 0;
                    gameOver = false;
                    gameOverSoundPlayed = false;
                    enemies.CurrentPosition = enemies.EnemiesPosition.ElementAt<Rectangle>(allCheckClass.Level);
                    differentLevelPlayer = player.ElementAt<Rectangle>(allCheckClass.Level);
                }
            }


            differentLevelPlayer.X += (int)velocity.X;
            differentLevelPlayer.Y += (int)velocity.Y;
            base.Update(gameTime);
        }

        private bool nearlyZero(float f1)
        {
            // sometimes 0 is not 0 when its a float or double
            // float.Epsilon is the variance around zero
            return (Math.Abs(f1) < float.Epsilon);
        }
        private bool nearlyEqual(float f1, float f2)
        {
            // sometimes 0 is not 0 when its a float or double
            // float.Epsilon is the variance around zero
            return nearlyZero(f1 - f2);
        }
        // 97.6666666666666667
        // 2 significant digits 97.67
    }
}
