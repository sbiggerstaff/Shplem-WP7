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
    class MainMenu:Menu
    {
        private Button _button1;
        private Entity _menuFrame;
        private Song _bgMusic;
        private Entity _menuLogo;
        
        
        public MainMenu(Game1 game, ContentManager content, string background)
            : base(game, content, background)
        {
            //load and play the background sound
            _bgMusic = _content.Load<Song>("Audio/Music/LightBG");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_bgMusic);
            MediaPlayer.Volume = 1.0f;
           
          
            AddButton(_button1 = new Button(content, "Buttons/button_play",new Vector2(400,380),1));
            _button1.Animations = 3;
           // AddButton(_button2 = new Button(content, "Buttons/button_score",new Vector2(400,380),1));
           // _button2.Animations = 3;
           // AddButton(_button3 = new Button(content, "Buttons/button_credits",new Vector2(475,380),1));
           // _button3.Animations = 3;
            _menuFrame = new Entity(content, "menu frame");
            _menuFrame.Position = new Vector2(400, 300);
            _menuFrame.Animations = 1;
            _menuFrame.Frames = 1;
            
            _menuLogo = new Entity(content, "menu logo");
            _menuLogo.Position = new Vector2(400, 250);
            _menuLogo.Animations = 1;
            _menuLogo.Frames = 1;
            
            
        }

        public override void OnEvent(Button sender)
        {
            if (sender == _button1)
            {
                //Game.LoadNextScreen();
                _game.NextScreen();
               // MediaPlayer.Stop();
            }


            base.OnEvent(sender);
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            base.Update(gameTime, spriteBatch);
            _menuFrame.Update(gameTime, spriteBatch);
            _menuLogo.Update(gameTime, spriteBatch);
            _menuFrame.Rotation += MathHelper.ToRadians(3);
            
            _menuLogo.Scale = (float)(1 + Math.Sin(_menuFrame.Rotation * 2) * 0.025f);
            
        }


    }
}
