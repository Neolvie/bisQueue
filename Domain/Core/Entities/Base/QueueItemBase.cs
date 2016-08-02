using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Base
{
  public abstract class QueueItemBase : Entity
  {
    public virtual DateTime Created { get; set; }

    public virtual int PinCode { get; set; }
  }
}
