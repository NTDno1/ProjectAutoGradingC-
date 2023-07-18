using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionDetailsController : ControllerBase
    {
        private readonly ProjectPrn231Context _context;

        public QuestionDetailsController(ProjectPrn231Context context)
        {
            _context = context;
        }

        // GET: api/QuestionDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDetail>>> GetQuestionDetails()
        {
          if (_context.QuestionDetails == null)
          {
              return NotFound();
          }
            return await _context.QuestionDetails.ToListAsync();
        }

        // GET: api/QuestionDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<QuestionDetail>>> GetQuestionDetail(string id)
        {
          if (_context.QuestionDetails == null)
          {
              return NotFound();
          }
            var questionDetail = await _context.QuestionDetails.Include(x => x.QuestionNos).Where(x => x.QuestionId == id).ToListAsync();

            if (questionDetail == null)
            {
                return NotFound();
            }

            return questionDetail;
        }

        // PUT: api/QuestionDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionDetail(string id, QuestionDetail questionDetail)
        {
            if (id != questionDetail.QuestionId)
            {
                return BadRequest();
            }

            _context.Entry(questionDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionDetailExists(id))
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

        // POST: api/QuestionDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionDetail>> PostQuestionDetail(QuestionDetail questionDetail)
        {
          if (_context.QuestionDetails == null)
          {
              return Problem("Entity set 'ProjectPrn231Context.QuestionDetails'  is null.");
          }
            _context.QuestionDetails.Add(questionDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionDetailExists(questionDetail.QuestionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestionDetail", new { id = questionDetail.QuestionId }, questionDetail);
        }

        // DELETE: api/QuestionDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionDetail(string id)
        {
            if (_context.QuestionDetails == null)
            {
                return NotFound();
            }
            var questionDetail = await _context.QuestionDetails.FindAsync(id);
            if (questionDetail == null)
            {
                return NotFound();
            }

            _context.QuestionDetails.Remove(questionDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionDetailExists(string id)
        {
            return (_context.QuestionDetails?.Any(e => e.QuestionId == id)).GetValueOrDefault();
        }
    }
}
