using Aiv.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallsA
{
    static class Game
    {
        static Window window;
        //static float totalTime;
        static float gravity;

        static List<Ball> activeBalls;
        static List<MagicBall> activeMagicBalls;
        static List<SpeedBall> activeSpeedBalls;

        const int DEFAULT_BALLS = 10;

        static Queue<Ball> balls;
        static Queue<MagicBall> magicBalls;
        static Queue<SpeedBall> speedBalls;

        static Vector2 cannonPos;
        static Vector2 MouseVector;

        const float DEFAULT_SHOOT_CD = 0.2f;
        static float shootCoutdown;

        public static float DeltaTime { get { return window.deltaTime; } }
        public static float Gravity { get { return gravity; } }

        static Game()
        {
            window = new Window(800, 600, "Balls", PixelFormat.RGB);
            gravity = 300.0f;
            GfxTools.Init(window);

            

            balls = new Queue<Ball>(DEFAULT_BALLS);
            speedBalls = new Queue<SpeedBall>(DEFAULT_BALLS);
            magicBalls = new Queue<MagicBall>(DEFAULT_BALLS);

            for (int i = 0; i < DEFAULT_BALLS; i++)
            {
                balls.Enqueue(new Ball());
                speedBalls.Enqueue(new SpeedBall());
                magicBalls.Enqueue(new MagicBall());
            }

            activeBalls = new List<Ball>(DEFAULT_BALLS);
            activeSpeedBalls = new List<SpeedBall>(DEFAULT_BALLS);
            activeMagicBalls = new List<MagicBall>(DEFAULT_BALLS);
            

            cannonPos = new Vector2(window.width / 2, window.height - 70);
        }

        public static void Play()
        {
            while (window.opened)
            {

                GfxTools.Clean();
                //Input

                shootCoutdown -= window.deltaTime;


                if (window.mouseLeft)
                    MouseVector = new Vector2(GfxTools.Win.mouseX, GfxTools.Win.mouseY);


                if (window.GetKey(KeyCode.Esc))
                    break;

                if (shootCoutdown <= 0)
                {
                    if (window.GetKey(KeyCode.A) && balls.Count > 0)
                    {
                        Ball ball = balls.Dequeue();
                        activeBalls.Add(ball);
                        ball.Shoot(cannonPos);
                    }

                    else if (window.GetKey(KeyCode.S) && speedBalls.Count > 0)
                    {
                        SpeedBall speedBall = speedBalls.Dequeue();
                        activeSpeedBalls.Add(speedBall);
                        speedBall.Shoot(cannonPos);
                    }

                    else if (window.GetKey(KeyCode.D) && magicBalls.Count > 0)
                    {
                        MagicBall magicBall = magicBalls.Dequeue();
                        activeMagicBalls.Add(magicBall);
                        magicBall.Shoot(cannonPos);
                    }

                    shootCoutdown = DEFAULT_SHOOT_CD;
                }
                //Update 
                for (int i = activeBalls.Count -1; i >= 0; i--)

                {
                    if (activeBalls[i].IsActive)
                        activeBalls[i].Update();

                    else
                    {
                        balls.Enqueue(activeBalls[i]);
                        activeBalls.Remove(activeBalls[i]);
                    }
                }

                for (int i = activeMagicBalls.Count -1 ; i >= 0; i--)
                {
                    if (activeMagicBalls[i].IsActive)
                        activeMagicBalls[i].Update();

                    else
                    {
                        magicBalls.Enqueue(activeMagicBalls[i]);
                        activeMagicBalls.Remove(activeMagicBalls[i]);
                    }
                }

                for (int i = activeSpeedBalls.Count -1; i >= 0; i--)

                {
                    if (activeSpeedBalls[i].IsActive)
                        activeSpeedBalls[i].Update();


                    else
                    {
                        speedBalls.Enqueue(activeSpeedBalls[i]);
                        activeSpeedBalls.Remove(activeSpeedBalls[i]);
                    }
                }

                
                for (int i = 0; i < activeBalls.Count; i++)
                {
                    if (activeBalls[i].IsActive)
                        activeBalls[i].Draw();
                }

                for (int i = 0; i < activeMagicBalls.Count; i++)
                {
                    if (activeMagicBalls[i].IsActive)
                        activeMagicBalls[i].Draw();
                }

                for (int i = 0; i < activeSpeedBalls.Count; i++)
                {
                    if (activeSpeedBalls[i].IsActive)
                        activeSpeedBalls[i].Draw();
                }

                window.Blit();
            }
        }


        private static void DrawGUI()
        {

        }
    }
}
