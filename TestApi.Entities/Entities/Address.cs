namespace TestApi.Entities.Entities;
/// <summary>
/// string? Country<br/>
/// string? City<br/>
/// string? Street<br/>
/// int? House<br/>
/// int? Flat<br/>
/// </summary>
public class Address : BaseEntity
{
    /// <summary>
    /// Country ...<br/>
    /// </summary>
    public string? Country { get; set; }
    /// <summary>
    /// City ...<br/>
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// Street ...<br/>
    /// </summary>
    public string? Street { get; set; }
    /// <summary>
    /// House ...<br/>
    /// </summary>
    public int? House { get; set; }
    /// <summary>
    /// Flat ...<br/>
    /// </summary>
    public int? Flat { get; set; }
}