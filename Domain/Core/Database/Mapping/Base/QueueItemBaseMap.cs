using FluentNHibernate.Mapping;

namespace Domain.Core.Database.Mapping.Base
{
  public class QueueItemBaseMap : ClassMap<Entities.Base.QueueItemBase>
  {
    public QueueItemBaseMap()
    {
      Table("Queue_Items");
      Id(x => x.Id);
      Map(x => x.TypeGuid).Column("TypeGuid").Not.Insert().Not.Update();
      Map(x => x.Name);
      Map(x => x.Created);
      Map(x => x.PinCode);

      DiscriminateSubClassesOnColumn("TypeGuid");
    }
  }
}
