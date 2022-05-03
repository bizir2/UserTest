using System.Xml.Serialization;

namespace UserTestLibrary.Models.CreateUser
{
    [XmlInclude(typeof(ResponseSuccess))]
    [XmlInclude(typeof(ResponseError))]
    public class ResponseDto
    {
        [XmlAttribute("Success")]
        public bool Success { get; set; }
        [XmlAttribute("Status")]
        public int Status { get; set; }
    }
}