using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.Domail.Handlers.CurrencyExchange;
using TestApi.Domail.Handlers.Dto.CurrencyExchange;

namespace TestApi.Controllers;
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class CurrencyExchangeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyExchangeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение курса валют от Нацбанка
    /// </summary>
    /// <param name="periodicity">опциональный </param>
    /// <remarks>
    ///     Simple Request:
    /// 
    ///         http://localhost:7000/api/CurrencyExchange/getExchangedData
    ///         or
    ///         http://localhost:7000/api/CurrencyExchange/getExchangedData?periodicity=0
    /// 
    ///     description:
    ///
    ///         periodicity = 0 - получение официального курса белорусского рубля по отношению к иностранным валютам, устанавливаемого ежедневно, на сегодня
    ///         periodicity = 1 - получение официального курса белорусского рубля по отношению к иностранным валютам, устанавливаемого ежедневно, на начало месяца
    ///         по умолчанию используется periodicity = 0(можно ничего не передавать)
    /// 
    ///     annotation:
    ///
    ///         обращаемся по API в Нацбан, получаем курсы валют, позвращаем клиенту
    /// </remarks>
    /// <response code="200">Возврат массива курсов валют</response>
    /// <response code="400">BadRequest. incorrect input parameters</response>
    /// <response code="500">Произошло исключение, некорректная работа метода</response>
    [HttpGet("getExchangedData")]
    [AllowAnonymous]
    public async Task<ActionResult<List<CurrencyExchangeDtoOut>>> GetExchangedData(int? periodicity)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await _mediator.Send(new GetExchangedDataQuery(periodicity)));
    }
}