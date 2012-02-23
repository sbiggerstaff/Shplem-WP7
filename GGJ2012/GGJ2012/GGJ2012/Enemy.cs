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
    /// Enemy class
    /// </summary>
    public class Enemy : Button
    {

        /// <summary>
        /// Enemy type
        /// </summary>
        public enum EnemyType
        {
            SPIKEY,
            BURSTING,
            STICKY
        }

        protected EnemyType _eType;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="asset"></param>
        public Enemy(ContentManager content, string asset)
            : base(content, asset, Vector2.Zero, 1)
        {
            _eType = EnemyType.SPIKEY;
        }


        #region Properties

        /// <summary>
        /// Get and set enemy type
        /// </summary>
        public EnemyType EType
        {
            get { return _eType; }
            set { _eType = value; }
        }

        #endregion
    }
}
