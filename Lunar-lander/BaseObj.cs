using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Lunar_lander
{
    class BaseObj
    {
        public struct Point
        {
            public double x;
            public double y;
        }

        public struct Rect
        {
            public double x;
            public double y;
            public double w;
            public double h;
        }

        public double springLength;
        public double constant;
        public int springObjId1;
        public int springObjId2;

        public Vector3d dim;

        public double r;



        public int id;                
        public string type = "point";

        public Vector3d pos;
        public Vector3d vel = new Vector3d(0, 0, 0);

        public double friction = 0.92;     //1=None  &   0=100%
        public double mass = 0;
        public double bounce = -0.9;

        public Vector3d gravity = new Vector3d(0, 0.00, 0);

        public int texture;
        public Color4 color = new Color4(1.0f,1.0f,1.0f,1.0f);  //Withe


        public virtual void tick() { ;}

        public virtual void render() { ;}


        public static void test(int[] numbers)
        {
            foreach (int i in numbers)
                Console.WriteLine(i);
            //Console.ReadKey();
        }



        public static double distance2D(double x1, double y1, double x2, double y2)
        {
            double x = Math.Abs(x1 - x2);
            double y = Math.Abs(y1 - y2);
            return Math.Sqrt((x * x) + (y * y));
        }

        public static double distance2D(Point p1, Point p2)
        {
            double x = Math.Abs(p1.x - p2.x);
            double y = Math.Abs(p1.x - p2.y);
            return Math.Sqrt((x * x) + (y * y));
        }

        public static double distance3D(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double x = Math.Abs(x1 - x2);
            double y = Math.Abs(y1 - y2);
            double z = Math.Abs(z1 - z2);
            return Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        public static double distance3D(Vector3d p1, Vector3d p2)
        {
            double x = Math.Abs(p1.X - p2.X);
            double y = Math.Abs(p1.Y - p2.Y);
            double z = Math.Abs(p1.Z - p2.Z);
            return Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        //////////////////////////////////////////////////////////Distance between 2 Points
        //////////////////////////////////////////////////////////////////////////////////////


        public static double distanceLinePoint(Vector3d line, Vector3d point)
        {
            double w = distance3D(line,point);
            double v = line.Length;
            return Math.Sqrt(w * (w - (v / (v * v))));
        }

        //////////////////////////////////////////////////////////Distance between Point and a vector
        //////////////////////////////////////////////////////////////////////////////////////


        public static bool col_circlePoint2D(double cx, double cy, double r, Point pExt)
        {
            Point pC = new Point();
            pC.x = cx;
            pC.y = cy;

            double x = Math.Abs(pC.x - pExt.x);
            double y = Math.Abs(pC.x - pExt.y);
            double dis = ((x * x) + (y * y));
            //Console.WriteLine("Distance: " + dis);
            if (dis * dis <= r)
                return true;
            return false;
        }

        public static bool col_circlePoint2D(double cx, double cy, double r, double px, double py)
        {
            Point pC = new Point();
            pC.x = cx;
            pC.y = cy;
            Point pE = new Point();
            pE.x = px;
            pE.y = py;
            double x = Math.Abs(pC.x - pE.x);
            double y = Math.Abs(pC.x - pE.y);
            double dis = ((x * x) + (y * y));
            //Console.WriteLine("Distance: " + dis);
            if (dis * dis <= r)
                return true;
            return false;
        }

        public static bool col_circlePoint3D(double cx, double cy, double cz, double r, Vector3d pExt)
        {
            Vector3d pC = new Vector3d();
            pC.X = cx;
            pC.Y = cy;
            pC.Z = cz;
            double x = Math.Abs(pC.X - pExt.X);
            double y = Math.Abs(pC.Y - pExt.Y);
            double z = Math.Abs(pC.Z - pExt.Z);
            double dis = ((x * x) + (y * y) + (z * z));
            //Console.WriteLine("Distance: " + dis);
            if (dis * dis <= r)
                return true;
            return false;
        }

        public static bool col_circlePoint3D(Vector3d cPos, double r, Vector3d pExt)
        {
            Vector3d pC = cPos;
            double x = Math.Abs(pC.X - pExt.X);
            double y = Math.Abs(pC.Y - pExt.Y);
            double z = Math.Abs(pC.Z - pExt.Z);
            double dis = ((x * x) + (y * y) + (z * z));
            //Console.WriteLine("Distance: " + dis);
            if (dis * dis <= r)
                return true;
            return false;
        }

        //////////////////////////////////////////////////////////Is Circle touching a Point
        ////////////////////////////////////////////////////////////////////////////////////////

        public static bool col_circleCircle2D(double x1, double y1, double r1, double x2, double y2, double r2)
        {
            double x = Math.Abs(x1 - x2);
            double y = Math.Abs(y1 - y2);
            return (Math.Sqrt((x * x) + (y * y)) <= (r1 + r2)); //distance less/equal than the 2 radius added together ?
        }

        public static bool col_circleCircle2D(Point p1, double r1, Point p2, double r2)
        {
            double x = Math.Abs(p1.x - p2.x);
            double y = Math.Abs(p1.x - p2.y);
            return (Math.Sqrt((x * x) + (y * y)) <= (r1 + r2));
        }

        public static bool col_circleCircle3D(double x1, double y1, double z1, double r1, double x2, double y2, double z2, double r2)
        {
            double x = Math.Abs(x1 - x2);
            double y = Math.Abs(y1 - y2);
            double z = Math.Abs(z1 - z2);
            return (Math.Sqrt((x * x) + (y * y) + (z * z)) <= (r1 + r2));
        }

        public static bool col_circleCircle3D(Vector3d p1, double r1, Vector3d p2, double r2)
        {
            double x = Math.Abs(p1.X - p2.X);
            double y = Math.Abs(p1.Y - p2.Y);
            double z = Math.Abs(p1.Z - p2.Z);
            return (Math.Sqrt((x * x) + (y * y) + (z * z)) <= (r1 + r2));
        }
        //////////////////////////////////////////////////////////Are 2 Circles touching ?
        ////////////////////////////////////////////////////////////////////////////////////



        public static bool col_boxBox2D(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2)
        {
            if ((x1 + w1 >= x2 && x1 <= x2 + w2) && (y1 + h1 >= y2 && y1 <= y2 + h2))
                return true;
            else
                return false;
        }

        public static bool col_boxBox2D(Rect obj1, Rect obj2)
        {
            if ((obj1.x + obj1.w >= obj2.x && obj1.x <= obj2.x + obj2.w) && (obj1.y + obj1.h >= obj2.y && obj1.y <= obj2.y + obj2.h))
                return true;
            else
                return false;
        }

        //////////////////////////////////////////////////////////Are 2 Rectangles touching ?
        ////////////////////////////////////////////////////////////////////////////////////


        public static bool col_boxBox3D(double x1, double y1, double z1, double w1, double h1, double d1, double x2, double y2, double z2, double w2, double h2, double d2)
        {
            if ((x1 + w1 >= x2 && x1 <= x2 + w2) && (y1 + h1 >= y2 && y1 <= y2 + h2) && (z1 + d1 >= z2 && z1 <= z2 + d2))
                return true;
            else
                return false;
        }

        public static bool col_boxBox3D(Vector3d pos1, Vector3d dim1, Vector3d pos2, Vector3d dim2)
        {
            if ((pos1.X + dim1.X >= pos2.X && pos1.X <= pos2.X + dim2.X) && (pos1.Y + dim1.Y >= pos2.Y && pos1.Y <= pos2.Y + dim2.Y) && (pos1.Z + dim1.Z >= pos2.Z && pos1.Z <= pos2.Z + dim2.Z))
                return true;
            else
                return false;
        }

        

        public static bool col_circleRect(double circleX, double circleY, double circleR, double rectX, double rectY, double rectW, double rectH)
        {
            Point circleDistance = new Point();
            circleDistance.x = Math.Abs(circleX - rectX);
            circleDistance.y = Math.Abs(circleY - rectY);

            if (circleDistance.x > (rectW / 2 + circleR)) { return false; }
            if (circleDistance.y > (rectH / 2 + circleR)) { return false; }

            if (circleDistance.x <= (rectW / 2)) { return true; }
            if (circleDistance.y <= (rectH / 2)) { return true; }

            double cornerDistance_sq = (circleDistance.x - rectW / 2) * (circleDistance.x - rectW / 2) +
                                        (circleDistance.y - rectH / 2) * (circleDistance.y - rectH / 2);

            return (cornerDistance_sq <= (circleR * circleR));
        }

        public static bool col_circleRect(double circleX, double circleY, double circleR, BaseObj.Rect rect)
        {
            Point circleDistance = new Point();
            circleDistance.x = Math.Abs(circleX - rect.x);
            circleDistance.y = Math.Abs(circleY - rect.y);

            if (circleDistance.x > (rect.w / 2 + circleR)) { return false; }
            if (circleDistance.y > (rect.h / 2 + circleR)) { return false; }

            if (circleDistance.x <= (rect.w / 2)) { return true; }
            if (circleDistance.y <= (rect.h / 2)) { return true; }

            double cornerDistance_sq = (circleDistance.x - rect.w / 2) * (circleDistance.x - rect.w / 2) +
                                        (circleDistance.y - rect.h / 2) * (circleDistance.y - rect.h / 2);

            return (cornerDistance_sq <= (circleR * circleR));
        }

    }
}
