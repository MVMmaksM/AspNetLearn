using AuthTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthTest.Controllers;

public class UserController(IUserService userService) : ControllerBase
{
    
}