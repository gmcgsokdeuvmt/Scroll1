using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll1
{
    class Player : asd.TextureObject2D
    {
        static asd.Vector2DF Left = new asd.Vector2DF(-1,0);
        static asd.Vector2DF Right = new asd.Vector2DF(1, 0);
        static asd.Vector2DF Up = new asd.Vector2DF(0, -1);
        static asd.Vector2DF Down = new asd.Vector2DF(0, 1);
        
        public asd.Vector2DF speed;
        int jumpCount;
        int jumpTime;
        const float jumpSpeed = 2.06f;
        const int initJumpTime = 10;
        float jumpingSpeed;
        List<Skewer> skewers = new List<Skewer>();
        int skewerJumpTime;

        asd.Texture2D texRight =  asd.Engine.Graphics.CreateTexture2D("Resources/KaishuyaR.png");
        asd.Texture2D texLeft = asd.Engine.Graphics.CreateTexture2D("Resources/KaishuyaL.png"); 
        asd.Texture2D texDownR = asd.Engine.Graphics.CreateTexture2D("Resources/KaishuyaDR.png");
        asd.Texture2D texDownL = asd.Engine.Graphics.CreateTexture2D("Resources/KaishuyaDL.png");
        asd.Texture2D texUp;
        
        Marker marker;
        public Player()
        {
            Texture = texRight;
            speed = new asd.Vector2DF();
            CenterPosition = Texture.Size.To2DF()/2;
            jumpCount = 0;
            jumpTime = 0;
            jumpingSpeed = 0;
            marker = new Marker();
            this.AddChild(marker, asd.ChildManagementMode.RegistrationToLayer, asd.ChildTransformingMode.Nothing);
            skewerJumpTime = -1;

            Position = new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 0);
        }

        protected override void OnUpdate()
        {
            speed += 0.6f * Down;
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.A) == asd.KeyState.Hold)
            {
                speed.X += 5 * Left.X / 5;
                Texture = texLeft;
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.D) == asd.KeyState.Hold)
            {
                speed.X += 5 * Right.X / 5;
                Texture = texRight;
            }
            speed.X *= 0.9f;

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.S) == asd.KeyState.Push)
            {
                texUp = Texture;
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.S) == asd.KeyState.Hold)
            {
                if (texUp.Equals(texRight))
                {
                    Texture = texDownR;
                }
                else if (texUp.Equals(texLeft))
                {
                    Texture = texDownL;
                }
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.S) == asd.KeyState.Release)
            {
                Texture = texUp;
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.W) == asd.KeyState.Push && jumpCount++ < 2)
            {
                jumpTime = initJumpTime;
                if (Math.Abs(speed.X) > 4.465f)
                    jumpingSpeed = jumpSpeed * 1.2f;
                else
                    jumpingSpeed = jumpSpeed;
                speed.Y = jumpingSpeed * Up.Y;
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.W) == asd.KeyState.Hold && jumpTime-- > 0)
            {
                jumpingSpeed -= 0.2f;
                speed.Y += jumpingSpeed*Up.Y;
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.W) == asd.KeyState.Release && speed.Y * Up.Y >= 0)
            {
                speed.Y *= 0.4f;
            }
            if (Position.Y >= Program.CurrentStage.FloorY - Texture.Size.Y/2)
            {
                if (speed.Y >= 0)
                {
                    speed.Y = 0;
                    jumpCount = 0;
                }
                Position = new asd.Vector2DF(Position.X, Program.CurrentStage.FloorY - Texture.Size.Y/2);
                speed *= 0.95f;
            }
            
            updateSkewer();
            if (asd.Engine.Mouse.RightButton.ButtonState == asd.MouseButtonState.Hold)
            {
                jumpCount = 2;
                asd.Vector2DF dir = new asd.Vector2DF();
                if (skewers.Count == 1)
                {
                    dir = (skewers.First().Position - this.Position).Normal;
                }
                else if (skewers.Count == 2)
                {
                    dir = (skewers.ElementAt(0).Position + skewers.ElementAt(1).Position - this.Position * 2).Normal;
                }
                float brake = asd.Vector2DF.Dot(dir, speed);
                if (brake < 0)
                {
                    speed -= brake * dir * 1.01f;
                    speed += 0.6f * Down;
                    if (dir.Y < 0)
                    {
                        speed -= dir / dir.Y * 0.6f;
                    }
                }
            }
            else if (asd.Engine.Mouse.RightButton.ButtonState == asd.MouseButtonState.Release)
            {
                asd.Vector2DF dir = new asd.Vector2DF();
                if (skewers.Count == 1)
                {
                    dir = (skewers.First().Position - this.Position)/10;
                }
                else if (skewers.Count == 2)
                {
                    dir = (skewers.ElementAt(0).Position + skewers.ElementAt(1).Position - this.Position * 2) / 10;
                }
                if (dir.Length != 0)
                {
                    dir.Length += 10.0f;
                }
                if (dir.Length > 50.0f)
                {
                    dir.Length = 50.0f;
                }
                if (dir.Y < 0) 
                { 
                    dir.Y *= 0.5f; 
                }
                if (!(Texture.Equals(texDownL) || Texture.Equals(texDownR)))
                {
                    speed += dir;
                }
                if (speed.X != 0)
                {
                    Texture = (speed.X >= 0) ? texRight : texLeft;
                }
                skewerJumpTime = 0;

            }
            if (skewerJumpTime >= 0 && ++skewerJumpTime == 20)
            {
                skewerJumpTime = -1;
                while (skewers.Count != 0)
                {
                    skewers.ElementAt(0).Dispose();
                    skewers.RemoveAt(0);
                }
                
            }

            if (speed.Length < 0.01f)
            {
                speed *= 0;
            }
            else
            {
                //player.CheckCollide
            }
            Position += speed;


        }

        void updateSkewer()
        {
            skewers.Clear();
            for (int i = 0; i < marker.Children.Count(); i++)
            {
                if (marker.Children.ElementAt(i).IsAlive)
                {
                    skewers.Add(marker.Children.ElementAt(i) as Skewer);
                }
            }
        }
    }
}
