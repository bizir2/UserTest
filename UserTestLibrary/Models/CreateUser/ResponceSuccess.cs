using System.Xml.Serialization;
using UserTestLibrary.Models.User;

namespace UserTestLibrary.Models.CreateUser
{
    [XmlRoot(ElementName = "Response")]
    public class ResponseSuccess : ResponseDto
    {
        [XmlElement(ElementName = "User")]
        public UserDto User { get; set; }

        public static ResponseDto Create(UserDto user)
            => new ResponseSuccess()
            {
                Status = 0,
                Success = true,
                User = user
            };
    }
}