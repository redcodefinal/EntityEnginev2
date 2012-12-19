using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Components
{
    public class ImageRender : Render
    {
        public Texture2D Texture { get; set; }

        public override Rectangle DrawRect 
        { 
            get
            {
                try
                {
                    Vector2 position = Entity.GetComponent<Body>().Position;
                    return new Rectangle(
                        (int)((int)position.X + Origin.X*Scale.X), 
                        (int)((int)position.Y + Origin.Y*Scale.Y), 
                        (int)(Texture.Width * Scale.X),
                        (int)(Texture.Height * Scale.Y));
                }
                catch 
                {
                    Error.Exception("Body should not be null!", Entity);
                    return new Rectangle();
                }
            } 
        }

        public override Vector2 Bounds
        {
            get { return new Vector2(Texture.Width, Texture.Height); }
        }
        
        public ImageRender(Entity e, string name)
            : base(e, name)
        {
            Origin = Vector2.Zero;
        }

        public ImageRender(Entity e, string name,Texture2D texture)
            : base(e, name)
        {
            Texture = texture;
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }

        public override void Draw(SpriteBatch sb)
        {
            try
            {
                sb.Draw(Texture, DrawRect, null, Color * Alpha, Entity.GetComponent<Body>().Angle,
                        Origin, Flip, Layer);
            }
            catch
            {
                Error.Exception("Body should not be null!", Entity);
            }
        }

        public Texture2D LoadTexture(string location)
        {
            return Entity.StateRef.GameRef.Game.Content.Load<Texture2D>(location);
        }

        public override void ParseXml(XmlParser xmlparser)
        {
            string rootnode = xmlparser.GetRootNode() + "->" + Name + "->";

            try
            {
                Texture = LoadTexture(xmlparser.GetString(rootnode + "Texture"));
            }
            catch{}

            try
            {
                Color = xmlparser.GetColor(rootnode + "Color");
            }
            catch {}

            try
            {
                Alpha = xmlparser.GetFloat(rootnode + "Alpha");
            }
            catch{}

            try
            {
                Scale = xmlparser.GetVector2(rootnode + "Scale");
            }
            catch{}

            try
            {
                Layer = xmlparser.GetFloat(rootnode + "Layer");
            }
            catch{}
        }
    }
}
