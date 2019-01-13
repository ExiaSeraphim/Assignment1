﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCaiFinalProject
{
    public abstract class ScenesComponent : DrawableGameComponent
    {
        public List<GameComponent> Components { get; set; }

        public virtual void show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        public virtual void hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        public ScenesComponent(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide();
        }

        public override void Update(GameTime gameTime)
        {

            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
