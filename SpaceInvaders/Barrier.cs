using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvadersB
{
    class Barrier
    {
        SpriteObj sprite;
        Vector2 position;

        public int Width { get { return sprite.Width; } }
        public int Height { get { return sprite.Height; } }
        float counter;

        public Barrier(int x, int y)
        {
            position = new Vector2(x, y);
            sprite = new SpriteObj("Assets/barrier.png", x, y);
            sprite.Translate(-Width / 2, -Height / 2);
            counter = 0;
        }

        public void Draw()
        {
            sprite.Draw();
            ReDraw();
        }

        private bool PixelCollides(Vector2 center, float ray, bool erase = false)
        {
            bool collision = false;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Vector2 pixelPos = new Vector2(position.X - Width / 2 + x, position.Y - Height / 2 + y);
                    float pixToBulletDist = pixelPos.Sub(center).GetLength();
                    if (pixToBulletDist <= ray)
                    {
                        //obtains index of current pixel's alpha
                        int pixelAlfaIndex = (y * Width + x) * 4 + 3;

                        if (erase)
                        {//must erase pixels inside the explosion's circle
                            sprite.GetSprite().bitmap[pixelAlfaIndex] = 0;


                            collision = true;
                        }
                        else
                        {
                            if (sprite.GetSprite().bitmap[pixelAlfaIndex] != 0)
                                return true;
                        }
                    }
                }
            }
            return collision;
        }


        private void ReDraw()
        {

            counter += GfxTools.Win.deltaTime;



            if (counter > 0.5)
            {
                for (int y = 0; y < Height; y++)
                {
                    int random = RandomGenerator.GetRandom(0, 100);
                    if (random > 50)
                        continue;

                    for (int x = 0; x < Width; x++)
                    {
                        if (random > 50)
                            continue;

                        int pixelAlfaIndex = (y * Width + x) * 4 + 3;
                        int pixelRedIndex = (y * Width + x) * 4;

                        if (sprite.GetSprite().bitmap[pixelAlfaIndex] == 0 && sprite.GetSprite().bitmap[pixelRedIndex] > 0 && sprite.GetSprite().bitmap[pixelRedIndex + 1] < 255)
                        {
                            
                                sprite.GetSprite().bitmap[pixelAlfaIndex] = 255;
                                counter = 0;
                            

                        }

                    }

                }
            }
        }



        public bool Collides(Vector2 center, float ray)
        {
            float explosionRay = RandomGenerator.GetRandom(15, 25);
            bool collision = false;
            Vector2 dist = position.Sub(center);
            if (dist.GetLength() <= Width / 2 + ray)//bounding circle collision
            {
                if (PixelCollides(center, ray))//collision between bullet and sprite pixels
                {
                    collision = true;
                    //pixel erasing
                    PixelCollides(center, explosionRay, true);
                }

            }
            return collision;
        }

    }
}
