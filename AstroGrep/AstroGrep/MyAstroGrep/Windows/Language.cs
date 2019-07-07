using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyAstroGrep.Windows
{
    public class Language
    {
        private static XmlNode XmlGenericNode = null;

        public static string GetGenericText(string name)
        {
            return GetGenericText(name, string.Empty);
        }

        public static string GetGenericText(string name, string defaultValue)
        {
            if (XmlGenericNode != null)
            {
                XmlNode node = XmlGenericNode.SelectSingleNode("text[@name='" + name + "']");

                if (node != null && node.Attributes["value"] != null)
                    return node.Attributes["value"].Value;
            }

            return defaultValue;
        }

        public static void Load(string language)
        {
            // todo
        }
    }
}
