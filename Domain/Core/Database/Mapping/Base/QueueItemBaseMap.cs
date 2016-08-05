﻿using FluentNHibernate.Mapping;

namespace Domain.Core.Database.Mapping.Base
{
  public class QueueItemBaseMap : ClassMap<Entities.Base.QueueItemBase>
  {
    public QueueItemBaseMap()
    {
      Table("Queue_Items");
      DiscriminateSubClassesOnColumn("TypeGuid");

      Id(x => x.Id);
      Map(x => x.TypeGuid).Column("TypeGuid").Not.Insert().Not.Update();
      Map(x => x.Status).Default("1");
      Map(x => x.Name);
      Map(x => x.Created).Default("getdate()").Not.Update();

      Id(x => x.Id);
      Map(x => x.PinCode);
      References(x => x.Service);
    }
  }
}
