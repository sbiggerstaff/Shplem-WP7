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
    public class Layer
    {
        private List<Entity> _ents;
        private Vector2 _offset;
        private ContentManager _content;
        private float mass = 100;

        public Layer(ContentManager content)
        {
            _ents = new List<Entity>();
            _offset = Vector2.Zero;
            _content = content;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            //Draw Level 
            foreach (Entity ent in _ents)
            {
                ent.Offset = _offset;
                ent.Update(gameTime, spriteBatch);
            }

        }


        /// <summary>
        /// Load entity
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public Entity LoadEntity(String asset)
        {
            Entity ent = new Entity(_content, asset);
            _ents.Add(ent);
            return ent;
        }


        /// <summary>
        /// Load button
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public Button LoadButton(String asset)
        {
            Button ent = new Button(_content, asset, Vector2.Zero, 0);
            _ents.Add(ent);
            return ent;
        }

        /// <summary>
        /// Load static
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public Static LoadStatic(String asset)
        {
            Static ent = new Static(_content, asset);
            _ents.Add(ent);
            return ent;
        }


        /// <summary>
        /// Load dynamic entity
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="mass"></param>
        /// <returns></returns>
        public Dynamic LoadDynamic(String asset, float mass )
        {
            Dynamic ent = new Dynamic(_content, asset, mass);
            _ents.Add(ent);
            return ent;
        }


        /// <summary>
        /// Load enemy
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public Enemy LoadEnemy(String asset)
        {
            Enemy enm = new Enemy(_content, asset);
            _ents.Add(enm);
            return enm;
        }

        public Vector2 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public List<Entity> Entities
        {
            get { return _ents; }
        }



    }
}