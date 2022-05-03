using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using UserTestConsole.Attribytes;
using UserTestConsole.Client;
using UserTestConsole.Mapping;

Console.Write($"Введите команду : ");


var mapper = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
}).CreateMapper();

var input = await Console.In.ReadLineAsync();

var param = input.Split(" ");

if (param.Length == 0)
{
    Console.WriteLine($"Пусто");
    return;
}

var p = typeof(UserTestClient).GetMethods()
    .FirstOrDefault(x => x.GetCustomAttribute<ClientAttribute>()?.Name.Contains(param[0], StringComparison.CurrentCultureIgnoreCase) == true);

if (p == null)
{
    Console.WriteLine("Метод не найден");
    return;
}

var attribute = p.GetCustomAttribute<ClientAttribute>();
if (attribute!.Param == param.Length)
{
    Console.WriteLine($"Параметров должно быть {attribute.Param}");
    return;
}
var client = new UserTestClient();
var user = mapper.Map(param.Skip(1).ToArray(), Activator.CreateInstance(attribute.Type));
var invoke = await (Task<string>)p.Invoke(client, new []{ user });
Console.WriteLine($"Результат : {invoke}");
