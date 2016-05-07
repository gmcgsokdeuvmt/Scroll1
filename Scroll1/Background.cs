using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll1
{
    class Background : asd.TextureObject2D
    {
        public Background(string str)
        {
            if (str == "game")
            {
                Texture = asd.Engine.Graphics.CreateTexture2D("Resources/bg1.png");
            }
        }
    }
}
