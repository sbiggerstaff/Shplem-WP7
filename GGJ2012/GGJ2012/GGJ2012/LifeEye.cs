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
    /// Life eye
    /// </summary>
    public class LifeEye : Entity
    {
        public bool _open = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="asset"></param>
        public LifeEye(ContentManager content, string asset)
            : base(content, asset)
        {
            Animations = 2;
            Frames = 12;
            Animating = false;
            Scale = 0.25f;
        }


        /// <summary>
        /// End animation
        /// </summary>
        public override void AnimationEnded()
        {
            if (_open)
            {
                CurAnimation = 1;
                CurFrame = 0;
                _animate = false;
            }
            else
            {
                _animate = false;
              //  CurFrame = 11;
            }

          //  base.AnimationEnded();
        }


        /// <summary>
        /// Close eye
        /// </summary>
        public void Close()
        {
            _open = false;
            CurAnimation = 0;
            CurFrame = 0;
            _animate = true;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_open)
            {
                Random rnd = new Random();
                if (rnd.Next(10) > 9)
                {
                    _animate = true;
                    CurAnimation = 2;
                    CurFrame = 0;
                }
            }

            base.Update(gameTime, spriteBatch);
        }


        #region Properties


        /// <summary>
        /// Get and set if open
        /// </summary>
        public bool Open
        {
            get { return _open; }
            set
            {
                _open = value;

                if (_open)
                {
                    _curFrame = 0;
                    _curAnim = 0;
                }
                else
                {
                    _curAnim = 0;
                    _curFrame = 11;
                }
            }
        }

        #endregion
    }
}
