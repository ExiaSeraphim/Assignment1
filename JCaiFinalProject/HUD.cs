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
    public class HUD : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D HUDTex;

        GameProject g;
        AllCheckClass allCheckClass;

        List<Rectangle> heartFullIcon;
        List<Rectangle> heartEmptyIcon;

        //        <SubTexture height = "45" width="53" y="47" x="0" name="hud_heartEmpty.png"/>

        //<SubTexture height = "45" width="53" y="94" x="0" name="hud_heartFull.png"/>

        const int HEARTWIDTH = 53;
        const int HEARTHEIGHT = 45;

        List<Rectangle> heartFrame;

        const int HEARTFULLFRAME = 0;
        const int HEARTEMPTYFRAME = 1;

        Rectangle keyIcon;

        //        <SubTexture height = "40" width="44" y="80" x="193" name="hud_keyRed.png"/>

        //<SubTexture height = "40" width="44" y="164" x="192" name="hud_keyRed_disabled.png"/>

        const int KEYICONWIDTH = 44;
        const int KEYICONHEIGHT = 40;


        List<Rectangle> keyFrame;

        const int KEYENABLEFRAME = 1;
        const int KEYDISABLEFRAME = 0;

        int currentKeyFrame;

        public HUD(Game game, SpriteBatch spriteBatch, AllCheckClass allCheckClass) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.allCheckClass = allCheckClass;

            g = (GameProject)game;

            HUDTex = this.g.Content.Load<Texture2D>("HUD/hud_spritesheet");

            keyIcon = new Rectangle(490, 30, KEYICONWIDTH, KEYICONHEIGHT);

            keyFrame = new List<Rectangle>();
            // key disable frame
            keyFrame.Add(new Rectangle(192, 164, KEYICONWIDTH, KEYICONHEIGHT));
            // key enable frame
            keyFrame.Add(new Rectangle(193, 80, KEYICONWIDTH, KEYICONHEIGHT));

            currentKeyFrame = KEYDISABLEFRAME;

            heartFullIcon = new List<Rectangle>();

            heartFullIcon.Add(new Rectangle(70, 25, HEARTWIDTH, HEARTHEIGHT));
            heartFullIcon.Add(new Rectangle(140, 25, HEARTWIDTH, HEARTHEIGHT));
            heartFullIcon.Add(new Rectangle(210, 25, HEARTWIDTH, HEARTHEIGHT));

            heartEmptyIcon = new List<Rectangle>();

            heartEmptyIcon.Add(new Rectangle(210, 25, HEARTWIDTH, HEARTHEIGHT));
            heartEmptyIcon.Add(new Rectangle(140, 25, HEARTWIDTH, HEARTHEIGHT));
            heartEmptyIcon.Add(new Rectangle(70, 25, HEARTWIDTH, HEARTHEIGHT));

            heartFrame = new List<Rectangle>();

            heartFrame.Add(new Rectangle(0, 94, HEARTWIDTH, HEARTHEIGHT));
            heartFrame.Add(new Rectangle(0, 47, HEARTWIDTH, HEARTHEIGHT));
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();            
            spriteBatch.Draw(HUDTex, keyIcon, keyFrame.ElementAt<Rectangle>(currentKeyFrame), Color.White);

            for (int i = 0; i < allCheckClass.LifeCount; i++)
            {
                spriteBatch.Draw(HUDTex, heartFullIcon.ElementAt<Rectangle>(i), heartFrame.ElementAt<Rectangle>(HEARTFULLFRAME), Color.White);
            }

            foreach (Rectangle r in heartEmptyIcon)
            {
                spriteBatch.Draw(HUDTex, r, heartFrame.ElementAt<Rectangle>(HEARTEMPTYFRAME), Color.White);
            }            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (allCheckClass.IsPicked)
            {
                currentKeyFrame = KEYENABLEFRAME;
            }
            else
            {
                currentKeyFrame = KEYDISABLEFRAME;
            }

            base.Update(gameTime);
        }
    }
}
