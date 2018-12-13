using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Fast2Da
{
    class WelcomeScene:TitleScene
    {
        Sprite text;
        Texture textTexture;
        float txtCounter;

        public WelcomeScene(string texture, KeyCode keyNext) : base(texture, keyNext)
        {
        }

        public override void Start()
        {
            base.Start();
            counter = 0;
            buttonPressWait = 0.6f;

            textTexture = new Texture("Assets/pressEnter.png");
            text = new Sprite(textTexture.Width, textTexture.Height);
            text.pivot.X = text.Width / 2;

            text.position = new Vector2(Game.window.Width / 2, Game.window.Height *0.75f);
        }

        public override void Draw()
        {
            img.DrawTexture(imgTexture);
            text.DrawTexture(textTexture);
        }

        //public override void Input()
        //{
        //    if (Game.window.GetKey(KeyCode.Return))
        //    {
        //        IsPlaying = false;
        //    }
        //}

        public override void Update()
        {
            base.Update();
            if (counterDirection == 0)
            {
                txtCounter += Game.DeltaTime;
                text.SetAdditiveTint(0, -Math.Abs((float)Math.Sin(txtCounter)) + 0.5f, 0, 0);
            }
            else
            {
                text.SetMultiplyTint(colorMul, colorMul, colorMul, 1);
            }
        }
    }
}
