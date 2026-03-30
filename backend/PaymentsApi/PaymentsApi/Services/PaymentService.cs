using PaymentsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace PaymentsApi.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreateAsync(Payment request)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            // Idempotency check
            var existing = await _context.Payments
                .FirstOrDefaultAsync(x => x.ClientRequestId == request.ClientRequestId);

            if (existing != null)
                return existing;

            var today = DateTime.UtcNow.Date;

            var count = await _context.Payments
                .CountAsync(x => x.CreatedAt.Date == today);

            var reference = $"PAY-{today:yyyyMMdd}-{(count + 1).ToString("D4")}";

            var payment = new Payment
            {
                Amount = request.Amount,
                Currency = request.Currency,
                ClientRequestId = request.ClientRequestId,
                CreatedAt = DateTime.UtcNow,
                Reference = reference
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            await tx.CommitAsync();

            return payment;
        }
    }
}
