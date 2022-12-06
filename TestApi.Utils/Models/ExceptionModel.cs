using System.Text.Json;

namespace TestApi.Utils.Models;

/// <summary>
/// int StatusCode<br/>
/// string? Message<br/>
/// public override string ToString()<br/>
/// </summary>
public class ExceptionModel
{
    public int StatusCode { get; init; }
    public string? Message { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}