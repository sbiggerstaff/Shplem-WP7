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
    /// Static entity
    /// </summary>
    public class Static : Entity
    {
        public enum StaticType
        {
            NORMAL,
            BOUNCY,
            STICKY,
            MULTI
        }
        protected StaticType _sType;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="asset"></param>
        public Static(ContentManager content, String asset)
            : base(content, asset)
        {
            _sType = StaticType.NORMAL;
        }


        /// <summary>
        /// Collide with entity
        /// </summary>
        /// <param name="ent"></param>
        public override void OnCollision(Entity ent)
        {
            if (ent is Player)
            {
                Player plr = (Player)ent;

                switch (_sType)
                {
                    case StaticType.NORMAL:
                        {
                            Vector2 axis = plr.Position - Position;
                            axis.Normalize();
                            plr.Velocity = axis * plr.Velocity.Length();

                            SoundEffect normalSound1;
                            normalSound1 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/NormalTerrain/Coll_L_1");
                            SoundEffect normalSound2;
                            normalSound2 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/NormalTerrain/Coll_L_2");
                            SoundEffect normalSound3;
                            normalSound3 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/NormalTerrain/Coll_L_3");
                            SoundEffect normalSound4;
                            normalSound4 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/NormalTerrain/Coll_L_4");
                            SoundEffect normalSound5;
                            normalSound5 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/NormalTerrain/Coll_L_5");
                            SoundEffect normalSound6;
                            normalSound6 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/NormalTerrain/Coll_L_6");

                            Random rand = new Random();
                            int randnum = rand.Next(6);

                            switch (randnum)
                            {
                                case (0):
                                    normalSound1.Play();
                                    break;

                                case (1):
                                    normalSound2.Play();
                                    break;

                                case (2):
                                    normalSound3.Play();
                                    break;

                                case (3):
                                    normalSound4.Play();
                                    break;

                                case (4):
                                    normalSound5.Play();
                                    break;

                                case (5):
                                    normalSound6.Play();
                                    break;
                            }
                            break;
                        }

                    case StaticType.BOUNCY:
                        {
                            Vector2 axis = plr.Position - Position;
                            axis.Normalize();
                            plr.Velocity = axis * plr.Velocity.Length() * 2.0f;

                            SoundEffect bounceySound1;
                            bounceySound1 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/ThrowTerrain/Throw_L_1");
                            SoundEffect bounceySound2;
                            bounceySound2 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/ThrowTerrain/Throw_L_1");

                            Random rand = new Random();
                            int randnum = rand.Next(2);

                            switch (randnum)
                            {
                                case (0):
                                    bounceySound1.Play();
                                    break;

                                case (1):
                                    bounceySound2.Play();
                                    break;


                            }

                            break;
                        }

                    case StaticType.STICKY:
                        {
                            Vector2 axis = plr.Position - Position; ;
                            plr.Position = Position + axis * 1.01f;
                            plr.Velocity = Vector2.Zero;
                            plr.ApplyGravity = false;


                            SoundEffect stickySound1;
                            stickySound1 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/StickyTerrain/Stick_L_1");
                            SoundEffect stickySound2;
                            stickySound2 = _content.Load<SoundEffect>("Audio/Environment/TerrainCollisions/StickyTerrain/Stick_L_1");

                            Random rand = new Random();
                            int randnum = rand.Next(2);

                            switch (randnum)
                            {
                                case (0):
                                    stickySound1.Play();
                                    break;

                                case (1):
                                    stickySound2.Play();
                                    break;


                            }
                            break;


                        }
                }
            }

            base.OnCollision(ent);
        }


        #region Properties


        /// <summary>
        /// Get and set static type
        /// </summary>
        public StaticType SType
        {
            get { return _sType; }
            set { _sType = value; }
        }

        #endregion
    }
}
