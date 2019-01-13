using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCaiFinalProject
{
    public class MenuPage : ScenesComponent
    {
        public MenuComponent Menu { get; set; }

        private SpriteBatch spriteBatch;

        string[] menuItems = { "Start",
                               "Quit"};
        GameProject g;

        Texture2D scrollingBackground;
        Texture2D scrollingPlayerTex;

        List<Rectangle> scrollingPlayerFrame;
        Rectangle scrollingPlayerPosition;

        const float SCALE = 0.70f;
        const int WALKFRAMEWIDTH = 72;
        const int WALKFRAMEHEIGHT = 97;

        const int FRAMEDELAYMAXCOUNT = 3;
        int currentFrameDelayCount = 0;
        const int FIRSTWALKFRAME = 0;
        const int WALKFRAMES = 10;
        private int currentFrame = FIRSTWALKFRAME;

        Vector2 speed = new Vector2(1, 0);
        Vector2 position1 = new Vector2(0, 0);
        Vector2 position2;
        Rectangle scrollingBackgroundSize;

        string[] helpInfo = {"<- : To move left" ,
                                "-> : To move right",
                                "SPACE : To jump",
                                "M : To mute",
                                "R : To restart",
                                "ESC: To back menu",
                                "H : To Help" };

        List<Vector2> helpInfoPos;

        SpriteFont infoFont;

        public MenuPage(Game game) : base(game)
        {
            g = (GameProject)game;

            this.spriteBatch = g.spriteBatch;
            SpriteFont noneSelectedFont = g.Content.Load<SpriteFont>("Fonts/menuFont");
            SpriteFont selectedFont = g.Content.Load<SpriteFont>("Fonts/selectedFont");

            this.scrollingBackground = g.Content.Load<Texture2D>("Background/scrollingBackground");
            position2 = new Vector2(position1.X + scrollingBackground.Width, position1.Y);
            scrollingBackgroundSize = new Rectangle(0, 0, scrollingBackground.Width, scrollingBackground.Height);

            this.scrollingPlayerTex = g.Content.Load<Texture2D>("Player/p3_spritesheet");

            scrollingPlayerPosition = new Rectangle(350, 770 - (int)(SCALE * WALKFRAMEHEIGHT), (int)(SCALE * WALKFRAMEWIDTH), (int)(SCALE * WALKFRAMEHEIGHT));

            scrollingPlayerFrame = new List<Rectangle>();

            scrollingPlayerFrame.Add(new Rectangle(0, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(73, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(146, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(0, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(73, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(146, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(219, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(292, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(219, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(365, 0, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));
            scrollingPlayerFrame.Add(new Rectangle(292, 98, WALKFRAMEWIDTH, WALKFRAMEHEIGHT));

            Menu = new MenuComponent(game, spriteBatch, noneSelectedFont, selectedFont, menuItems);
            this.Components.Add(Menu);

            this.infoFont = g.Content.Load<SpriteFont>("Fonts/selectedFont");

            helpInfoPos = new List<Vector2>();

            helpInfoPos.Add(new Vector2(70, 210));
            helpInfoPos.Add(new Vector2(70, 250));
            helpInfoPos.Add(new Vector2(70, 290));
            helpInfoPos.Add(new Vector2(70, 330));
            helpInfoPos.Add(new Vector2(70, 370));
            helpInfoPos.Add(new Vector2(70, 410));
            helpInfoPos.Add(new Vector2(70, 450));
        }

        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;

            if (position1.X < -scrollingBackground.Width)
            {
                position1.X = position2.X + scrollingBackground.Width;
            }
            if (position2.X < -scrollingBackground.Width)
            {
                position2.X = position1.X + scrollingBackground.Width;
            }

            currentFrameDelayCount++;
            if (currentFrameDelayCount > FRAMEDELAYMAXCOUNT)
            {
                currentFrameDelayCount = 0;
                currentFrame++;  //advance to the next frame
            }
            if (currentFrame > WALKFRAMES)
                currentFrame = FIRSTWALKFRAME;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(scrollingBackground, position1, scrollingBackgroundSize, Color.White);
            spriteBatch.Draw(scrollingBackground, position2, scrollingBackgroundSize, Color.White);
            spriteBatch.Draw(scrollingPlayerTex, scrollingPlayerPosition, scrollingPlayerFrame.ElementAt<Rectangle>(currentFrame), Color.White);

            if (g.ShowHelp)
            {
                for (int i = 0; i < helpInfo.Length; i++)
                {
                    spriteBatch.DrawString(infoFont, helpInfo[i], helpInfoPos[i], Color.Violet);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
