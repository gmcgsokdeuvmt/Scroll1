using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll1
{
    class ObjectFactory
    {
        public ObjectFactory() { }

        public asd.Object2D create(string str)
        {
            asd.Object2D obj;
            if (str == "player")
            {
                obj = new Player();
            }
            else if (str == "gameBackground")
            {
                obj = new Background("game");
            }
            else if (str == "stage0")
            {
                obj = new Stage(0);
            }
            else
            {
                obj = new asd.TextureObject2D();
            }
            return obj;
        }
    }
}
