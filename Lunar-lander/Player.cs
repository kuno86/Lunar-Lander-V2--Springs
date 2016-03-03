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
    class Player : BaseObj
    {
        private int mausLastX;
        private int mausLastY;
        private Vector3d forward = new Vector3d(0, 0, 0);
        private Vector3d right = new Vector3d(0, 0, 0);
        private double viewX = 0;
        private double viewY = 0;
        //private double viewZ = 0;

        public Player(double x, double y, double z)
            : base()
        {
            this.type = "player";
            this.pos.X = x;
            this.pos.Y = y;
            this.pos.Z = z;
            this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public Player(Vector3d pos)
            : base()
        {
            this.pos = pos;
            this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }


        public override void tick()
        {
            //var mouse = Mouse.GetState();

            //int mouseXDelta = mouse.X - mausLastX;
            //int mouseYDelta = mouse.Y - mausLastY;
            //mausLastX = mouse.X;
            //mausLastY = mouse.Y;

            //viewX += mouseYDelta;
                if (viewX > 90)
                    viewX = 90;
                if (viewX < -90)
                    viewX = -90;

                //viewY += mouseXDelta;

                //move.X = 0;
                //move.Y = 0;
                //move.Z = 0;

                var kb = Keyboard.GetState();

                //if (kb[Key.A])  //Strafe Left
                //{ move.X += 1; }
                //if (kb[Key.D])  //Strafe Right
                //{ move.X -= 1; }

                if (kb[Key.Left])
                { viewY--; }
                if (kb[Key.Right])
                { viewY++; }

                if (kb[Key.PageUp])  //Forward
                { forward.Z += 0.1; }
                if (kb[Key.PageDown])  //Backward
                { forward.Z -= 0.1; }

                if (kb[Key.Space])  //jump
                { vel.Y = -0.1; }
                if (kb[Key.LShift]) //duck
                { ; }



            vel.Add(RootThingy.gravity);

            vel.Mult(friction);

            if (pos.Y <= 0)
                vel.Y = vel.Y * bounce;

            pos.Add(vel);

            //r = 0.1;

        }


        public override void render()
        {


            
            GL.LineWidth(3);
            
            GL.PushMatrix();

            GL.Rotate(viewY, 0, 1, 0);  //Left-Right rotation
            GL.Rotate(viewX, 1, 0, 0);  //Up-Down rotation which doesn't affect the move-direction
            
            
                  //add the movement

            

            MyImage.beginDraw2D();


            GL.Color4(1.0f, 0.0f, 0.0f, 1.0f);

            GL.Begin(PrimitiveType.QuadStrip);

            GL.Vertex3(pos.X, pos.Y, pos.Z + 1);
            GL.Vertex3(pos.X, pos.Y+1, pos.Z + 1);

            GL.Vertex3(pos.X + 0.5, pos.Y, pos.Z);
            GL.Vertex3(pos.X + 0.5, pos.Y + 1, pos.Z);

            GL.Vertex3(pos.X - 0.5, pos.Y, pos.Z);
            GL.Vertex3(pos.X - 0.5, pos.Y + 1, pos.Z);

            GL.Vertex3(pos.X, pos.Y, pos.Z + 1);
            GL.Vertex3(pos.X, pos.Y + 1, pos.Z + 1);
            GL.End();

            
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(0.0f, 1.0f, 0.0f, 1.0f);
            GL.Vertex3(0, 0, 0);
            GL.Color4(1.0f, 0.0f, 0.0f, 1.0f);
            GL.Vertex3(pos);
            GL.End();

            MyImage.endDraw2D();

            GL.PopMatrix();
            
            GL.LineWidth(2);
        }

    }
}

