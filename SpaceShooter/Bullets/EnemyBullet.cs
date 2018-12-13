using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    abstract class EnemyBullet : Bullet
    {
        public EnemyBullet(string textureName = "bullets", int spriteWidth = 0, int spriteHeight = 0) : base(textureName, spriteWidth, spriteHeight)
        {
            RigidBody.Type = (uint)PhysicsManager.ColliderType.EnemyBullet;
            RigidBody.SetCollisionMask((uint)PhysicsManager.ColliderType.Player);

        }

        public override void OnCollide(GameObject other)
        {
            if(other is Player)
            {
                OnDie();
                ((Player)other).AddDamage(damage);
            }
        }

        public override void Update()
        {
            base.Update();

            if (IsActive)
            {
                if (X + Width / 2 < 0)
                {
                    OnDie();
                }
            }
        }
    }
}
