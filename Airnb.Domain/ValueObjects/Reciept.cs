using Airnb.Domain.Exceptions;

namespace Airnb.Domain.ValueObjects
{
    public record Reciept
    {
        public Money StartPrice { get; private set; }
        public string Service { get; private set; } //vi har diskuteret til vores røvhuller blødte

        public Reciept()
        {
            StartPrice = null!;
            Service = null!;
        }
        public Reciept(Money money, string service)
        {
            StartPrice = money;
            Service = service;
        }

        public static Reciept Create(Money pricePerDay, TimeRange timeRange, string service)
        {
            var nights = (timeRange.End.Date - timeRange.Start.Date).Days;

            if (nights < 1)
                throw new DomainException("En booking skal være mindst én nat.");

            var price = new Money(pricePerDay.Amount * nights, pricePerDay.Currency);
            return new Reciept(price, service);
        }
    }
}
