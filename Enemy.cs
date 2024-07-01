using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacobClaytonProject2
{
    public class Enemy
    {
        Texture2D texture;
        Vector2 position;
        float speed = 1;

        public Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        public Enemy(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            position.X += speed;

            if (position.X <= 0 || position.X + texture.Width >= 800) // Assuming screen width is 800
            {
                speed = -speed;
                position.Y += 20;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
