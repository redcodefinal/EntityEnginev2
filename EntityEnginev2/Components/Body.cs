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

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";
            Position = xp.GetVector2(rootnode + "Position", Vector2.Zero);
            Angle = xp.GetFloat(rootnode + "Angle", 0);
        }
    }
}