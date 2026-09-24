using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
