using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    class Player : Ship
    {
        protected float shootDelay;
        protected Bar nrgBar;
        protected int joystickIndex;

        public Player(string fileName, Vector2 spritePosition) : base(spritePosition, fileName)
        {
            //sprite.scale = new Vector2(0.3f, 0.3f);
            RigidBody = new RigidBody(sprite.position, this);
            RigidBody.Type = (uint)PhysicsManager.ColliderType.Player;
            horizontalSpeed = 300;
            shootDelay = 0.45f;
            cannonOffset = new Vector2(Width / 2, 0);

            nrgBar = new Bar(new Vector2(20, 20), "playerBar", MaxNrg);
            nrgBar.SetValue(100);
            nrgBar.BarOffset = new Vector2(4, 4);

            Nrg = 100;
            currentBulletType = BulletManager.BulletType.RedLaser;

            joystickIndex = 0;
        }

        protected override void SetNrg(float newValue)
        {
            base.SetNrg(newValue);
            nrgBar.SetValue(nrg);
        }

        public override bool AddDamage(float damage)
        {
            bool isDead= base.AddDamage(damage);
            if (isDead)
            {
                nrgBar.SetValue(0);
            }
            return isDead;
        }

        public override void OnDie()
        {
            //base.OnDie();
            Game.CurrentScene.IsPlaying = false;
        }



        public void Input()
        {
            shootCounter -= Game.DeltaTime;

            if (Game.NumJoysticks > 0)
            {
                Vector2 axis = Game.window.JoystickAxisLeft(joystickIndex);

                RigidBody.Velocity = axis * horizontalSpeed;

                if(shootCounter<=0 && Game.window.JoystickA(joystickIndex))
                {
                    Shoot(currentBulletType);
                    shootCounter = shootDelay;
                }
            }
            else
            {

                if (Game.window.GetKey(KeyCode.Right))
                {

                    RigidBody.SetXVelocity(horizontalSpeed);
                }
                else if (Game.window.GetKey(KeyCode.Left))
                {
                    RigidBody.SetXVelocity(-horizontalSpeed);
                }
                else
                {
                    RigidBody.SetXVelocity(0);
                }

                if (Game.window.GetKey(KeyCode.Up))
                {
                    RigidBody.SetYVelocity(-horizontalSpeed);
                }
                else if (Game.window.GetKey(KeyCode.Down))
                {
                    RigidBody.SetYVelocity(horizontalSpeed);
                }
                else
                {
                    RigidBody.SetYVelocity(0);
                }

                
                if (shootCounter <= 0)
                {
                    if (Game.window.GetKey(KeyCode.Space))
                    {
                        Shoot(currentBulletType);
                        shootCounter = shootDelay;
                    }


                }
            }
        }

    }
}
