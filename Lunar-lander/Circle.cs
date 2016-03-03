using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Lunar_lander
{
    class Circle : BaseObj
    {
        private const double DEG2RAD = 3.14159 / 180;
                
        
        public Circle(double x, double y, double z, double radius)
            : base()
        {
            this.type = "circle";
            this.pos.X = x;
            this.pos.Y = y;
            this.pos.Z = z;
            this.r = radius;
            this.mass = (4 / 3) * Math.PI * (r * r * r);
            this.gravity.Y = mass / (r * r) * -1;
            this.texture = MyImage.LoadTexture(RootThingy.exePath+@"\circle.bmp");
            //this.color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(200, 255));
            //this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public Circle(Vector3d pos, double radius, Vector3d vel)
            : base()
        {
            this.type = "circle";
            this.pos = pos;
            this.r = radius;
            this.vel = vel;
            this.mass = (4 / 3) * Math.PI * (r * r * r);
            this.gravity.Y = mass / (r * r) * -1;
            //this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public override void tick()
        {
            vel.Add(RootThingy.gravity);

            vel.Mult(friction);
            
            if (pos.Y - r <= 0)
                vel.Y = vel.Y * bounce;

            pos.Add(vel);

            //r = 0.1;
            
        }

        public override void render()
        {
            GL.LineWidth(1);
            GL.PushMatrix();
            
            int i, j;
            int lats = 16; 
            int longs = 16;
            for (i = 0; i <= lats; i++)
            {
                double lat0 = Math.PI * (-0.5 + (double)(i - 1) / lats);
                double z0 = Math.Sin(lat0) * r;
                double zr0 = Math.Cos(lat0);

                double lat1 = Math.PI * (-0.5 + (double)i / lats);
                double z1 = Math.Sin(lat1) * r;
                double zr1 = Math.Cos(lat1);

                GL.Color4(color);
                GL.Begin(PrimitiveType.QuadStrip);
                for (j = 0; j <= longs; j++)
                {
                    double lng = 2 * Math.PI * (double)(j - 1) / longs;
                    double x = Math.Cos(lng) * r;
                    double y = Math.Sin(lng) * r;

                    GL.Normal3(pos.X + x * zr0, pos.Y + y * zr0, pos.Z + z0);
                    GL.Vertex3(pos.X + x * zr0, pos.Y + y * zr0, pos.Z + z0);
                    GL.Normal3(pos.X + x * zr1, pos.Y + y * zr1, pos.Z + z1);
                    GL.Vertex3(pos.X + x * zr1, pos.Y + y * zr1, pos.Z + z1);
                }
                GL.End();

                GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                GL.Begin(PrimitiveType.LineLoop);
                for (j = 0; j <= longs; j++)
                {
                    double lng = 2 * Math.PI * (double)(j - 1) / longs;
                    double x = Math.Cos(lng) * r;
                    double y = Math.Sin(lng) * r;

                    GL.Normal3(pos.X + x * zr0, pos.Y + y * zr0, pos.Z + z0);
                    GL.Vertex3(pos.X + x * zr0, pos.Y + y * zr0, pos.Z + z0);
                    GL.Normal3(pos.X + x * zr1, pos.Y + y * zr1, pos.Z + z1);
                    GL.Vertex3(pos.X + x * zr1, pos.Y + y * zr1, pos.Z + z1);
                }
                GL.End();
            }
            GL.PopMatrix();
            GL.LineWidth(2);
        }



    }
}
