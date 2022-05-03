using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersTest.Interfaces;


namespace UsersTest.Controllers;

public class PublicController : Controller
{
    readonly private IUserDbOrCache _userDbOrCache;
    public PublicController(IUserDbOrCache userDbOrCache)
    {
        _userDbOrCache = userDbOrCache;
    }

    [HttpGet]
    public async Task<IActionResult> UserInfo([FromQuery]int id)
    {
        var user = await _userDbOrCache.ById(id);
        return View(user);
    }
}