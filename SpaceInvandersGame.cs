using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;

namespace JacobClaytonProject2
{
    public class SpaceInvadersGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        List<Enemy> enemies;
        List<Bullet> playerBullets;
        Texture2D playerTexture, enemyTexture, bulletTexture;
        SoundEffect shootSound, explosionSound, gameOverSound;
        SpriteFont font;
        int score;
        bool gameOver;

        public SpaceInvadersGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            player = new Player(playerTexture, bulletTexture, playerBullets, shootSound, GraphicsDevice);
            enemies = new List<Enemy>();
            playerBullets = new List<Bullet>();
            score = 0;
            gameOver = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("player");
            enemyTexture = Content.Load<Texture2D>("enemy");
            bulletTexture = Content.Load<Texture2D>("bullet");
            shootSound = Content.Load<SoundEffect>("shoot");
            explosionSound = Content.Load<SoundEffect>("explosion");
            gameOverSound = Content.Load<SoundEffect>("gameover");
            font = Content.Load<SpriteFont>("font");

            
            player = new Player(playerTexture, bulletTexture, playerBullets, shootSound, GraphicsDevice);

            InitializeEnemies();

            base.LoadContent();
        }

        private void InitializeEnemies()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Enemy enemy = new Enemy(enemyTexture, new Vector2(j * 50, i * 50 + 50));
                    enemies.Add(enemy);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (!gameOver)
            {
                player.Update(gameTime);

                foreach (Enemy enemy in enemies)
                {
                    enemy.Update(gameTime);
                }

                foreach (Bullet bullet in playerBullets)
                {
                    bullet.Update(gameTime);
                }

                CheckCollisions();

                if (enemies.Count == 0)
                {
                    gameOver = true;
                    gameOverSound.Play();
                }
            }

            base.Update(gameTime);
        }

        private void CheckCollisions()
        {
            foreach (Bullet bullet in playerBullets.ToList())
            {
                foreach (Enemy enemy in enemies.ToList())
                {
                    if (bullet.Bounds.Intersects(enemy.Bounds))
                    {
                        explosionSound.Play();
                        score += 10;
                        playerBullets.Remove(bullet);
                        enemies.Remove(enemy);
                        break;
                    }
                }
            }

            foreach (Enemy enemy in enemies)
            {
                if (player.Bounds.Intersects(enemy.Bounds))
                {
                    gameOver = true;
                    gameOverSound.Play();
                    break;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            player.Draw(spriteBatch);

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (Bullet bullet in playerBullets)
            {
                bullet.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(10, 10), Color.White);
            if (gameOver)
            {
                spriteBatch.DrawString(font, "Game Over", new Vector2(300, 300), Color.Red);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}