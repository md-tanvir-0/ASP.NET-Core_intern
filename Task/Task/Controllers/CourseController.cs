using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Task.EFc.Tables;

namespace Task.EFc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly TaskContext _context;

        public CourseController(TaskContext context)
        {
            _context = context;
        }

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"CALL insert_course({course.Name}, {course.Description})");

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }




        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.FromSqlRaw("SELECT * FROM get_all_courses()").ToListAsync();
        }

        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FromSqlRaw("SELECT * FROM get_course_by_id({0})", id).FirstOrDefaultAsync();

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }


        // PUT: api/Course/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            await _context.Database.ExecuteSqlInterpolatedAsync($"CALL update_course({course.Id}, {course.Name}, {course.Description})");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);

            await _context.Database.ExecuteSqlInterpolatedAsync($"CALL delete_course({id})");

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
