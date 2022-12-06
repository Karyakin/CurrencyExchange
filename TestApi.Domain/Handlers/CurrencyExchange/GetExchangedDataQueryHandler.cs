using System.Text.Json;
using MediatR;
using TestApi.Domail.Handlers.Dto.CurrencyExchange;
using TestApi.Utils.Settings;

namespace TestApi.Domail.Handlers.CurrencyExchange;

public class GetExchangedDataQueryHandler : IRequestHandler<GetExchangedDataQuery, List<CurrencyExchangeDtoOut>>
{
    public async Task<List<CurrencyExchangeDtoOut>> Handle(GetExchangedDataQuery request, CancellationToken cancellationToken)
    {
        var httpClient = new HttpClient();
        try
        {
            if (request.Periodicity is null or < 0 or > 1)
                request.Periodicity = 0;
            
            var str =
                $"{ApplicationParams.NationalBankBaseUrl}exrates/rates?ondate={DateTime.Now.ToString("yyyy-MM-dd")}&periodicity={request.Periodicity}";
            var responseString = await httpClient.GetStringAsync(str, cancellationToken);
            var catalogCurrencyDto =
                (JsonSerializer.Deserialize<IEnumerable<CurrencyExchangeDtoOut>>(responseString) ??
                 Array.Empty<CurrencyExchangeDtoOut>())
                .Where(x => x.CurId is 431 or 456 or 451 or 462);

           return catalogCurrencyDto.ToList();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}