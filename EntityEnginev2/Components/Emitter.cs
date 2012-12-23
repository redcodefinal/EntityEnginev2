using EntityEnginev2.Engine;
using EntityEnginev2.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Components
{
    public class Emitter : Component
    {
        public Texture2D Texture { get; protected set; }

        public Vector2 TileSize { get; protected set; }

        public bool AutoEmit;
        public int EmitAmount = 1;

        public int LastID { get; private set; }

        public Emitter(Entity e, string name, Texture2D texture, Vector2 tilesize)
            : base(e, name)
        {
            Texture = texture;
            TileSize = tilesize;
        }

        public override void Update()
        {
            if (AutoEmit)
                Emit(EmitAmount);
        }

        protected virtual Particle GenerateNewParticle()
        {
            var p = new Particle(0, Entity.GetComponent<Body>().Position / 2, 30, this) { Physics = { Velocity = Vector2.UnitY } };
            return p;
        }

        public virtual void Emit(int amount)
        {
            for (var i = 0; i < amount; i++)
                Entity.AddEntity(GenerateNewParticle());
        }

        public int GetID()
        {
            return LastID++;
        }

        //TODO: Add XMLParsing
    }
}