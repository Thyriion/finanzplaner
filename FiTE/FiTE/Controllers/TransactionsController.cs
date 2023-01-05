using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiTE.Data;
using FiTE.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FiTE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction(
            [FromQuery] String? date, 
            [FromQuery] String? purpose,
            [FromQuery] String? amount
        )
        {
            if (_context.Transaction == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transaction.ToListAsync();

            switch (date)
            {
                case "asc":
                    transactions = transactions.OrderBy(x => x.Date).ToList();
                    break;
                case "desc":
                    transactions = transactions.OrderByDescending(x => x.Date).ToList();
                    break;
            }

            switch (purpose)
            {
                case "asc":
                    transactions = transactions.OrderBy(x => x.Purpose).ToList();
                    break;
                case "desc":
                    transactions = transactions.OrderByDescending(x => x.Purpose).ToList();
                    break;
            }

            switch (amount)
            {
                case "asc":
                    transactions = transactions.OrderBy(x => x.Amount).ToList();
                    break;
                case "desc":
                    transactions = transactions.OrderByDescending(x => x.Amount).ToList();
                    break;
            }

            return transactions;
        }

        // GET: Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            if (_context.Transaction == null)
            {
                return NotFound();
            }
            var transaction = await _context.Transaction.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.ID)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            if (_context.Transaction == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transaction'  is null.");
            }
            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.ID }, transaction);
        }

        // DELETE: Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if (_context.Transaction == null)
            {
                return NotFound();
            }
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return (_context.Transaction?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
