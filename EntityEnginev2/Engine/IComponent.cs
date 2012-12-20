using EntityEnginev2.Data;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Engine
{
    public interface IComponent
    {
        Entity Entity { get; }
        string Name { get; }
        bool Default { get; set; }
        
        void Update();
        void Draw(SpriteBatch sb);
        void Destroy();
        void ParseXml(XmlParser xp);
        void ParseXml(XmlParser xp, string path);
    }
}
