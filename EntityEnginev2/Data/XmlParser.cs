using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace EntityEnginev2.Data
{
    public class XmlParser
    {
        public XDocument XmlFile;
        public Dictionary<string, string> Elements;

        public XmlParser(string xmlfile)
        {
            XmlFile = XDocument.Load(xmlfile);
            ParseElements();
        }

        private static string GetElementPath(XElement element)
        {
            var parent = element.Parent;
            if (parent == null)
            {
                return element.Name.LocalName;
            }
            return GetElementPath(parent) + "->" + element.Name.LocalName;
        }

        public void ParseElements()
        {
            Elements = new Dictionary<string, string>();
            foreach (var element in XmlFile.Descendants())
            {
                if (!element.HasElements)
                {
                    var key = GetElementPath(element);
                    Elements[key] = (string)element;
                }
            }
        }

        public bool CheckElement(string tag)
        {
            return Elements.ContainsKey(tag);
        }

        public string GetString(string tag)
        {
            if (!CheckElement(tag))
            {
                Error.Exception("Element " + tag + " does not exist!");
            }
            return Elements[tag];
        }

        public int GetInt(string tag)
        {
            return Convert.ToInt32(GetString(tag));
        }

        public float GetFloat(string tag)
        {
            return Convert.ToSingle(GetString(tag));
        }

        public Color GetColor(string tag)
        {
            int r, g, b, a;
            r = GetInt(tag + "->R");
            g = GetInt(tag + "->G");
            b = GetInt(tag + "->B");
            a = GetInt(tag + "->A");
            return new Color(r, g, b, a);
        }

        public Vector2 GetVector2(string tag)
        {
            if (CheckElement(tag + "->X") && CheckElement(tag + "->Y"))
            {
                float x = GetFloat(tag + "->X");
                float y = GetFloat(tag + "->Y");
                return new Vector2(x, y);
            }
            return new Vector2(GetFloat(tag));
        }

        public bool GetBool(string tag)
        {
            return Convert.ToBoolean(GetString(tag));
        }

        public Rectangle GetRectangle(string tag)
        {
            int x = GetInt(tag + "->X");
            int y = GetInt(tag + "->Y");
            int width = GetInt(tag + "->Width");
            int height = GetInt(tag + "->Height");
            return new Rectangle(x, y, width, height);
        }




        public string GetString(string tag, string def)
        {
            if (!CheckElement(tag))
            {
                return def;
            }
            return Elements[tag];
        }

        public int GetInt(string tag, int def)
        {
            return Convert.ToInt32(GetString(tag, def.ToString()));
        }

        public float GetFloat(string tag, float def)
        {
            return Convert.ToSingle(GetString(tag, def.ToString()));
        }

        public Color GetColor(string tag, Color def)
        {
            int r, g, b, a;
            r = GetInt(tag + "->R", def.R);
            g = GetInt(tag + "->G", def.B);
            b = GetInt(tag + "->B", def.G);
            a = GetInt(tag + "->A", def.A);
            return new Color(r, g, b, a);
        }

        public Vector2 GetVector2(string tag, Vector2 def)
        {
            float x = GetFloat(tag + "->X", def.X);
            float y = GetFloat(tag + "->Y", def.Y);
            return new Vector2(x, y);
        }

        public bool GetBool(string tag, bool def)
        {
            return Convert.ToBoolean(GetString(tag, def.ToString()));
        }

        public Rectangle GetRectangle(string tag, Rectangle def)
        {
            int x = GetInt(tag + "->X", def.X);
            int y = GetInt(tag + "->Y", def.Y);
            int width = GetInt(tag + "->Width", def.Width);
            int height = GetInt(tag + "->Height", def.Height);
            return new Rectangle(x, y, width, height);
        }
    }
}