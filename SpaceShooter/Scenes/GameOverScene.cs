using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    class GameOverScene : Scene
    {
        Sprite background;
        Texture bgTexture;

        public override void Start()
        {
            base.Start();
            bgTexture = new Texture("Assets/gameOverBg.png");
            background = new Sprite(Game.window.Width, Game.window.Height);
        }

        public override void Draw()
        {
            background.DrawTexture(bgTexture);
        }

        public override void Input()
        {
            if (Game.window.GetKey(KeyCode.Y))
            {
                IsPlaying = false;
            }
            else if (Game.window.GetKey(KeyCode.N))
            {
                IsPlaying = false;
                NextScene = null;
            }
        }

        public override void Update()
        {
            
        }
    }
}
