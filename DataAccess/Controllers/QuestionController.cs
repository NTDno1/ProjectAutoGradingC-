using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ProjectPrn231Context _context;

        public QuestionController(ProjectPrn231Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionDetails()
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            return await _context.Questions.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestionDetail(QuestionDTO question)
        {
            if (_context.QuestionDetails == null)
            {
                return Problem("Entity set 'ProjectPrn231Context.Question'  is null.");
            }
            Question add = new()
            {
                StudentId = 1,
                QuestionId = question.QuestionId,
                TotalMark = question.TotalMark,
                CreateDate= DateTime.Now,
                UpdateDate = DateTime.Now,
            };
            _context.Questions.Add(add);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionExists(question.QuestionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(question);
        }
        private bool QuestionExists(string id)
        {
            return (_context.QuestionDetails?.Any(e => e.QuestionId == id)).GetValueOrDefault();
        }
    }
}
