using Newtonsoft.Json;
using UserTestLibrary.Models.User;

namespace UserTestLibrary.Models.RemoveUser
{
    public class RemoveUserResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrorId { get; set; }
        public string Msg { get; set; }
        public bool Success { get; set; }
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public UserDto User { get; set; }

        public static RemoveUserResponse ErrorResponse(string msg) => new RemoveUserResponse()
        {
            ErrorId = 2,
            Msg = msg,
            Success = false
        };
        
        public static RemoveUserResponse SuccessResponse(string msg, UserDto user) => new RemoveUserResponse()
        {
            Msg = msg,
            Success = true,
            User = user
        };
    }
}