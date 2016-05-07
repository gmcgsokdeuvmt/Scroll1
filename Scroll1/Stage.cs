using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Scroll1
{
    class Stage : asd.MapObject2D
    {
        int number;
        public Bitmap bitmap;
        public int Height;
        public int Width;
        public int FloorY;


        asd.Texture2D texRed = asd.Engine.Graphics.CreateTexture2D("Resources/Red.png"); 
        asd.Texture2D texBlue = asd.Engine.Graphics.CreateTexture2D("Resources/Blue.png");
        asd.Texture2D texCream = asd.Engine.Graphics.CreateTexture2D("Resources/Cream.png");
        asd.Texture2D texSky = asd.Engine.Graphics.CreateTexture2D("Resources/Sky.png");
        asd.Texture2D texOrange = asd.Engine.Graphics.CreateTexture2D("Resources/Orange.png");

        public Stage(int num)
        {
            number = num;
            // num ごとにビットマップを読み込む。これは地形になる。
            // red がブロック

            if (num == 0)
            {
                bitmap = new Bitmap("Resources/Stage0.bmp");
            }
            Width = (bitmap.Width-1) * 32;
            Height = (bitmap.Height-1) * 32;
            FloorY = Height;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    if (bitmap.GetPixel(x, y).R == 255)
                    {
                        var block = new Block();
                        block.Texture = texCream;
                        block.Position = new asd.Vector2DF(x * 32, y * 32);
                        this.AddChip(block);
                    }
                    else
                    {
                        var block = new Block();
                        block.Texture = texOrange;
                        block.Position = new asd.Vector2DF(x * 32, y * 32);
                        this.AddChip(block);
                    }
                }
            }
            DrawingPriority = -1;
        }
    }
}
