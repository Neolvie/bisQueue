using FluentNHibernate.Mapping;

namespace Domain.Core.Database.Mapping.Base
{
  public class ServiceBaseMap : ClassMap<Core.Entities.Base.ServiceBase>
  {
    public ServiceBaseMap()
    {
      Table("Services");
      DiscriminateSubClassesOnColumn("TypeGuid");
      
      Id(x => x.Id);
      Map(x => x.TypeGuid).Column("TypeGuid").Not.Insert().Not.Update();
      Map(x => x.Status).Default("1");
      Map(x => x.Name);
      Map(x => x.Created).Default("getdate()").Not.Update();

      Map(x => x.Duration);
    }
  }
}
