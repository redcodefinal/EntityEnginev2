using EntityEnginev2.Data;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Engine
{
    public class Component : IComponent
    {
        public delegate void EventHandler(Component c);
        public event EventHandler DestroyEvent;

        public Entity Entity { get; private set; }
        public string Name { get; private set; }
        public bool Default { get; set; }

        public Component(Entity entity, string name)
        {
            Entity = entity;
            Name = name;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
        }

        public virtual void Destroy()
        {
            DestroyEvent(this);
        }

        public virtual void ParseXml(XmlParser xp)
        {
        }
    }
}
