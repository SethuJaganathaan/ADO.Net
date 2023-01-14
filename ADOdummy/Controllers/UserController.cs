using ADOdummy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ADOdummy.Models;


namespace ADOdummy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUser()
        {
            return Ok(Users.GetAllUser());
        }
        [HttpPost("InsertUpdate")]
        public IActionResult InsertUpdate(int UserId,string UserName,DateTime DateOfBirth,int DepartmentId)
        {
            var result = Users.InsertUpdate(UserId, UserName, DateOfBirth, DepartmentId);
            return Ok(result);
        }
        [HttpPost("GETandDELETE")]
        public IActionResult GETandDELETE(int UserId,string method)
        {
            var result = Users.GETandDELETE(UserId, method);
            return Ok(result);
        }
    }
}
