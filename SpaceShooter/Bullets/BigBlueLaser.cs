using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    class BigBlueLaser : EnemyBullet
    {
        public BigBlueLaser(string textureName = "blueLaser") : base(textureName)
        {
            Type = BulletManager.BulletType.BigBlueLaser;
            //textureOffset = new Vector2(314, 300);
            shootVelocity.X = -400;
        }

        public override void Shoot(Vector2 startPos)
        {
            base.Shoot(startPos);
            X -= 40;
        }
    }
}
