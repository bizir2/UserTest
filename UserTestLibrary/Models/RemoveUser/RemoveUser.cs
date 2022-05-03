using Newtonsoft.Json;

namespace UserTestLibrary.Models.RemoveUser
{
    public class RemoveUser
    {
        [JsonProperty("RemoveUser")]
        public RemoveUserId RemoveUserId { get; set; }
    }
    
    public class RemoveUserId
    {
        public int Id { get; set; }
    }
}