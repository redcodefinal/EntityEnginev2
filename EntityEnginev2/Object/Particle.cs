using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev2.Components;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;

namespace EntityEnginev2.Object
{
    public class Particle : Entity
    {
         public int TimeToLive { get; set; }
        public int MaxTimeToLive { get; private set; }
        public Emitter Emitter;
        public TileRender TileRender;
        public Body Body;
        public Physics Physics;

        public Particle(int index, Vector2 position, int ttl, Emitter e)
            : base(e.Entity.StateRef, e.Name + ".Particle" + e.GetID())
        {
            
            Body = new Body(this, "Body", position);
            AddComponent(Body);

            TileRender = new TileRender(this, "TileRender",e.Texture, e.TileSize);
            TileRender.Index = index;
            AddComponent(TileRender);

            Physics = new Physics(this, "Physics");
            AddComponent(Physics);

            Emitter = e;
            TimeToLive = ttl;
            MaxTimeToLive = TimeToLive;
        }

        public override void Update()
        {
            base.Update();

            TimeToLive--;
            if (TimeToLive <= 0)
                Destroy();
        }
    }

    public class FadeParticle : Particle
    {
        public int FadeAge;

        public FadeParticle(int index, Vector2 position, int fadeage, int ttl, Emitter e)
            : base(index, position, ttl, e)
        {
            FadeAge = fadeage;
        }

        public override void Update()
        {
            base.Update();
            if (TimeToLive < FadeAge)
            {
                TileRender.Alpha -= 1f / FadeAge;
            }
        }
    }
}
