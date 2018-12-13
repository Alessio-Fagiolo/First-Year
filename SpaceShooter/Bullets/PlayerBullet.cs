using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    abstract class PlayerBullet : Bullet
    {
        public PlayerBullet(string textureName = "bullets", int spriteWidth = 0, int spriteHeight = 0) : base(textureName, spriteWidth, spriteHeight)
        {
            RigidBody.Type = (uint)PhysicsManager.ColliderType.PlayerBullet;
            RigidBody.AddCollision((uint)PhysicsManager.ColliderType.Enemy);
            RigidBody.AddCollision((uint)PhysicsManager.ColliderType.EnemyBullet);

        }

        public override void OnCollide(GameObject other)
        {
            if(other is EnemyShip)
            {
                OnDie();
                ((EnemyShip)other).AddDamage(damage);
            }
            else if(other is EnemyBullet)
            {
                ((EnemyBullet)other).OnDie();
                OnDie();
            }
        }

        public override void Update()
        {
            base.Update();

            if (IsActive)
            {
                if (X - Width / 2 >= Game.window.Width)
                {
                    OnDie();
                }
            }
        }
    }
}
