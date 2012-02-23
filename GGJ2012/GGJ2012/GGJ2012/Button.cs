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

    
   public  class Button:Entity
    {
       private Screen eventListener;

       public enum ButtonState
       { 
           IDLE,
           HOVER,
           CLICK
       }

       private ButtonState _buttonstate;
       private int _data;


       public Button(ContentManager content, string asset, Vector2 position, float radius):base(content, asset)
       {
           _pos = position;
           _scale = radius;
           _buttonstate = ButtonState.IDLE;
           _data = 0;
       }


       public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
       {
           Vector2 maus = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

           if (PointIn(maus)&&(_buttonstate==ButtonState.IDLE))
           
           {
               _buttonstate = ButtonState.HOVER;
           }

           if ((_buttonstate == ButtonState.HOVER) && !PointIn(maus))
           {
               _buttonstate = ButtonState.IDLE;
           }

           if (_buttonstate == ButtonState.HOVER)
           {

               if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                   _buttonstate = ButtonState.CLICK;

           }

           if (_buttonstate == ButtonState.CLICK && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
           {
               _buttonstate = ButtonState.HOVER;
               OnClick();
           }



           switch (_buttonstate)
           {
               case ButtonState.IDLE:
                   CurAnimation = 0;
                   break;
               case ButtonState.HOVER:
                   CurAnimation = 1;
                   break;
               case ButtonState.CLICK:
                   CurAnimation = 2;
                   break;

           }


           base.Update(gameTime, spriteBatch);
       }

       public virtual void OnClick()
       {
           if (eventListener != null)
              eventListener.OnEvent(this);

           SoundEffect press;
           press = _content.Load<SoundEffect>("Audio/UI/ButtonPress");
           press.Play();
       }

       public Screen EventListener
       {
           get
           {
               return eventListener;     
           }

           set {
               eventListener = value;
           }
       }

       /// <summary>
       /// Get button state
       /// </summary>
       public ButtonState State
       {
           get { return _buttonstate; }
       }

       /// <summary>
       /// Private data
       /// </summary>
       public int PrivateData
       {
           get { return _data; }
           set { _data = value; }
       }

    }
}
