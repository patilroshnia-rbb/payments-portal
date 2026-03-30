using FluentValidation;

namespace PaymentsApi.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0);

            RuleFor(x => x.Currency)
                .Must(x => new[] { "USD", "EUR", "INR", "GBP" }.Contains(x))
                .WithMessage("Invalid currency");

            RuleFor(x => x.ClientRequestId)
                .NotEmpty();
        }
    }
}
