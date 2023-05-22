using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common.Models;
public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }

    public void ClearDomainEvents();
}
