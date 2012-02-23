using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GGJ2012
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        // Core stuff
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        // Current level and screens
        Level level;
        string curLevel;
        List<Screen> screens;
        int curScreenIndex = 0;
        Screen curScreen = null;

        // Player
        Player player = null;
        bool dragging;
        float maxVelocity = 100;

        // Lives
        LifeEye[] lifes;
        int lifeCount = 3;


        /// <summary>
        /// Constructor
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Initialize prototype
        /// </summary>
        protected override void Initialize()
        {
            base.IsMouseVisible = true;
            dragging = false;

            // Screens
            screens = new List<Screen>();
            screens.Add(new MainMenu(this, Content, "menubackground"));
            screens.Add(new GameScreen(this, Content, "level1.txt"));
            screens.Add(new GameScreen(this, Content, "level2.txt"));
            screens.Add(new GameScreen(this, Content, "level3.txt"));

            NextScreen();

            base.Initialize();
        }

        /// <summary>
        /// Load shit
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("menufont");

            // Lifes
            lifes = new LifeEye[5];
            for (int i = 0; i < 5; i++)
            {
                lifes[i] = new LifeEye(Content, "UI/eye_HUD");
                if (i >= lifeCount)
                    lifes[i].Open = false;
            }
        }

        /// <summary>
        /// Unload shit
        /// </summary>
        protected override void UnloadContent()
        {
        }


        /// <summary>
        /// Move to next screen
        /// </summary>
        public void NextScreen()
        {
            if (curScreenIndex <= 3)
                curScreen = screens[curScreenIndex++];
            else
                curScreen = new GameOverScreen(this, Content, "menubackground");

            if (curScreen is GameScreen)
            {
                level = ((GameScreen)curScreen).CurLevel;
                player = level.MainPlayer;
            }
        }


        /// <summary>
        /// Update game
        /// </summary>
        /// <param name="gameTime">Game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);


            // Update game screens
            if (curScreen is GameScreen)
            {

                // Launch player
                if (!dragging && player.PointIn(mousePos) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    dragging = true;

                if (dragging)
                {
                    Vector2 diff = player.Position - (mousePos - Vector2.UnitY * level.Scroll);
                    float length = diff.Length();
                    if (length > maxVelocity)
                    {
                        diff.Normalize();
                        diff *= maxVelocity;
                        length = maxVelocity;
                    }
                    
                    player.Tail.Scale = length * 0.001f;
                    player.Tail.Rotation = MathHelper.ToDegrees((float)Math.Atan2(diff.Y, diff.X)) + 90;

                    if (Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        dragging = false;
                        player.Tail.Scale = 0;

                       // Vector2 diff = player.Position - (mousePos - Vector2.UnitY * level.Scroll);
                        

                        player.AddForce(diff * 40);
                        player.Active = true;
                        player.ApplyGravity = true;
                    }
                }

                // Collide with edges
                if (player.Position.X < 60 || player.Position.X > 740)
                    player.Velocity = new Vector2(-player.Velocity.X, player.Velocity.Y);

                // Screen limits
                if (player.Position.X <= 60)
                    player.Position = new Vector2(60, player.Position.Y);
                if (player.Position.X >= 740)
                    player.Position = new Vector2(740, player.Position.Y);
                if (level.Inverted)
                {
                    if (player.Position.Y <= 200)
                        level.Scroll = 100 - player.Position.Y;
                }
                else
                {
                    if (player.Position.Y <= 100)
                        level.Scroll = 100 - player.Position.Y;     // Scroll
                }
                if (!level.Inverted && player.Position.Y < -1000)
                    player.Position = new Vector2(player.Position.X, -1000);
                else if (level.Inverted && player.Position.Y > 800)
                    player.Position = new Vector2(player.Position.X, 800);

                if (level.Scroll > 1100)
                    level.Scroll = 1100;

                // Collide with soup
                if ((!level.Inverted && player.Position.Y > 800) || (level.Inverted && player.Position.Y < -1200))
                {
                    if (player.ChildCount >= 6)
                    {
                        NextScreen();
                        if (lifeCount < 5)
                            lifes[lifeCount++].Open = true;
                    }
                    else
                    {
                        // Die
                        if (lifeCount > 0)
                        {
                            lifes[--lifeCount].Close();
                           // level = Level.LoadLevel((GameScreen)curScreen, Content, level.Filename);
                            player = level.MainPlayer;
                        }
                        else
                        {
                            // Game over
                            curScreen = new GameOverScreen(this, Content, "menubackground");
                        }
                    }
                }

                // Update level
                level.Update(gameTime);

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw game
        /// </summary>
        /// <param name="gameTime">Game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // Current screen
            curScreen.Update(gameTime, spriteBatch);

            // Lifes
            if (curScreen is GameScreen)
                for (int i = 0; i < 5; i++)
                {
                    if (!level.Inverted)
                        lifes[i].Position = new Vector2(650 + i * 30, 550);
                    else
                        lifes[i].Position = new Vector2(650 + i * 30, -1050);

                    lifes[i].Offset = player.Offset;
                    lifes[i].Update(gameTime, spriteBatch);
                }


            if (curScreen is GameScreen)
            {
                Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - player.Offset;
             //   spriteBatch.DrawString(font, mousePos.ToString(), new Vector2(10, 10), Color.White);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
