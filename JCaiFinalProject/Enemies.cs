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

namespace JCaiFinalProject
{
    public class Enemies : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D enemyTex;

        GameProject g;
        AllCheckClass allCheckClass;

        //List<Rectangle> level1EnemiesPosition;
        //List<Rectangle> level2EnemiesPosition;

        //List<List<Rectangle>> enemiesPosition;
        List<Rectangle> enemiesPosition;
        public List<Rectangle> EnemiesPosition { get { return enemiesPosition; } }

        //List<Rectangle> level1EnemiesMoveArea;
        //List<Rectangle> level2EnemiesMoveArea;

        //List<List<Rectangle>> enemiesMoveArea;
        List<Rectangle> enemiesMoveArea;

        List<Rectangle> enemiesFrame;

        const int ENEMIESBLOCKER = 70;

        const int ENEMYSTANDWIDTH = 54;
        const int ENEMYMOVEWIDTH = 57;
        const int ENEMYHEIGHT = 31;

        int enemyCurrentFrame = 0;
        const int ENEMYFRAMEDELAY = 15;
        int currentFrameCount = 0;
        const float SPEED = 1f;
        Vector2 velocity;

        int oldLevel;

        Rectangle currentPosition;

        public Rectangle CurrentPosition { get { return currentPosition; } set { currentPosition = value; } }

        SpriteEffects enemiesSpriteEffects;

        public Enemies(Game game, SpriteBatch spriteBatch, AllCheckClass allCheckClass) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.allCheckClass = allCheckClass;

            g = (GameProject)game;

            enemyTex = this.g.Content.Load<Texture2D>("Enemies/enemies_spritesheet");

            //snailWalk1 = 143 34 54 31
            //snailWalk2 = 67 87 57 31
            //level1EnemiesPosition = new List<Rectangle>();

            //level1EnemiesPosition.Add(new Rectangle(210,739,ENEMYSTANDWIDTH, ENEMYHEIGHT));
            //level1EnemiesPosition.Add(new Rectangle(350, 319, ENEMYSTANDWIDTH, ENEMYHEIGHT));

            enemiesPosition = new List<Rectangle>();

            enemiesPosition.Add(new Rectangle(350, 319, ENEMYSTANDWIDTH, ENEMYHEIGHT));
            enemiesPosition.Add(new Rectangle(350, 459, ENEMYSTANDWIDTH, ENEMYHEIGHT));



            //level2EnemiesPosition = new List<Rectangle>();

            //level2EnemiesPosition.Add(new Rectangle(210, 739, ENEMYSTANDWIDTH, ENEMYHEIGHT));
            //level2EnemiesPosition.Add(new Rectangle(350, 459, ENEMYSTANDWIDTH, ENEMYHEIGHT));
            //level2EnemiesPosition.Add(new Rectangle(420, 179, ENEMYSTANDWIDTH, ENEMYHEIGHT));

            //enemiesPosition = new List<List<Rectangle>>();

            //enemiesPosition.Add(level1EnemiesPosition);
            //enemiesPosition.Add(level2EnemiesPosition);

            //level1EnemiesMoveArea = new List<Rectangle>();

            enemiesMoveArea = new List<Rectangle>();

            enemiesMoveArea.Add(new Rectangle(350,280, ENEMIESBLOCKER*8, ENEMIESBLOCKER));
            enemiesMoveArea.Add(new Rectangle(350,420,ENEMIESBLOCKER*5, ENEMIESBLOCKER));

            //level1EnemiesMoveArea.Add(new Rectangle(140, 700, ENEMIESBLOCKER, ENEMIESBLOCKER));
            //level1EnemiesMoveArea.Add(new Rectangle(910, 700, ENEMIESBLOCKER, ENEMIESBLOCKER));
            //level1EnemiesMoveArea.Add(new Rectangle(280, 280, ENEMIESBLOCKER, ENEMIESBLOCKER));
            //level1EnemiesMoveArea.Add(new Rectangle(910, 280, ENEMIESBLOCKER, ENEMIESBLOCKER));

            //level2EnemiesMoveArea = new List<Rectangle>();

