using FluentNHibernate.Mapping;

namespace Domain.Core.Database.Mapping
{
  public class QueueItemMap : SubclassMap<Entities.QueueItem>
  {
    public QueueItemMap()
    {
      DiscriminatorValue("55D6485C-E09B-4665-ADD7-720E439FECF0");
    }
  }
}
