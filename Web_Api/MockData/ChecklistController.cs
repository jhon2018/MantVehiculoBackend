using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Api.DTOs; // O el namespace real de ChecklistDTO

namespace Web_Api.MockData
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var data = System.IO.File.ReadAllText("Data/checklists.json");
            var checklist = JsonConvert.DeserializeObject<List<ChecklistDTO>>(data);
            return Ok(checklist);
        }
    }
}
