using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Lunar_lander
{
    class Triangle : BaseObj
    {
        private short rot = 0;
        private Vector3d p0, p1, p2;
        private double depth;

        public Triangle(double x0, double y0, double z0, double x1, double y1, double z1, double x2, double y2, double z2, double depth)
            : base()
        {
            this.type = "triangle";
            this.p0 = new Vector3d(x0, y0, z0);
            this.p1 = new Vector3d(x1, y1, z1);
            this.p2 = new Vector3d(x2, y2, z2);
            this.depth = depth;
            this.pos.X = Math.Min(Math.Min(x0, x1), x2);
            this.pos.Y = Math.Min(Math.Min(y0, y1), y2);
            this.pos.Z = Math.Min(Math.Min(z0, z1), z2);
            this.dim.X = Math.Abs(Math.Max(Math.Max(x0, x1), x2)) - Math.Abs(Math.Min(Math.Min(x0, x1), x2));
            this.dim.Y = Math.Abs(Math.Max(Math.Max(y0, y1), y2)) - Math.Abs(Math.Min(Math.Min(y0, y1), y2));
            this.dim.Z = Math.Abs(Math.Max(Math.Max(z0, z1), z2)) - Math.Abs(Math.Min(Math.Min(z0, z1), z2));
            this.mass = (dim.X * dim.Y * dim.Z) / 2;
            this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public Triangle(Vector3d p0, Vector3d p1, Vector3d p2, double depth)
            : base()
        {
            this.type = "triangle";
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.depth = depth;
            this.pos = new Vector3d(Math.Min(Math.Min(p0.X, p1.X), p2.X), Math.Min(Math.Min(p0.Y, p1.Y), p2.Y), Math.Min(Math.Min(p0.Z, p1.Z), p2.Z));
            this.dim.X = Math.Abs(Math.Max(Math.Max(p0.X, p1.X), p2.X)) - Math.Abs(Math.Min(Math.Min(p0.X, p1.X), p2.X));
            this.dim.Y = Math.Abs(Math.Max(Math.Max(p0.Y, p1.Y), p2.Y)) - Math.Abs(Math.Min(Math.Min(p0.Y, p1.Y), p2.Y));
            this.dim.Z = Math.Abs(Math.Max(Math.Max(p0.Z, p1.Z), p2.Z)) - Math.Abs(Math.Min(Math.Min(p0.Z, p1.Z), p2.Z));
            this.mass = (dim.X * dim.Y * dim.Z) /2;
            this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }


        public override void tick()
        {
            vel.Add(RootThingy.gravity);

            vel.Mult(friction);

            if (pos.Y <= 0)
                vel.Y = vel.Y * bounce;

            pos.Add(vel);

            

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
            GL.Begin(PrimitiveType.QuadStrip);
            GL.Color4(1.0f, 0.0f, 0.0f, 1.0f);
            GL.TexCoord2(0, 0);
            GL.Vertex3(pos);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X, pos.Y, pos.Z + depth);
            GL.TexCoord2(0, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z + depth);

            GL.TexCoord2(0, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z + depth);

            GL.TexCoord2(0, 0);
            GL.Vertex3(pos);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X, pos.Y, pos.Z + depth);

            GL.End();

            GL.Begin(PrimitiveType.Triangles);  //Walls
            GL.TexCoord2(0, 0);
            GL.Vertex3(pos);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z);
            GL.TexCoord2(0, 1);
            GL.End();

            GL.Begin(PrimitiveType.Triangles);  //Walls
            GL.TexCoord2(0, 0);
            GL.Vertex3(pos.X, pos.Y, pos.Z + depth);
            GL.TexCoord2(1, 0);
            GL.Vertex3(pos.X + dim.X, pos.Y, pos.Z + depth);
            GL.TexCoord2(1, 1);
            GL.Vertex3(pos.X + dim.X, pos.Y + dim.Y, pos.Z + depth);
            GL.TexCoord2(0, 1);
            GL.End();


            GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Begin(PrimitiveType.LineStrip);  //Outlines
            
            GL.End();
            GL.Color4(1.0f, 1.0f, 1.0f, 1.0f);

            MyImage.endDraw2D();

            GL.PopMatrix();
            GL.LineWidth(2);
        }

    }
}

