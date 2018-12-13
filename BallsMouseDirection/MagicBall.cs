using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallsA
{
    class MagicBall : Ball
    {
        public MagicBall() : base("Assets/magic_ball.png")
        {
            shootVelocity.Y= 100;
            isGravityAffected = false;
        }

        public override bool CheckWindow()
        {
            if (!base.CheckWindow())//not exited from bottom
            {
                if (position.Y + ray < 0)
                {
                    isActive = false;
                }
            }
            return !isActive;
        }
    }
}
