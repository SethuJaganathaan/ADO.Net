using ADOdummy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADOdummy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            List<Department> departments = Department.GetAllDepartment();
            return Ok(departments);
        }
        [HttpPost("InsertUpdate")]
        public IActionResult InsertUpdate(int DepartmentId,string DepartmentName,string DepartmentInchargeName)
        {
            string result = Department.InsertUpdate(DepartmentId,DepartmentName,DepartmentInchargeName);
            return Ok(result);
        }
        [HttpPost("GETandDELETE")]
        public IActionResult GETandDELETE(int DepartmentId,string Method)
        {
            var result = Department.GETandDelete(DepartmentId, Method);
            return Ok(result);
        }
        [HttpGet("GetIncharge")]
        public IActionResult GetIncharge()
        {
            var result = Department.GetDepartmentIncharge();
            return Ok(result);
        }
    }
}
