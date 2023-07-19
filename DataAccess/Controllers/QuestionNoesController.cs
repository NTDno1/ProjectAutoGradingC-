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
    public class QuestionNoesController : ControllerBase
    {
        private readonly ProjectPrn231Context _context;

        public QuestionNoesController(ProjectPrn231Context context)
        {
            _context = context;
        }

        // GET: api/QuestionNoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionNo>>> GetQuestionNos()
        {
          if (_context.QuestionNos == null)
          {
              return NotFound();
          }
            return await _context.QuestionNos.ToListAsync();
        }

        // GET: api/QuestionNoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<QuestionNo>>> GetQuestionNo(int id)
        {
          if (_context.QuestionNos == null)
          {
              return NotFound();
          }
            var questionNo = await _context.QuestionNos.Where(u=>u.StudentId == id).ToListAsync();  

            if (questionNo == null)
            {
                return NotFound();
            }

            return questionNo;
        }

        // PUT: api/QuestionNoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionNo(int id, QuestionNo questionNo)
        {
            if (id != questionNo.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionNo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionNoExists(id))
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

        // POST: api/QuestionNoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionNo>> PostQuestionNo(QuestionNoDTO questionNoDTO)
        {
          if (_context.QuestionNos == null)
          {
              return Problem("Entity set 'ProjectPrn231Context.QuestionNos'  is null.");
          }
            QuestionNo question = new QuestionNo()
            {
                QuestionId= questionNoDTO.QuestionId,
                StudentId = questionNoDTO.StudentId,
                QuestionStt = questionNoDTO.QuestionStt,
                Mark = questionNoDTO.Mark,
                Status = questionNoDTO.Status,
                Note= questionNoDTO.Note,
                InputTestCase= questionNoDTO.InputTestCase,
                OutputTestCase= questionNoDTO.OutputTestCase,   
                Output = questionNoDTO.Output,
            };
            _context.QuestionNos.Add(question);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/QuestionNoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionNo(int id)
        {
            if (_context.QuestionNos == null)
            {
                return NotFound();
            }
            var questionNo = await _context.QuestionNos.FindAsync(id);
            if (questionNo == null)
            {
                return NotFound();
            }

            _context.QuestionNos.Remove(questionNo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionNoExists(int id)
        {
            return (_context.QuestionNos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
