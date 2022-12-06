using MediatR;
using TestApi.Domail.Handlers.Dto.CurrencyExchange;

namespace TestApi.Domail.Handlers.CurrencyExchange;
/// <summary>
/// Отправка запроса на API нацбанка за курсами валют
/// </summary>
public class GetExchangedDataQuery : IRequest<List<CurrencyExchangeDtoOut>>
{
    public GetExchangedDataQuery(int? periodicity)
    {
        Periodicity = periodicity;
    }

    public int? Periodicity { get; set; }
}