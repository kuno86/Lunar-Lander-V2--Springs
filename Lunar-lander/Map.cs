using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Lunar_lander
{
    class Map
    {
        private int naru;

        public Map()
        {
            ;
        }

        public void process()
        {
            GL.Color3(1.0f, 1.0f, 1.0f);


            MyImage.beginDraw2D();



            GL.BindTexture(TextureTarget.Texture2D, Texture.moon);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-0.512, -0.512, -1);
            GL.TexCoord2(1, 0);
            GL.Vertex3(0.512, -0.512, -1);
            GL.TexCoord2(1, 1);
            GL.Vertex3(0.512, 0.512, -1);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-0.512, 0.512, -1);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, Texture.moon);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-0.512, -0.512, 1);
            GL.TexCoord2(1, 0);
            GL.Vertex3(0.512, -0.512, 1);
            GL.TexCoord2(1, 1);
            GL.Vertex3(0.512, 0.512, 1);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-0.512, 0.512, 1);
            GL.End();

            //GL.BindTexture(TextureTarget.Texture2D, Texture.naru);
            //GL.Begin(PrimitiveType.Quads);
            //GL.TexCoord2(0, 0);
            //GL.Vertex3(-0.96, 0, -0.6);
            //GL.TexCoord2(1, 0);
            //GL.Vertex3(0.96, 0, -0.6);
            //GL.TexCoord2(1, 1);
            //GL.Vertex3(0.96, 0, 0.6);
            //GL.TexCoord2(0, 1);
            //GL.Vertex3(-0.96, 0, 0.6);
            //GL.End();


            GL.BindTexture(TextureTarget.Texture2D, Texture.gras);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-1, 0, -1);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1, 0, -1);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1, 0, 1);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-1, 0, 1);
            GL.End();


            

            MyImage.endDraw2D();

        }

    }
}
