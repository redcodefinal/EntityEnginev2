using EntityEnginev2.Components;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.GUI
{
    public class Text : Entity
    {
        public Body Body;
        public Physics Physics;
        public TextRender TextRender;

        public Text(EntityState es, string name)
            : base(es, name)
        {
            Body = new Body(this, "Body");
            AddComponent(Body);

            Physics = new Physics(this, "Physics");
            AddComponent(Physics);

            TextRender = new TextRender(this, "TextRender");
            AddComponent(TextRender);
        }

        public Text(EntityState es, string name, SpriteFont font, string text, Vector2 position)
            : base(es, name)
        {
            Body = new Body(this, "Body", position);
            AddComponent(Body);

            Physics = new Physics(this, "Physics");
            AddComponent(Physics);

            TextRender = new TextRender(this, "TextRender", font, text);
            AddComponent(TextRender);
        }

        public override void ParseXml(Data.XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
        }
    }
}