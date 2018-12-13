using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace BallsA
{
    class Ball
    {
        protected SpriteObj sprite;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 shootVelocity;
        protected Vector2 mousePos;

        protected bool isActive;
        protected bool isGravityAffected;
        protected float ray;

        public bool IsActive { get { return isActive; } }

        public Ball() : this("Assets/ball.png")
        {

        }

        protected Ball(string image)
        {
            sprite = new SpriteObj(image);
            shootVelocity = new Vector2(0, 0);

            isGravityAffected = true;
            ray = sprite.Width / 2;
            AlignSprite();
        }

        public void Shoot(Vector2 startPos)
        {
            isActive = true;
            position = startPos;
            shootVelocity.Y = -(GfxTools.Win.height - GfxTools.Win.mouseY);
            shootVelocity.X = (GfxTools.Win.mouseX - GfxTools.Win.width / 2);
            velocity.X = shootVelocity.X;
            velocity.Y = shootVelocity.Y;
            //velocity.X = RandomGenerator.GetRandom(-300, 300);
            AlignSprite();
        }

        protected void AlignSprite()
        {
            sprite.Position = new Vector2(position.X - ray, position.Y - ray);
        }

        public void Draw()
        {
            sprite.Draw();
        }

        protected void Translate(float x, float y)
        {
            sprite.Translate(x, y);
        }

        protected void ApplyGravity()
        {
            velocity.Y += Game.Gravity * Game.DeltaTime;
        }


        public void Update()
        {
            if (isGravityAffected)
                ApplyGravity();

            float deltaX = velocity.X * Game.DeltaTime;
            float deltaY = velocity.Y * Game.DeltaTime;

            position.X += deltaX;
            position.Y += deltaY;

            sprite.Translate(deltaX, deltaY);
            coll();

            
        }

        

        public void OnCollide(Ball b, Vector2 normal)
        {
            if (normal.X != 0)
            {
                velocity.X = -velocity.X;

            }
            else
            {
                velocity.Y = -velocity.Y;
            }
        }


        public virtual void coll()
        {
            if (position.X + ray >= GfxTools.Win.width || position.X - ray <= 0)
            {
                velocity.X = -velocity.X;

            }


            if (position.Y + ray >= GfxTools.Win.height || position.Y - ray <= 0)
            {
                if (position.Y - ray < 0)
                {
                    position.Y = ray;
                }

                if (position.Y + ray > GfxTools.Win.height)
                {
                    position.Y = GfxTools.Win.height - ray;
                   
                }

                    velocity.Y = -velocity.Y;

            }


        }

        public virtual bool CheckWindow()
        {
            if (position.Y - ray >= GfxTools.Win.height)
            {
                isActive = false;
            }
            return !isActive;//true if outside the window
        }
    }
}
