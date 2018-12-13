using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    static class Game
    {
        
        static List<Scene> Scenes;


        public static Scene CurrentScene { get; private set; }
        public static int NumJoysticks;

        public static Window window;
        //static float totalTime;
        static float gravity;

        public static float DeltaTime { get { return window.deltaTime; } }
        public static float Gravity { get { return gravity; } }

        static Game()
        {
            window = new Window(1280, 720, "Run!", false);
            gravity = 300.0f;

            //scenes creation
            TitleScene aivTitle = new TitleScene("Assets/aivBG.png",KeyCode.Return);
            WelcomeScene welcome = new WelcomeScene("Assets/welcomeBg.jpg", KeyCode.Return);
            PlayScene playScene = new PlayScene();
            GameOverScene gameover = new GameOverScene();

            //scenes config
            aivTitle.NextScene = welcome;
            aivTitle.ShowTime = 3;
            //aivTitle.FadeOut = false;
            //aivTitle.FadeIn = false;

            welcome.NextScene = playScene;
            welcome.FadeOut = false;

            playScene.NextScene = gameover;
            gameover.NextScene = playScene;

            aivTitle.Start();
            CurrentScene = aivTitle;


            string[] joysticks = Game.window.Joysticks;

            for(int i=0;i<joysticks.Length;i++)
            {
                if (joysticks[i]!=null && joysticks[i]!="Unmapped Controller")
                    NumJoysticks++;
            }

        }

        public static void Play()
        {
            while (window.opened)
            {
                if (!CurrentScene.IsPlaying)
                {
                    //next scene
                    if (CurrentScene.NextScene != null)
                    {
                        CurrentScene.OnExit();
                        CurrentScene = CurrentScene.NextScene;
                        CurrentScene.Start();
                    }
                    else
                    {
                        return;
                    }
                }

                //totalTime += GfxTools.Win.deltaTime;
                //Console.SetCursorPosition(0, 0);
                //Console.Write((1 / window.deltaTime)+"                   ");
               
                //Input
                if (window.GetKey(KeyCode.Esc))
                    break;

                CurrentScene.Input();

                //Update
                CurrentScene.Update();

                //Draw
                CurrentScene.Draw();

                window.Update();
            }
        }
    }
}
