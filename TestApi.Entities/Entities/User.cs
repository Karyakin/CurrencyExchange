namespace TestApi.Entities.Entities;

/// <summary>
/// string? UserName<br/>
/// string? FirstName<br/>
/// int? Age<br/>
/// Address? Address<br/>
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    ///UserName ...<br/>
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    ///UserName ...<br/>
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    ///UserName ...<br/>
    /// </summary>
    public int? Age { get; set; }

    /// <summary>
    /// Address ...<br/>
    /// </summary>
    public Address? Address { get; set; }
}