using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallsA
{
    class SpeedBall : Ball
    {
        public SpeedBall() : base("Assets/speed_ball.png")
        {
            shootVelocity.Y = -400;
        }
    }
}
