using System;
using System.Collections.Generic;
using System.Linq;
using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Components.Collision
{
    public class PerColorCollision : Collision
    {
        public List<Color> Colors = new List<Color>();
        private Texture2D _texture;
        public Texture2D Texture
        {
            get { return _texture; }
            set 
            {
                _texture = value;
                ColorData = _texture.GetData();
            }
        }
        public Color[,] ColorData { get; private set; }

        public PerColorCollision(Entity e, string name) : base(e, name)
        {
            ColorsColliedWith = new List<Color>();
        }

        public PerColorCollision(Entity e, string name, Texture2D texture) : base(e, name)
        {
            Texture = texture;
            ColorsColliedWith = new List<Color>();
        }

        public List<Color> ColorsColliedWith { get; private set; }

        public override bool TestCollision(Entity e)
        {
            if (BoundingBox.Intersects(e.GetComponent<Collision>().BoundingBox))
            {
                //Get the area of intersection
                var intersection = new Rectangle();
                intersection.Y = Math.Max(BoundingBox.Top, e.GetComponent<Collision>().BoundingBox.Top);
                intersection.Height = Math.Min(BoundingBox.Bottom, e.GetComponent<Collision>().BoundingBox.Bottom) -
                                      intersection.Y;
                intersection.X = Math.Max(BoundingBox.Left, e.GetComponent<Collision>().BoundingBox.Left);
                intersection.Width = Math.Min(BoundingBox.Right, e.GetComponent<Collision>().BoundingBox.Right) -
                                     intersection.X;
                foreach (var color in Colors)
                {
                    for (int y = intersection.Y; y < intersection.Bottom; y++)
                    {
                        for (int x = intersection.X; x < intersection.Right; x++)
                        {
                            //We subtract our bounding boxes to set the position back to relative, since the area of intersection would be the at the absolute positions
                            Color color1 = ColorData[(x - BoundingBox.Left), (y - BoundingBox.Top)];
                            Color color2 =
                                e.GetComponent<PerColorCollision>().ColorData[
                                    (x - e.GetComponent<Collision>().BoundingBox.Left),
                                    (y - e.GetComponent<Collision>().BoundingBox.Top)];
                            if (color1.A != 0 && color2 == color)
                            {
                                ColorsColliedWith.Add(color);
                                break;
                            }
                        }
                        if (ColorsColliedWith.Contains(color))
                            break;
                    }
                }
            }
            if (ColorsColliedWith.Count > 0)
                return true;
            return false;
        }

        public override void Update()
        {
            ColorsColliedWith.Clear();
            base.Update();
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);

            string rootnode = path + "->" + Name;
            if (xp.CheckElement(rootnode + "->Texture"))
            {
                Texture =
                    Entity.StateRef.GameRef.Game.Content.Load<Texture2D>(xp.GetString(rootnode + "->Texture",
                                                                                      "TEXTURENOTSET"));
            }

            if (xp.CheckElement(rootnode + "->Colors"))
            {
                string[] test = xp.GetAllDesendents(rootnode + "->Colors");
                foreach (string s in test)
                {
                    Colors.Add(xp.GetColor(rootnode + "->Colors->" + s));
                }
            }
        }
    }
}