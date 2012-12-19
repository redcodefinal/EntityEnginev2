using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;

namespace EntityEnginev2.Components
{
    public class Collision : Component
    {
        public List<Entity> Partners = new List<Entity>();
        public List<Entity> CollidedWith = new List<Entity>();
        public Rectangle Bounds;

        public bool Colliding
        {
            get { return (CollidedWith.Count > 0); }
        }
        public Rectangle BoundingBox
        {
            get 
            { 
                Vector2 position = Entity.GetComponent<Body>().Position;
                Vector2 scale;
                try
                {
                    scale = Entity.GetComponent<Render>().Scale;
                }
                catch
                {
                    scale = Vector2.One;
                }

                return new Rectangle(
                    (int)(Bounds.X + position.X),
                    (int)(Bounds.Y + position.Y),
                    (int) (Bounds.Width * scale.X),
                    (int) (Bounds.Height * scale.Y));
            }
        }


        public event Entity.EntityEventHandler CollideEvent;

        public Collision(Entity e, string name)
            : base(e, name)
        {
            try
            {
                var width = (int) Entity.GetComponent<Render>().Bounds.X;
                var height = (int)Entity.GetComponent<Render>().Bounds.Y;
                Bounds = new Rectangle(0, 0, width, height);
            }
            catch (Exception)
            {
                Bounds = new Rectangle();
                Error.Warning("Collision.Bounds not set", Entity);
            }
        }

        public override void Update()
        {
            //Erase the collided with list every frame
            CollidedWith = new List<Entity>();
            foreach (var p in Partners.ToArray())
            {
                if (TestCollision(p))
                {
                    CollidedWith.Add(p);
                    if (CollideEvent != null)
                        CollideEvent(p);
                }
            }
        }

        public override void Destroy()
        {
            Partners = new List<Entity>();
        }

        virtual public bool TestCollision(Entity e)
        {
            try
            {
                return (BoundingBox.Intersects(e.GetComponent<Collision>().BoundingBox));
            }
            catch
            {
                Error.Exception(Name + ".TestCollision" + " attempted to process a non-existant Collision!", Entity);
                return false;
            }
        }

        public void AddPartner(Entity e)
        {
            Partners.Add(e);
            e.DestroyEvent += RemovePartner;
        }

        public void RemovePartner(Entity e)
        {
            Partners.Remove(e);
        }
    }
}
