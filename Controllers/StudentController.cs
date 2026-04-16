using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Student>> GetAllStudents()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, HoDem = "Nguyen Van", Ten = "A" },
                new Student { Id = 2, HoDem = "Tran Van", Ten = "B" }
            };
            return Ok(new
            {
                total = 100,
                data = students
            });
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id");
            }

            var student = new Student
            {
                Id = id,
                HoDem = "Nguyen Van",
                Ten = "A"
            };

            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            return Ok(student);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var package = new ExcelPackage(stream);
            var sheet = package.Workbook.Worksheets[0];

            var value = sheet.Cells[1, 1].Text;

            return Ok(value);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            return Ok(new { id, student });
        }

        [HttpDelete("{id}")]
        public ActionResult<Student> Delete(int id)
        {
            return Ok(new { message = "Deleted", id });
        }
    }
}