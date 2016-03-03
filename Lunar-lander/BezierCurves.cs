using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Lunar_lander
{
    class BezierCurves
    {


        public static Vector3d quadraticBezier(Vector3d p0, Vector3d p1, Vector3d p2, double t)
        {
            Vector3d pFinal = new Vector3d(0, 0, 0);
            pFinal.X = Math.Pow(1 - t, 2) * p0.X +
                (1 - t) * 2 * t * p1.X +
                t * t * p2.X;
            pFinal.Y = Math.Pow(1 - t, 2) * p0.Y +
                (1 - t) * 2 * t * p1.Y +
                t * t * p2.Y;
            pFinal.Z = Math.Pow(1 - t, 2) * p0.Z +
                (1 - t) * 2 * t * p1.Z +
                t * t * p2.Z;

            return pFinal;
        }

        public static Vector3d cubicBezier(Vector3d p0, Vector3d p1, Vector3d p2, Vector3d p3, double t)
        {
            Vector3d pFinal = new Vector3d(0,0,0);

            pFinal.X = Math.Pow(1 - t, 3) * p0.X +
                Math.Pow(1 - t, 2) * 3 * t * p1.X +
                (1 - t) * 3 * t * t * p2.X +
                t * t * t * p3.X;
            pFinal.Y = Math.Pow(1 - t, 3) * p0.Y +
                Math.Pow(1 - t, 2) * 3 * t * p1.Y +
                (1 - t) * 3 * t * t * p2.Y +
                t * t * t * p3.Y;
            pFinal.X = Math.Pow(1 - t, 3) * p0.Z +
                Math.Pow(1 - t, 2) * 3 * t * p1.Z +
                (1 - t) * 3 * t * t * p2.Z +
                t * t * t * p3.Z;
            
            return pFinal;
        }


        public static void multicurve(Vector3d[] points, double t)
        {
            Vector3d p0 = new Vector3d();
            Vector3d p1 = new Vector3d();
            double midX=0;
            double midY=0;
            double midZ=0;
            
            GL.LineWidth(3);
            GL.Color4(1.0f, 0.0f, 0.0f, 1.0f);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(points[0]);

            for (int i = 1; i < points.Length - 2; i++)
            {
                p0 = points[i];
                p1 = points[i+1];
                midX = (p0.X + p1.X) / 2;
                midY = (p0.Y + p1.Y) / 2;
                midZ = (p0.Z + p1.Z) / 2;
                quadraticBezier(p0, new Vector3d(midX, midY, midZ), p1, t);
                
            }
            p0 = points[points.Length - 2];
            p1 = points[points.Length - 1];
            quadraticBezier(p0, new Vector3d(midX, midY, midZ), p1, t);
        }






    }
}
