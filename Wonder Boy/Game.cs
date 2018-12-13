using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FAST2D
{
    static class Game
    {
        static public Window window;
        static Player player;
        static Background background;
        
        static public float deltaTime { get { return window.deltaTime; } }

        

        static Game()
        {
            window = new Window(800, 500, "boy");
            player = new Player();
            background = new Background();
            DrawManager.AddItem(background);
            DrawManager.AddItem(player);
           
           
        }



        public static void Play()
        {
            while (window.IsOpened)
            {
                //input
                player.Input();

                //update
                background.Update();
                player.Update();




                //draw
                
                DrawManager.Draw();


                window.Update();
            }

        }
       
    }
}
