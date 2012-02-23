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
    /// Player class
    /// </summary>
    public class Player : Dynamic
    {


        /// <summary>
        /// Animation state
        /// </summary>
        public enum AnimState
        {
            IDLE,
            HIT,
            BLINK,
            GRIN
        };

        private AnimState _animState;
        private int _score;
        private Level _level = null;
        private SoundEffect loseChild1;
        private SoundEffect loseChild2;
        private SoundEffect hit1;
        private SoundEffect hit2;
        private SoundEffect hit3;
        private Entity _tail = null;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="asset"></param>
        /// <param name="mass"></param>
        public Player(ContentManager content, String asset, float mass)
            : base(content, asset, mass)
        {
            _animState = AnimState.IDLE;
            _score = 0;
            _tail = new Entity(content, "Characters/tail_yellow");
            _tail.Scale = 0;
        }



        /// <summary>
        /// Collision
        /// </summary>
        /// <param name="ent"></param>
        public override void OnCollision(Entity ent)
        {

            if (ent is Dynamic)
            {
                if (!IsChild((Dynamic)ent))
                {
                    // Audio object for collision of smaller circles
                    SoundEffect hitChild1;
                    SoundEffect hitChild2;

                    hitChild1 = _content.Load<SoundEffect>("Audio/Character/Yay_1");
                    hitChild2 = _content.Load<SoundEffect>("Audio/Character/Yay_2");
                    Random rand = new Random();
                    int randnum = rand.Next(2);

                    if (randnum == 0)
                    {
                        //Maybe check if we are losing child, if we are don't play this.
                        hitChild1.Play();



                    }
                    else if (randnum == 1)
                    {
                        //here too
                        //if (!loseChild1.IsDisposed || !loseChild2.IsDisposed)
                        hitChild2.Play();
                    }
                    //end of audio for collecting children

                    AddChild((Dynamic)ent);
                    ((Dynamic)ent).Active = true;

                    _score++;

                    if (_animState == AnimState.IDLE)
                    {
                        _animState = AnimState.GRIN;
                        _curAnim = 3;
                        _curFrame = 0;
                    }
                }
            }
            else if (ent is Enemy)
            {
                Enemy enm = (Enemy)ent;

                if (enm.State != Button.ButtonState.CLICK)
                {
                    switch (enm.EType)
                    {
                        case Enemy.EnemyType.SPIKEY:
                            {
                                if (_child.Count > 0)
                                {
                                    Dynamic child = _child[0];
                                    _child.RemoveAt(0);

                                    Vector2 axis = _pos - child.Position;//child.Position - _pos;
                                    axis.Normalize();
                                    child.Position = _pos + axis * (Radius + child.Radius) * 1.01f;
                                    child.AddForce(axis * 500);
                                    child.Active = true;

                                    //Audio for losing a child
                                    //SoundEffect loseChild1;
                                    //SoundEffect loseChild2;

                                    loseChild1 = _content.Load<SoundEffect>("Audio/Character/Ohno_1");
                                    loseChild2 = _content.Load<SoundEffect>("Audio/Character/Ohno_2");
                                    hit1 = _content.Load<SoundEffect>("Audio/Environment/GeneralSounds/Hit");
                                    hit2 = _content.Load<SoundEffect>("Audio/Environment/GeneralSounds/Hit2");
                                    hit3 = _content.Load<SoundEffect>("Audio/Environment/GeneralSounds/Hit3");
                                    Random rand = new Random();
                                    int randnum = rand.Next(7);

                                    switch (randnum)
                                    {
                                        case (0):
                                            loseChild1.Play();
                                            break;

                                        case (1):
                                            loseChild2.Play();
                                            break;

                                        case (2):
                                            hit1.Play();
                                            break;

                                        case (3):
                                            hit2.Play();
                                            break;

                                        case (4):
                                            hit3.Play();
                                            break;



                                    }
                                    //

                                    _score--;
                                }
                                else
                                {
                                    // Die
                                    SoundEffect die;
                                    die = _content.Load<SoundEffect>("Audio/Character/Explode");
                                    //die.Play();

                                }

                                if (_animState == AnimState.IDLE)
                                {
                                    _animState = AnimState.HIT;
                                    _curAnim = 2;
                                    _curFrame = 0;
                                }

                                break;
                            }

                        case Enemy.EnemyType.BURSTING:
                            {
                                break;
                            }

                        case Enemy.EnemyType.STICKY:
                            {
                                break;
                            }
                    }
                }
            }
            else if (ent is Static)
            {
                ent.OnCollision(this);
            }

            base.OnCollision(ent);
        }

        /// <summary>
        /// End current animation
        /// </summary>
        public override void AnimationEnded()
        {
            Random rnd = new Random();

            switch (_animState)
            {
                case AnimState.IDLE:
                    {
                        if (rnd.Next(10) < 7)
                        {
                            _curFrame = 0;
                            _curAnim = 0;
                            _animState = AnimState.IDLE;
                        }
                        else
                        {
                            _curFrame = 0;
                            _curAnim = 1;
                            _animState = AnimState.BLINK;
                        }

                        break;
                    }

                case AnimState.HIT:
                case AnimState.BLINK:
                case AnimState.GRIN:
                    {
                        _curFrame = 0;
                        _curAnim = 0;
                        _animState = AnimState.IDLE;
                        break;
                    }
            }
        }


        // Update player
        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _tail.Position = _pos;
            _tail.Offset = _offset;
            _tail.Update(gameTime, spriteBatch);

            base.Update(gameTime, spriteBatch);
        }

        /// <summary>
        /// Get and set score
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// Get child count
        /// </summary>
        public int ChildCount
        {
            get { return _child.Count; }
        }

        /// <summary>
        /// Get and set current level
        /// </summary>
        public Level CurLevel
        {
            get { return _level; }
            set { _level = value; }
        }


        /// <summary>
        /// Get tail
        /// </summary>
        public Entity Tail
        {
            get { return _tail; }
        }
    }
}
