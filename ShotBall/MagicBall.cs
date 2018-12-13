using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Balls
{
    class MagicBall : Ball
    {
        public MagicBall () : base("asset/magic.png")
        {
            isGravityAffected = false;
            shotVelocity = new Vector2(0, -150);
           
            
        }



        public override bool CheckWindow()
        {
            if (position.Y + ray <= 0)
                isActived = false;
            return !isActived;
        }




    }
}
