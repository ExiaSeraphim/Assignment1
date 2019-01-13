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
    public class Doors : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D doorTex;

        GameProject g;
        AllCheckClass allCheckClass;

        const int DOORWIDTHANDHEIGHT = 70;

        List<Rectangle> doorTop;
        List<Rectangle> doorMid;

        List<Rectangle> doorTopLocation;
        List<Rectangle> doorMidLocation;

        /*  <SubTexture height="70" width="70" y="432" x="648" name="door_closedMid.png"/>

            <SubTexture height="70" width="70" y="360" x="648" name="door_closedTop.png"/>

            <SubTexture height="70" width="70" y="288" x="648" name="door_openMid.png"/>

            <SubTexture height="70" width="70" y="216" x="648" name="door_openTop.png"/>*/

        //bool isOpen = false;
        //public bool IsOpen { get { return isOpen; } }

        int doorFrame = 0;

        List<Rectangle> door;

        public List<Rectangle> Door { get { return door; } }

        public Doors(Game game, SpriteBatch spriteBatch, AllCheckClass allCheckClass) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.allCheckClass = allCheckClass;

            g = (GameProject)game;

            doorTex = this.g.Content.Load<Texture2D>("Items/tiles_spritesheet");

            doorTop = new List<Rectangle>();

            doorTop.Add(new Rectangle(648, 360, DOORWIDTHANDHEIGHT, DOORWIDTHANDHEIGHT));
            doorTop.Add(new Rectangle(648, 216, DOORWIDTHANDHEIGHT, DOORWIDTHANDHEIGHT));

            doorMid = new List<Rectangle>();

            doorMid.Add(new Rectangle(648, 432, DOORWIDTHANDHEIGHT, DOORWIDTHANDHEIGHT));
            doorMid.Add(new Rectangle(648, 288, DOORWIDTHANDHEIGHT, DOORWIDTHANDHEIGHT));

            doorTopLocation = new List<Rectangle>();

            doorTopLocation.Add(new Rectangle(140, 70,DOORWIDTHANDHEIGHT, DOORWIDTHANDHEIGHT));
            doorTopLocation.Add(new Rectangle(70, 630, DOORWIDTHANDHEIGHT, DOORWIDTHANDHEIGHT));

            doorMidLocation = new List<Rectangle>();

            doorMidLocation.Add(new Rectangle(140, 140, DOORWIDTHANDHEIGHT, DOORWIDTHANDHEIGHT));
            doorMidLocation.Add(new Rectangle(70, 700, DOORWIDTHANDHEIGHT, DOORWIDTHANDHEIGHT));

            door = new List<Rectangle>();

            door.Add(new Rectangle(140, 100, DOORWIDTHANDHEIGHT, (DOORWIDTHANDHEIGHT + 40)));
            door.Add(new Rectangle(70, 660, DOORWIDTHANDHEIGHT, (DOORWIDTHANDHEIGHT + 40)));

        }

        public override void Draw(GameTime gameTime)
        {
            if (allCheckClass.IsOpen)
            {
                doorFrame = 1;
            }
            else if (!allCheckClass.IsOpen)
            {
                doorFrame = 0;
            }
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(doorTex, doorTopLocation.ElementAt<Rectangle>(allCheckClass.Level), doorTop.ElementAt<Rectangle>(doorFrame), Color.White, 0f, new Vector2(0), SpriteEffects.None,0f);
            spriteBatch.Draw(doorTex, doorMidLocation.ElementAt<Rectangle>(allCheckClass.Level), doorMid.ElementAt<Rectangle>(doorFrame), Color.White, 0f, new Vector2(0), SpriteEffects.None, 0f);

            //spriteBatch.DrawRectangle(door.ElementAt<Rectangle>(allCheckClass.Level), Color.Red);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
