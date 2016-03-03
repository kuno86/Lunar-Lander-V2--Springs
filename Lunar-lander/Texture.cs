using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lunar_lander  //Only purpose is to hold all the textures
{
    class Texture
    {
        public static int naru = MyImage.LoadTexture(RootThingy.exePath + @"\naru.bmp");
        public static int gras = MyImage.LoadTexture(RootThingy.exePath + @"\gras.bmp");
        public static int bricks = MyImage.LoadTexture(RootThingy.exePath + @"\brickwall.bmp");
        public static int moon = MyImage.LoadTexture(RootThingy.exePath + @"\moon.bmp");
    }
}

