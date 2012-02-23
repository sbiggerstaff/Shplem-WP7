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
    public class Menu : Screen
    {
        protected List<Button> buttonList;

        public Menu(Game1 game, ContentManager content)
            : base(game, content)
        {
            buttonList = new List<Button>();
        }

        public Menu(Game1 game, ContentManager content, string background)
            : base(game, content, background)
        {
            buttonList = new List<Button>();
        }

        public void AddButton(Button button)
        {
            buttonList.Add(button);
            button.Animations = 4;
            button.Frames = 25;
            button.EventListener = this;

        }



        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Update(gameTime, spriteBatch);
            foreach (Button button in buttonList)
            {
                button.Update(gameTime, spriteBatch);

            }

        }
    }
}
