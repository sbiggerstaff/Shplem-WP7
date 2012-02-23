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
    /// Dynamic entity
    /// </summary>
    public class Dynamic : Entity
    {
        protected Vector2 _acc, _vel, _grav, _frc;
        protected float _mass, _drag, _time;
        protected bool _active, _applyGrav;
        protected List<Dynamic> _child;
        protected Texture2D _glow = null;
        protected float mass = 100;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <param name="asset">Asset name</param>
        /// <param name="mass">Mass (default 100)</param>
        public Dynamic(ContentManager content, String asset, float mass)
            : base(content, asset)
        {
            _mass = mass;
            _drag = 0.98f;
            _acc = Vector2.Zero;
            _vel = Vector2.Zero;
            _frc = Vector2.Zero;
            _active = false;
            _dynamic = true;
            _child = new List<Dynamic>();
            _time = 0;
            _applyGrav = true;
        }


        #region Public methods

        /// <summary>
        /// Add force
        /// </summary>
        /// <param name="force">Force value</param>
        public void AddForce(Vector2 force)
        {
            _frc += force;
        }


        /// <summary>
        /// Collide with other entity
        /// </summary>
        /// <param name="ent">Other entity</param>
        public override void OnCollision(Entity ent)
        {

            base.OnCollision(ent);
        }

        /// <summary>
        /// Add child object
        /// </summary>
        /// <param name="child">Child object</param>
        public void AddChild(Dynamic child)
        {
            _child.Add(child);
         //   child.Active = true;
        }


        /// <summary>
        /// Check if entity is a child
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public bool IsChild(Dynamic ent)
        {
            return _child.Contains(ent);
        }


        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="gameTime">Game time</param>
        /// <param name="spriteBatch">Spritebatch</param>
        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            

            // Physics
            if (_active)
            {
                // Calculate total force
                Vector2 frc = _frc;
                if (_applyGrav)
                    frc += _grav;

                // Get acceleration and velocity
                _acc = frc / _mass;
                _vel += _acc;
                _vel *= _drag;

                // Get position
                _pos += _vel;
            }

            // Glow
            if (_glow != null)
            {
                spriteBatch.Draw(_glow, _pos + _offset, null, Color.White, 0, new Vector2(_glow.Bounds.Width, _glow.Bounds.Height) / 2, _scale * 4.0f, SpriteEffects.None, 0);
            }


            // Update entity
            base.Update(gameTime, spriteBatch);

            // Clear force
            _frc = Vector2.Zero;


            // Offset
            float diff = 360.0f / _child.Count;

            // Move children
            float angle = _time;
            _time += 2.0f;
            for (int c = 0; c < _child.Count; c++)
            {
                Dynamic child = _child[c];

                float dist = Radius;

                float ang = angle + (diff * c);
                ang = MathHelper.ToRadians(-ang);
                Vector2 dest = _pos + new Vector2((float)Math.Sin(ang), (float)Math.Cos(ang)) * dist;
               // child.AddForce((dest - child.Position) * 20);
                child.Position = child.Position + ((dest - child.Position) / (child.Mass / 40.0f));
            }
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gravity
        /// </summary>
        public Vector2 Gravity
        {
            get { return _grav; }
            set { _grav = value; }
        }

        /// <summary>
        /// Get and set gravity set
        /// </summary>
        public bool ApplyGravity
        {
            get { return _applyGrav; }
            set { _applyGrav = value; }
        }

        /// <summary>
        /// Get and set mass
        /// </summary>
        public float Mass
        {
            get { return _mass; }
            set { _mass = value; }
        }

        /// <summary>
        /// Get and set velocity
        /// </summary>
        public Vector2 Velocity
        {
            get { return _vel; }
            set { _vel = value; }
        }

        /// <summary>
        /// Get and set whether active
        /// </summary>
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        /// <summary>
        /// Get and set glow image
        /// </summary>
        public Texture2D Glow
        {
            get { return _glow; }
            set { _glow = value; }
        }

        #endregion

    }
}
