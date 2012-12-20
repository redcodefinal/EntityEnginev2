using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev2.Data;
using EntityEnginev2.Engine;

namespace EntityEnginev2.Components
{
    public class Health : Component
    {
        public Health(Entity e, string name)
            : base(e, name)
        {
        }

        public Health(Entity e, string name, int hp)
            : base(e, name)
        {
            HitPoints = hp;
        }

        public float HitPoints { get; set; }

        public bool Alive
        {
            get { return !(HitPoints <= 0); }
        }

        public event Entity.EntityEventHandler HurtEvent;

        public event Entity.EntityEventHandler DiedEvent;

        public void Hurt(float points)
        {
            if (!Alive) return;

            HitPoints -= points;
            if (HurtEvent != null)
                HurtEvent(Entity);

            if (!Alive)
            {
                if (DiedEvent != null)
                    DiedEvent(Entity);
            }
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";
            try
            {
                HitPoints = xp.GetInt(rootnode + "HitPoints");
            }
            catch{}
        }
    }
}
