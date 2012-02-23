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
    /// Gameplay screen
    /// </summary>
    public class GameScreen : Screen
    {
        protected Level _level;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        public GameScreen(Game1 game, ContentManager content, string level)
            : base(game, content)
        {
           // _level = Level.LoadLevel(this, content, level);
        }

      /*  /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="background"></param>
        public GameScreen(ContentManager content, string background)
            : base(content, background)
        {
            _level = null;
        }*/


        /// <summary>
        /// Override on event
        /// </summary>
        /// <param name="sender"></param>
        public override void OnEvent(Button sender)
        {
            if (sender.PrivateData == 1)
            {
                //_level.MainLayer.LoadEntity("sign_howto");
            }
            
            base.OnEvent(sender);
        }

        /// <summary>
        /// Update screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Update(gameTime, spriteBatch);

            if (_level != null)
               _level.Draw(gameTime, spriteBatch);
        }



        #region Properties


        /// <summary>
        /// Get and set level
        /// </summary>
        public Level CurLevel
        {
            get { return _level; }
            set { _level = value; _level.Screen = this; }
        }

        #endregion

    }
}
