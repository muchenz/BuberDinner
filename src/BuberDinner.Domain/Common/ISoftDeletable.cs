using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common;
public interface ISoftDeletable // probably bad design 
{
    bool IsDeleted { get; set; }
    DateTime? DeletedOnTime { get; set; }
}
