using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Environment = Domain.Core.Database.Environment;

namespace Domain.Core.Database
{
  public static class Repository
  {
    public static IQueryable<T> Get<T>() where T : Entities.Base.Entity
    {
      return Environment.Session.Query<T>();
    }

    public static void SaveAll<T>(this IEnumerable<T> objects) where T : Core.Entities.Base.Entity
    {
      var objectList = objects.ToList();
      if (!objectList.Any())
        return;

      var session = Environment.Session;
      var failed = false;
      
      using (var transact = session.BeginTransaction())
      {
        try
        {
          foreach (var entity in objectList)
          {
            if (entity.Id == 0)
              session.Save(entity);
            else
              session.Update(entity);
          }

          transact.Commit();
        }
        catch (Exception)
        {
          failed = true;
        }
      }

      if (!failed) return;
      try
      {
        Environment.Session.Dispose();
      }
      finally
      {
        Environment.Session = Environment.OpenSession();
      }
    }

    public static void Save<T>(this T entity) where T : Core.Entities.Base.Entity
    {
      SaveAll(new [] {entity});
    }

    public static void DeleteAll<T>(this IEnumerable<T> objects) where T : Core.Entities.Base.Entity
    {
      var objectList = objects.ToList();
      if (!objectList.Any())
        return;

      var session = Environment.Session;
      var failed = false;

      using (var transact = session.BeginTransaction())
      {
        try
        {
          foreach (var entity in objectList.Where(entity => entity.Id != 0))
            session.Delete(entity);

          transact.Commit();
        }
        catch (Exception)
        {
          failed = true;
        }
      }

      if (!failed) return;
      try
      {
        Environment.Session.Dispose();
      }
      finally
      {
        Environment.Session = Environment.OpenSession();
      }
    }

    public static void Delete<T>(this T entity) where T : Core.Entities.Base.Entity
    {
      DeleteAll(new [] { entity });
    }
  }
}