using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;

namespace Lunar_lander
{
    class RootThingy
    {
        public static int windowX = 800;
        public static int windowY = 600;
        public static string exePath = Environment.CurrentDirectory;
        public static double sceneX = 800;
        public static int sceneY = 600;

        public static Random rnd = new Random();

        private static bool MouseReset = false;

        public static Vector3d gravity = new Vector3d(0, -0.001, 0);     //gravitiy = Mass / (radius * radius)

        private static bool mode_3d = true;


        public static short spriteArrMax;
        public static BaseObj[] spriteArray = new BaseObj[2048];
        public static short spriteCount;

        

        [STAThread]
        public static void Main()
        { 

            //Player data
            double fuel = 300;
            double thrust = 0.25;
            double friction = 0.01;
            double x = 0;
            double y = 0;
            double z = 0;
            double xVel = 0;
            double yVel = 0;
            double angle = 0;
            double angleVel = 0;
            double speed = 0;

            double viewX = 0;
            double viewY = 0;
            double viewZ = 0;

            Vector2 mouseOld =new Vector2(windowX / 2, windowY / 2);
            Vector2 mouseNew = new Vector2(windowX / 2, windowY / 2);
            Vector3d forward = new Vector3d(0, 0, 1);
            Vector3d right = new Vector3d(1, 0, 0);
            Vector3d up = new Vector3d(0, 1, 0);
            Vector3d velocity = new Vector3d(0, 0, 0);
            Vector3d position = new Vector3d(0, 0, 0);
            Vector3d lookingAt = new Vector3d(0, 0, 0);


            //gravity.Y = 0;

            Random rnd = new Random();

            spriteAdd(new Circle(new Vector3d(0.4, 0.5, 0.2), 0.035, new Vector3d(0.00, 0.0, 0)));
            spriteAdd(new Circle(new Vector3d(0.5, 0.5, 0.5), 0.035, new Vector3d(0.00, 0.0, 0)));
            spriteAdd(new Circle(new Vector3d(0.3, 0.5, 0.7), 0.035, new Vector3d(0.00, 0.0, 0)));
            spriteAdd(new Circle(new Vector3d(0.1, 0.5, 0.7), 0.035, new Vector3d(0.00, 0.0, 0)));
            spriteAdd(new Circle(new Vector3d(0.3, 0.7, 0.7), 0.045, new Vector3d(0.00, 0.0, 0)));

            spriteAdd(new Spring(spriteArray[0], spriteArray[1], 0.3));
            spriteAdd(new Spring(spriteArray[1], spriteArray[2], 0.3));
            spriteAdd(new Spring(spriteArray[2], spriteArray[3], 0.3));
            spriteAdd(new Spring(spriteArray[3], spriteArray[0], 0.3));

            spriteAdd(new Spring(spriteArray[0], spriteArray[4], 0.4));
            spriteAdd(new Spring(spriteArray[1], spriteArray[4], 0.4));
            spriteAdd(new Spring(spriteArray[2], spriteArray[4], 0.4));
            spriteAdd(new Spring(spriteArray[3], spriteArray[4], 0.4));

            spriteAdd(new Rectangle(new Vector3d(3, 3, 3), new Vector3d(0.6, 0.4, 0.6)));

            //spriteAdd(new Player(0,0,0));
            spriteAdd(new Triangle(new Vector3d(3, 1, 3), new Vector3d(6, 1, 3), new Vector3d(4.5, 4, 3), 4));

            spriteAdd(new Circle(new Vector3d(4, 5, 4), 0.5, new Vector3d(0.00, 0.0, 0)));

            spriteArray[0].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
            spriteArray[1].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
            spriteArray[2].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
            spriteArray[3].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
            spriteArray[4].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);


