using System.Collections.Generic;
using System.Linq;

namespace System.Xml.Linq
{
    public static class XExtensions
    {
        public static XElement AddElement(this XContainer container, XElement content, String keyName = "name", bool updateOnExistance = false)
        {
            var exsistanceElement = container.Elements(content.Name).Where(e => e.Attributes(keyName).Count() != 0 && e.Attributes(keyName).First().Value == content.Attributes(keyName).First().Value);

            if (exsistanceElement.Count() == 0)
            {
                container.Add(content);
            }
            else
            {
                if (updateOnExistance)
                {
                    exsistanceElement.First().ReplaceWith(content);
                }
            }

            return container.Descendants().Where(e => e.Attributes(keyName).Count() != 0 && e.Name == content.Name && e.Attributes(keyName).First().Value == content.Attributes(keyName).First().Value).First();
        }

        public static IEnumerable<String> ElementsValue(this XContainer container, XName name)
        {
            var elements = container.Elements(name);

            if (elements.Count() > 0)
            {
                return elements.Select(e=> e.Value.Trim());
            }
            else
            {
                return null;
            }
        }

        public static string ElementValue(this XContainer container, XName name)
        {
            var elementsValue = container.ElementsValue(name);

            if (elementsValue.Count() > 0)
            {
                return elementsValue.First();
            }
            else
            {
                return String.Empty;
            }
        }

        public static IEnumerable<XElement> ElementsByName(this XContainer container, string name)
        {
            return container.Elements().Where(e => (e.Attribute("Name") != null && e.Attribute("Name").Value == name) || (e.Attribute("name") != null && e.Attribute("name").Value == name));
        }

        public static XElement ElementByName(this XContainer container, string name)
        {
            var elements = container.ElementsByName(name);

            if (elements.Count() > 0)
            {
                return elements.First();
            }
            else
            {
                return null;
            }
        }

        public static string ValueByName(this XContainer container, string name)
        {
            XElement subElement = container.ElementByName(name);

            if (subElement != null)
            {
                return subElement.Value.Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        public static XElement GetElement(this XContainer container, XName name, bool ignoreCase = false, bool firstChild = false)
        {
            if (container.Descendants(name).Count() != 0)
            {
                return container.Descendants(name).First();
            }
            else
            {
                if (!firstChild)
                {
                    container.Add(new XElement(name));
                }
                else
                {
                    container.AddFirst(new XElement(name));
                }

                return container.Descendants(name).First();
            }
        }

        public static IEnumerable<XElement> Elements(this XContainer container, XName name, bool ignoreCase)
        {
            if (!ignoreCase)
            {
                return container.Elements(name);
            }

            return container.Elements().Where(e => e.Name.LocalName.ToString().ToLowerInvariant() == name.ToString().ToLowerInvariant());
        }

        public static XElement Element(this XContainer container, XName name, bool ignoreCase)
        {
            return container.Elements(name, ignoreCase).FirstOrDefault();
        }

        public static XAttribute GetAttribute(this XElement element, XName name, bool ignoreCase = false, bool firstChild = false)
        {
            if (element.Attributes(name, ignoreCase).Count() == 0)
            {
                if (!firstChild)
                {
                    element.Add(new XAttribute(name, ""));
                }
                else
                {
                    element.AddFirst(new XAttribute(name, ""));
                }
            }
            
            return element.Attributes(name, ignoreCase).First();
        }

        public static IEnumerable<XAttribute> Attributes(this XElement element, XName name, bool ignoreCase)
        {
            if (!ignoreCase)
            {
                return element.Attributes(name);
            }

            return element.Attributes().Where(e => e.Name.LocalName.ToString().ToLowerInvariant() == name.ToString().ToLowerInvariant());
        }

        public static XAttribute Attribute(this XElement element, XName name, bool ignoreCase)
        {
            return element.Attributes(name, ignoreCase).FirstOrDefault();
        }

        public static XDocument RemoveAllNamespaces(this XDocument document)
        {
            if (document != null)
            {
                XDocument xDocument = new XDocument();
                xDocument.Add(document.Root.RemoveAllNamespaces());

                return xDocument;
            }

            return null;
        }

        public static XElement RemoveAllNamespaces(this XElement element)
        {
            if (!element.HasElements)
            {
                XElement xElement = new XElement(element.Name.LocalName);
                xElement.Value = element.Value;

                foreach (XAttribute attribute in element.Attributes())
                {
                    xElement.Add(attribute);
                }

                return xElement;
            }

            return new XElement(element.Name.LocalName, element.Elements().Select(el => RemoveAllNamespaces(el)));
        }
    }
}
