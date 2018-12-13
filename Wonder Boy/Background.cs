using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FAST2D
{
    class Background : GameObject
    {

      protected  Sprite Sprite2;
        


        public Background(string fileName = "assets/gameBg.jpg", int x = 0, int y = 0) : base(fileName, x, y)
        {
            //texture = new Texture(fileName);
            Sprite2 = new Sprite(this.texture.Width, this.texture.Height);
            Sprite2.position = new Vector2(sprite.Width, 0);
            velocity = new Vector2(-100, 0);


        }

        public void Scroll()
        {
            if(sprite.position.X < -this.texture.Width)
            {
                sprite.position = new Vector2(Sprite2.Width - 1, 0);
            }
            else if(Sprite2.position.X < -this.texture.Width )
            {
                Sprite2.position = new Vector2(sprite.Width -1, 0);
            }
        }

        public override void Update()
        {
            base.Update();
            Sprite2.position += new Vector2(this.velocity.X, this.velocity.Y) * Game.deltaTime;
            Scroll();
        }

        public override void Draw()
        {
            base.Draw();
            Sprite2.DrawTexture(this.texture);
        }

        public override void SetTexture(Texture newTex)
        {
            
        }

    }
}
