namespace FocusXp.Domain.Interfaces;

public interface IBaseEntity;

public interface IEntity : IBaseEntity
{
    Guid Id { get; init; }
}

public interface ICreatableEntity
{
    DateTime CreatedAt { get; init; }
}

public interface IUpdatableEntity
{
    DateTime UpdatedAt { get; set; }
}

public interface IDeletableEntity
{
    bool IsDeleted { get; set; }
}

public interface ITrackableEntity : IEntity, ICreatableEntity, IUpdatableEntity, IDeletableEntity
{
}