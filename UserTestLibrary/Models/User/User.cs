using System.Xml.Serialization;

namespace UserTestLibrary.Models.User
{
    [XmlRoot(ElementName = "User")]
    public class UserDto
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Status")]
        public UserStatus Status { get; set; }
    }
}