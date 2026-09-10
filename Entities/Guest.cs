using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Guest : AggregateRoot
    {
        public Guest() { }

        public Guid UserId { get; private set; }

        public Address Address { get; private set; }

        public Guest(Guid userId, Address address)
        {
            userId = UserId;
            address = Address;
        }

    }
}
