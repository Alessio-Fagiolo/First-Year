using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    class PlayScene : Scene
    {
        protected Player player;
        protected Background bg;

        public override void Start()
        {
            base.Start();
            GfxManager.AddTexture("player", "Assets/player_ship.png");
            GfxManager.AddTexture("bg", "Assets/spaceBg.png");
            GfxManager.AddTexture("bullets", "Assets/beams.png");
            GfxManager.AddTexture("enemy", "Assets/enemy_ship.png");
            GfxManager.AddTexture("enemy2", "Assets/redEnemy_ship.png");
            GfxManager.AddTexture("fireGlobe", "Assets/fireGlobe.png");
            GfxManager.AddTexture("blueLaser", "Assets/blueLaser.png");
            GfxManager.AddTexture("playerBar", "Assets/loadingBar_bar.png");
            GfxManager.AddTexture("barFrame", "Assets/loadingBar_frame.png");
            GfxManager.AddTexture("powerUp_Nrg", "Assets/powerUp_battery.png");

            UpdateManager.Init();
            DrawManager.Init();
            PhysicsManager.Init();
            BulletManager.Init();
            SpawnManager.Init();

            player = new Player("player", new Vector2(Game.window.Width / 2, Game.window.Height / 2));
            bg = new Background("bg", Vector2.Zero, -220);
        }
        public override void Draw()
        {
            DrawManager.Draw();
        }

        public override void Input()
        {
            if(player.IsActive)
                player.Input();
        }

        public override void Update()
        {
            PhysicsManager.Update();
            UpdateManager.Update();
            PhysicsManager.CheckCollisions();
            SpawnManager.Update();
        }

        public override void OnExit()
        {
            UpdateManager.RemoveAll();
            DrawManager.RemoveAll();
            PhysicsManager.RemoveAll();
            GfxManager.RemoveAll();
            BulletManager.RemoveAll();
            SpawnManager.RemoveAll();

        }
    }
}
