using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerSales/5
        [HttpGet("{customerId}")]
        [Route("api/CustomerSales/{customerId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(int customerId)
        {
            return await _context.Transactions.Where(transaction => transaction.CustomerId == customerId).ToListAsync();

        }

    }
}
