using System;
using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;

namespace EntityEnginev2.Components
{
    public class Physics : Component
    {
        public float AngularVelocity;
        public float AngularVelocityDrag = 1f;
        public Vector2 Velocity = Vector2.Zero;
        public float Drag = 1f;
        public Vector2 Acceleration = Vector2.Zero;

        public Physics(Entity e, string name)
            : base(e, name)
        {
        }

        public override void Update()
        {
            Velocity += Acceleration;
            Velocity *= Drag;
            AngularVelocity *= AngularVelocityDrag;

            Entity.GetComponent<Body>().Position += Velocity;
            Entity.GetComponent<Body>().Angle += AngularVelocity;
        }

        public void Thrust(float power)
        {
            var angle = Entity.GetComponent<Body>().Angle;
            Thrust(power, angle);
        }

        public void Thrust(float power, float angle)
        {
            Velocity.X -= (float)Math.Sin(-angle) * power;
            Velocity.Y -= (float)Math.Cos(-angle) * power;
        }

        public void FaceVelocity()
        {
            Entity.GetComponent<Body>().Angle = (float)Math.Atan2(Velocity.X, -Velocity.Y);
        }

        public void FaceVelocity(Vector2 velocity)
        {
            Entity.GetComponent<Body>().Angle = (float)Math.Atan2(velocity.X, velocity.Y);
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name;
            Drag = xp.GetFloat(rootnode + "->Drag", Drag);
            AngularVelocity = xp.GetFloat(rootnode + "->AngularVelocity", AngularVelocity);
            Velocity = xp.GetVector2(rootnode + "->Velocity", Velocity);
            Acceleration = xp.GetVector2(rootnode + "->Acceleration", Acceleration);
        }

        public Physics Clone()
        {
            Physics p = new Physics(Entity, Name);
            p.AngularVelocity = AngularVelocity;
            p.AngularVelocityDrag = AngularVelocityDrag;
            p.Drag = Drag;
            p.Velocity = Velocity;
            p.Acceleration = Acceleration;
            return p;
        }
    }
}