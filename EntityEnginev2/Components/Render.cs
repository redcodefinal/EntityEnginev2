using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Components
{
    public class Render : Component
    {
        public Color Color = Color.White;
        public float Alpha = 1f;
        public Vector2 Scale = Vector2.One;
        public float Layer;
        public SpriteEffects Flip = SpriteEffects.None;

        public Vector2 Origin { get; set; }

        public virtual Rectangle DrawRect { get; set; }

        public virtual Vector2 Bounds { get; set; }

        public Render(Entity entity, string name)
            : base(entity, name)
        {
        }
    }
}
