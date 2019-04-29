using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game2 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D dvdlogoorange;
        private Texture2D dvdlogoyellow;
        private Texture2D dvdlogogreen;
        Texture2D dvdlogoblue;
        Texture2D dvdlogored;
        Texture2D dvdlogo;

        Color[] colors = { Color.Blue, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Purple };

        float targetX = 400;
        float targetY;
        Vector2 scale;

        Vector2 pos = new Vector2(0, 0);
        Vector2 vel = new Vector2(250, 250);
        private int prevcolor;
        private bool hitwall;
        private bool firsttime = true;

        Dictionary<int, string> openWith =
        new Dictionary<int, string>();

        public Game2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            dvdlogo = this.Content.Load<Texture2D>("dvdlogo");

            scale = new Vector2(targetX / (float)dvdlogo.Width, targetX / (float)dvdlogo.Width);
            targetY = dvdlogo.Height * scale.Y;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            dvdlogogreen = this.Content.Load<Texture2D>("dvdlogogreen");

            scale = new Vector2(targetX / (float)dvdlogogreen.Width, targetX / (float)dvdlogogreen.Width);
            targetY = dvdlogogreen.Height * scale.Y;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            dvdlogoyellow = this.Content.Load<Texture2D>("dvdlogoyellow");

            scale = new Vector2(targetX / (float)dvdlogoyellow.Width, targetX / (float)dvdlogoyellow.Width);
            targetY = dvdlogoyellow.Height * scale.Y;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            dvdlogoorange = this.Content.Load<Texture2D>("dvdlogoorange");

            scale = new Vector2(targetX / (float)dvdlogoorange.Width, targetX / (float)dvdlogoorange.Width);
            targetY = dvdlogoorange.Height * scale.Y;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            dvdlogoblue = this.Content.Load<Texture2D>("dvdlogoblue");

            scale = new Vector2(targetX / (float)dvdlogoblue.Width, targetX / (float)dvdlogoblue.Width);
            targetY = dvdlogoblue.Height * scale.Y;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            dvdlogored = this.Content.Load<Texture2D>("dvdlogored");

            scale = new Vector2(targetX / (float)dvdlogored.Width, targetX / (float)dvdlogored.Width);
            targetY = dvdlogored.Height * scale.Y;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            pos += vel * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (pos.X <= 0 && vel.X < 0)
            {
                vel.X *= -1;
                hitwall = true;
            }
            else if (pos.Y <= 0 && vel.Y <= 0)
            {
                vel.Y *= -1;
                hitwall = true;
            }
            else if (pos.X >= graphics.GraphicsDevice.Viewport.Width - targetX && vel.X > 0)
            {
                vel.X *= -1;
                hitwall = true;
            }
            else if (pos.Y >= graphics.GraphicsDevice.Viewport.Height - targetY && vel.Y > 0)
            {
                vel.Y *= -1;
                hitwall = true;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            switch (prevcolor)
            {
                case 0: //blue
                    spriteBatch.Draw(dvdlogoyellow, position: pos, scale: scale);
                    break;
                case 1: //red
                    spriteBatch.Draw(dvdlogogreen, position: pos, scale: scale);
                    break;
                case 2://orange
                    spriteBatch.Draw(dvdlogoblue, position: pos, scale: scale);
                    break;
                case 3://yellow
                    spriteBatch.Draw(dvdlogo, position: pos, scale: scale);
                    break;
                case 4://green
                    spriteBatch.Draw(dvdlogored, position: pos, scale: scale);
                    break;
                case 5://purple
                    spriteBatch.Draw(dvdlogoyellow, position: pos, scale: scale);
                    break;

            }



            if (firsttime)
            {
                GraphicsDevice.Clear(Color.Red);

                prevcolor = 1;

                firsttime = false;
            }

            GraphicsDevice.Clear(colors[prevcolor]);

            if (hitwall)
            {
                var rnd = random_except_list(5, new int[] { prevcolor });

                GraphicsDevice.Clear(colors[rnd]);

                prevcolor = rnd;


                hitwall = false;
            }



            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static int random_except_list(int n, int[] x)
        {
            Random r = new Random();
            int result = r.Next(n - x.Length);

            for (int i = 0; i < x.Length; i++)
            {
                if (result < x[i])
                    return result;
                result++;
            }
            return result;
        }
    }
}
