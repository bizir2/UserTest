using System;

namespace UserTestConsole.Attribytes
{
    public class ClientAttribute : Attribute
    {
        public string Name;
        public int Param;
        public Type Type;
        
        public ClientAttribute(string name, int param, Type type)
        {
            Name = name;
            Param = param;
            Type = type;
        }
    }
}