using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using C3.XNA;

namespace JCaiFinalProject
{
    public class Background : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        //Texture2D backgroundTexLevel1;
        //Texture2D backgroundTexLevel2;
        //int currentTex = 0;

        GameProject g;
        AllCheckClass allCheckClass;

        List<Rectangle> level1GroundList;
        List<Rectangle> level2GroundList;

        //int currentGroundList = 0;

        List<List<Rectangle>> groundList;

        List<Texture2D> backgroundTex;

        public List<List<Rectangle>> GroundList { get { return groundList; } }
        
        public Background(Game game, SpriteBatch spriteBatch, AllCheckClass allCheckClass) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.allCheckClass = allCheckClass;

            g = (GameProject)game;
            

            //backgroundTexLevel1 = g.Content.Load<Texture2D>("Background/maplevel1");
            //backgroundTexLevel2 = g.Content.Load<Texture2D>("Background/maplevel2");

            backgroundTex = new List<Texture2D>();

            backgroundTex.Add(g.Content.Load<Texture2D>("Background/maplevel1"));
            backgroundTex.Add(g.Content.Load<Texture2D>("Background/maplevel2"));
        
            //currentTex = backgroundTexLevel1;

            level1GroundList = new List<Rectangle>();

            level1GroundList.Add(new Rectangle(0,210,350,70));
            level1GroundList.Add(new Rectangle(350,350,560, 70));
            level1GroundList.Add(new Rectangle(0,490,280, 70));
            level1GroundList.Add(new Rectangle(1050,490,210, 70));
            level1GroundList.Add(new Rectangle(0,770,910, 70));
            level1GroundList.Add(new Rectangle(840,630,280, 70));

            level2GroundList = new List<Rectangle>();

            level2GroundList.Add(new Rectangle(1120,140,140,70));
            level2GroundList.Add(new Rectangle(420,210,560, 70));
            level2GroundList.Add(new Rectangle(0,350,280, 70));
            level2GroundList.Add(new Rectangle(350,490,350, 70));
            level2GroundList.Add(new Rectangle(770,630,210, 70));
            level2GroundList.Add(new Rectangle(0,770,490, 70));
            level2GroundList.Add(new Rectangle(1050,770,210, 70));

            //currentGroundList = level1GroundList;

            groundList = new List<List<Rectangle>>();

            groundList.Add(level1GroundList);
            groundList.Add(level2GroundList);

        }

        public override void Update(GameTime gameTime)
        {
            //KeyboardState ks = Keyboard.GetState();

            //if (ks.IsKeyDown(Keys.Space))
            //{
            //    level = 1;
            //}

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {            
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(backgroundTex.ElementAt<Texture2D>(allCheckClass.Level), new Rectangle(0, 0, 1260, 840),null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0f);

            //foreach (Rectangle r in groundList.ElementAt<List<Rectangle>>(allCheckClass.Level))
            //{
            //    spriteBatch.DrawRectangle(r, Color.Red);
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
