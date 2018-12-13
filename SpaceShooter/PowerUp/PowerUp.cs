using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    abstract class PowerUp : GameObject
    {
        protected float duration;
        protected Player attachedPlayer;

        public PowerUp(Vector2 spritePosition, string textureName) : base(spritePosition, textureName, DrawManager.Layer.Foreground)
        {
            sprite.pivot = new Vector2(Width / 2, Height / 2);
            Circle circle = new Circle(Vector2.Zero, null, sprite.pivot.X);
            RigidBody = new RigidBody(sprite.position, this,circle,null,false);
            RigidBody.Velocity = new Vector2(-350, 0);
            RigidBody.Type = (uint)PhysicsManager.ColliderType.PowerUp;
            RigidBody.SetCollisionMask((uint)PhysicsManager.ColliderType.Player);
            IsActive = false;
        }

        protected abstract void OnAttach(Player player);

        protected virtual void OnDetach()
        {
            attachedPlayer = null;
            SpawnManager.Restore(this);
        }

        public override void OnCollide(GameObject other)
        {
            if(other is Player)
            {
                OnAttach((Player)other);
            }
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
            {
                sprite.Rotation += -Game.DeltaTime;
                if (Position.X + Width / 2 < 0)
                {
                    SpawnManager.Restore(this);
                }
            }
        }

    }
}
