using System.Collections.Generic;
using System.Linq;
using EntityEnginev2.Data;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Engine
{
    public class EntityState : List<Entity>
    {
        public delegate void EventHandler(string name);

        public event EventHandler ChangeState;

        public event Entity.EntityEventHandler EntityRemoved;

        public event Entity.EntityEventHandler EntityAdded;

        public string Name { get; private set; }

        public EntityGame GameRef { get; private set; }

        public EntityState(EntityGame stg, string name)
        {
            GameRef = stg;
            Name = name;
        }

        public T GetEntity<T>(string name) where T : Entity
        {
            var result = this.FirstOrDefault(entity => entity.Name == name);
            if (result == null)
                Error.Exception("Entity" + name + " does not exist!");
            return (T)result;
        }

        public virtual void AddEntity(Entity e)
        {
            if (this.Any(entity => e.Name == entity.Name))
            {
                Error.Exception("Entity " + e.Name + " already exists in this list!");
            }
            Add(e);

            e.CreateEvent += AddEntity;
            e.DestroyEvent += RemoveEntity;

            if (EntityAdded != null)
                EntityAdded(e);
        }

        public virtual void RemoveEntity(Entity e)
        {
            if (Contains(e))
            {
                Remove(e);
                if (EntityRemoved != null)
                    EntityRemoved(e);
            }
        }

        public virtual void Update()
        {
            foreach (var entity in this.ToList())
            {
                entity.Update();
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            foreach (var entity in this.ToList())
            {
                entity.Draw(sb);
            }
        }

        public virtual void Destroy()
        {
            foreach (var entity in this.ToList())
            {
                entity.Destroy();
            }
        }

        public virtual void Start()
        {
        }

        public virtual void Reset()
        {
            Clear();
        }

        public virtual void ChangeToState(string name)
        {
            if (ChangeState != null)
                ChangeState(name);
        }

        public virtual void Show(string name)
        {
            if (name == Name)
                Show();
        }

        public void Show()
        {
            GameRef.CurrentState = this;
        }
    }
}