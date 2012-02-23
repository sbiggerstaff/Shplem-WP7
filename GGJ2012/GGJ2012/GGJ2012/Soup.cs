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
    /// Soup
    /// </summary>
    public class Soup : Entity
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="asset"></param>
        public Soup(ContentManager content, string asset)
            : base(content, asset)
        {
            _frameDelay = 3;
        }


        /// <summary>
        /// Go to next animation and wrap
        /// </summary>
        public override void AnimationEnded()
        {
            _curAnim++;
            if (_curAnim > 2)
                _curAnim = 0;

            base.AnimationEnded();
        }
    }
}
