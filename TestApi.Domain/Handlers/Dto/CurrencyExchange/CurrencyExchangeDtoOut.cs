using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestApi.Domail.Handlers.Dto.CurrencyExchange;

public class CurrencyExchangeDtoOut
{
    [JsonPropertyName("Cur_ID")]
    public int CurId { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Release Date")]
    public System.DateTime Date { get; set; }

    [JsonPropertyName("Cur_Abbreviation")]
#pragma warning disable CS8618
    public string CurAbbreviation { get; set; }
#pragma warning restore CS8618

    [JsonPropertyName("Cur_Scale")]
    public int CurScale { get; set; }

    [JsonPropertyName("Cur_Name")]
#pragma warning disable CS8618
    public string CurName { get; set; }
#pragma warning restore CS8618

    [JsonPropertyName("Cur_OfficialRate")]
    public decimal? CurOfficialRate { get; set; }
}