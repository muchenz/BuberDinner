using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common.Models;
public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
{
    protected AggregateRoot(TId id) : base(id)
    {
    }
    protected AggregateRoot()
    {

    }
}


public abstract class AggregateRoot2<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id { get; protected set; }
    protected AggregateRoot2(TId id) 
    {
        Id = id;
    }
#pragma warning disable CS8618
    protected AggregateRoot2()
    {

    }
#pragma warning restore CS8618

}
