namespace Recipes.Application.Entities;

public abstract class Entity(Guid id)
{
    public Guid Id { get; private set; } = id;
    public bool Deleted { get; private set; }

    public void SetId(Guid id)
    {
        if (id == Guid.Empty) throw new ArgumentException($"{nameof(id)} may not be empty");

        Id = id;
    }

    public void Delete()
    {
        Deleted = true;
    }

    public void Undelete()
    {
        Deleted = false;
    }
}