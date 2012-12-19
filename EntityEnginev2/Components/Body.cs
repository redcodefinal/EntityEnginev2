using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;

namespace EntityEnginev2.Components
{
    public class Body : Component
    {
        public Vector2 Position;
        public float Angle;

        public Body(Entity e, string name)
            : base(e, name)
        {
        }

        public Body(Entity e, string name, Vector2 position)
            : base(e, name)
        {
            Position = position;
        }

        public override void ParseXml(XmlParser xmlparser)
        {
            string rootnode = xmlparser.GetRootNode();
            rootnode = rootnode + "->" + Name + "->";
            try
            {
                Position = xmlparser.GetVector2(rootnode + "Position");
            }
            catch { }

            try
            {
                Angle = xmlparser.GetFloat(rootnode + "Angle");
            }
            catch { }

        }
    }
}
