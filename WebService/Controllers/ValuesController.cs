using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Core.Database;

namespace WebService.Controllers
{
  public class ValuesController : ApiController
  {
    // GET api/values
    public List<Domain.Core.Entities.QueueItem> Get()
    {
      return Repository.Get<Domain.Core.Entities.QueueItem>().ToList();
    }

    // GET api/values/5
    public string Get(int id)
    {
      return "value";
    }

    // POST api/values
    public void Post([FromBody]Domain.Core.Entities.QueueItem value)
    {
      value.Save();
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }
  }
}
