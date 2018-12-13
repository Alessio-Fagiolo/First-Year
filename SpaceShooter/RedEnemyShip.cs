using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    class RedEnemyShip : EnemyShip
    {
        public RedEnemyShip(string textureName = "enemy2", DrawManager.Layer drawLayer = DrawManager.Layer.Playground, int spriteWidth = 0, int spriteHeight = 0) : base(textureName, drawLayer, spriteWidth, spriteHeight)
        {
            Nrg = 30;
        }

        public override void Shoot(BulletManager.BulletType type)
        {
            base.Shoot(type);
            int randBullet = RandomGenerator.GetRandom(0, 100);

            if (randBullet <= 30)
            {
                currentBulletType = BulletManager.BulletType.BigBlueLaser;
            }
            else
            {
                currentBulletType = BulletManager.BulletType.FireGlobe;
            }
        }
    }
}
