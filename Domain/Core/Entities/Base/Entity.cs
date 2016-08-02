using Domain.Core.Database;

namespace Domain.Core.Entities.Base
{
  /// <summary>
  /// Базовый класс сущности.
  /// </summary>
  public abstract class Entity
  {
    #region Поля и свойства

    /// <summary>
    /// Id сущности.
    /// </summary>
    public virtual int Id { get; protected set; }

    /// <summary>
    /// Guid типа сущности.
    /// </summary>
    public virtual string TypeGuid { get; }

    /// <summary>
    /// Название типа сущности.
    /// </summary>
    public abstract string TypeName { get; }

    /// <summary>
    /// Имя.
    /// </summary>
    public virtual string Name { get; set; }

    #endregion

    #region Методы

    /// <summary>
    /// Сохранить сущность в базе данных.
    /// </summary>
    public virtual void Save()
    {
      Repository.Save(this);

      OnEntitySaved();
    }

    /// <summary>
    /// Обновить значения сущности из базы данных.
    /// </summary>
    public virtual void Update()
    {
      if (Id == 0)
        return;

      Environment.Session.Refresh(this);

      OnEntityUpdated();
    }

    /// <summary>
    /// Удалить сущность из базы данных.
    /// </summary>
    public virtual void Delete()
    {
      Repository.Delete(this);

      OnEntityDeleted();
    }

    private void OnEntitySaved()
    {
      EntitySaved?.Invoke();
    }

    private void OnEntityDeleted()
    {
      EntityDeleted?.Invoke();
    }

    private void OnEntityUpdated()
    {
      EntityUpdated?.Invoke();
    }

    #endregion

    #region События

    public virtual event EntityEventHandler EntitySaved;
    public virtual event EntityEventHandler EntityDeleted;
    public virtual event EntityEventHandler EntityUpdated;

    public delegate void EntityEventHandler();

    #endregion

    #region Базовый класс

    public override bool Equals(object obj)
    {
      var entity = obj as Entity;

      if (entity == null)
        return false;

      return this.TypeGuid == entity.TypeGuid && this.Id == entity.Id;
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (Id * 397) ^ (TypeGuid?.GetHashCode() ?? 0);
      }
    }

    #endregion
  }
}