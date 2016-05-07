using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll1
{
    class Collision
    {
        public static void CheckCollision(asd.TextureObject2D obj)
        {
            if (obj as Player != null)
            {
                Player player = obj as Player;
                asd.Vector2DF[] hoge = getNewPosSpeed(player.Position, player.speed);
                player.Position = hoge[0];
                player.speed = hoge[0];
            }
            else if (obj as Skewer != null)
            {
                Skewer skewer = obj as Skewer;
                asd.Vector2DF[] hoge = getNewPosSpeed(skewer.Position, skewer.speed);
                skewer.Position = hoge[0];
                skewer.speed = hoge[1];
            }
        }

        private static asd.Vector2DF[] getNewPosSpeed(asd.Vector2DF pos, asd.Vector2DF speed)
        {
            asd.Vector2DF[] result = {pos, speed};
            if (speed.Length == 0)
            {
                return result;
            }

            asd.Vector2DF tempSpeed = new asd.Vector2DF(0,0);
            asd.Vector2DF dir = speed.Normal;
            if (speed.X * speed.X > speed.Y * speed.Y)
            {
                dir = dir / dir.X * 32;
                while (tempSpeed.Length < speed.Length)
                {
                    tempSpeed += dir;
                    int x = (int)(pos + tempSpeed).X / 32;
                    int y = (int)(pos + tempSpeed).Y / 32;
                    if (Program.CurrentStage.bitmap.GetPixel(x, y).R == 255)
                    {
                        int dx = (dir.X > 0) ? 1 : -1;
                        if (Program.CurrentStage.bitmap.GetPixel(x - dx, y).R == 255)
                        {
                            int dy = (dir.Y > 0) ? 1 : -1;
                        }
                        else
                        {
                            result[0] = new asd.Vector2DF((x - dx) * 32, (pos + tempSpeed).Y);
                        }
                    }
                }
            }
            else
            {
                dir = dir / dir.Y * 32;
                while (tempSpeed.Length < speed.Length)
                {
                    tempSpeed += dir;
                    int x = (int)(pos + tempSpeed).X / 32;
                    int y = (int)(pos + tempSpeed).Y / 32;
                    if (Program.CurrentStage.bitmap.GetPixel(x, y).R == 255)
                    {
                        int dy = (dir.Y > 0) ? 1 : -1;
                        if (Program.CurrentStage.bitmap.GetPixel(x, y - dy).R == 255)
                        {
                            int dx = (dir.X > 0) ? 1 : -1;
                        }
                    }
                }
            }
            return result;
        }
    }
}
