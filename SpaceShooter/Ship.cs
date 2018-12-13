using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    abstract class Ship:GameObject
    {
       
        protected float horizontalSpeed;
        protected Vector2 cannonOffset;
        protected float shootCounter;
        protected BulletManager.BulletType currentBulletType;
        protected float nrg;

        public float MaxNrg { get; protected set; }

        public float Nrg { get { return nrg; } set { SetNrg(value); } }

        public Ship(Vector2 spritePosition, string textureName, DrawManager.Layer drawLayer = DrawManager.Layer.Playground, int spriteWidth = 0, int spriteHeight = 0) : base(spritePosition, textureName, drawLayer, spriteWidth, spriteHeight)
        {
            sprite.pivot = new Vector2(Width / 2, Height / 2);
            MaxNrg = 100;
            
        }

        public virtual void Shoot(BulletManager.BulletType type)
        {
            Bullet b = BulletManager.GetBullet(type);
            if (b != null)
            {
                float bulletOffsetX = b.Width / 2;
                if (b is EnemyBullet)
                    bulletOffsetX = -bulletOffsetX;
                b.Shoot(Position + cannonOffset + new Vector2(bulletOffsetX,0));
            }
        }

        protected virtual void SetNrg(float newValue)
        {
            nrg = newValue;
            if (nrg > MaxNrg)
            {
                nrg = MaxNrg;
            }
        }

        public virtual void OnDie()
        {
            IsActive = false;
        }

        public virtual bool AddDamage(float damage)
        {
            Nrg -= damage;
            if (Nrg <= 0)
            {
                OnDie();
                return true;
            }
            return false;
        }
    }
}
