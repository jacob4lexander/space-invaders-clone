using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacobClaytonProject2
{
    public class Bullet
    {
        Texture2D texture;
        Vector2 position;
        float speed = 5;

        List<Bullet> playerBullets; // Added playerBullets list

        public Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        public Bullet(Texture2D texture, Vector2 position, List<Bullet> playerBullets) // Modified constructor to accept playerBullets
        {
            this.texture = texture;
            this.position = position;
            this.playerBullets = playerBullets; // Assign playerBullets
        }

        public void Update(GameTime gameTime)
        {
            position.Y -= speed;

            if (position.Y < 0)
            {
                // Remove bullet if it goes off-screen
                playerBullets.Remove(this);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
