using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentsApi.Data;
using PaymentsApi.Services;

namespace PaymentsApi.Controllers
{


    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _service;
        private readonly AppDbContext _context;

        public PaymentsController(PaymentService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Payments.OrderByDescending(x => x.CreatedAt).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Payment payment)
        {
            var result = await _service.CreateAsync(payment);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Payment req)
        {
            var p = await _context.Payments.FindAsync(id);
            if (p == null) return NotFound();

            p.Amount = req.Amount;
            p.Currency = req.Currency;

            await _context.SaveChangesAsync();
            return Ok(p);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var p = await _context.Payments.FindAsync(id);
            if (p == null) return NotFound();

            _context.Payments.Remove(p);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
