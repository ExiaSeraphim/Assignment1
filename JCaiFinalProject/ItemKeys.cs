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
    public class ItemKeys : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D keyTex;

        GameProject g;
        AllCheckClass allCheckClass;

        //Rectangle level1KeyLocation;
        //Rectangle level2KeyLocation;

        List<Rectangle> keyLocation;
        public List<Rectangle> KeyLocation { get { return keyLocation; } }
        //Rectangle currentKeyLocation;

        //bool isPicked = false;
        //public bool IsPicked { get { return isPicked; } }

        public ItemKeys(Game game, SpriteBatch spriteBatch, AllCheckClass allCheckClass) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.allCheckClass = allCheckClass;

            g = (GameProject)game;

            keyTex = this.g.Content.Load<Texture2D>("Items/keyRed");

            //level1KeyLocation = new Rectangle(70, 420, 70, 70);
            //level2KeyLocation = new Rectangle(1190, 70, 70, 70);
            keyLocation = new List<Rectangle>();

            keyLocation.Add(new Rectangle(70, 420, 70, 70));
            keyLocation.Add(new Rectangle(1190, 70, 70, 70));

            //currentKeyLocation = level1KeyLocation;

            //isPicked = false;

        }

        public override void Draw(GameTime gameTime)
        {
            //if (!isPicked)
            //{
            //    if (!background.IsLevel1)
            //    {
            //        currentKeyLocation = level2KeyLocation;
            //    }
            //    spriteBatch.Begin();
            //    spriteBatch.Draw(keyTex, currentKeyLocation, Color.White);
            //    spriteBatch.End();
            //}
            //base.Draw(gameTime);

            if (!allCheckClass.IsPicked)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack);

                spriteBatch.Draw(keyTex, keyLocation.ElementAt<Rectangle>(allCheckClass.Level), Color.White);
                //spriteBatch.DrawRectangle(keyLocation.ElementAt<Rectangle>(allCheckClass.Level), Color.Red);

                spriteBatch.End();
            }

            base.Draw(gameTime);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
