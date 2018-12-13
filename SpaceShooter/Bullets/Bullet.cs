using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    abstract class Bullet : GameObject
    {
        protected Vector2 shootVelocity;
        protected Vector2 textureOffset;
        protected float damage;
        public BulletManager.BulletType Type { get; protected set; }
        public Bullet(string textureName="bullets", int spriteWidth=0, int spriteHeight=0) : base(Vector2.Zero, textureName,DrawManager.Layer.Playground, spriteWidth, spriteHeight)
        {
            sprite.pivot = new Vector2(Width / 2, Height / 2);
            shootVelocity = new Vector2(400, 0);
            IsActive = false;
            RigidBody = new RigidBody(sprite.position, this);
            damage = 10;
        }

        public override void Draw()
        {
            if(IsActive)
                sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, Width, Height);
        }

        public virtual void Shoot(Vector2 startPos)
        {
            IsActive = true;
            Position = startPos;
            Velocity = shootVelocity;
        }

        public virtual void OnDie()
        {
            IsActive = false;
            //restore bullet
            BulletManager.RestoreBullet(this);
        }

        
    }
}
