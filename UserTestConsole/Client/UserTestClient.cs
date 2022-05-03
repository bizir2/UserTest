using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using UserTestConsole.Attribytes;
using UserTestConsole.Mapping;
using UserTestLibrary.Models.CreateUser;
using UserTestLibrary.Models.RemoveUser;
using UserTestLibrary.Models.User;

namespace UserTestConsole.Client
{
    public class UserTestClient
    {
        readonly private string _baseUrl = "https://localhost:7125";
        readonly private string _user = "Login";
        readonly private string _authKey = "Pass";
        readonly private IMapper _mapper;

        [Client("CreateUser", 3, typeof(CreateUser))]
        public Task<string> CreateUser(CreateUser user)
        {
            return PostXml("CreateUser", user);
        }
        
        [Client("RemoveUser", 1, typeof(RemoveUser))]
        public Task<string> RemoveUser(RemoveUser user)
        {
            return PostJson("RemoveUser", user);
        }
        
        [Client("SetStatus", 2, typeof(UserDto))]
        public Task<string> SetStatus(UserDto user)
        {
            return PostJsonData("SetStatus", user);
        }

        private async Task<string> ClientSend(HttpRequestMessage message)
        {
            AuthenticateRequest(message);
            
            var client = new HttpClient();
            var responseMessage = await client.SendAsync(message);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }
            
            return await responseMessage.Content.ReadAsStringAsync();
        }
        private void AuthenticateRequest(HttpRequestMessage message)
        {
            var credentials = "Basic " + Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{_user}:{_authKey}"));

            message.Headers.Add("Authorization", credentials);
        }
        
        private Task<string> PostJson<T>(string path, T body) 
            where T : class
        {
            var url = $"{_baseUrl}/{path}";
            var json = JsonConvert.SerializeObject(body);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
                { Content = new StringContent(json, Encoding.UTF8, "application/json") };
            
            return ClientSend(request);
        }
        
        private Task<string> PostJsonData<T>(string path, T body) 
            where T : class
        {
            
            var url = $"{_baseUrl}/{path}";
            
            var json = JsonConvert.SerializeObject(body);
            var parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                .Where(p => !string.IsNullOrEmpty(p.Value));
            
            var request = new HttpRequestMessage(HttpMethod.Post, url)
                { Content = new FormUrlEncodedContent(parameters) };
            
            return ClientSend(request);
        }
        
        private Task<string> PostXml<T>(string path, T body) 
            where T : class
        {
            var url = $"{_baseUrl}/{path}";
            var serializedBody = SerializeToXmlRequestBody(body);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
                { Content = new StringContent(serializedBody, Encoding.UTF8, "application/xml") };
            
            return ClientSend(request);
        }
        
        private string SerializeToXmlRequestBody<T>(T data) where T : class
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            var xml = "";
            using(var sww = new StringWriter())
            {
                using(XmlWriter writer = XmlWriter.Create(sww, settings))
                {
                    xsSubmit.Serialize(writer, data, emptyNamespaces);
                    xml = sww.ToString();
                }
            }

            return xml;
        }
    }
}