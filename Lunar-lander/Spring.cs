using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Lunar_lander
{
    class Spring :BaseObj
    {
        

        public Spring(double x, double y, double z, int objId1, int objId2, double springLength, double constant = 0.01)
            : base()
        {
            this.type = "spring";
            this.pos.X = x;
            this.pos.Y = y;
            this.pos.Z = z;
            this.constant = constant;
            this.springLength = springLength;
            this.springObjId1 = this.id;
            if (RootThingy.spriteArray[objId2] != null)
                this.springObjId2 = RootThingy.spriteArray[objId2].id;
            else
                Console.WriteLine("SpringID" + this.id + " can't link to spriteID" + springObjId2);

            //this.color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(200, 255));
            //this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public Spring(Vector3d pos, int objId1, int objId2, double springLength, double constant = 0.01)
            : base()
        {
            this.type = "spring";
            this.pos = pos;
            this.vel = vel;
            this.constant = constant;
            this.springLength = springLength;
            this.springObjId1 = this.id;
            if (RootThingy.spriteArray[objId2] != null)
                this.springObjId2 = RootThingy.spriteArray[objId2].id;
            else
                Console.WriteLine("SpringID" + this.id + " can't link to spriteID" + springObjId2);
            //this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }


        public Spring(int objId1, int objId2, double springLength, double constant = 0.01)
            : base()
        {
            this.type = "spring";
            if (RootThingy.spriteArray[objId1] != null)
                this.springObjId1 = RootThingy.spriteArray[objId2].id;
            else
                Console.WriteLine("SpringID" + this.id + " can't link to spriteID" + springObjId1);

            this.constant = constant;
            this.springLength = springLength;

            this.springObjId1 = this.id;
            if (RootThingy.spriteArray[objId2] != null)
                this.springObjId2 = RootThingy.spriteArray[objId2].id;
            else
                Console.WriteLine("SpringID" + this.id + " can't link to spriteID" + springObjId2);

            //this.color = new Color4((byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(128, 255), (byte)RootThingy.rnd.Next(200, 255));
            //this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public Spring(BaseObj obj1, BaseObj obj2, double springLength, double constant = 0.01)
            : base()
        {
            this.type = "spring";
            this.pos = pos;
            this.vel = vel;
            this.constant = constant;
            this.springLength = springLength;
            if (obj1 != null)
                this.springObjId1 = obj1.id;
            else
                Console.WriteLine("SpringID" + this.id + " can't link to spriteID" + springObjId2);
            if (obj2 != null)
                this.springObjId2 = obj2.id;
            else
                Console.WriteLine("SpringID" + this.id + " can't link to spriteID" + springObjId2);
            //this.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        }




        public override void tick()
        {
            
            Vector3d spring = RootThingy.spriteArray[springObjId1].pos - RootThingy.spriteArray[springObjId2].pos;
            double length = BaseObj.distance3D(RootThingy.spriteArray[springObjId1].pos, RootThingy.spriteArray[springObjId2].pos);
            double displacement = length - springLength;
            Vector3d springN = spring / length;
            Vector3d restoreForce = springN * (displacement * constant);
            RootThingy.spriteArray[springObjId2].vel.Add(restoreForce);
            RootThingy.spriteArray[springObjId1].vel.Sub(restoreForce);
            
        }


        public override void render()
        {
            GL.Color4(color);
            GL.LineWidth(3);
            GL.PushMatrix();
            
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(RootThingy.spriteArray[springObjId1].pos);
            GL.Vertex3(RootThingy.spriteArray[springObjId2].pos);
            GL.End();

            GL.PopMatrix();
            GL.LineWidth(1);
        }


    }
}
