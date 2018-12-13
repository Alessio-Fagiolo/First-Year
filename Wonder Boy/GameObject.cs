using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FAST2D
{
    class GameObject : IDrawable
    {
        protected Texture texture;
        public Vector2 velocity;
        protected Sprite sprite;
        protected bool isActive;
        public int Width { get { return texture.Width; } }
        public int Height { get { return texture.Height; } }
        public float X { get { return sprite.position.X; } set { sprite.position.X = value; } }
        public float Y { get { return sprite.position.Y; } set { sprite.position.Y = value; } }
        public Vector2 Position { get { return sprite.position; } set { sprite.position = value; } }


        public GameObject()
        {

        }



        public GameObject(string fileName, int x = 0, int y = 0)
        {
            texture = new Texture(fileName);
            sprite = new Sprite(texture.Width, texture.Height);
            isActive = true;
        }

        public GameObject(string fileName, Vector2 spritePosition) : this(fileName, (int)spritePosition.X, (int)spritePosition.Y)
        {

        }

        public virtual void Update()
        {
            X += velocity.X * Game.deltaTime;
            Y += velocity.Y * Game.deltaTime;

        }

        public void Translate(float deltaX, float deltaY)
        {
            sprite.position.X += deltaX;
            sprite.position.Y += deltaY;
        }

        public virtual void SetTexture(Texture newTex)
        {
            texture = newTex;
        }

        public Texture GetTexture()
        {
            return texture;
        }

        public virtual void Draw()
        {
            sprite.DrawTexture(texture);
        }


    }
}
