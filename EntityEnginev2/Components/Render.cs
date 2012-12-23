using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Components
{
    public class Render : Component
    {
        public Color Color = Color.White;
        public float Alpha = 1f;
        public Vector2 Scale = Vector2.One;
        public float Layer;
        public SpriteEffects Flip = SpriteEffects.None;

        public Vector2 Origin { get; set; }

        public virtual Rectangle DrawRect { get; set; }

        public virtual Vector2 Bounds { get; set; }

        public Render(Entity entity, string name)
            : base(entity, name)
        {
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);

            string rootnode = path + "->" + Name + "->";
            Color = xp.GetColor(rootnode + "Color", Color.White);
            Alpha = xp.GetFloat(rootnode + "Alpha", 1);
            Scale = xp.GetVector2(rootnode + "Scale", Vector2.One);
            Layer = xp.GetFloat(rootnode + "Layer", .5f);
        }
    }
}