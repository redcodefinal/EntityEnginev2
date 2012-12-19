using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace EntityEnginev2.Data
{
    public class XmlParser
    {
        public XDocument XmlFile;

        public XmlParser(string xmlfile)
        {
            XmlFile = XDocument.Load(xmlfile);
        }

        public bool CheckElement(string tag)
        {
            var elements = Regex.Split(tag, "->");

            var e = XmlFile.Element(elements[0]);
            if (e == null) return false;

            for (var i = 1; i < elements.Count(); i++)
            {
                e = e.Element(elements[i]);
                if (e == null) return false;
            }

            return true;
        }

        public string GetRootNode()
        {
            XmlReader xr = XmlFile.CreateReader();
            xr.MoveToContent();
            return xr.Name;
        }

        public string GetChildNode(string tag)
        {
            if (!CheckElement(tag))
            {
                Error.Exception("Element " + tag + " does not exist!");
            }

            var elements = Regex.Split(tag, "->");
            var e = XmlFile.Element(elements[0]);

            for (var i = 1; i < elements.Count(); i++)
            {
                e = e.Element(elements[i]);
            }

            IEnumerable<XElement> xe = e.Descendants();
            foreach (var xElement in xe)
            {
                return xElement.Name.ToString();
            }
            Error.Exception("No child nodes in " + tag);
            return null;
        }

        public string GetString(string tag)
        {
            if (!CheckElement(tag))
            {
                Error.Exception("Element " + tag + " does not exist!");
            }

            var elements = Regex.Split(tag, "->");
            var e = XmlFile.Element(elements[0]);

            for (var i = 1; i < elements.Count(); i++)
            {
                e = e.Element(elements[i]);
            }
            return e.Value;
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
            CheckElement(tag + "->R");
            r = GetInt(tag + "->R");
            g = GetInt(tag + "->G");
            b = GetInt(tag + "->B");
            a = GetInt(tag + "->A");
            return new Color(r,g,b,a);
        }

        public Vector2 GetVector2(string tag)
        {
            float x = GetFloat(tag + "->X");
            float y = GetFloat(tag + "->Y");
            return new Vector2(x,y);
        }

        public bool GetBool(string tag)
        {
            return Convert.ToBoolean(GetString(tag));
        }
    }
}
