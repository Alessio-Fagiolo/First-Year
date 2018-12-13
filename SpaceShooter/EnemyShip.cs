using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    class EnemyShip : Ship
    {
        public EnemyShip(string textureName="enemy", DrawManager.Layer drawLayer = DrawManager.Layer.Playground, int spriteWidth = 0, int spriteHeight = 0) : base(Vector2.Zero, textureName, drawLayer, spriteWidth, spriteHeight)
        {
            sprite.FlipX = true;
            RigidBody = new RigidBody(sprite.position, this);
            RigidBody.Type = (uint)PhysicsManager.ColliderType.Enemy;
            Velocity = new Vector2(-260, 0);
            IsActive = false;
            cannonOffset = new Vector2(-Width / 2, 14);
            Nrg = 10;
            currentBulletType = BulletManager.BulletType.BigBlueLaser;
        }

        public override void Update()
        {
            base.Update();

            if (IsActive)
            {
                if(Position.X+Width/2 < 0)
                {
                    OnDie();
                    return;
                }
                
                shootCounter -= Game.DeltaTime;
                if (shootCounter <= 0)
                {
                    Shoot(currentBulletType);
                    shootCounter = RandomGenerator.GetRandom(1, 3);
                }
            }
        }

        public override void OnDie()
        {
            base.OnDie();
            SpawnManager.Restore(this);
        }
    }
}
