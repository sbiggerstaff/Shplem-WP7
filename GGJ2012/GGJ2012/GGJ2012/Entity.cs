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
    /// Core entity
    /// </summary>
    public class Entity
    {
        protected ContentManager _content;
        protected Texture2D _texture;
        protected int _animations, _curAnim;
        protected int _frames, _curFrame, _frameDelay, _curFrameDelay;
        protected Vector2 _pos, _offset, _size, _texSize;
        protected float _scale, _rot;
        protected bool _dynamic, _animate = true, _visible = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <param name="asset">Asset name</param>
        public Entity(ContentManager content, String asset)
        {
            _content = content;
            _texture = content.Load<Texture2D>(asset);
            _pos = Vector2.Zero;
            _offset = Vector2.Zero;
            _texSize = new Vector2(_texture.Bounds.Width, _texture.Bounds.Height);
            _scale = 1.0f;
            _dynamic = false;
            _animations = 1;
            _curAnim = 0;
            _frames = 1;
            _curFrame = 0;
            _frameDelay = 2;
            _curFrameDelay = 0;
            _rot = 0;
        }

        #region Public methods


        /// <summary>
        /// Get distance to other entity
        /// </summary>
        /// <param name="ent">Other entity</param>
        /// <returns>Distance</returns>
        public float DistanceTo(Entity ent)
        {
            return (ent.Position - _pos).LengthSquared();
        }


        /// <summary>
        /// Check if two entities are overlapping
        /// </summary>
        /// <param name="ent">Other entity</param>
        /// <returns>Whether this entity overlaps the other</returns>
        public bool IsCollidingWith(Entity ent)
        {
            float totalRad = Radius + ent.Radius;
            return DistanceTo(ent) <= totalRad * totalRad;
        }

        /// <summary>
        /// Get if point is ontop of entity
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns>True of false</returns>
        public bool PointIn(Vector2 point)
        {
            float rad = Radius;
            return ((_pos + _offset) - point).LengthSquared() <= rad * rad;
        }


        /// <summary>
        /// Get if point is in bounding box
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool PointInBox(Vector2 point)
        {
            return point.X >= _pos.X - _size.X / 2 &&
                    point.Y >= _pos.Y - _size.Y / 2 &&
                    point.X <= _pos.X + _size.X / 2 &&
                    point.Y <= _pos.Y + _size.Y / 2;
        }

        /// <summary>
        /// Respond to collision
        /// </summary>
        /// <param name="ent">Entity collided with</param>
        public virtual void OnCollision(Entity ent)
        {
        }


        /// <summary>
        /// Called when current animation has ended
        /// </summary>
        public virtual void AnimationEnded()
        {
            _curFrame = 0;
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="gameTime">Game time</param>
        /// <param name="spriteBatch">Sprite batch</param>
        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
               // spriteBatch.Draw(_texture, _pos, null, Color.White, 0, _size / 2, _scale, SpriteEffects.None, 0);

                // Get actual size
                int frameSizeX = (int)_texSize.X / _frames;
                int frameSizeY = (int)_texSize.Y / _animations;
                _size = new Vector2(frameSizeX, frameSizeY);

                // Switch frame
                if (_animate)
                {
                    _curFrameDelay++;
                    if (_curFrameDelay >= _frameDelay)
                    {
                        _curFrame++;
                        if (_curFrame >= _frames - 1)
                            AnimationEnded();

                        _curFrameDelay = 0;
                    }
                }

                // Draw frame
                Vector2 pos = _pos + _offset;
                Vector2 size = _size * _scale;
                Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);

                Rectangle source = new Rectangle(_curFrame * frameSizeX, _curAnim * frameSizeY, frameSizeX, frameSizeY);
                if (_visible)
                    spriteBatch.Draw(_texture, dest, source, Color.White, MathHelper.ToRadians(_rot), _size / 2, SpriteEffects.None, 0);
            }
        }


        #endregion // Public methods


        #region Properties


        /// <summary>
        /// Check if dynamic
        /// </summary>
        public bool Dynamic
        {
            get { return _dynamic; }
        }

        /// <summary>
        /// Get and set texture
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        /// <summary>
        /// Get and set position
        /// </summary>
        public Vector2 Position
        {
            get { return _pos; }
            set { _pos = value; }
        }


        /// <summary>
        /// Get and set offset
        /// </summary>
        public Vector2 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        /// <summary>
        /// Get and set size
        /// </summary>
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Get and set scale factor
        /// </summary>
        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        /// <summary>
        /// Get and set rotation
        /// </summary>
        public float Rotation
        {
            get { return _rot; }
            set { _rot = value; }
        }

        /// <summary>
        /// Get radius of entity
        /// </summary>
        public float Radius
        {
            get { return (_size.X / 2) * _scale; }
        }


        /// <summary>
        /// Get and set frames
        /// </summary>
        public int Frames
        {
            get { return _frames; }
            set { _frames = value; }
        }

        /// <summary>
        /// Get and set current frame
        /// </summary>
        public int CurFrame
        {
            get { return _curFrame; }
            set { _curFrame = value; }
        }


        /// <summary>
        /// Get and set animations
        /// </summary>
        public int Animations
        {
            get { return _animations; }
            set { _animations = value; }
        }


        /// <summary>
        /// Get and set current animation
        /// </summary>
        public int CurAnimation
        {
            get { return _curAnim; }
            set { _curAnim = value; }
        }


        /// <summary>
        /// Animating
        /// </summary>
        public bool Animating
        {
            get { return _animate; }
            set { _animate = value; }
        }


        /// <summary>
        /// Get and set visibility
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        #endregion // Properties
    }
}
