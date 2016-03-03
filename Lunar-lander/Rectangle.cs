using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Lunar_lander
{
    class Rectangle : BaseObj
    {
        private short rot = 0;
        public Rectangle(double x, double y, double z, double heigthX, double widthY, double depthZ)
            : base()
        {
            this.type = "rectangle";
            this.pos.X = x;
            this.pos.Y = y;
            this.pos.Z = z;
            this.dim.X = heigthX;
            this.dim.Y = widthY;
            this.dim.Z = depthZ;
            this.mass = dim.X * dim.Y * dim.Z;
            this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public Rectangle(Vector3d pos, Vector3d dim)
            : base()
        {
            this.type = "rectangle";
            this.pos = pos;
            this.dim = dim;
            this.mass = dim.X * dim.Y * dim.Z;
            this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }


        public override void tick()
        {
            vel.Add(RootThingy.gravity);

            vel.Mult(friction);

            if (pos.Y <= 0)
                vel.Y = vel.Y * bounce;

            pos.Add(vel);

            rot++;
            if (rot >= 360)
                rot = 0;

            //r = 0.1;

        }


        public override void render()
        {
            GL.TexCoord2(0, 0);
            GL.TexCoord2(1, 0);
            GL.TexCoord2(1, 1);
            GL.TexCoord2(0, 1);


            GL.LineWidth(1);
            GL.PushMatrix();

            GL.Rotate(rot, 0, 1, 0);

            MyImage.beginDraw2D();

            GL.BindTexture(TextureTarget.Texture2D, Texture.bricks);

            GL.Color4(color);
            GL.Begin(PrimitiveType.Quads);  //Floor
            GL.TexCoord2(0, 0);
            GL.Vertex3(pos);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z + dim.Z);
            GL.TexCoord2(0, 1);
            GL.Vertex3(pos.X, pos.Y, pos.Z + dim.Z);
            GL.End();            

            GL.Begin(PrimitiveType.QuadStrip);  //Walls
            GL.TexCoord2(0, 1);
            GL.Vertex3(pos);
            GL.TexCoord2(0, 0);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z);
            GL.TexCoord2(0, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z + dim.Z);
            GL.TexCoord2(0, 0);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z + dim.Z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X, pos.Y, pos.Z + dim.Z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z + dim.Z);
            GL.TexCoord2(0, 1);
            GL.Vertex3(pos);
            GL.TexCoord2(0, 0);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z);
            GL.End();

            GL.Begin(PrimitiveType.Quads);  //Ceiling
            GL.TexCoord2(0, 0);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z + dim.Z);
            GL.TexCoord2(0, 1);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z + dim.Z);
            GL.End();
                      

            GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Begin(PrimitiveType.LineStrip);  //Outlines
            GL.Vertex3(pos);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z + dim.Z);
            GL.Vertex3(pos.X, pos.Y, pos.Z + dim.Z);
            GL.Vertex3(pos);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z + dim.Z);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z + dim.Z);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z + dim.Z);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z + dim.Z);
            GL.Vertex3(pos.X, pos.Y, pos.Z + dim.Z);
            GL.Vertex3(pos.X, pos.Y + dim.Y, pos.Z + dim.Z);
            GL.End();
            GL.Color4(1.0f, 1.0f, 1.0f, 1.0f);

            MyImage.endDraw2D();

            GL.PopMatrix();
            GL.LineWidth(2);
        }

    }
}
