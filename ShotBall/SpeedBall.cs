using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balls
{
    class SpeedBall : Ball
    {
        public SpeedBall() : base("asset/speed.png")
        {

            shotVelocity = new Vector2(0, -600f);
            AlignSprite();
            
        }



    }
}
