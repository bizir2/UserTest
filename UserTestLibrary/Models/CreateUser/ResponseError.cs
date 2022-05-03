using System.Xml.Serialization;

namespace UserTestLibrary.Models.CreateUser
{
    [XmlRoot(ElementName = "Response", Namespace = "")]
    public class ResponseError: ResponseDto
    {
        [XmlElement("ErrorMsg")] 
        public string ErrorMsg { get; set; }

        public static ResponseError Create(string message)
            => new ResponseError()
            {
                Status = 1,
                Success = false,
                ErrorMsg = message
            };
    }
}