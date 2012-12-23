using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Components
{
    public class TextRender : Render
    {
        public string Text;
        public SpriteFont Font;

        public override Rectangle DrawRect
        {
            get
            {
                Vector2 position;
                try
                {
                    position = Entity.GetComponent<Body>().Position;
                }
                catch
                {
                    position = Vector2.Zero;
                }
                return new Rectangle((int)position.X, (int)position.Y, (int)Font.MeasureString(Text).X, (int)Font.MeasureString(Text).Y);
            }
        }

        public TextRender(Entity entity, string name)
            : base(entity, name)
        {
        }

        public TextRender(Entity entity, string name, SpriteFont font, string text)
            : base(entity, name)
        {
            Text = text;
            Font = font;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, Text, Entity.GetComponent<Body>().Position, Color * Alpha,
                          Entity.GetComponent<Body>().Angle, Origin, Scale, Flip, Layer);
        }

        public void LoadFont(string location)
        {
            Font = Entity.StateRef.GameRef.Game.Content.Load<SpriteFont>(location);
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";

            LoadFont(xp.GetString(rootnode + "Font", "FontNotSet"));
            Text = xp.GetString(rootnode + "Text", "");
        }
    }
}