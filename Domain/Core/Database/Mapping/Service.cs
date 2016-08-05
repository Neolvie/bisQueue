using FluentNHibernate.Mapping;

namespace Domain.Core.Database.Mapping
{
  public class Service : SubclassMap<Core.Entities.Service>
  {
    public Service()
    {
      DiscriminatorValue("641B9947-8523-4630-9C91-572080CD8188");
    }
  }
}
