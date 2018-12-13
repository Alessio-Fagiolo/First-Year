using Aiv.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balls
{
    static class Game
    {
        static Window window;
        static Vector2 gravity = new Vector2(0, 200);

        static Ball palla = new Ball();
        static SpeedBall pallaLesta = new SpeedBall();
        static MagicBall pallaMagica = new MagicBall();
        static Vector2 standardPosition;
        static Vector2 magicPosition;
        static Vector2 speedPosition;


        static public Vector2 Gravity { get { return gravity; } }
        static public float DeltaTime { get { return window.deltaTime; } }

        static Game()
        {
            window = new Window(800, 600, "Balls", PixelFormat.RGB);
            standardPosition = new Vector2(window.width / 2, window.height);
            magicPosition = new Vector2( 70, window.height);
            speedPosition = new Vector2(window.width -70, window.height);
            GfxTools.Init(window);
        }




        public static void Play()
        {
            while (window.opened)
            {


                

                GfxTools.Clean();

                //input
                if (window.GetKey(KeyCode.S))
                {
                    palla.Input(standardPosition);

                }

                if(window.GetKey(KeyCode.A))
                {
                    pallaMagica.Input(magicPosition);
                }

                if(window.GetKey(KeyCode.D))
                {
                    pallaLesta.Input(speedPosition);
                }



                //update
                if(palla.IsActived)
                palla.Update();

                if(pallaMagica.IsActived)
                pallaMagica.Update();

                if (pallaLesta.IsActived)
                    pallaLesta.Update();



                //draw
                if (palla.IsActived)
                palla.Draw();

                if (pallaMagica.IsActived)
                pallaMagica.Draw();

                if (pallaLesta.IsActived)
                    pallaLesta.Draw();


                window.Blit();
            }
        }

        
    }
}