using System.Collections.Generic;

using System.Linq;
using EntityEnginev2.Data;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Engine
{
    public class Entity : List<Component>
    {
        public delegate void ComponentEventHandler(Component c);

        public event ComponentEventHandler ComponentAdded;

        public event ComponentEventHandler ComponentRemoved;

        public delegate void EntityEventHandler(Entity e);

        public EntityEventHandler CreateEvent;
        public EntityEventHandler DestroyEvent;

        public string Name { get; protected set; }
        public int ID { get; private set; }

        public EntityState StateRef { get; private set; }

        public Entity(EntityState es, string name)
        {
            Name = name;
            StateRef = es;
            ID = es.GetID();
        }

        public T GetComponent<T>(string name) where T : Component
        {
            var result = this.FirstOrDefault(c => c.Name == name);
            if (result == null)
                Error.Exception("Component " + name + " does not exist.", this);
            return (T)result;
        }

        public T GetComponent<T>() where T : Component
        {
            var result = this.FirstOrDefault(c => c is T && c.Default) ??
                         this.FirstOrDefault(c => c is T);
            if (result == null)
                Error.Exception("Component of type " + typeof(T) + " does not exist.", this);
            return (T)result;
        }

        public void AddComponent(Component c)
        {
            if (this.Any(component => c.Name == component.Name))
            {
                Error.Exception(c.Name + " already exists in this list!", this);
            }

            Add(c);

            c.DestroyEvent += RemoveComponent;

            if (ComponentAdded != null)
                ComponentAdded(c);
        }

        public void RemoveComponent(Component c)
        {
            Remove(c);
            if (ComponentRemoved != null)
                ComponentRemoved(c);
        }

        public virtual void Update()
        {
            foreach (var component in this.ToList().Where(component => component.Active))
            {
                component.Update();
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            foreach (var component in this.ToList().Where(component => component.Active))
            {
                component.Draw(sb);
            }
        }

        public void AddEntity(Entity e)
        {
            if (CreateEvent != null)
                CreateEvent(e);
        }
        public void Destroy()
        {
            Destroy(null);
        }

        public virtual void Destroy(Entity e = null)
        {
            foreach (var component in this.ToList())
            {
                component.Destroy();
            }

            if (DestroyEvent != null)
                DestroyEvent(this);
        }

        public virtual void ParseXml(XmlParser xp)
        {
            foreach (var c in this)
            {
                c.ParseXml(xp);
            }
        }

        public virtual void ParseXml(XmlParser xp, string path)
        {
            foreach (var c in this)
            {
                c.ParseXml(xp, path);
            }
        }
    }
}