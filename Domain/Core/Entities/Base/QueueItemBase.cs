using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Base
{
  public abstract class QueueItemBase : Entity
  {
    public virtual Service Service { get; set; }

    public virtual int PinCode { get; set; }
  }
}
