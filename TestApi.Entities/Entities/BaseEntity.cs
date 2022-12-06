namespace TestApi.Entities.Entities;
/// <summary>
/// Guid? Id<br/>
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Autogenerate Id for new entity<br/>
    /// </summary>
    public Guid? Id { get; init; } = Guid.NewGuid();
}