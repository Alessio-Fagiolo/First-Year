using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    class FireGlobeBullet : EnemyBullet
    {
        protected double elapsedTime;
        public FireGlobeBullet(string textureName = "fireGlobe", int spriteWidth = 0, int spriteHeight = 0) : base(textureName, spriteWidth, spriteHeight)
        {
            RigidBody rb = new RigidBody(sprite.position, this, new Circle(Vector2.Zero, null, Width / 2), null, false);
            rb.Velocity = RigidBody.Velocity;
            rb.Type = (uint)PhysicsManager.ColliderType.EnemyBullet;
            rb.SetCollisionMask((uint)PhysicsManager.ColliderType.Player);
            PhysicsManager.RemoveItem(RigidBody);
            RigidBody = rb;

            Type = BulletManager.BulletType.FireGlobe;
            shootVelocity = new Vector2(-300, 0);
            damage = 25;
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
            {   elapsedTime += Game.DeltaTime;
                RigidBody.SetYVelocity((float)Math.Cos(elapsedTime*5)*500);
            }
        }


    }
}
