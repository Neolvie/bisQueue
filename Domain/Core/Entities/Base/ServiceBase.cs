using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Base
{
  public abstract class ServiceBase : Entity
  {
    public virtual int Duration { get; set; }
  }
}