            //level2EnemiesMoveArea.Add(new Rectangle(210, 739, ENEMIESBLOCKER, ENEMIESBLOCKER));
            //level2EnemiesMoveArea.Add(new Rectangle(350, 459, ENEMIESBLOCKER, ENEMIESBLOCKER));
            //level2EnemiesMoveArea.Add(new Rectangle(420, 179, ENEMIESBLOCKER, ENEMIESBLOCKER));

            //enemiesMoveArea = new List<List<Rectangle>>();

            //enemiesMoveArea.Add(level1EnemiesMoveArea);
            //enemiesMoveArea.Add(level2EnemiesMoveArea);

            oldLevel = allCheckClass.Level;

            enemiesFrame = new List<Rectangle>();

            enemiesFrame.Add(new Rectangle(143, 34, ENEMYSTANDWIDTH, ENEMYHEIGHT));
            enemiesFrame.Add(new Rectangle(67, 87, ENEMYMOVEWIDTH, ENEMYHEIGHT));

            enemiesSpriteEffects = SpriteEffects.FlipHorizontally;

            currentPosition = enemiesPosition.ElementAt<Rectangle>(allCheckClass.Level);

            velocity = new Vector2(0);

            velocity.X = SPEED;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //foreach (Rectangle r in enemiesPosition.ElementAt<List<Rectangle>>(allCheckClass.Level))
            //{   

            //    spriteBatch.Draw(enemyTex, r, enemiesFrame.ElementAt<Rectangle>(enemyCurrentFrame), Color.White, 0f, new Vector2(0), enemiesSpriteEffects, 0f);
            //}

            spriteBatch.Draw(enemyTex, 
                currentPosition/*enemiesPosition.ElementAt<Rectangle>(allCheckClass.Level)*/, 
                enemiesFrame.ElementAt<Rectangle>(enemyCurrentFrame), 
                Color.White, 0f, 
                new Vector2(0), 
                enemiesSpriteEffects, 
                0f);

            //spriteBatch.DrawRectangle(enemiesMoveArea.ElementAt<Rectangle>(allCheckClass.Level), Color.Yellow);

            //spriteBatch.DrawRectangle(currentPosition, Color.Blue);

            //foreach (Rectangle r in enemiesMoveArea.ElementAt<List<Rectangle>>(allCheckClass.Level))
            //{
            //    spriteBatch.DrawRectangle(r, Color.Yellow);
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            


            if (oldLevel != allCheckClass.Level)
            {
                currentPosition = enemiesPosition.ElementAt<Rectangle>(allCheckClass.Level);
                oldLevel = allCheckClass.Level;
            }
            
            //for (int i = 0; i < enemiesPosition[allCheckClass.Level].Count(); i++)
            //{
            //Rectangle position = enemiesPosition[allCheckClass.Level][0];

            //Sides collisionSides = position.CheckCollisions(enemiesMoveArea.ElementAt<List<Rectangle>>(allCheckClass.Level));

            //if ((collisionSides & Sides.RIGHT) == Sides.RIGHT)
            //{
            //    velocity.X = SPEED;
            //    enemiesSpriteEffects = SpriteEffects.FlipHorizontally;
            //}

            //if ((collisionSides & Sides.LEFT) == Sides.LEFT)
            //{
            //    velocity.X = -SPEED;
            //    enemiesSpriteEffects = SpriteEffects.FlipHorizontally;
            //}

            //position.X += (int)velocity.X;

            //enemiesPosition[allCheckClass.Level][0] = position;

            //}

            if (currentPosition.X <= enemiesMoveArea.ElementAt<Rectangle>(allCheckClass.Level).X)
            {
                velocity.X = SPEED;
                enemiesSpriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if (currentPosition.X + currentPosition.Width >= enemiesMoveArea.ElementAt<Rectangle>(allCheckClass.Level).X + enemiesMoveArea.ElementAt<Rectangle>(allCheckClass.Level).Width)
            {
                velocity.X = -SPEED;
                enemiesSpriteEffects = SpriteEffects.None;
            }

            currentFrameCount++;
            if (currentFrameCount > ENEMYFRAMEDELAY)
            {
                currentFrameCount = 0;
                enemyCurrentFrame++;
                if (enemyCurrentFrame > 1)
                {
                    enemyCurrentFrame = 0;
                }
            }

            currentPosition.X += (int)velocity.X;

            base.Update(gameTime);
        }
    }
}
