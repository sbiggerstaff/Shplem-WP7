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
    /// Level class
    /// </summary>
    public class Level
    {
        private List<Layer> _layers;
        private Layer _main;
        private ContentManager _content;
        private Player _plr;
        private Vector2 _grav;
        private Song _bgMusic = null;
        private GameScreen _gameScreen = null;
        private Texture2D _midground = null;
        private Texture2D _foreground = null;
        private Soup _soup = null;
        private Entity _tree = null;
        private float _scroll = 0, _scrollOffset = 1200;
        private bool _invert = false;
        private string _filename = "";
        public float mass = 200.0f;


        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="content"></param>
        public Level(ContentManager content)
        {
            _content = content;
            _layers = new List<Layer>();
            _plr = null;

            // Main layer
            _main = AddLayer();

            //load and play the background sound
            _bgMusic = _content.Load<Song>("Audio/Music/LightBG");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_bgMusic);
        }


        /// <summary>
        /// Add a layer
        /// </summary>
        /// <returns></returns>
        public Layer AddLayer()
        {
            Layer layer = new Layer(_content);
            _layers.Add(layer);
            return layer;
        }


        /// <summary>
        /// Load level from file
        /// </summary>
        /// <param name="filename"></param>
        /*public static Level LoadLevel(GameScreen screen, ContentManager content, string filename)
        {
            // Load lines
            
            
            //string[] lines = System.IO.File.ReadAllLines(filename);

            // Create new level instance
            Level level = new Level(content);
            screen.CurLevel = level;
            level._filename = filename;

            // Read invert, gravity and scroll offset
            string[] toks = lines[0].Split(' ');
            level.Inverted = Convert.ToInt32(toks[0]) > 0;
            level.Gravity = Vector2.UnitY * Convert.ToInt32(toks[1]);
            level._scrollOffset = Convert.ToInt32(toks[2]);

            // Read background
            screen.Background = content.Load<Texture2D>(lines[1]);

            // Read midground
            level.Midground = content.Load<Texture2D>(lines[2]);

            // Read foreground
            level.Foreground = content.Load<Texture2D>(lines[3]);

            // Read pool
            toks = lines[4].Split(' ');
            level.SoupPool = new Soup(content, toks[0]);
            level.SoupPool.Animations = 3;
            level.SoupPool.Frames = 7;
            level.SoupPool.Position = new Vector2(Convert.ToInt32(toks[1]), Convert.ToInt32(toks[2]));

            // Read tree
            toks = lines[5].Split(' ');
            level._tree = new Entity(content, toks[0]);
            level._tree.Position = new Vector2(Convert.ToInt32(toks[1]), Convert.ToInt32(toks[2]));
            level._tree.Animations = 1;
            level._tree.Frames = 7;
            level._tree.Animating = false;

            // Templates
            string[] templ = new string[7];
            for (int i = 0; i < 7; i++)
                templ[i] = lines[i + 6];

            // Read player data
            toks = lines[13].Split(' ');
            string playerAsset = toks[0];
            level._plr = level.LoadPlayer(playerAsset, 4, 25, 0.3f);
            level._plr.Position = new Vector2(Convert.ToInt32(toks[1]), Convert.ToInt32(toks[2]));
            level._plr.Glow = content.Load<Texture2D>("Characters/Glow");
            Player plr = level._plr;

            Layer curLayer = level.MainLayer;

            Random rnd = new Random();

            // Level entities
            for (int i = 15; i < lines.Length; i++)
            {
                string line = lines[i];

                toks = line.Split(' ');

                // Static piece
                if (toks[0] == "NextLayer:")
                {
                    curLayer = level.AddLayer();
                }
                else if (toks[0][0] == 'T')
                {
                    int type = Convert.ToInt32("" + toks[0][1]) - 1;

                    Static ent = curLayer.LoadStatic(templ[type]);
                    ent.Position = new Vector2(Convert.ToInt32(toks[1]), Convert.ToInt32(toks[2]));
                    ent.Scale = Convert.ToInt32(toks[3]) / 100.0f;

                    if (type == 0)
                        ent.SType = Static.StaticType.NORMAL;
                    else if (type == 1)
                        ent.SType = Static.StaticType.BOUNCY;
                    else if (type == 2)
                        ent.SType = Static.StaticType.STICKY;
                    else if (type == 3)
                        ent.SType = Static.StaticType.MULTI;

                    ent.Visible = false;
                }
                else if (toks[0][0] == 'E')
                {
                    int type = Convert.ToInt32("" + toks[0][1]) - 1;

                    Enemy enm = curLayer.LoadEnemy(templ[4 + type]);
                    enm.Animations = 3;
                    enm.Frames = 25;
                    enm.EventListener = screen;
                    enm.Position = new Vector2(Convert.ToInt32(toks[1]), Convert.ToInt32(toks[2]));
                    enm.Scale = Convert.ToInt32(toks[3]) / 100.0f;
                    enm.Rotation = Convert.ToInt32(toks[4]);

                   
                    if (type == 0)
                        enm.EType = Enemy.EnemyType.SPIKEY;
                    else if (type == 1)
                        enm.EType = Enemy.EnemyType.BURSTING;
                    else if (type == 2)
                        enm.EType = Enemy.EnemyType.STICKY;
                }
                else if (toks[0] == "C")
                {
                    Dynamic dyn = curLayer.LoadDynamic(playerAsset, 200 );
                    dyn.Position = new Vector2(Convert.ToInt32(toks[1]), Convert.ToInt32(toks[2]));
                    dyn.Mass = rnd.Next(50, 150);
                    dyn.Scale = 0.19f;
                    dyn.Glow = plr.Glow;
                    dyn.Animations = 4;
                    dyn.Frames = 25;
                }
                else if (toks[0] == "G")
                {
                    Entity ent = curLayer.LoadEntity(toks[1]);
                    ent.Position = new Vector2(Convert.ToInt32(toks[2]), Convert.ToInt32(toks[3]));
                    ent.Scale = Convert.ToInt32(toks[4]) / 100.0f;
                }
                else if (toks[0] == "GA")
                {
                    Entity ent = curLayer.LoadEntity(toks[1]);
                    ent.Position = new Vector2(Convert.ToInt32(toks[2]), Convert.ToInt32(toks[3]));
                    ent.Scale = Convert.ToInt32(toks[4]) / 100.0f;
                    ent.Frames = Convert.ToInt32(toks[5]);
                }
                else if (toks[0] == "BT")
                {
                    Button ent = curLayer.LoadButton(toks[1]);
                    ent.EventListener = screen;
                    ent.PrivateData = Convert.ToInt32(toks[2]);
                    ent.Position = new Vector2(Convert.ToInt32(toks[3]), Convert.ToInt32(toks[4]));
                    ent.Scale = Convert.ToInt32(toks[5]) / 100.0f;
                    ent.Animations = 3;
                    ent.Frames = 25;
                }
            }

            return level;
        }
        */

        /// <summary>
        /// Load Player
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="animations"></param>
        /// <param name="frames"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Player LoadPlayer(String asset, int animations, int frames, float scale)
        {
            _plr = new Player(_content, asset, mass);
            _plr.Animations = animations;
            _plr.Frames = frames;
            _plr.Scale = scale;
            _plr.CurLevel = this;
            return _plr;
        }


        /// <summary>
        /// Update level
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (_plr != null)
            {
                // Player gravity
                _plr.Gravity = _grav;

                // Collisions
                foreach (Entity ent in _main.Entities)
                {
                    if (_plr.IsCollidingWith(ent))
                    {
                        _plr.OnCollision(ent);
                    }
                }
            }
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 offset = Vector2.UnitY * _scroll;

            // Get how close player is to center
            float plrd = 400 - _plr.Position.X;
            plrd /= 400;

            // Scroll background
            _gameScreen.Offset = Vector2.UnitY * (_scroll - 1200) + (Vector2.UnitX * (plrd - 1) * 100);

            // Draw midground
          //  if (_midground != null)
            //    spriteBatch.Draw(_midground, new Vector2((plrd - 1) * 50, _scroll + (600 - _midground.Bounds.Height)), Color.White);

            // Draw layers
            foreach (Layer layer in _layers)
            {
                layer.Offset = offset;
                layer.Draw(gameTime, spriteBatch);
            }

            // Draw pool
            if (_soup != null)
            {
                _soup.Offset = offset;
                _soup.Update(gameTime, spriteBatch);
            }

            // Draw player
            if (_plr != null)
            {
                _plr.Offset = offset;
                _plr.Update(gameTime, spriteBatch);
            }

            // Draw foreground
            if (_foreground != null)
                spriteBatch.Draw(_foreground, new Vector2(0, _scroll - _scrollOffset), Color.White);

            // Draw tree
            if (_tree != null)
            {
                if (_plr.ChildCount < 6)
                    _tree.CurFrame = _plr.ChildCount;
                else
                    _tree.CurFrame = 6;
                _tree.Offset = offset;
                _tree.Update(gameTime, spriteBatch);
            }
        }


        #region Properties


        /// <summary>
        /// Get file name
        /// </summary>
        public string Filename
        {
            get { return _filename; }
        }

        /// <summary>
        /// Get and set main player
        /// </summary>
        public Player MainPlayer
        {
            get { return _plr; }
            set { _plr = value; }
        }

        /// <summary>
        /// Get main layer
        /// </summary>
        public Layer MainLayer
        {
            get { return _main; }
        }

        /// <summary>
        /// Get and set inverted
        /// </summary>
        public bool Inverted
        {
            get { return _invert; }
            set { _invert = value; }
        }

        /// <summary>
        /// Get and set gravity
        /// </summary>
        public Vector2 Gravity
        {
            get { return _grav; }
            set { _grav = value; }
        }

        /// <summary>
        /// Get and set game screen
        /// </summary>
        public GameScreen Screen
        {
            get { return _gameScreen; }
            set { _gameScreen = value; }
        }

        /// <summary>
        /// Get and set midground
        /// </summary>
        public Texture2D Midground
        {
            get { return _midground; }
            set { _midground = value; }
        }

        /// <summary>
        /// Get and set foreground
        /// </summary>
        public Texture2D Foreground
        {
            get { return _foreground; }
            set { _foreground = value; }
        }


        /// <summary>
        /// Get and set soup
        /// </summary>
        public Soup SoupPool
        {
            get { return _soup; }
            set { _soup = value; }
        }


        /// <summary>
        /// Get and set tree
        /// </summary>
        public Entity Tree
        {
            get { return _tree; }
            set { _tree = value; }
        }

        /// <summary>
        /// Get and set scroll
        /// </summary>
        public float Scroll
        {
            get { return _scroll; }
            set { _scroll = value; }
        }

        #endregion
    }
}
