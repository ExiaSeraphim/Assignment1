using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCaiFinalProject
{
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont noneSelectedFont;
        private SpriteFont selectedFont;
        private List<string> menuItems;
        private Vector2 position;
        private Color wordColor = Color.SkyBlue;
        public int SelectedIndex { get; set; }

        private KeyboardState oldKeyState;

        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont noneSelectedFont,
            SpriteFont selectedFont,
            string[] menu) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.noneSelectedFont = noneSelectedFont;
            this.selectedFont = selectedFont;
            menuItems = menu.ToList();
            position = new Vector2(600, 400);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Down) && oldKeyState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;

                if (SelectedIndex == menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }

            if (keyState.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;

                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }

            oldKeyState = keyState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 temPos = position;

            spriteBatch.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(selectedFont, menuItems[i], temPos, wordColor);
                    temPos.Y += selectedFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(noneSelectedFont, menuItems[i], temPos, wordColor);
                    temPos.Y += noneSelectedFont.LineSpacing;
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
