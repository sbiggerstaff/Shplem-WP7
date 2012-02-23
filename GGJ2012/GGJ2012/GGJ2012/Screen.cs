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
    public class Screen
    {
        protected ContentManager _content;
        protected Texture2D _background = null;
        protected Vector2 _offset;
        protected Game1 _game;

        public Screen(Game1 game, ContentManager content)
        {
            _game = game;
            _content = content;
        }
        public Screen(Game1 game, ContentManager content, string background)
        {
            _game = game;
            _background = content.Load<Texture2D>(background);
            _content = content;
        }

        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_background != null)
                spriteBatch.Draw(_background, _offset, Color.White);
        }

        public virtual void OnEvent(Button sender)
        {
        }

        public Texture2D Background
        {
            get { return _background; }
            set { _background = value; }
        }

        /// <summary>
        /// Get and set offset
        /// </summary>
        public Vector2 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public Game1 Game
        {
            get { return _game; }
            set { _game = value; }
        }

    }
}
