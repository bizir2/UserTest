using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace UsersTest.Serializers
{
    public class XmlSerializerOutputFormatterNamespace : XmlSerializerOutputFormatter
    {
        protected override void Serialize(XmlSerializer xmlSerializer, XmlWriter xmlWriter, object value)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
            xmlSerializer.Serialize(xmlWriter, value, emptyNamespaces);
        }
    }
}