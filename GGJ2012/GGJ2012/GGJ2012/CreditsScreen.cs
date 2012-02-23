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
    class CreditsScreen:Menu
    {
        private Button _button1;
        
        private Entity _credits;
        private Entity _menuFrame;
        private Entity _CreditsTitle;

        public CreditsScreen(Game1 game, ContentManager content, string background)
            : base(game, content, background)
        {
            _content = content;
            AddButton(_button1 = new Button(content, "Buttons/button_back", new Vector2(400, 430), 1));
            _button1.Animations = 3;

            _CreditsTitle = new Entity(content, "Credits title");
            _CreditsTitle.Position = new Vector2(400, 170);
            _CreditsTitle.Animations = 1;
            _CreditsTitle.Frames = 1;

            _menuFrame = new Entity(content, "menu frame");
            _menuFrame.Position = new Vector2(400, 300);
            _menuFrame.Animations = 1;
            _menuFrame.Frames = 1;

            _credits = new Entity(content, "credits");
            _credits.Position = new Vector2(400, 300);
            _credits.Animations = 1;
            _credits.Frames = 1;

        }

        public override void OnEvent(Button sender)
        {
            if (sender == _button1)
            {
               // Game.LoadNextScreen();
                // MediaPlayer.Stop();
            }


            base.OnEvent(sender);
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {

            base.Update(gameTime, spriteBatch);
            _menuFrame.Update(gameTime, spriteBatch);
            _credits.Update(gameTime, spriteBatch);
            _CreditsTitle.Update(gameTime, spriteBatch);


            _menuFrame.Rotation += MathHelper.ToRadians(3);

            _CreditsTitle.Scale = (float)(1 + Math.Sin(_menuFrame.Rotation * 2) * 0.025f);
            _credits.Scale = (float)(1 + Math.Sin(_menuFrame.Rotation * 2) * 0.010f);

        }

    }
}
