using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll1
{
    class Skewer : asd.TextureObject2D
    {
        public asd.Vector2DF speed;
        private int debugCount;
        public Skewer(asd.Vector2DF pos, asd.Vector2DF dir)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Skewer.png");
            Position = pos;
            this.speed = dir.Normal * 80.0f;
            Angle = speed.Degree;
            CenterPosition = Texture.Size.To2DF() / 2;
            debugCount = 0;
        }

        protected override void OnUpdate()
        {
            speed *= 0.9f;
            speed += 0.6f * new asd.Vector2DF(0, 1);

            if (debugCount++ >5)
            {
                speed *= 0;
            }

            if (Position.Y >= Program.CurrentStage.FloorY - Texture.Size.Y / 2)
            {
                if (speed.Y >= 0)
                {
                    speed *= 0;
                }
            }
            Position += speed;
        }
    }
}
