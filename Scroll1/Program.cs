using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll1
{
    class Program
    {
        public static ObjectFactory Factory = new ObjectFactory();
        public static float AxisX, AxisY;
        public static Stage CurrentStage;
        public static int floorY;
        static void Main(string[] args)
        {
            asd.Engine.Initialize("SCROLL1", 1280, 720, new asd.EngineOption());
            floorY = asd.Engine.WindowSize.Y * 4;

            asd.Scene gameScene = new asd.Scene();
            asd.Layer2D gameLayer = new asd.Layer2D();
            asd.Layer2D backgroundLayer = new asd.Layer2D();
            gameLayer.DrawingPriority = 0;
            backgroundLayer.DrawingPriority = -1;

            var player = Factory.create("player");
            gameLayer.AddObject(player);
            var stage0 = Factory.create("stage0");
            gameLayer.AddObject(stage0);
            CurrentStage = stage0 as Stage;
            var background = Factory.create("gameBackground");
            backgroundLayer.AddObject(background);
            gameScene.AddLayer(gameLayer);
            gameScene.AddLayer(backgroundLayer);
            asd.Engine.ChangeScene(gameScene);

            asd.RectI playView = new asd.RectI(0, 0, asd.Engine.WindowSize.X, asd.Engine.WindowSize.Y);
            asd.RectI backView = new asd.RectI(0, 0, asd.Engine.WindowSize.X, asd.Engine.WindowSize.Y);

            var backgroundCamera = new asd.CameraObject2D();
            backgroundCamera.Src = backgroundCamera.Dst = playView;
            backgroundLayer.AddObject(backgroundCamera);

            var gameCamera = new asd.CameraObject2D();
            gameCamera.Src = gameCamera.Dst = playView;
            gameLayer.AddObject(gameCamera);

            AxisX = player.Position.X;
            AxisY = player.Position.Y;

            var bgm = asd.Engine.Sound.CreateSoundSource("Resources/demo.wav", true);
            bgm.IsLoopingMode = true;
            int id_bgm = asd.Engine.Sound.Play(bgm);
            asd.Engine.Sound.SetVolume(id_bgm, 0.2f);

            while (asd.Engine.DoEvents())
            {
                if (player.Position.X - AxisX > asd.Engine.WindowSize.X / 12)
                {
                    AxisX = player.Position.X - asd.Engine.WindowSize.X / 12;
                }
                else if (player.Position.X - AxisX < -asd.Engine.WindowSize.X / 12) 
                {
                    AxisX = player.Position.X + asd.Engine.WindowSize.X / 12;
                }
                if (player.Position.Y - AxisY > asd.Engine.WindowSize.Y / 6)
                {
                    AxisY = player.Position.Y - asd.Engine.WindowSize.Y / 6;
                }
                else if (player.Position.Y - AxisY < -asd.Engine.WindowSize.Y / 6)
                {
                    AxisY = player.Position.Y + asd.Engine.WindowSize.Y / 6;
                }
                playView.X = backView.X = (int)(AxisX-asd.Engine.WindowSize.X/3);
                playView.Y = (int)(AxisY - asd.Engine.WindowSize.Y / 2);


                backgroundCamera.Src = gameCamera.Src = playView;
                asd.Engine.Update();

                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Escape) == asd.KeyState.Push)
                {
                    break;
                }
            }

            asd.Engine.Terminate();
        }
    }
}
