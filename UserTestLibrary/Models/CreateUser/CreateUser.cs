using System.Xml.Serialization;
using UserTestLibrary.Models.User;

namespace UserTestLibrary.Models.CreateUser
{
    [XmlRoot(ElementName = "Request")]
    public class CreateUser
    {
        [XmlElement(ElementName = "user")]
        public UserDto User { get; set; }
    }
}