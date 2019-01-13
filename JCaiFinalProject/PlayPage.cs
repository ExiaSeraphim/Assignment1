using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using JCaiFinalProject;

namespace JCaiFinalProject
{
    public class PlayPage : ScenesComponent
    {
        private SpriteBatch spriteBatch;
        AllCheckClass allCheckClass = new AllCheckClass();

        Player player;

        public PlayPage(Game game) : base(game)
        {
            GameProject g = (GameProject)game;

            this.spriteBatch = g.spriteBatch;

            Background background = new Background(game, spriteBatch, allCheckClass);
            Components.Add(background);

            HUD hud = new HUD(game, spriteBatch, allCheckClass);
            Components.Add(hud);

            ItemKeys itemKeys = new ItemKeys(game, spriteBatch, allCheckClass);
            Components.Add(itemKeys);

            Doors doors = new Doors(game, spriteBatch, allCheckClass);
            Components.Add(doors);

            Enemies enemies = new Enemies(game, spriteBatch, allCheckClass);
            Components.Add(enemies);

            player = new Player(game, spriteBatch, background, allCheckClass, itemKeys, doors, enemies);
            Components.Add(player);

            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
