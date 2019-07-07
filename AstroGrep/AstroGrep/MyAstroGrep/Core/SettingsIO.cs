using MyAstroGrep.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyAstroGrep.Core
{
    public class SettingsIO
    {
        public static bool Load(object classRecord, string path, string version)
        {
            try
            {
                Type recordType = classRecord.GetType();
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode rootNode = null;

                if (!File.Exists(path))
                    return false;

                xmlDoc.Load(path);
                rootNode = xmlDoc.SelectSingleNode(recordType.Name);

                if (rootNode != null)
                {
                    // check for correct version
                    if (rootNode.Attributes.Count > 0 && rootNode.Attributes["version"] != null && rootNode.Attributes["version"].Value.Equals(version))
                    {
                        XmlNodeList propertyNodes = rootNode.SelectNodes("property");

                        // check for at least 1 property node
                        if (propertyNodes != null && propertyNodes.Count > 0)
                        {
                            PropertyInfo[] properties = recordType.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

                            //have properties
                            foreach (XmlNode node in propertyNodes)
                            {
                                string name = node.Attributes["name"].Value;
                                string data = node.FirstChild.InnerText;

                                foreach (PropertyInfo property in properties)
                                {
                                    if (property.Name.Equals(name))
                                    {
                                        try
                                        {
                                            // try for type's Parse method with a string parameter
                                            MethodInfo method = property.PropertyType.GetMethod("Parse", new Type[] { typeof(string) });
                                            if (method != null)
                                            {
                                                //property contains a parse
                                                property.SetValue(classRecord, method.Invoke(property, new object[] { data }), null);
                                            }
                                            else
                                            {
                                                // just try to set the object directly
                                                if (property.CanWrite)
                                                    property.SetValue(classRecord, data, null);
                                            }
                                            method = null;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.ToString());
                                        }

                                        break;
                                    }
                                }
                            }

                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogClient.Instance.Logger.Error("Unable to load settings at {0}, version {1}, message {2}", path, version, ex.Message);
            }

            return false;
        }
    }
}