            Map m = new Map();

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    GL.Enable(EnableCap.DepthTest);
                    GL.LineWidth(2);
                    game.VSync = VSyncMode.On;
                    game.Width = windowX;
                    game.Height = windowY;
                    //game.WindowBorder = WindowBorder.Fixed; //Disables the resizable windowframe
                    GL.Enable(EnableCap.Blend);                                                     //These lines
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);  //enable transparency using alpha-channel
                    
                };
                mouseOld.X = game.Mouse.X;
                mouseOld.Y = game.Mouse.Y;

                game.Resize += (sender, e) =>
                {
                    //sceneX = game.Height;
                    //sceneY = game.Width;
                    GL.Viewport(0, 0, windowX, windowY);
                };



                var mouse = Mouse.GetState();
                if (!MouseReset)
                {
                    game.Mouse.Move += (sender, e) =>   //Handling all mouse-stuff
                    {
                        viewX += e.YDelta;
                        forward.X = e.YDelta;
                        if (viewX > 90)
                            viewX = 90;
                        if (viewX < -90)
                            viewX = -90;

                        viewY += e.XDelta;
                        forward.Y = e.XDelta;
                        

                        MouseReset = true;
                    };
                }
                else 
                { Mouse.SetPosition(game.X + windowX / 2, game.Y + windowY / 2); }

                lookingAt.X = Math.Cos(viewX) * Math.Cos(viewY);
                lookingAt.Y = Math.Sin(viewY);
                lookingAt.Z = Math.Sin(viewX) * Math.Cos(viewY);


                game.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    mouse = Mouse.GetState();

                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }
                    if (game.Keyboard[Key.A])
                    { x += 0.1; }
                    if (game.Keyboard[Key.D])
                    { x -= 0.1; }
                    if (game.Keyboard[Key.LControl])
                    { y += 0.1; }
                    if (game.Keyboard[Key.LShift])
                    { y -= 0.1; }
                    if (game.Keyboard[Key.W])
                    { z += 0.1; }
                    if (game.Keyboard[Key.S])
                    { z -= 0.1; }


                    if (game.Keyboard[Key.A])
                    {
                        Vector3d strafeDir = Vector3d.Cross(lookingAt,up);
                        position += 0.1 * strafeDir; 
                    }
                    if (game.Keyboard[Key.D])
                    {
                        Vector3d strafeDir = Vector3d.Cross(lookingAt, up);
                        position += -0.1 * strafeDir; 
                    }
                    if (game.Keyboard[Key.LControl])
                    { position.Y += 0.1; }
                    if (game.Keyboard[Key.LShift])
                    { position.Y -= 0.1; }
                    if (game.Keyboard[Key.W])
                    { position += 0.1 *lookingAt; }
                    if (game.Keyboard[Key.S])
                    { position -= 0.1 *lookingAt; }


                    if(game.Keyboard[Key.R])
                    {
                        for (int i = 0; i <= spriteArrMax; i++)
                        {
                            if (spriteArray[i] != null)
                            {
                                spriteArray[i] = null;  //clear the array
                            }
                        }

                        spriteAdd(new Circle(new Vector3d(0.4, 0.5, 0.2), 0.035, new Vector3d(0.00, 0.0, 0)));
                        spriteAdd(new Circle(new Vector3d(0.5, 0.5, 0.5), 0.035, new Vector3d(0.00, 0.0, 0)));
                        spriteAdd(new Circle(new Vector3d(0.3, 0.5, 0.7), 0.035, new Vector3d(0.00, 0.0, 0)));
                        spriteAdd(new Circle(new Vector3d(0.1, 0.5, 0.7), 0.035, new Vector3d(0.00, 0.0, 0)));
                        spriteAdd(new Circle(new Vector3d(0.3, 0.7, 0.7), 0.045, new Vector3d(0.00, 0.0, 0)));

                        spriteAdd(new Spring(spriteArray[0], spriteArray[1], 0.3));
                        spriteAdd(new Spring(spriteArray[1], spriteArray[2], 0.3));
                        spriteAdd(new Spring(spriteArray[2], spriteArray[3], 0.3));
                        spriteAdd(new Spring(spriteArray[3], spriteArray[0], 0.3));

                        spriteAdd(new Spring(spriteArray[0], spriteArray[4], 0.4));
                        spriteAdd(new Spring(spriteArray[1], spriteArray[4], 0.4));
                        spriteAdd(new Spring(spriteArray[2], spriteArray[4], 0.4));
                        spriteAdd(new Spring(spriteArray[3], spriteArray[4], 0.4));

                        spriteAdd(new Rectangle(new Vector3d(3, 3, 3), new Vector3d(0.6, 0.4, 0.6)));

                        //spriteAdd(new Player(2, 2, 2));
                        spriteAdd(new Triangle(new Vector3d(3, 1, 3), new Vector3d(6, 1, 3), new Vector3d(4.5, 4, 3), 4));
                        spriteAdd(new Circle(new Vector3d(4, 5, 4), 0.5, new Vector3d(0.00, 0.0, 0)));

                        spriteArray[0].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
                        spriteArray[1].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
                        spriteArray[2].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
                        spriteArray[3].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
                        spriteArray[4].color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), 255);
                    }



                    if (game.Keyboard[Key.F1])  //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    { ; }


                    //if (game.Keyboard[Key.Up])
                    //{ viewX += 1; } 
                    //if (game.Keyboard[Key.Down])
                    //{ viewX -= 1; }
                    //if (game.Keyboard[Key.Left])
                    //{ viewY += 1; }
                    //if (game.Keyboard[Key.Right])
                    //{ viewY -= 1; } 
                    //if (game.Keyboard[Key.PageUp])
                    //{ viewZ += 1; }
                    //if (game.Keyboard[Key.PageDown])
                    //{ viewZ -= 1; }

                    

                    Console.Clear();
                    Console.WriteLine("Mouse X: " + game.Mouse.X);
                    Console.WriteLine("Mouse Y: " + game.Mouse.Y);
                    Console.WriteLine("viewX (LR): " + viewX);
                    Console.WriteLine("ViewY (UD): " + viewY);
                    Console.WriteLine("LookingAt: [" + lookingAt.X + " | " + lookingAt.Y + " | " + lookingAt.Z+"]");



                    game.Title = (("FPS: " + (int)(game.RenderFrequency) + " ; " + Math.Round(game.RenderTime * 1000, 2) + "ms/frame"));
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    {
                        GL.Viewport(0, 0, windowX, windowY);
                        if (!mode_3d)
                        {
                            GL.MatrixMode(MatrixMode.Projection);
                            GL.LoadIdentity();
                            GL.Ortho(0, windowX, windowY, 0, -1000, 1);  //Render  distant objects smaller
                        }
                        else
                        {
                                                        
                            GL.MatrixMode(MatrixMode.Projection);
                            GL.LoadIdentity();
                            perspectiveGL(90, (windowX / windowY), 0.1, 100);
                            GL.MatrixMode(MatrixMode.Modelview);
                            GL.LoadIdentity();
                        }
                    }

                    //Matrix4d look = Matrix4d.LookAt(new Vector3d(1, 1, 1), spriteArray[13].pos, up);
                    //GL.LoadMatrix(ref look);
                    


                    //GL.MatrixMode(MatrixMode.Modelview);

                    //GL.PopMatrix();
                    //GL.PushMatrix();

                    


                    //GL.Frustum(windowX / 2 * -1, windowX / 2 * 1, windowY / 2 * -1, windowY / 2 * 1, -0, 10);
                    //GL.MatrixMode(MatrixMode.Projection);





                    //GL.Begin(PrimitiveType.Quads); //Background
                    //GL.Color3(0.0f, 0.9f, 0.0f);
                    //GL.Vertex3(0, 0, 500);
                    //GL.Vertex3(sceneX, 0, 500);
                    //GL.Vertex3(sceneX, sceneY, 500);
                    //GL.Vertex3(0, sceneY, 500);
                    //GL.End();

                    

                    GL.Rotate(viewX, 1, 0, 0);
                    GL.Rotate(viewY, 0, 1, 0);
                    GL.Rotate(viewZ, 0, 0, 1);


                    GL.Translate(position);
                    //GL.Translate(new Vector3d(x, y, z));

                    


                    m.process();


                    spriteCount = 0;
                    for (int i = 0; i <= spriteArrMax; i++)
                    {
                        if (spriteArray[i] != null)
                        {
                            spriteCount++;
                            //GL.Translate(spriteArray[i].pos);
                            spriteArray[i].tick();
                            spriteArray[i].render();
                        }
                    }
                    //14 & 15
                    if(spriteArray[14]!=null && spriteArray[15]!=null)
                    {
                        if (BaseObj.col_boxBox3D(spriteArray[14].pos, spriteArray[14].dim, spriteArray[15].pos, spriteArray[15].dim))
                        {
                            ;//BaseObj.distanceLinePoint(
                        }
                    }
                    
                        BaseObj.test(new int[]{1,3,5,8,9,22,9});


                    GL.LineWidth(1);
                    double g = -100;
                    while (g < 500)
                    {
                        g += 1;
                        GL.Begin(PrimitiveType.Lines);
                        GL.Color3(1.0f, 1.0f, 1.0f);
                        GL.Vertex3(g, 0, -500);
                        GL.Vertex3(g, 0, 500);
                        GL.End();
                        GL.Begin(PrimitiveType.Lines);
                        GL.Color3(1.0f, 1.0f, 1.0f);
                        GL.Vertex3(-500, 0, g);
                        GL.Vertex3(500, 0, g);
                        GL.End();
                    }


                    GL.LineWidth(4);


                    GL.Begin(PrimitiveType.Lines);  //Red
                    GL.Color3(1.0f, 0.0f, 0.0f);
                    GL.Vertex3(0, 0 + 3, 0);
                    GL.Vertex3(0 + 1, 0 + 3, 0);
                    GL.End();

                    GL.Begin(PrimitiveType.Lines);  //Green
                    GL.Color3(0.0f, 1.0f, 0.0f);
                    GL.Vertex3(0, 0 + 3, 0);
                    GL.Vertex3(0, 0 + 1 + 3, 0);
                    GL.End();

                    GL.Begin(PrimitiveType.Lines);  //Blue
                    GL.Color3(0.0f, 0.0f, 1.0f);
                    GL.Vertex3(0, 0 + 3, 0);
                    GL.Vertex3(0, 0 + 3, 0 + 1);
                    GL.End();



                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }

        



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// ADD_A_SPRITE
        public static int spriteAdd(BaseObj spriteObj)
        {
            bool done = false;
            short i = 0;
            while (!done && i <= spriteArray.Count())
            {
                if (spriteArray[i] == null)
                {
                    spriteArray[i] = spriteObj;
                    spriteArray[i].id = i;
                    done = true;
                    if (i > spriteArrMax)
                        spriteArrMax = i;
                }
                else
                {
                    i++;
                }
            }
            return i;
        }


        public static void perspectiveGL( double FOV, double aspectRatio, double zNear, double zFar)
        {
            double fW, fH;
            fH = Math.Tan(FOV / 360 * Math.PI) * zNear;
            fW = fH * aspectRatio;
            GL.Frustum(-fW, fW, -fH, fH, zNear, zFar);
        }


        //End of Root =============================================================
    }
}
