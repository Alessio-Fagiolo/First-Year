using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FAST2D
{
    class Player : GameObject
    {
        float horizontalSpeed;

        public Player(string filename = "assets/boy.png", int x = 0, int y = 0) : base(filename, x, y)
        {

            horizontalSpeed = 150f;
            Position = new Vector2(30, 415);
            sprite.pivot = new Vector2(30, sprite.Height);
        }

        public void Input()
        {
            if (Game.window.GetKey(KeyCode.Right))
            {
                velocity.X = horizontalSpeed;
            }
            else if (Game.window.GetKey(KeyCode.Left))
            {
                velocity.X = -horizontalSpeed;
                sprite.Rotation -= 1.5f * Game.window.deltaTime;
                if (sprite.Rotation < -MathHelper.PiOver6)
                    sprite.Rotation = -MathHelper.PiOver6;
            }


            else if (sprite.Rotation < 0)
                sprite.Rotation += Game.window.deltaTime;

            else
            {
                velocity.X = 0;
            }
        }
        

        public override void Update()
        {
            base.Update();
        }
    }

}

