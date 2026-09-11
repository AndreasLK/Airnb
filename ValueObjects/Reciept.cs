using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Domain.ValueObjects
{
    public record Reciept
    {
        public Money StartPrice { get; private set; }
        public string Service {  get; private set; } //vi har diskuteret til vores røvhuller blødte

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
    }
}
