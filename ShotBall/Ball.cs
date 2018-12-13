using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Balls
{
    class Ball
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 shotVelocity;
        protected SpriteObj sprite;
        protected bool isActived;
        protected bool isGravityAffected;
        protected float ray;


        public bool IsActived { get { return isActived; } }


        public Ball(string image)
        {
            sprite = new SpriteObj(image);

            isGravityAffected = true;
            isActived = false;
            shotVelocity = new Vector2(0, -350);
            ray = sprite.Width / 2;
            AlignSprite();

            //key = KeyCode.S;
        }

        public Ball() : this("asset/ball.png")
        {
        }

        public virtual void Input(Vector2 startPosition)
        {

            isActived = true;
            position = startPosition;
            velocity = shotVelocity;
            AlignSprite();



        }



        public void Update()
        {

            if (isGravityAffected)
                ApplyGravity();

            float deltaY = velocity.Y * GfxTools.Win.deltaTime;
            position.Y += deltaY;

            sprite.Translate(0, deltaY);

            CheckWindow();

        }

        public void Draw()
        {
            if (isActived)
                sprite.Draw();
        }


        protected void AlignSprite()
        {
            sprite.Position = new Vector2(position.X - ray, position.Y - ray);
        }

        protected void ApplyGravity()
        {
            velocity.Y += Game.Gravity.Y * Game.DeltaTime;
        }

        public virtual bool CheckWindow()
        {
            if (position.Y - ray >= GfxTools.Win.height)
                isActived = false;
            return !isActived;
        }
    }
}
