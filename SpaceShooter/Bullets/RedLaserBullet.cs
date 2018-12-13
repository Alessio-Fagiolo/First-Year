using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    class RedLaserBullet : PlayerBullet
    {
        public RedLaserBullet(string textureName = "bullets") : base(textureName, 28, 15)
        {
            Type = BulletManager.BulletType.RedLaser;
            textureOffset = new Vector2(247, 38);
        }
    }
}
