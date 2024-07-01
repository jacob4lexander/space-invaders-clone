using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace JacobClaytonProject2
{
    public class Player
    {
        Texture2D texture;
        Vector2 position;
        float speed = 5;
        float shootCooldown = 0.5f;
        float shootTimer = 0f;

        Texture2D bulletTexture;
        List<Bullet> playerBullets; // Added playerBullets list
        SoundEffect shootSound; // Added shootSound
        private GraphicsDevice graphicsDevice;
        public Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        public Player(Texture2D texture, Texture2D bulletTexture, List<Bullet> playerBullets, SoundEffect shootSound, GraphicsDevice graphicsDevice)
        {
            this.texture = texture;
            this.bulletTexture = bulletTexture;
            this.playerBullets = playerBullets; // Assign playerBullets
            this.shootSound = shootSound; // Assign shootSound
            position = new Vector2(350, 700); // Initial player position
            this.graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
                position.X -= speed;
            if (keyboardState.IsKeyDown(Keys.Right))
                position.X += speed;
            if (keyboardState.IsKeyDown(Keys.Up))
                position.Y -= speed;
            if (keyboardState.IsKeyDown(Keys.Down))
                position.Y += speed;

            position.X = MathHelper.Clamp(position.X, 0, graphicsDevice.Viewport.Width - texture.Width); // Adjusted for screen width
            position.Y = MathHelper.Clamp(position.Y, 0, graphicsDevice.Viewport.Height - texture.Height); // Adjusted for screen height

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (shootTimer >= shootCooldown)
                {
                    Shoot();
                    shootTimer = 0f;
                }
            }
        }

        private void Shoot()
        {
            // Create a new bullet instance at the player's position
            Bullet bullet = new Bullet(bulletTexture, new Vector2(position.X + texture.Width / 2 - bulletTexture.Width / 2, position.Y), playerBullets);
            playerBullets.Add(bullet);
            shootSound.Play();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
